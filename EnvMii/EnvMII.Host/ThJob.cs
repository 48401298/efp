using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.Collections;
using EnvMII.SSAppServer.TH10WWiFi;
using EnvMII.SSAppServer;
using EnvMII.SSAppServer.TH11SBRS485;
using EnvMII.SSAppServer.ANEMOCLINOGRAP;
using EnvMII.SSAppServer.ACTWCAR;
using EnvMII.SSAppServer.ACLWCAR;
using EnvMII.SSAppServer.LouverlightSensor;
using EnvMII.VO;
using System.Configuration;
using System.Collections.Concurrent;

namespace EnvMII.Quartz
{
    public class ThJob : IJob
    {
        public static ConcurrentDictionary<String, DeviceInfo> deviceList = new ConcurrentDictionary<String, DeviceInfo>();

        public static ConcurrentDictionary<String, CommonSession> th10w = new ConcurrentDictionary<String, CommonSession>();

        public static ConcurrentDictionary<String, CommonSession> th11s = new ConcurrentDictionary<String, CommonSession>();

        public static ConcurrentDictionary<String, CommonSession> anemoclinoGrap = new ConcurrentDictionary<String, CommonSession>();

        public static ConcurrentDictionary<String, CommonSession> tw = new ConcurrentDictionary<String, CommonSession>();

        public static ConcurrentDictionary<String, CommonSession> lw = new ConcurrentDictionary<String, CommonSession>();

        public static ConcurrentDictionary<String, CommonSession> lls = new ConcurrentDictionary<String, CommonSession>();

        //5分钟
        public static int clearTime = ConfigurationManager.AppSettings["clearTime"] == null ? 300000 : Convert.ToInt32(ConfigurationManager.AppSettings["clearTime"].ToString());

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CommonSession getTh10w(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.th10w.TryGetValue(key, out cs);
            return cs;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void addTh10w(string key, CommonSession cs)
        {
            ThJob.updateTh10w(key, cs);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void updateTh10w(string key, CommonSession cs)
        {
            if (ThJob.th10w == null)
            {
                ThJob.th10w = new ConcurrentDictionary<String, CommonSession>();
            }
            ThJob.th10w.AddOrUpdate(key, cs, (oldKey, oldValue) => cs);
        }
        
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void removeTh10w(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.th10w.TryRemove(key, out cs);
        }

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CommonSession getTh11s(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.th11s.TryGetValue(key, out cs);
            return cs;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void addTh11s(string key, CommonSession cs)
        {
            ThJob.updateTh11s(key, cs);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void updateTh11s(string key, CommonSession cs)
        {
            if (ThJob.th11s == null)
            {
                ThJob.th11s = new ConcurrentDictionary<String, CommonSession>();
            }
            ThJob.th11s.AddOrUpdate(key, cs, (oldKey, oldValue) => cs);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void removeTh11s(string key)
        {
            CommonSession cs = null;
            ThJob.th11s.TryRemove(key, out cs);
        }

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CommonSession getAnemoclinoGrap(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.anemoclinoGrap.TryGetValue(key, out cs);
            return cs;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void addAnemoclinoGrap(string key, CommonSession cs)
        {
            ThJob.updateAnemoclinoGrap(key, cs);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void updateAnemoclinoGrap(string key, CommonSession cs)
        {
            if (ThJob.anemoclinoGrap == null)
            {
                ThJob.anemoclinoGrap = new ConcurrentDictionary<String, CommonSession>();
            }
            ThJob.anemoclinoGrap.AddOrUpdate(key, cs, (oldKey, oldValue) => cs);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void removeAnemoclinoGrap(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.anemoclinoGrap.TryRemove(key, out cs);
        }

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CommonSession getTw(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.tw.TryGetValue(key, out cs);
            return cs;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void addTw(string key, CommonSession cs)
        {
            ThJob.updateTw(key, cs);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void updateTw(string key, CommonSession cs)
        {
            if (ThJob.tw == null)
            {
                ThJob.tw = new ConcurrentDictionary<String, CommonSession>();
            }
            ThJob.tw.AddOrUpdate(key, cs, (oldKey, oldValue) => cs);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void removeTw(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.tw.TryRemove(key, out cs);
        }

        /// <summary>
        /// 获取工作状态
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool twIsWorking(string key)
        {
            CommonSession cs = ThJob.getTw(key);
            return cs == null ? false : cs.isWorking;
        }

        /// <summary>
        /// 设置工作状态
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void setTwIsWorking(string key, bool status)
        {
            CommonSession cs = ThJob.getTw(key);
            cs.isWorking = status;
        }

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CommonSession getLw(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.lw.TryGetValue(key, out cs);
            return cs;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void addLw(string key, CommonSession cs)
        {
            ThJob.updateLw(key, cs);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void updateLw(string key, CommonSession cs)
        {
            if (ThJob.lw == null)
            {
                ThJob.lw = new ConcurrentDictionary<String, CommonSession>();
            }
            ThJob.lw.AddOrUpdate(key, cs, (oldKey, oldValue) => cs);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void removeLw(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.lw.TryRemove(key, out cs);
        }

        /// <summary>
        /// 获取工作状态
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool lwIsWorking(string key)
        {
            CommonSession cs = ThJob.getLw(key);
            return cs == null ? false : cs.isWorking;
        }

        /// <summary>
        /// 设置工作状态
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void setLwIsWorking(string key, bool status)
        {
            CommonSession cs = ThJob.getLw(key);
            cs.isWorking = status;
        }

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CommonSession getLouverlightSensor(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.lls.TryGetValue(key, out cs);
            return cs;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void addLouverlightSensor(string key, CommonSession cs)
        {
            ThJob.updateLouverlightSensor(key, cs);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void updateLouverlightSensor(string key, CommonSession cs)
        {
            if (ThJob.lls == null)
            {
                ThJob.lls = new ConcurrentDictionary<String, CommonSession>();
            }
            ThJob.lls.AddOrUpdate(key, cs, (oldKey, oldValue) => cs);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void removeLouverlightSensor(string key)
        {
            //判断静态变量中是否存在当前SESSION
            CommonSession cs = null;
            ThJob.lls.TryRemove(key, out cs);
        }

        public void Execute(IJobExecutionContext context)
        {
            List<string> delList = new List<string>();

            //定时清理指定时间之前的SESSION连接
            foreach (KeyValuePair<string, CommonSession> th in th10w)
            {
                if (th.Value != null)
                {
                    //定时下发查询指令
                    TH10WProtocolSession session = ((TH10WProtocolSession)(th.Value).session);
                    //清理5分钟前的连接
                    if ((DateTime.Now - session.LastActiveTime).TotalMilliseconds > ThJob.clearTime)
                    {
                        //记录要删除的KEY
                        delList.Add(session.SessionID);
                        continue;
                    }
                }
            }

            //删除记录的KEY
            if (delList != null && delList.Count > 0)
            {
                foreach (string key in delList)
                {
                    ThJob.removeTh10w(key);
                }
            }

            delList = new List<string>();

            //th11s定时下发查询指令
            foreach (KeyValuePair<string, CommonSession> th in th11s)
            {
                if (th.Value != null)
                {
                    //定时下发查询指令
                    TH11SProtocolSession session = ((TH11SProtocolSession)(th.Value).session);

                    //清理5分钟前的连接
                    if ((DateTime.Now - session.LastActiveTime).TotalMilliseconds > ThJob.clearTime)
                    {
                        //记录要删除的KEY
                        delList.Add(session.SessionID);
                        continue;
                    }

                    session.Send("010300000002C40B");
                }
            }

            //删除记录的KEY
            if (delList != null && delList.Count > 0)
            {
                foreach (string key in delList)
                {
                    ThJob.removeTh11s(key);
                }
            }

            delList = new List<string>();

            //ane定时下发查询指令
            foreach (KeyValuePair<string, CommonSession> th in anemoclinoGrap)
            {
                if (th.Value != null)
                {
                    //定时下发查询指令
                    ANEMOCLINOGRAPProtocolSession session = ((ANEMOCLINOGRAPProtocolSession)(th.Value).session);

                    //清理5分钟前的连接
                    if ((DateTime.Now - session.LastActiveTime).TotalMilliseconds > ThJob.clearTime)
                    {
                        //记录要删除的KEY
                        delList.Add(session.SessionID);
                        continue;
                    }

                    session.Send("0101C1E0");
                }
            }

            //删除记录的KEY
            if (delList != null && delList.Count > 0)
            {
                foreach (string key in delList)
                {
                    ThJob.removeAnemoclinoGrap(key);
                }
            }

            delList = new List<string>();

            //ACTW-CAR小型清洁刷式温盐传感器
            foreach (KeyValuePair<string, CommonSession> th in tw)
            {
                if (th.Value != null)
                {
                    CommonSession cs = th.Value;
                    //定时下发查询指令
                    ACTWCARProtocolSession session = ((ACTWCARProtocolSession)(cs).session);

                    //清理5分钟前的连接
                    if ((DateTime.Now - session.LastActiveTime).TotalMilliseconds > ThJob.clearTime)
                    {
                        //记录要删除的KEY
                        delList.Add(session.SessionID);
                        continue;
                    }

                    if (!ThJob.twIsWorking(th.Key))
                    {
                        session.Send("1111776970652C302C0D");
                        //设置设备工作状态
                        ThJob.setTwIsWorking(th.Key, true);
                    }
                }
            }

            //删除记录的KEY
            if (delList != null && delList.Count > 0)
            {
                foreach (string key in delList)
                {
                    ThJob.removeTw(key);
                }
            }

            delList = new List<string>();

            //ACLW-CAR传感器通讯协议与电气参数
            foreach (KeyValuePair<string, CommonSession> th in lw)
            {
                if (th.Value != null)
                {
                    CommonSession cs = th.Value;
                    //定时下发查询指令
                    ACLWCARProtocolSession session = ((ACLWCARProtocolSession)(cs).session);

                    //清理5分钟前的连接
                    if ((DateTime.Now - session.LastActiveTime).TotalMilliseconds > ThJob.clearTime)
                    {
                        //记录要删除的KEY
                        delList.Add(session.SessionID);
                        continue;
                    }

                    if (!ThJob.lwIsWorking(th.Key))
                    {
                        session.Send("1111776970652C302C0D");
                        ThJob.setLwIsWorking(th.Key, true);
                    }
                }
            }

            //删除记录的KEY
            if (delList != null && delList.Count > 0)
            {
                foreach(string key in delList)
                {
                    ThJob.removeLw(key);
                }
            }

            delList = new List<string>();

            //百叶箱光照传感器
            foreach (KeyValuePair<string, CommonSession> th in lls)
            {
                if (th.Value != null)
                {
                    CommonSession cs = th.Value;
                    //定时下发查询指令
                    LouverlightSensorProtocolSession session = ((LouverlightSensorProtocolSession)(cs).session);

                    //清理5分钟前的连接
                    if ((DateTime.Now - session.LastActiveTime).TotalMilliseconds > ThJob.clearTime)
                    {
                        //记录要删除的KEY
                        delList.Add(session.SessionID);
                        continue;
                    }

                    session.Send("010300000006C5C8");
                }
            }

            //删除记录的KEY
            if (delList != null && delList.Count > 0)
            {
                foreach (string key in delList)
                {
                    ThJob.removeLouverlightSensor(key);
                }
            }
        }

        
    }
}
