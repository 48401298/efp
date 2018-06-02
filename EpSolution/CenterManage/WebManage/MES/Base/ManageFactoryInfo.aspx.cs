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
    public partial class ManageFactoryInfo : ParentPage
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
            FactoryInfoBLL bll = null;
            DataPage dp = new DataPage();
            FactoryInfo condition = new FactoryInfo();

            try
            {
                bll = BLLFactory.CreateBLL<FactoryInfoBLL>();
                condition.PCODE = this.PCODE.Text;
                condition.PNAME = this.PNAME.Text;

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<FactoryInfo> list = dp.Result as List<FactoryInfo>;
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
            FactoryInfoBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<FactoryInfoBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    if (!bll.HasProductLine(key.ToString()))
                    {
                        bll.DeleteFactory(new FactoryInfo { PID = key.ToString() });
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('选中的工厂有生产线，不能被删除');", true);
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