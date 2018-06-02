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
    /// 编辑库存盘点单
    /// </summary>
    public partial class EditCheckStockBill : ParentPage
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

            //仓库负责人
            List<Manage.Entity.Sys.User> userList = BLLFactory.CreateBLL<UserManageBLL>().GetAllUsers(new Manage.Entity.Sys.User());
            dicts = userList.Select(p => new DictInfo { ID = p.UserID, Des = p.UserName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.CheckHeader, dicts, "");

            //存储区域
            List<WHArea> areaList = BLLFactory.CreateBLL<WHAreaBLL>().GetList("");
            dicts = whList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.AreaID, dicts, "");

            Tools.SetDateTimeControl(this.BillDate);
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

                    this.btConfirm.Visible = true;
                    this.btBuild.Visible = false;
                }
                else
                {
                    info = new CheckStockBill();
                    info.Details = new List<CheckStockDetail>();
                    this.BillNO.Text = bll.GetNewBillNO();
                    this.BillDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    LoginInfo user= this.GetLoginInfo();
                    this.CheckHeader.SelectedValue = user.UserID;
                }

                this.IsConfirmName.Text = info.IsConfirm == 0 ? "未确认" : "已确认";

                //绑定明细
                DataGridResult<CheckStockDetail> matList = new DataGridResult<CheckStockDetail>();
                matList.Total = 0;
                matList.Rows = info.Details.OrderBy(p=>p.Seq).ToList();

                this.hiCheckList.Value = matList.GetJsonSource();
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
            CheckStockBill info = new CheckStockBill();
            CheckStockBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<CheckStockBLL>();

                info.Details = JsonConvertHelper.DeserializeObject<List<CheckStockDetail>>(this.hiCheckList.Value);

                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                    this.btConfirm.Visible = true;
                    this.btBuild.Visible = false;
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
        
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btBuild_Click(object sender, EventArgs e)
        {
            CheckStockBill info = new CheckStockBill();
            CheckStockBLL bll = null;

            bll = BLLFactory.CreateBLL<CheckStockBLL>();

            info = bll.BuildBill(new CheckStockBill {Warehouse=this.Warehouse.SelectedValue,AreaID=this.AreaID.SelectedValue });

            this.BillNO.Text = info.BillNO;
            //绑定明细
            DataGridResult<CheckStockDetail> matList = new DataGridResult<CheckStockDetail>();
            matList.Total = 0;
            matList.Rows = info.Details;

            this.hiCheckList.Value = matList.GetJsonSource();
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btConfirm_Click(object sender, EventArgs e)
        {
            CheckStockBill info = new CheckStockBill();
            CheckStockBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<CheckStockBLL>();

                info.Details = JsonConvertHelper.DeserializeObject<List<CheckStockDetail>>(this.hiCheckList.Value);

                info.CREATEUSER = this.HiCREATEUSER.Value;
                info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                info.ID = this.hiID.Value;
                bll.ConfirmCheck(info);

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.refreshData();parent.closeAppWindow1();", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}