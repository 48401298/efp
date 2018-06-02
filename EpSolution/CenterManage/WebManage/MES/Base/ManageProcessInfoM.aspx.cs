using System;
using System.Collections.Generic;
using System.Linq;
using LAF.WebUI;
using LAF.WebUI.DataSource;
using Manage.BLL.MES;
using Manage.Entity.MES;

namespace Manage.Web.MES.Base
{
    public partial class ManageProcessInfoM : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindWS();
            }
        }

        #endregion

        #region 绑定数据

        private void BindWS()
        {
            this.HiEList.Value = this.GetWSList();
        }

        #endregion

        #region 获取仓库树数据源

        /// <summary>
        /// 获取仓库树数据源
        /// </summary>
        /// <returns></returns>
        public string GetWSList()
        {
            List<TreeNodeResult> list = new List<TreeNodeResult>();
            FactoryInfoBLL bllF = null;
            ProcessFlowBLL bllP = null;
            List<FactoryInfo> arrayF = null;
            List<ProcessFlow> arrayP = null;
            try
            {
                bllF = BLLFactory.CreateBLL<FactoryInfoBLL>();
                bllP = BLLFactory.CreateBLL<ProcessFlowBLL>();
                arrayF = bllF.GetList();
                arrayP = bllP.GetList();
                TreeNodeResult rootNode = new TreeNodeResult();
                rootNode.Tid = "";
                rootNode.Ttext = "工厂/工艺流程";
                foreach (FactoryInfo infoF in arrayF)
                {
                    TreeNodeResult node = new TreeNodeResult();
                    node.Tid = infoF.PID + "|F";
                    node.Ttext = infoF.PCODE + "|" + infoF.PNAME;
                    rootNode.AddchildNode(node);
                    List<ProcessFlow> plines = arrayP.Where(o => o.FACTORYPID == infoF.PID).ToList();
                    foreach (ProcessFlow infoP in plines)
                    {
                        TreeNodeResult nodeP = new TreeNodeResult();
                        nodeP.Tid = infoP.PID + "|P";
                        nodeP.Ttext = infoP.PCODE + "|" + infoP.PNAME;
                        node.AddchildNode(nodeP);

                    }
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