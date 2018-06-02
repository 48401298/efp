using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.Sys;
using System.Data;

namespace Manage.DAL.Sys
{
    /// <summary>
    /// 组织机构管理
    /// </summary>
    public class OrgaizationManageDAL
    {
        #region 获取组织机构信息

        /// <summary>
        /// 获取组织机构信息
        /// </summary>
        /// <param name="orga">条件</param>
        /// <returns>组织机构信息</returns>
        public Orgaization Get(Orgaization orga)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            Orgaization tmporga = new Orgaization();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {

                    //获取用户信息
                    sql = "SELECT ORGANID,ORGANDESC,ORGANPARENT,CREATEUSER,CREATETIME,UPDATEUSER,UPDATETIME,DELFLAG FROM T_ORGANIZATION AS A WHERE ORGANID=@ORGANID";

                    parameters.Add(new DataParameter { ParameterName = "ORGANID", DataType = DbType.String, Value = orga.OrganID });

                    tmporga = session.Get<Orgaization>(sql, parameters.ToArray());

                }


                return tmporga;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 组织机构编号是否重复

        /// <summary>
        /// 判断组织机构编号是否重复
        /// </summary>
        /// <param name="orga">组织机构信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsOrga(Orgaization orga)
        {
            string organID = "";
            int count = 0;
            string sql = null;
            try
            {
                if (string.IsNullOrEmpty(orga.OrganID) == false)
                {
                    organID = orga.OrganID;
                }

                sql = "SELECT COUNT(*) FROM T_ORGANIZATION WHERE OrganID <> @OrganID OR ORGANDESC=@ORGANDESC";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    count = (int)session.ExecuteSqlScalar(sql, new DataParameter { ParameterName = "OrganID", Value = organID }
                        , new DataParameter { ParameterName = "ORGANDESC", Value = orga.OrganDESC });
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
        
        #region 获取组织机构所有的角色信息

        /// <summary>
        /// 获取组织机构所有的角色信息
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>同级别菜单列表</returns>
        public Orgaization GetOrgaRoleList(Orgaization orga)
        {
            try
            {
                Orgaization list = new Orgaization();
                StringBuilder sb = new StringBuilder();
                List<DataParameter> parameters = new List<DataParameter>();
                list.OrgaRoleList = new List<RoleInfo>();
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    sb.Append("SELECT * FROM T_ROLE WHERE  1=1 ");
                    //如果组织机构list不为空，那么证明是非admin登陆
                    if (orga.Orgas != null)
                    {
                        sb.Append(" AND EXISTS (SELECT ORGANID FROM T_ORGANIZATION WHERE T_ROLE.ORGANID = ORGANID AND ( 1!=1 ");
                        int tmpcount = 0;
                        foreach (var item in orga.Orgas)
                        {

                            sb.Append(" OR ORGANID=@ORGANID" + tmpcount);
                            parameters.Add(new DataParameter { ParameterName = "ORGANID" + tmpcount, DataType = DbType.String, Value = item.OrganID });
                            tmpcount++;
                        }
                        sb.Append("))");
                    }
                    
                    list.OrgaRoleList = session.GetList<RoleInfo>(sb.ToString(), parameters.ToArray()).ToList();
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 获取组织机构数列表

        /// <summary>
        /// 获取同一级别的所有组织机构数列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>同级别菜单列表</returns>
        public List<Orgaization> GetSubTreeList(Orgaization orga)
        {
            try
            {
                List<DataParameter> parameters = new List<DataParameter>();
                List<Orgaization> list = null;
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = "SELECT * FROM T_ORGANIZATION WHERE DELFLAG='0' AND ORGAPARENT=@ORGAPARENT";
                    parameters.Add(new DataParameter { ParameterName = "ORGAPARENT", DataType = DbType.String, Value = orga.OrganParent });
                    list = session.GetList<Orgaization>(sql, parameters.ToArray()).ToList();
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 获取组织机构数列表

        /// <summary>
        /// 获取所有组织机构数列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>同级别菜单列表</returns>
        public List<Orgaization> GetAllList()
        {
            try
            {
                List<DataParameter> parameters = new List<DataParameter>();
                List<Orgaization> list = null;
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = "SELECT * FROM T_ORGANIZATION WHERE DELFLAG='0'";

                    list = session.GetList<Orgaization>(sql, parameters.ToArray()).ToList();
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 插入组织机构信息

        /// <summary>
        /// 插入组织机构信息
        /// </summary>
        /// <param name="orga">组织机构信息</param>
        /// <returns>插入数</returns>
        public int Insert(Orgaization orga)
        {
            int count = 0;

            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    orga.DelFlag = "0";
                    if (String.IsNullOrEmpty(orga.OrganParent))
                    {
                        orga.OrganParent = "0";
                    }
                    //插入基本信息
                    session.Insert<Orgaization>(orga);

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

        #region 删除组织机构信息

        /// <summary>
        /// 删除组织机构信息
        /// </summary>
        /// <param name="orga">组织机构信息</param>
        /// <returns>删除个数</returns>
        public int Delete(Orgaization orga)
        {
            int count = 0;
            string sql = null;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //先删除组织机构所有权限信息
                    sql = "DELETE T_ORGAIZATIONAUTHORITY WHERE ORGANID=@ORGANID";
                    count = session.ExecuteSql(sql, new DataParameter { ParameterName = "ORGANID", DataType = DbType.String, Value = orga.OrganID });



                    //删除组织机构信息
                    sql = "UPDATE T_ORGANIZATION SET DELFLAG = '1' WHERE ORGANID=@ORGANID";
                    count = session.ExecuteSql(sql, new DataParameter { ParameterName = "ORGANID", DataType = DbType.String, Value = orga.OrganID });

                    
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

        #region 更新组织机构

        /// <summary>
        /// 更新组织机构
        /// </summary>
        /// <param name="orga">组织机构</param>
        /// <returns>更新个数</returns>
        public int Update(Orgaization orga)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //更新基本信息
                    count = session.Update<Orgaization>(orga);

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
