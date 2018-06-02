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
    /// 监测设备
    /// </summary>
    public partial class InspectDeviceInfo : ParentPage
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
            List<DictInfo> dicts = null;

            //绑定仓库
            List<Orgaization> whList = BLLFactory.CreateBLL<OrgaizationManageBLL>().GetAllList();
            dicts = whList.Select(p => new DictInfo { ID = p.OrganID, Des = p.OrganDESC }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.OrganID, dicts, "");

        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            InspectDeviceBLL bll = null;
            DataPage dp = new DataPage();
            InspectDeviceEntity condition = new InspectDeviceEntity();

            try
            {
                bll = BLLFactory.CreateBLL<InspectDeviceBLL>();
                condition.DeviceCode = this.DeviceCode.Text;
                condition.DeviceName = this.DeviceName.Text;
                condition.OrganID = this.OrganID.Text;

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<InspectDeviceEntity> list = dp.Result as List<InspectDeviceEntity>;

                CommonDropdown commonDropdown = new CommonDropdown();
                List<DictInfo> typeList = commonDropdown.getInspectDeviceType();
                foreach (InspectDeviceEntity ide in list)
                {
                    var temp = typeList.Find(p => p.ID == ide.DeviceType);
                    if (temp != null && temp.Des != null)
                    {
                        ide.DeviceTypeName = temp.Des;
                    }
                }

                this.GvList.DataSource = list;
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["Id"].ToString());

                    (this.GvList.Rows[i].Cells[14].Controls[0] as WebControl).Attributes.Add("onclick", click);
                }
                PagerHelper.SetPageControl(AspNetPager1, dp, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 查询

        protected void btQuery_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        #endregion

        #region 增加

        protected void btAdd_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 删除

        protected void btDelete_Click(object sender, EventArgs e)
        {
            ArrayList pkArray = null;
            InspectDeviceBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<InspectDeviceBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    bll.Delete(new InspectDeviceEntity { Id = key.ToString() });
                }

                this.BindData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 设置样式

        protected void GvList_PreRender(object sender, EventArgs e)
        {
            GvHelper.DatagridSkin(this.GvList);
        }

        protected void GvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GvHelper.DatagridSkinUpdate(e);
        }

        #endregion

        #region 分页

        protected void AspNetPager1_PageChanged(object src, Wuqi.Webdiyer.PageChangedEventArgs e)
        {
            this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }

        #endregion
    }
}