using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.WH
{
    /// <summary>
    /// 仓库权限
    /// </summary>
    [DBTable(TableName = "T_WH_WHPower")]
    public class WarehousePower : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// 用户主键
        /// </summary>
        [DBColumn(ColumnName = "UserID", DataType = DbType.String)]
        public string UserID { get; set; }

        /// <summary>
        /// 仓库主键
        /// </summary>
        [DBColumn(ColumnName = "WarehouseID", DataType = DbType.String)]
        public string WarehouseID { get; set; }
    }
}
