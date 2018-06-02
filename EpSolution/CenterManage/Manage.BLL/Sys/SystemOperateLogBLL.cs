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
using Manage.BLL.Dict;
using System.Data;

namespace Manage.BLL.Sys
{
    /// <summary>
    /// 系统操作LOG记录
    /// </summary>
    public class SystemOperateLogBLL : BaseBLL
    {
        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="log"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(SystemOperateLog log, DataPage page)
        {
            try
            {
                page = new SystemOperateLogDAL().GetList(log, page);
                List<SystemOperateLog> retList = (List<SystemOperateLog>)page.Result;
               
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public int Insert(SystemOperateLog log)
        {
            try
            {
                log.OperateID = Guid.NewGuid().ToString();
                //操作用户
                log.UserID = this.LoginUser.LoginUserID;
                //操作时间
                log.OperateTime = DateTime.Now.ToString();
                return new SystemOperateLogDAL().Insert(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取导出的数据

        /// <summary>
        /// 获取导出的数据
        /// </summary>
        /// <param name="user">查询条件</param>      
        /// <returns>数据</returns>
        public DataTable GetExportData(SystemOperateLog log)
        {
            try
            {
                return new SystemOperateLogDAL().GetExportData(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
