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
    /// 库存移动单主表
    /// </summary>
    [DBTable(TableName = "T_WH_MoveStockBill")]
    public class MoveStockBill : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        /// 移库单号
        ///</summary>
        [DBColumn(ColumnName = "BillNO", DataType = DbType.String)]
        public string BillNO { get; set; }

        ///<summary>
        /// 移动日期
        ///</summary>
        [DBColumn(ColumnName = "BillDate", DataType = DbType.DateTime)]
        public DateTime BillDate { get; set; }

        /// <summary>
        /// 移动方式
        /// </summary>
        [DBColumn(ColumnName = "MoveMode", DataType = DbType.String)]
        public string MoveMode { get; set; }

        /// <summary>
        /// 移出仓库
        /// </summary>
        [DBColumn(ColumnName = "FromWarehouse", DataType = DbType.String)]
        public string FromWarehouse { get; set; }

        /// <summary>
        /// 移出仓库负责人
        /// </summary>
        [DBColumn(ColumnName = "FromWHHeader", DataType = DbType.String)]
        public string FromWHHeader { get; set; }

        /// <summary>
        /// 移入仓库
        /// </summary>
        [DBColumn(ColumnName = "ToWarehouse", DataType = DbType.String)]
        public string ToWarehouse { get; set; }

        /// <summary>
        /// 移入仓库负责人
        /// </summary>
        [DBColumn(ColumnName = "ToWHHeader", DataType = DbType.String)]
        public string ToWHHeader { get; set; }

        ///<summary>
        /// 备注
        ///</summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

        ///<summary>
        /// 创建者
        ///</summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CREATEUSER { get; set; }

        ///<summary>
        /// 创建时间
        ///</summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CREATETIME { get; set; }

        ///<summary>
        /// 创建时间
        ///</summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UPDATEUSER { get; set; }

        ///<summary>
        /// 修改时间
        ///</summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UPDATETIME { get; set; }

        /// <summary>
        /// 明细
        /// </summary>
        public List<MoveStockDetail> Details { get; set; }

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
        public string ToWarehouseName { get; set; }

        /// <summary>
        /// 移入仓库负责人
        /// </summary>
        public string ToWHHeaderName { get; set; }
    }
}
