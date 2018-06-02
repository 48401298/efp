using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.Entity.MES;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.BLL.MES;

namespace Manage.Web.MES.Base
{
    public partial class EditProcessFlow : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindFactoryList();
                this.BindProductLineList();
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

        private void BindData()
        {
            string id = Request.QueryString["id"];
            ProcessFlowBLL bll = null;
            ProcessFlow info = new ProcessFlow();
            try
            {
                bll = BLLFactory.CreateBLL<ProcessFlowBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.PID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.PID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                }
                else
                {
                    info = new ProcessFlow();
                }
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
            ProcessFlow info = new ProcessFlow();
            ProcessFlowBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<ProcessFlowBLL>();

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
