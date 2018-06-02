
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data.Attributes
{
    /// <summary>
    /// 数据库表属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DBTableAttribute : System.Attribute
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        public string TableDes { get; set; }

        /// <summary>
        /// 时间戳实体属性
        /// </summary>
        public string TimeStampColumn { get; set; }

    }
}
