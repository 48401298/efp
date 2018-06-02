using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using LAF.Data.Attributes;
using LAF.Common.Util;
using LAF.Data.DbHelper;


namespace LAF.Data
{
    /// <summary>
    /// 数据映射会话
    /// </summary>
    public class DataSession : IDataSession
    {
        #region 成员变量

        //是否被释放       
        private bool _alreadyDisposed = false;

        /// <summary>
        /// sql日志
        /// </summary>
        private List<DataLogInfo> DataLogs = new List<DataLogInfo>();

        #endregion

        #region 属性

        /// <summary>
        /// 数据库连接信息
        /// </summary>
        public ConnectionInfo ConInfo { get; set; }

        /// <summary>
        /// 数据库事务
        /// </summary>
        public IDbTransaction Transaction { get; set; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection Connection { get; set; }

        /// <summary>
        /// Sql工具
        /// </summary>
        public IDbHelper DbHelper { get; set; }

        /// <summary>
        /// 输出日志模式
        /// </summary>
        public SqlRecordMode LogMode { get; set; }

        #endregion

        /// <summary>
        /// 输出日志委托
        /// </summary>
        /// <param name="sender">所属容器</param>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        public delegate void ExportLogEventHandler(object sender, ExportLogEventArg arg);

        /// <summary>
        /// 输出日志事件
        /// </summary>
        public event ExportLogEventHandler ExportLogEvent = null;


        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataSession()
        {
            this.ConInfo = DbManager.MainConnectionInfo;
            this.Init(this.ConInfo);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbKey">数据库名</param>
        public DataSession(string dbKey)
        {
            this.ConInfo = DbManager.GetConInfo(dbKey);
            this.Init(this.ConInfo);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conInfo">数据库连接信息</param>
        public DataSession(ConnectionInfo conInfo)
        {
            this.ConInfo = conInfo;
            this.Init(this.ConInfo);
        }

        private void Init(ConnectionInfo conInfo)
        {
            //获取数据库连接
            this.Connection = DbManager.GetCon(conInfo.DbKey, true);

            //设置sql工具
            switch (this.ConInfo.DbType)
            {
                case DataBaseType.SqlServer:
                    this.DbHelper = new SqlServerHelper();
                    break;
                case DataBaseType.Oracle:
                    this.DbHelper = new OracleHelper();
                    break;
                case DataBaseType.OracleOdp:
                    this.DbHelper = new OracleOdpHelper();
                    break;
                case DataBaseType.OleDb:
                    this.DbHelper = new OleDbHelper();
                    break;
                case DataBaseType.MySql:
                    this.DbHelper = new MySqlHelper();
                    break;
                case DataBaseType.SqlLite:
                    break;
            }

            //设置超时时间
            this.DbHelper.Timeout = DbManager.SqlTimeout;
        }


        #endregion

        #region IDisposable

        public void Dispose()
        {

            Dispose(true);

        }

        protected virtual void Dispose(bool isDisposing)
        {

            // Don't dispose more than one

            if (_alreadyDisposed)
                return;

            if (isDisposing)
            {
                if (this.Transaction != null)
                {
                    //回滚事务 
                    this.Transaction.Rollback();
                }
                //关闭连接
                this.CloseCon();
            }

            // TODO: free unmanaged resources here

            // Set disposed flag

            _alreadyDisposed = true;

        }

        #endregion

        #region 连接管理

        /// <summary>
        /// 打开连接
        /// </summary>
        public virtual void OpenCon()
        {
            try
            {
                if (this.Connection != null && this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public virtual void CloseCon()
        {
            try
            {
                if (this.Connection != null && this.Connection.State != ConnectionState.Closed && this.Transaction == null)
                {
                    this.Connection.Close();
                }
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 打开事务

        public virtual void OpenTs()
        {
            try
            {
                this.OpenCon();
                this.Transaction = this.Connection.BeginTransaction();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 事务管理

        /// <summary>
        /// 提交事务
        /// </summary>
        public virtual void CommitTs()
        {
            try
            {
                if (this.Transaction != null)
                {
                    this.Transaction.Commit();
                    this.Transaction = null;
                }
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public virtual void RollbackTs()
        {
            try
            {
                if (this.Transaction != null)
                {
                    this.Transaction.Rollback();
                    this.Transaction = null;
                }
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 插入实体

        /// <summary>
        /// 插入单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>实体主键为自增时，返回主键值；否则返回插入行数。</returns>
        public virtual int Insert<T>(T model) where T : new()
        {
            Type type = null;

            string sql = "";
            string tableName = "";
            List<string> columns = new List<string>();
            List<string> values = new List<string>();
            List<DataParameter> parameters = new List<DataParameter>();
            string timeStampDbColumn = "";
            bool isPkIdentity = false;
            int r;
            try
            {
                type = typeof(T);

                //获取表信息
                object[] attrsClassAtt = type.GetCustomAttributes(typeof(DBTableAttribute), true);

                if (attrsClassAtt == null)
                {
                    throw new Exception("当前实体没有添加属性DBTableAttribute！");
                }
                DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                tableName = this.DbHelper.GetDbObjectName(tableAtt.TableName);

                PropertyInfo[] pArray = type.GetProperties();

                if (string.IsNullOrEmpty(tableAtt.TimeStampColumn) == false)
                {
                    //验证时间戳有效性
                    int timeColumnIndex = pArray.ToList().FindIndex(p => p.Name == tableAtt.TimeStampColumn);

                    if (timeColumnIndex == -1)
                        throw new Exception("时间戳属性指定不正确！");
                }

                //获取字段信息
                foreach (var item in pArray)
                {
                    object[] attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (attrs.Count() == 0)
                    {
                        continue;
                    }

                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];//字段属性

                    //判断是否为自增
                    if (ca.IsIdentity == true)
                    {
                        if (ca.IsKey == true)
                            isPkIdentity = true;

                        continue;
                    }

                    string columnName = ca.ColumnName;//字段名
                    object value = item.GetValue(model, null);//获取值

                    //提取时间戳字段名
                    if (item.Name == tableAtt.TimeStampColumn)
                        timeStampDbColumn = columnName;

                    if (value == null)
                        continue;

                    if (ca.DataType == DbType.DateTime && value != null && (DateTime)value == new DateTime())
                    {
                        value = System.DBNull.Value;
                    }

                    columns.Add(this.DbHelper.GetDbObjectName(columnName));//添加字段

                    //处理时间戳
                    if (string.IsNullOrEmpty(tableAtt.TimeStampColumn) == false && tableAtt.TimeStampColumn == item.Name)
                    {
                        values.Add(this.DbHelper.GetDateFuncName());

                        continue;
                    }

                    if (ca.DefaultValue == DataDefaultValue.None)
                    {
                        //添加值
                        values.Add(this.DbHelper.GetParameterPrefix() + columnName);
                        //添加参数
                        parameters.Add(new DataParameter { ParameterName = columnName, Value = value, DataType = ca.DataType });
                    }
                    else
                    {
                        //添加默认值
                        values.Add(this.GetDefaultValue(ca.DefaultValue));
                    }

                }

                this.OpenCon();

                //执行语句
                sql = string.Format("INSERT INTO {0} ({1}) VALUES  ({2}){3}"
                                        , tableName
                                        , string.Join(",", columns.ToArray())
                                        , string.Join(",", values.ToArray())
                                        , isPkIdentity ? ";SELECT @@IDENTITY;" : "");

                //执行语句
                if (isPkIdentity == true)
                    r = int.Parse(this.DbHelper.ExecuteScalar(sql, parameters, this.Connection, this.Transaction).ToString());
                else
                    r = this.DbHelper.ExecuteSql(sql, parameters, this.Connection, this.Transaction);                

                return r;

            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();

                //记录日志
                this.RecordLog(sql, parameters);
            }
        }

        /// <summary>
        /// 插入实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体列表</param>
        /// <returns>插入行数</returns>
        public virtual int Insert<T>(List<T> list) where T : new()
        {
            int count = 0;

            try
            {
                foreach (T info in list)
                {
                    this.Insert<T>(info);
                }

                count = list.Count;

                return count;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();
            }
        }

        #endregion

        #region 删除实体

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>删除行数</returns>
        public virtual int Delete<T>(T model) where T : new()
        {
            int count = 0;
            Type type = null;
            string sql = "";
            string tableName = "";
            string whereSql = "";
            List<DataParameter> parameters = null;
            try
            {
                type = typeof(T);

                //获取表信息
                object[] attrsClassAtt = type.GetCustomAttributes(typeof(DBTableAttribute), true);

                if (attrsClassAtt == null)
                {
                    throw new Exception("当前实体没有添加属性DBTableAttribute！");
                }
                DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                tableName = this.DbHelper.GetDbObjectName(tableAtt.TableName);

                //获取删除条件
                this.GetSingleWhere<T>(model, out whereSql, out parameters);

                if (string.IsNullOrEmpty(whereSql) == true)
                    throw new Exception("删除条件提取失败，不能执行删除操作！");

                //构成删除语句
                sql = string.Format("delete from {0} where {1}", tableName, whereSql);

                this.OpenCon();

                //执行语句
                count = this.DbHelper.ExecuteSql(sql, parameters, this.Connection, this.Transaction);                

                return count;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();

                //记录日志
                this.RecordLog(sql, parameters);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>删除行数</returns>
        public virtual int Delete<T>(List<T> list) where T : new()
        {
            int count = 0;

            try
            {
                foreach (T info in list)
                {
                    this.Delete<T>(info);
                }

                count = list.Count;
                return count;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();
            }
        }

        #endregion

        #region 更新实体

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>更新行数</returns>
        public virtual int Update<T>(T model) where T : new()
        {
            return Update<T>(model,null);
        }

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <param name="updateItems">需要更新的字段</param>
        /// <returns>更新行数</returns>
        public virtual int Update<T>(T model,List<string> updateItems) where T : new()
        {
            int count = 0;
            Type type = null;
            string sql = "";
            string tableName = "";
            List<string> updateColumns = new List<string>();
            string whereSql = "";
            List<DataParameter> parameters = null;
            string timeStampDbColumn = "";
            try
            {
                type = typeof(T);

                //获取表信息
                object[] attrsClassAtt = type.GetCustomAttributes(typeof(DBTableAttribute), true);

                if (attrsClassAtt == null)
                {
                    throw new Exception("当前实体没有添加属性DBTableAttribute！");
                }
                DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                tableName = this.DbHelper.GetDbObjectName(tableAtt.TableName);

                //获取更新条件
                this.GetSingleWhere<T>(model, out whereSql, out parameters);

                this.OpenCon();

                #region 处理时间戳

                if (string.IsNullOrEmpty(tableAtt.TimeStampColumn) == false)
                {
                    PropertyInfo timeProperty = model.GetType().GetProperty(tableAtt.TimeStampColumn);

                    if (timeProperty == null)
                        throw new Exception("时间戳属性指定不正确！");

                    object[] attrs = timeProperty.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (attrs.Count() == 0)
                    {
                        throw new Exception("时间戳属性没有配置字段信息！");
                    }

                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];//字段属性

                    //帮段时间戳是否过期
                    DateTime oldTimeStamp;
                    DateTime currentTimeStamp;

                    timeStampDbColumn = ca.ColumnName;

                    //获取更新实体中的时间戳
                    oldTimeStamp = (DateTime)BindHelper.GetPropertyValue(model, tableAtt.TimeStampColumn);

                    //获取数据库中时间戳
                    sql = string.Format("select {0} from {1} where {2}", timeStampDbColumn, tableAtt.TableName, whereSql);

                    currentTimeStamp = (DateTime)this.DbHelper.ExecuteScalar(sql, parameters, this.Connection, this.Transaction);

                    if (oldTimeStamp.ToString() != currentTimeStamp.ToString())
                        throw new Exception("时间戳过期！");

                }

                #endregion

                //获取字段属性
                foreach (var item in type.GetProperties())
                {
                    object[] attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (attrs.Count() == 0)
                    {
                        continue;
                    }

                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];//字段属性

                    //判断主键字段或者自增字段无法更新
                    if (ca.IsKey == true || ca.IsIdentity == true)
                    {
                        continue;
                    }

                    //添加更新字段
                    if (ca.ColumnName != timeStampDbColumn)
                    {
                        object value = item.GetValue(model, null);//获取值

                        if (value == null)
                        {
                            value = System.DBNull.Value;
                        }

                        if (ca.DataType == DbType.DateTime && value != null && (DateTime)value == new DateTime())
                        {
                            value = System.DBNull.Value;
                        }

                        //不在字段更新列表中，不进行更新
                        if (updateItems != null && !updateItems.Contains(ca.ColumnName))
                            continue;

                        //普通字段
                        updateColumns.Add(string.Format("{0} = {1}{2}"
                            , this.DbHelper.GetDbObjectName(ca.ColumnName), this.DbHelper.GetParameterPrefix(), ca.ColumnName));

                        parameters.Add(new DataParameter { ParameterName = ca.ColumnName, Value = value, DataType = ca.DataType });//添加参数
                    }
                    else
                    {
                        //时间戳字段
                        updateColumns.Add(string.Format("{0} = {1}"
                            , this.DbHelper.GetDbObjectName(ca.ColumnName), this.DbHelper.GetDateFuncName()));
                    }

                }

                if (string.IsNullOrEmpty(whereSql) == true)
                {
                    throw new Exception("更新条件为空，实体未设置主键！");
                }

                //构成更新语句
                sql = string.Format("update {0} set {1} where {2}"
                            , tableName, string.Join(",", updateColumns), whereSql);

                //执行语句
                count = this.DbHelper.ExecuteSql(sql, parameters, this.Connection, this.Transaction);                

                return count;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();

                //记录日志
                this.RecordLog(sql, parameters);
            }
        }

        /// <summary>
        /// 更新实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体</param>
        /// <returns>更新行数</returns>
        public virtual int Update<T>(List<T> list) where T : new()
        {
            int count = 0;

            try
            {
                foreach (T info in list)
                {
                    this.Update<T>(info);
                }

                count = list.Count;

                return count;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();
            }
        }

        #endregion

        #region 更新表

        /// <summary>
        /// 更新表
        /// </summary>
        /// <param name="table">表</param>
        public virtual void UpdateTable(DataTable table)
        {
            try
            {
                //打开数据库连接
                this.OpenCon();

                //更新表
                this.DbHelper.Update(table, this.Connection, this.Transaction);
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();
            }
        }

        #endregion

        #region 执行sql语句

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>        
        /// <param name="parameters">参数</param>
        /// <returns>返回影响行数</returns>
        public virtual int ExecuteSql(string sql, params DataParameter[] parameters)
        {
            return this.ExecuteSql(sql, CommandType.Text, parameters);
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">命令执行方式</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回影响行数</returns>
        public virtual int ExecuteSql(string sql, CommandType commandType, params DataParameter[] parameters)
        {
            int count = 0;

            try
            {
                this.OpenCon();//打开数据库连接

                //为空处理
                foreach (DataParameter p in parameters)
                {
                    if (string.IsNullOrEmpty(p.ParameterName)==false&&(p.Value == null||p.GetType().ToString()=="System.DataTime"))
                    {
                        p.Value = System.DBNull.Value;
                    }
                }

                //执行
                count = this.DbHelper.ExecuteSql(sql, commandType, parameters.ToList(), this.Connection, this.Transaction);

                return count;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();

                //记录日志
                this.RecordLog(sql, parameters.ToList());
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回结果</returns>
        public virtual object ExecuteSqlScalar(string sql, params DataParameter[] parameters)
        {
            return this.ExecuteSqlScalar(sql, CommandType.Text, parameters);
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回结果</returns>
        public virtual object ExecuteSqlScalar(string sql, CommandType commandType, params DataParameter[] parameters)
        {
            object value = null;

            try
            {
                this.OpenCon();//打开数据库连接

                //为空处理
                foreach (DataParameter p in parameters)
                {
                    if (string.IsNullOrEmpty(p.ParameterName) == false&&(p.Value == null || p.GetType().ToString() == "System.DataTime"))
                    {
                        p.Value = System.DBNull.Value;
                    }
                }

                //执行
                value = this.DbHelper.ExecuteScalar(sql, commandType, parameters.ToList(), this.Connection, this.Transaction);

                return value;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();

                //记录日志
                this.RecordLog(sql, parameters.ToList());
            }
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>实体数据</returns>
        public virtual T Get<T>(string sql, params DataParameter[] parameters) where T : new()
        {
            T model;
            DataTable dt = new DataTable();
            try
            {
                this.OpenCon();

                //获取数据
                this.DbHelper.FillDataTable(dt, sql, parameters.ToList(), this.Connection, this.Transaction);

                if (dt.Rows.Count == 0)
                {
                    //未检索到符合条件数据
                    model = default(T);
                }
                else
                {
                    //检索到符合条件数据
                    model = ConvertToModel<T>(dt.Rows[0]);
                }
                return model;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();
                //记录日志
                this.RecordLog(sql, parameters.ToList());
            }
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>实体数据</returns>
        public virtual T Get<T>(T model) where T : new()
        {
            Type type = null;
            string sql = "";
            string tableName = "";
            List<string> selectColumns = new List<string>();
            string whereSql = "";
            List<DataParameter> parameters = null;
            DataTable dt = new DataTable();

            try
            {
                type = typeof(T);

                //获取表信息
                object[] attrsClassAtt = type.GetCustomAttributes(typeof(DBTableAttribute), true);

                if (attrsClassAtt == null)
                {
                    throw new Exception("当前实体没有添加属性DBTableAttribute！");
                }
                DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                tableName = this.DbHelper.GetDbObjectName(tableAtt.TableName);

                //获取查询条件
                this.GetSingleWhere<T>(model, out whereSql, out parameters);

                //获取字段属性
                foreach (var item in type.GetProperties())
                {
                    object[] attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (attrs.Count() == 0)
                    {
                        continue;
                    }

                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];//字段属性

                    selectColumns.Add(this.DbHelper.GetDbObjectName(ca.ColumnName));
                }

                if (string.IsNullOrEmpty(whereSql) == true)
                {
                    throw new Exception("获取条件为空，实体未设置主键！");
                }

                //构成查询语句
                sql = string.Format("SELECT {0} FROM {1} WHERE {2}", string.Join(",", selectColumns), tableName, whereSql);

                this.OpenCon();

                //获取数据
                this.DbHelper.FillDataTable(dt, sql, parameters, this.Connection, this.Transaction);

                if (dt.Rows.Count == 0)
                {
                    //未检索到符合条件数据
                    model = default(T);
                }
                else
                {
                    //检索到符合条件数据
                    model = ConvertToModel<T>(dt.Rows[0]);
                }

                return model;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();

                //记录日志
                this.RecordLog(sql, parameters.ToList());
            }
        }


        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="condition">查询条件</param>      
        /// <returns>实体列表</returns>
        public virtual IList<T> GetList<T, C>(C condition) where T : new()
        {
            string sql = null;
            string whereSql = null;
            string tableName = null;
            Type type = null;
            List<DataParameter> parameters = null;
            DataQueryHelper queryHelper = new DataQueryHelper();

            try
            {
                //设置sql工具
                queryHelper.DbHelper = this.DbHelper;

                type = typeof(T);

                //获取表信息
                object[] attrsClassAtt = type.GetCustomAttributes(typeof(DBTableAttribute), true);

                if (attrsClassAtt == null)
                {
                    throw new Exception("当前实体没有添加属性DBTableAttribute！");
                }
                DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                tableName = this.DbHelper.GetDbObjectName(tableAtt.TableName);

                //构成查询语句
                sql = "SELECT " + queryHelper.GetSelectColumns<T>() + " FROM " + tableName;

                queryHelper.GetWhere<C>(condition, out whereSql, out parameters);

                if (string.IsNullOrEmpty(whereSql) == false)
                    sql += " WHERE " + whereSql;

                //获取列表
                return this.GetList<T>(sql, parameters.ToArray());

            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();
            }
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>实体列表</returns>
        public virtual IList<T> GetList<T>(string sql, DataParameter[] parameters) where T : new()
        {
            IList<T> list = null;
            DataTable table = null;
            try
            {
                //获取表
                table = this.GetTable(sql, parameters);

                //转换
                list = ConvertToList<T>(table);

                return list;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();
            }
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        public virtual DataTable GetTable(string sql, DataParameter[] parameters)
        {
            try
            {
                return this.GetTable(sql, CommandType.Text, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        public virtual DataTable GetTable(string sql, CommandType commandType, DataParameter[] parameters)
        {
            DataTable table = null;
            try
            {
                table = new DataTable();

                this.FillTable(table, sql, commandType, parameters);

                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //记录日志
                this.RecordLog(sql, parameters.ToList());
            }

        }

        /// <summary>
        /// 填充DataTable
        /// </summary>
        /// <param name="table">datatable</param>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>       
        public virtual void FillTable(DataTable table, string sql, DataParameter[] parameters)
        {
            try
            {
                this.DbHelper.FillDataTable(table, sql, CommandType.Text, parameters.ToList(), this.Connection, this.Transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseCon();

                //记录日志
                this.RecordLog(sql, parameters.ToList());
            }

        }

        /// <summary>
        /// 填充DataTable
        /// </summary>
        /// <param name="table">datatable</param>
        /// <param name="sql">查询语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">参数</param>       
        public virtual void FillTable(DataTable table, string sql, CommandType commandType, DataParameter[] parameters)
        {
            try
            {
                this.OpenCon();

                this.DbHelper.FillDataTable(table, sql, commandType, parameters.ToList(), this.Connection, this.Transaction);

            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();

                //记录日志
                this.RecordLog(sql, parameters.ToList());
            }
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 按页获取数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="parameters">参数</param>
        /// <param name="page">分页信息</param>
        /// <returns>数据页(实体列表)</returns>
        public virtual DataPage GetDataPage<T, C>(C condition, DataPage page) where T : new()
        {
            string sql = null;
            string whereSql = null;
            string tableName = null;
            Type type = null;
            List<DataParameter> parameters = null;
            DataQueryHelper queryHelper = new DataQueryHelper();

            try
            {
                //设置sql工具
                queryHelper.DbHelper = this.DbHelper;

                type = typeof(T);

                //获取表信息
                object[] attrsClassAtt = type.GetCustomAttributes(typeof(DBTableAttribute), true);

                if (attrsClassAtt == null)
                {
                    throw new Exception("当前实体没有添加属性DBTableAttribute！");
                }
                DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                tableName = this.DbHelper.GetDbObjectName(tableAtt.TableName);

                //构成查询语句
                sql = "SELECT " + queryHelper.GetSelectColumns<T>() + " FROM " + tableName;

                queryHelper.GetWhere<C>(condition, out whereSql, out parameters);

                if (string.IsNullOrEmpty(whereSql) == false)
                    sql += " WHERE " + whereSql;

                return this.GetDataPage<T>(sql, parameters.ToArray(), page);
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();
            }
        }

        /// <summary>
        /// 按页获取数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="page">分页信息</param>
        /// <returns>数据页(实体列表)</returns>
        public virtual DataPage GetDataPage<T>(string sql, DataParameter[] parameters, DataPage page) where T : new()
        {
            IList<T> list = null;
            try
            {
                this.GetDataPage(sql, parameters, page);

                if (page.Result != null)
                {
                    list = ConvertToList<T>(page.Result as DataTable);
                }
                else
                {
                    list = new List<T>();
                }
                page.Result = list;

                return page;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();
            }
        }

        /// <summary>
        /// 按页获取数据
        /// </summary>       
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="page">分页信息</param>
        /// <returns>数据页(Datatable)</returns>
        public virtual DataPage GetDataPage(string sql, DataParameter[] parameters, DataPage page)
        {
            DataPageManager pageManager = new DataPageManager();
            try
            {
                //打开连接
                this.OpenCon();

                //设置处理对象
                pageManager.ConnInfo = this.ConInfo;
                pageManager.Connection = this.Connection;
                pageManager.Transaction = this.Transaction;
                pageManager.DbHelper = this.DbHelper;
                pageManager.DbType = this.ConInfo.DbType;

                //获取数据页
                page = pageManager.GetDataPage(sql, parameters.ToList(), page);

                return page;
            }
            catch (Exception ex)
            {
                this.RollbackTs();
                throw ex;
            }
            finally
            {
                this.CloseCon();

                //记录日志
                this.RecordLog(sql, parameters.ToList());
            }
        }

        #endregion

        #region 获取唯一记录查询条件

        /// <summary>
        /// 获取唯一记录查询条件
        /// </summary>
        public virtual void GetSingleWhere<T>(T model, out string whereSql, out List<DataParameter> parameters)
        {
            Type type = null;
            StringBuilder sBuilder = new StringBuilder();
            try
            {
                //初始化
                whereSql = "";
                parameters = new List<DataParameter>();

                type = typeof(T);

                //获取字段信息
                foreach (var item in type.GetProperties())
                {
                    object[] attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (attrs.Count() == 0)
                    {
                        continue;
                    }

                    //字段属性
                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];

                    //判断是否为主键
                    if (ca.IsKey == false)
                    {
                        continue;
                    }

                    //添加条件
                    sBuilder.Append(string.Format(" and {0} = {1}{2}"
                        , this.DbHelper.GetDbObjectName(ca.ColumnName), this.DbHelper.GetParameterPrefix(), ca.ColumnName));

                    //获取值
                    object value = item.GetValue(model, null);

                    //添加参数 
                    parameters.Add(new DataParameter { ParameterName = ca.ColumnName, Value = value, DataType = ca.DataType });
                }

                whereSql = sBuilder.ToString();

                if (whereSql != "")
                    whereSql = whereSql.Substring(4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 日志处理

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        protected void RecordLog(string sql, List<DataParameter> parameters)
        {
            switch (this.LogMode)
            {
                case SqlRecordMode.AllSql://所有sql语句                    
                    DataLogInfo log1 = new DataLogInfo();
                    log1.Sql = sql;
                    log1.Parameters = parameters;
                    log1.ExecuteTime = DateTime.Now;

                    this.DataLogs.Add(log1);
                    break;
                case SqlRecordMode.InsertUpdateDelete://insert update delete语句                    
                    if (sql.Trim().ToUpper().IndexOf("UPDATE ") == 0 || sql.Trim().ToUpper().IndexOf("INSERT ") == 0 || sql.Trim().ToUpper().IndexOf("DELETE ") == 0)
                    {
                        DataLogInfo log2 = new DataLogInfo();
                        log2.Sql = sql;
                        log2.Parameters = parameters;
                        log2.ExecuteTime = DateTime.Now;

                        this.DataLogs.Add(log2);
                    }
                    break;
            }
            //输出log
            this.ExportLog();
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        protected void ExportLog()
        {
            if (this.ExportLogEvent != null && this.DataLogs.Count > 0)
            {
                ExportLogEventArg arg = new ExportLogEventArg();
                arg.LogInfos = this.DataLogs;

                this.ExportLogEvent(this, arg);
                this.DataLogs.Clear();
            }
        }

        #endregion

        #region 获取默认值

        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <param name="valueType">默认值类型</param>
        /// <returns>默认值</returns>
        private string GetDefaultValue(DataDefaultValue valueType)
        {
            string v = "";

            switch (valueType)
            {
                case DataDefaultValue.SysDate://系统日期
                    v = this.DbHelper.GetDateFuncName();
                    break;
                case DataDefaultValue.Empty://空字符串
                    v = "''";
                    break;
                case DataDefaultValue.Zero://零
                    v = "0";
                    break;
            }

            return v;
        }

        #endregion

        #region 转换

        /// <summary>
        /// 将数据表转换为实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <returns>实体列表</returns>
        public static IList<T> ConvertToList<T>(DataTable table) where T : new()
        {
            IList<T> list = new List<T>();

            T model = new T();

            PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            Dictionary<string, string> cols = new Dictionary<string, string>();

            //获取属性-字段对应关系
            foreach (PropertyInfo p in properties)
            {
                object[] attrs = p.GetCustomAttributes(typeof(DBColumnAttribute), true);
                string colName = "";

                if (attrs.Count() == 0)
                {
                    if (table.Columns.IndexOf(p.Name) < 0)
                        continue;
                    colName = p.Name;
                }
                else
                {
                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];//字段属性 
                    if (table.Columns.IndexOf(ca.ColumnName) < 0)
                        continue;

                    colName = ca.ColumnName;
                }

                cols.Add(p.Name, colName);
            }

            //绑定数据
            foreach (DataRow row in table.Rows)
            {
                T e = new T();

                foreach (PropertyInfo p in properties)
                {
                    if (cols.ContainsKey(p.Name) == false)
                        continue;

                    string colName = cols[p.Name];

                    if (!(row[colName] is DBNull))
                        p.SetValue(e, ChangeType(row[colName], p.PropertyType), null);
                }
                list.Add(e);
            }

            return list;
        }

        /// <summary>
        /// 将数据行转换成实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="row">数据行</param>
        /// <returns>实体</returns>
        public static T ConvertToModel<T>(DataRow row) where T : new()
        {
            //判断行是否为空
            if (row == null)
                return default(T);

            T model = new T();

            PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            //赋值
            foreach (PropertyInfo p in properties)
            {
                object[] attrs = p.GetCustomAttributes(typeof(DBColumnAttribute), true);
                string colName = "";

                if (attrs.Count() == 0)
                {
                    if (row.Table.Columns.IndexOf(p.Name) < 0)
                        continue;
                    colName = p.Name;
                }
                else
                {
                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];//字段属性 
                    if (row.Table.Columns.IndexOf(ca.ColumnName) < 0)
                        continue;

                    colName = ca.ColumnName;
                }

                if (!(row[colName] is DBNull))
                    p.SetValue(model, ChangeType(row[colName], p.PropertyType), null);

            }

            return model;
        }

        private static object ChangeType(object value, Type conversionType)
        {
            if (null == value)
            {
                return conversionType;
            }
            if (!conversionType.IsGenericType)
            {
                return Convert.ChangeType(value, conversionType);
            }
            else
            {
                Type genericTypeDefinition = conversionType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return Convert.ChangeType(value, Nullable.GetUnderlyingType(conversionType));
                }
            }
            throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", value.GetType().FullName, conversionType.FullName));
        }

        #endregion
    }
}
