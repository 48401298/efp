using System;
using System.Collections.Generic;
using System.Linq;
using LAF.WebUI;
using LAF.WebUI.DataSource;
using LAF.WebUI.Util;
using Manage.BLL.MES;
using Manage.Entity.MES;

namespace Manage.Web.MES.QualityCheck
{
    public partial class EditQualityCheck : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                BindFactoryList();

                this.BindData();
            }
        }

        #endregion

        #region 绑定数据
        private void BindFactoryList()
        {
            List<DictInfo> dicts = null;
            //绑定批次
            List<ProducePlan> whList = BLLFactory.CreateBLL<ProducePlanBLL>().GetCList();
            dicts = whList.Select(p => new DictInfo { ID = p.BATCHNUMBER, Des = p.BATCHNUMBER }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.BatchNumber, dicts, "");

        }

        private void BindData()
        {
            string id = Request.QueryString["id"];
            QualityCheckBLL bll = null;
            QualityCheckInfo info = new QualityCheckInfo();
            DataGridResult<QualityCheckResult> pList = new DataGridResult<QualityCheckResult>();
            try
            {
                bll = BLLFactory.CreateBLL<QualityCheckBLL>();
                int seq = 0;
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.Get(info);
                    if (!string.IsNullOrEmpty(info.BatchNumber))
                    {
                        QualityCheckBLL qcBll = BLLFactory.CreateBLL<QualityCheckBLL>();                        
                        QualityCheckInfo qualityCheckInfo = qcBll.GetPDInfo(info.BatchNumber);
                        if (qualityCheckInfo != null)
                        {
                            info.PDDATE = qualityCheckInfo.PDDATE;
                            info.PDCODE = qualityCheckInfo.PDCODE;
                            info.PDNAME = qualityCheckInfo.PDNAME;
                        }
                    }
                    UIBindHelper.BindForm(this.Page, info);
                    if (info.CheckResult == "0")
                    {
                        RB1.Checked = true;
                    }
                    else
                    {
                        RB2.Checked = true;
                    }
                    this.BatchNumber.Enabled = false;
                    this.hiID.Value = info.ID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                    List<QualityCheckResult> list = bll.GetResultList(id);
                    foreach (QualityCheckResult detail in list)
                    {
                        detail.DeleteAction = "deleteF(\'" + detail.FileName + "\')";

                        detail.DetailAction = "viewF(\'" + detail.FileName + "\')";
                        detail.SEQ = seq++;
                    }
                    pList.Total = list.Count;
                    pList.Rows = list;
                    
                }
                else
                {
                    info = new QualityCheckInfo();
                    
                    info.CheckPerson = GetLoginInfo().UserName;
                    pList.Total = 0;
                    pList.Rows = new List<QualityCheckResult>();
                    UIBindHelper.BindForm(this.Page, info);
                }

                this.hiPList.Value = pList.GetJsonSource();
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
            QualityCheckInfo info = new QualityCheckInfo();
            QualityCheckBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                info.QualityCheckResults = LAF.Common.Serialization.JsonConvertHelper.DeserializeObject<List<QualityCheckResult>>(this.hiPList.Value);
                bll = BLLFactory.CreateBLL<QualityCheckBLL>();
                if (RB1.Checked)
                {
                    info.CheckResult = "0";
                }
                else
                {
                    info.CheckResult = "1";
                }
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
