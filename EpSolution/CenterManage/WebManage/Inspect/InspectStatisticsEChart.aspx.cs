using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.BLL.Inspect;
using Manage.Entity.Inspect;

namespace Manage.Web.Inspect
{
    /// <summary>
    /// 监测项目
    /// </summary>
    public partial class InspectStatisticsEChart : ParentPage
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
            Tools.SetDateTimeControl(this.StartTimeH);
            Tools.SetDateTimeControlYM(this.StartTimeD);
            Tools.SetDateTimeControlY(this.StartTimeM);

            List<DictInfo> dicts = null;

            //绑定仓库
            List<Orgaization> whList = BLLFactory.CreateBLL<OrgaizationManageBLL>().GetAllList();
            dicts = whList.Select(p => new DictInfo { ID = p.OrganID, Des = p.OrganDESC }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.OrganID, dicts, "");

            CommonDropdown commonDropdown = new CommonDropdown();
            Tools.BindDataToDDL(this.DeviceType, commonDropdown.getInspectDeviceType(), "");

            //绑定监测项目
            List<InspectItemEntity> itemList = BLLFactory.CreateBLL<InspectItemBLL>().GetAllItemInfo(new InspectItemEntity());
            List<DictInfo> dictsItem = itemList.Select(p => new DictInfo { ID = p.ItemCode, Des = p.ItemName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.ItemCode, dictsItem, "");

            //绑定监测设备
            List<InspectDeviceEntity> deviceList = BLLFactory.CreateBLL<InspectDeviceBLL>().GetAllItemInfo(new InspectDeviceEntity());
            List<DictInfo> dictsDevice = deviceList.Select(p => new DictInfo { ID = p.DeviceCode, Des = p.DeviceName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.DeviceCode, dictsDevice, "");

            CommonDropdown resultTypeDropdown = new CommonDropdown();
            Tools.BindDataToDDL(this.ResultType, commonDropdown.getInspectResultType(), "");

        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            
        }

        #endregion

        #region 查询

        protected void btQuery_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        #endregion

        #region 设置样式

        protected void GvList_PreRender(object sender, EventArgs e)
        {
            //GvHelper.DatagridSkin(this.GvList);
        }

        protected void GvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //GvHelper.DatagridSkinUpdate(e);
        }

        #endregion

        #region 分页

        protected void AspNetPager1_PageChanged(object src, Wuqi.Webdiyer.PageChangedEventArgs e)
        {
            //this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            //this.BindData();
        }

        #endregion
    }
}