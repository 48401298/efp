using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;

namespace Manage.Web.Organ
{
    /// <summary>
    /// 编辑监控机构信息
    /// </summary>
    public partial class EditOrgan : ParentPage
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
            string id = Request.QueryString["id"];
            OrgaizationManageBLL bll = null;
            Orgaization info = new Orgaization();
            try
            {
                bll = BLLFactory.CreateBLL<OrgaizationManageBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.OrganID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.OrganID;
                    this.HiCREATEUSER.Value = info.CreateUser;
                    this.HiCREATETIME.Value = info.CreateTime.ToString();
                    this.HiParentID.Value = info.OrganParent;
                }
                else
                {
                    this.HiParentID.Value = Request.QueryString["parentID"];
                    info = new Orgaization();
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
            Orgaization info = new Orgaization();
            OrgaizationManageBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<OrgaizationManageBLL>();
                info.OrganParent = this.HiParentID.Value;
                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.addNode('" + info.OrganID + "','" + info.OrganDESC + "');parent.closeAppWindow1();", true);
                }
                else
                {
                    info.CreateUser = this.HiCREATEUSER.Value;
                    info.CreateTime = DateTime.Parse(this.HiCREATETIME.Value);
                    info.OrganID = this.hiID.Value;
                    info.OrganParent = this.HiParentID.Value;
                    bll.Update(info);
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.editNode('" + info.OrganID + "','" + info.OrganDESC + "');parent.closeAppWindow1();", true);
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