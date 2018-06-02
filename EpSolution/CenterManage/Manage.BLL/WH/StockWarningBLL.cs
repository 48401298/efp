using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.BLL;
using LAF.Data;
using Manage.Entity.WH;
using Manage.DAL.WH;

namespace Manage.BLL.WH
{
    /// <summary>
    /// 库存预警查询
    /// </summary>
    public class StockWarningBLL:BaseBLL
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
            if (this.LoginUser.UserName.ToLower() != "admin")
            {
                condition.CreateUser = this.LoginUser.UserID;
            }
            page = new StockWarningDAL().GetList(condition, page);

            List<StockWarningResult> list = page.Result as List<StockWarningResult>;

            foreach (StockWarningResult item in list)
            {
                if (item.StockAmount >= item.MaxAmount)
                {
                    item.WarningMode = "1";
                }

                if (item.StockAmount <= item.MinAmount)
                {
                    item.WarningMode = "2";
                }
            }

            return page;
        }

        #endregion
    }
}
