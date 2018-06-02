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
    /// 获取库存货品信息
    /// </summary>
    public class GetStockMatAH : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";

            string idCode = context.Request.QueryString["IDCode"];

            StockBLL bll = BLLFactory.CreateBLL<StockBLL>();

            WHMatAmount mat = null;

            if (string.IsNullOrEmpty(idCode) == false)
            {
                //根据识别码
                mat = bll.GetStockByBarCode(idCode);
                if (mat == null||mat.ProductAmount==0)
                {
                    mat = new WHMatAmount();
                    mat.MatCode = "none";
                    mat.MatBarCode = "none";
                }
            }            

            context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(mat));
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