using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data
{
    /// <summary>
    /// 数据工厂
    /// </summary>
    public class DataFactory
    {
        /// <summary>
        /// 装配工厂
        /// <param name="isOpenCon">打开默认数据库连接</param>
        /// </summary>
        public static void Configure(bool isOpenCon)
        {
            DbManager.IsOpenConnection = isOpenCon;
            DbManager.Configure();
        }

        /// <summary>
        /// 关闭工厂
        /// </summary>
        public static void Stop()
        {
            DbManager.Close();
        }

        #region 创建普通会话

        /// <summary>
        /// 创建普通会话
        /// </summary>
        /// <returns>普通会话</returns>
        public static DataSession CreateSession()
        {
            DataSession session = new DataSession();

            return session;
        }

        /// <summary>
        /// 创建普通会话
        /// </summary>
        /// <param name="dbKey">数据库名</param>
        /// <returns>普通会话</returns>
        public static DataSession CreateSession(string dbKey)
        {
            DataSession session = new DataSession(dbKey);

            return session;
        }

        /// <summary>
        /// 创建普通会话
        /// </summary>
        /// <param name="conInfo">数据库连接信息</param>
        /// <returns>普通会话</returns>
        public static DataSession CreateSession(ConnectionInfo conInfo)
        {
            DataSession session = new DataSession(conInfo);

            return session;
        }

        #endregion
    }
}
