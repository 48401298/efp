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
    /// 监测项目
    /// </summary>
    public class InspectItemBLL : BaseBLL
    {
        #region 获取监测项目列表

        /// <summary>
        /// 获取监测项目列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(InspectItemEntity condition, DataPage page)
        {
            try
            {
                return new InspectItemDAL().GetList(condition, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<InspectItemEntity> GetAllItemInfo(InspectItemEntity condition)
        {
            try
            {
                return new InspectItemDAL().GetAllItemInfo(condition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取监测项目信息

        /// <summary>
        /// 获取监测项目信息
        /// </summary>
        /// <param name="itemInfo">条件</param>
        /// <returns>监测项目信息</returns>
        public InspectItemEntity Get(InspectItemEntity itemInfo)
        {
            try
            {
                return new InspectItemDAL().Get(itemInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 插入监测项目信息

        /// <summary>
        /// 插入监测项目信息
        /// </summary>
        /// <param name="itemInfo">监测项目信息</param>
        /// <returns>插入数</returns>
        public DataResult<int> Insert(InspectItemEntity itemInfo)
        {
            try
            {
                DataResult<int> result = new DataResult<int>();
                InspectItemDAL cmdDAL = new InspectItemDAL();
                if (ExistsInspectItem(itemInfo))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                itemInfo.Id = Guid.NewGuid().ToString();
                itemInfo.CreateUser = this.LoginUser.UserID;
                itemInfo.CreateTime = DateTime.Now;
                itemInfo.UpdateUser = itemInfo.CreateUser;
                itemInfo.UpdateTime = itemInfo.CreateTime;
     
                result.Result = new InspectItemDAL().Insert(itemInfo);
                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除监测项目信息

        /// <summary>
        /// 删除监测项目信息
        /// </summary>
        /// <param name="itemInfo">删除监测项目信息</param>
        /// <returns>删除行数</returns>
        public int Delete(InspectItemEntity itemInfo)
        {
            int count = 0;

            try
            {
                count = new InspectItemDAL().Delete(itemInfo);
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新监测项目信息

        /// <summary>
        /// 更新监测项目信息
        /// </summary>
        /// <param name="user">监测项目信息</param>
        /// <returns>更新个数</returns>
        public DataResult<int> Update(InspectItemEntity itemInfo)
        {
            try
            {
                DataResult<int> result = new DataResult<int>();
                if (ExistsInspectItem(itemInfo))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                itemInfo.UpdateUser = this.LoginUser.UserID;
                itemInfo.UpdateTime = DateTime.Now;
                result.Result = new InspectItemDAL().Update(itemInfo);
                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 信息是否重复
        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsInspectItem(InspectItemEntity model)
        {
            try
            {
                return new InspectItemDAL().ExistsInspectItem(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
