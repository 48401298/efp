using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI;
using LAF.Common.Serialization;
using Manage.BLL.MES;
using LAF.WebUI.Util;
using Manage.BLL.WH;
using Manage.Entity.MES;
using LAF.WebUI.DataSource;
using Manage.Entity.WH;

namespace Manage.Web.WH.In
{
    /// <summary>
    /// 查看领料单
    /// </summary>
    public partial class ViewUseMatBill : ParentPage
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
            UseMatBLL bll = null;
            UseMatBill info = new UseMatBill();
            try
            {
                bll = BLLFactory.CreateBLL<UseMatBLL>();
                if (!string.IsNullOrEmpty(id))
                {
                    info.PID = id;
                    info = bll.Get(info);
                    UIBindHelper.BindForm(this.Page, info);
                    PDNAME.Text = info.ProduceName;
                    FNAME.Text = info.FactoryName;
                    REMARK.Text = info.REMARK;
                    DELIVERYDATE.Text = info.DELIVERYDATE.ToString();
                }

                //绑定明细
                DataGridResult<UseMatAmount> supplyMaterialInfo = new DataGridResult<UseMatAmount>();
                supplyMaterialInfo.Total = info.Amounts.Count;
                supplyMaterialInfo.Rows = info.Amounts;

                this.hiMaterialList.Value = supplyMaterialInfo.GetJsonSource();

                DataGridResult<UseMatDetail> details = new DataGridResult<UseMatDetail>();
                details.Total = info.Details.Count;
                details.Rows = info.Details;

                this.hiUseMatList.Value = details.GetJsonSource();
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
            Response.Redirect("ManageSupplyInfo.aspx");
        }

        #endregion
    }
}