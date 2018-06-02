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
    /// 移库单明细表
    /// </summary>
    [DBTable(TableName = "T_WH_MoveStockDetail")]
    public class MoveStockDetail
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// 单据主键
        /// </summary>
        [DBColumn(ColumnName = "BillID", DataType = DbType.String)]
        public string BillID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [DBColumn(ColumnName = "Seq", DataType = DbType.Int32)]
        public int Seq { get; set; }

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
        /// 移动数量
        /// </summary>
        [DBColumn(ColumnName = "MoveAmount", DataType = DbType.Decimal)]
        public decimal MoveAmount { get; set; }

        /// <summary>
        /// 移出仓位
        /// </summary>
        [DBColumn(ColumnName = "FromSaveSite", DataType = DbType.String)]
        public string FromSaveSite { get; set; }

        /// <summary>
        /// 移入仓位
        /// </summary>
        [DBColumn(ColumnName = "ToSaveSite", DataType = DbType.String)]
        public string ToSaveSite { get; set; }

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
    }
}
