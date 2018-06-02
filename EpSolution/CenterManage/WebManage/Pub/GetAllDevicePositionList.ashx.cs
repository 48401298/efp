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
    public class GetAllDevicePositionList : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";

            InspectMapBLL bll = BLLFactory.CreateBLL<InspectMapBLL>();
            List<InspectDeviceEntity> list = bll.GetList(new InspectDeviceEntity());

            foreach(InspectDeviceEntity ide in list)
            {
                if (ide.LastLoginTime != null)
                {
                    //当前时间比设备最后在线时间小于10分钟,设备在线+
                    if (DateTime.Now < ide.LastLoginTime.AddMinutes(10))
                    {
                        ide.onlineStatus = "online";
                    }
                    else
                    {
                        ide.onlineStatus = "offline";
                    }
                }
            }
          
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