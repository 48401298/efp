using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.MES;

namespace Manage.DAL.MES
{
    /// <summary>
    ///　模块名称：产成品质量检查单
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
        public QualityCheckInfo Get(QualityCheckInfo model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<QualityCheckInfo>(model);
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
        public List<QualityCheckResult> GetResultList(string checkID)
        {
            List<QualityCheckResult> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT * FROM T_FP_QualityCheckResult qcr 
                 where CheckID = @CheckID order by UploadTime desc";
                parameters.Add(new DataParameter("CheckID", checkID));

                list = session.GetList<QualityCheckResult>(sql, parameters.ToArray()).ToList<QualityCheckResult>();
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
                    page = session.GetDataPage<QualityCheckInfo>(sql, parameters.ToArray(), page);
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
                //构成查询语句
                sqlBuilder.Append(@"SELECT QC.ID,QC.BillNO,QC.CheckDate,QC.BatchNumber,QC.CheckPerson,CASE QC.CheckResult 
         WHEN '1' THEN '不合格' 
ELSE '合格' END AS CheckResult, A.* FROM T_FP_QUALITYCHECK QC 
LEFT OUTER JOIN (SELECT distinct(GI.BATCHNUMBER) BN,GI.FACTORYPID,GI.PRODUCTIONID,PI.PNAME PDNAME FROM T_FP_GOODINFO GI 
INNER JOIN T_FP_PRODUCTINFO PI ON GI.PRODUCTIONID = PI.PID
) A ON QC.BATCHNUMBER =  A.BN ");
   
                if (!string.IsNullOrEmpty(condition.BillNO))
                {
                    whereBuilder.Append(" AND BillNO like @BillNO");
                    parameters.Add(new DataParameter("BillNO", "%" + condition.BillNO + "%"));
                }
                if (!string.IsNullOrEmpty(condition.StartDate)) 
                {
                    whereBuilder.Append(" AND CheckDate >= @StartDate");
                    parameters.Add(new DataParameter("StartDate", condition.StartDate));
                }
                if (!string.IsNullOrEmpty(condition.StartDate))
                {
                    whereBuilder.Append(" AND CheckDate <= @EndDate");
                    parameters.Add(new DataParameter("EndDate", condition.EndDate));
                }
                if (!string.IsNullOrEmpty(condition.CheckResult))
                {
                    whereBuilder.Append(" AND CheckResult = @CheckResult");
                    parameters.Add(new DataParameter("CheckResult", condition.CheckResult));
                }
                if (!string.IsNullOrEmpty(condition.FACTORYPID))
                {
                    whereBuilder.Append(" AND A.FACTORYPID = @FACTORYPID");
                    parameters.Add(new DataParameter("FACTORYPID", condition.FACTORYPID));
                }
                if (!string.IsNullOrEmpty(condition.PRODUCEID))
                {
                    whereBuilder.Append(" AND A.PRODUCTIONID = @PRODUCTIONID");
                    parameters.Add(new DataParameter("PRODUCTIONID", condition.PRODUCEID));
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
        public bool ExistsBillNO(QualityCheckInfo model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_QUALITYCHECK");
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
        public int Insert(QualityCheckInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    //插入基本信息
                    count = session.Insert<QualityCheckInfo>(model);
                    foreach (QualityCheckResult detail in model.QualityCheckResults)
                    {
                        detail.ID = Guid.NewGuid().ToString();
                        detail.CheckID = model.ID;
                        session.Insert<QualityCheckResult>(detail);
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
        public int Update(QualityCheckInfo model)
        {
            int count = 0;
            string sql = "delete from T_FP_QUALITYCHECKRESULT where CheckID = @CheckID";
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    count = session.Update<QualityCheckInfo>(model);
                    session.ExecuteSql(sql, new DataParameter("CheckID", model.ID));

                    foreach (QualityCheckResult detail in model.QualityCheckResults)
                    {
                        detail.ID = Guid.NewGuid().ToString();
                        detail.CheckID = model.ID;
                        session.Insert<QualityCheckResult>(detail);
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
        public int Delete(QualityCheckInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除BOM信息
                    count = session.Delete<QualityCheckInfo>(model);
                    //删除BOM明细信息
                    string sql = "DELETE FROM T_FP_QUALITYCHECKRESULT WHERE CheckID = @CheckID";
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

        /// <summary>
        /// 根据批次号取出质检相关产品信息
        /// </summary>
        /// <param name="batchNumber"></param>
        /// <returns></returns>
        public QualityCheckInfo GetPDInfo(string batchNumber)
        {
            List<QualityCheckInfo> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT GI.BATCHNUMBER,GI.PLANDATE PDDATE,PI.PCODE PDCODE,PI.PNAME PDNAME FROM T_FP_ProducePlan GI 
INNER JOIN T_FP_PRODUCTINFO PI ON GI.PRODUCTIONID = PI.PID WHERE GI.BATCHNUMBER = @BatchNumber";
                parameters.Add(new DataParameter("BatchNumber", batchNumber));

                list = session.GetList<QualityCheckInfo>(sql, parameters.ToArray()).ToList<QualityCheckInfo>();
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
