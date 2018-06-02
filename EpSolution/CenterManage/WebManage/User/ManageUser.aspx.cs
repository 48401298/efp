using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;
using System.Configuration;

namespace Manage.Web.User
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public partial class ManageUser : ParentPage
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
            UserManageBLL bll = null;
            DataPage dp = new DataPage();
            Manage.Entity.Sys.User condition = new Manage.Entity.Sys.User();

            try
            {
                bll = BLLFactory.CreateBLL<UserManageBLL>();
                condition.LoginUserID = this.LoginUserID.Text;
                condition.UserName = this.UserName.Text;
                condition.OrganID = this.OrganID.Value;
                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<Entity.Sys.User> list = dp.Result as List<Entity.Sys.User>;

                foreach (Entity.Sys.User user in list)
                {
                    if (string.IsNullOrEmpty(user.OrganID) == false && user.OrganID == "root")
                    {
                        user.OrganDESC = ConfigurationManager.AppSettings["rootOrgan"];
                    }
                }

                this.GvList.DataSource = list;
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["UserID"].ToString());

                    (this.GvList.Rows[i].Cells[8].Controls[0] as WebControl).Attributes.Add("onclick", click);

                    LinkButton btWHPowers =this.GvList.Rows[i].Cells[9].FindControl("btWHPowers") as LinkButton;

                    btWHPowers.OnClientClick = "setWHPowers('" + this.GvList.DataKeys[i]["UserID"].ToString() + "');return false;";
                }
                PagerHelper.SetPageControl(AspNetPager1, dp, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 查询

        protected void btQuery_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        #endregion

        #region 增加

        protected void btAdd_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 删除

        protected void btDelete_Click(object sender, EventArgs e)
        {
            ArrayList pkArray = null;
            UserManageBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<UserManageBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    bll.Delete(new Entity.Sys.User { UserID = key.ToString() });
                }

                this.BindData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 设置样式

        protected void GvList_PreRender(object sender, EventArgs e)
        {
            GvHelper.DatagridSkin(this.GvList);
        }

        protected void GvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GvHelper.DatagridSkinUpdate(e);
        }

        #endregion

        #region 分页

        protected void AspNetPager1_PageChanged(object src, Wuqi.Webdiyer.PageChangedEventArgs e)
        {
            this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }

        #endregion
    }
}