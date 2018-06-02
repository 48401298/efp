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
    ///　模块名称：出库单
    ///　作    者：李炳海
    ///　编写日期：2017年07月11日
    /// </summary>
    [DBTable(TableName = "T_WH_OutStockBill")]
    public class OutStockBill : BaseEntity
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
        [DBColumn(ColumnName = "BillNO", DataType = DbType.String)]
        public string BillNO { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "BillDate", DataType = DbType.DateTime)]
        public DateTime BillDate { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ClientCode", DataType = DbType.String)]
        public string ClientCode { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "OutStockMode", DataType = DbType.String)]
        public string OutStockMode { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Warehouse", DataType = DbType.String)]
        public string Warehouse { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Header", DataType = DbType.String)]
        public string Header { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "WHHeader", DataType = DbType.String)]
        public string WHHeader { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "HandlePerson", DataType = DbType.String)]
        public string HandlePerson { get; set; }

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
        /// 出库单明细
        /// </summary>
        public List<OutStockDetail> Details { get; set; }

        #endregion

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
        /// 收货单位名称
        /// </summary>
        public string ClientName { get; set; }

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
