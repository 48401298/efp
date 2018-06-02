using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;

namespace Manage.Web.Organ
{
    /// <summary>
    /// 组织机构管理
    /// </summary>
    public partial class ManageOrgan : ParentPage
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
            OrgaizationManageBLL bll = null;
            Orgaization condition = new Orgaization();
            List<Orgaization> list = null;
            try
            {
                bll = BLLFactory.CreateBLL<OrgaizationManageBLL>();

                list = bll.GetAllList();

                this.hiOrganList.Value = this.GetTreeNodes(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取机构信息树

        /// <summary>
        /// 获取位置信息树
        /// </summary>
        /// <returns></returns>
        public string GetTreeNodes(List<Orgaization> list)
        {
            List<TreeNodeResult> nodes = new List<TreeNodeResult>();
            List<Orgaization> childList = null;
            try
            {
                TreeNodeResult rootNode = new TreeNodeResult();
                rootNode.Tid = "root";
                rootNode.Ttext = ConfigurationManager.AppSettings["rootOrgan"];

                childList = list.Where(p => p.OrganParent == "root").ToList<Orgaization>();

                foreach (Orgaization info in childList)
                {
                    TreeNodeResult node = new TreeNodeResult();
                    node.Tid = info.OrganID;
                    node.Ttext = info.OrganDESC;

                    //添加子机构
                    this.BuildChildItems(node, list);

                    rootNode.AddchildNode(node);                    
                }
                nodes.Add(rootNode);
                return TreeNodeResult.GetResultJosnS(nodes.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建子机构信息
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="childPowers">子权限</param>
        private void BuildChildItems(TreeNodeResult parentNode, List<Orgaization> list)
        {
            List<Orgaization> childList = null;
            childList = list.Where(p => p.OrganParent == parentNode.Tid).ToList<Orgaization>();

            foreach (Orgaization info in childList)
            {
                TreeNodeResult node = new TreeNodeResult();
                node.Tid = info.OrganID;
                node.Ttext = info.OrganDESC;
                
                //添加子机构
                this.BuildChildItems(node, list);

                parentNode.AddchildNode(node);
            }
        }

        #endregion

        #region 查询

        protected void btQuery_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        #endregion

        #region 增加

        protected void btAdd_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 删除

        protected void btDelete_Click(object sender, EventArgs e)
        {
            //ArrayList pkArray = null;
            //WHInModeBLL bll = null;
            //try
            //{
            //    bll = BLLFactory.CreateBLL<WHInModeBLL>();

            //    pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

            //    foreach (object key in pkArray)
            //    {
            //        bll.DeleteWHInMode(new WHInMode { ID = key.ToString() });
            //    }

            //    this.BindData();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        #endregion
    }
}