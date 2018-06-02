using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Manage.Web.Pub
{
    /// <summary>
    /// GetCurrentTimeAjaxHandler 的摘要说明
    /// </summary>
    public class GetCurrentTimeAjaxHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";

            string date = context.Request.QueryString["date"];
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Parse(date).Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            string s1 = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            string s2 = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(s2 + "|" + s1));
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