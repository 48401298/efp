using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;
using System.Data;

namespace Manage.Entity.Video
{
    /// <summary>
    /// 视频监控位置
    /// </summary>
    [DBTable(TableName = "T_VD_Position")]
    public class VDPosition
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///位置编号
        ///</summary>
        [DBColumn(ColumnName = "PositionCode", DataType = DbType.String)]
        public string PositionCode { get; set; }

        ///<summary>
        ///位置名称
        ///</summary>
        [DBColumn(ColumnName = "PositionName", DataType = DbType.String)]
        public string PositionName { get; set; }

        ///<summary>
        ///经度
        ///</summary>
        [DBColumn(ColumnName = "LO", DataType = DbType.Decimal)]
        public decimal LO { get; set; }

        ///<summary>
        ///纬度
        ///</summary>
        [DBColumn(ColumnName = "LA", DataType = DbType.Decimal)]
        public decimal LA { get; set; }

        ///<summary>
        ///上级主键
        ///</summary>
        [DBColumn(ColumnName = "ParentID", DataType = DbType.String)]
        public string ParentID { get; set; }

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

        /// <summary>
        /// 摄像头列表
        /// </summary>
        public List<VDCamera> CameraList { get; set; }

    }
}
