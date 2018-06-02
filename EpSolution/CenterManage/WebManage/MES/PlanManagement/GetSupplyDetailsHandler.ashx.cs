using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using LAF.WebUI;
using Manage.BLL.MES;
using Manage.Entity.MES;

namespace Manage.Web.MES.PlanManagement
{
    /// <summary>
    /// 获取要活明细列表
    /// </summary>
    public class GetSupplyDetailsHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            ProducePlanBLL ppbll = BLLFactory.CreateBLL<ProducePlanBLL>();
            SupplyInfoBLL sibll = BLLFactory.CreateBLL<SupplyInfoBLL>();
            string planID = context.Request.QueryString["planID"];
            SupplyInfo siresult = ppbll.GetProducePlanInfoByID(planID);
            if (siresult == null)
            {
                siresult = new SupplyInfo();
                if (siresult != null)
                    siresult.PID = "none";
            }
            else
            {
                //获取生产计划
                ProducePlan plan=new ProducePlanBLL().Get(new ProducePlan{PID=planID});

                //获取bom明细
                siresult.Details = sibll.GetMaterialListByBOM(siresult.ProductionID);

                //获取bom基本信息
                ProduceBOM bomBase = new ProduceDOMBLL().GetByProduceID(siresult.ProductionID);

                //生成要货明细
                foreach (SupplyMaterialInfo detail in siresult.Details)
                {
                    if (bomBase.Amount != 0)
                    {
                        detail.AMOUNT = Convert.ToInt32(decimal.Parse(plan.PLANAMOUNT)) / bomBase.Amount;
                    }
                    else
                    {
                        detail.AMOUNT = 0; 
                    }
                }

            }

            context.Response.Write(LAF.Common.Serialization.JsonConvertHelper.GetSerializes(siresult));

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