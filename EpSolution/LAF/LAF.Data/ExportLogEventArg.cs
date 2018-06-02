using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data
{
    /// <summary>
    /// 输出数据日志事件参数
    /// </summary>
    public class ExportLogEventArg
    {
        /// <summary>
        /// Sql日志
        /// </summary>
        public List<DataLogInfo> LogInfos { get; set; }
    }
}
