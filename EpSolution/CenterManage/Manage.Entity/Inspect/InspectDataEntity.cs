﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.Inspect
{
    /// <summary>
    /// 监测数据
    /// </summary>
    [Serializable]
    [DBTable(TableName = "inspectdatainfo")]
    public class InspectDataEntity : BaseEntity 
    {
        /// <summary>
        /// ID
        /// </summary>
        [DBColumn(ColumnName = "Id", DataType = DbType.String, IsKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [DBColumn(ColumnName = "DeviceCode", DataType = DbType.String)]
        public string DeviceCode { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        [DBColumn(ColumnName = "ItemCode", DataType = DbType.String)]
        public string ItemCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 监测时间
        /// </summary>
        [DBColumn(ColumnName = "InspectTime", DataType = DbType.DateTime)]
        public DateTime InspectTime { get; set; }

        /// <summary>
        /// 监测数据
        /// </summary>
        [DBColumn(ColumnName = "InspectData", DataType = DbType.String)]
        public string InspectData { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        [DBColumn(ColumnName = "OrganID", DataType = DbType.String)]
        public string OrganID { get; set; }

        /// <summary>
        /// 机构描述
        /// </summary>
        public string OrganDESC { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [DBColumn(ColumnName = "UpdateTime", DataType = DbType.DateTime)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }
    }
}
