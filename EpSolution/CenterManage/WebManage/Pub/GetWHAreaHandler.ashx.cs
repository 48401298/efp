using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Manage.BLL.WH;
using Manage.Entity.WH;
using LAF.WebUI;

namespace Manage.Web.Pub
{
    /// <summary>
    /// 获取区域信息
    /// </summary>
    public class GetWHAreaHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";

            string whID = context.Request.QueryString["whID"];

            List<WHArea> list = new WHAreaBLL().GetList(whID);

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