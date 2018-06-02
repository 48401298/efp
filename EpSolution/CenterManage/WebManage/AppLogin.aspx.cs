using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.Entity;
using LAF.WebUI;

namespace Manage.Web
{
    /// <summary>
    /// 智慧渔业移动终端登录页面
    /// </summary>
    public partial class AppLogin : System.Web.UI.Page
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {

            }
        }

        #endregion        

        protected void btLogin_Click(object sender, EventArgs e)
        {
            LoginBLL bll = BLLFactory.CreateBLL<LoginBLL>();

            LoginInfo loginUser = new LoginInfo();

            loginUser.LoginUserID = this.inputUserName.Text;
            loginUser.PassWord = this.inputPassword.Text;

            loginUser = bll.IsLogin(loginUser);

            if (loginUser != null)
            {
                //用户名和密码输入正确
                Session["userInfo"] = loginUser;
                Response.Redirect("AppDefault.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('用户名或密码不正确');", true);
            }
        }
    }
}