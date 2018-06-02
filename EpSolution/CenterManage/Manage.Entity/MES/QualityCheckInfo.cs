using System;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;
using System.Collections.Generic;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：产成品质量检查单
    ///　作    者：wanglu
    ///　编写日期：2018年02月01日
    /// </summary>
    [DBTable(TableName = "T_FP_QUALITYCHECK")]
    public class QualityCheckInfo : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///质检单号
        ///</summary>
        [DBColumn(ColumnName = "BillNO", DataType = DbType.String)]
        public string BillNO { get; set; }

        ///<summary>
        ///检查日期
        ///</summary>
        [DBColumn(ColumnName = "CheckDate", DataType = DbType.DateTime)]
        public DateTime CheckDate { get; set; }

        ///<summary>
        ///产品批次号
        ///</summary>
        [DBColumn(ColumnName = "BatchNumber", DataType = DbType.String)]
        public string BatchNumber { get; set; }

        ///<summary>
        ///质检人员
        ///</summary>
        [DBColumn(ColumnName = "CheckPerson", DataType = DbType.String)]
        public string CheckPerson { get; set; }

        ///<summary>
        ///质检描述
        ///</summary>
        [DBColumn(ColumnName = "CheckResult", DataType = DbType.String)]
        public String CheckResult { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "REMARK", DataType = DbType.String)]
        public string REMARK { get; set; }

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

        public List<QualityCheckResult> QualityCheckResults { get; set; }
        
        ///<summary>
        ///产品名称
        ///</summary>
        public string PDNAME { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string PDCODE { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string PDDATE { get; set; }


        /// <summary>
        /// 质检员
        /// </summary>
        public string CheckPersonName { get; set; }
        

    }
}
