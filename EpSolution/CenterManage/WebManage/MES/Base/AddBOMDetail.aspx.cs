using System;
using System.Collections.Generic;
using System.Linq;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.BLL.WH;

namespace Manage.Web.MES.Base
{
    /// <summary>
    /// 添加产品BOM明细
    /// </summary>
    public partial class AddBOMDetail : ParentPage
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
            //绑定计量单位
            dicts = BLLFactory.CreateBLL<MatUnitBLL>().GetList().Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.Unit, dicts, "");
            //货品
            dicts = BLLFactory.CreateBLL<WHMatBLL>().GetList().Select(p => new DictInfo { ID = p.ID, Des = p.MatName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.MATRIALID, dicts, "");
            this.AMOUNT.Text = "0";
        }

        #endregion
    }
}