using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Log
{
    /// <summary>
    /// 日志帮助类接口
    /// </summary>
    public interface ILogHelper
    {
        /// <summary>
        /// 加载配置信息
        /// </summary>
        void Configure();

        /// <summary>
        /// 记录行为
        /// </summary>
        void Info(LogInfo log);

        /// <summary>
        /// 记录调试
        /// </summary>
        void Debug(LogInfo log);

        /// <summary>
        /// 记录异常
        /// </summary>
        void Error(LogInfo log);
    }
}
