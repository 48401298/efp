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

namespace Manage.Web.WH.In
{
    /// <summary>
    /// 编辑原材料入库单
    /// </summary>
    public partial class EditMlInBill : ParentPage
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
            List<DictInfo> dicts = null;

            Tools.SetDateTimeControl(this.BillDate);

            //绑定仓库
            List<Warehouse> whList = BLLFactory.CreateBLL<WarehouseBLL>().GetListByUserID(this.GetLoginInfo());
            dicts = whList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.Warehouse, dicts, null);

            //入库方式
            List<WHInMode> imList = BLLFactory.CreateBLL<WHInModeBLL>().GetList();
            dicts = imList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.InStockMode, dicts, "");

            //供货单位
            List<WHProvider> wpList = BLLFactory.CreateBLL<WHProviderBLL>().GetList();
            dicts = wpList.Select(p => new DictInfo { ID = p.ID, Des = p.ProviderName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.ProviderID, dicts, "");

            //仓库负责人
            List<Manage.Entity.Sys.User> userList = BLLFactory.CreateBLL<UserManageBLL>().GetAllUsers(new Manage.Entity.Sys.User());
            dicts = userList.Select(p => new DictInfo { ID = p.UserID, Des = p.UserName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.WHHeader, dicts, "");

            //验收人
            dicts = userList.Select(p => new DictInfo { ID = p.UserID, Des = p.UserName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.Receiver, dicts, "");

        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            InStockBLL bll = null;
            InStockBill info = new InStockBill();
            try
            {
                bll = BLLFactory.CreateBLL<InStockBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.GetInfo(info);
                    info.Details = info.Details.OrderBy(p => p.Seq).ToList();
                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.ID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiUPDATEUSER.Value = info.UPDATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                    this.btAdd.Visible = false;
                    this.BillDate.Text = info.BillDate.ToString("yyyy年MM月dd日");
                }
                else
                {
                    info = new InStockBill();
                    info.Details = new List<InStockDetail>();
                    this.BillNO.Text = bll.GetNewBillNO();
                    this.BillDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
                }

                //绑定明细
                DataGridResult<InStockDetail> matList = new DataGridResult<InStockDetail>();
                matList.Total = 0;
                matList.Rows = info.Details;

                foreach (InStockDetail detail in info.Details)
                {
                    detail.UnitName = string.IsNullOrEmpty(detail.UnitName) == false ? detail.UnitName : detail.MainUnitName;
                    detail.InSpecName = string.IsNullOrEmpty(detail.InSpecName) == false ? detail.InSpecName : detail.SpecCode;
                    detail.DeleteAction = "none";
                }

                this.hiMatList.Value = matList.GetJsonSource();
            }
            catch (Exception ex)
            {
                throw ex;
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

                bll = BLLFactory.CreateBLL<InStockBLL>();

                info.Details = JsonConvertHelper.DeserializeObject<List<InStockDetail>>(this.hiMatList.Value);

                if (this.hiID.Value == "")
                {
                    bll.InStorage(info);                   
                }
                else
                {
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.ID = this.hiID.Value;
                    bll.Update(info);                    
                }
                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.refreshData();parent.closeAppWindow1();", true);
                
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
            Response.Redirect("ManageInStockBill.aspx");
        }

        #endregion
    }
}