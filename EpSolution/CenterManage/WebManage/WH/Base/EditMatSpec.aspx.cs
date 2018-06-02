using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.WH;
using Manage.Entity.WH;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;

namespace Manage.Web.WH.Base
{
    /// <summary>
    /// 编辑货品规格信息
    /// </summary>
    public partial class EditMatSpec : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.InitPage();
                this.BindData();
            }
        }

        #endregion

        #region 初始化

        private void InitPage()
        {
            List<DictInfo> dicts = null;

            string matID = Request.QueryString["matID"];
            this.hiMatID.Value = matID;
            //绑定规格
            List<MatUnit> specList = BLLFactory.CreateBLL<WHMatSpecBLL>().GetMayUnits(matID);
            dicts = specList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.ChangeUnit, dicts, null);
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            WHMatSpecBLL bll = null;
            WHMatSpec info = new WHMatSpec();
            try
            {
                bll = BLLFactory.CreateBLL<WHMatSpecBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.ID;
                }
                else
                {
                    info = new WHMatSpec();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 保存

        protected void btSave_Click(object sender, EventArgs e)
        {
            WHMatSpec info = new WHMatSpec();
            WHMatSpecBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<WHMatSpecBLL>();
                info.MatID = this.hiMatID.Value;
                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                }
                else
                {
                    info.ID = this.hiID.Value;
                    bll.Update(info);

                }

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.refreshData();parent.closeAppWindow1();", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}