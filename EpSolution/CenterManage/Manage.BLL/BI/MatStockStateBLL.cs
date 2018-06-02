using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Entity.WH;
using Manage.DAL.BI;

namespace Manage.BLL.BI
{
    /// <summary>
    /// 货品库存分析
    /// </summary>
    public class MatStockStateBLL
    {
        #region 获取指定仓库货品库存信息

        /// <summary>
        /// 获取指定仓库货品库存信息
        /// </summary>
        /// <param name="whID">仓库主键</param>
        /// <returns></returns>
        public List<WHMatAmount> GetMatStockListByWH(string whID)
        {
            return new MatStockStateDAL().GetMatStockListByWH(whID);
        }

        #endregion
    }
}
