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

namespace Manage.Web.WH.Out
{
    /// <summary>
    /// 出库（按条码拆包）
    /// </summary>
    public partial class OutMatSplitPackage : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                DataGridResult<OutStockDetail> matList = new DataGridResult<OutStockDetail>();
                matList.Total = 0;
                matList.Rows = new List<OutStockDetail>();
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

            //出库方式
            List<WHOutMode> imList = BLLFactory.CreateBLL<WHOutModeBLL>().GetList();
            dicts = imList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.OutStockMode, dicts, null);
        }

        #endregion

        #region 保存

        protected void btSave_Click(object sender, EventArgs e)
        {
            OutStockBill info = new OutStockBill();
            OutStockBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                info.Warehouse = this.Warehouse.SelectedValue;
                info.Details = LAF.Common.Serialization.JsonConvertHelper.DeserializeObject<List<OutStockDetail>>(this.hiMatList.Value);

                foreach (OutStockDetail detail in info.Details)
                {
                    detail.OutAmount = 0;
                    detail.SaveSite = this.SaveSite.SelectedValue;
                }

                bll = BLLFactory.CreateBLL<OutStockBLL>();
                string result = "";
                if (this.hiID.Value == "")
                {
                    result=bll.MLOutStorage(info);
                }
                if (result == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSA('提示', '出库完成', function () { window.location.href='OutMatSplitPackage.aspx'; });", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSA('提示', '" + result + "', function () { window.location.href='OutMatSplitPackage.aspx'; });", true);
                }
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