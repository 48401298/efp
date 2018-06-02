using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Log
{
    /// <summary>
    /// 日志管理者
    /// </summary>
    public class LogManager
    {
        public static ILogHelper LogHelper = null;

        /// <summary>
        /// 加载配置信息
        /// <param name="helperName">处理组件名</param>
        /// </summary>
        public static void Configure(string helperName)
        {
            try
            {
                //创建日志处理对象
                switch (helperName.ToLower())
                {
                    case "log4net":
                        LogHelper = new Log4NetHelper();
                        break;
                }

                //加载配置
                LogHelper.Configure();
            }
            catch (Exception ex)
            {
                LogHelper = null;
                throw ex;
            }

        }
    }
}
