using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.WH
{
    /// </summary>
    ///　模块名称：货品库存数量
    ///　作    者：李炳海
    ///　编写日期：2017年07月11日
    /// </summary>
    [DBTable(TableName = "T_WH_MatAmount")]
    public class WHMatAmount : BaseEntity
    {
        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Warehouse", DataType = DbType.String)]
        public string Warehouse { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "SaveSite", DataType = DbType.String)]
        public string SaveSite { get; set; }

        /// <summary>
        /// 货品唯一识别码
        /// </summary>
        [DBColumn(ColumnName = "MatBarCode", DataType = DbType.String)]
        public string MatBarCode { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "MatID", DataType = DbType.String)]
        public string MatID { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ProductAmount", DataType = DbType.Decimal)]
        public decimal ProductAmount { get; set; }

        [DBColumn(ColumnName = "Unit", DataType = DbType.String)]
        public string Unit { get; set; }

        [DBColumn(ColumnName = "MainAmount", DataType = DbType.Decimal)]
        public decimal MainAmount { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ProductPrice", DataType = DbType.Decimal)]
        public decimal ProductPrice { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ProductSum", DataType = DbType.Decimal)]
        public decimal ProductSum { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        [DBColumn(ColumnName = "ProduceDate", DataType = DbType.DateTime)]
        public DateTime ProduceDate { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "CreateUser", DataType = DbType.String)]
        public string CreateUser { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "CreateTime", DataType = DbType.DateTime)]
        public DateTime CreateTime { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "UpdateUser", DataType = DbType.String)]
        public string UpdateUser { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "UpdateTime", DataType = DbType.DateTime)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否显示明细
        /// </summary>
        public bool IsDetail { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        public string MatCode { get; set; }

        /// <summary>
        /// 货品名称
        /// </summary>
        public string MatName { get; set; }

        /// <summary>
        /// 货品规格
        /// </summary>
        public string MatSpec { get; set; }

        /// <summary>
        /// 货品类别
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 主计量单位
        /// </summary>
        public string MainUnitName { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// 保质期(天)
        /// </summary>
        public int QualityPeriod { get; set; }

    }

}
