
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data.DbHelper
{
    /// <summary>
    /// 数据库访问对象接口
    /// </summary>
    public interface IDbHelper
    {
        /// <summary>
        /// Sql执行超时时间
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// 判断数据库连接是否有效
        /// </summary>
        /// <param name="con">数据库连接</param>
        void isValidCon(IDbConnection con);

        /// <summary>
        /// 返回数据流
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <returns>数据流</returns>
        IDataReader GetReader(string sql, List<DataParameter> parameters, IDbConnection con);

        /// <summary>
        /// 返回数据流
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <returns>数据流</returns>
        IDataReader GetReader(string sql, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 返回数据流
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>数据流</returns>
        IDataReader GetReader(string sql, CommandType commandType, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 查询，将查询结果设置到DataTable中
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="dt">表</param>
        /// <param name="con">数据库连接</param>
        void FillDataTable(DataTable dt, string sql, List<DataParameter> parameters, IDbConnection con);

        /// <summary>
        /// 查询，将查询结果设置到DataTable中
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="dt">表</param>
        /// <param name="con">数据库连接</param>
        void FillDataTable(DataTable dt, string sql, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 查询，将查询结果设置到DataTable中
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="sql">查询语句</param>
        /// <param name="commandType">命令执行方式</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        void FillDataTable(DataTable dt, string sql, CommandType commandType, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 更新表
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="con">数据库连接</param>
        void Update(DataTable dt, IDbConnection con);

        /// <summary>
        /// 更新表，带事务
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="con">数据库连接</param>
        void Update(DataTable dt, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <returns>影响行数</returns>
        int ExecuteSql(string sql, List<DataParameter> parameters, IDbConnection con);

        /// <summary>
        /// 执行sql语句，带事务
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>影响行数</returns>
        int ExecuteSql(string sql, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 执行sql语句，可以指定命令方式，带事务
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="commandType">命令方式</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>影响行数</returns>
        int ExecuteSql(string sql, CommandType commandType, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 执行sql语句返回一个值
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <returns>值</returns>
        Object ExecuteScalar(string sql, List<DataParameter> parameters, IDbConnection con);

        /// <summary>
        /// 执行sql语句返回一个值
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>值</returns>
        Object ExecuteScalar(string sql, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 执行sql语句返回一个值
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="commandType">命令方式</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>值</returns>
        Object ExecuteScalar(string sql, CommandType commandType, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 批量数据复制
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="toTable">目标表</param>
        /// <param name="con">数据库连接</param>      
        void BulkCopyData(DataTable dt, string toTable, IDbConnection con);

        /// <summary>
        /// 批量数据复制
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="toTable">目标表</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        void BulkCopyData(DataTable dt, string toTable, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 返回具有指定表完整架构信息，包括主键，约束等信息的DataTable。
        /// </summary>
        /// <param name="tableName">表名</param>        
        /// <param name="schemaType">Type of the schema.</param>
        /// <param name="con">数据库连接</param>
        /// <returns>包含完整架构信息的DataTable</returns>
        DataTable FillShema(string tableName, SchemaType schemaType, IDbConnection con);
        
        
        /// <summary>
        /// 返回具有指定表完整架构信息，包括主键，约束等信息的DataTable。
        /// </summary>
        /// <param name="tableName">表名</param>        
        /// <param name="schemaType">Type of the schema.</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>包含完整架构信息的DataTable</returns>
        DataTable FillShema(string tableName, SchemaType schemaType, IDbConnection con, IDbTransaction ts);

        /// <summary>
        /// 获取日期函数名称
        /// </summary>
        /// <returns>日期函数名称</returns>
        string GetDateFuncName();

        /// <summary>
        /// 获取数据库可识别的对象名称
        /// </summary>
        /// <param name="objectName">对象名</param>
        /// <returns>对象名称</returns>
        string GetDbObjectName(string objectName);

        /// <summary>
        /// 获取参数前缀
        /// </summary>
        /// <returns>参数前缀</returns>
        string GetParameterPrefix();

        
    }
}
