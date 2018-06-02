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
    /// 入库查询
    /// </summary>
    public class InStockQueryBLL:BaseBLL
    {

        #region 获取入库查询结果列表

        /// <summary>
        /// 获取入库查询结果列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(InStockBill condition, DataPage page)
        {
            if (this.LoginUser.UserName.ToLower() != "admin")
            {
                condition.CREATEUSER = this.LoginUser.UserID;
            }

            return new InStockQueryDAL().GetList(condition, page);
        }

        #endregion

         #region 获取入库查询结果列表

        /// <summary>
        /// 获取入库查询结果列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetInMatDetails(InStockBill condition, DataPage page)
        {
            return new InStockQueryDAL().GetInMatDetails(condition, page);
        }

        #endregion

        #region 获取入库数量列表

        public List<InStockQueryResult> GetInAmountList(InStockQueryResult condition)
        {
            return new InStockQueryDAL().GetInAmountList(condition);
        }

        #endregion
    }
}
