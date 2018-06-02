using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.BLL;
using Manage.Entity;
using Manage.Entity.WH;
using Manage.DAL.WH;

namespace Manage.BLL.WH
{
    /// <summary>
    /// 出库查询
    /// </summary>
    public class OutStockQueryBLL:BaseBLL
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
            if (this.LoginUser.UserName.ToLower() != "admin")
            {
                condition.CREATEUSER = this.LoginUser.UserID;
            }
            return new OutStockQueryDAL().GetList(condition, page);
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
            return new OutStockQueryDAL().GetOutMatDetails(condition, page);
        }

        #endregion

        #region 获取出库数量列表

        public List<OutStockQueryResult> GetOutAmountList(OutStockQueryResult condition)
        {
            return new OutStockQueryDAL().GetOutAmountList(condition);
        }

        #endregion
    }
}
