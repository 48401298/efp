using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data
{
    /// <summary>
    /// 数据日志
    /// </summary>
    public class DataLogInfo
    {
        /// <summary>
        /// 所执行的SQL语句
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<DataParameter> Parameters { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }
    }
}
