using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.WH
{
    /// <summary>
    /// 收货单位
    /// </summary>
    [DBTable(TableName = "T_WH_Client")]
    public class WHClient
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        [DBColumn(ColumnName = "ClientName", DataType = DbType.String)]
        public string ClientName { get; set; }

        /// <summary>
        /// 所在地区
        /// </summary>
        [DBColumn(ColumnName = "AreaCode", DataType = DbType.String)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [DBColumn(ColumnName = "Address", DataType = DbType.String)]
        public string Address { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [DBColumn(ColumnName = "Postalcode", DataType = DbType.String)]
        public string Postalcode { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DBColumn(ColumnName = "Linkman", DataType = DbType.String)]
        public string Linkman { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DBColumn(ColumnName = "Telephone", DataType = DbType.String)]
        public string Telephone { get; set; }

        ///<summary>
        ///手机
        ///</summary>
        [DBColumn(ColumnName = "Mobilephone", DataType = DbType.String)]
        public string Mobilephone { get; set; }

        ///<summary>
        ///传真
        ///</summary>
        [DBColumn(ColumnName = "Fax", DataType = DbType.String)]
        public string Fax { get; set; }

        ///<summary>
        ///电子邮箱
        ///</summary>
        [DBColumn(ColumnName = "Email", DataType = DbType.String)]
        public string Email { get; set; }

        ///<summary>
        ///网址
        ///</summary>
        [DBColumn(ColumnName = "WwwAddress", DataType = DbType.String)]
        public string WwwAddress { get; set; }

        ///<summary>
        ///开户行
        ///</summary>
        [DBColumn(ColumnName = "Bank", DataType = DbType.String)]
        public string Bank { get; set; }

        ///<summary>
        ///开户行帐号
        ///</summary>
        [DBColumn(ColumnName = "BankAmount", DataType = DbType.String)]
        public string BankAmount { get; set; }

        ///<summary>
        ///纳税号
        ///</summary>
        [DBColumn(ColumnName = "TaxNumber", DataType = DbType.String)]
        public string TaxNumber { get; set; }

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
