using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;

namespace EnvMII.VO
{
    /// <summary>
    /// 监测设备
    /// </summary>
    [DBTable(TableName = "deviceinfo")]
    public class DeviceInfo
    {
        
        /// <summary>
        /// 设备识主键
        /// </summary>
        [DBColumn(ColumnName = "Id", DataType = DbType.String, IsKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 设备识别码
        /// </summary>
        [DBColumn(ColumnName = "DeviceCode", DataType = DbType.String)]
        public string DeviceSN { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [DBColumn(ColumnName = "DeviceName", DataType = DbType.String)]
        public string DeviceName { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [DBColumn(ColumnName = "DeviceType", DataType = DbType.String)]
        public string DeviceType { get; set; }

        /// <summary>
        /// 设备IP
        /// </summary>
        [DBColumn(ColumnName = "DeviceIP", DataType = DbType.String)]
        public string DeviceIP { get; set; }

        /// <summary>
        /// 设备端口
        /// </summary>
        [DBColumn(ColumnName = "DevicePort", DataType = DbType.String)]
        public string DevicePort { get; set; }

        /// <summary>
        /// LANIP
        /// </summary>
        [DBColumn(ColumnName = "LanIP", DataType = DbType.String)]
        public string LanIP { get; set; }

        /// <summary>
        /// LAN端口
        /// </summary>
        [DBColumn(ColumnName = "LanPort", DataType = DbType.String)]
        public string LanPort { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [DBColumn(ColumnName = "LastRegisterTime", DataType = DbType.DateTime)]
        public DateTime LastRegisterTime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [DBColumn(ColumnName = "LastLoginTime", DataType = DbType.DateTime)]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [DBColumn(ColumnName = "Lon", DataType = DbType.Decimal)]
        public decimal Lon { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [DBColumn(ColumnName = "Lat", DataType = DbType.Decimal)]
        public decimal Lat { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        [DBColumn(ColumnName = "OrganID", DataType = DbType.String)]
        public string OrganID { get; set; }

    }
}
