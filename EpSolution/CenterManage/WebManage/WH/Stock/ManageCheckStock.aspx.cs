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

namespace Manage.Web.WH.Stock
{
    /// <summary>
    /// 盘点管理
    /// </summary>
    public partial class ManageCheckStock : ParentPage
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

            //存储区域
            List<WHArea> areaList = BLLFactory.CreateBLL<WHAreaBLL>().GetList("");
            dicts = whList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.AreaID, dicts, "");
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            CheckStockBLL bll = null;
            DataPage dp = new DataPage();
            CheckStockBill condition = new CheckStockBill();
            try
            {
                bll = BLLFactory.CreateBLL<CheckStockBLL>();
                condition.StartDate = this.StartDate.Text;
                condition.EndDate = this.EndDate.Text;
                condition.Warehouse = this.Warehouse.SelectedValue;
                condition.AreaID = this.AreaID.SelectedValue;
                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp=bll.GetList(condition, dp);

                List<CheckStockBill> list = dp.Result as List<CheckStockBill>;
                this.GvList.DataSource = list; 
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    this.GvList.Rows[i].Cells[6].Text = this.GvList.Rows[i].Cells[6].Text == "0" ? "未确认" : "已确认";

                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["ID"].ToString());

                    LinkButton btEdit = (this.GvList.Rows[i].Cells[9].FindControl("lbtEdit") as LinkButton);
                    if (this.GvList.Rows[i].Cells[6].Text == "已确认")
                    {
                        btEdit.Visible = false;
                    }
                    else
                    {
                        //绑定编辑功能
                        btEdit.Attributes.Add("onclick", click);
                    }

                    //绑定查看功能
                    (this.GvList.Rows[i].Cells[8].FindControl("lbtView") as LinkButton)
                        .Attributes.Add("onclick", "return view('" + this.GvList.DataKeys[i]["ID"].ToString() + "');");
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

        #region 删除

        protected void btDelete_Click(object sender, EventArgs e)
        {
            ArrayList pkArray = null;
            CheckStockBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<CheckStockBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    bll.Delete(new CheckStockBill { ID = key.ToString() });
                }

                this.BindData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}