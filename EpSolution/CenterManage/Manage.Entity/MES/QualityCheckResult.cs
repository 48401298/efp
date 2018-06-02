using System;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：产成品质量检查单结果
    ///　作    者：wanglu
    ///　编写日期：2018年02月01日
    /// </summary>
    [DBTable(TableName = "T_FP_QualityCheckResult")]
    public class QualityCheckResult : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///质检主键
        ///</summary>
        [DBColumn(ColumnName = "CheckID", DataType = DbType.String)]
        public string CheckID { get; set; }

        ///<summary>
        ///附件名称
        ///</summary>
        [DBColumn(ColumnName = "FileAlias", DataType = DbType.String)]
        public String FileAlias { get; set; }

        ///<summary>
        ///附件
        ///</summary>
        [DBColumn(ColumnName = "FileName", DataType = DbType.String)]
        public string FileName { get; set; }

        ///<summary>
        ///上传时间
        ///</summary>
        [DBColumn(ColumnName = "UploadTime", DataType = DbType.DateTime)]
        public DateTime UploadTime { get; set; }

        ///<summary>
        ///xiangxi 
        ///</summary>
        public String DetailAction { get; set; }

        ///<summary>
        ///删除
        ///</summary>
        public String DeleteAction { get; set; }

        public int SEQ { get; set; }

    }
}
