using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Manage.BLL.MES;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.Entity.MES;

namespace Manage.Web.MES.Tracking
{
    /// <summary>
    /// 生产监控
    /// </summary>
    public partial class ProducePlanManage : ParentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindData();
            }
        }

        private void BindData()
        {
            ProducePlanBLL bll = new ProducePlanBLL();

            List<ProducePlan> list = bll.GetUnFinishedPlans(new ProducePlan());

            this.OnPList.DataSource = list;
            this.OnPList.DataBind();
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../HDDefault.aspx");
        }

        protected void OnPList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "finished")
            {
                new ProducePlanBLL().SignPlanFinished(new ProducePlan {PID=e.CommandArgument.ToString() });
                this.BindData();
            }
        }
    }
}