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
    public partial class LeftMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginInfo login = new ParentPage().GetLoginInfo();

            //加载菜单
            MenuHelper helper = new MenuHelper();
            if (login.LoginUserID.ToLower() != "admin")
            {
                helper.Powers = login.Powers;
            }
            List<MenuInfo> menus = helper.GetMenuInfos();
            menus = menus.Where(p => p.MenuID != "HD").ToList();

            OutLookMenu menu = new OutLookMenu();
            this.Menu.Text = menu.GetMenuHtml(menus); 
        }
    }
}