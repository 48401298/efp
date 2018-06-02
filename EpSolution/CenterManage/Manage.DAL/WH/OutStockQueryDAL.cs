using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data;
using LAF.Data.Attributes;
using Manage.Entity.WH;

namespace Manage.DAL.WH
{
    /// <summary>
    /// 出库查询
    /// </summary>
    public class OutStockQueryDAL
    {
        #region 获取出库查询结果列表

        /// <summary>
        /// 获取出库查询结果列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(OutStockBill condition, DataPage page)
        {
            DataTable dt = new DataTable();
            List<DataParameter> parameters = new List<DataParameter>();
            StringBuilder sqlBuiler = new StringBuilder();
            try
            {
                page.KeyName = "ID";

                sqlBuiler.Append(@"select t2.ID+t3.ID as ID,t2.ID as WareHouseID,t2.Description as WarehouseName,t4.Description as ProductType
                                   ,t3.ID as MatID,t3.MatCode,t3.MatName,t5.Description as UnitName
                                   ,t6.Description as SpecName,twhisd.MainUnitAmount as Amount
                                   from T_WH_OutStockBill t1
                                    inner join T_WH_OutStockDetail twhisd on t1.ID = twhisd.BillID
                                    inner join T_WH_Warehouse t2 on t1.Warehouse=t2.ID
                                    left outer join T_WH_Mat t3 on twhisd.MatID=t3.ID
                                    left outer join T_WH_MatType t4 on t3.ProductType=t4.ID
                                    left outer join T_WH_MatUnit t5 on t3.UnitCode=t5.ID
                                    left outer join T_WH_Spec t6 on t3.SpecCode=t6.ID
                                    where 1=1");
                if (!string.IsNullOrEmpty(condition.BillNO))
                {
                    sqlBuiler.Append(" and t1.BillNO like @BillNO");
                    parameters.Add(new DataParameter("BillNO", "%" + condition.BillNO + "%"));
                }                

                if (!string.IsNullOrEmpty(condition.Warehouse))
                {
                    sqlBuiler.Append(" and t1.Warehouse = @Warehouse");
                    parameters.Add(new DataParameter("Warehouse", condition.Warehouse));
                }

                if (!string.IsNullOrEmpty(condition.OutStockMode))
                {
                    sqlBuiler.Append(" and t1.OutStockMode = @OutStockMode");
                    parameters.Add(new DataParameter("OutStockMode", condition.OutStockMode));
                }

                if (!string.IsNullOrEmpty(condition.StartDate))
                {
                    sqlBuiler.Append(" and t1.BillDate >= @StartDate");
                    parameters.Add(new DataParameter("StartDate", DateTime.Parse(condition.StartDate)));
                }

                if (!string.IsNullOrEmpty(condition.EndDate))
                {
                    sqlBuiler.Append(" and t1.BillDate < @EndDate");
                    parameters.Add(new DataParameter("EndDate", DateTime.Parse(condition.EndDate + " 23:59:59")));
                }
                if (!string.IsNullOrEmpty(condition.ProductType))
                {
                    sqlBuiler.Append(" and t3.ProductType = @ProductType");
                    parameters.Add(new DataParameter("ProductType", condition.ProductType));
                }
                if (!string.IsNullOrEmpty(condition.MatID))
                {
                    sqlBuiler.Append(" and twhisd.MatID = @MatID");
                    parameters.Add(new DataParameter("MatID", condition.MatID));
                }
                if (!string.IsNullOrEmpty(condition.CREATEUSER))
                {
                    sqlBuiler.Append(" and exists(select WarehouseID from T_WH_WHPower where UserID=@userID and WarehouseID=t1.Warehouse)");
                    parameters.Add(new DataParameter("userID", condition.CREATEUSER));
                }
                sqlBuiler.Append(@" group by t2.ID,t2.Description,t4.Description
                                   ,t3.ID,t3.MatCode,t3.MatName,t5.Description,t6.Description");
                //分页关键字段及排序
                page.KeyName = "ID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "MatCode DESC";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<InStockQueryResult>(sqlBuiler.ToString(), parameters.ToArray(), page);
                }

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取出库查询结果列表

        /// <summary>
        /// 获取出库查询结果列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetOutMatDetails(OutStockBill condition, DataPage page)
        {
            DataTable dt = new DataTable();
            List<DataParameter> parameters = new List<DataParameter>();
            StringBuilder sqlBuiler = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                page.KeyName = "ID";
                sqlBuiler.Append(@"select t1.ID,t1.Seq,t1.IDCode as MatBarCode,t1.MatID,t2.MatCode as MatCode,t2.MatName as MatName,
                            t1.OutAmount as Amount,t1.MainUnitAmount,t1.OutPrice,t1.OutSum,t3.Description as SaveSite,t1.Remark,wh.Description as WarehouseName,
                            twms.UnitName,t1.MatSpec,isb.BillDate as OutDate,t5.Description as MainUnitName
                            from T_WH_OutStockDetail t1
                            inner join T_WH_OutStockBill isb on t1.BillID=isb.ID
                            left outer join T_WH_MatIDCode mic on t1.IDCode=mic.IDCode
                            left outer join T_WH_MatSpec twms on mic.MatSpec = twms.ID
                            left outer join T_WH_Mat t2 on t1.MatID=t2.ID
                            left outer join T_WH_Site t3 on t1.SaveSite=t3.ID
                            left join T_WH_Warehouse wh on t3.whid=wh.ID
                            left outer join T_WH_MatUnit t5 on t2.UnitCode=t5.ID
                           ");

              
                if (!string.IsNullOrEmpty(condition.Warehouse))
                {
                    whereBuilder.Append(" and isb.Warehouse = @Warehouse");
                    parameters.Add(new DataParameter("Warehouse", condition.Warehouse));
                }

                if (!string.IsNullOrEmpty(condition.StartDate))
                {
                    whereBuilder.Append(" and isb.BillDate >= @StartDate");
                    parameters.Add(new DataParameter("StartDate", DateTime.Parse(condition.StartDate)));
                }

                if (!string.IsNullOrEmpty(condition.EndDate))
                {
                    whereBuilder.Append(" and isb.BillDate < @EndDate");
                    parameters.Add(new DataParameter("EndDate", DateTime.Parse(condition.EndDate + " 23:59:59")));
                }
                if (!string.IsNullOrEmpty(condition.MatID))
                {
                    whereBuilder.Append(" and t1.MatID = @MatID");
                    parameters.Add(new DataParameter("MatID", condition.MatID));
                }

                if (whereBuilder.Length > 0)
                    sqlBuiler.Append(" where "+whereBuilder.ToString().Substring(4));

                //分页关键字段及排序
                page.KeyName = "ID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "BillDate ASC";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<OutStockQueryResult>(sqlBuiler.ToString(), parameters.ToArray(), page);
                }

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region 获取出库数量列表

        public List<OutStockQueryResult> GetOutAmountList(OutStockQueryResult condition)
        {
            List<OutStockQueryResult> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            sql = @"select t1.Warehouse as WarehouseID,t2.MatID,sum(t2.MainUnitAmount) as Amount
                    from T_WH_OutStockBill t1
                    inner join T_WH_OutStockDetail t2 on t1.ID = t2.BillID
                    where t1.BillDate >= @StartDate and t1.BillDate < @EndDate
                    group by t1.Warehouse,t2.MatID";

            parameters.Add(new DataParameter("StartDate", DateTime.Parse(condition.StartDate)));
            parameters.Add(new DataParameter("EndDate", DateTime.Parse(condition.EndDate)));
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<OutStockQueryResult>(sql, parameters.ToArray()).ToList();
            }

            return list;
        }

        #endregion
    }
}
