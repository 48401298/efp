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
    /// 角色信息
    /// </summary>
    [Serializable]
    [DBTable(TableName = "T_ROLE")]
    public class RoleInfo : BaseEntity 
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        [DBColumn(ColumnName = "ROLEID", DataType = DbType.String, IsKey = true)]
        public string RoleID { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [DBColumn(ColumnName = "ROLEDESC", DataType = DbType.String)]
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

        /// <summary>
        /// 权限设置
        /// </summary>
        public List<RoleAuthority> Powers { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<RoleInfo> Roles { get; set; }
        /// <summary>
        /// 用户角色列表
        /// </summary>
        public IList<UserRole> UserRoles { get; set; }
    }
}
