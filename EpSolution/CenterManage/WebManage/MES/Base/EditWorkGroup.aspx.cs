using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.MES;
using Manage.Entity.MES;
using LAF.WebUI.Util;
using LAF.WebUI;
using LAF.WebUI.DataSource;

namespace Manage.Web.MES.Base
{
    public partial class EditWorkGroup : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                BindFactoryList();
                
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

            this.FAID.DataSource = array;
            this.FAID.DataBind();
        }

        private void BindData()
        {
            string id = Request.QueryString["id"];
            WorkGroupBLL bll = null;
            WorkGroup info = new WorkGroup();
            DataGridResult<WorkGroupRef> pList = new DataGridResult<WorkGroupRef>(); 
            
            try
            {
                bll = BLLFactory.CreateBLL<WorkGroupBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.PID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.PID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                    List<WorkGroupRef> list = bll.GetPList(id);
                    pList.Total = list.Count;
                    pList.Rows = list;
                }
                else
                {
                    info = new WorkGroup();
                    pList.Total = 0;
                    pList.Rows = new List<WorkGroupRef>();
                }
                
                this.hiPList.Value = pList.GetJsonSource();
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
            WorkGroup info = new WorkGroup();
            WorkGroupBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                info.Details = LAF.Common.Serialization.JsonConvertHelper.DeserializeObject<List<WorkGroupRef>>(this.hiPList.Value);
                bll = BLLFactory.CreateBLL<WorkGroupBLL>();

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
