using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Common.Encrypt;
using LAF.BLL;
using LAF.Entity;
using Manage.Entity.MES;
using Manage.DAL.Sys;
using Manage.BLL.MES;

namespace Manage.BLL.Sys
{
    /// <summary>
    /// 登录管理
    /// </summary>
    public class LoginBLL:BaseBLL
    {
        #region 检验登录信息

        /// <summary>
        /// 检验登录信息
        /// </summary>
        /// <param name="login">登录信息</param>
        /// <returns>登录信息，Message为null时登录成功。</returns>
        public LoginInfo IsLogin(LoginInfo login)
        {
            try
            {
                login.PassWord = LAF.Common.Encrypt.DESEncrypt.Encrypt(login.PassWord);

                login = new LoginDAL().IsLogin(login);

                if (login == null)
                    return login;

                //获取所在班组
                List<WorkGroup> wgList = new WorkGroupBLL().GetList();
                if (wgList.Count > 0)
                {
                    login.WorkGroupID = wgList[0].PID;
                }

                return login;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

    }
}
