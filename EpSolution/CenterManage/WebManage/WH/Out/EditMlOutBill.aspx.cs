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

namespace Manage.Web.WH.Out
{
    /// <summary>
    /// 编辑出库单
    /// </summary>
    public partial class EditMlOutBill : ParentPage
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

            //出库方式
            List<WHOutMode> imList = BLLFactory.CreateBLL<WHOutModeBLL>().GetList();
            dicts = imList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.OutStockMode, dicts, "");

            //收货单位
            List<WHClient> wpList = BLLFactory.CreateBLL<WHClientBLL>().GetList();
            dicts = wpList.Select(p => new DictInfo { ID = p.ID, Des = p.ClientName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.ClientCode, dicts, "");

            //仓库负责人
            List<Manage.Entity.Sys.User> userList = BLLFactory.CreateBLL<UserManageBLL>().GetAllUsers(new Manage.Entity.Sys.User());
            dicts = userList.Select(p => new DictInfo { ID = p.UserID, Des = p.UserName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.WHHeader, dicts, "");

            //验收人
            dicts = userList.Select(p => new DictInfo { ID = p.UserID, Des = p.UserName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.HandlePerson, dicts, "");

            Tools.SetDateTimeControl(this.BillDate);
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            OutStockBLL bll = null;
            OutStockBill info = new OutStockBill();
            try
            {
                bll = BLLFactory.CreateBLL<OutStockBLL>();
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
                    info = new OutStockBill();
                    info.Details = new List<OutStockDetail>();
                    this.BillNO.Text = bll.GetNewBillNO();
                    this.BillDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
                }

                //绑定明细
                DataGridResult<OutStockDetail> matList = new DataGridResult<OutStockDetail>();
                matList.Total = 0;
                matList.Rows = info.Details;

                foreach (OutStockDetail detail in info.Details)
                {
                    detail.UnitName = string.IsNullOrEmpty(detail.UnitName) == false ? detail.UnitName : detail.MainUnitName;
                    detail.OutSpecName = string.IsNullOrEmpty(detail.OutSpecName) == false ? detail.OutSpecName : detail.SpecCode;
                    if (detail.OutAmount == 0)
                    {
                        detail.OutAmount = detail.MainUnitAmount;
                        detail.UnitName = detail.MainUnitName;
                    }
                    
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
            OutStockBill info = new OutStockBill();
            OutStockBLL bll = null;
            string result = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<OutStockBLL>();

                info.Details = JsonConvertHelper.DeserializeObject<List<OutStockDetail>>(this.hiMatList.Value);

                if (this.hiID.Value == "")
                {
                    result=bll.OutStorage(info);                   
                }
                else
                {
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.ID = this.hiID.Value;
                    bll.Update(info);                    
                }
                if(string.IsNullOrEmpty(result)==true)
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.refreshData();parent.closeAppWindow1();", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSI('提示','"+result+"');", true);
                
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
            Response.Redirect("ManageOutStockBill.aspx");
        }

        #endregion
    }
}