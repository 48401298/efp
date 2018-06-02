using System;
using System.Collections.Generic;
using LAF.WebUI;
using Manage.BLL.MES;
using Manage.Entity.MES;

namespace Manage.Web.MES.Tracking
{
    public partial class OnProcessingList : ParentPage
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
            ProduceTrackBLL bll = null;

            string barCode = this.tbBarCode.Text;
            DateTime DtNow = DateTime.Now;
            try
            {
                bll = BLLFactory.CreateBLL<ProduceTrackBLL>();
                List<OnProcessingInfo> list = bll.GetOnProcessingList(barCode);
                if (list != null)
                {
                    if (!string.IsNullOrEmpty(barCode) && list.Count > 0)
                    {
                        Response.Redirect("MaterialTrace.aspx?id=" + list[0].PID);
                    }
                    else
                    {
                        foreach (var item in list)
                        {
                            TimeSpan ts1 = new TimeSpan(DtNow.Ticks);
                            TimeSpan ts2 = new TimeSpan(item.WorkingStartTime.Ticks);
                            TimeSpan ts = ts1.Subtract(ts2).Duration();
                            item.SpendTime = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟"+ts.Seconds.ToString()+"秒";
                            item.CurrentTime = DtNow.Hour + ":" + DtNow.Minute + ":" + DtNow.Second;
                            item.StartTime = item.WorkingStartTime.Hour + ":" + item.WorkingStartTime.Minute + ":" + item.WorkingStartTime.Second;
                        }
                        this.OnPList.DataSource = list;
                        this.OnPList.DataBind();
                    }
                }

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

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../HDDefault.aspx");
        }

    }
}