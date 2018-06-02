using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Manage.BLL.Sys;
using Manage.Entity.Sys;
using LAF.WebUI;
using LAF.WebUI.DataSource;
using System.Configuration;

namespace Manage.Web.User
{
    /// <summary>
    /// GetOrgansHandler 的摘要说明
    /// </summary>
    public class GetOrgansHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";

            context.Response.Write(GetTreeNodes());
        }

        #region 获取机构信息树


        /// <summary>
        /// 获取位置信息树
        /// </summary>
        /// <returns></returns>
        public string GetTreeNodes()
        {
            OrgaizationManageBLL bll = new OrgaizationManageBLL();
            List<Orgaization> list = null;
            try
            {
                list = bll.GetAllList();

            }
            catch (Exception ex)
            {
                throw ex;
            }

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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}