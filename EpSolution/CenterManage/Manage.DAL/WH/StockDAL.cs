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
    /// 库存管理
    /// </summary>
    public class StockDAL:BaseDAL
    {
        /// <summary>
        /// 数据库会话
        /// </summary>
        public IDataSession Session { get; set; }
        #region 入库

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="record">入库记录</param>
        public void InStock(WHMatAmount record)
        {
            WHMatAmount info = this.GetStock(record);

            if (info != null)
            {
                //已有库存
                info.ProductAmount += record.ProductAmount;
                info.ProductSum += record.ProductSum;
                info.ProductPrice = info.ProductSum / info.ProductAmount;
                info.UpdateTime = DateTime.Now;

                Session.Update<WHMatAmount>(info);
            }
            else
            {
                //无库存
                info = new WHMatAmount();
                info.ProduceDate = record.ProduceDate;
                info.ID = Guid.NewGuid().ToString();
                info.MatBarCode = record.MatBarCode;
                info.Warehouse = record.Warehouse;
                info.SaveSite = record.SaveSite;
                info.MatID = record.MatID;
                info.ProductAmount = record.ProductAmount;
                info.ProductSum = record.ProductSum;
                info.ProductPrice = record.ProductPrice;
                info.Unit = record.Unit;
                info.MainAmount = record.MainAmount;
                info.CreateUser = record.CreateUser;
                info.CreateTime = DateTime.Now;
                info.UpdateTime = DateTime.Now;


                Session.Insert<WHMatAmount>(info);
            }
        }

        #endregion 

        #region 更新库存

        public void Update(WHMatAmount info)
        {
            Session.Update<WHMatAmount>(info);
        }

        #endregion

        #region 出库

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="record">出库记录</param>
        public string OutStock(WHMatAmount record)
        {
            WHMatAmount info = this.GetStock(record);

            string result = "";

            if (info != null)
            {
                if (info.ProductAmount < record.ProductAmount || info.MainAmount < record.MainAmount)
                {
                    //库存数量不足
                    return "库存数量不足";
                }
                
                info.MainAmount -= record.MainAmount;
                info.ProductAmount -= record.ProductAmount;

                if (info.ProductAmount == 0 || info.MainAmount==0)
                {
                    info.ProductSum = 0;
                    info.ProductPrice = 0;
                    Session.Delete<WHMatAmount>(info);
                }
                else
                {
                    info.ProductSum = info.ProductPrice*info.ProductAmount;
                    info.UpdateTime = DateTime.Now;
                    Session.Update<WHMatAmount>(info);
                }   
            }
            else
            {
                return "无库存，无法出库";
            }

            return result;
        }

        #endregion

        #region 获取库存信息

        /// <summary>
        /// 判断条码是否已入库
        /// </summary>
        /// <param name="matBarCode"></param>
        /// <returns></returns>
        public bool ExistsMatBarCode(string matBarCode)
        {
            bool exists = false;
            string sql = null;
            int count = 0;


            sql = "select count(*) from T_WH_MatAmount where MatBarCode = @MatBarCode";
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                count = int.Parse(session.ExecuteSqlScalar(sql, new DataParameter("MatBarCode", matBarCode)).ToString());
            }

            if (count > 0)
                exists = true;

            return exists;
        }

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public WHMatAmount GetStock(WHMatAmount condition)
        {
            string sql = null;
            WHMatAmount info = null;

            sql = "select * from T_WH_MatAmount where Warehouse = @Warehouse and SaveSite = @SaveSite and MatID = @MatID and MatBarCode = @MatBarCode";

            if (Session != null)
            {
                info = Session.Get<WHMatAmount>(sql
                    , new DataParameter("Warehouse", condition.Warehouse)
                    , new DataParameter("SaveSite", condition.SaveSite)
                    , new DataParameter("MatID", condition.MatID)
                     , new DataParameter("MatBarCode", condition.MatBarCode));
            }
            else
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    info = session.Get<WHMatAmount>(sql
                        , new DataParameter("Warehouse", condition.Warehouse)
                        , new DataParameter("SaveSite", condition.SaveSite)
                        , new DataParameter("MatID", condition.MatID)
                        , new DataParameter("MatBarCode", condition.MatBarCode));
                }
            }

            return info;
        }

        /// <summary>
        /// 获取根据条码库存信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public WHMatAmount GetStockByBarCode(string matBarCode)
        {
            string sql = null;
            WHMatAmount info = null;

            sql = @"select t1.*,twms.UnitName,t2.MatCode,t2.MatName,twms.Description as MatSpec
                    from T_WH_MatAmount t1
                    left outer join T_WH_MatSpec twms on t1.Unit = twms.ID
                    left outer join T_WH_Mat t2 on t1.MatID=t2.ID
                    where t1.MatBarCode = @MatBarCode";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                info = session.Get<WHMatAmount>(sql
                    , new DataParameter("MatBarCode", matBarCode));
            }

            return info;
        }

        #endregion        

        #region 获取库存列表

        /// <summary>
        /// 获取库存列表
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
                if (condition.IsDetail == false)
                {
                    sql = new StringBuilder(@"select t1.MatID,t3.Description as ProductType,sum(t1.MainAmount) as ProductAmount,sum(t1.ProductSum) as ProductSum
                                                        ,t2.Description as WarehouseName,t4.MatCode,t4.MatName,twmu.Description as UnitName,'' as MatSpec       
                                                        from T_WH_MatAmount t1 
                                                        left outer join T_WH_Warehouse t2 on t1.Warehouse=t2.ID
                                                        left outer join T_WH_Mat t4 on t1.MatID=t4.ID
                                                        left outer join T_WH_MatType t3 on t4.ProductType=t3.ID
                                                        left outer join T_WH_MatUnit twmu on t4.UnitCode=twmu.ID
                                                        where 1=1 ");
                }
                else
                {
                    sql = new StringBuilder(@"select t1.MatBarCode,t1.MatID,t4.MatCode,t4.MatName,t3.Description as ProductType,t1.ProductAmount,t1.MainAmount as MainAmount,t1.ProductSum
                                                        ,t2.Description as WarehouseName,t5.Description as SaveSite,twms.UnitName,twmu.Description as MainUnitName,twms.Description as MatSpec,t1.CreateTime,
                                                        t1.ProduceDate                                
                                                        from T_WH_MatAmount t1 
                                                        left outer join T_WH_Warehouse t2 on t1.Warehouse=t2.ID  
                                                        left outer join T_WH_MatSpec twms on t1.Unit=twms.ID                                                      
                                                        left outer join T_WH_Mat t4 on t1.MatID=t4.ID
                                                        left outer join T_WH_MatType t3 on t4.ProductType=t3.ID
                                                        left outer join T_WH_Site t5 on t1.SaveSite = t5.ID
                                                        left outer join T_WH_MatUnit twmu on t4.UnitCode=twmu.ID
                                                        where 1=1 ");
                }

                page.KeyName = "ID";

                if (!string.IsNullOrEmpty(condition.Warehouse))
                {
                    sql.Append(" and t1.Warehouse = @Warehouse");
                    parameters.Add(new DataParameter("Warehouse", condition.Warehouse));
                }

                //if (string.IsNullOrEmpty(condition.CreateUser) == false && condition.CreateUser.ToLower() != "admin")
                //{
                //    sql.Append(" and t1.Warehouse in (select WarehouseID from TWarehousePower where UserID=@UserID)");
                //    parameters.Add(new DataParameter("UserID", condition.CreateUser));
                //}

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

                if (condition.IsDetail == false)
                {
                    sql.Append(" group by WarehouseName,t1.MatID,t4.MatCode,t4.MatName,ProductType");
                    page.SortExpression = "WarehouseName,MatCode";
                }
                else
                {
                    page.SortExpression = "WarehouseName,SaveSite,MatCode,MatBarCode,CreateTime";
                }

                //page.KeyName = "ID";                

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

        #endregion

        #region 库存移动

        /// <summary>
        /// 库存移动
        /// </summary>
        /// <param name="records">移动记录</param>
        public void MoveStock(List<MoveStockRecord> records)
        {
            string sql = null;
            sql = "update T_WH_MatAmount set Warehouse = @Warehouse,SaveSite = @SaveSite where MatBarCode = @MatBarCode";

            foreach (MoveStockRecord record in records)
            {
                Session.ExecuteSql(sql, new DataParameter("MatBarCode", record.IDCode)
                    , new DataParameter("Warehouse", record.ToWarehouse)
                    , new DataParameter("SaveSite",record.ToSaveSite));
            }
        }

        #endregion
    }
}
