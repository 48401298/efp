using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：产品信息
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    [DBTable(TableName = "T_FP_PRODUCTINFO")]
    public class ProductInfo
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///产品编号
        ///</summary>
        [DBColumn(ColumnName = "PCODE", DataType = DbType.String)]
        public string PCODE { get; set; }

        ///<summary>
        ///产品名称
        ///</summary>
        [DBColumn(ColumnName = "PNAME", DataType = DbType.String)]
        public String PNAME { get; set; }

        ///<summary>
        ///工艺流程
        ///</summary>
        [DBColumn(ColumnName = "PRID", DataType = DbType.String)]
        public string PRID { get; set; }

        ///<summary>
        ///规格
        ///</summary>
        [DBColumn(ColumnName = "SPECIFICATION", DataType = DbType.String)]
        public string SPECIFICATION { get; set; }

        ///<summary>
        ///单位
        ///</summary>
        [DBColumn(ColumnName = "UNIT", DataType = DbType.String)]
        public string UNIT { get; set; }

        ///<summary>
        ///制造商
        ///</summary>
        [DBColumn(ColumnName = "Manufacturer", DataType = DbType.String)]
        public string Manufacturer { get; set; }

        ///<summary>
        ///生产地址
        ///</summary>
        [DBColumn(ColumnName = "ProductionAddress", DataType = DbType.String)]
        public string ProductionAddress { get; set; }

        ///<summary>
        ///保质期
        ///</summary>
        [DBColumn(ColumnName = "QualityPeriod", DataType = DbType.String)]
        public string QualityPeriod { get; set; }

        ///<summary>
        ///生产许可证号
        ///</summary>
        [DBColumn(ColumnName = "ProductionLicense", DataType = DbType.String)]
        public string ProductionLicense { get; set; }

        ///<summary>
        ///产品标准号
        ///</summary>
        [DBColumn(ColumnName = "ProductStandard", DataType = DbType.String)]
        public string ProductStandard { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "REMARK", DataType = DbType.String)]
        public string REMARK { get; set; }

        ///<summary>
        ///是否停用
        ///</summary>
        [DBColumn(ColumnName = "FLGACTIVE", DataType = DbType.String)]
        public string FLGACTIVE { get; set; }

        ///<summary>
        ///删除标识
        ///</summary>
        [DBColumn(ColumnName = "FLGDEL", DataType = DbType.String)]
        public string FLGDEL { get; set; }

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
        ///工艺流程名称
        ///</summary>
        public string PRNAME { get; set; }
        
    }
}
