using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI.DataSource;
using LAF.WebUI.Util;
using Manage.Entity.MES;
using LAF.WebUI;
using Manage.BLL.MES;
using LAF.Entity;

namespace Manage.Web.MES.Tracking
{
    public partial class MaterialPacking : ParentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                DataGridResult<GoodPackingForm> matList = new DataGridResult<GoodPackingForm>();
                matList.Total = 0;
                matList.Rows = new List<GoodPackingForm>();
                this.hiMatList.Value = matList.GetJsonSource();
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../HDDefault.aspx");
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            List<GoodPackingForm> list = LAF.Common.Serialization.JsonConvertHelper.DeserializeObject<List<GoodPackingForm>>(this.hiMatList.Value);

            foreach (GoodPackingForm item in list)
            {
                item.PID = Guid.NewGuid().ToString();
                item.GoodBarCode = this.GoodBarCode.Text;
            }

            if (list.Count == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('请添加信息');", true);
                return;
            }

            GoodPackingBLL bll = BLLFactory.CreateBLL<GoodPackingBLL>();

            bll.Packing(list);

            ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('操作成功');window.location.href='MaterialPacking.aspx';", true);
        }
    }
}