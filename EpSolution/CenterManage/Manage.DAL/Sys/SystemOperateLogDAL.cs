using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.Sys;
using System.Data;
using LAF.Common.Encrypt;

namespace Manage.DAL.Sys
{
    /// <summary>
    /// 系统操作LOG记录
    /// </summary>
    public class SystemOperateLogDAL
    {
        #region 获取列表

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="log">记录</param>
        /// <returns>插入数</returns>
        public DataPage GetList(SystemOperateLog log, DataPage page)
        {

            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = GetQuerySQL(log,ref  parameters);
                //分页关键字段及排序
                page.KeyName = "OPERATEID";
                if (string.IsNullOrEmpty(page.SortExpression))
                {
                    page.SortExpression = "OPERATETIME DESC";
                }
                if (page.SortExpression.IndexOf("OperateTypeName") > -1)
                {
                    page.SortExpression = page.SortExpression.Replace("OperateTypeName", "OperateType");
                }

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<SystemOperateLog>(sql, parameters.ToArray(), page);
                }

                return page;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 查询语句
        /// <summary>
        /// 查询语句
        /// </summary>
        /// <returns></returns>
        public string GetQuerySQL(SystemOperateLog log, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                sqlBuilder.Append(@" SELECT OP.*,U.USERNAME AS UserName FROM T_SYSTEM_OPERATE_LOG OP LEFT JOIN T_QM_USER U ON OP.USERID = U.LOGINUSERID");
                //用户
                if (!string.IsNullOrEmpty(log.UserID))
                {
                    whereBuilder.Append(" AND OP.USERID = @USERID");
                    parameters.Add(new DataParameter { ParameterName = "USERID", DataType = DbType.String, Value = log.UserID });
                }
                //起始时间
                if (!string.IsNullOrEmpty(log.StartDate))
                {
                    whereBuilder.Append(" AND OP.OPERATETIME >= @StartDate");
                    parameters.Add(new DataParameter { ParameterName = "StartDate", DataType = DbType.String, Value = log.StartDate });
                }
                //结束时间
                if (!string.IsNullOrEmpty(log.EndDate))
                {
                    whereBuilder.Append(" AND OP.OPERATETIME <= @EndDate");
                    parameters.Add(new DataParameter { ParameterName = "EndDate", DataType = DbType.String, Value = log.EndDate });
                }
                //操作类型
                if (!string.IsNullOrEmpty(log.OperateType))
                {
                    whereBuilder.Append(" AND OP.OPERATETYPE = @OPERATETYPE");
                    parameters.Add(new DataParameter { ParameterName = "OPERATETYPE", DataType = DbType.String, Value = log.OperateType });
                }

                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
                }

                return sqlBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region 插入操作记录

        /// <summary>
        /// 插入操作记录
        /// </summary>
        /// <param name="log">记录</param>
        /// <returns>插入数</returns>
        public int Insert(SystemOperateLog log)
        {
            int count = 0;

            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入权限信息
                    count = session.Insert<SystemOperateLog>(log);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 插入操作记录(列表)

        /// <summary>
        /// 插入操作记录(列表)
        /// </summary>
        /// <param name="log">记录</param>
        /// <returns>插入数</returns>
        public int InsertList(List<SystemOperateLog> logList)
        {
            int count = 0;

            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    session.Insert<SystemOperateLog>(logList);

                    session.CommitTs();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 获取导出的数据
        /// <summary>
        /// 获取导出的数据
        /// </summary>
        /// <param name="user">查询条件</param>      
        /// <returns>数据</returns>
        public DataTable GetExportData(SystemOperateLog log)
        {
            DataTable dt = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                //构成查询语句
                sql = this.GetQuerySql(log, ref parameters);
                sql += " ORDER BY OPERATETIME DESC";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    dt = session.GetTable(sql, parameters.ToArray());
                    dt.TableName = "SystemOperateLog";
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        #region 获取查询语句

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询语句</returns>
        private string GetQuerySql(SystemOperateLog log, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();

            try
            {
                //构成查询语句
                sqlBuilder.Append(" SELECT LG.CLIENTIP AS ClientIP,LG.OPERATETYPE AS OperateTypeName,LG.OPERATETIME AS OperateTime,LG.OPERATECONTENT AS OperateContent,LG.REMAEK AS Remark,QU.USERNAME AS UserName ");
                sqlBuilder.Append(" FROM T_SYSTEM_OPERATE_LOG LG LEFT JOIN T_QM_USER QU ON LG.USERID = QU.LOGINUSERID ");

                return sqlBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
