using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using LAF.Entity;

namespace Manage.Entity.Sys
{
    /// <summary>
    /// 数据变更痕迹明细
    /// </summary>
    public class DataMarkDetail
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        [DataMember]
        public string ColumnDes { get; set; }

        /// <summary>
        /// 原数据
        /// </summary>
        [DataMember]
        public string OldValue { get; set; }

        /// <summary>
        /// 变更后数据
        /// </summary>
        [DataMember]
        public string NewValue { get; set; }
    }
}
