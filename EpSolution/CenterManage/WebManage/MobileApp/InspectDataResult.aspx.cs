using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI.Util;
using Manage.Entity.Inspect;
using LAF.WebUI;
using Manage.BLL.Inspect;
using LAF.Data;


namespace Manage.Web.MobileApp
{
    /// <summary>
    /// 环境监测查询页面
    /// </summary>
    public partial class InspectDataResult : System.Web.UI.Page
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
            InspectDataBLL bll = null;
            DataPage dp = new DataPage();

            InspectDataEntity condition = Session["InspectDataCondition"] as InspectDataEntity;

            bll = BLLFactory.CreateBLL<InspectDataBLL>();
            dp.PageIndex = 1;
            dp.PageSize=100;
            dp = bll.GetList(condition, dp);

            List<InspectDataEntity> list = dp.Result as List<InspectDataEntity>;

            this.hiInspectData.Value = LAF.Common.Serialization.JsonConvertHelper.GetSerializes(list);
        }

        #endregion
    }
}