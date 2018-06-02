using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Log
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIP { get; set; }

        /// <summary>
        /// 动作信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 相关信息
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception ErrorInfo { get; set; }
    }
}
