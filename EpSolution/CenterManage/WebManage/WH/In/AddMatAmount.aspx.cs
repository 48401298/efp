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

namespace Manage.Web.WH.In
{
    public partial class AddMatAmount : ParentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.InitForm();
            }
        }

        #region 初始化页面

        private void InitForm()
        {
            string whID = Request.QueryString["whID"];
            List<DictInfo> dicts = null;
            //绑定仓位
            List<WHSite> siteList = BLLFactory.CreateBLL<WHSiteBLL>().GetList(whID);
            dicts = siteList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.SaveSite, dicts, null);
        }

        #endregion
    }
}