﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI.Util;
using Manage.Entity.Inspect;
using LAF.WebUI;
using Manage.BLL.Inspect;


namespace Manage.Web.MobileApp
{
    /// <summary>
    /// 环境监测查询页面
    /// </summary>
    public partial class InspectDataQuery : System.Web.UI.Page
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("AppLogin.aspx");
                return;
            }

            if (this.IsPostBack == false)
            {
                this.InitForm();
            }
        }

        #endregion

        #region 初始化页面

        private void InitForm()
        {
            //绑定仓设备类型
            CommonDropdown commonDropdown = new CommonDropdown();
            Tools.BindDataToDDL(this.DeviceType, commonDropdown.getInspectDeviceType(), "");

            //绑定监测项目
            List<InspectItemEntity> itemList = new InspectItemBLL().GetAllItemInfo(new InspectItemEntity());
            List<DictInfo> dictsItem = itemList.Select(p => new DictInfo { ID = p.ItemCode, Des = p.ItemName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.ItemCode, dictsItem, "");

            this.StartDate.Value = DateTime.Now.ToString("yyyy-MM-01");
            this.EndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            this.StartDateShow.Value = DateTime.Now.ToString("yyyy-MM-01");
            this.EndDateShow.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        #endregion

        #region 查询

        protected void btQuery_Click(object sender, EventArgs e)
        {
            InspectDataEntity condition = new InspectDataEntity();

            condition.ItemCode = this.ItemCode.Text;
            condition.StartTime = this.StartDate.Value;
            condition.EndTime = this.EndDate.Value;
            condition.DeviceType = this.DeviceType.Text;

            Session["InspectDataCondition"] = condition;

            Response.Redirect("InspectDataResult.aspx");
        }

        #endregion

    }
}