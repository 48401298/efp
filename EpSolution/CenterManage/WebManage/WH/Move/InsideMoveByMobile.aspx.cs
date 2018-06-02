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

namespace Manage.Web.WH.Move
{
    /// <summary>
    /// 库内移动（移动端）
    /// </summary>
    public partial class InsideMoveByMobile : ParentPage
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
            Tools.BindDataToDDL(this.ToWarehouse, dicts, null);

            if (whList.Count > 0)
            {
                //绑定仓位
                List<WHSite> siteList = BLLFactory.CreateBLL<WHSiteBLL>().GetList(whList[0].ID);
                dicts = siteList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
                Tools.BindDataToDDL(this.FromSaveSite, dicts, null);
                Tools.BindDataToDDL(this.ToSaveSite, dicts, "");
            }
        }

        #endregion

        #region 保存

        protected void btSave_Click(object sender, EventArgs e)
        {
            MoveStockBill info = new MoveStockBill();
            MoveStockBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                info.FromWarehouse = this.ToWarehouse.SelectedValue;
                info.ToWarehouse = this.ToWarehouse.SelectedValue;
                info.Details = LAF.Common.Serialization.JsonConvertHelper.DeserializeObject<List<MoveStockDetail>>(this.hiMatList.Value);

                foreach (MoveStockDetail detail in info.Details)
                {
                    if (detail.IDCode == null)
                    {
                        detail.IDCode = "";
                    }
                }

                bll = BLLFactory.CreateBLL<MoveStockBLL>();

                if (this.hiID.Value == "")
                {
                    bll.InsideMoveStock(info);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSA('提示', '移库完成', function () { window.location.href='InsideMoveByMobile.aspx'; });", true);
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