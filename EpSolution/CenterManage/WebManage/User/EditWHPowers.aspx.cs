using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Menu;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;
using Manage.Entity.WH;
using Manage.BLL.WH;

namespace Manage.Web.User
{
    /// <summary>
    /// 编辑仓库权限
    /// </summary>
    public partial class EditWHPowers : ParentPage
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
            string id = Request.QueryString["userID"];
            UserManageBLL bll = null;
            Entity.Sys.User info = new Entity.Sys.User();
            try
            {
                bll = BLLFactory.CreateBLL<UserManageBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.UserID = id;
                    info = bll.Get(info);

                    UIBindHelper.BindForm(this.Page, info);
                    this.hiID.Value = info.UserID;
                    this.HiCREATEUSER.Value = info.CreateUser;
                    this.HiCREATETIME.Value = info.CreateTime.ToString();
                }
                info.WHPowers = new WHPowerBLL().GetWHPowers(info.UserID);
                this.HiWHList.Value = this.GetWarehouseList(info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取仓库权限

        /// <summary>
        /// 获取仓库权限
        /// </summary>
        /// <returns></returns>
        public string GetWarehouseList(Entity.Sys.User user)
        {
            List<TreeNodeResult> list = new List<TreeNodeResult>();
            List<Warehouse> whArray = null;
            try
            {
                whArray = new WarehouseBLL().GetList();
                foreach (Warehouse r in whArray)
                {
                    TreeNodeResult node = new TreeNodeResult();
                    node.Tid = r.ID;
                    node.Ttext = r.Description;

                    node.TChecked = user.WHPowers.Exists(p => p.WarehouseID == r.ID);

                    list.Add(node);
                }
                return TreeNodeResult.GetResultJosnS(list.ToArray());
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
            Entity.Sys.User info = new Entity.Sys.User();
            WHPowerBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                info.UserID = this.hiID.Value;
                bll = BLLFactory.CreateBLL<WHPowerBLL>();

                //绑定权限
                if (this.HiSelectedWHList.Value != "")
                {
                    string[] roles = this.HiSelectedWHList.Value.Split(",".ToCharArray());

                    info.WHPowers = new List<WarehousePower>();

                    foreach (string whID in roles)
                    {
                        WarehousePower r = new WarehousePower();
                        r.WarehouseID = whID;
                        info.WHPowers.Add(r);
                    }
                }
                else
                {
                    info.WHPowers = new List<WarehousePower>();
                }
                bll.SaveWHPowers(info.UserID, info.WHPowers);
                

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