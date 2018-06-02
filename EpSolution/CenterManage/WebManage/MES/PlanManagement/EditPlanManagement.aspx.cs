using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.MES;
using Manage.Entity.MES;
using LAF.WebUI;
using LAF.WebUI.Util;

namespace Manage.Web.MES.PlanManagement
{
    public partial class EditPlanManagement : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindFactoryList();
                this.BindProductLineList();
                this.BindProductInfoList();
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

            this.PRID.DataSource = array;
            this.PRID.DataBind();
        }

        private void BindProductInfoList()
        {
            ProductInfoBLL bll = null;
            List<ProductInfo> array = null;

            bll = BLLFactory.CreateBLL<ProductInfoBLL>();
            array = bll.GetList();

            this.PRODUCTIONID.DataSource = array;
            this.PRODUCTIONID.DataBind();
        }

        private void BindData()
        {
            string id = Request.QueryString["id"];
            ProducePlanBLL bll = null;
            ProducePlan info = new ProducePlan();
            try
            {
                bll = BLLFactory.CreateBLL<ProducePlanBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.PID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.PLANDATE.Text = info.PLANDATE.ToString("yyyy-MM-dd");
                    this.hiID.Value = info.PID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                }
                else
                {
                    info = new ProducePlan();
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
            ProducePlan info = new ProducePlan();
            ProducePlanBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<ProducePlanBLL>();

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