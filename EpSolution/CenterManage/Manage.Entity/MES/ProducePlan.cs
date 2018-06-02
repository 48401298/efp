using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;
using LAF.Entity;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：产品信息
    ///　作    者：wanglu
    ///　编写日期：2017年09月10日
    /// </summary>
    [DBTable(TableName = "T_FP_PRODUCEPLAN")]
    public class ProducePlan : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///所属工厂主键
        ///</summary>
        [DBColumn(ColumnName = "FACTORYPID", DataType = DbType.String)]
        public string FACTORYPID { get; set; }

        ///<summary>
        ///生产线主键
        ///</summary>
        [DBColumn(ColumnName = "PRID", DataType = DbType.String)]
        public String PRID { get; set; }

        ///<summary>
        ///日期
        ///</summary>
        [DBColumn(ColumnName = "PLANDATE", DataType = DbType.DateTime)]
        public DateTime PLANDATE { get; set; }

        ///<summary>
        ///产品主键
        ///</summary>
        [DBColumn(ColumnName = "PRODUCTIONID", DataType = DbType.String)]
        public string PRODUCTIONID { get; set; }

        ///<summary>
        ///计划产量
        ///</summary>
        [DBColumn(ColumnName = "PLANAMOUNT", DataType = DbType.Decimal)]
        public string PLANAMOUNT { get; set; }

        ///<summary>
        ///实际产量
        ///</summary>
        [DBColumn(ColumnName = "FACTAMOUNT", DataType = DbType.Decimal)]
        public string FACTAMOUNT { get; set; }

        ///<summary>
        ///批次号
        ///</summary>
        [DBColumn(ColumnName = "BATCHNUMBER", DataType = DbType.String)]
        public string BATCHNUMBER { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "REMARK", DataType = DbType.String)]
        public string REMARK { get; set; }

        ///<summary>
        ///状态
        ///</summary>
        [DBColumn(ColumnName = "STATUS", DataType = DbType.String)]
        public string STATUS { get; set; }

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
        ///分厂名称
        ///</summary>
        public string FNAME { get; set; }

        ///<summary>
        ///流水线名称
        ///</summary>
        public string PLNAME { get; set; }

        ///<summary>
        ///产品名称
        ///</summary>
        public string PDNAME { get; set; }

        /// <summary>
        /// 物料组成
        /// </summary>
        public string Materials { get; set; }

        /// <summary>
        /// 未完成工序
        /// </summary>
        public string UnFinishedProcess { get; set; }

        /// <summary>
        /// 已完成工序
        /// </summary>
        public string FinishedProcess { get; set; }

        /// <summary>
        /// 生产开始时间
        /// </summary>
        public string StartTime { get; set; }
    }
}
