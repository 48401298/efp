using System;
using System.Collections.Generic;
using System.Linq;
using LAF.Common.Serialization;
using LAF.WebUI;
using LAF.WebUI.DataSource;
using LAF.WebUI.Util;
using Manage.BLL.MES;
using Manage.BLL.WH;
using Manage.Entity.MES;

namespace Manage.Web.MES.Base
{
    /// <summary>
    /// 编辑产品BOM信息
    /// </summary>
    public partial class EditBOM : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.InitForm();
                this.BindData();
            }
        }

        #endregion

        #region 初始化页面

        private void InitForm()
        {
            List<DictInfo> dicts = null;
            //绑定产品信息
            dicts = BLLFactory.CreateBLL<ProductInfoBLL>().GetList().Select(p => new DictInfo { ID = p.PID, Des = p.PNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.PRODUCEID, dicts, "");
            //绑定计量单位
            dicts = BLLFactory.CreateBLL<MatUnitBLL>().GetList().Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.MainUnit, dicts, "");
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            ProduceDOMBLL bll = null;
            ProduceBOM info = new ProduceBOM();
            try
            {
                bll = BLLFactory.CreateBLL<ProduceDOMBLL>();
                if (!string.IsNullOrEmpty(id))
                {
                    info.PID = id;
                    info = bll.Get(info);
                    info.Details = bll.GetList(info.PID);
                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.PID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
          
                }
                else
                {
                    info = new ProduceBOM();
                    info.Details = new List<BOMDetail>();
                }

                //绑定明细
                DataGridResult<BOMDetail> bomDetail = new DataGridResult<BOMDetail>();
                bomDetail.Total = info.Details.Count;
                bomDetail.Rows = info.Details;

                foreach (BOMDetail detail in info.Details)
                {
                    detail.DeleteAction = "deleteItem(\'" + detail.MATRIALID + "\')";
                }

                this.hiBomDetailList.Value = bomDetail.GetJsonSource();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 保存

        protected void btSave_Click(object sender, EventArgs e)
        {
            ProduceBOM info = new ProduceBOM();
            ProduceDOMBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<ProduceDOMBLL>();

                info.Details = JsonConvertHelper.DeserializeObject<List<BOMDetail>>(this.hiBomDetailList.Value);

                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                }
                else
                {
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.PID = this.hiID.Value;
                    bll.Update(info);
                }
                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.refreshData();parent.closeAppWindow1();", true);

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
            Response.Redirect("ManageBOM.aspx");
        }

        #endregion
    }
}