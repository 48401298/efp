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
    /// 获取货品信息
    /// </summary>
    public class GetMatInfoAjaxHandler : IHttpHandler,IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";

            string idCode = context.Request.QueryString["IDCode"];
            string matCode = context.Request.QueryString["MatCode"];

            WHMatBLL bll = BLLFactory.CreateBLL<WHMatBLL>();

            WHMat mat = null;

            bool exists = false;

            if (string.IsNullOrEmpty(idCode) == false)
            {
                //根据识别码
                mat=bll.GetMatByIDCode(idCode);
                if (mat != null)
                {
                    mat.BarCode = idCode;

                    //判断条码是否已入库
                    WHMatAmount amount = new StockBLL().GetStockByBarCode(idCode);
                    exists = amount != null ? true : false;
                    if (amount != null)
                    {
                        mat.Warehouse = amount.Warehouse;
                        mat.SaveSite = amount.SaveSite;
                    }
                }
            }
            else
            {
                //根据货品编号
                mat = bll.GetMatByMatCode(matCode);
                if (mat!=null)
                    mat.BarCode = "";
            }

            if (mat == null)
            {
                mat = new WHMat();
                if (mat != null)
                    mat.MatCode = "none";
            }

            MatGetResult result = LAF.Common.Util.BindHelper.CopyToModel<MatGetResult, WHMat>(mat);
            result.CheckResult=exists==true?"该条码已入库，无需再次操作":"";

            context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(result));
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