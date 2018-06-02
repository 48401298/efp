using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using LAF.Data.Attributes;
using System.Data;

namespace ExchangeEntity
{
    [BsonIgnoreExtraElements]
    [DBTable(TableName = "inspectrealtimedata")]
    public class InspectRealTimeItemData
    {
        /// <summary>
        /// ID
        /// </summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// 设备识别码
        /// </summary>
        [DBColumn(ColumnName = "DeviceCode", DataType = DbType.String)]
        public string DeviceSN { get; set; }

        /// <summary>
        /// 监测项目编号
        /// </summary>
        [DBColumn(ColumnName = "ItemCode", DataType = DbType.String)]
        public string ItemCode { get; set; }

        /// <summary>
        /// 监测时间
        /// </summary>
        [DBColumn(ColumnName = "InspectTime", DataType = DbType.DateTime)]
        public DateTime InspectTime { get; set; }

        /// <summary>
        /// 监测值
        /// </summary>
        [DBColumn(ColumnName = "InspectData", DataType = DbType.String)]
        public string InspectData { get; set; }

        /// <summary>
        /// 机构
        /// </summary>
        [DBColumn(ColumnName = "OrganID", DataType = DbType.String)]
        public string OrganID { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        [DBColumn(ColumnName = "UpdateTime", DataType = DbType.Date)]
        public DateTime UpdateTime { get; set; }

    }
}
