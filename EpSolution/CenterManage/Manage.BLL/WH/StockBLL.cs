using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.BLL;
using Manage.Entity.WH;
using Manage.DAL.WH;
using Manage.Entity;

namespace Manage.BLL.WH
{
    /// <summary>
    /// 库存管理
    /// </summary>
    public class StockBLL:BaseBLL
    {
        #region 获取库存列表

        /// <summary>
        /// 获取库存列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(WHMatAmount condition, DataPage page)
        {
            if (this.LoginUser.UserName.ToLower() != "admin")
            {
                condition.CreateUser = this.LoginUser.UserID;
            }

            return new StockDAL().GetList(condition, page);
        }

        #endregion

        #region 获取根据条码库存信息

        /// <summary>
        /// 获取根据条码库存信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public WHMatAmount GetStockByBarCode(string matBarCode)
        {
            return new StockDAL().GetStockByBarCode(matBarCode);
        }

        #endregion

        #region 判断条码是否已入库

        /// <summary>
        /// 判断条码是否已入库
        /// </summary>
        /// <param name="matBarCode"></param>
        /// <returns></returns>
        public bool ExistsMatBarCode(string matBarCode)
        {
            return new StockDAL().ExistsMatBarCode(matBarCode); 
        }

        #endregion
    }
}
