using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;

namespace Manage.Web.Role
{
    /// <summary>
    /// 编辑角色信息
    /// </summary>
    public partial class EditRole : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindData();
            }
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            RoleManageBLL bll = null;
            Entity.Sys.RoleInfo info = new Entity.Sys.RoleInfo();
            try
            {
                bll = BLLFactory.CreateBLL<RoleManageBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.RoleID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.RoleID;
                    this.HiCREATEUSER.Value = info.CreateUser;
                    this.HiCREATETIME.Value = info.CreateTime.ToString();
                }
                else
                {
                    info = new RoleInfo();
                    info.Powers = new List<RoleAuthority>();
                }

                this.HiPowerList.Value = this.GetTreePowers(info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取权限树数据源

        /// <summary>
        /// 获取权限树数据源
        /// </summary>
        /// <returns></returns>
        public string GetTreePowers(RoleInfo role)
        {
            List<TreeNodeResult> list = new List<TreeNodeResult>();
            List<MenuInfo> menus = null;
            try
            {
                menus = new MenuHelper().GetMenuInfos();

                foreach (MenuInfo info in menus)
                {
                    TreeNodeResult node = new TreeNodeResult();
                    node.Tid = info.MenuID;
                    node.Ttext = info.MenuDes;

                    node.TChecked = role.Powers.Exists(p => p.AuthorityID == info.MenuID);

                    //添加子权限
                    this.BuildChildItems(node, info.ChildMenus, role);

                    list.Add(node);
                }
                return TreeNodeResult.GetResultJosnS(list.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建子权限
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="childPowers">子权限</param>
        private void BuildChildItems(TreeNodeResult parentNode, List<MenuInfo> childPowers, RoleInfo role)
        {
            foreach (MenuInfo info in childPowers)
            {
                TreeNodeResult node = new TreeNodeResult();
                node.Tid = info.MenuID;
                node.Ttext = info.MenuDes;
                node.TChecked = role.Powers.Exists(p => p.AuthorityID == info.MenuID);
                //如果为空，那么结束循环
                if (info.ChildMenus != null)
                {
                    //添加子权限
                    this.BuildChildItems(node, info.ChildMenus, role);

                }
                parentNode.AddchildNode(node);
            }
        }

        #endregion

        #region 保存

        protected void btSave_Click(object sender, EventArgs e)
        {
            RoleInfo info = new RoleInfo();
            RoleManageBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<RoleManageBLL>();

                //绑定权限
                if (this.HiSelectedPowerList.Value != "")
                {
                    string[] powers = this.HiSelectedPowerList.Value.Split(",".ToCharArray());

                    info.Powers = new List<RoleAuthority>();

                    foreach (string powerID in powers)
                    {
                        RoleAuthority p = new RoleAuthority();
                        p.AuthorityID = powerID;
                        info.Powers.Add(p);
                    }
                }

                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                }
                else
                {
                    info.CreateUser = this.HiCREATEUSER.Value;
                    info.CreateTime = DateTime.Parse(this.HiCREATETIME.Value);
                    info.RoleID = this.hiID.Value;
                    bll.Update(info);

                }

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.refreshData();parent.closeAppWindow1();", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}