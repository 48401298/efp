using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using LAF.Data;
using LAF.BLL;
using LAF.Common.Encrypt;
using Manage.Entity.Sys;
using Manage.DAL.Sys;
using Manage.DAL.Inspect;
using Manage.Entity.Inspect;
using Manage.Entity;

namespace Manage.BLL.Inspect
{
    /// <summary>
    /// 监测地图
    /// </summary>
    public class InspectMapBLL : BaseBLL
    {
        #region 获取设备列表

        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<InspectDeviceEntity> GetList(InspectDeviceEntity condition)
        {
            try
            {
                return new InspectMapDAL().GetList(condition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
