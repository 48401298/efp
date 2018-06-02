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
    public partial class ManageEquipment : ParentPage
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
            EquipmentBLL bll = null;
            DataPage dp = new DataPage();
            EquipmentInfo condition = new EquipmentInfo();
            string eID = Request.QueryString["eID"];
            if (!string.IsNullOrEmpty(eID))
            {
                if (eID.Split('|')[1] == "F")
                {
                    condition.FACTORYPID = eID.Split('|')[0];
                }
                else
                {
                    condition.PRODUCTLINEPID = eID.Split('|')[0];
                }
            }
            try
            {
                bll = BLLFactory.CreateBLL<EquipmentBLL>();
                condition.ECODE = this.ECODE.Text;
                condition.ENAME = this.ENAME.Text;

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                dp=bll.GetList(condition, dp);

                List<EquipmentInfo> list = dp.Result as List<EquipmentInfo>;
                this.GvList.DataSource = list; 
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    string click = string.Format("return edit('{0}');", this.GvList.DataKeys[i]["PID"].ToString());

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
            EquipmentBLL bll = null;
            try
            {
                bll = BLLFactory.CreateBLL<EquipmentBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    if (!bll.HasUsed(key.ToString()))
                    {
                        bll.DeleteEquipment(new EquipmentInfo { PID = key.ToString() });
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('选中设备被使用，不能被删除');", true);
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