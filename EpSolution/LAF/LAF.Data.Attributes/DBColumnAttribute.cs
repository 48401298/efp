
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.ComponentModel;

namespace LAF.Data.Attributes
{
    /// <summary>
    /// 数据默认值类型
    /// </summary>
    [Description("数据默认值类型")]
    public enum DataDefaultValue
    {
        /// <summary>
        /// 无默认值
        /// </summary>
        [Description("无默认值")]
        None,
        /// <summary>
        /// 系统日期
        /// </summary>
        [Description("系统日期")]
        SysDate,

        /// <summary>
        /// 空字符串
        /// </summary>
        [Description("空字符串")]
        Empty,

        /// <summary>
        /// 零
        /// </summary>
        [Description("零")]
        Zero

    }

    /// <summary>
    /// 数据库列属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DBColumnAttribute : System.Attribute
    {

        private bool _iskey = false;

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 列描述
        /// </summary>
        public string ColumnDes { get; set; }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsKey
        {
            get { return _iskey; }
            set { _iskey = value; }
        }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        public DbType DataType { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public DataDefaultValue DefaultValue { get; set; }

    }


}
