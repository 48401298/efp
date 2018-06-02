using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;
using System.Configuration;

namespace Manage.Web.User
{
    /// <summary>
    /// 编辑用户信息
    /// </summary>
    public partial class EditUser : ParentPage
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
            UserManageBLL bll = null;
            Entity.Sys.User info = new Entity.Sys.User();
            try
            {
                bll = BLLFactory.CreateBLL<UserManageBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {                    
                    info.UserID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.OrganID.Value = info.OrganID;
                    this.RPassWord.Text = this.PassWord.Text;
                    this.hiID.Value = info.UserID;
                    this.HiCREATEUSER.Value = info.CreateUser;
                    this.HiCREATETIME.Value = info.CreateTime.ToString();
                    this.IsStop.Value = info.IsStop;
                }
                else
                {
                    info = new Entity.Sys.User();
                    info.Roles = new List<UserRole>();
                }

                this.HiRoleList.Value = this.GetTreeRoles(info);
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
        public string GetTreeRoles(Entity.Sys.User user)
        {
            List<TreeNodeResult> list = new List<TreeNodeResult>();
            List<Entity.Sys.RoleInfo> roles = null;
            try
            {
                roles = new RoleManageBLL().GetAll();
                foreach (Entity.Sys.RoleInfo r in roles)
                {
                    TreeNodeResult node = new TreeNodeResult();
                    node.Tid = r.RoleID;
                    node.Ttext = r.RoleDESC;

                    node.TChecked = user.Roles.Exists(p => p.RoleID == r.RoleID);

                    list.Add(node);
                }
                return TreeNodeResult.GetResultJosnS(list.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 保存

        protected void btSave_Click(object sender, EventArgs e)
        {
            Entity.Sys.User info = new Entity.Sys.User();
            UserManageBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                info.OrganID = this.OrganID.Value;
                bll = BLLFactory.CreateBLL<UserManageBLL>();

                //绑定权限
                if (this.HiSelectedRoleList.Value != "")
                {
                    string[] roles = this.HiSelectedRoleList.Value.Split(",".ToCharArray());

                    info.Roles = new List<UserRole>();

                    foreach (string roleID in roles)
                    {
                        UserRole r = new UserRole();
                        r.RoleID = roleID;
                        info.Roles.Add(r);
                    }
                }
                else
                {
                    info.Roles = new List<UserRole>();
                }

                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                }
                else
                {
                    info.CreateUser = this.HiCREATEUSER.Value;
                    info.CreateTime = DateTime.Parse(this.HiCREATETIME.Value);
                    info.UserID = this.hiID.Value;
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