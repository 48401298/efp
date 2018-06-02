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
    /// 用户组织机构信息
    /// </summary>
    [Serializable]
    [DBTable(TableName = "T_USERORGAIZATION", TimeStampColumn = "UpdateTime")]
    public class UserOrgaization : BaseEntity
    {

        /// <summary>
        /// 用户编号
        /// </summary>
        [DBColumn(ColumnName = "USERID", DataType = DbType.String)]
        public string UserID { get; set; }
        /// <summary>
        /// 用户描述
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 组织机构编号
        /// </summary>
        [DBColumn(ColumnName = "ORGAID", DataType = DbType.String)]
        public string OrgaID { get; set; }
        /// <summary>
        /// 组织机构描述
        /// </summary>
        public string OrgaDESC { get; set; }

        /// <summary>
        /// 是否主组织机构
        /// </summary>
        [DBColumn(ColumnName = "ISMAINORGA", DataType = DbType.String)]
        public string IsMainOgra { get; set; }

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
        /// 用户与组织机构列表 
        /// </summary>
        public List<UserOrgaization> Orgas { get; set; }

    }
}
