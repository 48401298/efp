using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Common.Encrypt;
using LAF.Data;
using LAF.BLL;
using LAF.Entity;
using Manage.Entity.Sys;
using Manage.DAL.Sys;
using Manage.BLL.Dict;

namespace Manage.BLL.Sys
{
    /// <summary>
    /// 公告管理
    /// </summary>
    public class NoticeManageBll:BaseBLL
    {
        #region 获取公告信息

        /// <summary>
        /// 获取公告信息
        /// </summary>
        /// <param name="user">条件</param>
        /// <returns>用户信息</returns>
        public NoticeInfo Get(NoticeInfo notice)
        {
            try
            {
                return new NoticeManageDAL().Get(notice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
        #endregion

        #region 获取公告列表

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(NoticeInfo condition, DataPage page)
        {
            try
            {
                return new NoticeManageDAL().GetList(condition, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 公告信息是否重复

        /// <summary>
        /// 判断公告名称是否存在
        /// </summary>
        /// <param name="info">信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsNotice(NoticeInfo notice)
        {
            try
            {
                return new NoticeManageDAL().ExistsNotice(notice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 插入公告

        /// <summary>
        /// 插入公告
        /// </summary>
        /// <param name="user">公告信息</param>
        /// <returns>插入数</returns>
        public int Insert(NoticeInfo notice)
        {
            try
            {
                return new NoticeManageDAL().Insert(notice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除公告

        /// <summary>
        /// 删除公告信息
        /// </summary>
        /// <param name="users">公告信息</param>
        /// <returns>删除个数</returns>
        public int Delete(string[] notices)
        {
            int count = 0;
            try
            {
                foreach (string notice in notices)
                {
                    count += this.Delete(new NoticeInfo { NoticeID = notice });
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除公告信息
        /// </summary>
        /// <param name="user">公告信息</param>
        /// <returns>删除个数</returns>
        public int Delete(NoticeInfo notice)
        {
            try
            {
                return new NoticeManageDAL().Delete(notice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新公告

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="user">公告信息</param>
        /// <returns>更新个数</returns>
        public int Update(NoticeUpdateInfo notice)
        {
            try
            {
                return new NoticeManageDAL().Update(notice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取未读取通知信息
        /// <summary>
        /// 获取未读取通知信息
        /// </summary>
        /// <returns></returns>
        public List<NoticeInfo> GetNotReadNotice()
        {
            try
            {
                return new NoticeManageDAL().GetNotReadNotice();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
 