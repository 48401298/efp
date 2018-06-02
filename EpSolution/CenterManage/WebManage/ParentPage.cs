
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LAF.WebUI;
using LAF.Entity;

namespace Manage.Web
{
    /// <summary>
    /// 页面祖先类
    /// </summary>
    public class ParentPage : System.Web.UI.Page
    {
        /// <summary>
        /// 动态CSS的占位

        /// </summary>
        protected System.Web.UI.WebControls.PlaceHolder CssHolder;
        /// <summary>
        /// 功能标识
        /// </summary>
        protected string FunctionID = null;

        public ParentPage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            

            //OnUseKeyPage_Load();

        }

        /// <summary>
        /// 动态设置CSS样式表，需要在HTML文件的Header区 ，放置一个PlaceHoler服务器控件
        /// </summary>
        /// <returns></returns>
        public void SetCSS(string cssFileName)
        {
            if (this.CssHolder == null)
                return;
            //指定的标记"LINK"初始化此类的新实例. 
            HtmlGenericControl objLink = new HtmlGenericControl("LINK");
            objLink.Attributes["rel"] = "stylesheet";
            objLink.Attributes["type"] = "text/css";
            objLink.Attributes["href"] = WebUIGlobal.SiteRoot + cssFileName + ".css";


            //此控件不产生任何可见输出,仅作为其他控件的容器,可在其中添加,插入或移除控件. 
            this.CssHolder.Controls.Add(objLink);

        }

        /// <summary>
        /// 动态设置CSS样式表，需要在HTML文件的Header区 ，放置一个PlaceHoler服务器控件
        /// </summary>
        /// <returns></returns>
        public void SetCSS(PlaceHolder cssPosition, string cssFileName)
        {
            if (cssPosition == null)
                return;
            //指定的标记"LINK"初始化此类的新实例. 
            HtmlGenericControl objLink = new HtmlGenericControl("LINK");
            objLink.Attributes["rel"] = "stylesheet";
            objLink.Attributes["type"] = "text/css";
            objLink.Attributes["href"] = WebUIGlobal.SiteRoot + "CSS/" + cssFileName + ".css";


            //此控件不产生任何可见输出,仅作为其他控件的容器,可在其中添加,插入或移除控件. 
            cssPosition.Controls.Add(objLink);

        }

        /// <summary>
        /// 动态设置脚本文件，需要在HTML文件的Header区 ，放置一个PlaceHoler服务器控件

        /// </summary>
        /// <returns></returns>
        public void SetScript(PlaceHolder cssPosition, string scriptFileName)
        {
            if (cssPosition == null)
                return;
            //指定的标记"LINK"初始化此类的新实例. 
            HtmlGenericControl objLink = new HtmlGenericControl("Script");
            objLink.Attributes["src"] = WebUIGlobal.SiteRoot + scriptFileName + ".js";

            //此控件不产生任何可见输出,仅作为其他控件的容器,可在其中添加,插入或移除控件. 
            cssPosition.Controls.Add(objLink);

        }

        /// <summary>
        /// 权限校验
        /// </summary>
        private void CheckSession()
        {
            if (Session["UserInfo"] == null)
            {
                throw new Exception("Session 超时！您长时间没有操作，为了保护数据，需要重新登陆.");
            }
        }

        /// <summary>
        /// 权限编号校验
        /// </summary>
        /// <returns></returns>
        public bool IsPowerCode(string powerID)
        {
            LoginInfo user = (Session["UserInfo"] as LoginInfo) as LoginInfo;

            if (user.Powers.IndexOf(powerID) >= 0)
                return true;
            else
                return false;
        }

       
        /// <summary>
        /// 接收查询条件
        /// </summary>
        /// <param name="conditon">查询条件</param>
        public void AccceptQueryCondition(object conditon)
        {
            this.ViewState.Add("QueryCondition", conditon);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        public object GetQueryCondition()
        {
            object condition = this.ViewState["QueryCondition"];

            return condition;
        }

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。


            //            

            this.CheckSession();

            if (this.Session != null)
                this.ViewStateUserKey = Session.SessionID;

            //if (new BusinessHelper().IsLockData() == true)
            //    throw new Exception("数据被锁定，禁止操作。");

            InitializeComponent();

            this.CssHolder = this.FindControl("MyCSS") as System.Web.UI.WebControls.PlaceHolder;

            if (this.CssHolder != null)
            {
                this.SetScript(this.CssHolder, "JS/jquery.min-1.11.1");
                this.SetScript(this.CssHolder, "easyui/jquery.easyui.min");
                this.SetScript(this.CssHolder, "easyui/locale/easyui-lang-zh_CN");


                this.SetScript(this.CssHolder, "JS/CommonActions");
                this.SetScript(this.CssHolder, "JS/DatePicker/WdatePicker");

                this.SetCSS("easyui/themes/bootstrap/easyui");
                this.SetCSS("easyui/themes/icon");
                this.SetCSS("css/Site");  

            }

            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("cache-control", "no-cache");
            Response.AddHeader("expires", "-1");

            base.OnInit(e);
       


        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改


        /// 此方法的内容。


        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region 获取登录信息

        public LoginInfo GetLoginInfo()
        {
            LoginInfo login = Session["UserInfo"] as LoginInfo;

            return login;
        }

        #endregion


    }
}
