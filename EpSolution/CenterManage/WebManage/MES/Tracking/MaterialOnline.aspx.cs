using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI.DataSource;
using Manage.Entity.WH;
using LAF.WebUI;
using Manage.BLL.MES;
using Manage.Entity.MES;
using LAF.WebUI.Util;

namespace Manage.Web.MES.Tracking
{
    public partial class MaterialOnline : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                DataGridResult<InStockDetail> matList = new DataGridResult<InStockDetail>();
                matList.Total = 0;
                matList.Rows = new List<InStockDetail>();
                this.hiMatList.Value = matList.GetJsonSource();
                this.InitForm();
            }
        }

        #endregion

        #region 初始化页面

        private void InitForm()
        {
            List<DictInfo> dicts = null;

            //绑定批次
            List<ProducePlan> whList = BLLFactory.CreateBLL<ProducePlanBLL>().GetList();
            dicts = whList.Select(p => new DictInfo { ID = p.BATCHNUMBER, Des = p.BATCHNUMBER }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.BatchNumber, dicts, null);

            //获取产品信息
            if (string.IsNullOrEmpty(this.BatchNumber.SelectedValue) == false)
            {
                ProducePlanBLL bll = BLLFactory.CreateBLL<ProducePlanBLL>();
                ProductInfo result = bll.GetPNameByIDBatchNumber(this.BatchNumber.SelectedValue);
                this.PRODUCTIONID.Value = result.PID;
                this.PNAME.Text = result.PNAME;
            }
        }

        #endregion

        #region 保存

        protected void btSave_Click(object sender, EventArgs e)
        {
            ProduceTrack info = new ProduceTrack();
            ProduceTrackBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<ProduceTrackBLL>();
                
                if (this.hiID.Value == "")
                {
                    info.MATID = this.hiMatID.Value;
                    info.STATUS = "1";
                    info.WPID = "start";
                    info.MATBARCODE = this.HiMatBarCode.Value;
                    bll.Insert(info);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('原料上线完成');window.location.href='MaterialOnline.aspx';", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 返回

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../HDDefault.aspx");
        }

        #endregion
    }
}