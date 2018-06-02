using System;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.WH
{
    /// <summary>
    /// 货品规格
    /// </summary>
    [DBTable(TableName = "T_WH_MatSpec")]
    public class WHMatSpec : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// 货品主键
        /// </summary>
        [DBColumn(ColumnName = "MatID", DataType = DbType.String)]
        public string MatID { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [DBColumn(ColumnName = "UnitName", DataType = DbType.String)]
        public string UnitName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DBColumn(ColumnName = "Amount", DataType = DbType.Int32)]
        public int Amount { get; set; }

        /// <summary>
        /// 转换单位
        /// </summary>
        [DBColumn(ColumnName = "ChangeUnit", DataType = DbType.String)]
        public string ChangeUnit { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [DBColumn(ColumnName = "Description", DataType = DbType.String)]
        public string Description { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 转换单位
        /// </summary>
        public string ChangeUnitName { get; set; }
    }
}
