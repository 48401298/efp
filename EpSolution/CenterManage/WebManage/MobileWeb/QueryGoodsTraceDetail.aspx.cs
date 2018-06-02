using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI.Util;
using Manage.Entity.Inspect;
using LAF.WebUI;
using Manage.BLL.Inspect;
using LAF.Data;
using Manage.Entity.Query;
using Manage.BLL.MES;
using Manage.BLL.Query;


namespace Manage.Web.MobileWeb
{
    /// <summary>
    /// 质量追溯结果查询页面
    /// </summary>
    public partial class QueryGoodsTraceDetail : System.Web.UI.Page
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (this.IsPostBack == false)
            {
                this.BindData();
            }
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string batchNumber = "2017111401";
            string barCode = Request.QueryString["BarCode"];

            TraceGood good = null;

            batchNumber = new GoodPackingBLL().GetBatchNumberByBarCode(barCode);

            if (string.IsNullOrEmpty(batchNumber) == true)
            {
                this.ProduceName.Text = "追溯码无效";
                return;
            }

            //绑定产品基本信息
            good = new QualityTraceQueryBLL().GetTraceGoodByBN(batchNumber);

            if (good == null)
            {
                this.ProduceName.Text = "追溯码无效";
                return;
            }

            this.ProduceName.Text = good.ProductName;
            this.SPECIFICATION.Text = good.SPECIFICATION;
            this.ProductionAddress.Text = good.ProductionAddress;
            this.Manufacturer.Text = good.Manufacturer;
            this.QualityPeriod.Text = good.QualityPeriod;
            this.ProduceDate.Text = good.ProduceDate.ToString("yyyy年MM月dd日");
            this.ProduceBatchNumber.Text = good.BatchNumber;
            this.ProductionLicense.Text = good.ProductionLicense;
            this.ProductStandard.Text = good.ProductStandard;

            //绑定物料组成信息
            List<TraceMaterial> materialList = new QualityTraceQueryBLL().GetTraceMaterial(new TraceGood { BatchNumber = good.BatchNumber });

            materialList=materialList.GroupBy(x => new
                        {
                            x.MatName,
                            x.ProductPlace
                        }).Select(g => new TraceMaterial
                        {
                            MatName = g.Key.MatName,
                            ProductPlace = g.Key.ProductPlace
                        })
                        .ToList();
            
            int seq1 = 1;
            foreach (TraceMaterial tm in materialList)
            {
                this.lblMaterials.Text += string.Format("<tr><td align=center>{0}</td><td align=center>{1}</td><td align=center><a href=\"#\">{2}</a></td></tr>", seq1.ToString(), tm.MatName, tm.ProductPlace);
                seq1++;
            }

            //绑定加工工序信息
            List<TraceProcess> processList = new QualityTraceQueryBLL().GetTraceProcess(new TraceGood { BatchNumber = good.BatchNumber });

            int seq2 = 1;
            foreach (TraceProcess tp in processList)
            {
                this.tblProcess.Text += string.Format("<tr><td align=center>{0}</td><td align=center>{1}</td><td align=center>{2}</td></tr>", seq2.ToString(), tp.ProcessName, tp.EquName);
                seq2++;
            }

        }

        #endregion
    }
}