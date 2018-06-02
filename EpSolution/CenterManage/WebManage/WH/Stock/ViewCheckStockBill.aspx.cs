using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Sys;
using Manage.BLL.WH;
using Manage.Entity.WH;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;
using LAF.Common.Serialization;
using LAF.Entity;

namespace Manage.Web.WH.Stock
{
    /// <summary>
    /// 浏览库存盘点单
    /// </summary>
    public partial class ViewCheckStockBill : ParentPage
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
           

        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            CheckStockBLL bll = null;
            CheckStockBill info = new CheckStockBill();
            try
            {
                bll = BLLFactory.CreateBLL<CheckStockBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.GetInfo(info);
                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.ID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                    this.BillDate.Text = info.BillDate.ToString("yyyy-MM-dd");
                }
                

                this.IsConfirmName.Text = info.IsConfirm == 0 ? "未确认" : "已确认";

                //绑定明细
                DataGridResult<CheckStockDetail> matList = new DataGridResult<CheckStockDetail>();
                matList.Total = 0;
                matList.Rows = info.Details;

                this.hiCheckList.Value = matList.GetJsonSource();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 返回

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageCheckStock.aspx");
        }

        #endregion

    }
}