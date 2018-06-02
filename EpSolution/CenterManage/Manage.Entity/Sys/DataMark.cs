using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.Sys
{
    /// <summary>
    /// 数据痕迹
    /// </summary>
    [DataContract]
    [DBTable(TableName = "T_BD_DATAMARK")]
    public class DataMark : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "MARKID", DataType = DbType.String, IsKey = true)]
        public string MARKID { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "OPERATEUSER", DataType = DbType.String)]
        public string OPERATEUSER { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "OPERATETIME", DataType = DbType.DateTime)]
        public DateTime OPERATETIME { get; set; }

        /// <summary>
        /// 操作开始时间
        /// </summary>
        [DataMember]
        public string STARTOPERATETIME { get; set; }

        /// <summary>
        /// 操作截至时间
        /// </summary>
        [DataMember]
        public string ENDOPERATETIME { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "OPERATETYPE", DataType = DbType.String)]
        public string OPERATETYPE { get; set; }

        /// <summary>
        /// 业务数据类型(数据表名)
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "DATAKIND", DataType = DbType.String)]
        public string DATAKIND { get; set; }

        /// <summary>
        /// 数据表名
        /// </summary>
        public string DATAKINDDES { get; set; }

        /// <summary>
        /// 数据主键
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "DATAID", DataType = DbType.String)]
        public string DATAID { get; set; }

        /// <summary>
        /// 原数据(JSON)
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "ORIGINALDATA", DataType = DbType.String)]
        public string ORIGINALDATA { get; set; }

        /// <summary>
        /// 变更后数据(JSON)
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "CHANGEDDATA", DataType = DbType.String)]
        public string CHANGEDDATA { get; set; }

        /// <summary>
        /// 数据变更明细
        /// </summary>
        [DataMember]
        public List<DataMarkDetail> Details { get; set; }
    }
}
