using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;
using Manage.BLL.Inspect;
using Manage.Entity.Inspect;

namespace Manage.Web.Inspect
{
    /// <summary>
    /// 编辑用户信息
    /// </summary>
    public partial class InspectDataShow : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.InitForm();
                this.BindData();
            }
        }

        #endregion

        #region 初始化页面

        private void InitForm()
        {
            Tools.SetDateTimeControl(this.StartTime);
            Tools.SetDateTimeControl(this.EndTime);
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string DeviceCodeLink = Request.QueryString["DeviceCodeLink"];
            this.DeviceName.Text = DeviceCodeLink;
            InspectDataBLL bll = null;
            DataPage dp = new DataPage();
            InspectDataEntity condition = new InspectDataEntity();
            condition.DeviceCode = this.DeviceName.Text;
            condition.StartTime = this.StartTime.Text;
            condition.EndTime = this.EndTime.Text;
            
            try
            {
                bll = BLLFactory.CreateBLL<InspectDataBLL>();

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<InspectDataEntity> list = dp.Result as List<InspectDataEntity>;
                this.GvList.DataSource = list;
                this.GvList.DataBind();

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