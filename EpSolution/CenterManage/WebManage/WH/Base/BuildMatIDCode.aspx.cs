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

namespace Manage.Web.WH.Base
{
    /// <summary>
    /// 生成货品唯一识别码
    /// </summary>
    public partial class BuildMatIDCode : ParentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.BindMatInfo();
                this.BindData();
            }
        }

        #region 绑定货品信息

        private void BindMatInfo()
        {
            string matID = Request.QueryString["matID"];
            List<DictInfo> dicts = null;

            WHMat mat = BLLFactory.CreateBLL<WHMatBLL>().Get(new WHMat {ID=matID });

            this.MatCode.Text = mat.MatCode;
            this.MatName.Text = mat.MatName;
            this.hiMatID.Value = matID;

            //绑定规格
            List<MatUnit> specList = BLLFactory.CreateBLL<WHMatSpecBLL>().GetMayUnits(matID);
            dicts = specList.Select(p => new DictInfo { ID = p.ID, Des = p.Description }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.MatSpec, dicts, null);
        }

        #endregion

        #region 绑定数据

        private void BindData()
        {
            WHMatBLL bll = null;
            DataPage dp = new DataPage();
            WHMat condition = new WHMat();

            try
            {
                bll = BLLFactory.CreateBLL<WHMatBLL>();

                PagerHelper.InitPageControl(this.AspNetPager1, dp, true);
                condition.ID = this.hiMatID.Value;
                condition.IDCodeStatus = this.CodeStatus.SelectedValue;
                condition.SpecCode = this.MatSpec.SelectedValue;
                dp = bll.GetMatIDCodeList(condition, dp);

                List<MatIDCode> list = dp.Result as List<MatIDCode>;
                this.GvList.DataSource = list;
                this.GvList.DataBind();

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    Label lblBarCode = this.GvList.Rows[i].Cells[2].FindControl("lblBarCode") as Label;
                    lblBarCode.Text = this.GvList.Rows[i].Cells[1].Text;
                }

                PagerHelper.SetPageControl(AspNetPager1, dp, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 设置样式

        protected void GvList_PreRender(object sender, EventArgs e)
        {
            GvHelper.DatagridSkin(this.GvList);
        }

        protected void GvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GvHelper.DatagridSkinUpdate(e);
        }

        #endregion

        #region 分页

        protected void AspNetPager1_PageChanged(object src, Wuqi.Webdiyer.PageChangedEventArgs e)
        {
            this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            this.BindData();
        }

        #endregion

        #region 打印

        protected void btPrint_Click(object sender, EventArgs e)
        {
            ArrayList pkArray = null;
            List<string> idCodeList = new List<string>();
            try
            {
                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                foreach (object key in pkArray)
                {
                    idCodeList.Add(key.ToString());
                }

                Session["idCodeList"] = idCodeList;
                Session["idcodetext"] = "智慧玉洋-"+"货品条码"+"-"+this.MatName.Text;

                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "document.getElementById(\"frmPrint\").src = \"IDCodePrint.aspx\";", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除

        protected void btDelete_Click(object sender, EventArgs e)
        {
            ArrayList pkArray = null;
            WHMatBLL bll = null;
            List<string> msgList = new List<string>();
            try
            {
                bll = BLLFactory.CreateBLL<WHMatBLL>();

                pkArray = GvHelper.GetPKValueByChk(this.GvList, 0, "cbxSelect", 0);

                for (int i = 0; i < this.GvList.Rows.Count; i++)
                {
                    CheckBox cbxSelect = this.GvList.Rows[i].Cells[0].FindControl("cbxSelect") as CheckBox;

                    if (cbxSelect.Checked == false)
                        continue;


                    //判断是否已使用
                    if (this.GvList.Rows[i].Cells[4].Text!="0")
                    {
                        msgList.Add(this.GvList.Rows[i].Cells[1].Text);
                        continue;
                    }

                    bll.DeleteMatIDCode(new MatIDCode { IDCode = this.GvList.Rows[i].Cells[1].Text });

                }
                string msg = string.Join(",", msgList.ToArray());
                if (msg != "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myjs", "MSI('提示','" + msg + "条码已使用，无法删除');", true);
                }

                this.BindData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 生成

        protected void btAdd_Click(object sender, EventArgs e)
        {
            WHMatBLL bll = BLLFactory.CreateBLL<WHMatBLL>();
            bll.BuildMatIDCode(new WHMat {ID=this.hiMatID.Value,MatCode=this.MatCode.Text,SpecCode=this.MatSpec.SelectedValue }, int.Parse(this.Count.Text));

            this.BindData();
        }

        #endregion

        #region 返回

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageWHMat.aspx");
        }

        #endregion

        #region 查询

        protected void btQuery_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        #endregion
    }
}