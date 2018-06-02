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
    /// 出库管理
    /// </summary>
    public class OutStockDAL:BaseDAL
    {
        /// <summary>
        /// 适配器
        /// </summary>
        public IDataSession Session { get; set; }

        #region 判断出库单号是否存在

        /// <summary>
        /// 判断出库单号是否存在
        /// </summary>
        /// <param name="billNO">入库单号</param>
        /// <returns>true:存在;false:不存在</returns>
        public bool ExistsBillNO(string billNO)
        {
            string sql = null;
            object value = null;
            sql = "select Bill_NO from T_WH_OutStockBill where Bill_NO = @Bill_NO";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                value = session.ExecuteSqlScalar(sql, new DataParameter("Bill_NO", billNO));
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

        #region 获取出库信息

        /// <summary>
        /// 获取出库信息
        /// </summary>
        /// <param name="info">入库信息</param>
        /// <returns></returns>
        public OutStockBill GetInfo(OutStockBill info)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取数据
                    info = session.Get<OutStockBill>(info);

                    sql = @"select t1.*,t1.IDCode as MatBarCode,t2.MatCode,t2.MatName,t3.Description as SaveSite
                            ,t3.Description as SaveSiteName,t1.Remark,t4.Description as SpecCode,t5.Description as UnitCode,
                            t5.Description as MainUnitName,twms.UnitName,twms.Description as OutSpecName 
                            from T_WH_OutStockDetail t1
                            left outer join T_WH_Mat t2 on t1.MatID=t2.ID
                             left outer join T_WH_MatIDCode mic on t1.IDCode=mic.IDCode
                            left outer join T_WH_Site t3 on t1.SaveSite=t3.ID
                            left outer join T_WH_Spec t4 on t2.SpecCode=t4.ID
                            left outer join T_WH_MatUnit t5 on t2.UnitCode=t5.ID
                            left outer join T_WH_MatSpec twms on mic.MatSpec = twms.ID
                            where t1.BillID = @BillID";
                    parameters.Add(new DataParameter("BillID", info.ID));
                    info.Details = session.GetList<OutStockDetail>(sql, parameters.ToArray()).ToList();
                }

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion       
        
        #region 新增出库单信息

        /// <summary>
        /// 新增出库单信息
        /// </summary>
        /// <param name="info"></param>
        public void Insert(OutStockBill info)
        {
            try
            {
                Session.Insert<OutStockBill>(info);
                Session.Insert<OutStockDetail>(info.Details);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新出库单信息

        /// <summary>
        /// 更新出库单信息
        /// </summary>
        /// <param name="info"></param>
        public void Update(OutStockBill info)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.Update<OutStockBill>(info);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取最大出库单号

        /// <summary>
        /// 获取最大出库单号
        /// </summary>
        /// <returns></returns>
        public string GetMaxBillNO()
        {
            string no = null;
            object value = null;
            string sql = "select max(BillNO) from T_WH_OutStockBill Where BillDate >= @StartDate and BillDate <=@EndDate";

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

        #region 获取出库单列表

        /// <summary>
        /// 获取出库单列表
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

                sqlBuiler.Append(@"select t1.ID,t1.BillNO,t1.BillDate,whom.Description as OutStockMode,t1.Header,
                                    u3.USERNAME as HandlePerson,t1.Remark,t2.Description as WarehouseName,t3.Clientname as ClientName,u2.USERNAME as WHHeader
                                   from T_WH_OutStockBill t1
                                    inner join T_WH_Warehouse t2 on t1.Warehouse=t2.ID
                                    left outer join T_WH_Client t3 on t1.ClientCode=t3.ID
                                    left outer join T_WH_OutMode whom on t1.OutStockMode=whom.ID
                                    left outer join T_USER u2 on t1.WHHeader=u2.USERID
                                    left outer join T_USER u3 on t1.HandlePerson=u3.USERID
                                    where 1=1");
                if (!string.IsNullOrEmpty(condition.BillNO))
                {
                    sqlBuiler.Append(" and t1.BillNO like @BillNO");
                    parameters.Add(new DataParameter("BillNO", "%" + condition.BillNO + "%"));
                }

                if (!string.IsNullOrEmpty(condition.ClientCode))
                {
                    sqlBuiler.Append(" and t1.ClientCode = @ClientCode");
                    parameters.Add(new DataParameter("ClientCode", condition.ClientCode));
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
                page.KeyName = "Code";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "BillDate DESC";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<OutStockBill>(sqlBuiler.ToString(), parameters.ToArray(), page);
                }

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取出库单浏览信息

        /// <summary>
        /// 获取出库单浏览信息
        /// </summary>
        /// <param name="info">获取条件</param>
        /// <returns>出库单浏览信息</returns>
        public OutStockBillView GetViewInfo(OutStockBillView info)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                //获取基本信息
                sql = @"select t1.ID,t1.BillNO,t1.BillDate,t2.Clientname as Client,t3.Description as OutStockMode,
                            t4.Description as Warehouse,u1.USERNAME as Header,u2.USERNAME as WHHeader,
                            u3.USERNAME as HandlePerson,t1.Remark 
                        from T_WH_OutStockBill t1
                        left outer join T_WH_Client t2 on t1.ClientCode=t2.ID
                        left outer join T_WH_OutMode t3 on t1.OutStockMode=t3.ID
                        left outer join T_WH_Warehouse t4 on t1.Warehouse=t4.ID
                        left outer join T_USER u1 on t1.Header=u1.USERID
                        left outer join T_USER u2 on t1.WHHeader=u2.USERID
                        left outer join T_USER u3 on t1.HandlePerson=u3.USERID
                        where t1.ID=?ID";

                info = session.Get<OutStockBillView>(sql, new DataParameter("ID", info.ID));

                //获取明细信息
                sql = @"select t1.ID,t1.Seq,t1.IDCode,t1.MatID,t2.MatCode as MatCode,t2.MatName as MatName,t1.MainUnitAmount,
                            t1.OutAmount,t1.OutPrice,t1.OutSum,t3.Description as SaveSite,t1.Remark,twms.Description as SpecCode,
                            twms.UnitName,t5.Description as MainUnitName
                        from T_WH_OutStockDetail t1                        
                        left outer join T_WH_Mat t2 on t1.MatID=t2.ID
                        left outer join T_WH_MatIDCode mic on t1.IDCode=mic.IDCode
                        left outer join T_WH_MatSpec twms on mic.MatSpec = twms.ID
                        left outer join T_WH_Site t3 on t1.SaveSite=t3.ID
                        left outer join T_WH_Spec t4 on t2.SpecCode=t4.ID
                        left outer join T_WH_MatUnit t5 on t2.UnitCode=t5.ID
                        where t1.BillID=@BillID";
                parameters.Add(new DataParameter("BillID", info.ID));

                info.Details = session.GetList<OutStockDetailView>(sql, parameters.ToArray()).ToList();
            }

            return info;
        }

        #endregion
    }
}
