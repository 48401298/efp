using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.Entity;
using LAF.Common.Util;
using Manage.Entity.Sys;

namespace Manage.DAL.Sys
{
    /// <summary>
    /// 登录管理
    /// </summary>
    public class LoginDAL
    {
        #region 检验登录信息

        /// <summary>
        /// 获取用户和密码是否正确
        /// </summary>
        /// <param name="user">条件</param>
        /// <returns>用户信息</returns>
        public LoginInfo IsLogin(LoginInfo login)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            DataTable powerDt = new DataTable();
            List<DataParameter> parameters = new List<DataParameter>();        
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {

                    //获取用户信息
                    sqlBuilder.Append(" SELECT * ");
                    sqlBuilder.Append(" FROM T_USER AS A WHERE LoginUserID = @LoginUserID AND PASSWORD = @PASSWORD  ");

                    if (login.LoginUserID != "admin")
                    {
                        sqlBuilder.Append("  AND ISSTOP='0' ");
                    }
                    parameters.Add(new DataParameter { ParameterName = "LoginUserID", DataType = DbType.String, Value = login.LoginUserID });
                    parameters.Add(new DataParameter { ParameterName = "PASSWORD", DataType = DbType.String, Value = login.PassWord });

                    login = session.Get<LoginInfo>(sqlBuilder.ToString(), parameters.ToArray());

                    if (login != null)
                    {
                        //获取用户权限                        
                        sqlBuilder.Clear();
                        sqlBuilder.Append(@"
                                SELECT T1.AUTHORITYID FROM T_ROLEAUTHORITY T1
                                INNER JOIN T_USERROLE T2 ON T1.ROLEID = T2.ROLEID
                                WHERE T2.USERID = @USERID
                                UNION
                                SELECT AUTHORITYID FROM T_USERAUTHORITY
                                WHERE USERID = @USERID");

                        parameters.Clear();
                        parameters.Add(new DataParameter("USERID", login.UserID));

                        session.FillTable(powerDt, sqlBuilder.ToString(), parameters.ToArray());

                        login.Powers = new List<string>();
                        foreach (DataRow row in powerDt.Rows)
                        {
                            login.Powers.Add(row["AUTHORITYID"].ToString());
                        }

                        //获取用户所属的组织机构
                        sqlBuilder.Clear();
                        sqlBuilder.Append(" SELECT * ");
                        sqlBuilder.Append(" FROM T_USERORGAIZATION AS A WHERE USERID = @USERID");

                        parameters.Clear();
                        parameters.Add(new DataParameter { ParameterName = "USERID", DataType = DbType.String, Value = login.UserID });

                    }
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
