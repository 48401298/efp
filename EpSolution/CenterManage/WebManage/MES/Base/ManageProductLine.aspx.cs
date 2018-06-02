using System;
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
    public partial class ManageProductLine : ParentPage
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
            ProductLineBLL bll = null;
            DataPage dp = new DataPage();
            ProductLine condition = new ProductLine();

            try
            {
                bll = BLLFactory.CreateBLL<ProductLineBLL>();
                condition.PLCODE = this.PLCODE.Text;
                condition.PLNAME = this.PLNAME.Text;

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<ProductLine> list = dp.Result as List<ProductLine>;
                this.GvList.DataSource = list;
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["PID"].ToString());

                    (this.GvList.Rows[i].Cells[6].Controls[0] as WebControl).Attributes.Add("onclick", click);
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
            ProductLineBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<ProductLineBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    if (!bll.HasWorkStation(key.ToString()))
                    {
                        bll.DeleteProductLine(new ProductLine { PID = key.ToString() });
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('选中的生产线有工位，不能被删除');", true);
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