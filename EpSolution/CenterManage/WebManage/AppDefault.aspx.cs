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
    /// 智慧渔业移动终端主页面
    /// </summary>
    public partial class AppDefault : System.Web.UI.Page
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("AppLogin.aspx");
                return;
            }

            if (this.IsPostBack == false)
            {
                //this.BindMenu();
            }
        }

        #endregion
        
    }
}