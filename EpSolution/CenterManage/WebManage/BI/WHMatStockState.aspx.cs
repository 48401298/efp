using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.BI;
using Manage.Entity.BI;
using Manage.Entity.WH;

namespace Manage.Web.BI
{
    /// <summary>
    /// 仓库货品库存统计
    /// </summary>
    public partial class WHMatStockState : ParentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                BarChartResult chartResult = new BarChartResult();
                BarChartData seriesData = new BarChartData();
                string whID = Request.QueryString["whID"];


                if (string.IsNullOrEmpty(whID) == true)
                    return;

                List<WHMatAmount> list = new MatStockStateBLL().GetMatStockListByWH(whID);

                chartResult.XAxisData = new List<string>();
                chartResult.Series = new List<BarChartData>();
                seriesData.data = new List<decimal>();

                foreach (WHMatAmount item in list)
                {
                    if (chartResult.XAxisData.IndexOf(item.MatName) < 0)
                    {
                        chartResult.XAxisData.Add(item.MatName);
                    }
                    seriesData.data.Add(item.MainAmount);
                }

                chartResult.Series.Add(seriesData);

                this.hiChartData.Value = LAF.Common.Serialization.JsonConvertHelper.GetSerializes(chartResult);
            }
        }
    }
}