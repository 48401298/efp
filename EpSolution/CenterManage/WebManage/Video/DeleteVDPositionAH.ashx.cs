using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Manage.BLL.Video;
using Manage.Entity.Video;
using LAF.WebUI;

namespace Manage.Web.Video
{
    /// <summary>
    /// GetMatInfoAjaxHandler 的摘要说明
    /// </summary>
    public class DeleteVDPositionAH : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";

            string id = context.Request.QueryString["id"];
            string matCode = context.Request.QueryString["MatCode"];

            VDPositionBLL bll = BLLFactory.CreateBLL<VDPositionBLL>();

            bll.Delete(new VDPosition { ID=id});            

            context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes("ok"));
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