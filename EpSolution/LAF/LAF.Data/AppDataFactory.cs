using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data
{
    /// <summary>
    /// 应用数据工厂
    /// </summary>
    public class AppDataFactory
    {
        /// <summary>
        /// 创建主数据库访问会话
        /// </summary>
        /// <returns>数据访问会话</returns>
        public static IDataSession CreateMainSession()
        {
            DataSession session = DataFactory.CreateSession();

            return session;
        }

        /// <summary>
        /// 创建其他数据库访问会话
        /// <param name="dbKey">数据标识</param>
        /// </summary>
        /// <returns>数据访问会话</returns>
        public static IDataSession CreateSession(string dbKey)
        {
            DataSession session = DataFactory.CreateSession(dbKey);

            return session;
        }

    }    
}
