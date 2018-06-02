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
    /// 组织机构信息
    /// </summary>
    [Serializable]
    [DBTable(TableName = "T_ORGANIZATION")]
    public class Orgaization : BaseEntity
    {
        /// <summary>
        /// 组织机构编号
        /// </summary>
        [DBColumn(ColumnName = "ORGANID", DataType = DbType.String, IsKey = true)]
        public string OrganID { get; set; }

        /// <summary>
        /// 组织机构描述
        /// </summary>
        [DBColumn(ColumnName = "ORGANDESC", DataType = DbType.String)]
        public string OrganDESC { get; set; }

        /// <summary>
        /// 上级组织机构
        /// </summary>
        [DBColumn(ColumnName = "ORGANPARENT", DataType = DbType.String)]
        public string OrganParent { get; set; }
        /// <summary>
        /// 上级组织机构描述
        /// </summary>
        public string OrganParentDESC { get; set; }
        /// <summary>
        /// 删除标识位
        /// </summary>
        [DBColumn(ColumnName = "DELFLAG", DataType = DbType.String)]
        public string DelFlag { get; set; }
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
        /// 组织机构所具有的角色
        /// </summary>
        public List<RoleInfo> OrgaRoleList { get; set; }

        /// <summary>
        /// 组织机构集合
        /// </summary>
        public List<Orgaization> Orgas { get; set; }
    }
}
