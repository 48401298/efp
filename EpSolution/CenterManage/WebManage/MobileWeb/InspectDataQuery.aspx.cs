using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI.Util;
using Manage.Entity.Inspect;
using LAF.WebUI;
using Manage.BLL.Inspect;

namespace Manage.Web.MobileWeb
{
    public partial class InspectDataQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.InitForm();
            }
        }

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
            this.StartDateShow.Value = DateTime.Now.ToString("yyyy年MM月01日");
            this.EndDateShow.Value = DateTime.Now.ToString("yyyy年MM月dd日");
        }

        #endregion
    }
}