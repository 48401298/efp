using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.WebUI;

namespace Manage.Web.Organ
{
    /// <summary>
    /// GetMatInfoAjaxHandler 的摘要说明
    /// </summary>
    public class DeleteOrganAH : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";

            string id = context.Request.QueryString["id"];

            OrgaizationManageBLL bll = BLLFactory.CreateBLL<OrgaizationManageBLL>();

            bll.Delete(new Orgaization { OrganID=id});            

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