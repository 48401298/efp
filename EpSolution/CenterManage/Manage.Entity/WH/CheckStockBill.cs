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
    [DBTable(TableName = "T_WH_CheckStock")]
    public class CheckStockBill : BaseEntity
    {
        #region 持久化

        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///单据号
        ///</summary>
        [DBColumn(ColumnName = "BillNO", DataType = DbType.String)]
        public string BillNO { get; set; }

        ///<summary>
        ///盘点日期
        ///</summary>
        [DBColumn(ColumnName = "BillDate", DataType = DbType.DateTime)]
        public DateTime BillDate { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        [DBColumn(ColumnName = "Warehouse", DataType = DbType.String)]
        public string Warehouse { get; set; }

        /// <summary>
        /// 存储区域
        /// </summary>
        [DBColumn(ColumnName = "AreaID", DataType = DbType.String)]
        public string AreaID { get; set; }

        /// <summary>
        /// 盘点负责人
        /// </summary>
        [DBColumn(ColumnName = "CheckHeader", DataType = DbType.String)]
        public string CheckHeader { get; set; }

        /// <summary>
        /// 是否已确认
        /// </summary>
        [DBColumn(ColumnName = "IsConfirm", DataType = DbType.Int32)]
        public int IsConfirm { get; set; }

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

        /// <summary>
        /// 明细
        /// </summary>
        public List<CheckStockDetail> Details { get; set; }

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
        /// 存储区域
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string CheckHeaderName { get; set; }
    }
}
