using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LAF.Data
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    [Description("数据库类型")]
    public enum DataBaseType
    {
        /// <summary>
        /// Oracle数据库(微软驱动)
        /// </summary>
        [Description("Oracle数据库(微软驱动)")]
        Oracle,
        /// <summary>
        /// Oracle数据库(微软驱动)
        /// </summary>
        [Description("Oracle数据库(甲骨文驱动)")]
        OracleOdp,
        /// <summary>
        /// SqlServer数据库
        /// </summary>
        [Description("SqlServer数据库")]
        SqlServer,
        /// <summary>
        /// OleDb数据库
        /// </summary>
        [Description("OleDb数据库")]
        OleDb,
        /// <summary>
        /// MySql数据库
        /// </summary>
        [Description("MySql数据库")]
        MySql,
        /// <summary>
        /// SqlLite数据库
        /// </summary>
        [Description("SqlLite数据库")]
        SqlLite
    }

    /// <summary>
    /// 数据操作类型
    /// </summary>
    public enum DataOprType
    {
        [Description("插入")]
        Insert,
        [Description("删除")]
        Delete,
        [Description("修改")]
        Update,
        [Description("查询")]
        Query
    }

    /// <summary>
    /// 数据实体框架Sql语句日志模式
    /// </summary>
    [Description("数据实体框架Sql语句日志模式")]
    public enum SqlRecordMode
    {
        [Description("无记录")]
        None,
        [Description("全部Sql语句")]
        AllSql,
        [Description("Insert\\Update\\Delete语句")]
        InsertUpdateDelete
    }
}
