using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using LAF.WebUI;
using Manage.BLL.MES;
using Manage.Entity.MES;

namespace Manage.Web.Pub
{
    /// <summary>
    /// GetProductInfoAjaxHandler 的摘要说明
    /// </summary>
    public class GetProductInfoAjaxHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            string type = context.Request.QueryString["Type"];
            string code = context.Request.QueryString["Code"];
            string batchNumber = "";
            switch (type)
            {
                case "CP":
                    ProducePlanBLL bll = BLLFactory.CreateBLL<ProducePlanBLL>();
                    ProductInfo result = bll.GetPNameByIDBatchNumber(code);
                    if (result == null)
                    {
                        result = new ProductInfo();
                        if (result != null)
                            result.PID = "none";
                    }
                    context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(result));
                    break;
                case "CP2":
                    ProducePlanBLL bllGoodInfo = BLLFactory.CreateBLL<ProducePlanBLL>();
                    GoodInfo resultGoodInfo = bllGoodInfo.GetGoodInfoByBatchNumber(code);
                    if (resultGoodInfo == null)
                    {
                        resultGoodInfo = new GoodInfo();
                        if (resultGoodInfo != null)
                            resultGoodInfo.PID = "none";
                    }
                    context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(resultGoodInfo));
                    break;
                case "CB":
                    EquipmentBLL bll2 = BLLFactory.CreateBLL<EquipmentBLL>();
                    EquipmentInfo result2 = bll2.GetInfoByBarCode(code);
                    if (result2 == null)
                    {
                        result2 = new EquipmentInfo();
                        if (result2 != null)
                            result2.PID = "none";
                    }
                    context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(result2));
                    break;
                case "GX"://根据工位条码获取加工工序
                    ProcessInfoBLL bll3 = BLLFactory.CreateBLL<ProcessInfoBLL>();
                    Manage.Entity.MES.ProcessInfo result3 = bll3.GetInfoByWS(code);
                    if (result3 == null)
                    {
                        result3 = new Manage.Entity.MES.ProcessInfo();
                        if (result3 != null)
                            result3.PID = "none";
                    }
                    context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(result3));
                    break;
                case "GX2":
                    ProcessInfoBLL bll4 = BLLFactory.CreateBLL<ProcessInfoBLL>();
                    batchNumber = context.Request.QueryString["BatchNumber"];
                    Manage.Entity.MES.ProcessInfo result4 = bll4.GetInfoByBarCodeAndBatchNumber(code, batchNumber);
                    if (result4 == null)
                    {
                        result4 = new Manage.Entity.MES.ProcessInfo();
                        if (result4 != null)
                            result4.PID = "none";
                    }
                    context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(result4));
                    break;
                case "PP":
                    ProducePlanBLL ppbll = BLLFactory.CreateBLL<ProducePlanBLL>();
                    SupplyInfoBLL sibll = BLLFactory.CreateBLL<SupplyInfoBLL>();
                    string id = context.Request.QueryString["id"];
                    SupplyInfo siresult = ppbll.GetProducePlanInfoByID(id);
                    if (siresult == null)
                    {
                        siresult = new SupplyInfo();
                        if (siresult != null)
                            siresult.PID = "none";
                    }
                    else
                    {
                        siresult.Details = sibll.GetMaterialListByBOM(siresult.ProductionID);
                    }

                    context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(siresult));
                    break;
                case "ZJ":
                    QualityCheckBLL qcBll = BLLFactory.CreateBLL<QualityCheckBLL>();
                    batchNumber = context.Request.QueryString["BatchNumber"];
                    QualityCheckInfo qualityCheckInfo = qcBll.GetPDInfo(batchNumber);
                    if (qualityCheckInfo == null)
                    {
                        qualityCheckInfo = new QualityCheckInfo();
                        qualityCheckInfo.ID = "none";
                    }
                    context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(qualityCheckInfo));
                    break;
                default:
                    break;
            }

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