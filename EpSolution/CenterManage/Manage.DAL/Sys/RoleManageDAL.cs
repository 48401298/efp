using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.Sys;

namespace Manage.DAL.Sys
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleManageDAL:BaseDAL
    {
        #region 获取角色信息

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="role">条件</param>
        /// <returns>角色信息</returns>
        public RoleInfo Get(RoleInfo role)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            RoleInfo r = new RoleInfo();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取角色信息
                    sql = "SELECT ROLEID,ROLEDESC,CREATEUSER,CREATETIME,UPDATEUSER,UPDATETIME FROM T_ROLE WHERE ROLEID=@ROLEID";

                    parameters.Add(new DataParameter { ParameterName = "ROLEID", DataType = DbType.String, Value = role.RoleID });

                    sql = this.ChangeSqlByDB(sql, session);
                    r = session.Get<RoleInfo>(sql, parameters.ToArray());

                    //获取权限信息
                    sql.Remove(0,sql.Length);
                    parameters = new List<DataParameter>();
                    sql = "SELECT * FROM T_ROLEAUTHORITY WHERE ROLEID=@ROLEID";

                    parameters.Add(new DataParameter { ParameterName = "ROLEID", DataType = DbType.String, Value = role.RoleID });
                    sql = this.ChangeSqlByDB(sql, session);
                    r.Powers = session.GetList<RoleAuthority>(sql, parameters.ToArray()).ToList();

                }


                return r;
            }
            catch (Exception ex)
            {
                throw;
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
                StringBuilder sqlBuilder = new StringBuilder();
                StringBuilder whereBuilder = new StringBuilder();
                List<DataParameter> parameters = new List<DataParameter>();

                sqlBuilder.Append("SELECT ROLEID,ROLEDESC,CREATEUSER,CREATETIME,UPDATEUSER,UPDATETIME FROM T_ROLE WHERE 1=1 ");
                
                //查询条件
                if (string.IsNullOrEmpty(condition.RoleDESC) == false)
                {
                    whereBuilder.Append(" AND ROLEDESC=@ROLEDESC  ");
                    parameters.Add(new DataParameter { ParameterName = "ROLEDESC", DataType = DbType.String, Value = condition.RoleDESC });
                }
                
                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(whereBuilder.ToString());
                }

                //分页关键字段及排序
                page.KeyName = "ROLEID";
                if (string.IsNullOrEmpty(page.SortExpression))
                {
                    page.SortExpression = "UPDATETIME DESC";
                }

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = this.ChangeSqlByDB(sqlBuilder.ToString(), session);
                    page = session.GetDataPage<RoleInfo>(sql, parameters.ToArray(), page);
                }

                return page;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取全部角色信息
        /// </summary>
        /// <returns>数据列表</returns>
        public List<RoleInfo> GetAll()
        {
            try
            {
                List<RoleInfo> list = null;
                List<DataParameter> parameters = new List<DataParameter>();

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {

                    string sql = "SELECT * FROM T_ROLE ";
                    list = session.GetList<RoleInfo>(sql, parameters.ToArray()).ToList();
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 角色信息是否重复

        /// <summary>
        /// 判断角色名称是否存在
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsRole(RoleInfo role)
        {
            string roleID = "";
            int count = 0;
            string sql = null;
            try
            {
                if (string.IsNullOrEmpty(role.RoleID) == false)
                {
                    roleID = role.RoleID;
                }

                sql = "SELECT COUNT(*) FROM T_ROLE WHERE ROLEID <> @ROLEID AND ROLEDESC=@ROLEDESC";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    sql = this.ChangeSqlByDB(sql, session);
                    count = (int)session.ExecuteSqlScalar(sql, new DataParameter { ParameterName = "ROLEID", Value = roleID }
                        , new DataParameter { ParameterName = "ROLEDESC", Value = role.RoleDESC });
                }

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw;
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
            int count = 0;

            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //插入基本信息
                    session.Insert<RoleInfo>(role);
                    if (role.Powers != null)
                    {
                        foreach(RoleAuthority ra in role.Powers){
                            ra.RoleID = role.RoleID;
                        }
                        //插入权限信息
                        session.Insert<RoleAuthority>(role.Powers);
                    }
                    session.CommitTs();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>删除个数</returns>
        public int Delete(RoleInfo role)
        {
            int count = 0;
            string sql = null;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //删除权限信息，子表
                    sql = "DELETE FROM T_ROLEAUTHORITY WHERE ROLEID=@ROLEID";
                    sql = this.ChangeSqlByDB(sql, session);
                    session.ExecuteSql(sql, new DataParameter { ParameterName = "ROLEID", DataType = DbType.String, Value = role.RoleID });

                    //删除基本信息，主表
                    sql.Remove(0, sql.Length);
                    sql = "DELETE FROM T_ROLE WHERE ROLEID=@ROLEID";
                    sql = this.ChangeSqlByDB(sql, session);
                    session.ExecuteSql(sql, new DataParameter { ParameterName = "ROLEID", DataType = DbType.String, Value = role.RoleID });


                    session.CommitTs();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
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
            int count = 0;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //更新基本信息
                    session.Update<RoleInfo>(role);

                    //更新权限信息
                    sql = "DELETE FROM T_ROLEAUTHORITY WHERE ROLEID=@ROLEID";
                    sql = this.ChangeSqlByDB(sql, session);
                    session.ExecuteSql(sql, new DataParameter { ParameterName = "ROLEID", DataType = DbType.String, Value = role.RoleID });

                    if (role.Powers != null)
                        session.Insert<RoleAuthority>(role.Powers);


                    session.CommitTs();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
