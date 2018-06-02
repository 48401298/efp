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
    /// 角色与权限信息
    /// </summary>
    [Serializable]
    [DBTable(TableName = "T_ROLEAUTHORITY")]
    public class RoleAuthority : BaseEntity 
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        [DBColumn(ColumnName = "ROLEID", DataType = DbType.String)]
        public string RoleID { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        [DBColumn(ColumnName = "AUTHORITYID", DataType = DbType.String)]
        public string AuthorityID { get; set; }

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
