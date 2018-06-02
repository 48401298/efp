using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.WH;
using Manage.Entity.WH;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;

namespace Manage.Web.WH.Base
{
    /// <summary>
    /// 仓库管理
    /// </summary>
    public partial class ManageWHSiteFrm : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindWH();
            }
        }

        #endregion

        #region 绑定数据

        private void BindWH()
        {
            this.HiWHList.Value = this.GetWHList();
        }

        #endregion

        #region 获取仓库树数据源

        /// <summary>
        /// 获取仓库树数据源
        /// </summary>
        /// <returns></returns>
        public string GetWHList()
        {
            List<TreeNodeResult> list = new List<TreeNodeResult>();
            WarehouseBLL bll = null;            
            List<Warehouse> array = null;
            try
            {
                bll = BLLFactory.CreateBLL<WarehouseBLL>();
                array = bll.GetList();
                TreeNodeResult rootNode = new TreeNodeResult();
                rootNode.Tid = "";
                rootNode.Ttext = "仓库";
                foreach (Warehouse info in array)
                {
                    TreeNodeResult node = new TreeNodeResult();
                    node.Tid = info.ID;
                    node.Ttext = info.Code+"|"+info.Description;
                    rootNode.AddchildNode(node);
                    
                }
                list.Add(rootNode);
                return TreeNodeResult.GetResultJosnS(list.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}