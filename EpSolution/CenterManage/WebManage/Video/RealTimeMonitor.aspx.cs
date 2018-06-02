using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Video;
using Manage.Entity.Video;
using LAF.WebUI;

namespace Manage.Web.Video
{
    public partial class RealTimeMonitor : System.Web.UI.Page
    {
        public string EquPort { get; set; }
        public string EquUserName { get; set; }
        public string EquPassWord { get; set; }
        public string IChannelID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (this.IsPostBack == false)
            {
                VDCamera camera = null;
                VDCameraBLL cbll = BLLFactory.CreateBLL<VDCameraBLL>();
                camera = cbll.Get(new VDCamera { ID = Request.QueryString["id"] });

                this.hiCamera.Value = LAF.Common.Serialization.JsonConvertHelper.GetSerializes(camera);
            }
        }
    }
}