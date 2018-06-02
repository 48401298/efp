using System;
using System.Data;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：设备管理
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    [DBTable(TableName = "T_FP_EQUIPMENT")]
    public class EquipmentInfo : BaseEntity
    {
        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ECODE", DataType = DbType.String)]
        public string ECODE { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ENAME", DataType = DbType.String)]
        public String ENAME { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "EBRAND", DataType = DbType.String)]
        public string EBRAND { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ETYPE", DataType = DbType.String)]
        public string ETYPE { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "MDATE", DataType = DbType.DateTime)]
        public DateTime MDATE { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "SUPPLIERADDR", DataType = DbType.String)]
        public string SUPPLIERADDR { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "SUPPLIERCONTACT", DataType = DbType.String)]
        public string SUPPLIERCONTACT { get; set; }

        ///<summary>
        ///所属工厂主键
        ///</summary>
        [DBColumn(ColumnName = "FACTORYPID", DataType = DbType.String)]
        public string FACTORYPID { get; set; }

        ///<summary>
        ///所属生产线主键
        ///</summary>
        [DBColumn(ColumnName = "PRODUCTLINEPID", DataType = DbType.String)]
        public string PRODUCTLINEPID { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "BARCODE", DataType = DbType.String)]
        public string BARCODE { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "QRCODE", DataType = DbType.String)]
        public string QRCODE { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "RFIDCODE", DataType = DbType.String)]
        public string RFIDCODE { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "REMARK", DataType = DbType.String)]
        public string REMARK { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "FLGACTIVE", DataType = DbType.String)]
        public string FLGACTIVE { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "FLGDEL", DataType = DbType.String)]
        public string FLGDEL { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CREATEUSER { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CREATETIME { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UPDATEUSER { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UPDATETIME { get; set; }


    }
}
