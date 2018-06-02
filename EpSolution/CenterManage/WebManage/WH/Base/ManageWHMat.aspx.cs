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

namespace Manage.Web.WH.Base
{
    /// <summary>
    /// 货品信息管理
    /// </summary>
    public partial class ManageWHMat : ParentPage
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
            WHMatBLL bll = null;
            DataPage dp = new DataPage();
            WHMat condition = new WHMat();

            try
            {
                bll = BLLFactory.CreateBLL<WHMatBLL>();
                condition.MatName = this.Description.Text;

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<WHMat> list = dp.Result as List<WHMat>;
                this.GvList.DataSource = list; 
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["ID"].ToString());

                    (this.GvList.Rows[i].Cells[10].Controls[0] as WebControl).Attributes.Add("onclick", click);

                    LinkButton lbtBuildCode = this.GvList.Rows[i].Cells[11].FindControl("lbtBuildCode") as LinkButton;

                    lbtBuildCode.PostBackUrl = "BuildMatIDCode.aspx?matID=" + this.GvList.DataKeys[i]["ID"].ToString();

                    LinkButton lbtSetSpec = this.GvList.Rows[i].Cells[12].FindControl("lbtSetSpec") as LinkButton;

                    lbtSetSpec.PostBackUrl = "ManageMatSpec.aspx?matID=" + this.GvList.DataKeys[i]["ID"].ToString();
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
            WHMatBLL bll = null;
            List<string> msgList = new List<string>();
            try
            {
                bll = BLLFactory.CreateBLL<WHMatBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);
                
                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    CheckBox cbxSelect = this.GvList.Rows[i].Cells[0].FindControl("cbxSelect") as CheckBox;

                    if (cbxSelect.Checked == false)
                        continue;

                    WHMat mat = new WHMat();
                    mat.ID = this.GvList.DataKeys[i]["ID"].ToString();
                    mat.MatName = this.GvList.Rows[i].Cells[2].Text;

                    //判断是否已使用
                    bool r = bll.IsUse(mat);
                    if (r == true)
                    {
                        msgList.Add(mat.MatName);
                        continue;
                    }

                    //删除
                    bll.Delete(mat);

                }
                string msg = string.Join(",", msgList.ToArray());
                if (msg != "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSI('提示','" + msg + "货品已使用，无法删除');", true);
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