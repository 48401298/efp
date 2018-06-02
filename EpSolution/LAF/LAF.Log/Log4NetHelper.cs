using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using LAF.Common.Serialization;

namespace LAF.Log
{
    /// <summary>
    /// logfornet帮助类
    /// </summary>
    public class Log4NetHelper : ILogHelper
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Log4NetHelper()
        {

        }

        #endregion

        #region 加载配置信息

        /// <summary>
        /// 加载配置信息
        /// </summary>
        public void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        #endregion

        #region 记录行为

        /// <summary>
        /// 记录行为
        /// </summary>
        public void Info(LogInfo log)
        {
            try
            {
                ILog loger = GetLoger("loginfo");

                string info = this.GetLogString(log);
               
                //记录日志
                loger.Info(info + "\r\n");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region 记录调试

        /// <summary>
        /// 记录调试
        /// </summary>
        public void Debug(LogInfo log)
        {
            try
            {
                ILog loger = GetLoger("debuginfo");

                string info = this.GetLogString(log);

                //记录日志
                loger.Debug(info + "\r\n");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 记录异常

        /// <summary>
        /// 记录异常
        /// </summary>
        public void Error(LogInfo log)
        {
            try
            {
                if (log.ErrorInfo == null)
                    throw new Exception("未指定异常信息！");

                ILog loger = GetLoger("errorinfo");

                string info = this.GetLogString(log);

                //记录日志
                loger.Error(info, log.ErrorInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 将日志转成字符串

        /// <summary>
        /// 将日志转成字符串
        /// </summary>
        /// <param name="log">日志信息</param>
        /// <returns>字符串</returns>
        private string GetLogString(LogInfo log)
        {
            List<string> infos = new List<string>();
            try
            {
                //客户端IP
                if (string.IsNullOrEmpty(log.ClientIP) == false)
                {
                    infos.Add(log.ClientIP);
                }
                else
                {
                    infos.Add("null");
                }

                //登录用户名
                if (string.IsNullOrEmpty(log.UserName) == false)
                {
                    infos.Add(log.UserName);
                }
                else
                {
                    infos.Add("null");
                }

                //动作信息
                if (string.IsNullOrEmpty(log.Info) == false)
                {
                    infos.Add(log.Info);
                }
                else
                {
                    infos.Add("null");
                }

                //数据
                if (log.Tag != null)
                {
                    infos.Add(JsonConvertHelper.GetSerializes(log.Tag));
                }

                else
                {
                    infos.Add("null");
                }


                return string.Join(" ",infos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取日志记录对象

        /// <summary>
        /// 获取日志记录对象
        /// </summary>
        /// <param name="logerName">名字</param>
        /// <returns>日志记录对象</returns>
        private ILog GetLoger(string logerName)
        {
            try
            {
                ILog loger = log4net.LogManager.GetLogger(logerName);

                if (loger == null)
                    throw new Exception("为获取日志记录对象！");

                return loger;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
