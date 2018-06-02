using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manage.Web.MobileApp
{
    public partial class GoodsTraceQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            Response.Redirect("QueryGoodsTraceDetail.aspx?BarCode=" + this.barCode.Text);
        }
    }
}