using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;


namespace Manage.Entity.Video
{
    /// <summary>
    /// 设想头信息
    /// </summary>
    [DBTable(TableName = "T_VD_Camera")]
    public class VDCamera
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///摄像头编号
        ///</summary>
        [DBColumn(ColumnName = "CameraCode", DataType = DbType.String)]
        public string CameraCode { get; set; }

        ///<summary>
        ///摄像头名称
        ///</summary>
        [DBColumn(ColumnName = "CameraName", DataType = DbType.String)]
        public string CameraName { get; set; }

        ///<summary>
        ///设备类型
        ///</summary>
        [DBColumn(ColumnName = "EquKind", DataType = DbType.String)]
        public string EquKind { get; set; }

        ///<summary>
        ///所属位置
        ///</summary>
        [DBColumn(ColumnName = "PostionID", DataType = DbType.String)]
        public string PostionID { get; set; }

        ///<summary>
        ///设备IP
        ///</summary>
        [DBColumn(ColumnName = "EquIP", DataType = DbType.String)]
        public string EquIP { get; set; }

        ///<summary>
        ///设备端口
        ///</summary>
        [DBColumn(ColumnName = "EquPort", DataType = DbType.Int32)]
        public int EquPort { get; set; }

        ///<summary>
        ///用户名
        ///</summary>
        [DBColumn(ColumnName = "UserName", DataType = DbType.String)]
        public string UserName { get; set; }

        ///<summary>
        ///密码
        ///</summary>
        [DBColumn(ColumnName = "PassWord", DataType = DbType.String)]
        public string PassWord { get; set; }

        ///<summary>
        ///通道
        ///</summary>
        [DBColumn(ColumnName = "IChannelID", DataType = DbType.String)]
        public string IChannelID { get; set; }

        ///<summary>
        ///移动监控地址
        ///</summary>
        [DBColumn(ColumnName = "MobileUrl", DataType = DbType.String)]
        public string MobileUrl { get; set; }

        /// <summary>
        /// 码流类型
        /// </summary>
        [DBColumn(ColumnName = "CodeStream", DataType = DbType.String)]
        public string CodeStream { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

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

    }
}
