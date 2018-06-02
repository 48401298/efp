using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.Entity;

namespace Manage.Web
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 主页信息
        /// </summary>
        public DefaultInfo MyDefaultInfo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (this.IsPostBack == false)
            {
                MyDefaultInfo = new DefaultInfo();                
            }
            this.lblUserName.Text = "当前用户：" + (Session["UserInfo"] as LoginInfo).UserName;
        }
    }
}