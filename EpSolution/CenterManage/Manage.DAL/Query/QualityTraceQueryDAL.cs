using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data;
using LAF.Data.Attributes;
using Manage.Entity.Query;

namespace Manage.DAL.Query
{
    /// <summary>
    /// 追溯查询
    /// </summary>
    public class QualityTraceQueryDAL
    {
        #region 获取质量追溯产品列表

        /// <summary>
        /// 获取质量追溯产品列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(QualityTraceCondition condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "ID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "CREATETIME DESC";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<QualityTraceInfo>(sql, parameters.ToArray(), page);
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
        private string GetQuerySql(QualityTraceCondition condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT G.PID,G.GoodBarCode,p.PCODE as ProductCode,p.PNAME as ProductName,
                                    G.BatchNumber,f.PNAME as FactoryName,l.PLNAME as LineName,G.PLANDATE,g.CREATETIME
                                    FROM T_FP_GoodInfo G
                                    left outer join T_FP_PRODUCTINFO p on g.ProductionID=p.PID
                                    left outer join T_FP_FACTORYINFO f on g.FACTORYPID=f.PID
                                    left outer join T_FP_PRODUCTLINE l on g.PRID=l.PID");

                if (string.IsNullOrEmpty(condition.StartDate) == false)
                {
                    whereBuilder.Append(" AND PLANDATE >= @StartDate");
                    parameters.Add(new DataParameter("StartDate", DateTime.Parse(condition.StartDate)));
                }
                if (string.IsNullOrEmpty(condition.EndDate) == false)
                {
                    whereBuilder.Append(" AND PLANDATE <= @EndDate");
                    parameters.Add(new DataParameter("EndDate", DateTime.Parse(condition.EndDate)));
                }

                if (string.IsNullOrEmpty(condition.Factory) == false)
                {
                    whereBuilder.Append(" AND FACTORYPID <= @Factory");
                    parameters.Add(new DataParameter("Factory", condition.Factory));
                }

                if (string.IsNullOrEmpty(condition.ProductLine) == false)
                {
                    whereBuilder.Append(" AND PRID <= @ProductLine");
                    parameters.Add(new DataParameter("ProductLine", condition.ProductLine));
                }

                if (string.IsNullOrEmpty(condition.Product) == false)
                {
                    whereBuilder.Append(" AND ProductionID <= @Product");
                    parameters.Add(new DataParameter("Product", condition.Product));
                }

                if (string.IsNullOrEmpty(condition.ProductBatchNumber) == false)
                {
                    whereBuilder.Append(" AND BatchNumber <= @BatchNumber");
                    parameters.Add(new DataParameter("BatchNumber", condition.ProductBatchNumber));
                }

                if (string.IsNullOrEmpty(condition.MatIDCode) == false)
                {
                    //whereBuilder.Append(" AND BatchNumber <= @BatchNumber");
                    //parameters.Add(new DataParameter("BatchNumber", condition.ProductBatchNumber));
                }

                if (string.IsNullOrEmpty(condition.ProductIDCode) == false)
                {
                    whereBuilder.Append(" AND GoodBarCode <= @ProductIDCode");
                    parameters.Add(new DataParameter("ProductIDCode", condition.ProductIDCode));
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

        #region 获取质量追溯产品列表

        /// <summary>
        /// 获取质量追溯产品列表
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <returns>数据</returns>
        public DataTable GetExportData(QualityTraceCondition condition)
        {
            DataTable dt = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                //构成查询语句
                sql = this.GetQuerySql(condition, ref parameters);
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    dt = session.GetTable(sql, parameters.ToArray());
                    dt.TableName = "T_FP_GoodInfo";
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取产品基本信息

        /// <summary>
        /// 获取产品基本信息
        /// </summary>
        /// <param name="PID">产品主键</param>
        /// <returns>产品基本信息</returns>
        public TraceGood GetTraceGood(string PID)
        {
            string sql = null;
            TraceGood info = null;
            sql = @"select g.PID,p.PNAME as ProductName,p.SPECIFICATION,g.PLANDATE as ProduceDate,g.BatchNumber,
                    f.PNAME as FactoryName,l.PLNAME as LineName,pf.PNAME as FlowName,wg.PNAME as WorkGroupName
                    from T_FP_GoodInfo g
                    left outer join T_FP_PRODUCTINFO p on g.ProductionID=p.PID
                    left outer join T_FP_FACTORYINFO f on g.FACTORYPID=f.PID
                    left outer join T_FP_PRODUCTLINE l on g.PRID=l.PID
                    left outer join T_FP_PROCESSFLOW pf on p.PRID=pf.PID
                    left outer join T_FP_WORKGROUP wg on g.WOID=wg.PID
                    where g.PID=@PID
                    ";
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                info = session.Get<TraceGood>(sql, new DataParameter("PID",PID));
            }


            return info;
        }

        /// <summary>
        /// 根据批次号获取产品基本信息
        /// </summary>
        /// <param name="batchNumber">批次号</param>
        /// <returns>产品基本信息</returns>
        public TraceGood GetTraceGoodByBN(string batchNumber)
        {
            string sql = null;
            List<TraceGood> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            sql = @"select g.PID,p.PNAME as ProductName,p.SPECIFICATION,g.PLANDATE as ProduceDate,g.BatchNumber,
                    f.PNAME as FactoryName,l.PLNAME as LineName,pf.PNAME as FlowName,wg.PNAME as WorkGroupName,
                    p.ProductionAddress,p.Manufacturer,p.QualityPeriod,p.ProductionLicense,p.ProductStandard
                    from T_FP_GoodInfo g
                    left outer join T_FP_PRODUCTINFO p on g.ProductionID=p.PID
                    left outer join T_FP_FACTORYINFO f on g.FACTORYPID=f.PID
                    left outer join T_FP_PRODUCTLINE l on g.PRID=l.PID
                    left outer join T_FP_PROCESSFLOW pf on p.PRID=pf.PID
                    left outer join T_FP_WORKGROUP wg on g.WOID=wg.PID
                    where g.BatchNumber=@BatchNumber";
            parameters.Add(new DataParameter("BatchNumber", batchNumber));
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<TraceGood>(sql, parameters.ToArray()).ToList();
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

        #endregion

        #region 获取物料组成信息

        /// <summary>
        /// 获取物料组成信息
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <returns>物料组成信息</returns>
        public List<TraceMaterial> GetTraceMaterial(TraceGood condition)
        {
            List<TraceMaterial> list = null;
            string sql = @"select pt.PID,pt.MatBarCode,pt.MatID,ml.MatCode,ml.MatName,pt.CREATETIME,ml.ProductPlace
                            from T_FP_ProduceTrack pt
                            left outer join T_WH_Mat ml on pt.MatID=ml.ID
                            where BatchNumber = @BatchNumber and pt.WPID = 'start'
                            order by pt.CREATETIME";
            List<DataParameter> parameters = new List<DataParameter>();
            parameters.Add(new DataParameter("BatchNumber",condition.BatchNumber));

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<TraceMaterial>(sql, parameters.ToArray()).ToList();
            }

            return list;
        }

        #endregion 

        #region 获取加工工序信息

        /// <summary>
        /// 获取加工工序信息
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <returns>加工工序信息</returns>
        public List<TraceProcess> GetTraceProcess(TraceGood condition)
        {
            List<TraceProcess> list = null;
            string sql = @"select pt.PID,pi.PCODE as ProcessCode,pi.PNAME as ProcessName,pt.WorkingStartTime,pt.WorkingEndTime,
                            equ.ECODE as EquCode,equ.ENAME as EquName
                            from T_FP_ProduceTrack pt
                            left outer join T_FP_PROCESSINFO pi on pt.WPID=pi.PID
                            left outer join T_FP_EQUIPMENT equ on pt.EQUID=equ.PID
                            where BatchNumber = @BatchNumber and pt.WPID <> 'start' and STATUS = '1'
                            order by pi.PCODE";
            List<DataParameter> parameters = new List<DataParameter>();
            parameters.Add(new DataParameter("BatchNumber", condition.BatchNumber));

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<TraceProcess>(sql, parameters.ToArray()).ToList();
            }

            return list;
        }

        #endregion
    }
}
