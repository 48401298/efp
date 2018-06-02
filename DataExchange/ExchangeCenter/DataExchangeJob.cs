using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Quartz;
using LAF.Data;
using System.Collections.Concurrent;
using ExchangeEntity;
using System.Threading;
using MongoDB.Driver;
using MongoDB.Bson;
using LAF.MongoDBClient;
using System.Data;
using LAF.Common.Serialization;
using System.ServiceModel;

namespace ExchangeCenter
{
    public class DataExchangeJob : IJob
    {
        //每次从数据库取记录数量
        //public static int eachHandleCount = 100;

        //每次从数据库取记录数量
        public static int eachHandleTime = 600000;

        //默认处理时间为当前时间1分钟以前
        public static int eachHandleLastTime = 1000;

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("数据转发程序执行开始");

            if (CommonCache.organList != null && CommonCache.organList.Count > 0)
            {
                //循环企业列表
                foreach (KeyValuePair<string, Orgaization> th in CommonCache.organList)
                {
                    if (th.Value != null)
                    {
                        Thread temp = null;
                        //判断企业在线程集合中是否存在
                        bool result = CommonCache.threadList.TryGetValue(th.Key, out temp);
                        //缓存中保存的企业对应的线程不为空说明上次的线程没有执行完..本次不执行
                        if (!result || temp == null)
                        {
                            //企业列表不为空时起动多线程
                            Orgaization organ = th.Value;
                            Thread vThread = new Thread(ThreadFun);
                            //企业及对应的线程ID放入到缓存中
                            CommonCache.threadList.TryAdd(th.Key, vThread);
                            vThread.Start(organ);   // 开始执行线程,传递参数
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("未查询到机构列表!" + DateTime.Now);
            }
            

            Console.WriteLine("数据转发程序执行结束!");
        }

        /// <summary>
        /// 线程方法
        /// </summary>
        /// <param name="pObj"></param>
        void ThreadFun(object pObj) // 来自委托：ParameterizedThreadStart 
        {
            Orgaization organEntity = (Orgaization)pObj;
            try
            {
                //根据企业ID.获取对应企业的服务地址
                OrganInfo organInfo = null;
                CommonCache.organServiceList.TryGetValue(organEntity.OrganID, out organInfo);

                if (organInfo == null)
                {
                    Console.WriteLine("当前企业(" + organEntity.OrganDESC + ")下没有配置服务地址,请联系管理员!");
                    return;
                }

                ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client("BasicHttpBinding_IService1", organInfo.ServiceAddress);

                //WCF未正常开放时.结束当前企业的循环
                if (sc.State == CommunicationState.Closed || sc.State == CommunicationState.Faulted)
                {
                    Console.WriteLine("未能连接客户端服务,请联系管理员!");
                    return;
                }

                //设备列表
                List<DeviceInfo> deviceList = null;
                //根据企业ID获取企业下的设备列表
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = "SELECT Id,DeviceCode,DeviceName FROM aifishingep.deviceinfo T WHERE T.ORGANID = @OrganID ORDER BY DeviceCode ";
                    List<DataParameter> dataParameter = new List<DataParameter>();
                    dataParameter.Add(new DataParameter { ParameterName = "OrganID", DataType = DbType.String, Value = organEntity.OrganID });
                    //插入基本信息
                    deviceList = session.GetList<DeviceInfo>(sql, dataParameter.ToArray()).ToList();
                }

                if (deviceList == null || deviceList.Count == 0)
                {
                    Console.WriteLine("当前企业下没有可用设备,请联系管理员!");
                    return;
                }

                //循环设备
                foreach (DeviceInfo di in deviceList)
                {
                    HandleResult handleResult = null;
                    //判断企业的数据记录表中是不有记录.没有记录从数据库中取现有数据的最小日期
                    using (IDataSession session = AppDataFactory.CreateMainSession())
                    {
                        string sql = @"SELECT MAX(HandleEndTime) AS HandleEndTime FROM aifishingep.exchangeresult T 
                                        WHERE T.ORGANID = @OrganID AND T.DeviceSN = @DeviceSN";
                        List<DataParameter> dataParameter = new List<DataParameter>();
                        dataParameter.Add(new DataParameter { ParameterName = "OrganID", DataType = DbType.String, Value = organEntity.OrganID });
                        dataParameter.Add(new DataParameter { ParameterName = "DeviceSN", DataType = DbType.String, Value = di.DeviceSN });
                        //获取转发记录中时间的最大值
                        handleResult = session.Get<HandleResult>(sql, dataParameter.ToArray());
                    }
                    //查询的起始时间
                    DateTime st;
                    //如果没有已处理记录
                    if (handleResult != null && handleResult.HandleEndTime != new DateTime())
                    {
                        st = handleResult.HandleEndTime;
                    }
                    else
                    {
                        Console.WriteLine("当前企业没数据转发记录!");
                        //查询设备ID的最小日期值
                        st = getMinDate(di.DeviceSN);
                    }

                    //起始时间不存在或者没有记录
                    if (st == null || st == new DateTime())
                    {
                        continue;
                    }

                    //st = DateTime.Parse("2018-03-28 21:01:02");

                    //取出当前时间的前一分钟
                    DateTime currTime = DateTime.Now.AddMilliseconds(eachHandleLastTime);

                    //起始时间+设置的时间间隔.计算出理论结束时间
                    DateTime et = st.AddMilliseconds(eachHandleTime);
                    //跳出循环flag
                    bool looping = true;
                    while (looping)
                    {
                        //理论结束时间大于当前时间时.使用当前时间获取数据
                        if (et >= currTime)
                        {
                            //当前时间的前一分钟设置为结束时间
                            et = currTime;
                            //执行完成后要退回循环
                            looping = false;
                        }

                        bool handResult = doSaveData(sc, organEntity.OrganID, di.DeviceSN, st, et);

                        //数据未成功保存时,跳出当前循环
                        if (!handResult)
                        {
                            Console.WriteLine("转发企业(" + organEntity.OrganDESC + ")-设备(" + di.DeviceSN + ")-起始时间(" + st.ToString("yyyy-MM-dd HH:mm:ss") + ")-结束时间(" + et.ToString("yyyy-MM-dd HH:mm:ss") + "),发生异常请联系管理员!");
                            break;
                        }

                        //没有退出循环.继续增加指定时间
                        if (looping)
                        {
                            st = et;
                            //循环完.再为结束时间增加指定间隔的时间量
                            et = et.AddMilliseconds(eachHandleTime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Thread oldThread = null;
                //清空缓存中企业对应的线程信息
                CommonCache.threadList.TryGetValue(organEntity.OrganID, out oldThread);
                if (oldThread.ThreadState != ThreadState.Stopped)
                {
                    CommonCache.threadList.TryRemove(organEntity.OrganID, out oldThread);
                    oldThread.Abort();
                }
                
            }
        }

        /// <summary>
        /// 获取数据并调用服务保存
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="OrganID"></param>
        /// <param name="DeviceSN"></param>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <returns></returns>
        public bool doSaveData(ServiceReference1.Service1Client sc, string OrganID, string DeviceSN, DateTime st, DateTime et)
        {
            //从指定日期开始到指定日期结束的数据
            List<InspectItemData> itemDataList = getInspectItemData(DeviceSN, st, et);
            
            //保存结果
            bool saveResult = false;
            //采集列表不为空时
            if (itemDataList != null && itemDataList.Count > 0)
            {
                //设置企业信息机构ID及更新时间
                foreach (InspectItemData iid in itemDataList)
                {
                    iid.ID = Guid.NewGuid().ToString();
                    iid.OrganID = OrganID;
                    iid.UpdateTime = DateTime.Now;
                }
            }

            //获取计算结果
            List<InspectResultData> culResultList = null;
            //从mysql取出计算结果
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                string sql = @"SELECT * FROM aifishingep.inspectcalcresult T 
                                        WHERE T.ORGANID = @OrganID AND T.DeviceCode = @DeviceSN AND T.UpdateTime > @ST AND T.UpdateTime <= @ET ";
                //此处查询不能使用InspectTime.InspectTime存放的是对应的月份.天或者小时数.结果统计时以更新时间为主
                List<DataParameter> dataParameter = new List<DataParameter>();
                dataParameter.Add(new DataParameter { ParameterName = "OrganID", DataType = DbType.String, Value = OrganID });
                dataParameter.Add(new DataParameter { ParameterName = "DeviceSN", DataType = DbType.String, Value = DeviceSN });
                dataParameter.Add(new DataParameter { ParameterName = "ST", DataType = DbType.String, Value = st });
                dataParameter.Add(new DataParameter { ParameterName = "ET", DataType = DbType.String, Value = et });
                //插入基本信息
                culResultList = session.GetList<InspectResultData>(sql, dataParameter.ToArray()).ToList();
            }

            if (itemDataList.Count > 0 || culResultList.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("转发企业(" + OrganID + ")-设备(" 
                + DeviceSN + ")-起始时间(" + st.ToString("yyyy-MM-dd HH:mm:ss") 
                + ")-结束时间(" + et.ToString("yyyy-MM-dd HH:mm:ss") 
                + ")-采集记录条数(" + itemDataList.Count
                + ")-计算结果记录条数(" + culResultList.Count + ")");


            //调用WCF保存数据到企业端
            saveResult = sc.saveItemAndCalResultData(JsonConvertHelper.GetSerializes(itemDataList), JsonConvertHelper.GetSerializes(culResultList));

            //保存成功
            if (saveResult)
            {
                //将本次执行记录保存到数据
                HandleResult handelResult = new HandleResult()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrganId = OrganID,
                    HandleTime = DateTime.Now,
                    DeviceSN = DeviceSN,
                    HandleStartTime = st,
                    HandleEndTime = et,
                    HandleCountMdb = itemDataList.Count,
                    HandleCountMs = culResultList.Count
                };

                //处理记录保存到数据库中
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.Insert<HandleResult>(handelResult);
                }
            }

            return saveResult;
        }

        /// <summary>
        /// 根据企业的设备列表获取数据的最小时间
        /// </summary>
        /// <param name="deviceList"></param>
        /// <returns></returns>
        public DateTime getMinDate(string DeviceSN)
        {
            //创建数据库链接
            MongoClient mc = new MongoClient(ConnectionManager.MongodbConectionStr);
            MongoServer server = mc.GetServer();
            server.Connect();

            //获得数据库
            MongoDatabase db = server.GetDatabase("InspectDB");
            MongoCollection colOdata = db.GetCollection("InspectOriginalData");

            try
            {
                //插入原始监测数据
                QueryDocument query = new QueryDocument();

                //查询全部集合里的数据
                List<InspectItemData> list = colOdata.FindAllAs<InspectItemData>().Where(x => x.DeviceSN == DeviceSN).ToList();
                DateTime minDateTime = DateTime.Now;
                foreach(InspectItemData iid in list)
                {
                    if (iid.InspectTime < minDateTime)
                    {
                        minDateTime = iid.InspectTime;
                    }
                }

                return minDateTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库连接
                if (server != null)
                    server.Disconnect();
            }
        }


        /// <summary>
        /// 获取子表数据
        /// </summary>
        /// <param name="deviceList"></param>
        /// <returns></returns>
        public List<InspectItemData> getInspectItemData(string DeviceSN, DateTime st, DateTime et)
        {
            //创建数据库链接
            MongoClient mc = new MongoClient(ConnectionManager.MongodbConectionStr);
            MongoServer server = mc.GetServer();
            server.Connect();

            //获得数据库
            MongoDatabase db = server.GetDatabase("InspectDB");
            MongoCollection colOdata = db.GetCollection("InspectItemData");
            List<InspectItemData> list = null;
            try
            {
                //插入原始监测数据
                QueryDocument query = new QueryDocument();
                list = colOdata.FindAllAs<InspectItemData>().Where(x => x.DeviceSN == DeviceSN && x.InspectTime >= st && x.InspectTime < et )
                    .OrderBy(x => x.InspectTime).ToList<InspectItemData>();
 
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库连接
                if (server != null)
                    server.Disconnect();
            }
        }
    }
}
