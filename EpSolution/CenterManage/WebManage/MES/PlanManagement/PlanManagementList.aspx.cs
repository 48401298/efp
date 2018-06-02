using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Manage.BLL.MES;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.Entity.MES;

namespace Manage.Web.MES.PlanManagement
{
    public partial class PlanManagementList : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindFactoryList();
                this.BindProductLineList();
                this.BindProductInfoList();
                this.BindData();
            }
        }

        #endregion

        #region 绑定数据

        private void BindFactoryList()
        {
            List<DictInfo> dicts = null;
            FactoryInfoBLL bll = null;
            List<FactoryInfo> array = null;

            bll = BLLFactory.CreateBLL<FactoryInfoBLL>();
            array = bll.GetList();

            dicts = array.Select(p => new DictInfo { ID = p.PID, Des = p.PNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.FACTORYPID, dicts, Tools.QueryDDLFirstItem);
        }

        private void BindProductLineList()
        {
            List<DictInfo> dicts = null;
            ProductLineBLL bll = null;
            List<ProductLine> array = null;

            bll = BLLFactory.CreateBLL<ProductLineBLL>();
            array = bll.GetList();

            dicts = array.Select(p => new DictInfo { ID = p.PID, Des = p.PLNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.PRID, dicts, Tools.QueryDDLFirstItem);
        }

        private void BindProductInfoList()
        {
            List<DictInfo> dicts = null;
            ProductInfoBLL bll = null;
            List<ProductInfo> array = null;

            bll = BLLFactory.CreateBLL<ProductInfoBLL>();
            array = bll.GetList();

            dicts = array.Select(p => new DictInfo { ID = p.PID, Des = p.PNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.PRODUCTIONID, dicts, Tools.QueryDDLFirstItem);

        }

        private void BindData()
        {
            DataPage dp = new DataPage();
            ProducePlan condition = new ProducePlan();
            ProducePlanBLL bll = null;
            try
            {

                bll = BLLFactory.CreateBLL<ProducePlanBLL>();
                condition.FACTORYPID = this.FACTORYPID.SelectedValue;
                condition.PRID = this.PRID.SelectedValue;
                condition.PRODUCTIONID = this.PRODUCTIONID.SelectedValue;
                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);
                List<ProducePlan> list = dp.Result as List<ProducePlan>;
                this.GvList.DataSource = list;
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    if (this.GvList.Rows[i].Cells[8].Text == "0")
                    {
                        this.GvList.Rows[i].Cells[8].Text = "未完成";
                    }
                    else
                    {
                        this.GvList.Rows[i].Cells[8].Text = "已完成";
                    }

                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["PID"].ToString());

                    (this.GvList.Rows[i].Cells[9].Controls[0] as WebControl).Attributes.Add("onclick", click);
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
            ProducePlanBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<ProducePlanBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    bll.DeleteProducePlan(new ProducePlan { PID = key.ToString() });
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