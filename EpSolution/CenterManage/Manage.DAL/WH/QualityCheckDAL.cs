using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.WH;

namespace Manage.DAL.WH
{
    /// <summary>
    ///　模块名称：货品质量检查单
    ///　作    者：wanglu
    ///　编写日期：2018年02月02日
    /// </summary>
    public class QualityCheckDAL : BaseDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public WHQualityCheck Get(WHQualityCheck model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<WHQualityCheck>(model);
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
        /// 产成品质量检查单结果
        /// </summary>
        /// <param name="checkID"></param>
        /// <returns></returns>
        public List<WHQualityCheckResult> GetResultList(string checkID)
        {
            List<WHQualityCheckResult> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT * FROM T_WH_QualityCheckResult qcr 
                 where CheckID = @CheckID order by UploadTime desc";
                parameters.Add(new DataParameter("CheckID", checkID));

                list = session.GetList<WHQualityCheckResult>(sql, parameters.ToArray()).ToList<WHQualityCheckResult>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(QualityCheckCondition condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "ID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "UPDATETIME DESC";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<WHQualityCheck>(sql, parameters.ToArray(), page);
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
        private string GetQuerySql(QualityCheckCondition condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT QC.ID,QC.BillNO,QC.CheckDate,QC.CheckPerson,CASE QC.CheckResult 
                                         WHEN '1' THEN '不合格' ELSE '合格' END AS CheckResult, bill.BillNO as InStockBillNo,QC.UPDATETIME
                                    FROM T_WH_QualityCheck QC 
                                    LEFT OUTER JOIN T_WH_InStockBill bill on QC.BillID = bill.ID");
   
                if (!string.IsNullOrEmpty(condition.BillNO))
                {
                    whereBuilder.Append(" AND QC.BillNO like @BillNO");
                    parameters.Add(new DataParameter("BillNO", "%" + condition.BillNO + "%"));
                }
                if (!string.IsNullOrEmpty(condition.StartDate)) 
                {
                    whereBuilder.Append(" AND QC.CheckDate >= @StartDate");
                    parameters.Add(new DataParameter("StartDate", condition.StartDate));
                }
                if (!string.IsNullOrEmpty(condition.StartDate))
                {
                    whereBuilder.Append(" AND QC.CheckDate <= @EndDate");
                    parameters.Add(new DataParameter("EndDate", condition.EndDate));
                }
                if (!string.IsNullOrEmpty(condition.CheckResult))
                {
                    whereBuilder.Append(" AND QC.CheckResult = @CheckResult");
                    parameters.Add(new DataParameter("CheckResult", condition.CheckResult));
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

        #region 信息是否重复
        /// <summary>
        /// 判断单号是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsBillNO(WHQualityCheck model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_WH_QualityCheck");
                whereBuilder.Append(" AND ID <> @ID ");
                parameters.Add(new DataParameter { ParameterName = "ID", DataType = DbType.String, Value = model.ID });
                if (!string.IsNullOrEmpty(model.BillNO))
                {
                    whereBuilder.Append(" AND BillNO = @BillNO ");
                    parameters.Add(new DataParameter { ParameterName = "BillNO", DataType = DbType.String, Value = model.BillNO });
                }
                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
                }
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    count = Convert.ToInt32(session.ExecuteSqlScalar(sqlBuilder.ToString(), parameters.ToArray()));
                }
                return count > 0;
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
        public int Insert(WHQualityCheck model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    //插入基本信息
                    count = session.Insert<WHQualityCheck>(model);
                    foreach (WHQualityCheckResult detail in model.QualityCheckResults)
                    {
                        detail.ID = Guid.NewGuid().ToString();
                        detail.CheckID = model.ID;
                        session.Insert<WHQualityCheckResult>(detail);
                    }
                    session.CommitTs();
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
        public int Update(WHQualityCheck model)
        {
            int count = 0;
            string sql = "delete from T_WH_QualityCheckResult where CheckID = @CheckID";
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    count = session.Update<WHQualityCheck>(model);
                    session.ExecuteSql(sql, new DataParameter("CheckID", model.ID));

                    foreach (WHQualityCheckResult detail in model.QualityCheckResults)
                    {
                        detail.ID = Guid.NewGuid().ToString();
                        detail.CheckID = model.ID;
                        session.Insert<WHQualityCheckResult>(detail);
                    }
                    session.CommitTs();
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
        public int Delete(WHQualityCheck model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除基本信息
                    count = session.Delete<WHQualityCheck>(model);
                    //删除明细信息
                    string sql = "DELETE FROM T_WH_QualityCheckResult WHERE CheckID = @CheckID";
                    session.ExecuteSql(sql, new DataParameter("CheckID", model.ID));
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取最大单号

        /// <summary>
        /// 获取最大单号
        /// </summary>
        /// <returns></returns>
        public string GetMaxBillNO()
        {
            string no = null;
            object value = null;
            string sql = "select max(BillNO) from T_WH_QualityCheck Where CheckDate >= @StartDate and CheckDate <=@EndDate";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                value = session.ExecuteSqlScalar(sql
                    , new DataParameter("StartDate", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                    , new DataParameter("EndDate", DateTime.Now));
            }

            if (value != null && value != System.DBNull.Value)
                no = value.ToString();

            return no;
        }

        #endregion

        #region 获取需质检入库单

        /// <summary>
        /// 获取需质检入库单
        /// </summary>
        /// <returns>入库单列表</returns>
        public List<InStockBill> GetNeedInStockBills()
        {
            List<InStockBill> list = null;
            string sql = @"select ID,BillNO 
                            from T_WH_InStockBill bill 
                            where not exists(select qc.ID from T_WH_QualityCheck qc  where qc.BillID=bill.ID)
                            order by bill.BillNO";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<InStockBill>(sql, new List<DataParameter>().ToArray()).ToList();
            }

            return list;
        }

        #endregion

    }
}
