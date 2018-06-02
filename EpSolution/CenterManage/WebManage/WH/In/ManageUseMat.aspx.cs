using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.WH;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.Entity.WH;
using System.Collections;

namespace Manage.Web.WH.In
{
    /// <summary>
    /// 领料管理
    /// </summary>
    public partial class ManageUseMat : ParentPage
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
            UseMatBLL bll = null;
            DataPage dp = new DataPage();
            UseMatBill condition = new UseMatBill();
            try
            {
                bll = BLLFactory.CreateBLL<UseMatBLL>();
                condition.BatchNumber = this.BatchNumber.Text;


                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<UseMatBill> list = dp.Result as List<UseMatBill>;
                this.GvList.DataSource = list;
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string pid = null;

                    if (this.GvList.DataKeys[i]["PID"] != null)
                    {
                        pid = this.GvList.DataKeys[i]["PID"].ToString();
                        string viewClick = string.Format("return view('{0}');", pid);

                        (this.GvList.Rows[i].Cells[6].FindControl("LinkView") as LinkButton).Attributes.Add("onclick", viewClick);
                        (this.GvList.Rows[i].Cells[6].FindControl("LinkView") as LinkButton).Text = "查看";
                        (this.GvList.Rows[i].Cells[7].FindControl("LinkBuild") as LinkButton).Visible = false;
                    }
                    else
                    {
                        (this.GvList.Rows[i].Cells[6].FindControl("LinkView") as LinkButton).Enabled = false;
                    }
                    
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
            UseMatBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<UseMatBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    bll.Delete(new UseMatBill { PID = key.ToString() });
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

        #region 表格行为

        protected void GvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "buildBill")
            {
                string supplyID = e.CommandArgument.ToString();

                UseMatBLL bll = BLLFactory.CreateBLL<UseMatBLL>();

                bll.Build(supplyID);

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MS('提示', '生成完成')", true);
            }
            this.BindData();
        }

        #endregion
    }
}
