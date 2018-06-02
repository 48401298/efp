using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace LAF.Data.DbHelper
{
    /// <summary>
    /// Oledb数据访问对象
    /// </summary>
    public class OleDbHelper : IDbHelper
    {
        #region 属性

        /// <summary>
        /// 执行超时时间
        /// </summary>
        private int _timeout = 30;
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

        /// <summary>
        /// 参数前缀
        /// </summary>
        public static string ParameterPrefix { get; set; }

        #region 构造函数

        public OleDbHelper()
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
            if ((con as OleDbConnection).State != ConnectionState.Open)
            {
                try
                {
                    con.Open();
                }
                catch (OleDbException ex)
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
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader sdr = null;
            try
            {
                cmd.Connection = con as OleDbConnection;
                if (ts != null)
                    cmd.Transaction = ts as OleDbTransaction;

                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                cmd.CommandTimeout = this.Timeout;
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
            catch (OleDbException ex)
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
            OleDbDataAdapter sda = new OleDbDataAdapter();
            try
            {
                sda.SelectCommand = new OleDbCommand(sql, con as OleDbConnection);
                sda.SelectCommand.CommandType = commandType;
                sda.SelectCommand.CommandTimeout = this.Timeout;
                if (ts != null)
                    sda.SelectCommand.Transaction = ts as OleDbTransaction;

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
            }
            catch (OleDbException ex)
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
            OleDbDataAdapter sda = new OleDbDataAdapter();
            OleDbCommandBuilder scb = new OleDbCommandBuilder(sda);
            try
            {
                sda.SelectCommand = new OleDbCommand("SELECT * FROM " + dt.TableName, con as OleDbConnection);
                sda.SelectCommand.Transaction = ts as OleDbTransaction;
                scb.RefreshSchema();
                sda.Update(dt);
            }
            catch (OleDbException ex)
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
                OleDbCommand cmd = new OleDbCommand(sql, con as OleDbConnection);
                cmd.CommandType = commandType;
                cmd.CommandTimeout = this.Timeout;

                if (ts != null)
                    cmd.Transaction = ts as OleDbTransaction;
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        OleDbParameter p = this.GetParameter(parameters[i]);
                        p.Direction = parameters[i].Direction;
                        cmd.Parameters.Add(p);
                    }
                }
                count = cmd.ExecuteNonQuery();

                return count;
            }
            catch (OleDbException ex)
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
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                cmd.Connection = con as OleDbConnection;
                if (ts != null)
                    cmd.Transaction = ts as OleDbTransaction;
                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                cmd.CommandTimeout = this.Timeout;
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        OleDbParameter p = this.GetParameter(parameters[i]);
                        p.Direction = parameters[i].Direction;
                        cmd.Parameters.Add(p);
                    }
                }

                return cmd.ExecuteScalar();
            }
            catch (OleDbException ex)
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
            this.BulkCopyData(dt, toTable, con, null);
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
                throw new Exception("未实现");
            }
            catch (OleDbException ex)
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
            return this.FillShema(tableName, schemaType, con, null);
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
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataTable dt = null;
            try
            {
                adapter.SelectCommand = new OleDbCommand("select * from " + tableName, con as OleDbConnection);

                if (ts != null)
                    adapter.SelectCommand.Transaction = ts as OleDbTransaction;

                dt = new DataTable(tableName);

                adapter.FillSchema(dt, schemaType);

                return dt;
            }
            catch (OleDbException ex)
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
        public OleDbParameter GetParameter(DataParameter p)
        {
            OleDbParameter parameter = new OleDbParameter();

            //设置参数名
            parameter.ParameterName = p.ParameterName;
            if (p.ParameterName.IndexOf(this.GetParameterPrefix()) < 0)
                parameter.ParameterName = this.GetParameterPrefix() + parameter.ParameterName;

            //设置参数值
            parameter.Value = p.Value;

            //设置参数类型
            switch (p.DataType)
            {
                case DbType.String:
                    parameter.OleDbType = OleDbType.VarChar;
                    break;
                case DbType.DateTime:
                    parameter.OleDbType = OleDbType.Date;
                    break;
                case DbType.Decimal:
                    parameter.OleDbType = OleDbType.Decimal;
                    break;
                case DbType.Int16:
                    parameter.OleDbType = OleDbType.Integer;
                    break;
                case DbType.Int32:
                    parameter.OleDbType = OleDbType.Integer;
                    break;
                case DbType.Int64:
                    parameter.OleDbType = OleDbType.Integer;
                    break;
            }

            return parameter;
        }

        #endregion

        /// <summary>
        /// 获取日期函数名称
        /// </summary>
        /// <returns>日期函数名称</returns>
        public string GetDateFuncName()
        {
            return "SYSDATE";
        }

        /// <summary>
        /// 获取数据库可识别的对象名称
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public string GetDbObjectName(string objectName)
        {
            return "\"" + objectName + "\"";
        }

        #region 获取参数前缀

        /// <summary>
        /// 获取参数前缀
        /// </summary>
        /// <returns>参数前缀</returns>
        public string GetParameterPrefix()
        {
            if (string.IsNullOrEmpty(ParameterPrefix) == true)
                return ":";
            else
                return ParameterPrefix;
        }

        #endregion
    }
}
