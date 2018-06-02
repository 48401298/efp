using System;
using Manage.BLL.MES;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.Entity.MES;
using System.Collections.Generic;
using LAF.WebUI.DataSource;

namespace Manage.Web.MES.Base
{
    public partial class EditProcessInfo : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindFactoryList();
                this.BindProductLineList();
                BindWorkStation();
                BindEquipmentList();
                this.BindData();
            }
        }

        #endregion

        #region 绑定数据

        private void BindFactoryList()
        {
            FactoryInfoBLL bll = null;
            List<FactoryInfo> array = null;

            bll = BLLFactory.CreateBLL<FactoryInfoBLL>();
            array = bll.GetList();

            this.FACTORYPID.DataSource = array;
            this.FACTORYPID.DataBind();
        }

        private void BindProductLineList()
        {
            ProductLineBLL bll = null;
            List<ProductLine> array = null;

            bll = BLLFactory.CreateBLL<ProductLineBLL>();
            array = bll.GetList();

            this.PRODUCTLINEPID.DataSource = array;
            this.PRODUCTLINEPID.DataBind();
        }

        private void BindWorkStation()
        {
            WorkStationBLL bll = null;
            List<WorkStation> array = null;

            bll = BLLFactory.CreateBLL<WorkStationBLL>();
            array = bll.GetList();

            this.STID.DataSource = array;
            this.STID.DataBind();
        }

        private void BindEquipmentList()
        {
            EquipmentBLL bll = null;
            List<EquipmentInfo> array = null;

            bll = BLLFactory.CreateBLL<EquipmentBLL>();
            array = bll.GetList();

            this.EQID.DataSource = array;
            this.EQID.DataBind();
        }

        private void BindData()
        {
            string id = Request.QueryString["id"];
            ProcessInfoBLL bll = null;
            Manage.Entity.MES.ProcessInfo info = new Manage.Entity.MES.ProcessInfo();
            DataGridResult<EquipmentRef> eqList = new DataGridResult<EquipmentRef>();
            DataGridResult<StationRef> wsList = new DataGridResult<StationRef>(); 
            try
            {
                bll = BLLFactory.CreateBLL<ProcessInfoBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.PID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.PID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                    List<EquipmentRef> eqlist = bll.GetEList(id);
                    foreach (EquipmentRef item in eqlist)
                    {
                        item.DeleteAction = "deleteEQ('" + item.EQID + "')";
                    }

                    List<StationRef> wslist = bll.GetSList(id);
                    foreach (StationRef item in wslist)
                    {
                        item.DeleteAction = "deleteWS('" + item.STID + "')";
                    }
                    wsList.Total = wslist.Count;
                    wsList.Rows = wslist;
                    
                    eqList.Total = eqlist.Count;
                    eqList.Rows = eqlist;
                    this.HiFLOWID.Value = info.FLOWID;
                }
                else
                {
                    string flowID = Request.QueryString["flowID"];
                    info = new ProcessInfo();
                    info.FLOWID = flowID;
                    wsList.Total = 0;
                    wsList.Rows = new List<StationRef>();
                    eqList.Total = 0;
                    eqList.Rows = new List<EquipmentRef>();
                }
                this.HiFLOWID.Value = info.FLOWID;
                this.hiWSList.Value = wsList.GetJsonSource();
                this.hiEQList.Value = eqList.GetJsonSource();
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
            ProcessInfo info = new ProcessInfo();
            ProcessInfoBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                info.Details = LAF.Common.Serialization.JsonConvertHelper.DeserializeObject<List<EquipmentRef>>(this.hiEQList.Value);
                info.Details2 = LAF.Common.Serialization.JsonConvertHelper.DeserializeObject<List<StationRef>>(this.hiWSList.Value);
                bll = BLLFactory.CreateBLL<ProcessInfoBLL>();
                info.FLOWID = this.HiFLOWID.Value;
                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                }
                else
                {
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.PID = this.hiID.Value;
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
    }
}