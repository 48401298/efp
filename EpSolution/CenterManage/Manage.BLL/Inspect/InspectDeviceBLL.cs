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
    /// 监测设备
    /// </summary>
    public class InspectDeviceBLL : BaseBLL
    {
        #region 获取监测设备列表

        /// <summary>
        /// 获取监测设备列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(InspectDeviceEntity condition, DataPage page)
        {
            try
            {
                return new InspectDeviceDAL().GetList(condition, page);
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
        public List<InspectDeviceEntity> GetAllItemInfo(InspectDeviceEntity condition)
        {
            try
            {
                return new InspectDeviceDAL().GetAllItemInfo(condition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取监测设备信息

        /// <summary>
        /// 获取监测设备信息
        /// </summary>
        /// <param name="itemInfo">条件</param>
        /// <returns>监测设备信息</returns>
        public InspectDeviceEntity Get(InspectDeviceEntity model)
        {
            try
            {
                return new InspectDeviceDAL().Get(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 插入监测设备信息

        /// <summary>
        /// 插入监测设备信息
        /// </summary>
        /// <param name="itemInfo">监测设备信息</param>
        /// <returns>插入数</returns>
        public DataResult<int> Insert(InspectDeviceEntity model)
        {
            try
            {
                DataResult<int> result = new DataResult<int>();
                InspectDeviceDAL cmdDAL = new InspectDeviceDAL();
                if (ExistsInspectDevice(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                model.Id = Guid.NewGuid().ToString();
                model.CreateUser = this.LoginUser.UserID;
                model.CreateTime = DateTime.Now;
                model.UpdateUser = model.CreateUser;
                model.UpdateTime = model.CreateTime;

                result.Result = new InspectDeviceDAL().Insert(model);
                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除监测设备信息

        /// <summary>
        /// 删除监测设备信息
        /// </summary>
        /// <param name="itemInfo">删除监测设备信息</param>
        /// <returns>删除行数</returns>
        public int Delete(InspectDeviceEntity model)
        {
            int count = 0;

            try
            {
                count = new InspectDeviceDAL().Delete(model);
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新监测设备信息

        /// <summary>
        /// 更新监测设备信息
        /// </summary>
        /// <param name="user">监测设备信息</param>
        /// <returns>更新个数</returns>
        public DataResult<int> Update(InspectDeviceEntity model)
        {
            try
            {
                DataResult<int> result = new DataResult<int>();
                if (ExistsInspectDevice(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                model.UpdateUser = this.LoginUser.UserID;
                model.UpdateTime = DateTime.Now;
                result.Result = new InspectDeviceDAL().Update(model);
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
        public bool ExistsInspectDevice(InspectDeviceEntity model)
        {
            try
            {
                return new InspectDeviceDAL().ExistsInspectDevice(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        
        #region 根据企业和设备类型获取设备的下拉列表
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<InspectDeviceEntity> GetAllDeviceByOrgAndType(InspectDeviceEntity condition)
        {
            try
            {
                return new InspectDeviceDAL().GetAllDeviceByOrgAndType(condition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
