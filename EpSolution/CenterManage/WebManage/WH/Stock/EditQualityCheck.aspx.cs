using System;
using System.Collections.Generic;
using System.Linq;
using LAF.WebUI;
using LAF.WebUI.DataSource;
using LAF.WebUI.Util;
using Manage.BLL.WH;
using Manage.Entity.WH;
using System.Web.UI.WebControls;

namespace Manage.Web.WH.Stock
{
    /// <summary>
    /// 编辑质检信息
    /// </summary>
    public partial class EditQualityCheck : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                BindBillList();

                this.BindData();
            }
        }

        #endregion

        #region 绑定数据
        private void BindBillList()
        {
            List<DictInfo> dicts = null;
            //绑定入库单号
            List<InStockBill> whList = BLLFactory.CreateBLL<QualityCheckBLL>().GetNeedInStockBills();
            dicts = whList.Select(p => new DictInfo { ID = p.ID, Des = p.BillNO }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.BillNoList, dicts, "");

        }

        private void BindData()
        {
            string id = Request.QueryString["id"];
            QualityCheckBLL bll = null;
            WHQualityCheck info = new WHQualityCheck();
            DataGridResult<WHQualityCheckResult> pList = new DataGridResult<WHQualityCheckResult>();
            try
            {
                bll = BLLFactory.CreateBLL<QualityCheckBLL>();
                int seq = 1;
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.ID = id;
                    info = bll.Get(info);
                    
                    UIBindHelper.BindForm(this.Page, info);
                    if (info.CheckResult == "0")
                    {
                        RB1.Checked = true;
                    }
                    else
                    {
                        RB2.Checked = true;
                    }
                    this.BillNoList.Items.Add(new ListItem(info.InStockBillNo,info.BillID));
                    this.BillNoList.SelectedValue = info.BillID;
                    this.BillNoList.Enabled = false;
                    this.hiID.Value = info.ID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                    List<WHQualityCheckResult> list = bll.GetResultList(id);
                    foreach (WHQualityCheckResult detail in list)
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
                    info = new WHQualityCheck();
                    this.BillNO.Text = bll.GetNewBillNO();
                    info.CheckPerson = GetLoginInfo().UserName;
                    pList.Total = 0;
                    pList.Rows = new List<WHQualityCheckResult>();
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
            WHQualityCheck info = new WHQualityCheck();
            QualityCheckBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);
                info.BillID = this.BillNoList.SelectedValue;
                info.InStockBillNo = this.BillNoList.SelectedItem.Text;
                info.QualityCheckResults = LAF.Common.Serialization.JsonConvertHelper.DeserializeObject<List<WHQualityCheckResult>>(this.hiPList.Value);
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
