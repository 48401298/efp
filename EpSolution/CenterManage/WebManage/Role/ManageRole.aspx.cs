﻿using System;
using System.Data;
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

namespace Manage.Web.Role
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public partial class ManageRole : ParentPage
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
            RoleManageBLL bll = null;
            DataPage dp = new DataPage();
            Entity.Sys.RoleInfo condition = new Entity.Sys.RoleInfo();

            try
            {
                bll = BLLFactory.CreateBLL<RoleManageBLL>();
                condition.RoleDESC = this.RoleName.Text;

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp=bll.GetList(condition, dp);

                List<Entity.Sys.RoleInfo> list = dp.Result as List<Entity.Sys.RoleInfo>;
                this.GvList.DataSource = list; 
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["RoleID"].ToString());

                    (this.GvList.Rows[i].Cells[3].Controls[0] as WebControl).Attributes.Add("onclick", click);
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
            RoleManageBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<RoleManageBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    bll.Delete(new RoleInfo { RoleID = key.ToString() });
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