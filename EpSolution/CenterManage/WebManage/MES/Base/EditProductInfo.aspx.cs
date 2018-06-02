using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.MES;
using LAF.WebUI;
using Manage.Entity.MES;
using LAF.WebUI.Util;

namespace Manage.Web.MES.Base
{
    public partial class EditProductInfo : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                BindProcessFlowList();
                this.BindData();
            }
        }

        #endregion

        #region 绑定数据

        private void BindProcessFlowList()
        {
            ProcessFlowBLL bll = null;
            List<ProcessFlow> array = null;

            bll = BLLFactory.CreateBLL<ProcessFlowBLL>();
            array = bll.GetList();

            this.PRID.DataSource = array;
            this.PRID.DataBind();
        }

        private void BindData()
        {
            string id = Request.QueryString["id"];
            ProductInfoBLL bll = null;
            ProductInfo info = new ProductInfo();
            try
            {
                bll = BLLFactory.CreateBLL<ProductInfoBLL>();
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
                    info = new ProductInfo();
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
            ProductInfo info = new ProductInfo();
            ProductInfoBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<ProductInfoBLL>();

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
