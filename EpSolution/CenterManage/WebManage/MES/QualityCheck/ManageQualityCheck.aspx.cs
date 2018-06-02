using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.BLL.MES;
using Manage.Entity.MES;

namespace Manage.Web.MES.QualityCheck
{
    public partial class ManageQualityCheck : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                InitForm();
                this.BindData();
            }
        }

        #endregion

        #region 初始化页面

        private void InitForm()
        {
            List<DictInfo> dicts = null;
            //绑定工厂
            dicts = BLLFactory.CreateBLL<FactoryInfoBLL>().GetList().Select(p => new DictInfo { ID = p.PID, Des = p.PNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.FACTORYPID, dicts, "");

            //绑定产品信息
            dicts = BLLFactory.CreateBLL<ProductInfoBLL>().GetList().Select(p => new DictInfo { ID = p.PID, Des = p.PNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.PRODUCEID, dicts, "");

        }

        #endregion
        #region 绑定数据

        private void BindData()
        {
            QualityCheckBLL bll = null;
            DataPage dp = new DataPage();
            QualityCheckCondition condition = new QualityCheckCondition();
            try
            {
                bll = BLLFactory.CreateBLL<QualityCheckBLL>();
                condition.BillNO = this.BillNO.Text;
                condition.StartDate = this.StartDate.Text;
                condition.EndDate = this.EndDate.Text;
                condition.CheckResult = this.CheckResult.SelectedValue;
                condition.FACTORYPID = this.FACTORYPID.SelectedValue;
                condition.PRODUCEID = this.PRODUCEID.SelectedValue;

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp = bll.GetList(condition, dp);

                List<QualityCheckInfo> list = dp.Result as List<QualityCheckInfo>;
                this.GvList.DataSource = list;
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["ID"].ToString());

                    (this.GvList.Rows[i].Cells[7].Controls[0] as WebControl).Attributes.Add("onclick", click);
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
            QualityCheckBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<QualityCheckBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    bll.Delete(new QualityCheckInfo { ID = key.ToString() });
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
