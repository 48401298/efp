﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.MES;
using LAF.Data;
using Manage.Entity.MES;
using LAF.WebUI;
using LAF.WebUI.Util;
using System.Collections;

namespace Manage.Web.MES.Base
{
    public partial class ManageWorkGroup : ParentPage
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
            WorkGroupBLL bll = null;
            DataPage dp = new DataPage();
            WorkGroup condition = new WorkGroup();

            try
            {
                bll = BLLFactory.CreateBLL<WorkGroupBLL>();
                condition.PNAME = this.PNAME.Text;

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<WorkGroup> list = dp.Result as List<WorkGroup>;
                this.GvList.DataSource = list;
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["PID"].ToString());

                    (this.GvList.Rows[i].Cells[5].Controls[0] as WebControl).Attributes.Add("onclick", click);
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
            WorkGroupBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<WorkGroupBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    if (!bll.HasProducePlan(key.ToString()))
                    {
                        bll.DeleteWorkGroup(new WorkGroup { PID = key.ToString() });
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('选中的班组在排班中使用过，不能被删除');", true);
                    }
                    
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