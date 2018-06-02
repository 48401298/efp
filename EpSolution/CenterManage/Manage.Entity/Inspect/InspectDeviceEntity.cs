using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.Inspect
{
    /// <summary>
    /// 监测项目
    /// </summary>
    [Serializable]
    [DBTable(TableName = "deviceinfo")]
    public class InspectDeviceEntity : BaseEntity 
    {
        /// <summary>
        /// 设备ID
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
        [DBColumn(ColumnName = "DeviceName", DataType = DbType.String)]
        public string DeviceName { get; set; }

        /// <summary>
        /// 设备IP
        /// </summary>
        [DBColumn(ColumnName = "DeviceIP", DataType = DbType.String)]
        public string DeviceIP { get; set; }

        /// <summary>
        /// 设备端口
        /// </summary>
        [DBColumn(ColumnName = "DevicePort", DataType = DbType.String)]
        public int DevicePort { get; set; }

        /// <summary>
        /// 网络IP
        /// </summary>
        [DBColumn(ColumnName = "LanIP", DataType = DbType.String)]
        public string LanIP { get; set; }

        /// <summary>
        /// 网络端口
        /// </summary>
        [DBColumn(ColumnName = "LanPort", DataType = DbType.String)]
        public int LanPort { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        [DBColumn(ColumnName = "LastLoginTime", DataType = DbType.DateTime)]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 最后注册时间
        /// </summary>
        [DBColumn(ColumnName = "LastRegisterTime", DataType = DbType.DateTime)]
        public DateTime LastRegisterTime { get; set; }

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
        /// 机构ID
        /// </summary>
        [DBColumn(ColumnName = "OrganID", DataType = DbType.String)]
        public string OrganID { get; set; }

        /// <summary>
        /// 机构描述
        /// </summary>
        public string OrganDESC { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [DBColumn(ColumnName = "DeviceType", DataType = DbType.String)]
        public string DeviceType { get; set; }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DeviceTypeName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UpdateUser { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 在线状态
        /// </summary>
        public string onlineStatus { get; set; }
    }
}
