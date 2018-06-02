using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data.Attributes;

namespace ExchangeEntity
{
    /// <summary>
    /// 处理结题
    /// </summary>
    [Serializable]
    [DBTable(TableName = "exchangeresult")]
    public class HandleResult
    {
        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColumnName = "Id", DataType = DbType.String, IsKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        [DBColumn(ColumnName = "OrganID", DataType = DbType.String)]
        public string OrganId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DBColumn(ColumnName = "HandleTime", DataType = DbType.DateTime)]
        public DateTime HandleTime { get; set; }
   
        /// <summary>
        /// 本次处理的开始时间
        /// </summary>
        [DBColumn(ColumnName = "HandleStartTime", DataType = DbType.DateTime)]
        public DateTime HandleStartTime { get; set; }

        /// <summary>
        /// 本次处理的结束时间
        /// </summary>
        [DBColumn(ColumnName = "HandleEndTime", DataType = DbType.Date)]
        public DateTime HandleEndTime { get; set; }

        /// <summary>
        /// 处理记录数量(采集数据)
        /// </summary>
        [DBColumn(ColumnName = "HandleCountMdb", DataType = DbType.Int32)]
        public int HandleCountMdb { get; set; }

        /// <summary>
        /// 处理记录数量(计算结果)
        /// </summary>
        [DBColumn(ColumnName = "HandleCountMs", DataType = DbType.Int32)]
        public int HandleCountMs { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [DBColumn(ColumnName = "DeviceSN", DataType = DbType.String)]
        public string DeviceSN { get; set; }
    }
}
