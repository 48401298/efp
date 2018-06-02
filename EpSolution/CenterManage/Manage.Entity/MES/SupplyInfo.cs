using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：要货信息
    ///　作    者：wanglu
    ///　编写日期：2018年01月10日
    /// </summary>
    [DBTable(TableName = "T_FP_SUPPLYINFO")]
    public class SupplyInfo : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///生产计划主键
        ///</summary>
        [DBColumn(ColumnName = "PLANID", DataType = DbType.String)]
        public string PLANID { get; set; }

        ///<summary>
        ///批次号
        ///</summary>
        [DBColumn(ColumnName = "BATCHNUMBER", DataType = DbType.String)]
        public string BATCHNUMBER { get; set; }

        ///<summary>
        ///仓库
        ///</summary>
        [DBColumn(ColumnName = "Warehouse", DataType = DbType.String)]
        public string Warehouse { get; set; }

        ///<summary>
        ///配送日期
        ///</summary>
        [DBColumn(ColumnName = "DELIVERYDATE", DataType = DbType.DateTime)]
        public DateTime DELIVERYDATE { get; set; }

        ///<summary>
        ///备注
        ///</summary>
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

        ///<summary>
        ///产品主键
        ///</summary>
        public string ProductionID { get; set; }

        ///<summary>
        ///产品名称
        ///</summary>
        public string ProduceName { get; set; }

        ///<summary>
        ///工厂名称
        ///</summary>
        public string FactoryName { get; set; }

        ///<summary>
        ///生产线名称
        ///</summary>
        public string PLName { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// 产品BOM明细
        /// </summary>
        public List<SupplyMaterialInfo> Details { get; set; }
    }
}
