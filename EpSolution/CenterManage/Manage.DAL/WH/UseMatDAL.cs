using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data;
using LAF.Data.Attributes;
using Manage.Entity.WH;
using Manage.Entity.MES;

namespace Manage.DAL.WH
{
    /// <summary>
    /// 领料管理
    /// </summary>
    public class UseMatDAL : BaseDAL
    {
        #region 新增领料单

        public void Insert(UseMatBill bill)
        {
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.OpenTs();
                session.Insert<UseMatBill>(bill);
                session.Insert<UseMatAmount>(bill.Amounts);
                session.Insert<UseMatDetail>(bill.Details);
                session.CommitTs();
            }
        }

        #endregion

        #region 获取领料单信息

        /// <summary>
        /// 获取领料单信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public UseMatBill Get(UseMatBill model)
        {
            UseMatBill info = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                //基本信息
                sql=@"SELECT um.PID,si.BatchNumber,fi.PNAME AS FACTORYNAME,pi.PNAME AS ProduceName,pl.PLNAME AS PLName
                ,wh.Description as WarehouseName
                FROM T_WH_UseMat um
                INNER JOIN T_FP_SUPPLYINFO si on um.SUPPLYID = si.PID
                LEFT OUTER JOIN T_FP_ProducePlan pp ON  si.PLANID = pp.PID 
                LEFT OUTER JOIN T_FP_FACTORYINFO fi ON pp.FACTORYPID =fi.PID
                LEFT OUTER JOIN T_FP_PRODUCTINFO pi ON pp.PRODUCTIONID = pi.PID
                LEFT OUTER JOIN T_FP_PRODUCTLINE pl on pp.PRID = pl.PID
                LEFT OUTER JOIN T_WH_Warehouse wh on si.Warehouse=wh.ID
                where um.PID = @PID";

                info = session.Get<UseMatBill>(sql, new DataParameter("PID", model.PID));

                //领料数量
                sql = @"select mat.MatName,uma.AMOUNT,mu.Description as UnitName from T_WH_UseMatAmount uma
                        left outer join T_WH_Mat mat on uma.MATRIALID =  mat.ID
                        left outer join T_WH_MatUnit mu on mat.UnitCode = mu.ID";

                parameters.Clear();
                parameters.Add(new DataParameter("PID", model.PID));
                info.Amounts=session.GetList<UseMatAmount>(sql,parameters.ToArray()).ToList();

                //领料明细
                sql = @"select whs.Description as SaveSite,umd.MatBarCode,mat.MatName,umd.AMOUNT,twms.UnitName as Unit
                        from T_WH_UseMatDetail umd
                        left outer join T_WH_MatIDCode mic on umd.MatBarCode=mic.IDCode
                        left outer join T_WH_MatSpec twms on mic.MatSpec = twms.ID
                        left outer join T_WH_Site whs on umd.SaveSite =  whs.ID
                        left outer join T_WH_Mat mat on umd.MATRIALID =  mat.ID
                        left outer join T_WH_MatUnit mu on mat.UnitCode = mu.ID";
                parameters.Clear();
                parameters.Add(new DataParameter("PID", model.PID));
                info.Details = session.GetList<UseMatDetail>(sql, parameters.ToArray()).ToList();
            }
            return info;
        }
        #endregion

        #region 获取列表

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(UseMatBill condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "PID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "UPDATETIME DESC";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<UseMatBill>(sql, parameters.ToArray(), page);
                }
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新领料单

        public void Update(UseMatBill bill)
        {
            string sql = null;
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.OpenTs();
                session.Update<UseMatBill>(bill);

                sql = "delete from T_WH_UseMatAmount where USEID = @USEID";
                session.ExecuteSql(sql, new DataParameter("USEID", bill.PID));
                session.Insert<UseMatAmount>(bill.Amounts);

                sql = "delete from T_WH_UseMatDetail where USEID = @USEID";
                session.ExecuteSql(sql, new DataParameter("USEID", bill.PID));
                session.Insert<UseMatDetail>(bill.Details);
                session.CommitTs();
            }
        }

        #endregion

        #region 删除领料单

        public void Delete(UseMatBill bill)
        {
            string sql = null;
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.OpenTs();
                session.Delete<UseMatBill>(bill);

                sql = "delete from T_WH_UseMatAmount where USEID = @USEID";
                session.ExecuteSql(sql, new DataParameter("USEID", bill.PID));
                session.Insert<UseMatAmount>(bill.Amounts);

                sql = "delete from T_WH_UseMatDetail where USEID = @USEID";
                session.ExecuteSql(sql, new DataParameter("USEID", bill.PID));
                session.Insert<UseMatDetail>(bill.Details);
                session.CommitTs();
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
        private string GetQuerySql(UseMatBill condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT si.PID as SUPPLYID,si.UPDATETIME,si.BatchNumber,si.DELIVERYDATE,fi.PNAME AS FACTORYNAME,
                    pi.PNAME AS ProduceName,pl.PLNAME AS PLNAME,wh.Description as WarehouseName,um.PID
                    FROM T_FP_SUPPLYINFO si 
                    LEFT OUTER JOIN T_FP_ProducePlan pp ON si.PLANID = pp.PID 
                    LEFT OUTER JOIN T_FP_FACTORYINFO fi ON pp.FACTORYPID =fi.PID
                    LEFT OUTER JOIN T_FP_PRODUCTINFO pi ON pp.PRODUCTIONID = pi.PID
                    LEFT OUTER JOIN T_FP_PRODUCTLINE pl on pp.PRID = pl.PID
                    LEFT OUTER JOIN T_WH_Warehouse wh on si.Warehouse=wh.ID
                    LEFT OUTER JOIN T_WH_UseMat um on si.PID = um.SUPPLYID
                    ");

                if (string.IsNullOrEmpty(condition.BatchNumber) == false)
                {
                    whereBuilder.Append(" AND si.BatchNumber like @BatchNumber");
                    parameters.Add(new DataParameter("BatchNumber", "%" + condition.BatchNumber + "%"));
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

        #region 获取可用库存货品

        /// <summary>
        /// 获取可用库存货品
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <returns>可用库存货品</returns>
        public List<WHMatAmount> GetMayUseStock(WHMatAmount condition)
        {
            List<WHMatAmount> list = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();

            sql = @"select * from T_WH_MatAmount 
                    where Warehouse = @Warehouse and MatID = @MatID and ProductAmount>0 order by UpdateTime asc";

            parameters.Add(new DataParameter("Warehouse", condition.Warehouse));
            parameters.Add(new DataParameter("MatID", condition.MatID));

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<WHMatAmount>(sql, parameters.ToArray()).ToList();
            }

            return list;
        }

        #endregion
    }
}
