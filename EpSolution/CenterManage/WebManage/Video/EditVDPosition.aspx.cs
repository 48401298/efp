using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Video;
using Manage.Entity.Video;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;

namespace Manage.Web.Video
{
    /// <summary>
    /// 编辑监控位置信息
    /// </summary>
    public partial class EditVDPosition : ParentPage
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
            VDPositionBLL bll = null;
            VDPosition info = new VDPosition();
            try
            {
                bll = BLLFactory.CreateBLL<VDPositionBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.ID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                    this.HiParentID.Value = info.ParentID;
                }
                else
                {
                    this.HiParentID.Value = Request.QueryString["parentID"];
                    info = new VDPosition();
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
            VDPosition info = new VDPosition();
            VDPositionBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<VDPositionBLL>();
                info.ParentID = this.HiParentID.Value;
                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.addNode('" + info.ID + "','" + info.PositionName + "');parent.closeAppWindow1();", true);
                }
                else
                {
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.ID = this.hiID.Value;
                    bll.Update(info);
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.editNode('" + info.ID + "','" + info.PositionName + "');parent.closeAppWindow1();", true);
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