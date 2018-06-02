﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LAF.Data.DbHelper
{
    /// <summary>
    /// Sqlserver数据访问对象
    /// </summary>
    public class SqlServerHelper:IDbHelper
    {
        #region 属性

        /// <summary>
        /// 执行超时时间
        /// </summary>
        private int _timeout=30;
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                if (this._timeout != value)
                {
                    this._timeout = value;
                }
            }
        }

        #endregion

        #region 构造函数

        public SqlServerHelper()
        {

        }

        #endregion

        #region 判断数据库连接是否有效


        /// <summary>
        /// 判断数据库连接是否有效
        /// </summary>
        /// <param name="con">数据库连接</param>
        public void isValidCon(IDbConnection con)
        {
            if (con == null)
                throw new Exception("请设置一个有效的数据库连接");
            if ((con as SqlConnection).State != ConnectionState.Open)
            {
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {
                    throw new Exception("请设置一个有效的数据库连接:" + ex.Message);
                }
            }
        }

        #endregion

        #region 查询

        /// <summary>
        /// 返回数据流
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <returns>数据流</returns>
        public IDataReader GetReader(string sql, List<DataParameter> parameters, IDbConnection con)
        {
            return GetReader(sql, CommandType.Text, parameters, con, null);
        }

        /// <summary>
        /// 返回数据流
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <returns>数据流</returns>
        public IDataReader GetReader(string sql, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts)
        {
            return GetReader(sql, CommandType.Text, parameters, con, ts);
        }

        /// <summary>
        /// 返回数据流
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>数据流</returns>
        public IDataReader GetReader(string sql, CommandType commandType, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sdr = null;
            try
            {
                cmd.Connection = con as SqlConnection;
                if (ts != null)
                    cmd.Transaction = ts as SqlTransaction;

                cmd.CommandText = sql;
                cmd.CommandTimeout = this.Timeout;
                cmd.CommandType = commandType;
                
                //设置参数
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        cmd.Parameters.Add(this.GetParameter(parameters[i]));
                    }
                }

                //执行查询
                sdr = cmd.ExecuteReader();

                return sdr;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询，将查询结果设置到DataTable中
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="dt">表</param>
        /// <param name="con">数据库连接</param>
        public void FillDataTable(DataTable dt, string sql, List<DataParameter> parameters, IDbConnection con)
        {
            FillDataTable(dt, sql, CommandType.Text, parameters, con, null);
        }


        /// <summary>
        /// 查询，将查询结果设置到DataTable中
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="dt">表</param>
        /// <param name="con">数据库连接</param>
        public void FillDataTable(DataTable dt, string sql, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts)
        {
            FillDataTable(dt, sql, CommandType.Text, parameters, con, ts);
        }

        /// <summary>
        /// 查询，将查询结果设置到DataTable中
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="sql">查询语句</param>
        /// <param name="commandType">命令执行方式</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        public void FillDataTable(DataTable dt, string sql, CommandType commandType, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts)
        {
            try
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = new SqlCommand(sql, con as SqlConnection);
                    sda.SelectCommand.CommandType = commandType;
                    sda.SelectCommand.CommandTimeout = this.Timeout;
                    if (ts != null)
                        sda.SelectCommand.Transaction = ts as SqlTransaction;

                    //设置参数
                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            sda.SelectCommand.Parameters.Add(this.GetParameter(parameters[i]));
                        }
                    }

                    //填充表
                    sda.Fill(dt);
                    sda.SelectCommand.Parameters.Clear();
                    sda.SelectCommand.Dispose();
                    sda.Dispose();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新表

        /// <summary>
        /// 更新表
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="con">数据库连接</param>
        public void Update(DataTable dt, IDbConnection con)
        {
            Update(dt, con, null);
        }

        /// <summary>
        /// 更新表，带事务
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="con">数据库连接</param>
        public void Update(DataTable dt, IDbConnection con, IDbTransaction ts)
        {
            SqlDataAdapter sda = new SqlDataAdapter();
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            try
            {
                sda.SelectCommand = new SqlCommand("SELECT * FROM " + dt.TableName, con as SqlConnection);
                sda.SelectCommand.Transaction = ts as SqlTransaction;
                scb.RefreshSchema();
                sda.Update(dt);
            }
            catch (SqlException ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 执行

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <returns>影响行数</returns>
        public int ExecuteSql(string sql, List<DataParameter> parameters, IDbConnection con)
        {
            return ExecuteSql(sql, parameters, con, null);
        }

        /// <summary>
        /// 执行sql语句，带事务
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>影响行数</returns>
        public int ExecuteSql(string sql, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts)
        {
            return ExecuteSql(sql, CommandType.Text, parameters, con, ts);
        }

        /// <summary>
        /// 执行sql语句，可以指定命令方式，带事务
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="commandType">命令方式</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>影响行数</returns>
        public int ExecuteSql(string sql, CommandType commandType, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts)
        {
            int count = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con as SqlConnection);
                cmd.CommandType = commandType;
                cmd.CommandTimeout = this.Timeout;
                if (ts != null)
                    cmd.Transaction = ts as SqlTransaction;
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        SqlParameter p = this.GetParameter(parameters[i]);
                        p.Direction = parameters[i].Direction;
                        cmd.Parameters.Add(p);
                    }
                }
                count = cmd.ExecuteNonQuery();

                return count;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行sql语句返回一个值
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <returns>值</returns>
        public Object ExecuteScalar(string sql, List<DataParameter> parameters, IDbConnection con)
        {
            return ExecuteScalar(sql, null, con, null);
        }

        /// <summary>
        /// 执行sql语句返回一个值
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>值</returns>
        public Object ExecuteScalar(string sql, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts)
        {
            return ExecuteScalar(sql, CommandType.Text, parameters, con, ts);

        }

        /// <summary>
        /// 执行sql语句返回一个值
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="commandType">命令方式</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>值</returns>
        public Object ExecuteScalar(string sql, CommandType commandType, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, con as SqlConnection, ts as SqlTransaction))
                {
                    //cmd.Connection = con as SqlConnection;
                    //if (ts != null)
                    //    cmd.Transaction = ts as SqlTransaction;
                    //cmd.CommandText = sql;
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = this.Timeout;
                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            SqlParameter p = this.GetParameter(parameters[i]);
                            p.Direction = parameters[i].Direction;
                            cmd.Parameters.Add(p);
                        }
                    }

                    object result = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 批量数据复制

        /// <summary>
        /// 批量数据复制
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="toTable">目标表</param>
        /// <param name="con">数据库连接</param>      
        public void BulkCopyData(DataTable dt, string toTable, IDbConnection con)
        {
            this.BulkCopyData(dt, toTable, con,null);
        }

        /// <summary>
        /// 批量数据复制
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="toTable">目标表</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        public void BulkCopyData(DataTable dt, string toTable, IDbConnection con, IDbTransaction ts)
        {
            try
            {
                using (SqlBulkCopy copy = new SqlBulkCopy(con as SqlConnection, SqlBulkCopyOptions.CheckConstraints, ts as SqlTransaction))
                {
                    copy.DestinationTableName = toTable;
                    copy.BulkCopyTimeout = this.Timeout;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        copy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                    }

                    copy.WriteToServer(dt);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 返回具有指定表完整架构信息，包括主键，约束等信息的DataTable

        /// <summary>
        /// 返回具有指定表完整架构信息，包括主键，约束等信息的DataTable。
        /// </summary>
        /// <param name="tableName">表名</param>        
        /// <param name="schemaType">Type of the schema.</param>
        /// <param name="con">数据库连接</param>
        /// <returns>包含完整架构信息的DataTable</returns>
        public DataTable FillShema(string tableName, SchemaType schemaType, IDbConnection con)
        {
            return this.FillShema(tableName, schemaType, con,null);
        }

        /// <summary>
        /// 返回具有指定表完整架构信息，包括主键，约束等信息的DataTable。
        /// </summary>
        /// <param name="tableName">表名</param>        
        /// <param name="schemaType">Type of the schema.</param>
        /// <param name="con">数据库连接</param>
        /// <param name="ts">事务</param>
        /// <returns>包含完整架构信息的DataTable</returns>
        public DataTable FillShema(string tableName, SchemaType schemaType, IDbConnection con, IDbTransaction ts)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = null;
            try
            {
                adapter.SelectCommand = new SqlCommand("select * from " + tableName, con as SqlConnection);

                if (ts != null)
                    adapter.SelectCommand.Transaction = ts as SqlTransaction;

                dt = new DataTable(tableName);

                adapter.FillSchema(dt, schemaType);

                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取参数

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="p">通用参数</param>
        /// <returns>sqlserver参数</returns>
        public SqlParameter GetParameter(DataParameter p)
        {
            SqlParameter parameter = new SqlParameter();

            //设置参数名
            parameter.ParameterName = p.ParameterName;
            //if (p.ParameterName.IndexOf("@") < 0)
            //    parameter.ParameterName = "@"+parameter.ParameterName;

            //设置参数值
            parameter.Value = p.Value;

            //设置参数类型
            switch (p.DataType)
            {
                case DbType.String:
                    if (p.Value != null && p.Value.ToString().Length > 2000)
                        parameter.SqlDbType = SqlDbType.Text;
                    else
                        parameter.SqlDbType = SqlDbType.VarChar;
                    break;
                case DbType.Binary:
                    parameter.SqlDbType = SqlDbType.Image;
                    break;
                case DbType.DateTime:
                    parameter.SqlDbType = SqlDbType.DateTime;
                    break;
                case DbType.Decimal:
                    parameter.SqlDbType = SqlDbType.Decimal;
                    break;
                case DbType.Int16:
                    parameter.SqlDbType = SqlDbType.Int;
                    break;
                case DbType.Int32:
                    parameter.SqlDbType = SqlDbType.Int;
                    break;
                case DbType.Int64:
                    parameter.SqlDbType = SqlDbType.Int;
                    break;
            }           

            return parameter;
        }

        #endregion

        #region 获取日期函数名称

        /// <summary>
        /// 获取日期函数名称
        /// </summary>
        /// <returns>日期函数名称</returns>
        public string GetDateFuncName()
        {
            return "getdate()";
        }

        #endregion

        #region 获取数据库可识别的对象名称

        /// <summary>
        /// 获取数据库可识别的对象名称
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public string GetDbObjectName(string objectName)
        {
            return "["+objectName+"]";
        }

        #endregion

        #region 获取参数前缀

        /// <summary>
        /// 获取参数前缀
        /// </summary>
        /// <returns>参数前缀</returns>
        public string GetParameterPrefix()
        {
            return "@";
        }

        #endregion

    }
}
