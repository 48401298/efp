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
    /// 库存台账管理
    /// </summary>
    public class StockAccountDAL
    {
        #region 获取台账列表

        /// <summary>
        /// 获取台账列表
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>列表</returns>
        public List<WHMonthAccount> GetList(WHMonthAccount condition)
        {
            string sql = null;
            List<WHMonthAccount> list=null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = "select * from T_WH_MonthAccount where AccountYear = @AccountYear and AccountMonth = @AccountMonth";
                parameters.Add(new DataParameter("AccountYear", condition.AccountYear));
                parameters.Add(new DataParameter("AccountMonth", condition.AccountMonth));
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    list = session.GetList<WHMonthAccount>(sql,parameters.ToArray()).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取台账信息

        public WHMonthAccount Get(WHMonthAccount condition)
        {
            string sql = null;
            WHMonthAccount info = null;
            try
            {
                sql = @"select * from T_WH_MonthAccount 
                        where Warehouse = @Warehouse and MatID = @MatID and  AccountYear = @AccountYear and AccountMonth = @AccountMonth";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    info = session.Get<WHMonthAccount>(sql
                        , new DataParameter("Warehouse", condition.Warehouse)
                        , new DataParameter("MatID", condition.MatID)
                        , new DataParameter("AccountYear", condition.AccountYear)
                        , new DataParameter("AccountMonth", condition.AccountMonth));
                }
                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 插入

        public void Insert(WHMonthAccount info)
        {
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.Insert<WHMonthAccount>(info);
            }
        }

        #endregion

        #region 更新

        public void Update(WHMonthAccount info)
        {
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.Update<WHMonthAccount>(info);
            }
        }

        #endregion

        #region 库存台账查询

        /// <summary>
        /// 库存台账查询
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage QueryMonthAccount(WHMonthAccount condition, DataPage page)
        {
            DataTable dt = new DataTable();
            StringBuilder sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = new StringBuilder(@"select t1.ID,t2.MatCode,t2.MatName,t3.Description as ProductType,t4.Description as WarehouseName,t5.Description as UnitName   
                                                 ,t1.PrimeAmount,t1.InAmount,t1.OutAmount,t1.GainAmount,t1.LossAmount,t1.LateAmount                             
                                                        from T_WH_MonthAccount t1 
                                                        inner join T_WH_Mat t2 on t1.MatID=t2.ID
                                                        inner join T_WH_MatType t3 on t2.ProductType=t3.ID
                                                        inner join T_WH_Warehouse t4 on t1.Warehouse=t4.ID  
                                                        inner join T_WH_MatUnit t5 on t2.UnitCode=t5.ID    
                                                        where AccountYear = @AccountYear and AccountMonth = @AccountMonth ");

                page.KeyName = "ID";

                parameters.Add(new DataParameter("AccountYear", condition.AccountYear));
                parameters.Add(new DataParameter("AccountMonth", condition.AccountMonth));

                if (!string.IsNullOrEmpty(condition.Warehouse))
                {
                    sql.Append(" and t1.Warehouse = @Warehouse");
                    parameters.Add(new DataParameter("Warehouse", condition.Warehouse));
                }


                if (!string.IsNullOrEmpty(condition.ProductType))
                {
                    sql.Append(" and t4.ProductType = @ProductType");
                    parameters.Add(new DataParameter("ProductType", condition.ProductType));
                }

                if (!string.IsNullOrEmpty(condition.MatID))
                {
                    sql.Append(" and t1.MatID = @MatID");
                    parameters.Add(new DataParameter("MatID", condition.MatID));
                }

                if (!string.IsNullOrEmpty(condition.UserID))
                {
                    sql.Append(" and exists(select WarehouseID from T_WH_WHPower where UserID=@userID and WarehouseID=t1.Warehouse)");
                    parameters.Add(new DataParameter("userID", condition.UserID));
                }

                page.SortExpression = "WarehouseName,MatCode";                             

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<WHMonthAccount>(sql.ToString(), parameters.ToArray(), page);
                }

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
