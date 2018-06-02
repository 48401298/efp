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
    /// 盘点单主表
    /// </summary>
    [DBTable(TableName = "T_WH_StockLimit")]
    public class WHStockLimit
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
        /// 计量单位
        /// </summary>
        [DBColumn(ColumnName = "UnitID", DataType = DbType.String)]
        public string UnitID { get; set; }

        /// <summary>
        /// 库存线下
        /// </summary>
        [DBColumn(ColumnName = "MinAmount", DataType = DbType.Decimal)]
        public decimal MinAmount { get; set; }

        /// <summary>
        /// 库存上线
        /// </summary>
        [DBColumn(ColumnName = "MaxAmount", DataType = DbType.Decimal)]
        public decimal MaxAmount { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

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
        /// 仓库名称
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 货品编号
        /// </summary>
        public string MatCode { get; set; }

        /// <summary>
        /// 货品名称
        /// </summary>
        public string MatName { get; set; }
    }
}
