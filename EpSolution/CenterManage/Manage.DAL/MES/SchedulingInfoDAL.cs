using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Entity.MES;
using LAF.Data;
using System.Data;

namespace Manage.DAL.MES
{
    /// </summary>
    ///　模块名称：排班信息
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    public class SchedulingInfoDAL : BaseDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public SchedulingInfo Get(SchedulingInfo model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<SchedulingInfo>(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(SchedulingInfo condition, DataPage page)
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
                    page = session.GetDataPage<SchedulingInfo>(sql, parameters.ToArray(), page);
                }
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
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
        private string GetQuerySql(SchedulingInfo condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT t1.*,t2.PNAME as FNAME,t3.PLNAME as PLNAME,t4.PNAME AS WNAME FROM T_FP_SCHEDULING t1 
                                  LEFT OUTER JOIN T_FP_FACTORYINFO t2 on t1.FAID = t2.PID 
                                  LEFT OUTER JOIN T_FP_PRODUCTLINE t3 on t1.PRID = t3.PID
                                  LEFT OUTER JOIN T_FP_WORKGROUP t4 on t1.WOID = t4.PID");

                if (!string.IsNullOrEmpty(condition.FAID))
                {
                    whereBuilder.Append(" AND t2.PNAME like @FAID");
                    parameters.Add(new DataParameter("FAID", "%" + condition.FAID + "%"));
                }
                if (!string.IsNullOrEmpty(condition.PRID))
                {
                    whereBuilder.Append(" AND t2.PLNAME like @PRID");
                    parameters.Add(new DataParameter("PRID", "%" + condition.PRID + "%"));
                }
                if (!string.IsNullOrEmpty(condition.WOID))
                {
                    whereBuilder.Append(" AND t4.PNAME like @WOID");
                    parameters.Add(new DataParameter("WOID", "%" + condition.WOID + "%"));
                }

                //查询条件
                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
                }
                return sqlBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取导出的数据
        /// <summary>
        /// 获取导出的数据
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <returns>数据</returns>
        public DataTable GetExportData(SchedulingInfo model)
        {
            DataTable dt = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                //构成查询语句
                sql = this.GetQuerySql(model, ref parameters);
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    dt = session.GetTable(sql, parameters.ToArray());
                    dt.TableName = "SchedulingInfo";
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 插入信息
        /// <summary>
        /// 插入信息(单表)
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>插入行数</returns>
        public int Insert(SchedulingInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<SchedulingInfo>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>更新行数</returns>
        public int Update(SchedulingInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<SchedulingInfo>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>删除个数</returns>
        public int Delete(SchedulingInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除基本信息
                    count = session.Delete<SchedulingInfo>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

