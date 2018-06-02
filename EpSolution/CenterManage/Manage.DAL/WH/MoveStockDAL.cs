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
    /// 库存移动
    /// </summary>
    public class MoveStockDAL:BaseDAL
    {
        /// <summary>
        /// 数据库会话
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
            sql = "select BillNO from T_WH_MoveStockBill where BillNO = @BillNO";

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

        #region 获取最大入库单号

        /// <summary>
        /// 获取最大入库单号
        /// </summary>
        /// <returns></returns>
        public string GetMaxBillNO()
        {
            string no = null;
            object value = null;
            string sql = "select max(BillNO) from T_WH_MoveStockBill Where BillDate >= @StartDate and BillDate <=@EndDate";

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

        #region 插入信息

        /// <summary>
        /// 插入信息(单表)
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>插入行数</returns>
        public int Insert(MoveStockBill model)
        {
            int count = 0;
            try
            {
                //插入基本信息
                count = Session.Insert<MoveStockBill>(model);
                Session.Insert<MoveStockDetail>(model.Details);
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取库存移动单列表

        /// <summary>
        /// 获取库存移动单列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(MoveStockBill condition, DataPage page)
        {
            DataTable dt = new DataTable();
            List<DataParameter> parameters = new List<DataParameter>();
            StringBuilder sqlBuiler = new StringBuilder();
            try
            {
                page.KeyName = "ID";

                sqlBuiler.Append(@"select t1.ID,t1.BillNO,t1.BillDate,t1.ToWHHeader,u3.USERNAME as ToWHHeaderName,
                                    t1.Remark,t2.Description as ToWarehouseName
                                   from T_WH_MoveStockBill t1
                                    inner join T_WH_Warehouse t2 on t1.Warehouse=t2.ID
                                    left outer join T_USER u3 on t1.ToWHHeader=u3.USERID
                                    left outer join T_WH_InMode t4 on t1.InStockMode=t4.ID
                                    where t1.MoveMode = @MoveMode");

                parameters.Add(new DataParameter("MoveMode", condition.MoveMode));

                if (!string.IsNullOrEmpty(condition.BillNO))
                {
                    sqlBuiler.Append(" and t1.BillNO like @BillNO");
                    parameters.Add(new DataParameter("BillNO", "%" + condition.BillNO + "%"));
                }

                if (!string.IsNullOrEmpty(condition.ToWarehouse))
                {
                    sqlBuiler.Append(" and t1.ToWarehouse = @ToWarehouse");
                    parameters.Add(new DataParameter("ToWarehouse", condition.ToWarehouse));
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
                    page = session.GetDataPage<MoveStockBill>(sqlBuiler.ToString(), parameters.ToArray(), page);
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
