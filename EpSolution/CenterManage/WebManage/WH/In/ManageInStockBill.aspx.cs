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

namespace Manage.Web.WH.In
{
    /// <summary>
    /// 入库管理
    /// </summary>
    public partial class ManageInStockBill : ParentPage
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
            Tools.SetDateTimeControl(this.StartDate);
            Tools.SetDateTimeControl(this.EndDate);

            List<DictInfo> dicts = null;

            //绑定仓库
            List<Warehouse> whList = BLLFactory.CreateBLL<WarehouseBLL>().GetListByUserID(this.GetLoginInfo());
            dicts = whList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.Warehouse, dicts, "");

            //入库方式
            List<WHInMode> imList = BLLFactory.CreateBLL<WHInModeBLL>().GetList();
            dicts = imList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.InStockMode, dicts, "");

            //货品类别
            List<MatType> matTypeList = BLLFactory.CreateBLL<MatTypeBLL>().GetList();
            dicts = matTypeList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.ProductType, dicts, "");

            //货品
            List<WHMat> matList = BLLFactory.CreateBLL<WHMatBLL>().GetList();
            dicts = matList.Select(p => new DictInfo { ID = p.ID, Des = p.MatName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.MatID, dicts, "");
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            InStockBLL bll = null;
            DataPage dp = new DataPage();
            InStockBill condition = new InStockBill();

            try
            {
                bll = BLLFactory.CreateBLL<InStockBLL>();
                condition.StartDate = this.StartDate.Text;
                condition.EndDate = this.EndDate.Text;
                condition.InStockMode = this.InStockMode.SelectedValue;
                condition.Warehouse = this.Warehouse.SelectedValue;
                condition.ProductType = this.ProductType.SelectedValue;
                condition.MatID = this.MatID.SelectedValue;
                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp=bll.GetList(condition, dp);

                List<InStockBill> list = dp.Result as List<InStockBill>;
                this.GvList.DataSource = list; 
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["ID"].ToString());

                    //绑定编辑功能
                    (this.GvList.Rows[i].Cells[10].Controls[0] as WebControl).Attributes.Add("onclick", click);

                    //绑定查看功能
                    (this.GvList.Rows[i].Cells[11].FindControl("lbtView") as LinkButton).PostBackUrl = "ViewInStockBill.aspx?id=" + this.GvList.DataKeys[i]["ID"].ToString();
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
            Response.Redirect("EditMlInBill.aspx");
        }

        protected void btMlAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("MlInStorageByMobile.aspx");

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

        protected void btFGAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("FGInStorageByMobile.aspx");
        }
        
    }
}