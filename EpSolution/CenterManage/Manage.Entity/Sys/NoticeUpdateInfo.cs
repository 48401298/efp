using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.Sys
{
    /// <summary>
    /// 公告信息
    /// </summary>
    [DataContract]
    [DBTable(TableName = "T_NOTICE", TimeStampColumn = "UpdateTime")]
    public class NoticeUpdateInfo : BaseEntity
    {
        /// <summary>
        /// 公告主键
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "NOTICEID", DataType = DbType.String, IsKey = true)]
        public string NoticeID { get; set; }

        /// <summary>
        /// 公告标题
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "NOTICETITLE", DataType = DbType.String)]
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "NOTICECONTEXT", DataType = DbType.String)]
        public string NoticeContext { get; set; }

        /// <summary>
        /// 公告时间
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "USETIME", DataType = DbType.DateTime)]
        public DateTime UseTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "OUTTIME", DataType = DbType.DateTime)]
        public DateTime OutTime { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "ATTACHFILE", DataType = DbType.String)]
        public string AttachFile { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [DataMember]
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UpdateUser { get; set; }
      
    }
}
