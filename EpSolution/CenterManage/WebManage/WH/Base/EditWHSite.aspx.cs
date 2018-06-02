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
using Manage.Entity;

namespace Manage.Web.WH.Base
{
    /// <summary>
    /// 编辑仓位信息
    /// </summary>
    public partial class EditWHSite : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindWHList();
                this.BindData();
            }
        }

        #endregion

        #region 绑定仓库列表

        private void BindWHList()
        {
            WarehouseBLL bll = null;
            List<Warehouse> array = null;

            bll = BLLFactory.CreateBLL<WarehouseBLL>();
            array = bll.GetList();

            this.Warehouse.DataSource = array;
            this.Warehouse.DataBind();
            
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            string id = Request.QueryString["id"];
            WHSiteBLL bll = null;
            WHSite info = new WHSite();
            string whID=Request.QueryString["whID"];
            try
            {
                if (string.IsNullOrEmpty(whID) == false)
                {
                    this.Warehouse.Enabled = false;
                }
                else
                {
                    this.Warehouse.Enabled = true;
                }


                bll = BLLFactory.CreateBLL<WHSiteBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.Warehouse.SelectedValue = info.WHID;
                    this.hiID.Value = info.ID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();

                    this.Warehouse.Enabled = false;
                }
                else
                {
                    info = new WHSite();
                    if (string.IsNullOrEmpty(whID) == false)
                    {
                        this.Warehouse.SelectedValue = whID;
                    }
                }

                List<DictInfo> dicts = null;
                //绑定区域
                if (string.IsNullOrEmpty(this.Warehouse.SelectedValue) == false)
                {
                    List<WHArea> whList = BLLFactory.CreateBLL<WHAreaBLL>().GetList(this.Warehouse.SelectedValue);
                    dicts = whList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
                    Tools.BindDataToDDL(this.AreaID, dicts,null);

                    if (string.IsNullOrEmpty(info.AreaID) == false)
                    {
                        this.AreaID.SelectedValue = info.AreaID;
                    }
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
            WHSite info = new WHSite();
            WHSiteBLL bll = null;
            DataResult<int> result = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<WHSiteBLL>();
                info.WHID = this.Warehouse.SelectedValue;
                if (this.hiID.Value == "")
                {
                    result=bll.Insert(info);
                }
                else
                {
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.ID = this.hiID.Value;
                    result=bll.Update(info);

                }

                if (string.IsNullOrEmpty(result.Msg) == false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSI('提示','" + result.Msg + "')", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "parent.refreshData();parent.closeAppWindow1();", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}