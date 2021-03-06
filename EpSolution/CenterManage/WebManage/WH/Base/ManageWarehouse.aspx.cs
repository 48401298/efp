﻿using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.WH;
using Manage.Entity.WH;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;

namespace Manage.Web.WH.Base
{
    /// <summary>
    /// 仓库管理
    /// </summary>
    public partial class ManageWarehouse : ParentPage
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
            WarehouseBLL bll = null;
            DataPage dp = new DataPage();
            Warehouse condition = new Warehouse();

            try
            {
                bll = BLLFactory.CreateBLL<WarehouseBLL>();
                condition.Code = this.Code.Text.Trim();
                condition.Description = this.Description.Text.Trim();

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp=bll.GetList(condition, dp);

                List<Warehouse> list = dp.Result as List<Warehouse>;
                this.GvList.DataSource = list; 
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["ID"].ToString());

                    (this.GvList.Rows[i].Cells[4].Controls[0] as WebControl).Attributes.Add("onclick", click);
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
            WarehouseBLL bll = null;
            List<string> msgList = new List<string>();
            try
            {
                bll = BLLFactory.CreateBLL<WarehouseBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                if (pkArray.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSI('提示','请选择要删除的记录');", true);
                    return;
                }

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    CheckBox cbxSelect = this.GvList.Rows[i].Cells[0].FindControl("cbxSelect") as CheckBox;

                    if (cbxSelect.Checked == false)
                        continue;

                    Warehouse site = new Warehouse();
                    site.ID = this.GvList.DataKeys[i]["ID"].ToString();
                    site.Description = this.GvList.Rows[i].Cells[1].Text;

                    //判断是否已使用
                    bool r = bll.IsUse(site);
                    if (r == true)
                    {
                        msgList.Add(site.Description);
                        continue;
                    }

                    //删除
                    bll.Delete(site);

                }
                string msg = string.Join(",", msgList.ToArray());
                if (msg != "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSI('提示','" + msg + "仓库已使用，无法删除');", true);

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