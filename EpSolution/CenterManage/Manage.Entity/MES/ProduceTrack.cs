using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Entity;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：追溯跟踪信息
    ///　作    者：wanglu
    ///　编写日期：2017年09月10日
    /// </summary>
    [DBTable(TableName = "T_FP_PRODUCETRACK")]
    public class ProduceTrack : BaseEntity
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
        public string PRID { get; set; }

        ///<summary>
        ///班组主键
        ///</summary>
        [DBColumn(ColumnName = "WOID", DataType = DbType.String)]
        public string WOID { get; set; }

        ///<summary>
        ///操作人员
        ///</summary>
        [DBColumn(ColumnName = "OPERATOR", DataType = DbType.String)]
        public string OPERATOR { get; set; }

        ///<summary>
        ///日期
        ///</summary>
        [DBColumn(ColumnName = "PLANDATE", DataType = DbType.DateTime)]
        public DateTime PLANDATE { get; set; }

        ///<summary>
        ///原料主键
        ///</summary>
        [DBColumn(ColumnName = "MATID", DataType = DbType.String)]
        public string MATID { get; set; }

        ///<summary>
        ///物料唯一识别码
        ///</summary>
        [DBColumn(ColumnName = "MATBARCODE", DataType = DbType.String)]
        public string MATBARCODE { get; set; }

        ///<summary>
        ///产品主键
        ///</summary>
        [DBColumn(ColumnName = "PRODUCTIONID", DataType = DbType.String)]
        public String PRODUCTIONID { get; set; }

        ///<summary>
        ///产品唯一识别码
        ///</summary>
        [DBColumn(ColumnName = "GOODBARCODE", DataType = DbType.String)]
        public string GOODBARCODE { get; set; }

        ///<summary>
        ///批次号
        ///</summary>
        [DBColumn(ColumnName = "BATCHNUMBER", DataType = DbType.String)]
        public string BATCHNUMBER { get; set; }

        ///<summary>
        ///工序主键
        ///</summary>
        [DBColumn(ColumnName = "WPID", DataType = DbType.String)]
        public string WPID { get; set; }

        ///<summary>
        ///设备主键
        ///</summary>
        [DBColumn(ColumnName = "EQUID", DataType = DbType.String)]
        public string EQUID { get; set; }

        ///<summary>
        ///器具主键
        ///</summary>
        [DBColumn(ColumnName = "CONTAINERID", DataType = DbType.String)]
        public string CONTAINERID { get; set; }

        ///<summary>
        ///工位主键
        ///</summary>
        [DBColumn(ColumnName = "WSID", DataType = DbType.String)]
        public string WSID { get; set; }
        
        ///<summary>
        ///加工开始时间
        ///</summary>
        [DBColumn(ColumnName = "WORKINGSTARTTIME", DataType = DbType.DateTime)]
        public DateTime WORKINGSTARTTIME { get; set; }

        ///<summary>
        ///加工结束时间
        ///</summary>
        [DBColumn(ColumnName = "WORKINGENDTIME", DataType = DbType.DateTime)]
        public DateTime WORKINGENDTIME { get; set; }

        ///<summary>
        ///视频信息主键
        ///</summary>
        [DBColumn(ColumnName = "VIDEOID", DataType = DbType.String)]
        public string VIDEOID { get; set; }

        ///<summary>
        ///完成状态
        ///</summary>
        [DBColumn(ColumnName = "STATUS", DataType = DbType.String)]
        public string STATUS { get; set; }

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

        /// <summary>
        /// 产品名称
        /// </summary>
        public string PNAME { get; set; }

        /// <summary>
        /// 设备条码
        /// </summary>
        public string CBBARCODE { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string CBNAME { get; set; }

        /// <summary>
        /// 工位
        /// </summary>
        public string WSCODE { get; set; }

        /// <summary>
        /// 工序
        /// </summary>
        public string GXNAME { get; set; }
        
    }
}
