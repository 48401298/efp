using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.Entity.Sys;
using Manage.BLL.Sys;
using LAF.Entity;
using LAF.WebUI;

namespace Manage.Web.User
{
    public partial class ModifyPwd :ParentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            Manage.Entity.Sys.User info = new Manage.Entity.Sys.User();
            info.PassWord = this.NewPassWord.Text;

            LoginInfo login = this.GetLoginInfo();
            login.PassWord = this.OldPassWord.Text;

            login = new LoginBLL().IsLogin(login);

            if (login == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSI('提示','原密码输入不正确！');", true);
                return;
            }
            info.UserID = login.UserID;
            BLLFactory.CreateBLL<UserManageBLL>().SetPassWord(info);

            ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.closeAppWindow1();", true);
        }
    }
}