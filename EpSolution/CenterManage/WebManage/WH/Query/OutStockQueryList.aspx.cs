using System;
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

namespace Manage.Web.WH.Query
{
    /// <summary>
    /// 出库查询
    /// </summary>
    public partial class OutStockQueryList : ParentPage
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

            //出库方式
            List<WHOutMode> imList = BLLFactory.CreateBLL<WHOutModeBLL>().GetList();
            dicts = imList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.OutStockMode, dicts, "");

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
            OutStockQueryBLL bll = null;
            DataPage dp = new DataPage();
            OutStockBill condition = new OutStockBill();

            try
            {
                bll = BLLFactory.CreateBLL<OutStockQueryBLL>();
                condition.StartDate = this.StartDate.Text;
                condition.EndDate = this.EndDate.Text;
                condition.OutStockMode = this.OutStockMode.SelectedValue;
                condition.Warehouse = this.Warehouse.SelectedValue;
                condition.ProductType = this.ProductType.SelectedValue;
                condition.MatID = this.MatID.SelectedValue;
                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp=bll.GetList(condition, dp);

                List<InStockQueryResult> list = dp.Result as List<InStockQueryResult>;
                this.GvList.DataSource = list; 
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    //绑定查看功能
                    LinkButton lbtView = this.GvList.Rows[i].Cells[7].FindControl("lbtView") as LinkButton;
                    lbtView.OnClientClick = string.Format("view('{0}','{1}','{2}','{3}');return false;"
                        , this.GvList.DataKeys[i]["WarehouseID"].ToString(), this.GvList.DataKeys[i]["MatID"].ToString()
                        , this.StartDate.Text, this.EndDate.Text);                        
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

        
    }
}