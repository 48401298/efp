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
    /// 入库管理
    /// </summary>
    public class InStockDAL:BaseDAL
    {
        /// <summary>
        /// 适配器
        /// </summary>
        public IDataSession Session { get; set; }

        #region 判断入库单号是否存在

        /// <summary>
        /// 判断入库单号是否存在
        /// </summary>
        /// <param name="billNO">入库单号</param>
        /// <returns>true:存在;false:不存在</returns>
        public bool ExistsBillNO(string billNO)
        {
            string sql = null;
            object value = null;
            sql = "select BillNO from T_WH_InStockBill where BillNO = @BillNO";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                value = session.ExecuteSqlScalar(sql, new DataParameter("BillNO", billNO));
            }
            if (value == null || value == System.DBNull.Value)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        #endregion

        #region 获取入库单信息

        /// <summary>
        /// 获取入库单信息
        /// </summary>
        /// <param name="info">入库信息</param>
        /// <returns></returns>
        public InStockBill GetInfo(InStockBill info)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取数据
                    info = session.Get<InStockBill>(info);

                    //获取明细信息
                    sql = @"select t1.ID,t1.Seq,t1.MatBarCode,t1.MatID,t2.MatCode as MatCode,t2.MatName as MatName,t1.ProduceDate,
                            t1.InAmount,t1.InPrice,t1.InSum,t3.Description as SaveSite,t3.Description as SaveSiteName,t1.Remark,t4.Description as SpecCode,
                            t5.Description as MainUnitName,twms.UnitName,twms.Description as InSpecName
                        from T_WH_InStockDetail t1
                        left outer join T_WH_Mat t2 on t1.MatID=t2.ID
                        left outer join T_WH_MatIDCode mic on t1.MatBarCode=mic.IDCode
                        left outer join T_WH_Site t3 on t1.SaveSite=t3.ID
                        left outer join T_WH_Spec t4 on t2.SpecCode=t4.ID
                        left outer join T_WH_MatUnit t5 on t2.UnitCode=t5.ID
                        left outer join T_WH_MatSpec twms on mic.MatSpec = twms.ID
                        where t1.BillID=@BillID";
                    parameters.Add(new DataParameter("BillID", info.ID));
                    info.Details = session.GetList<InStockDetail>(sql, parameters.ToArray()).ToList();
                }

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion        

        #region 新增入库单信息

        /// <summary>
        /// 新增入库单信息
        /// </summary>
        /// <param name="info"></param>
        public void Insert(InStockBill info)
        {
            try
            {
                Session.Insert<InStockBill>(info);
                Session.Insert<InStockDetail>(info.Details);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新入库单信息

        /// <summary>
        /// 更新入库单信息
        /// </summary>
        /// <param name="info"></param>
        public void Update(InStockBill info)
        {
            try
            {                
                Session.Update<InStockBill>(info);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取最大入库单号

        /// <summary>
        /// 获取最大入库单号
        /// </summary>
        /// <returns></returns>
        public string GetMaxBillNO()
        {
            string no = null;
            object value = null;
            string sql = "select max(BillNO) from T_WH_InStockBill Where BillDate >= @StartDate and BillDate <=@EndDate";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                value = session.ExecuteSqlScalar(sql
                    , new DataParameter("StartDate",DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                    , new DataParameter("EndDate", DateTime.Now));
            }

            if (value != null && value != System.DBNull.Value)
                no = value.ToString();

            return no;
        }

        #endregion

        #region 获取入库单列表

        /// <summary>
        /// 获取入库单列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(InStockBill condition, DataPage page)
        {
            DataTable dt = new DataTable();
            List<DataParameter> parameters = new List<DataParameter>();
            StringBuilder sqlBuiler = new StringBuilder();
            try
            {
                page.KeyName = "ID";

                sqlBuiler.Append(@"select t1.ID,t1.BillNO,t1.BillDate,t4.Description as InStockMode,t1.DeliveryPerson,
                                    u3.USERNAME as WHHeader,t1.Remark,t2.Description as WarehouseName,t3.ProviderName,u2.USERNAME as Receiver
                                   from T_WH_InStockBill t1
                                    inner join T_WH_Warehouse t2 on t1.Warehouse=t2.ID
                                    left outer join T_WH_Provider t3 on t1.ProviderID=t3.ID
                                    left outer join T_USER u2 on t1.Receiver=u2.USERID
                                    left outer join T_USER u3 on t1.WHHeader=u3.USERID
                                    left outer join T_WH_InMode t4 on t1.InStockMode=t4.ID
                                    where 1=1");
                if (!string.IsNullOrEmpty(condition.BillNO))
                {
                    sqlBuiler.Append(" and t1.BillNO like @BillNO");
                    parameters.Add(new DataParameter("BillNO", "%" + condition.BillNO + "%"));
                }

                if (!string.IsNullOrEmpty(condition.ProviderID))
                {
                    sqlBuiler.Append(" and t1.ProviderID = @ProviderID");
                    parameters.Add(new DataParameter("ProviderID", condition.ProviderID));
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
                    sqlBuiler.Append(" and exists(select WarehouseID from T_WH_WHPower where UserID=@userID and WarehouseID=t1.Warehouse)");
                    parameters.Add(new DataParameter("userID", condition.CREATEUSER));
                }
                //分页关键字段及排序
                page.KeyName = "ID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "t1.BillDate DESC";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<InStockBill>(sqlBuiler.ToString(), parameters.ToArray(), page);
                }

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取入库单浏览信息

        /// <summary>
        /// 获取入库单浏览信息
        /// </summary>
        /// <param name="info">获取条件</param>
        /// <returns>入库单浏览信息</returns>
        public InStockBillView GetViewInfo(InStockBillView info)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                //获取基本信息
                sql = @"select t1.ID,t1.BillNO,t1.BillDate,t2.ProviderName as Provider,t3.Description as InStockMode,
                            t4.Description as Warehouse,u1.USERNAME as DeliveryPerson,u2.USERNAME as Receiver,
                            u3.USERNAME as WHHeader,t1.Remark 
                        from T_WH_InStockBill t1
                        left outer join T_WH_Provider t2 on t1.ProviderID=t2.ID
                        left outer join T_WH_InMode t3 on t1.InStockMode=t3.ID
                        left outer join T_WH_Warehouse t4 on t1.Warehouse=t4.ID
                        left outer join T_USER u1 on t1.DeliveryPerson=u1.USERID
                        left outer join T_USER u2 on t1.Receiver=u2.USERID
                        left outer join T_USER u3 on t1.WHHeader=u3.USERID
                        where t1.ID=?ID";

                info=session.Get<InStockBillView>(sql,new DataParameter("ID",info.ID));

                //获取明细信息
                sql = @"select t1.ID,t1.Seq,t1.MatBarCode,t1.MatID,t2.MatCode as MatCode,t2.MatName as MatName,t1.ProduceDate,
                            t1.InAmount,t1.InPrice,t1.InSum,t3.Description as SaveSite,t1.Remark,
                            twms.UnitName,t1.MatSpec,t5.Description as MainUnitName
                        from T_WH_InStockDetail t1
                        left outer join T_WH_Mat t2 on t1.MatID=t2.ID
                        left outer join T_WH_MatIDCode mic on t1.MatBarCode=mic.IDCode
                        left outer join T_WH_MatSpec twms on mic.MatSpec = twms.ID
                        left outer join T_WH_Site t3 on t1.SaveSite=t3.ID
                        left outer join T_WH_MatUnit t5 on t2.UnitCode=t5.ID
                        where t1.BillID=@BillID";
                parameters.Add(new DataParameter("BillID", info.ID));

                info.Details = session.GetList<InStockDetailView>(sql, parameters.ToArray()).ToList();
            }

            return info;
        }

        #endregion
    }
}
