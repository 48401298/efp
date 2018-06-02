using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LAF.Data;
using ExchangeEntity;
using LAF.Common.Serialization;
using LAF.Data;
using System.Data;

namespace ExchangeNode
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        /// <summary>
        /// 保存采集数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool saveItemAndCalResultData(string dataStr, string resDataStr)
        {
            int count = 0;
            List<InspectItemData> itemList = JsonConvertHelper.DeserializeObject<List<InspectItemData>>(dataStr);
            List<InspectRealTimeItemData> realTimeItemList = JsonConvertHelper.DeserializeObject<List<InspectRealTimeItemData>>(dataStr);
            List<InspectResultData> culResultList = JsonConvertHelper.DeserializeObject<List<InspectResultData>>(resDataStr);
            //用于事务处理
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                try
                {
                    //插入基本信息
                    count = session.Insert<InspectItemData>(itemList);
                    foreach (InspectRealTimeItemData irti in realTimeItemList)
                    {
                        List<DataParameter> dataParameter = new List<DataParameter>();
                        dataParameter.Add(new DataParameter { ParameterName = "DeviceSN", DataType = DbType.String, Value = irti.DeviceSN });
                        dataParameter.Add(new DataParameter { ParameterName = "ItemCode", DataType = DbType.String, Value = irti.ItemCode });
                        //更新实时记录表
                        int updCount = Convert.ToInt32(session.ExecuteSqlScalar("select count(0) from inspectrealtimedata where DeviceCode = @DeviceSN and ItemCode = @ItemCode ", dataParameter.ToArray()));
                        //如果返回数据为0说明记录不存在则插入一条新的记录
                        if (updCount == 0)
                        {
                            session.Insert(irti);
                        }
                        else
                        {
                            //更新值
                            dataParameter.Add(new DataParameter { ParameterName = "InspectTime", DataType = DbType.DateTime, Value = irti.InspectTime });
                            dataParameter.Add(new DataParameter { ParameterName = "InspectData", DataType = DbType.String, Value = irti.InspectData });
                            dataParameter.Add(new DataParameter { ParameterName = "UpdateTime", DataType = DbType.DateTime, Value = irti.UpdateTime });
                            session.ExecuteSqlScalar("update inspectrealtimedata set InspectTime = @InspectTime, InspectData = @InspectData, UpdateTime = @UpdateTime where DeviceCode = @DeviceSN and ItemCode = @ItemCode ", dataParameter.ToArray());
                        }
                    }

                    //测试时使用否则插入同一张表中数据会主键冲突
                    foreach (InspectResultData ird in culResultList)
                    {
                        List<DataParameter> dataParameter = new List<DataParameter>();
                        dataParameter.Add(new DataParameter { ParameterName = "ID", DataType = DbType.String, Value = ird.ID });
                        int dataCount = Convert.ToInt32(session.ExecuteSqlScalar("select count(0) from inspectcalcresult where ID = @ID ", dataParameter.ToArray()));
                        //如果返回数据为0说明记录不存在则插入一条新的记录
                        if (dataCount == 0)
                        {
                            count += session.Insert(ird);
                        }
                        else
                        {
                            count += session.Update(ird);
                        }
                    }
 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //保存出错时回滚数据
                    session.RollbackTs();
                    return false;
                }
            }
            
            return true;
        }
    }
}
