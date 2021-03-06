﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Entity;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：生产线
    ///　作    者：wanglu
    ///　编写日期：2017年07月25日
    /// </summary>
    [DBTable(TableName = "T_FP_PRODUCTLINE")]
    public class ProductLine : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///生产线编号
        ///</summary>
        [DBColumn(ColumnName = "PLCODE", DataType = DbType.String)]
        public string PLCODE { get; set; }

        ///<summary>
        ///生产线名称
        ///</summary>
        [DBColumn(ColumnName = "PLNAME", DataType = DbType.String)]
        public String PLNAME { get; set; }

        ///<summary>
        ///所属工厂主键
        ///</summary>
        [DBColumn(ColumnName = "FACTORYPID", DataType = DbType.String)]
        public string FACTORYPID { get; set; }

        ///<summary>
        ///负责人
        ///</summary>
        [DBColumn(ColumnName = "PERSONINCHARGE", DataType = DbType.String)]
        public string PERSONINCHARGE { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "REMARK", DataType = DbType.String)]
        public string REMARK { get; set; }

        ///<summary>
        ///是否停用
        ///</summary>
        [DBColumn(ColumnName = "FLGACTIVE", DataType = DbType.String)]
        public string FLGACTIVE { get; set; }

        ///<summary>
        ///删除标识
        ///</summary>
        [DBColumn(ColumnName = "FLGDEL", DataType = DbType.String)]
        public string FLGDEL { get; set; }

        ///<summary>
        ///创建人
        ///</summary>
        [DBColumn(ColumnName = "CREATEUSER", DataType = DbType.String)]
        public string CREATEUSER { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [DBColumn(ColumnName = "CREATETIME", DataType = DbType.DateTime)]
        public DateTime CREATETIME { get; set; }

        ///<summary>
        ///更新人
        ///</summary>
        [DBColumn(ColumnName = "UPDATEUSER", DataType = DbType.String)]
        public string UPDATEUSER { get; set; }

        ///<summary>
        ///更新时间
        ///</summary>
        [DBColumn(ColumnName = "UPDATETIME", DataType = DbType.DateTime)]
        public DateTime UPDATETIME { get; set; }

        ///<summary>
        ///分厂名称
        ///</summary>
        public string FNAME { get; set; }
        
    }
}
