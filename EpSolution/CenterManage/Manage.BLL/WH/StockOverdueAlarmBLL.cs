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
    /// 库存过期预警查询
    /// </summary>
    public class StockOverdueAlarmBLL:BaseBLL
    {
        /// <summary>
        /// 获取过期预警列表
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

            return new StockOverdueAlarmDAL().GetList(condition, page);
        }

    }
}
