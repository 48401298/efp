using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.BLL;
using Manage.Entity.Sys;
using Manage.DAL.Sys;

namespace Manage.BLL.Sys
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleManageBLL : BaseBLL
    {
        #region 获取角色信息

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="role">条件</param>
        /// <returns>角色信息</returns>
        public RoleInfo Get(RoleInfo role)
        {
            try
            {
                return new RoleManageDAL().Get(role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(RoleInfo condition, DataPage page)
        {
            try
            {
                return new RoleManageDAL().GetList(condition, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取全部角色信息

        /// <summary>
        /// 获取全部角色信息
        /// </summary>
        /// <param name="role">条件</param>
        /// <returns>角色信息</returns>
        public List<RoleInfo> GetAll()
        {
            try
            {
                return new RoleManageDAL().GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 判断角色名称是否存在

        /// <summary>
        /// 判断角色名称是否存在
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsRole(RoleInfo role)
        {
            try
            {
                return new RoleManageDAL().ExistsRole(role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 插入角色

        /// <summary>
        /// 插入角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>插入数</returns>
        public int Insert(RoleInfo role)
        {
            try
            {
                role.RoleID = Guid.NewGuid().ToString();
                role.CreateTime = DateTime.Now;
                role.CreateUser = this.LoginUser.UserID;
                role.UpdateTime = DateTime.Now;
                role.UpdateUser = this.LoginUser.UserID;

                return new RoleManageDAL().Insert(role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>删除个数</returns>
        public int BatchDelete(ArrayList roles)
        {
            int count = 0;
            try
            {
                foreach (string role in roles)
                {
                    count += this.Delete(new RoleInfo { RoleID = role });
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>删除行数</returns>
        public int Delete(RoleInfo role)
        {
            int count = 0;

            try
            {
                count = new RoleManageDAL().Delete(role);
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新角色

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>更新个数</returns>
        public int Update(RoleInfo role)
        {
            try
            {
                role.UpdateTime = DateTime.Now;
                role.UpdateUser = this.LoginUser.UserID;
                if (role.Powers != null)
                {
                    foreach (RoleAuthority ra in role.Powers)
                    {
                        ra.RoleID = role.RoleID;
                    }
                }
                return new RoleManageDAL().Update(role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
