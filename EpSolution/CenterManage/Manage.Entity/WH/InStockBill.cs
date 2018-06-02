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
    ///　模块名称：入库单
    ///　作    者：李炳海
    ///　编写日期：2017年07月11日
    /// </summary>
    [DBTable(TableName = "T_WH_InStockBill")]
    public class InStockBill : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///入库单号
        ///</summary>
        [DBColumn(ColumnName = "BillNO", DataType = DbType.String)]
        public string BillNO { get; set; }

        ///<summary>
        ///入库日期
        ///</summary>
        [DBColumn(ColumnName = "BillDate", DataType = DbType.DateTime)]
        public DateTime BillDate { get; set; }

        ///<summary>
        ///供货单位
        ///</summary>
        [DBColumn(ColumnName = "ProviderID", DataType = DbType.String)]
        public string ProviderID { get; set; }

        ///<summary>
        ///入库方式
        ///</summary>
        [DBColumn(ColumnName = "InStockMode", DataType = DbType.String)]
        public string InStockMode { get; set; }

        ///<summary>
        ///仓库
        ///</summary>
        [DBColumn(ColumnName = "Warehouse", DataType = DbType.String)]
        public string Warehouse { get; set; }

        ///<summary>
        ///交货人
        ///</summary>
        [DBColumn(ColumnName = "DeliveryPerson", DataType = DbType.String)]
        public string DeliveryPerson { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Receiver", DataType = DbType.String)]
        public string Receiver { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "WHHeader", DataType = DbType.String)]
        public string WHHeader { get; set; }

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
        /// 入库单明细
        /// </summary>
        public List<InStockDetail> Details { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 截至日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// 供货单位名称
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 货品类别
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 货品主键
        /// </summary>
        public string MatID { get; set; }

    }

}
