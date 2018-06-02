using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI;
using LAF.WebUI.Util;
using Manage.BLL.MES;
using Manage.Entity.MES;

namespace Manage.Web.MES.Base
{
    /// <summary>
    /// 设备编辑
    /// </summary>
    public partial class EditEquipment : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindFactoryList();
                this.BindProductLineList();
                this.BindData();
            }
        }

        #endregion

        #region 绑定数据
        private void BindFactoryList()
        {
            List<DictInfo> dicts = null;
            //绑定批次
            List<FactoryInfo> whList = BLLFactory.CreateBLL<FactoryInfoBLL>().GetList();
            dicts = whList.Select(p => new DictInfo { ID = p.PID, Des = p.PNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.FACTORYPID, dicts, "");

        }

        private void BindProductLineList()
        {
            List<DictInfo> dicts = null;
            //绑定批次
            List<ProductLine> whList = BLLFactory.CreateBLL<ProductLineBLL>().GetList();
            dicts = whList.Select(p => new DictInfo { ID = p.PID, Des = p.PLNAME }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.PRODUCTLINEPID, dicts, "");
        }

        private void BindData()
        {
            string id = Request.QueryString["id"];
            EquipmentBLL bll = null;
            EquipmentInfo info = new EquipmentInfo();
            try
            {
                bll = BLLFactory.CreateBLL<EquipmentBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.PID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.PID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                }
                else
                {
                    info = new EquipmentInfo();
                }
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
            EquipmentInfo info = new EquipmentInfo();
            EquipmentBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<EquipmentBLL>();

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

        protected void lbtPrintCode_Click(object sender, EventArgs e)
        {
            List<string> idCodeList = new List<string>();
            try
            {
                idCodeList.Add(this.BARCODE.Text);

                Session["idCodeList"] = idCodeList;
                Session["idcodetext"] = "智慧玉洋-" + "设备编码" + "-" + this.ENAME.Text;

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "document.getElementById(\"frmPrint\").src = \"../../Pub/IDCodePrint.aspx\";", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}