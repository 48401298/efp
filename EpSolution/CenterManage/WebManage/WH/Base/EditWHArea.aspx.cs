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
using Manage.Entity;

namespace Manage.Web.WH.Base
{
    /// <summary>
    /// 编辑存储区域信息
    /// </summary>
    public partial class EditWHArea : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindWHList();
                this.BindData();
            }
        }

        #endregion

        #region 绑定仓库列表

        private void BindWHList()
        {
            WarehouseBLL bll = null;
            List<Warehouse> array = null;

            bll = BLLFactory.CreateBLL<WarehouseBLL>();
            array = bll.GetList();

            this.Warehourse.DataSource = array;
            this.Warehourse.DataBind();
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            WHAreaBLL bll = null;
            WHArea info = new WHArea();
            string whID=Request.QueryString["whID"];
            try
            {
                if (string.IsNullOrEmpty(whID) == false)
                {
                    this.Warehourse.Enabled = false;
                }
                else
                {
                    this.Warehourse.Enabled = true;
                }


                bll = BLLFactory.CreateBLL<WHAreaBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.Warehourse.SelectedValue = info.WHID;
                    this.hiID.Value = info.ID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();

                    this.Warehourse.Enabled = false;
                }
                else
                {
                    info = new WHArea();
                    this.Warehourse.SelectedValue = whID;
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
            WHArea info = new WHArea();
            WHAreaBLL bll = null;
            DataResult<int> result = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<WHAreaBLL>();
                info.WHID = this.Warehourse.SelectedValue;
                if (this.hiID.Value == "")
                {
                    result=bll.Insert(info);
                }
                else
                {
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.ID = this.hiID.Value;
                    result=bll.Update(info);

                }

                if (string.IsNullOrEmpty(result.Msg) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSI('提示','" + result.Msg + "')", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.refreshData();parent.closeAppWindow1();", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}