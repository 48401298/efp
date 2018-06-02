using System;
using System.Collections.Generic;
using System.Linq;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.BLL.MES;
using Manage.Entity.MES;

namespace Manage.Web.MES.Tracking
{
    public partial class MaterialOffline : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.InitForm();
            }
        }

        #endregion

        #region 初始化页面

        private void InitForm()
        {
            List<DictInfo> dicts = null;

            //绑定批次
            List<ProducePlan> whList = BLLFactory.CreateBLL<ProducePlanBLL>().GetList();
            dicts = whList.Select(p => new DictInfo { ID = p.BATCHNUMBER, Des = p.BATCHNUMBER }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.BatchNumber, dicts, null);

            this.OfflineNum.Text = "1";

        }

        #endregion

        #region 保存

        protected void btSave_Click(object sender, EventArgs e)
        {
            GoodInfo info = new GoodInfo();
            GoodInfoBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                bll = BLLFactory.CreateBLL<GoodInfoBLL>();

                //获取生产计划信息
                ProducePlan plan = new ProducePlanBLL().GetByBatchNumber(info.BatchNumber);
                if (plan != null)
                {
                    info.FACTORYPID = plan.FACTORYPID;
                    info.PRID = plan.PRID;
                    info.ProductionID = plan.PRODUCTIONID;
                    info.PLANID = plan.PID;
                }
                //校验是否有漏序
                string result = new ProcessCheckBLL().CheckMissingProcess(info.BatchNumber, info.ProductionID);
                if (result != "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('" + result + "');", true);
                    return;
                }

                bll.Insert(info);
                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('产品完工下线完成');window.location.href='MaterialOffline.aspx';", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 返回

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../HDDefault.aspx");
        }

        #endregion
    }
}