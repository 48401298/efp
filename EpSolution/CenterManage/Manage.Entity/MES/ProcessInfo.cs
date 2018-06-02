using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;

namespace Manage.Entity.MES
{
    /// </summary>
    ///　模块名称：工序信息
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    [DBTable(TableName = "T_FP_PROCESSINFO")]
    public class ProcessInfo
    {
        ///<summary>
        ///主键
        ///</summary>
        [DBColumn(ColumnName = "PID", DataType = DbType.String, IsKey = true)]
        public string PID { get; set; }

        ///<summary>
        ///工序编号
        ///</summary>
        [DBColumn(ColumnName = "PCODE", DataType = DbType.String)]
        public string PCODE { get; set; }

        ///<summary>
        ///工序名称
        ///</summary>
        [DBColumn(ColumnName = "PNAME", DataType = DbType.String)]
        public String PNAME { get; set; }

        /// <summary>
        /// 所属工艺流程
        /// </summary>
        [DBColumn(ColumnName = "FLOWID", DataType = DbType.String)]
        public string FLOWID { get; set; }

        ///<summary>
        ///加工时间
        ///</summary>
        [DBColumn(ColumnName = "SEQ", DataType = DbType.Decimal)]
        public string SEQ { get; set; }

        ///<summary>
        ///加工时间
        ///</summary>
        [DBColumn(ColumnName = "PTIME", DataType = DbType.Decimal)]
        public string PTIME { get; set; }

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
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "REMARK", DataType = DbType.String)]
        public string REMARK { get; set; }

        /// <summary>
        /// 入库单明细
        /// </summary>
        public List<EquipmentRef> Details { get; set; }

        /// <summary>
        /// 入库单明细
        /// </summary>
        public List<StationRef> Details2 { get; set; }

        /// <summary>
        /// 工位ID
        /// </summary>
        public string GWID { get; set; }
    }
}
