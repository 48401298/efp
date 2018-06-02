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
    ///　模块名称：入库单明细
    ///　作    者：李炳海
    ///　编写日期：2017年07月11日
    /// </summary>
    [DBTable(TableName = "T_WH_InStockDetail")]
    public class InStockDetail : BaseEntity
    {
        #region 持久化

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "BillID", DataType = DbType.String)]
        public string BillID { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Seq", DataType = DbType.Int32)]
        public int Seq { get; set; }

        ///<summary>
        ///货品唯一识别码
        ///</summary>
        [DBColumn(ColumnName = "MatBarCode", DataType = DbType.String)]
        public string MatBarCode { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "MatID", DataType = DbType.String)]
        public string MatID { get; set; }

        [DBColumn(ColumnName = "MatSpec", DataType = DbType.String)]
        public string MatSpec { get; set; }

        [DBColumn(ColumnName = "MainUnitAmount", DataType = DbType.Decimal)]
        public decimal MainUnitAmount { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "InAmount", DataType = DbType.Decimal)]
        public decimal InAmount { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "InPrice", DataType = DbType.Decimal)]
        public decimal InPrice { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "InSum", DataType = DbType.Decimal)]
        public decimal InSum { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "SaveSite", DataType = DbType.String)]
        public string SaveSite { get; set; }        

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
        /// 生产日期
        /// </summary>
        [DBColumn(ColumnName = "ProduceDate", DataType = DbType.String)]
        public string ProduceDate { get; set; }

        #endregion        


        public string DeleteAction { get; set; }

        /// <summary>
        /// 货品编号
        /// </summary>
        public string MatCode { get; set; }

        /// <summary>
        /// 货品名称
        /// </summary>
        public string MatName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string SpecCode { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitCode { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 主计量单位
        /// </summary>
        public string MainUnitName { get; set; }

        /// <summary>
        /// 入库规格
        /// </summary>
        public string InSpecName { get; set; }

    }

}
