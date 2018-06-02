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
    /// 角色信息
    /// </summary>
    [Serializable]
    [DBTable(TableName = "T_USERROLE")]
    public class UserRole : BaseEntity
    {

        /// <summary>
        /// 用户编号
        /// </summary>
        [DBColumn(ColumnName = "USERID", DataType = DbType.String)]
        public string UserID { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        [DBColumn(ColumnName = "ROLEID", DataType = DbType.String)]
        public string RoleID { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDESC { get; set; }

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
