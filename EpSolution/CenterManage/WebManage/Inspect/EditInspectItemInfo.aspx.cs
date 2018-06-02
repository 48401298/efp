using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;
using Manage.BLL.Inspect;
using Manage.Entity.Inspect;

namespace Manage.Web.Inspect
{
    /// <summary>
    /// 编辑用户信息
    /// </summary>
    public partial class EditInspectItemInfo : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindData();
            }
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string Id = Request.QueryString["Id"];
            InspectItemBLL bll = null;
            InspectItemEntity info = new InspectItemEntity();
            try
            {
                bll = BLLFactory.CreateBLL<InspectItemBLL>();
                if (string.IsNullOrEmpty(Id) == false)
                {
                    info.Id = Id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.Id;
                    this.ItemCode.Text = info.ItemCode;
                    this.ItemName.Text = info.ItemName;
                    this.Unit.Text = info.Unit;
                    this.PointCount.Text = info.PointCount + "";
                    this.Remark.Text = info.Remark;
                    this.HiCREATEUSER.Value = info.CreateUser;
                    this.HiCREATETIME.Value = info.CreateTime.ToString();
                }
                else
                {
                    info = new InspectItemEntity();
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
            InspectItemEntity info = new InspectItemEntity();
            InspectItemBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<InspectItemBLL>();

                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                }
                else
                {
                    info.Id = this.hiID.Value;
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