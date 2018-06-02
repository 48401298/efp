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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            LoginBLL bll = BLLFactory.CreateBLL<LoginBLL>();

            LoginInfo loginUser = new LoginInfo();

            if (Session["CheckCode"] != null
                && this.txtCheckCode.Value.ToLower() != Session["CheckCode"].ToString().ToLower())
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('验证码错误！');", true);
                return;
            }

            loginUser.LoginUserID = this.txtUser.Text;
            loginUser.PassWord = this.txtPassWord.Text;

            loginUser = bll.IsLogin(loginUser);

            if (loginUser != null)
            {
                //用户名和密码输入正确
                Session["userInfo"] = loginUser;
                Response.Redirect("Default.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('用户名或密码不正确');", true);
            }
        }
    }
}