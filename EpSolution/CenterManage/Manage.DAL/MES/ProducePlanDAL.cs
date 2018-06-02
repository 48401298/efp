using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Entity.MES;
using LAF.Data;
using System.Data;

namespace Manage.DAL.MES
{
    /// <summary>
    ///　模块名称：工厂信息
    ///　作    者：wanglu
    ///　编写日期：2017年09月10日
    /// </summary>
    public class ProducePlanDAL : BaseDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public ProducePlan Get(ProducePlan model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<ProducePlan>(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过生产批次号获取生产计划
        /// </summary>
        /// <param name="batchNumber">生产批次号</param>
        /// <returns>生产计划</returns>
        public ProducePlan GetByBatchNumber(string batchNumber)
        {
            ProducePlan plan = null;
            string sql = "select * from T_FP_ProducePlan where BatchNumber = @BatchNumber";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                //获取信息
                plan = session.Get<ProducePlan>(sql, new DataParameter("BatchNumber",batchNumber));
            }

            return plan;
        }

        #endregion

        #region 获取列表

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public List<ProducePlan> GetList()
        {
            List<ProducePlan> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT DISTINCT BATCHNUMBER FROM T_FP_PRODUCEPLAN WHERE Status='0' ORDER BY BATCHNUMBER";
                list = session.GetList<ProducePlan>(sql, new List<DataParameter>().ToArray()).ToList<ProducePlan>();
            }

            return list;
        }

        /// <summary>
        /// 获取完成列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public List<ProducePlan> GetCList()
        {
            List<ProducePlan> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT DISTINCT BATCHNUMBER FROM T_FP_PRODUCEPLAN WHERE Status='1' ORDER BY BATCHNUMBER";
                list = session.GetList<ProducePlan>(sql, new List<DataParameter>().ToArray()).ToList<ProducePlan>();
            }

            return list;
        }

        /// <summary>
        /// 获取生产计划下拉列表
        /// </summary>
        /// <returns>生产计划列表</returns>
        public List<ProducePlan> GetDDList()
        {
            List<ProducePlan> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT PID, BATCHNUMBER FROM T_FP_PRODUCEPLAN WHERE Status='0' ORDER BY BATCHNUMBER";
                list = session.GetList<ProducePlan>(sql, new List<DataParameter>().ToArray()).ToList<ProducePlan>();
            }

            return list;
        }

        /// <summary>
        /// 获取未完成生产计划
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>生产计划</returns>
        public List<ProducePlan> GetUnFinishedPlans(ProducePlan condition)
        {
            List<ProducePlan> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT DISTINCT pp.PID,pp.BATCHNUMBER,pi.PNAME AS PDNAME,pp.PLANAMOUNT,pp.FACTAMOUNT
                    FROM T_FP_PRODUCEPLAN  pp
                    INNER JOIN T_FP_PRODUCTINFO pi on pp.PRODUCTIONID = pi.PID
                    WHERE pp.Status='0' ORDER BY pp.BATCHNUMBER";
                list = session.GetList<ProducePlan>(sql, new List<DataParameter>().ToArray()).ToList<ProducePlan>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
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
        #endregion

        #region 获取查询语句
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
                                  LEFT OUTER JOIN T_FP_PRODUCTINFO t4 on t1.ProductionID = t4.PID ");

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
        public DataTable GetExportData(ProducePlan model)
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
                    dt.TableName = "FactoryInfo";
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
        public int Insert(ProducePlan model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<ProducePlan>(model);
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
        public int Update(ProducePlan model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<ProducePlan>(model);
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
        public int Delete(ProducePlan model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除基本信息
                    count = session.Delete<ProducePlan>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设置计划已完成

        public void SignPlanFinished(ProducePlan plan)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = "update T_FP_PRODUCEPLAN set STATUS = '1' where PID = @PID";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.ExecuteSql(sql, new DataParameter("PID",plan.PID));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public ProductInfo GetPNameByIDBatchNumber(string batchNumber)
        {
            List<ProductInfo> list = null;
            string sql = null;

            sql = @"SELECT t2.* from T_FP_ProducePlan t1 ,T_FP_PRODUCTINFO t2
                   WHERE t1.ProductionID =  t2.PID AND t1.BatchNumber = @BatchNumber";

            List<DataParameter> parameters = new List<DataParameter>();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                parameters.Add(new DataParameter("BatchNumber", batchNumber));
                list = session.GetList<ProductInfo>(sql, parameters.ToArray()).ToList<ProductInfo>();
            }
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public GoodInfo GetGoodInfoByBatchNumber(string batchNumber)
        {
            List<GoodInfo> list = null;
            string sql = null;

            sql = @"SELECT t1.PID AS PLANID,t1.BATCHNUMBER,t1.FACTORYPID,t1.PRID,t1.ProductionID,t1.FactAmount,
                  t2.PCODE,t2.PNAME,t2.SPECIFICATION 
                  FROM T_FP_ProducePlan t1 ,T_FP_PRODUCTINFO t2
                  WHERE t1.ProductionID =  t2.PID AND t1.BatchNumber = @BatchNumber";

            List<DataParameter> parameters = new List<DataParameter>();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                parameters.Add(new DataParameter("BatchNumber", batchNumber));
                list = session.GetList<GoodInfo>(sql, parameters.ToArray()).ToList<GoodInfo>();
            }
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据ID获取产品信息（要货信息）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SupplyInfo GetProducePlanInfoByID(string id)
        {
            List<SupplyInfo> list = null;
            string sql = null;

            sql = @"SELECT pp.BATCHNUMBER,fi.PNAME AS FactoryName,pi.PNAME AS ProduceName,pl.PLNAME AS PLName,pp.ProductionID
                    FROM T_FP_ProducePlan pp 
                    LEFT OUTER JOIN T_FP_FACTORYINFO fi ON pp.FACTORYPID =fi.PID
                    LEFT OUTER JOIN T_FP_PRODUCTINFO pi ON pp.PRODUCTIONID = pi.PID
                    LEFT OUTER JOIN T_FP_PRODUCTLINE pl on pp.PRID = pl.PID
                    WHERE pp.PID = @PID";

            List<DataParameter> parameters = new List<DataParameter>();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                parameters.Add(new DataParameter("PID", id));
                list = session.GetList<SupplyInfo>(sql, parameters.ToArray()).ToList<SupplyInfo>();
            }
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }
    }
}