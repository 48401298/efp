using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.MES;

namespace Manage.DAL.MES
{
    /// <summary>
    /// 追溯资源管理
    /// </summary>
    public class TrackManageDAL
    {
        #region 获取产品批次列表

        /// <summary>
        /// 获取产品批次列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(ProducePlan condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "PID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "UPDATETIME DESC";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<ProducePlan>(sql, parameters.ToArray(), page);
                }
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询语句</returns>
        private string GetQuerySql(ProducePlan condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT t1.*,t2.PNAME as FNAME,t3.PLNAME as PLNAME,t4.PNAME AS PDNAME FROM T_FP_PRODUCEPLAN t1 
                                  LEFT OUTER JOIN T_FP_FACTORYINFO t2 on t1.FACTORYPID = t2.PID 
                                  LEFT OUTER JOIN T_FP_PRODUCTLINE t3 on t1.PRID = t3.PID
                                  LEFT OUTER JOIN T_FP_PRODUCTINFO t4 on t1.ProductionID = t4.PID 
                                  WHERE STATUS = 1");

                if (!string.IsNullOrEmpty(condition.FACTORYPID))
                {
                    whereBuilder.Append(" AND t1.FACTORYPID = @FACTORYPID");
                    parameters.Add(new DataParameter("FACTORYPID", condition.FACTORYPID));
                }

                if (!string.IsNullOrEmpty(condition.PRID))
                {
                    whereBuilder.Append(" AND t1.PRID = @PRID");
                    parameters.Add(new DataParameter("PRID", condition.PRID));
                }

                if (!string.IsNullOrEmpty(condition.PRODUCTIONID))
                {
                    whereBuilder.Append(" AND t1.PRODUCTIONID = @PRODUCTIONID");
                    parameters.Add(new DataParameter("PRODUCTIONID", condition.PRODUCTIONID));
                }

                sqlBuilder.Append(whereBuilder.ToString());

                return sqlBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion
    }
}
