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
    /// 库存台账
    /// </summary>
    [DBTable(TableName = "T_WH_MonthAccount")]
    public class WHMonthAccount : BaseEntity
    {
        #region 持久化

        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        [DBColumn(ColumnName = "Warehouse", DataType = DbType.String)]
        public string Warehouse { get; set; }

        /// <summary>
        /// 货品主键
        /// </summary>
        [DBColumn(ColumnName = "MatID", DataType = DbType.String)]
        public string MatID { get; set; }

        /// <summary>
        /// 主计量单位
        /// </summary>
        [DBColumn(ColumnName = "MainUnitID", DataType = DbType.String)]
        public string MainUnitID { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        [DBColumn(ColumnName = "AccountYear", DataType = DbType.Int32)]
        public int AccountYear { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        [DBColumn(ColumnName = "AccountMonth", DataType = DbType.Int32)]
        public int AccountMonth { get; set; }

        /// <summary>
        /// 期初数量
        /// </summary>
        [DBColumn(ColumnName = "PrimeAmount", DataType = DbType.Decimal)]
        public decimal PrimeAmount { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        [DBColumn(ColumnName = "InAmount", DataType = DbType.Decimal)]
        public decimal InAmount { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
        [DBColumn(ColumnName = "OutAmount", DataType = DbType.Decimal)]
        public decimal OutAmount { get; set; }

        /// <summary>
        /// 盘盈数量
        /// </summary>
        [DBColumn(ColumnName = "GainAmount", DataType = DbType.Decimal)]
        public decimal GainAmount { get; set; }

        /// <summary>
        /// 盘亏数量
        /// </summary>
        [DBColumn(ColumnName = "LossAmount", DataType = DbType.Decimal)]
        public decimal LossAmount { get; set; }

        /// <summary>
        /// 期末数量
        /// </summary>
        [DBColumn(ColumnName = "LateAmount", DataType = DbType.Decimal)]
        public decimal LateAmount { get; set; }

        ///<summary>
        ///创建者
        ///</summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CREATEUSER { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CREATETIME { get; set; }

        ///<summary>
        ///修改者
        ///</summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UPDATEUSER { get; set; }

        ///<summary>
        ///修改时间
        ///</summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UPDATETIME { get; set; }

        #endregion

        /// <summary>
        /// 货品类别
        /// </summary>
        public string ProductType { get; set; }

        public string WarehouseName { get; set; }

        public string UnitName { get; set; }

        public string MatCode { get; set; }

        public string MatName { get; set; }

        public string UserID { get; set; }
    }
}
