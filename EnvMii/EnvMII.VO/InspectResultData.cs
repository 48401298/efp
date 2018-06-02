using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using LAF.Data.Attributes;
using System.Data;

namespace EnvMII.VO
{
    [DBTable(TableName = "inspectcalcresult")]
    public class InspectResultData
    {
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [DBColumn(ColumnName = "DeviceCode", DataType = DbType.String)]
        public string DeviceCode { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        [DBColumn(ColumnName = "ItemCode", DataType = DbType.String)]
        public string ItemCode { get; set; }

        /// <summary>
        /// 结果类型
        /// </summary>
        [DBColumn(ColumnName = "ResultType", DataType = DbType.String)]
        public string ResultType { get; set; }

        /// <summary>
        /// 监测时间
        /// </summary>
        [DBColumn(ColumnName = "InspectTime", DataType = DbType.DateTime)]
        public DateTime InspectTime{ get; set; }
        
        /// <summary>
        /// 最大值
        /// </summary>
        [DBColumn(ColumnName = "MaxDataValue", DataType = DbType.Decimal)]
        public decimal MaxDataValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        [DBColumn(ColumnName = "MinDataValue", DataType = DbType.Decimal)]
        public decimal MinDataValue { get; set; }

        /// <summary>
        /// 平均值
        /// </summary>
        [DBColumn(ColumnName = "AvgValue", DataType = DbType.Decimal)]
        public decimal AvgValue { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        [DBColumn(ColumnName = "OrganID", DataType = DbType.String)]
        public string OrganID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [DBColumn(ColumnName = "UpdateTime", DataType = DbType.DateTime)]
        public DateTime UpdateTime { get; set; }
    }
}
