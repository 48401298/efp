using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.WH
{
    /// </summary>
    ///　模块名称：货品类别
    ///　作    者：李炳海
    ///　编写日期：2017年07月11日
    /// </summary>
    [DBTable(TableName = "T_WH_MatType")]
    public class MatType : BaseEntity
    {
        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "ID", DataType = DbType.String, IsKey = true)]
        public string ID { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Code", DataType = DbType.String)]
        public string Code { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Description", DataType = DbType.String)]
        public string Description { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Parentcode", DataType = DbType.String)]
        public string Parentcode { get; set; }

        ///<summary>
        ///
        ///</summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

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
