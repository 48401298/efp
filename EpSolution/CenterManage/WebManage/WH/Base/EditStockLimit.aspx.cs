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
    /// 编辑预警设置
    /// </summary>
    public partial class EditStockLimit : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindWHList();
                this.InitForm();
                this.BindData();
            }
        }

        #endregion

        #region 初始化页面

        private void InitForm()
        {
            List<DictInfo> dicts = null;

            //货品
            List<WHMat> matList = BLLFactory.CreateBLL<WHMatBLL>().GetList();
            dicts = matList.Select(p => new DictInfo { ID = p.ID, Des = p.MatName }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.MatID, dicts, Tools.QueryDDLFirstItem);

            //绑定计量单位
            dicts = new MatUnitBLL().GetList().Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.UnitID, dicts, "");
        }

        #endregion

        #region 绑定仓库列表

        private void BindWHList()
        {
            WarehouseBLL bll = null;
            List<Warehouse> array = null;

            bll = BLLFactory.CreateBLL<WarehouseBLL>();
            array = bll.GetList();

            this.Warehourse.DataSource = array;
            this.Warehourse.DataBind();
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            StockLimitBLL bll = null;
            WHStockLimit info = new WHStockLimit();
            string whID=Request.QueryString["whID"];
            try
            {
                if (string.IsNullOrEmpty(whID) == false)
                {
                    this.Warehourse.Enabled = false;
                }
                else
                {
                    this.Warehourse.Enabled = true;
                }


                bll = BLLFactory.CreateBLL<StockLimitBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.Warehourse.SelectedValue = info.Warehouse;
                    this.hiID.Value = info.ID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();

                    this.Warehourse.Enabled = false;
                }
                else
                {
                    info = new WHStockLimit();
                    this.Warehourse.SelectedValue = whID;
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
            WHStockLimit info = new WHStockLimit();
            StockLimitBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<StockLimitBLL>();
                info.Warehouse = this.Warehourse.SelectedValue;
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
    }
}