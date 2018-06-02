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
    ///　模块名称：库存货品
    ///　作    者：李炳海
    ///　编写日期：2017年07月11日
    /// </summary>
    [DBTable(TableName = "T_WH_Mat")]
    public class WHMat : BaseEntity
    {
        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "MatCode", DataType = DbType.String)]
        public string MatCode { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "MatName", DataType = DbType.String)]
        public string MatName { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "BarCode", DataType = DbType.String)]
        public string BarCode { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ProductType", DataType = DbType.String)]
        public string ProductType { get; set; }

        ///<summary>
        ///主计量单位
        ///</summary>
        [DBColumn(ColumnName = "UnitCode", DataType = DbType.String)]
        public string UnitCode { get; set; }

        ///<summary>
        ///规格
        ///</summary>
        [DBColumn(ColumnName = "SpecCode", DataType = DbType.String)]
        public string SpecCode { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "InPrice", DataType = DbType.Decimal)]
        public decimal InPrice { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "OutPrice", DataType = DbType.Decimal)]
        public decimal OutPrice { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ProductPlace", DataType = DbType.String)]
        public string ProductPlace { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Pic", DataType = DbType.String)]
        public string Pic { get; set; }

        /// <summary>
        /// 保质期(天)
        /// </summary>
        [DBColumn(ColumnName = "QualityPeriod", DataType = DbType.Int32)]
        public int QualityPeriod { get; set; }

        /// <summary>
        /// 过期预警天数
        /// </summary>
        [DBColumn(ColumnName = "OverdueAlarmDay", DataType = DbType.Int32)]
        public int OverdueAlarmDay { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CREATEUSER { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CREATETIME { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UPDATEUSER { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UPDATETIME { get; set; }

        /// <summary>
        /// 主计量单位
        /// </summary>
        public string UnitName { get; set; }

        public string MatID { get; set; }

        /// <summary>
        /// 条码状态
        /// </summary>
        public string IDCodeStatus { get; set; }

        /// <summary>
        /// 货品规格
        /// </summary>
        public string MatSpecID { get; set; }

        ///<summary>
        ///操作规格
        ///</summary>
        public string OperateSpecName { get; set; }

        /// <summary>
        /// 操作单位
        /// </summary>
        public string OperateUnitName { get; set; }

        /// <summary>
        /// 核算数量
        /// </summary>
        public decimal MainUnitAmount { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string Warehouse { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string SaveSite { get; set; }

    }

}
