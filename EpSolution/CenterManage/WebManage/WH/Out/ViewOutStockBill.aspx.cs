using System;
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

namespace Manage.Web.WH.Out
{
    /// <summary>
    /// 查看出库单
    /// </summary>
    public partial class ViewOutStockBill : ParentPage
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
            string id = Request.QueryString["id"];
            OutStockBLL bll = null;
            OutStockBillView info = new OutStockBillView();
            try
            {
                bll = BLLFactory.CreateBLL<OutStockBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.GetViewInfo(info);

                    UIBindHelper.BindForm(this.Page, info);

                    foreach (OutStockDetailView detail in info.Details)
                    {
                        detail.UnitName = string.IsNullOrEmpty(detail.UnitName) == false ? detail.UnitName : detail.MainUnitName;
                        if (detail.OutAmount == 0)
                        {
                            detail.OutAmount = detail.MainUnitAmount;
                            detail.UnitName = detail.MainUnitName;
                        }
                    }

                    this.GvList.DataSource = info.Details;
                    this.GvList.DataBind();
                }
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

        #region 返回

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageOutStockBill.aspx");
        }

        #endregion
    }
}