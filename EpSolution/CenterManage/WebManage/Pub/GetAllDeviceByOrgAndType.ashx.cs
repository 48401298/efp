using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using LAF.WebUI;
using Manage.BLL.MES;
using Manage.Entity.MES;
using Manage.BLL.Inspect;
using Manage.Entity.Inspect;

namespace Manage.Web.Pub
{
    /// <summary>
    /// GetAllDevicePositionList 的摘要说明
    /// </summary>
    public class GetAllDeviceByOrgAndType : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            string OrganID = context.Request.QueryString["OrganID"];
            string DeviceType = context.Request.QueryString["DeviceType"];
            InspectDeviceEntity param = new InspectDeviceEntity();
            param.OrganID = OrganID;
            param.DeviceType = DeviceType;

            InspectDeviceBLL bll = BLLFactory.CreateBLL<InspectDeviceBLL>();
            List<InspectDeviceEntity> list = bll.GetAllDeviceByOrgAndType(param);
          
            context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(list));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}