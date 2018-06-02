using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.Sys
{
    /// <summary>
    /// 用户权限信息
    /// </summary>
    [Serializable]
    [DBTable(TableName = "T_USERAUTHORITY", TimeStampColumn = "UpdateTime")]
    public class UserAuthority : BaseEntity
    {

        /// <summary>
        /// 用户编号
        /// </summary>
        [DBColumn(ColumnName = "USERID", DataType = DbType.String)]
        public string UserID { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        [DBColumn(ColumnName = "AUTHORITYID", DataType = DbType.String)]
        public string AuthorityID { get; set; }

        /// <summary>
        /// 组织机构编号
        /// </summary>
        [DBColumn(ColumnName = "ORGAID", DataType = DbType.String)]
        public string OrgaID { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.Date)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CreateUser { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.Date)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UpdateUser { get; set; }

    }
}
