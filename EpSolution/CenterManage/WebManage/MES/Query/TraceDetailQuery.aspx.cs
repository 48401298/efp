using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.Entity.Query;
using Manage.BLL.Query;
using LAF.WebUI.Util;

namespace Manage.Web.MES.Query
{
    /// <summary>
    /// 质量追溯明细
    /// </summary>
    public partial class TraceDetailQuery : ParentPage
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

        #region 绑定信息

        private void BindData()
        {
            TraceGood good = null;
            string pid=Request.QueryString["pid"];

            //绑定产品基本信息
            good = new QualityTraceQueryBLL().GetTraceGood(pid);
            UIBindHelper.BindForm(this.Page, good);

            //绑定物料组成信息
            List<TraceMaterial> materialList = new QualityTraceQueryBLL().GetTraceMaterial(new TraceGood {BatchNumber=good.BatchNumber });
            this.HiMaterial.Value = LAF.Common.Serialization.JsonConvertHelper.GetSerializes(materialList);

            //绑定加工工序信息
            List<TraceProcess> processList = new QualityTraceQueryBLL().GetTraceProcess(new TraceGood { BatchNumber = good.BatchNumber });
            this.HiProcess.Value = LAF.Common.Serialization.JsonConvertHelper.GetSerializes(processList);

        }

        #endregion
    }
}