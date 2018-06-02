using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.Sys;
using System.Data;
using LAF.Common.Encrypt;

namespace Manage.DAL.Sys
{
    /// <summary>
    /// 用户信息管理
    /// </summary>
    public class UserManageDAL
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
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);

                //分页关键字段及排序
                page.KeyName = "LOGINUSERID";
                if (string.IsNullOrEmpty(page.SortExpression))
                {
                    page.SortExpression = "UPDATETIME DESC";
                }

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<User>(sql, parameters.ToArray(), page);
                }

                return page;
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
            List<User> users = null;
            string sql = null;
            List<DataParameter> parameters=new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    users = session.GetList<User>(sql, parameters.ToArray()).ToList();
                }

                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="parameters">查询</param>
        /// <returns>查询语句</returns>
        private string GetQuerySql(User condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();

            sqlBuilder.Append(" SELECT a.USERID, ");
            sqlBuilder.Append(" a.LOGINUSERID,a.USERNAME,a.PASSWORD,a.TEL  ");
            sqlBuilder.Append(" ,a.MOBILETEL,a.EMAIL,a.CREATEUSER,a.CREATETIME,a.UPDATEUSER,a.UPDATETIME,a.ORGANID,o.ORGANDESC as OrganDESC  ");
            sqlBuilder.Append(" ,a.DELFLAG,(CASE WHEN a.ISSTOP ='0' THEN '否' else '是' end) as ISSTOP  ");
            sqlBuilder.Append(@" FROM T_USER a
                                left outer join T_ORGANIZATION o on a.ORGANID = o.ORGANID");

            //查询条件
            if (string.IsNullOrEmpty(condition.LoginUserID) == false)
            {
                whereBuilder.Append(" AND a.LOGINUSERID like @LOGINUSERID");
                parameters.Add(new DataParameter { ParameterName = "LOGINUSERID", DataType = DbType.String, Value = "%" + condition.LoginUserID + "%" });
            }
            //查询条件
            if (string.IsNullOrEmpty(condition.UserName) == false)
            {
                whereBuilder.Append(" AND a.USERNAME like @USERNAME");
                parameters.Add(new DataParameter { ParameterName = "USERNAME", DataType = DbType.String, Value = "%" + condition.UserName + "%" });
            }

            //登录用户
            if (string.IsNullOrEmpty(condition.OrganID) == false && condition.OrganID!="root")
            {
                whereBuilder.Append(" AND a.ORGANID = @OrganID");

                parameters.Add(new DataParameter { ParameterName = "OrganID", DataType = DbType.String, Value = condition.OrganID });
            }
            if (whereBuilder.Length > 0)
            {
                sqlBuilder.Append(" where "+whereBuilder.ToString().Substring(4));
            }

            return sqlBuilder.ToString();
        }

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 获取用户信息信息
        /// </summary>
        /// <param name="user">条件</param>
        /// <returns>用户信息信息</returns>
        public User Get(User user)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            User tmpuser = new User();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {                    
                    //获取用户信息
                    sqlBuilder.Append(" SELECT USERID, ");
                    sqlBuilder.Append(" LOGINUSERID,USERNAME,ORGANID,PASSWORD,TEL,  ");
                    sqlBuilder.Append(" MOBILETEL,EMAIL,CREATEUSER,CREATETIME,UPDATEUSER,UPDATETIME,  ");
                    sqlBuilder.Append(" DELFLAG,ISSTOP  ");
                    sqlBuilder.Append(" FROM T_USER A WHERE UserID = @UserID");

                    parameters.Add(new DataParameter { ParameterName = "UserID", DataType = DbType.String, Value = user.UserID });

                    tmpuser = session.Get<User>(sqlBuilder.ToString(), parameters.ToArray());
                    tmpuser.PassWord = DESEncrypt.Decrypt(tmpuser.PassWord);

                    //获取用户所有角色信息
                    sqlBuilder.Remove(0, sqlBuilder.Length);
                    parameters = new List<DataParameter>();
                    sqlBuilder.Append(@"SELECT UR.USERID,UR.ROLEID,R.ROLEDESC  
                                    FROM T_USERROLE UR
                                    INNER JOIN T_ROLE R ON UR.ROLEID = R.ROLEID
                                    WHERE UR.USERID=@USERID");

                    parameters.Add(new DataParameter { ParameterName = "USERID", DataType = DbType.String, Value= user.UserID });

                    tmpuser.Roles = session.GetList<UserRole>(sqlBuilder.ToString(), parameters.ToArray()).ToList();

                }

                return tmpuser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 判断登陆账户是否重复

        /// <summary>
        /// 判断登陆账户是否重复
        /// </summary>
        /// <param name="user">条件</param>
        /// <returns>用户信息信息</returns>
        public User GetLoginUser(User user)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            User tmpuser = new User();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {

                    //获取用户信息
                    sqlBuilder.Append(" SELECT USERID, ");
                    sqlBuilder.Append(" LOGINUSERID,USERNAME,PASSWORD,TEL,  ");
                    sqlBuilder.Append(" MOBILETEL,EMAIL,CREATEUSER,CREATETIME,UPDATEUSER,UPDATETIME, ");
                    sqlBuilder.Append(" DELFLAG,ISSTOP  ");
                    sqlBuilder.Append(" FROM T_USER AS A WHERE LOGINUSERID = @LOGINUSERID  AND DELFLAG='0'");

                    parameters.Add(new DataParameter { ParameterName = "LOGINUSERID", DataType = DbType.String, Value = user.LoginUserID });

                    tmpuser = session.Get<User>(sqlBuilder.ToString(), parameters.ToArray());
                    
                }

                return tmpuser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 插入用户信息信息

        /// <summary>
        /// 插入用户信息信息
        /// </summary>
        /// <param name="user">用户信息信息</param>
        /// <returns>插入数</returns>
        public int Insert(User user)
        {
            int count = 0;

            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    
                    //插入基本信息
                    session.Insert<User>(user);

                    //插入用户角色信息
                    if (user.Roles != null)
                    {
                        foreach (UserRole ur in user.Roles)
                        {
                            ur.UserID = user.UserID;
                        }
                        //插入角色信息
                        session.Insert<UserRole>(user.Roles);
                    }

                    session.CommitTs();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除用户信息信息

        /// <summary>
        /// 删除用户信息信息
        /// </summary>
        /// <param name="user">用户信息信息</param>
        /// <returns>删除个数</returns>
        public int Delete(User user)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //删除用户所有角色信息
                    string sql = "DELETE from T_USERROLE WHERE USERID=@USERID";
                    count = session.ExecuteSql(sql, new DataParameter { ParameterName = "USERID", DataType = DbType.String, Value = user.UserID });
                    
                    //删除用户信息信息
                    sql = "DELETE from T_USER WHERE USERID=@USERID";
                    count = session.ExecuteSql(sql, new DataParameter { ParameterName = "USERID", DataType = DbType.String, Value = user.UserID });

                    session.CommitTs();
                }
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
        /// <param name="user">用户信息</param>
        /// <returns>更新个数</returns>
        public int Update(User user)
        {
            int count = 0;

            try
            {
                using (var session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    user.DelFlag = "0";
                    user.PassWord = DESEncrypt.Encrypt(user.PassWord);
                    //更新基本信息
                    count = session.Update<User>(user);

                    //更新角色信息
                    string rolesql = "DELETE FROM T_USERROLE WHERE USERID=@USERID";
                    session.ExecuteSql(rolesql, new DataParameter { ParameterName = "USERID", DataType = DbType.String, Value = user.UserID });

                    if (user.Roles != null)
                        session.Insert<UserRole>(user.Roles);

                    session.CommitTs();
                }
                return count;
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
        /// <returns>影响行数</returns>
        public int SetPassWord(User info)
        {
            int count = 0;
            string sql = null;
            try
            {
                sql = "UPDATE T_USER SET PASSWORD=@PASSWORD,UPDATEUSER=@UPDATEUSER WHERE USERID=@USERID";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    count = session.ExecuteSql(sql
                        , new DataParameter("PASSWORD", info.PassWord)
                        , new DataParameter("UPDATEUSER", info.UpdateUser)
                        , new DataParameter("USERID", info.UserID));
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
