using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity
{
    /// <summary>
    /// 返回结果
    /// 创建者：李炳海
    /// 创建日期：2014.8.8
    /// </summary>
    public class DataResult
    {
        /// <summary>
        /// 消息代码
        /// </summary>
        public string MsgCode { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 异常消息
        /// </summary>
        public Exception Ex { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        public int RequestStatus { get; set; }

        public bool BackStatus { get; set; }
    }

    /// <summary>
    /// 返回参数数据模型
    /// 创建者：单雨春
    /// 创建日期：2013年4月20日
    /// </summary>
    public class DataResult<T> : DataResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public T Result { get; set; }
    }
}