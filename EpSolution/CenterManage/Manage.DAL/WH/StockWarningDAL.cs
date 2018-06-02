using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.WH;
using System.Data;

namespace Manage.DAL.WH
{
    /// <summary>
    /// 库存预警查询
    /// </summary>
    public class StockWarningDAL
    {
        #region 获取库存预警列表

        /// <summary>
        /// 获取库存预警列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="page">分页信息</param>
        /// <returns>库存预警列表</returns>
        public DataPage GetList(WHMatAmount condition, DataPage page)
        {
            DataTable dt = new DataTable();
            StringBuilder sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = new StringBuilder(@"select t1.MatID,t3.Description as ProductType,t1.StockAmount as StockAmount
                                                        ,t2.Description as WarehouseName,t4.MatCode,t4.MatName,t5.Description as MainUnitName,
                                                        t6.MaxAmount,t6.MinAmount       
                                                        from v_wh_matamount t1 
                                                        inner join T_WH_Warehouse t2 on t1.Warehouse=t2.ID
                                                        inner join T_WH_Mat t4 on t1.MatID=t4.ID
                                                        inner join T_WH_MatType t3 on t4.ProductType=t3.ID
                                                        inner join T_WH_MatUnit t5 on t4.UnitCode=t5.ID
                                                        inner join T_WH_StockLimit t6 on t1.Warehouse = t6.Warehouse and t1.MatID =  t6.MatID
                                                        where (t1.StockAmount >= t6.MaxAmount or t1.StockAmount <= t6.MinAmount)");

                page.KeyName = "ID";

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

                if (!string.IsNullOrEmpty(condition.CreateUser))
                {
                    sql.Append(" and exists(select WarehouseID from T_WH_WHPower where UserID=@userID and WarehouseID=t1.Warehouse)");
                    parameters.Add(new DataParameter("userID", condition.CreateUser));
                }

                page.SortExpression = "WarehouseName,MatCode";           

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<StockWarningResult>(sql.ToString(), parameters.ToArray(), page);
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
