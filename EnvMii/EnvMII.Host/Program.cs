using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Logging;
using SuperSocket.SocketEngine;
using EnvMII.SSAppServer.TH10WWiFi;
using EnvMII.VO;
using EnvMII.SSAppServer;
using EnvMII.Service;
using LAF.MongoDBClient;
using EnvMII.SSAppServer.TH11SBRS485;
using EnvMII.SSAppServer.ANEMOCLINOGRAP;
using EnvMII.SSAppServer.ACTWCAR;
using EnvMII.SSAppServer.ACLWCAR;
using Quartz;
using Quartz.Impl;
using EnvMII.Quartz;
using EnvMII.SSAppServer.LouverlightSensor;
using LAF.Data;
using System.Collections.Concurrent;

namespace EnvMII.Host
{
    /// <summary>
    /// 主程序
    /// </summary>
    class Program
    {
        //TH10W-WiFi无线温湿度记录仪监控
        private static TH10WProtocolServer Th10w_Server;
        private static IServerConfig Th10w_Config;

        //TH11S-B RS485通讯型温湿度变送器
        private static TH11SProtocolServer Th11s_Server;
        private static IServerConfig Th11s_Config;

        //风速风向仪
        private static ANEMOCLINOGRAPProtocolServer Anemoclinograp_Server;
        private static IServerConfig Anemoclinograp_Config;

        //ACTW-CAR小型清洁刷式温盐传感器
        private static ACTWCARProtocolServer Actwcar_Server;
        private static IServerConfig Actwcar_Config;

        //ACLW-CAR传感器通讯协议与电气参数
        private static ACLWCARProtocolServer Aclwcar_Server;
        private static IServerConfig Aclwcar_Config;

        //百叶箱光照传感器
        private static LouverlightSensorProtocolServer LouverlightSensor_Server;
        private static IServerConfig LouverlightSensor_Config;

        static void Main(string[] args)
        {
            //初始化数据库连接串
            ConnectionManager.MongodbConectionStr = ConfigurationManager.AppSettings["MongodbConectionStr"];

            //装配数据工厂
            LAF.Data.DataFactory.Configure(true);

            #region 获取设备缓存信息
            //获取所有设备列表放入到字典表中
            try
            {
                List<DeviceInfo> deviceList = null;

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = "SELECT * FROM deviceinfo T ORDER BY DeviceCode";
                    List<DataParameter> dataParameter = new List<DataParameter>();
                    //插入基本信息
                    deviceList = session.GetList<DeviceInfo>(sql, dataParameter.ToArray()).ToList<DeviceInfo>();
                }

                if (deviceList != null)
                {
                    if (ThJob.deviceList == null)
                    {
                        ThJob.deviceList = new ConcurrentDictionary<String, DeviceInfo>();
                    }

                    foreach (DeviceInfo di in deviceList)
                    {
                        ThJob.deviceList.TryAdd(di.DeviceSN, di);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region 无线温湿度记录仪 服务

            //获取配置端口结果
            bool th10wResult = false;
            int th10wPort = getPortByType("th10wPort", out th10wResult);
            if (th10wResult)
            {
                //TH10W-WiFi无线温湿度记录仪监控
                Th10w_Config = new ServerConfig
                {
                    Port = th10wPort,
                    Ip = "Any",
                    MaxConnectionNumber = 1000,
                    Mode = SocketMode.Tcp,
                    Name = "TH10W-WiFi无线温湿度记录仪监控"
                };

                Th10w_Server = new TH10WProtocolServer();
                Th10w_Server.NewSessionConnected += new SessionHandler<TH10WProtocolSession>(th10w_Server_NewSessionConnected);
                Th10w_Server.SessionClosed += new SessionHandler<TH10WProtocolSession, CloseReason>(th10w_Server_SessionClosed);
                Th10w_Server.NewRequestReceived += new RequestHandler<TH10WProtocolSession, SuperSocket.SocketBase.Protocol.BinaryRequestInfo>(th10w_Server_NewRequestReceived);
                Th10w_Server.Setup(Th10w_Config, logFactory: new ConsoleLogFactory());
                //var sessions = Th10w_Server.GetAllSessions();
                //sessions.First().SendResponse("");
                //foreach (var s in sessions)
                //{
                //    s.SendResponse("Hello world!");
                //}

                Th10w_Server.Start();
            }

            #endregion

            #region 温湿度变送器 服务

            //获取配置端口结果
            bool th11sResult = false;
            int th11sPort = getPortByType("th11sPort", out th11sResult);
            if (th11sResult)
            {
                //TH11S-B RS485通讯型温湿度变送器
                Th11s_Config = new ServerConfig
                {
                    Port = th11sPort,
                    Ip = "Any",
                    MaxConnectionNumber = 1000,
                    Mode = SocketMode.Tcp,
                    Name = "TH11S-B RS485通讯型温湿度变送器"
                };

                Th11s_Server = new TH11SProtocolServer();
                Th11s_Server.NewSessionConnected += new SessionHandler<TH11SProtocolSession>(th11s_Server_NewSessionConnected);
                Th11s_Server.SessionClosed += new SessionHandler<TH11SProtocolSession, CloseReason>(th11s_Server_SessionClosed);
                Th11s_Server.NewRequestReceived += new RequestHandler<TH11SProtocolSession, SuperSocket.SocketBase.Protocol.BinaryRequestInfo>(th11s_Server_NewRequestReceived);
                Th11s_Server.Setup(Th11s_Config, logFactory: new ConsoleLogFactory());

                Th11s_Server.Start();
            }

            #endregion

            #region 风速风向仪 服务

            //获取配置端口结果
            bool aneResult = false;
            int anePort = getPortByType("anePort", out aneResult);
            if (aneResult)
            {
                //风速风向仪
                Anemoclinograp_Config = new ServerConfig
                {
                    Port = anePort,
                    Ip = "Any",
                    MaxConnectionNumber = 1000,
                    Mode = SocketMode.Tcp,
                    Name = "风速风向仪监控"
                };

                Anemoclinograp_Server = new ANEMOCLINOGRAPProtocolServer();
                Anemoclinograp_Server.NewSessionConnected += new SessionHandler<ANEMOCLINOGRAPProtocolSession>(anemoclinograp_Server_NewSessionConnected);
                Anemoclinograp_Server.SessionClosed += new SessionHandler<ANEMOCLINOGRAPProtocolSession, CloseReason>(anemoclinograp_Server_SessionClosed);
                Anemoclinograp_Server.NewRequestReceived += new RequestHandler<ANEMOCLINOGRAPProtocolSession, SuperSocket.SocketBase.Protocol.BinaryRequestInfo>(anemoclinograp_Server_NewRequestReceived);
                Anemoclinograp_Server.Setup(Anemoclinograp_Config, logFactory: new ConsoleLogFactory());

                Anemoclinograp_Server.Start();
            }

            #endregion

            #region 小型清洁刷式温盐传感器 服务

            //获取配置端口结果
            bool twResult = false;
            int twPort = getPortByType("twPort", out twResult);
            if (twResult)
            {
                //ACTW-CAR小型清洁刷式温盐传感器
                Actwcar_Config = new ServerConfig
                {
                    Port = twPort,
                    Ip = "Any",
                    MaxConnectionNumber = 1000,
                    Mode = SocketMode.Tcp,
                    Name = "ACTW-CAR小型清洁刷式温盐传感器监控"
                };

                Actwcar_Server = new ACTWCARProtocolServer();
                Actwcar_Server.NewSessionConnected += new SessionHandler<ACTWCARProtocolSession>(actwcar_Server_NewSessionConnected);
                Actwcar_Server.SessionClosed += new SessionHandler<ACTWCARProtocolSession, CloseReason>(actwcar_Server_SessionClosed);
                Actwcar_Server.NewRequestReceived += new RequestHandler<ACTWCARProtocolSession, SuperSocket.SocketBase.Protocol.BinaryRequestInfo>(actwcar_Server_NewRequestReceived);
                Actwcar_Server.Setup(Actwcar_Config, logFactory: new ConsoleLogFactory());

                Actwcar_Server.Start();
            }

            #endregion

            #region 小型清洁刷式温浊传感器 服务

            //获取配置端口结果
            bool lwResult = false;
            int lwPort = getPortByType("lwPort", out lwResult);
            if (lwResult)
            {
                //ACLW-CAR传感器通讯协议与电气参数
                Aclwcar_Config = new ServerConfig
                {
                    Port = lwPort,
                    Ip = "Any",
                    MaxConnectionNumber = 1000,
                    Mode = SocketMode.Tcp,
                    Name = "ACLW-CAR小型清洁刷式温浊传感器监控"
                };

                Aclwcar_Server = new ACLWCARProtocolServer();
                Aclwcar_Server.NewSessionConnected += new SessionHandler<ACLWCARProtocolSession>(aclwcar_Server_NewSessionConnected);
                Aclwcar_Server.SessionClosed += new SessionHandler<ACLWCARProtocolSession, CloseReason>(aclwcar_Server_SessionClosed);
                Aclwcar_Server.NewRequestReceived += new RequestHandler<ACLWCARProtocolSession, SuperSocket.SocketBase.Protocol.BinaryRequestInfo>(aclwcar_Server_NewRequestReceived);
                Aclwcar_Server.Setup(Aclwcar_Config, logFactory: new ConsoleLogFactory());

                Aclwcar_Server.Start();
            }

            #endregion

            #region 百叶箱光照传感器 服务

            //获取配置端口结果
            bool llsResult = false;
            int llsPort = getPortByType("llsPort", out llsResult);
            if (llsResult)
            {
                //百叶箱光照传感器
                LouverlightSensor_Config = new ServerConfig
                {
                    Port = llsPort,
                    Ip = "Any",
                    MaxConnectionNumber = 1000,
                    Mode = SocketMode.Tcp,
                    Name = "百叶箱光照传感器"
                };

                LouverlightSensor_Server = new LouverlightSensorProtocolServer();
                LouverlightSensor_Server.NewSessionConnected += new SessionHandler<LouverlightSensorProtocolSession>(louverlightSensor_Server_NewSessionConnected);
                LouverlightSensor_Server.SessionClosed += new SessionHandler<LouverlightSensorProtocolSession, CloseReason>(louverlightSensor_Server_SessionClosed);
                LouverlightSensor_Server.NewRequestReceived += new RequestHandler<LouverlightSensorProtocolSession, SuperSocket.SocketBase.Protocol.BinaryRequestInfo>(louverlightSensor_Server_NewRequestReceived);
                LouverlightSensor_Server.Setup(LouverlightSensor_Config, logFactory: new ConsoleLogFactory());

                LouverlightSensor_Server.Start();
            }

            #endregion

            #region 定时器配置

            // 从工厂里获取调度实例
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            // 创建一个任务
            IJobDetail job = JobBuilder.Create<ThJob>()
                .WithIdentity("MyJob", "group1")
                .Build();

            // 创建一个触发器
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1").WithCronSchedule("*/20 * * * * ?") //每20秒执行一次
                .StartNow()
                .Build();

            scheduler.ScheduleJob(job, trigger); 

            #endregion

            #region 更新设备缓存
            // 创建一个任务
            IJobDetail cacheJob = JobBuilder.Create<CacheJob>()
                .WithIdentity("cacheJob", "cacheGroup")
                .Build();

            // 创建一个触发器
            ITrigger cacheTrigger = TriggerBuilder.Create()
                .WithIdentity("cacheTrigger", "cacheGroup").WithCronSchedule("0 */10 * * * ?") //每10分钟更新一次缓存
                .StartNow()
                .Build();

            scheduler.ScheduleJob(cacheJob, cacheTrigger);

            #endregion

            Console.ReadKey();

            #region 服务关闭

            if (Th10w_Server != null)
            {
                Th10w_Server.Stop();
            }
            if (Th11s_Server != null)
            {
                Th11s_Server.Stop();
            }
            if (Anemoclinograp_Server != null)
            {
                Anemoclinograp_Server.Stop();
            }
            if (Actwcar_Server != null)
            {
                Actwcar_Server.Stop();
            }
            if (Aclwcar_Server != null)
            {
                Aclwcar_Server.Stop();
            }
            if (LouverlightSensor_Server != null)
            {
                LouverlightSensor_Server.Stop();
            }

            #endregion

            Console.WriteLine("The server was stopped!");
        }

        static void th10w_Server_NewRequestReceived(TH10WProtocolSession session, SuperSocket.SocketBase.Protocol.BinaryRequestInfo requestInfo)
        {
            //string remoteEndPoint = session.RemoteEndPoint.Address + ":" + session.RemoteEndPoint.Port.ToString();
            string terminalType = DeviceCode.TH10W;
            string DeviceSN = "";
            //处理设备的登陆信息
            handleDeviceInfo(DeviceCode.TH10W, requestInfo.Key.Substring(0, 2), session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), out DeviceSN);
          
            //获取原始监控数据
            InspectOriginalData oData = new TH10WDataResolver().CreateOriginalData(DeviceSN, requestInfo);

            //解析监控数据
            List<InspectItemData> itemDatas = new TH10WDataResolver().ResolveItemData(oData);

            ///保存监测数据
            saveData(oData, itemDatas);

            ///打印采集信息
            printData(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), oData, itemDatas);

            ////更新当前连接信息到静态变量中,用于定时清理垃圾session
            //CommonSession cs = ThJob.getTh10w(session.SessionID);
            //if (cs != null)
            //{
            //    cs.session = session;
            //    ThJob.updateTh10w(session.SessionID, cs);
            //}
        }

        static void th10w_Server_SessionClosed(TH10WProtocolSession session, CloseReason value)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            ThJob.removeTh10w(key);
            //连接关闭
            printColsed(session.SessionID);
        }

        static void th10w_Server_NewSessionConnected(TH10WProtocolSession session)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            CommonSession cs = new CommonSession();
            cs.session = session;
            ThJob.addTh10w(key, cs);

            ///连接打开
            printConnectioned(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString());
        }

        static void th11s_Server_NewRequestReceived(TH11SProtocolSession session, SuperSocket.SocketBase.Protocol.BinaryRequestInfo requestInfo)
        {
            //string remoteEndPoint = session.RemoteEndPoint.Address + ":" + session.RemoteEndPoint.Port.ToString();
            string terminalType = DeviceCode.TH11S;
            string DeviceSN = "";
            //处理设备的登陆信息
            handleDeviceInfo(terminalType, requestInfo.Key.Substring(0, 2), session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), out DeviceSN);

            //获取原始监控数据
            InspectOriginalData oData = new TH11SDataResolver().CreateOriginalData(DeviceSN, requestInfo);

            //解析监控数据
            List<InspectItemData> itemDatas = new TH11SDataResolver().ResolveItemData(oData);

            ///保存监测数据
            saveData(oData, itemDatas);

            ///打印采集信息
            printData(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), oData, itemDatas);

            ////更新当前连接信息到静态变量中,用于定时清理垃圾session
            //CommonSession cs = ThJob.getTh11s(session.SessionID);
            //if (cs != null)
            //{
            //    cs.session = session;
            //    ThJob.updateTh11s(session.SessionID, cs);
            //}
        }

        static void th11s_Server_SessionClosed(TH11SProtocolSession session, CloseReason value)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            ThJob.removeTh11s(key);
            //连接关闭
            printColsed(session.SessionID);
        }

        static void th11s_Server_NewSessionConnected(TH11SProtocolSession session)
        {

            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            CommonSession cs = new CommonSession();
            cs.session = session;
            ThJob.addTh11s(key, cs);
            ///连接打开
            printConnectioned(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString());
        }

        static void anemoclinograp_Server_NewRequestReceived(ANEMOCLINOGRAPProtocolSession session, SuperSocket.SocketBase.Protocol.BinaryRequestInfo requestInfo)
        {
            //string remoteEndPoint = session.RemoteEndPoint.Address + ":" + session.RemoteEndPoint.Port.ToString();
            string terminalType = DeviceCode.ANE;
            string DeviceSN = "";

            //处理设备的登陆信息
            handleDeviceInfo(terminalType, requestInfo.Key.Substring(0, 2), session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), out DeviceSN);

            //获取原始监控数据
            InspectOriginalData oData = new ANEMOCLINOGRAPDataResolver().CreateOriginalData(DeviceSN, requestInfo);

            //解析监控数据
            List<InspectItemData> itemDatas = new ANEMOCLINOGRAPDataResolver().ResolveItemData(oData);

            ///保存监测数据
            saveData(oData, itemDatas);

            ///打印采集信息
            printData(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), oData, itemDatas);

            ////更新当前连接信息到静态变量中,用于定时清理垃圾session
            //CommonSession cs = ThJob.getAnemoclinoGrap(session.SessionID);
            //if (cs != null)
            //{
            //    cs.session = session;
            //    ThJob.updateAnemoclinoGrap(session.SessionID, cs);
            //}
        }

        static void anemoclinograp_Server_SessionClosed(ANEMOCLINOGRAPProtocolSession session, CloseReason value)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            ThJob.removeAnemoclinoGrap(key);
            //连接关闭
            printColsed(session.SessionID);
        }

        static void anemoclinograp_Server_NewSessionConnected(ANEMOCLINOGRAPProtocolSession session)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            CommonSession cs = new CommonSession();
            cs.session = session;
            ThJob.addAnemoclinoGrap(key, cs);

            ///连接打开
            printConnectioned(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString());
        }

        static void actwcar_Server_NewRequestReceived(ACTWCARProtocolSession session, SuperSocket.SocketBase.Protocol.BinaryRequestInfo requestInfo)
        {
            //string remoteEndPoint = session.RemoteEndPoint.Address + ":" + session.RemoteEndPoint.Port.ToString();
            string terminalType = DeviceCode.TW;
            string DeviceSN = "";

            //处理设备的登陆信息
            handleDeviceInfo(terminalType, requestInfo.Key.Substring(0, 2), session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), out DeviceSN);

            //获取原始监控数据
            InspectOriginalData oData = new ACTWCARDataResolver().CreateOriginalData(DeviceSN, requestInfo);

            if ("wipe,0,".Equals(Tools.HexStringToString(oData.InspectData.Substring(2), Encoding.UTF8)))
            {
                string sr = "11117076616C2C0D";
                session.Send(sr);

                ///打印采集信息
                printData(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), oData, null);

                Console.WriteLine("SendResponse:" + sr);
                return;
            }
            else
            {
                //解析监控数据
                List<InspectItemData> itemDatas = new ACTWCARDataResolver().ResolveItemData(oData);

                ///保存监测数据
                saveData(oData, itemDatas);

                //返回状态为0时修改集合中的状态,设置设备状态为复位
                if("0".Equals(itemDatas[itemDatas.Count() - 1].InspectData))
                {
                    ThJob.setTwIsWorking(session.SessionID, false);
                }

                ///打印采集信息
                printData(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), oData, itemDatas);
            }

            ////更新当前连接信息到静态变量中,用于定时清理垃圾session
            //CommonSession cs = ThJob.getTw(session.SessionID);
            //if (cs != null)
            //{
            //    cs.session = session;
            //    ThJob.updateTw(session.SessionID, cs);
            //}
        }

        static void actwcar_Server_SessionClosed(ACTWCARProtocolSession session, CloseReason value)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            ThJob.removeTw(key);
            //连接关闭
            printColsed(session.SessionID);
        }

        static void actwcar_Server_NewSessionConnected(ACTWCARProtocolSession session)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            CommonSession cs = new CommonSession();
            cs.session = session;
            ThJob.addTw(key, cs);

            ///连接打开
            printConnectioned(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString());
        }

        static void aclwcar_Server_NewRequestReceived(ACLWCARProtocolSession session, SuperSocket.SocketBase.Protocol.BinaryRequestInfo requestInfo)
        {
            //string remoteEndPoint = session.RemoteEndPoint.Address + ":" + session.RemoteEndPoint.Port.ToString();
            string terminalType = DeviceCode.LW;
            string DeviceSN = "";

            //处理设备的登陆信息
            handleDeviceInfo(terminalType, requestInfo.Key.Substring(0, 2), session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), out DeviceSN);

            //获取原始监控数据
            InspectOriginalData oData = new ACLWCARDataResolver().CreateOriginalData(DeviceSN, requestInfo);

            if ("wipe,0,".Equals(Tools.HexStringToString(oData.InspectData.Substring(2), Encoding.UTF8)))
            {
                string sr = "11117076616C2C0D";
                session.Send(sr);

                ///打印采集信息
                printData(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), oData, null);

                Console.WriteLine("SendResponse:" + sr);
                return;
            }
            else
            {
                //解析监控数据
                List<InspectItemData> itemDatas = new ACLWCARDataResolver().ResolveItemData(oData);

                ///保存监测数据
                saveData(oData, itemDatas);

                //返回状态为0时修改集合中的状态,设置设备状态为复位
                if ("0".Equals(itemDatas[itemDatas.Count() - 1].InspectData))
                {
                    ThJob.setLwIsWorking(session.SessionID, false);
                }

                ///打印采集信息
                printData(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), oData, itemDatas);
            }

            ////更新当前连接信息到静态变量中,用于定时清理垃圾session
            //CommonSession cs = ThJob.getLw(session.SessionID);
            //if (cs != null)
            //{
            //    cs.session = session;
            //    ThJob.updateLw(session.SessionID, cs);
            //}
        }

        static void aclwcar_Server_SessionClosed(ACLWCARProtocolSession session, CloseReason value)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            ThJob.removeLw(key);
            //连接关闭
            printColsed(session.SessionID);
        }

        static void aclwcar_Server_NewSessionConnected(ACLWCARProtocolSession session)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            CommonSession cs = new CommonSession();
            cs.session = session;
            ThJob.addLw(key, cs);

            ///连接打开
            printConnectioned(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString());
        }

        static void louverlightSensor_Server_NewRequestReceived(LouverlightSensorProtocolSession session, SuperSocket.SocketBase.Protocol.BinaryRequestInfo requestInfo)
        {
            //string remoteEndPoint = session.RemoteEndPoint.Address + ":" + session.RemoteEndPoint.Port.ToString();
            string terminalType = DeviceCode.LLS;
            string DeviceSN = "";

            //处理设备的登陆信息
            handleDeviceInfo(terminalType, requestInfo.Key.Substring(0, 2), session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), out DeviceSN);

            //获取原始监控数据
            InspectOriginalData oData = new LouverlightSensorDataResolver().CreateOriginalData(DeviceSN, requestInfo);

            //解析监控数据
            List<InspectItemData> itemDatas = new LouverlightSensorDataResolver().ResolveItemData(oData);

            ///保存监测数据
            saveData(oData, itemDatas);

            ///打印采集信息
            printData(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString(), oData, itemDatas);
          
            ////更新当前连接信息到静态变量中,用于定时清理垃圾session
            //CommonSession cs = ThJob.getLouverlightSensor(session.SessionID);
            //if (cs != null)
            //{
            //    cs.session = session;
            //    ThJob.updateLouverlightSensor(session.SessionID, cs);
            //}
        }

        static void louverlightSensor_Server_SessionClosed(LouverlightSensorProtocolSession session, CloseReason value)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            ThJob.removeLouverlightSensor(key);
            //连接关闭
            printColsed(session.SessionID);
        }

        static void louverlightSensor_Server_NewSessionConnected(LouverlightSensorProtocolSession session)
        {
            //识别码_IP生成KEY用于保存session
            string key = session.SessionID;
            CommonSession cs = new CommonSession();
            cs.session = session;
            ThJob.addLouverlightSensor(key, cs);
            ///连接打开
            printConnectioned(session.SessionID, session.RemoteEndPoint.Address.ToString(), session.RemoteEndPoint.Port.ToString());
        }

        /// <summary>
        /// 处理设备信息.数据库中没有时插入.有时更新最后登陆时间.并更新缓存中的设备信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        static void handleDeviceInfo(string terminalType, string keyStr, string ip, string port, out string key)
        {
            string no = Tools.HexStrToDecimal(keyStr).ToString().PadLeft(2, '0');
            key = terminalType + "_" + no;

            //判断设备ID在设备列表里是否存在.不存在直接返回
            DeviceInfo deviceInfo = (ThJob.deviceList.ContainsKey(key) ? ThJob.deviceList[key] : null);
            //在数据库中创建一个设备
            if (deviceInfo == null)
            {
                deviceInfo = new DeviceInfo();
                deviceInfo.Id = Guid.NewGuid().ToString();
                deviceInfo.DeviceSN = key;
                deviceInfo.DeviceName = key;
                deviceInfo.DeviceIP = ip;
                deviceInfo.DevicePort = port;
                deviceInfo.DeviceType = terminalType;
                deviceInfo.LastRegisterTime = DateTime.Now;
                deviceInfo.LastLoginTime = deviceInfo.LastRegisterTime;
                //从配置文件中取出设置的当前企业的ID
                string organid = ConfigurationManager.AppSettings["deviceInfo"];
                deviceInfo.OrganID = (organid != null ? organid.ToString() : "");
                //机构
                //备注
                //插入设备信息
                using (IDataSession mysqlSession = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    mysqlSession.Insert(deviceInfo);
                }
                //新的设备信息放入到字典表中
                ThJob.deviceList.TryAdd(key, deviceInfo);
            }
            else
            {
                //更新设备最后登陆时间
                using (IDataSession mysqlSession = AppDataFactory.CreateMainSession())
                {
                    deviceInfo.LastLoginTime = DateTime.Now;
                    //更新设备最后登陆时间
                    mysqlSession.Update(deviceInfo);
                }
            }
        }

        /// <summary>
        /// 采集的数据保存到数据库中
        /// </summary>
        /// <param name="oData"></param>
        /// <param name="itemDatas"></param>
        static void saveData(InspectOriginalData oData, List<InspectItemData> itemDatas)
        {
            //发布监测信息
            InspectMsg msg = new InspectMsg();
            msg.OriginalData = oData;
            msg.ItemDatas = itemDatas;
            CollectInspectMsg(msg);
        }

        /// <summary>
        /// 打印连接信息
        /// </summary>
        /// <param name="oData"></param>
        /// <param name="itemDatas"></param>
        static void printData(string SessionID, string ip, string port, InspectOriginalData oData, List<InspectItemData> itemDatas)
        {
            Console.WriteLine("received data");
            Console.WriteLine("sessionid:" + SessionID);
            Console.WriteLine("RemoteEndPoint:" + ip + ":" + port);
            Console.WriteLine("DeviceSN:" + oData.DeviceSN);
            Console.WriteLine("InspectTime:" + oData.InspectTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine("OriginalData:" + oData.InspectData);

            if (itemDatas != null && itemDatas.Count > 0)
            {
                foreach (InspectItemData itemData in itemDatas)
                {
                    Console.WriteLine(itemData.ItemCode + ":" + itemData.InspectData);
                }
            }
        }

        /// <summary>
        /// 打印连接关闭信息
        /// </summary>
        /// <param name="SessionID"></param>
        static void printColsed(string SessionID)
        {
            Console.WriteLine("close sessionid:" + SessionID);
        }

        /// <summary>
        /// 打印连接打开信息
        /// </summary>
        /// <param name="SessionID"></param>
        static void printConnectioned(string SessionID, string ip, string port)
        {
            Console.WriteLine("logion sessionid:" + SessionID);
            Console.WriteLine("RemoteEndPoint:" + ip + ":" + port);
        }

        static void CollectInspectMsg(InspectMsg msg)
        {
            new InspectDataService().Save(msg.OriginalData, msg.ItemDatas);
        }

        /// <summary>
        /// 根据传入的类型返回配置中的端口号
        /// </summary>
        /// <param name="portType"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        static int getPortByType(string portType, out bool result)
        {
            int port = 0;
            result = false;
            try
            {
                port = Convert.ToInt32(ConfigurationManager.AppSettings[portType].ToString());
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("端口格式错误,请联系管理员!");
            }
            return port;
        }

    }
}
