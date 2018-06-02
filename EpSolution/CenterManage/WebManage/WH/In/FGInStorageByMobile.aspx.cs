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
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;

namespace Manage.Web.WH.In
{
    /// <summary>
    /// 产成品入库（移动端）
    /// </summary>
    public partial class FGInStorageByMobile : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                DataGridResult<InStockDetail> matList = new DataGridResult<InStockDetail>();
                matList.Total = 0;
                matList.Rows = new List<InStockDetail>();
                this.hiMatList.Value = matList.GetJsonSource();

                this.InitForm();
            }
        }

        #endregion

        #region 初始化页面

        private void InitForm()
        {
            List<DictInfo> dicts = null;

            //绑定仓库
            List<Warehouse> whList = BLLFactory.CreateBLL<WarehouseBLL>().GetListByUserID(this.GetLoginInfo());
            dicts = whList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.Warehouse, dicts, null);

            if (whList.Count > 0)
            {
                //绑定仓位
                List<WHSite> siteList = BLLFactory.CreateBLL<WHSiteBLL>().GetList(whList[0].ID);
                dicts = siteList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
                Tools.BindDataToDDL(this.SaveSite, dicts, null);
            }
        }

        #endregion

        #region 保存

        protected void btSave_Click(object sender, EventArgs e)
        {
            InStockBill info = new InStockBill();
            InStockBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                info.Warehouse = this.Warehouse.SelectedValue;
                info.Details = LAF.Common.Serialization.JsonConvertHelper.DeserializeObject<List<InStockDetail>>(this.hiMatList.Value);

                foreach (InStockDetail detail in info.Details)
                {
                    detail.SaveSite = this.SaveSite.SelectedValue;
                    if (detail.MatBarCode == null)
                    {
                        detail.MatBarCode = "";
                    }
                }

                bll = BLLFactory.CreateBLL<InStockBLL>();

                if (this.hiID.Value == "")
                {
                    bll.FGInStorage(info);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('入库完成');window.location.href='MlInStorageByMobile.aspx';", true);
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
            Response.Redirect("../../HDDefault.aspx");
        }

        #endregion
    }
}