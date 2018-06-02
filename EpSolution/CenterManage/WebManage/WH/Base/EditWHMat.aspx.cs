using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.WH;
using Manage.Entity.WH;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;

namespace Manage.Web.WH.Base
{
    /// <summary>
    /// 编辑货品信息
    /// </summary>
    public partial class EditWHMat : ParentPage
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

        #region 初始化

        private void InitForm()
        {
            List<DictInfo> dicts = null;

            //绑定货品类别
            dicts = new MatTypeBLL().GetList().Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.ProductType, dicts, "");

            //绑定计量单位
            dicts = new MatUnitBLL().GetList().Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.UnitCode, dicts, "");

            //绑定规格
            dicts = new WHSpecBLL().GetList().Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.SpecCode, dicts, "");
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            WHMatBLL bll = null;
            WHMat info = new WHMat();
            try
            {
                bll = BLLFactory.CreateBLL<WHMatBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.ID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                }
                else
                {
                    info = new WHMat();
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
            WHMat info = new WHMat();
            WHMatBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<WHMatBLL>();

                if (this.hiID.Value == "")
                {
                    bll.Insert(info);
                }
                else
                {
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.ID = this.hiID.Value;
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

        #region 打印货品编码

        protected void lbtPrintCode_Click(object sender, EventArgs e)
        {
            List<string> idCodeList = new List<string>();
            try
            {
                idCodeList.Add(this.MatCode.Text);

                Session["idCodeList"] = idCodeList;
                Session["idcodetext"] = "智慧玉洋-" + "货品编码" + "-" + this.MatName.Text;

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "document.getElementById(\"frmPrint\").src = \"IDCodePrint.aspx\";", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}