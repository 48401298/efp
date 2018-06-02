using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.Sys
{
    /// <summary>
    /// 系统操作LOG记录
    /// </summary>
    [Serializable]
    [DBTable(TableName = "T_SYSTEM_OPERATE_LOG")]
    public class SystemOperateLog : BaseEntity 
    {
        /// <summary>
        /// 记录主键
        /// </summary>
        [DBColumn(ColumnName = "OPERATEID", DataType = DbType.String, IsKey = true)]
        public string OperateID { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [DBColumn(ColumnName = "CLIENTIP", DataType = DbType.String)]
        public string ClientIP { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [DBColumn(ColumnName = "USERID", DataType = DbType.String)]
        public string UserID { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [DBColumn(ColumnName = "OPERATETYPE", DataType = DbType.String)]
        public string OperateType { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperateTypeName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [DBColumn(ColumnName = "OPERATETIME", DataType = DbType.String)]
        public string OperateTime { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        [DBColumn(ColumnName = "OPERATECONTENT", DataType = DbType.String)]
        public string OperateContent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DBColumn(ColumnName = "REMAEK", DataType = DbType.String)]
        public string Remaek { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public string StartDate { set; get; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public string EndDate { set; get; }
    }
}
