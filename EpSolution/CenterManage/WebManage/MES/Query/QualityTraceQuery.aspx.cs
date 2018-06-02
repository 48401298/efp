using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Query;
using Manage.BLL.MES;
using Manage.Entity.MES;
using Manage.Entity.Query;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;

namespace Manage.Web.MES.Query
{
    /// <summary>
    /// 质量追溯查询
    /// </summary>
    public partial class QualityTraceQuery : ParentPage
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

            //工厂
            List<FactoryInfo> fList = BLLFactory.CreateBLL<FactoryInfoBLL>().GetList();
            dicts = fList.Select(p => new DictInfo { ID = p.PID, Des = p.PNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.Factory, dicts, Tools.QueryDDLFirstItem);

            //生产线
            List<ProductLine> plList = BLLFactory.CreateBLL<ProductLineBLL>().GetList();
            dicts = plList.Select(p => new DictInfo { ID = p.PID, Des = p.PLNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.ProductLine, dicts, Tools.QueryDDLFirstItem);

            //产品
            List<ProductInfo> piList = BLLFactory.CreateBLL<ProductInfoBLL>().GetList();
            dicts = piList.Select(p => new DictInfo { ID = p.PID, Des = p.PNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.Product, dicts, Tools.QueryDDLFirstItem);

        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            QualityTraceQueryBLL bll = null;
            DataPage dp = new DataPage();
            QualityTraceCondition condition = new QualityTraceCondition();

            try
            {
                bll = BLLFactory.CreateBLL<QualityTraceQueryBLL>();
                condition.StartDate = this.StartDate.Text;
                condition.EndDate = this.EndDate.Text;
                condition.Factory = this.Factory.SelectedValue;
                condition.ProductLine = this.ProductLine.SelectedValue;
                condition.Product = this.Product.SelectedValue;
                condition.ProductBatchNumber = this.ProductBatchNumber.Text;
                condition.MatIDCode = this.MatIDCode.Text;
                condition.ProductIDCode = this.ProductIDCode.Text;                    
                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<QualityTraceInfo> list = dp.Result as List<QualityTraceInfo>;
                this.GvList.DataSource = list;
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    LinkButton lbtQualityTrace = this.GvList.Rows[i].Cells[7].FindControl("lbtQualityTrace") as LinkButton;

                    lbtQualityTrace.OnClientClick = "viewTraceDetail('"+this.GvList.DataKeys[i]["PID"].ToString()+"');return false;";
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