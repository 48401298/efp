using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using LAF.Data.Distribute;
using MySql.Data.MySqlClient;


namespace LAF.Data
{
    /// <summary>
    /// 数据库连接管理
    /// </summary>
    public class DbManager
    {
        /// <summary>
        /// 数据库连接信息
        /// </summary>
        public static List<ConnectionInfo> _ConnectionInfos { get; set; }

        private static bool _IsOpenConnection = true;

        /// <summary>
        /// 是否打开默认数据库连接
        /// </summary>
        public static bool IsOpenConnection
        {
            get
            {
                return _IsOpenConnection;
            }
            set
            {
                _IsOpenConnection = value;
            }
        }

        /// <summary>
        /// 主数据库连接
        /// </summary>
        public static ConnectionInfo MainConnectionInfo { get; set; }

        /// <summary>
        /// 分布式引擎
        /// </summary>
        public static IShareEngine ShareEngine { get; set; }

        /// <summary>
        /// Sql执行超时时间
        /// </summary>
        public static int SqlTimeout { get; set; }

        #region 加载配置信息

        /// <summary>
        /// 加载配置信息
        /// </summary>
        public static void Configure()
        {
            _ConnectionInfos = new List<ConnectionInfo>();
            try
            {
                //读取字符串是否进行加密标志
                string conEncrypt = ConfigurationManager.AppSettings["ConEncrypt"];
                if (conEncrypt == null)
                    conEncrypt = "false";

                //获取sql超时时间
                int timeout = 0;
                if (ConfigurationManager.AppSettings["SqlTimeout"] != null)
                {                    
                    int.TryParse(ConfigurationManager.AppSettings["SqlTimeout"], out timeout);
                }
                if (timeout == 0)
                {
                    SqlTimeout = 30;
                }
                else
                {
                    SqlTimeout = timeout;
                }

                //提取数据连接信息
                foreach (System.Configuration.ConnectionStringSettings cs in System.Configuration.ConfigurationManager.ConnectionStrings)
                {
                    if (cs.Name.ToLower() == "localsqlserver")
                        continue;

                    ConnectionInfo conInfo = new ConnectionInfo();

                    conInfo.DbKey = cs.Name;
                    conInfo.ProviderName = cs.ProviderName;
                    conInfo.ConnectionString = cs.ConnectionString;

                    if (conEncrypt == "true")
                    {
                        conInfo.ConnectionString=LAF.Common.Encrypt.DESEncrypt.Decrypt(cs.ConnectionString);
                    }

                    //设置数据库类型
                    switch (conInfo.ProviderName.ToLower())
                    {
                        case "system.data.sqlclient"://SqlServer
                            conInfo.DbType = DataBaseType.SqlServer;
                            break;
                        case "system.data.oracleclient"://Oracle(微软)
                            conInfo.DbType = DataBaseType.Oracle;
                            break;
                        case "oracle.dataaccess.client"://Oracle(甲骨文)
                            conInfo.DbType = DataBaseType.OracleOdp;
                            break;
                        case "system.data.oledb"://OleDb
                            conInfo.DbType = DataBaseType.OleDb;
                            break;
                        case "mysql.data.mysqlclient"://mysql
                            conInfo.DbType = DataBaseType.MySql;
                            break;
                        case "system.data.sqlite"://SqlLite
                            conInfo.DbType = DataBaseType.SqlLite;
                            break;
                        default:
                            continue;
                    }

                    //打开数据库连接
                    if (_IsOpenConnection == true)
                    {
                        conInfo.Connection = GetCon(conInfo.DbType, conInfo.ConnectionString, true);
                    }

                    _ConnectionInfos.Add(conInfo);
                }

                MainConnectionInfo = _ConnectionInfos[0];
            }
            catch (DbException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 加载配置信息
        /// </summary>
        /// <param name="connections">数据库连接信息</param>
        public static void Configure(List<ConnectionInfo> connections)
        {
            _ConnectionInfos = connections;

            try
            {

                //提取数据连接信息
                foreach (ConnectionInfo conInfo in connections)
                {
                    //打开数据库连接
                    if (_IsOpenConnection == true)
                    {
                        conInfo.Connection = GetCon(conInfo.DbType, conInfo.ConnectionString, true);
                    }
                }
                MainConnectionInfo = _ConnectionInfos[0];

            }
            catch (DbException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 打开连接

        public static void Open()
        {
            foreach (ConnectionInfo conInfo in _ConnectionInfos)
            {
                //打开数据库连接
                conInfo.Connection = GetCon(conInfo.DbType, conInfo.ConnectionString, true);
            }
        }

        #endregion

        #region 关闭连接

        public static void Close()
        {
            if (_ConnectionInfos != null)
            {
                foreach (ConnectionInfo conInfo in _ConnectionInfos)
                {
                    //关闭数据库连接
                    if (conInfo.Connection != null && conInfo.Connection.State != ConnectionState.Closed)
                        conInfo.Connection.Close();
                }
            }
        }

        #endregion

        #region 根据标识获取数据库连接

        /// <summary>
        /// 根据名称，获取数据库连接
        /// </summary>
        /// <param name="dbKey">标识</param>
        /// <param name="isThrow">是否抛出异常</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection GetCon(string dbKey, bool isThrow)
        {
            IDbConnection conn = null;
            try
            {

                conn = GetCon(GetConInfo(dbKey).DbType, GetConInfo(dbKey).ConnectionString, isThrow);

                if (conn == null)
                    throw new Exception("没有找到指定数据库连接！");

            }
            catch (DbException e)
            {
                if (isThrow)
                    throw (e);
                else
                    return null;
            }
            return conn;
        }

        #endregion

        #region 根据指定连接字符串，获得数据库连接

        /// <summary>
        /// 根据指定连接字符串，获得数据库连接
        /// </summary>
        /// <param name="dbType">类型</param>
        /// <param name="conStr">数据库连接字符串</param>
        /// <param name="isThrow">是否抛出异常</param>
        /// <returns></returns>
        public static DbConnection GetCon(DataBaseType dbType, string conStr, bool isThrow)
        {
            DbConnection conn = null;
            try
            {
                switch (dbType)
                {
                    case DataBaseType.SqlServer://sqlserver
                        conn = new SqlConnection(conStr);
                        break;
                    case DataBaseType.Oracle://oracle
                        conn = new OracleConnection(conStr);
                        break;
                    case DataBaseType.OleDb://oledb
                        conn = new OleDbConnection(conStr);
                        break;
                    case DataBaseType.MySql://mysql
                        conn = new MySqlConnection(conStr);
                        break;
                }

                //判断是否打开数据库
                if (IsOpenConnection == true)
                    conn.Open();
            }
            catch (DbException ex)
            {
                if (isThrow)
                    throw (ex);
                else
                    return null;
            }
            return conn;

        }

        #endregion

        #region 根据标识获取连接信息

        /// <summary>
        /// 根据标识获取连接信息
        /// </summary>
        /// <param name="dbKey">标识</param>
        /// <returns>连接信息</returns>
        public static ConnectionInfo GetConInfo(string dbKey)
        {
            ConnectionInfo conInfo = null;

            foreach (ConnectionInfo c in _ConnectionInfos)
            {
                if (c.DbKey == dbKey)
                {
                    conInfo = c;
                    break;
                }
            }

            return conInfo;
        }

        /// <summary>
        /// 根据标识获取分布式连接信息
        /// </summary>
        /// <param name="shareKey">标识</param>
        /// <returns>连接信息</returns>
        public static ConnectionInfo GetShareConInfo(string shareKey)
        {
            ConnectionInfo conInfo = null;
            string dbKey="";

            if (ShareEngine == null)
                throw new Exception("未创建分布式引擎。");

            //获取数据库关键字
            dbKey = ShareEngine.GetDbKey(shareKey);

            //获取连接信息
            conInfo = _ConnectionInfos.Find(p => p.DbKey == dbKey);

            return conInfo;
        }

        #endregion

    }
}
