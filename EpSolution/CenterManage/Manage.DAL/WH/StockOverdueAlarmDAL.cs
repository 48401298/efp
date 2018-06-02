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
    /// 库存过期预警查询
    /// </summary>
    public class StockOverdueAlarmDAL:BaseDAL
    {
        /// <summary>
        /// 获取过期预警列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(WHMatAmount condition, DataPage page)
        {
            DataTable dt = new DataTable();
            StringBuilder sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = new StringBuilder(@"select t1.MatBarCode,t1.MatID,t4.MatCode,t4.MatName,t3.Description as ProductType,t1.ProductAmount,t1.ProductSum
                                                        ,t2.Description as WarehouseName,t5.Description as SaveSite,twms.UnitName,twms.Description as MatSpec,t1.CreateTime,
                                                        t1.ProduceDate,t4.QualityPeriod                                
                                                        from T_WH_MatAmount t1 
                                                        left outer join T_WH_Warehouse t2 on t1.Warehouse=t2.ID  
                                                        left outer join T_WH_MatSpec twms on t1.Unit=twms.ID                                                      
                                                        left outer join T_WH_Mat t4 on t1.MatID=t4.ID
                                                        left outer join T_WH_MatType t3 on t4.ProductType=t3.ID
                                                        left outer join T_WH_Site t5 on t1.SaveSite = t5.ID
                                                        where (t4.QualityPeriod-DATEDIFF(NOW(),t1.ProduceDate))<=t4.OverdueAlarmDay ");

                page.KeyName = "ID";

                if (!string.IsNullOrEmpty(condition.Warehouse))
                {
                    sql.Append(" and t1.Warehouse = @Warehouse");
                    parameters.Add(new DataParameter("Warehouse", condition.Warehouse));
                }                

                if (!string.IsNullOrEmpty(condition.SaveSite))
                {
                    sql.Append(" and t1.SaveSite = @SaveSite");
                    parameters.Add(new DataParameter("SaveSite", condition.SaveSite));
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

                if (!string.IsNullOrEmpty(condition.MatCode))
                {
                    sql.Append(" and t4.MatCode like @MatCode");
                    parameters.Add(new DataParameter("MatCode", "%" + condition.MatCode + "%"));
                }

                if (!string.IsNullOrEmpty(condition.MatName))
                {
                    sql.Append(" and t4.MatName like @MatName");
                    parameters.Add(new DataParameter("MatName", "%" + condition.MatName + "%"));
                }

                if (!string.IsNullOrEmpty(condition.CreateUser))
                {
                    sql.Append(" and exists(select WarehouseID from T_WH_WHPower where UserID=@userID and WarehouseID=t1.Warehouse)");
                    parameters.Add(new DataParameter("userID", condition.CreateUser));
                }

                page.SortExpression = "WarehouseName,SaveSite,MatCode,MatBarCode,CreateTime";
                              

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<WHMatAmount>(sql.ToString(), parameters.ToArray(), page);
                }

                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
