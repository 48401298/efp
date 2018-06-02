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
    /// 仓库权限管理
    /// </summary>
    public class WHPowerBLL:BaseBLL
    {
        #region 仓库权限

        /// <summary>
        /// 获取仓库权限
        /// </summary>
        /// <param name="userID">用户主键</param>
        /// <returns>权限列表</returns>
        public List<WarehousePower> GetWHPowers(string userID)
        {
            return new WHPowerDAL().GetWHPowers(userID);
        }

        /// <summary>
        /// 删除仓库权限
        /// </summary>
        /// <param name="userID">用户主键</param>
        public void DeleteWHPowers(string userID)
        {
            new WHPowerDAL().DeleteWHPowers(userID);
        }

        /// <summary>
        /// 保存仓库权限
        /// </summary>
        /// <param name="userID">用户主键</param>
        /// <param name="powers">仓库权限</param>
        public void SaveWHPowers(string userID, List<WarehousePower> powers)
        {
            foreach (WarehousePower power in powers)
            {
                power.ID = Guid.NewGuid().ToString();
                power.UserID = userID;
            }
            new WHPowerDAL().SaveWHPowers(userID,powers);
        }

        #endregion
    }
}
