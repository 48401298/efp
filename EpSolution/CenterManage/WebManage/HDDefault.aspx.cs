using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.Entity;
using LAF.WebUI;
using LAF.WebUI.Util;
using LAF.WebUI.Menu;
using Manage.Web.Util;


namespace Manage.Web
{
    /// <summary>
    /// 手持系统主页面
    /// </summary>
    public partial class HDDefault : System.Web.UI.Page
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("HDLogin.aspx");
                return;
            }

            if (this.IsPostBack == false)
            {
                this.BindMenu();
            }
        }

        #endregion

        #region 绑定菜单

        private void BindMenu()
        {
            LoginInfo login = new ParentPage().GetLoginInfo();

            //加载菜单
            MenuHelper helper = new MenuHelper();

            if (login.LoginUserID.ToLower() != "admin")
            {
                helper.Powers = login.Powers;
            }

            List<MenuInfo> menus = helper.GetMenuInfos(Server.MapPath("~/App_Data/Menu.xml"));

            MenuInfo findMenu = menus.Find(p => p.MenuID == "HD");

            if (findMenu != null)
            {
                menus = findMenu.ChildMenus;

                //绑定主菜单
                foreach (MenuInfo m in menus)
                {
                    this.MainMenu.Text += string.Format("<button class=\"btn btn-app btn-primary\" onclick=\"{0}return false;\" style=\"height:100px;width:150px\">{1}</button>", m.Action, m.MenuDes);
                }
            }
        }

        #endregion
    }
}