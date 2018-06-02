using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.WH;
using Manage.Entity.WH;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;

namespace Manage.Web.WH.Query
{
    /// <summary>
    /// 入库查询明细
    /// </summary>
    public partial class ViewInMatList : ParentPage
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
            this.MatID.Value = Request.QueryString["MatID"];
            this.WarehouseID.Value = Request.QueryString["WarehouseID"];
            this.StartDate.Value = Request.QueryString["StartDate"];
            this.EndDate.Value = Request.QueryString["EndDate"];
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            InStockQueryBLL bll = null;
            DataPage dp = new DataPage();
            InStockBill condition = new InStockBill();

            try
            {
                bll = BLLFactory.CreateBLL<InStockQueryBLL>();
                condition.StartDate = this.StartDate.Value;
                condition.EndDate = this.EndDate.Value;
                condition.Warehouse = this.WarehouseID.Value;
                condition.MatID = this.MatID.Value;
                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetInMatDetails(condition, dp);

                List<InStockQueryResult> list = dp.Result as List<InStockQueryResult>;

                foreach (InStockQueryResult detail in list)
                {
                    detail.UnitName = string.IsNullOrEmpty(detail.UnitName) == false ? detail.UnitName : detail.MainUnitName;
                }

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