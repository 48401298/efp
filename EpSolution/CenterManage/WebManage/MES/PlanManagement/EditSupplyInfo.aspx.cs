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

namespace Manage.Web.MES.PlanManagement
{
    public partial class EditSupplyInfo : ParentPage
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
            //绑定生产计划
            dicts = BLLFactory.CreateBLL<ProducePlanBLL>().GetDDList().Select(p => new DictInfo { ID = p.PID, Des = p.BATCHNUMBER }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.PLANID, dicts, "");

            List<Warehouse> whList = BLLFactory.CreateBLL<WarehouseBLL>().GetList();
            dicts = whList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.Warehouse, dicts, "");

        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            SupplyInfoBLL bll = null;
            SupplyInfo info = new SupplyInfo();
            try
            {
                bll = BLLFactory.CreateBLL<SupplyInfoBLL>();
                if (!string.IsNullOrEmpty(id))
                {
                    info.PID = id;
                    info = bll.Get(info);
                    info.Details = bll.GetList(info.PID);
                    UIBindHelper.BindForm(this.Page, info);
                    this.FNAME.Text = info.FactoryName;
                    this.PDNAME.Text = info.ProduceName;
                    this.PLNAME.Text = info.PLName;
                    this.DELIVERYDATE.Text = info.DELIVERYDATE.ToString("yyyy-MM-dd");
                    this.REMARK.Text = info.REMARK;
                    this.hiID.Value = info.PID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                    this.hiPlanID.Value = info.PLANID;
                    this.PLNAME.Text = info.PLName;
                    this.BatchNumber.Visible = true;
                    this.PLANID.Visible = false;
                }
                else
                {
                    info = new SupplyInfo();
                    info.Details = new List<SupplyMaterialInfo>();
                    this.BatchNumber.Visible = false;
                    this.PLANID.Visible = true;
                }

                //绑定明细
                DataGridResult<SupplyMaterialInfo> supplyMaterialInfo = new DataGridResult<SupplyMaterialInfo>();
                supplyMaterialInfo.Total = info.Details.Count;
                supplyMaterialInfo.Rows = info.Details;

                foreach (SupplyMaterialInfo detail in info.Details)
                {
                    detail.DeleteAction = "deleteItem(\'" + detail.MATRIALID + "\')";
                }

                this.hiMaterialList.Value = supplyMaterialInfo.GetJsonSource();
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
            SupplyInfo info = new SupplyInfo();
            SupplyInfoBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<SupplyInfoBLL>();

                info.Details = JsonConvertHelper.DeserializeObject<List<SupplyMaterialInfo>>(this.hiMaterialList.Value);

                info.BATCHNUMBER = this.PLANID.SelectedItem.Text;
                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                }
                else
                {
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.PID = this.hiID.Value;
                    info.PLANID = this.hiPlanID.Value;
                    info.PLName = this.PLNAME.Text;
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
            Response.Redirect("ManageSupplyInfo.aspx");
        }

        #endregion
    }
}