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
    /// 库存盘点
    /// </summary>
    public class CheckStockDAL:BaseDAL
    {
        public IDataSession Session { get; set; }

        #region 生成盘点单

        /// <summary>
        /// 生成盘点单
        /// </summary>
        /// <param name="condition">生成条件</param>
        /// <returns>盘点单</returns>
        public CheckStockBill BuildBill(CheckStockBill condition)
        {
            CheckStockBill info = new CheckStockBill();
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();

            sql = @"select t1.SaveSite,t2.Description as SaveSiteName,t1.MatBarCode as IDCode,t1.MatID,
                    t3.MatCode,t3.MatName,t1.Unit,t4.UnitName,t1.ProductAmount as StockAmount
                    from T_WH_MatAmount t1
                    left outer join T_WH_Site t2 on t1.SaveSite=t2.ID
                    left outer join T_WH_Mat t3 on t1.MatID=t3.ID
                    left outer join T_WH_MatSpec t4 on t1.Unit=t4.ID
                    where t1.Warehouse = @Warehouse";

            parameters.Add(new DataParameter("Warehouse",condition.Warehouse));

            if (string.IsNullOrEmpty(condition.AreaID) == false)
            {
                sql += " and t2.AreaID = @AreaID";
                parameters.Add(new DataParameter("AreaID", condition.AreaID));
            }

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                info.Details = session.GetList<CheckStockDetail>(sql, parameters.ToArray()).ToList();
            }

            return info;
        }

        #endregion

        #region 新增盘点单

        /// <summary>
        /// 新增盘点单
        /// </summary>
        /// <param name="info"></param>
        public void Insert(CheckStockBill info)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    session.Insert<CheckStockBill>(info);
                    session.Insert<CheckStockDetail>(info.Details);
                    session.CommitTs();
                }
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
        public int Delete(CheckStockBill info)
        {
            int count = 0;
            string sql = "";
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    count = session.Delete<CheckStockBill>(info);

                    sql = "delete from T_WH_CheckStockDetail where BillID = @BillID";
                    session.ExecuteSql(sql, new DataParameter("BillID",info.ID));

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

        #region 更新盘点单

        /// <summary>
        /// 更新盘点单
        /// </summary>
        /// <param name="info"></param>
        public void Update(CheckStockBill info)
        {
            string sql = null;
            try
            {
                Session.Update<CheckStockBill>(info);

                sql = "delete from T_WH_CheckStockDetail where BillID = @BillID";
                Session.ExecuteSql(sql, new DataParameter("BillID", info.ID));

                Session.Insert<CheckStockDetail>(info.Details);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取盘点单信息

        /// <summary>
        /// 获取盘点单信息
        /// </summary>
        /// <param name="info">入库信息</param>
        /// <returns></returns>
        public CheckStockBill GetInfo(CheckStockBill info)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取数据
                    sql = @"select t1.*,t3.Description as AreaName,u3.USERNAME as CheckHeaderName,t2.Description as WarehouseName
                            from T_WH_CheckStock t1
                            inner join T_WH_Warehouse t2 on t1.Warehouse=t2.ID
                            left outer join T_WH_AREA t3 on t1.AreaID=t3.ID
                            left outer join T_USER u3 on t1.CheckHeader=u3.USERID
                            where t1.ID = @ID";
                    info = session.Get<CheckStockBill>(sql,new DataParameter("ID",info.ID));

                    //获取明细信息
                    sql = @"select t1.ID,t1.Seq,t1.IDCode,t1.MatID,t1.SaveSite,t2.MatCode as MatCode,t2.MatName as MatName,
                            t1.StockAmount,t1.FactAmount,t1.ProfitAmount,t1.LossAmount,t1.Remark
                            ,t3.Description as SaveSiteName,t1.Unit,t4.Description as UnitName
                        from T_WH_CheckStockDetail t1
                        left outer join T_WH_Mat t2 on t1.MatID=t2.ID
                        left outer join T_WH_Site t3 on t1.SaveSite=t3.ID
                        left outer join T_WH_MatUnit t4 on t1.Unit=t4.ID
                        where t1.BillID=?BillID";
                    parameters.Add(new DataParameter("BillID", info.ID));
                    info.Details = session.GetList<CheckStockDetail>(sql, parameters.ToArray()).ToList();
                }

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion    
    
        #region 获取盘点单列表

        /// <summary>
        /// 获取盘点单列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(CheckStockBill condition, DataPage page)
        {
            DataTable dt = new DataTable();
            List<DataParameter> parameters = new List<DataParameter>();
            StringBuilder sqlBuiler = new StringBuilder();
            try
            {
                page.KeyName = "ID";

                sqlBuiler.Append(@"select t1.ID,t1.BillNO,t1.BillDate,t3.Description as AreaName,
                                    u3.USERNAME as CheckHeader,t1.Remark,t2.Description as WarehouseName,t1.IsConfirm
                                   from T_WH_CheckStock t1
                                    inner join T_WH_Warehouse t2 on t1.Warehouse=t2.ID
                                    left outer join T_WH_AREA t3 on t1.AreaID=t3.ID
                                    left outer join T_USER u3 on t1.CheckHeader=u3.USERID
                                    where 1=1");
                if (!string.IsNullOrEmpty(condition.BillNO))
                {
                    sqlBuiler.Append(" and t1.BillNO like @BillNO");
                    parameters.Add(new DataParameter("BillNO", "%" + condition.BillNO + "%"));
                }

                if (!string.IsNullOrEmpty(condition.AreaID))
                {
                    sqlBuiler.Append(" and t1.AreaID = @AreaID");
                    parameters.Add(new DataParameter("ProviderID", condition.AreaID));
                }

                if (!string.IsNullOrEmpty(condition.Warehouse))
                {
                    sqlBuiler.Append(" and t1.Warehouse = @Warehouse");
                    parameters.Add(new DataParameter("Warehouse", condition.Warehouse));
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
                if (!string.IsNullOrEmpty(condition.CREATEUSER))
                {
                    sqlBuiler.Append(" and t1.CREATEUSER = @CREATEUSER");
                    parameters.Add(new DataParameter("CREATEUSER", condition.CREATEUSER));
                }
                //分页关键字段及排序
                page.KeyName = "ID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "t1.BillDate DESC";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<CheckStockBill>(sqlBuiler.ToString(), parameters.ToArray(), page);
                }

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取最大盘点单号

        /// <summary>
        /// 获取最大入库单号
        /// </summary>
        /// <returns></returns>
        public string GetMaxBillNO()
        {
            string no = null;
            object value = null;
            string sql = "select max(BillNO) from T_WH_CheckStock Where BillDate >= @StartDate and BillDate <=@EndDate";

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

        #region 获取盘点数量列表

        public List<CheckStockQueryResult> GetCheckAmountList(CheckStockQueryResult condition)
        {
            List<CheckStockQueryResult> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            sql = @"select t1.Warehouse as WarehouseID,t2.MatID,sum(t2.ProfitAmount) as ProfitAmount,sum(t2.LossAmount) as LossAmount
                    from T_WH_CheckStock t1
                    inner join T_WH_CheckStockDetail t2 on t1.ID = t2.BillID
                    where BillDate >= @StartDate and BillDate < @EndDate
                    group by t1.Warehouse,t2.MatID";

            parameters.Add(new DataParameter("StartDate", DateTime.Parse(condition.StartDate)));
            parameters.Add(new DataParameter("EndDate", DateTime.Parse(condition.EndDate)));
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<CheckStockQueryResult>(sql, parameters.ToArray()).ToList();
            }

            return list;
        }

        #endregion
    }
}
