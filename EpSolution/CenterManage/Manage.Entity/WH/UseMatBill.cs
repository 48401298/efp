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
    /// 领料信息
    /// </summary>
    [DBTable(TableName = "T_WH_UseMat")]
    public class UseMatBill
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        /// <summary>
        /// 要货单主键
        /// </summary>
        [DBColumn(ColumnName = "SUPPLYID", DataType = DbType.String)]
        public string SUPPLYID { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        [DBColumn(ColumnName = "Warehouse", DataType = DbType.String)]
        public string Warehouse { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [DBColumn(ColumnName = "BatchNumber", DataType = DbType.String)]
        public string BatchNumber { get; set; }

        /// <summary>
        /// 领料日期
        /// </summary>
        [DBColumn(ColumnName = "USEDATE", DataType = DbType.DateTime)]
        public DateTime USEDATE { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DBColumn(ColumnName = "REMARK", DataType = DbType.String)]
        public string REMARK { get; set; }

        ///<summary>
        ///创建人
        ///</summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CREATEUSER { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CREATETIME { get; set; }

        ///<summary>
        ///更新人
        ///</summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UPDATEUSER { get; set; }

        ///<summary>
        ///更新时间
        ///</summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UPDATETIME { get; set; }

        /// <summary>
        /// 领料数量列表
        /// </summary>
        public List<UseMatAmount> Amounts { get; set; }

        /// <summary>
        /// 明细
        /// </summary>
        public List<UseMatDetail> Details { get; set; }

        ///<summary>
        ///产品名称
        ///</summary>
        public string ProduceName { get; set; }

        ///<summary>
        ///工厂名称
        ///</summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string PLName { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string WarehouseName { get; set; }

        ///<summary>
        ///配送日期
        ///</summary>
        public DateTime DELIVERYDATE { get; set; }
    }
}
