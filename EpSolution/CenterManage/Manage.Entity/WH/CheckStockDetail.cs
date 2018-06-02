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
    /// 盘点单明细表
    /// </summary>
    [DBTable(TableName = "T_WH_CheckStockDetail")]
    public class CheckStockDetail:BaseEntity
    {
        #region 持久化

        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///单据主键
        ///</summary>
        [DBColumn(ColumnName = "BillID", DataType = DbType.String)]
        public string BillID { get; set; }

        ///<summary>
        ///序号
        ///</summary>
        [DBColumn(ColumnName = "Seq", DataType = DbType.Int32)]
        public int Seq { get; set; }

        /// <summary>
        /// 仓位
        /// </summary>
        [DBColumn(ColumnName = "SaveSite", DataType = DbType.String)]
        public string SaveSite { get; set; }

        /// <summary>
        /// 货品唯一识别码
        /// </summary>
        [DBColumn(ColumnName = "IDCode", DataType = DbType.String)]
        public string IDCode { get; set; }

        /// <summary>
        /// 货品主键
        /// </summary>
        [DBColumn(ColumnName = "MatID", DataType = DbType.String)]
        public string MatID { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [DBColumn(ColumnName = "Unit", DataType = DbType.String)]
        public string Unit { get; set; }
        
        /// <summary>
        /// 库存数量
        /// </summary>
        [DBColumn(ColumnName = "StockAmount", DataType = DbType.Decimal)]
        public decimal StockAmount { get; set; }

        /// <summary>
        /// 实际数量
        /// </summary>
        [DBColumn(ColumnName = "FactAmount", DataType = DbType.Decimal)]
        public decimal FactAmount { get; set; }

        /// <summary>
        /// 盘盈数量
        /// </summary>
        [DBColumn(ColumnName = "ProfitAmount", DataType = DbType.Decimal)]
        public decimal ProfitAmount { get; set; }

        /// <summary>
        /// 盘亏数量
        /// </summary>
        [DBColumn(ColumnName = "LossAmount", DataType = DbType.Decimal)]
        public decimal LossAmount { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

        #endregion

        /// <summary>
        /// 仓位
        /// </summary>
        public string SaveSiteName { get; set; }

        /// <summary>
        /// 货品编号
        /// </summary>
        public string MatCode { get; set; }

        /// <summary>
        /// 货品名称
        /// </summary>
        public string MatName { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitName { get; set; }
        
    }
}
