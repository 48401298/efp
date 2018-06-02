using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;
using Manage.Entity.WH;

namespace Manage.Entity.Sys
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    [DBTable(TableName = "T_USER")]
    public class User : BaseEntity 
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        [DBColumn(ColumnName = "USERID", DataType = DbType.String, IsKey = true)]
        public string UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DBColumn(ColumnName = "LOGINUSERID", DataType = DbType.String)]
        public string LoginUserID { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [DBColumn(ColumnName = "USERNAME", DataType = DbType.String)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DBColumn(ColumnName = "PASSWORD", DataType = DbType.String)]
        public string PassWord { get; set; }

        /// <summary>
        /// 所在机构
        /// </summary>
        [DBColumn(ColumnName = "ORGANID", DataType = DbType.String)]
        public string OrganID { get; set; }

        /// <summary>
        /// 所在机构描述
        /// </summary>
        public string OrganDESC { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        [DBColumn(ColumnName = "TEL", DataType = DbType.String)]
        public string Tel { get; set; }

        /// <summary>
        /// 是否停用
        /// </summary>
        [DBColumn(ColumnName = "ISSTOP", DataType = DbType.String)]
        public string IsStop { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [DBColumn(ColumnName = "MOBILETEL", DataType = DbType.String)]
        public string MobileTel { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [DBColumn(ColumnName = "EMAIL", DataType = DbType.String)]
        public string Email { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        [DBColumn(ColumnName = "DELFLAG", DataType = DbType.String)]
        public string DelFlag { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DBColumn(ColumnName = "CREATETIME")]
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
        /// 角色列表 
        /// </summary>
        public List<UserRole> Roles { get; set; }

        /// <summary>
        /// 仓库权限
        /// </summary>
        public List<WarehousePower> WHPowers { get; set; }
        
    }
}
