using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manage.Web.echarts
{
    public partial class bar_simple : System.Web.UI.Page
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
            BarChartData data = new BarChartData();
            data.XList = new List<string> {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            data.Datas = new List<decimal> { 120, 200, 150, 80, 70, 110, 130 };
            this.hiChartData.Value = LAF.Common.Serialization.JsonConvertHelper.GetSerializes(data);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.BindData();
        }
    }
}