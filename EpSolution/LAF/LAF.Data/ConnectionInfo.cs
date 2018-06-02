
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data
{
    /// <summary>
    /// 数据库连接配置信息
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// 数据库标识
        /// </summary>
        public string DbKey { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DbType { get; set; }

        /// <summary>
        /// 驱动提供者
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection Connection { get; set; }

        /// <summary>
        /// 连接状态
        /// </summary>
        public bool ConnectionStatus { get; set; }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns></returns>
        public string GetDataBaseName()
        {
            string dbName = null;
            string[] values = this.ConnectionString.Split(";".ToCharArray());

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].ToLower().IndexOf("initial catalog") >= 0)
                {
                    string[] n = values[i].Split("=".ToCharArray());
                    if (n.Length > 1)
                        dbName = n[1];
                    break;
                }
            }

            return dbName;
        }
    }
}
