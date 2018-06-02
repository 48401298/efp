using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.BLL;
using Manage.Entity.Sys;
using Manage.DAL.Sys;
using System.Collections;

namespace Manage.BLL.Sys
{
    /// <summary>
    /// 组织机构管理
    /// </summary>
    public class OrgaizationManageBLL : BaseBLL
    {

        #region 获取组织机构树形列表

        /// <summary>
        /// 获取组织机构树形列表
        /// </summary>
        /// <returns>数据页</returns>
        public List<Orgaization> GetSubTreeList(Orgaization orga)
        {
            try
            {
                return new OrgaizationManageDAL().GetSubTreeList(orga);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 判断组织机构编号是否重复
        /// <summary>
        /// 判断组织机构编号是否重复
        /// </summary>
        /// <param name="orga">组织机构信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsOrga(Orgaization orga)
        {
            try
            {
                return new OrgaizationManageDAL().ExistsOrga(orga);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return new OrgaizationManageDAL().GetAllList();
            }
            catch (Exception ex)
            {
                throw ex;
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
                return new OrgaizationManageDAL().GetOrgaRoleList(orga);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取组织机构信息

        /// <summary>
        /// 获取组织机构信息
        /// </summary>
        /// <param name="role">条件</param>
        /// <returns>组织机构信息</returns>
        public Orgaization Get(Orgaization orga)
        {
            try
            {
                return new OrgaizationManageDAL().Get(orga);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 插入组织机构信息

        /// <summary>
        /// 插入组织机构
        /// </summary>
        /// <param name="orga">组织机构信息</param>
        /// <returns>插入数</returns>
        public int Insert(Orgaization organ)
        {
            try
            {
                organ.OrganID = Guid.NewGuid().ToString();
                organ.CreateTime = DateTime.Now;
                organ.UpdateTime = organ.CreateTime;

                return new OrgaizationManageDAL().Insert(organ);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除组织机构

        /// <summary>
        /// 删除组织机构信息
        /// </summary>
        /// <param name="role">组织机构信息</param>
        /// <returns>删除个数</returns>
        public int BatchDelete(ArrayList orgas)
        {
            int count = 0;
            try
            {
                foreach (string orga in orgas)
                {
                    count += this.Delete(new Orgaization { OrganID = orga });
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除组织机构信息
        /// </summary>
        /// <param name="role">组织机构信息</param>
        /// <returns>删除行数</returns>
        public int Delete(Orgaization organ)
        {
            int count = 0;

            try
            {
                count = new OrgaizationManageDAL().Delete(organ);
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
        /// <param name="role">组织机构信息</param>
        /// <returns>更新个数</returns>
        public int Update(Orgaization organ)
        {
            try
            {
                organ.UpdateUser = this.LoginUser.UserID;
                organ.UpdateTime = DateTime.Now;
                organ.DelFlag = "0";
                return new OrgaizationManageDAL().Update(organ);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
