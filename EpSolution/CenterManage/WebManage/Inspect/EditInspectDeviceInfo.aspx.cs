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
using Manage.BLL.Inspect;
using Manage.Entity.Inspect;

namespace Manage.Web.Inspect
{
    /// <summary>
    /// 编辑用户信息
    /// </summary>
    public partial class EditInspectDeviceInfo : ParentPage
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
            Tools.SetDateTimeControlFull(this.LastLoginTime);
            Tools.SetDateTimeControlFull(this.LastRegisterTime);

            List<DictInfo> dicts = null;

            //绑定仓库
            List<Orgaization> whList = BLLFactory.CreateBLL<OrgaizationManageBLL>().GetAllList();
            dicts = whList.Select(p => new DictInfo { ID = p.OrganID, Des = p.OrganDESC }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.OrganID, dicts, "");


            CommonDropdown commonDropdown = new CommonDropdown();
            Tools.BindDataToDDL(this.DeviceType, commonDropdown.getInspectDeviceType(), "");
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string Id = Request.QueryString["Id"];
            InspectDeviceBLL bll = null;
            InspectDeviceEntity info = new InspectDeviceEntity();
            try
            {
                bll = BLLFactory.CreateBLL<InspectDeviceBLL>();
                if (string.IsNullOrEmpty(Id) == false)
                {
                    info.Id = Id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.Id;
                    this.DeviceCode.Text = info.DeviceCode;
                    this.DeviceName.Text = info.DeviceName;
                    this.DeviceIP.Text = info.DeviceIP;
                    this.DevicePort.Text = info.DevicePort + "";
                    this.LanIP.Text = info.LanIP;
                    this.LanPort.Text = info.LanPort + "";
                    if (!"0001/1/1 0:00:00".Equals(info.LastLoginTime + ""))
                    {
                        this.LastLoginTime.Text = info.LastLoginTime + "";
                    }
                    else
                    {
                        this.LastLoginTime.Text = "";
                    }
                    if (!"0001/1/1 0:00:00".Equals(info.LastRegisterTime + ""))
                    {
                        this.LastRegisterTime.Text = info.LastRegisterTime + "";
                    }
                    else
                    {
                        this.LastRegisterTime.Text = "";
                    }
                    this.Lon.Text = info.Lon + "";
                    this.Lat.Text = info.Lat + "";
                    this.OrganID.Text = info.OrganID + "";
                    this.DeviceType.Text = info.DeviceType + "";
                    this.Remark.Text = info.Remark;
                    this.HiCREATEUSER.Value = info.CreateUser;
                    this.HiCREATETIME.Value = info.CreateTime.ToString();
                }
                else
                {
                    info = new InspectDeviceEntity();
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
            InspectDeviceEntity info = new InspectDeviceEntity();
            InspectDeviceBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<InspectDeviceBLL>();

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