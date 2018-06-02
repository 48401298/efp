using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Entity
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class BusinessResult
    {
        public BusinessResult()
        {
            Type = BusinessResultType.OK;
        }
        public BusinessResultType Type { get; set; }
        public string Message { get; set; }
        public int RecortCount { get; set; }
        public Exception ex { get; set; }
    }

    public enum BusinessResultType
    {
        /// <summary>
        /// 处理正常
        /// </summary>
        OK,

        /// <summary>
        /// 终止
        /// </summary>
        NG,

        /// <summary>
        /// 异常
        /// </summary>
        Exception,

        /// <summary>
        /// 其他
        /// </summary>
        Other,
    }
}
