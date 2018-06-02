
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Data.Common;


namespace LAF.Data.DbHelper
{
    /// <summary>
    /// Oracle数据访问对象
    /// </summary>
    public class OracleHelper : IDbHelper
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

        #region 构造函数

        public OracleHelper()
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
            if ((con as OracleConnection).State != System.Data.ConnectionState.Open)
            {
                try
                {
                    con.Open();
                }
                catch (OracleException ex)
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
        public IDataReader GetReader(string sql, System.Data.CommandType commandType, List<DataParameter> parameters, IDbConnection con, IDbTransaction ts)
        {
            OracleCommand cmd = new OracleCommand();
            OracleDataReader sdr = null;
            try
            {
                cmd.Connection = con as OracleConnection;
                if (ts != null)
                    cmd.Transaction = ts as OracleTransaction;

                cmd.CommandText = sql;
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
            catch (OracleException ex)
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
            OracleDataAdapter sda = new OracleDataAdapter();
            try
            {
                sda.SelectCommand = new OracleCommand(sql, con as OracleConnection);
                sda.SelectCommand.CommandType = commandType;
                if (ts != null)
                    sda.SelectCommand.Transaction = ts as OracleTransaction;

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
            catch (OracleException ex)
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
            OracleDataAdapter sda = new OracleDataAdapter();
            OracleCommandBuilder scb = new OracleCommandBuilder(sda);
            try
            {
                sda.SelectCommand = new OracleCommand("SELECT * FROM " + dt.TableName, con as OracleConnection);
                sda.SelectCommand.Transaction = ts as OracleTransaction;
                scb.RefreshSchema();
                sda.Update(dt);
            }
            catch (OracleException ex)
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
                OracleCommand cmd = new OracleCommand(sql, con as OracleConnection);
                cmd.CommandType = commandType;

                if (ts != null)
                    cmd.Transaction = ts as OracleTransaction;
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        OracleParameter p = this.GetParameter(parameters[i]);
                        p.Direction = parameters[i].Direction;
                        cmd.Parameters.Add(p);
                    }
                }
                count = cmd.ExecuteNonQuery();

                return count;
            }
            catch (OracleException ex)
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
            OracleCommand cmd = new OracleCommand();
            try
            {
                cmd.Connection = con as OracleConnection;
                if (ts != null)
                    cmd.Transaction = ts as OracleTransaction;
                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        OracleParameter p = this.GetParameter(parameters[i]);
                        p.Direction = parameters[i].Direction;
                        cmd.Parameters.Add(p);
                    }
                }

                return cmd.ExecuteScalar();
            }
            catch (OracleException ex)
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
            throw new Exception("未定义该方法！");
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
            throw new Exception("未定义该方法！");
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
            OracleDataAdapter adapter = new OracleDataAdapter();
            DataTable dt = null;
            try
            {
                adapter.SelectCommand = new OracleCommand("select * from " + tableName, con as OracleConnection);

                if (ts != null)
                    adapter.SelectCommand.Transaction = ts as OracleTransaction;

                dt = new DataTable(tableName);

                adapter.FillSchema(dt, schemaType);

                return dt;
            }
            catch (OracleException ex)
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
        public OracleParameter GetParameter(DataParameter p)
        {
            OracleParameter parameter = new OracleParameter();

            //设置参数名
            parameter.ParameterName = p.ParameterName;
            //if (p.ParameterName.IndexOf(":") < 0)
            //    parameter.ParameterName = ":"+parameter.ParameterName;

            //设置参数值
            parameter.Value = p.Value;
         
            //设置参数类型
            switch (p.DataType)
            {
                case DbType.String:
                    if (p.Value != null && p.Value.ToString().Length > 2000)
                    {
                        parameter.OracleType = OracleType.Clob;
                    }
                    else
                    {
                        parameter.OracleType = OracleType.NVarChar;
                    }
                    break;
                case DbType.Binary:
                    parameter.OracleType = OracleType.Blob;
                    break;
                case DbType.DateTime:
                    parameter.OracleType = OracleType.DateTime;
                    break;
                case DbType.Decimal:
                    parameter.OracleType = OracleType.Double;
                    break;
                case DbType.Int16:
                    parameter.OracleType = OracleType.Int16;
                    break;
                case DbType.Int32:
                    parameter.OracleType = OracleType.Int32;
                    break;
                case DbType.Int64:
                    parameter.OracleType = OracleType.Double;
                    break;
                case DbType.Double:
                    parameter.OracleType = OracleType.Double;
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
            return "SYSDATE";
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
            return "\"" + objectName + "\"";
        }

        #endregion

        #region 获取参数前缀

        /// <summary>
        /// 获取参数前缀
        /// </summary>
        /// <returns>参数前缀</returns>
        public string GetParameterPrefix()
        {
            return ":";
        }

        #endregion

    }

}