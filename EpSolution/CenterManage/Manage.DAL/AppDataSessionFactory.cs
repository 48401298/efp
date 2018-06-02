using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.Log;

namespace Manage.DAL
{
    /// <summary>
    /// 数据会话工厂
    /// </summary>
    public class AppDataSessionFactory
    {
        /// <summary>
        /// 创建主数据库访问会话
        /// </summary>
        /// <returns>数据访问会话</returns>
        public static IDataSession CreateMainSession()
        {
            DataSession session = DataFactory.CreateSession();

            session.LogMode = SqlRecordMode.InsertUpdateDelete;
            session.ExportLogEvent += new DataSession.ExportLogEventHandler(session_ExportLogEvent);

            return session;
        }

        /// <summary>
        /// 输出sql日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        static void session_ExportLogEvent(object sender, ExportLogEventArg arg)
        {
            StringBuilder infoBuilder = new StringBuilder();

            foreach (DataLogInfo log in arg.LogInfos)
            {
                infoBuilder.AppendLine(log.Sql);
                infoBuilder.AppendLine("ParameterList:");
                
                foreach(DataParameter parameter in log.Parameters)
                {
                    infoBuilder.AppendLine(string.Format("{0}:{1}"
                        ,parameter.ParameterName,parameter.Value!=null?parameter.Value.ToString():""));
                }
            }

            LogInfo l = new LogInfo();
            l.Info = infoBuilder.ToString();
            LogManager.LogHelper.Debug(l);
        }

        /// <summary>
        /// 创建其他数据库访问会话
        /// <param name="dbKey">数据标识</param>
        /// </summary>
        /// <returns>数据访问会话</returns>
        public static IDataSession CreateSession(string dbKey)
        {
            DataSession session = DataFactory.CreateSession(dbKey);

            session.ExportLogEvent += new DataSession.ExportLogEventHandler(session_ExportLogEvent);

            return session;
        }
    }
}
