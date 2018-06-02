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
    /// 监测统计
    /// </summary>
    public class InspectStatisticsBLL : BaseBLL
    {
        #region 获取统计数据列表

        /// <summary>
        /// 获取统计数据列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(InspectResultEntity condition, DataPage page)
        {
            try
            {
                return new InspectStatisticsDAL().GetList(condition, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
