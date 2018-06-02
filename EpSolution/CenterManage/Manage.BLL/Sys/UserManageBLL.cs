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

namespace Manage.BLL.Sys
{
    /// <summary>
    /// 用户信息管理
    /// </summary>
    public class UserManageBLL : BaseBLL
    {
        #region 获取用户信息列表

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(User condition, DataPage page)
        {
            try
            {
                return new UserManageDAL().GetList(condition, page);
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
        public List<User> GetAllUsers(User condition)
        {
            try
            {
                return new UserManageDAL().GetAllUsers(condition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取用户信息信息

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user">条件</param>
        /// <returns>用户信息</returns>
        public User Get(User user)
        {
            try
            {
                return new UserManageDAL().Get(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取输入的登陆账户是否存在

        /// <summary>
        /// 获取输入的登陆账户是否存在
        /// </summary>
        /// <param name="user">条件</param>
        /// <returns>用户信息信息</returns>
        public User GetLoginUser(User user)
        {
            try
            {
                return new UserManageDAL().GetLoginUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region 插入用户信息信息

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="user">用户信息信息</param>
        /// <returns>插入数</returns>
        public int Insert(User user)
        {
            try
            {
                user.UserID = Guid.NewGuid().ToString();
                user.PassWord = DESEncrypt.Encrypt(user.PassWord);
                user.DelFlag = "0";
                user.IsStop = "0";
                user.CreateUser = this.LoginUser.UserID;
                user.CreateTime = DateTime.Now;

                foreach (UserRole ur in user.Roles)
                {
                    ur.UserID = user.UserID;
                }

                return new UserManageDAL().Insert(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除用户信息

        /// <summary>
        /// 删除用户信息信息
        /// </summary>
        /// <param name="user">用户信息信息</param>
        /// <returns>删除个数</returns>
        public int BatchDelete(ArrayList users)
        {
            int count = 0;
            try
            {
                foreach (string user in users)
                {
                    count += this.Delete(new User { UserID = user });
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除用户信息信息
        /// </summary>
        /// <param name="user">用户信息信息</param>
        /// <returns>删除行数</returns>
        public int Delete(User user)
        {
            int count = 0;

            try
            {
                count = new UserManageDAL().Delete(user);
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新用户信息

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户信息信息</param>
        /// <returns>更新个数</returns>
        public int Update(User user)
        {
            try
            {
                user.UpdateUser = this.LoginUser.UserID;
                user.UpdateTime = DateTime.Now;
                foreach (UserRole ur in user.Roles)
                {
                    ur.UserID = user.UserID;
                }
                return new UserManageDAL().Update(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 设置密码

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns>执行结果</returns>
        public void SetPassWord(User info)
        {
            try
            {
                info.PassWord = DESEncrypt.Encrypt(info.PassWord);
                info.UpdateUser = this.LoginUser.UserID;
                new UserManageDAL().SetPassWord(info);              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
