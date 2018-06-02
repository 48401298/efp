using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.WebUI.DataSource;
using LAF.WebUI.Util;
using Manage.Entity.MES;
using LAF.WebUI;
using Manage.BLL.MES;
using LAF.Entity;

namespace Manage.Web.MES.Tracking
{
    public partial class MaterialTrace : ParentPage
    {
        #region 页面载入

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                this.InitForm();
                //获取产品信息
                if (string.IsNullOrEmpty(this.BatchNumber.SelectedValue) == false)
                {
                    ProducePlanBLL ppbll = BLLFactory.CreateBLL<ProducePlanBLL>();
                    ProductInfo result = ppbll.GetPNameByIDBatchNumber(this.BatchNumber.SelectedValue);
                    this.PRODUCTIONID.Value = result.PID;
                    this.PNAME.Text = result.PNAME;
                }
            }            

            SetBtnState();
        }

        #endregion

        #region 初始化页面

        private void InitForm()
        {
            //批次號 
            List<DictInfo> dicts = null;

            //绑定绑定批次
            List<ProducePlan> whList = BLLFactory.CreateBLL<ProducePlanBLL>().GetList();
            dicts = whList.Select(p => new DictInfo { ID = p.BATCHNUMBER, Des = p.BATCHNUMBER }).ToList<DictInfo>();
            Tools.BindDataToDDL(this.BatchNumber, dicts, null);
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                BindData(id);
            }

        }
        #endregion

        private void SetBtnState()
        {
            if (this.StartTime.Text == "")
            {
                this.btStart.Enabled = true;
                this.btEnd.Enabled = false;
                this.btReset.Enabled = false;
            }
            else
            {
                this.btStart.Enabled = false;
                this.btEnd.Enabled = true;
                this.btReset.Enabled = true;
            }
        }
        #region 绑定数据

        private void BindData(string id)
        {
            ProduceTrackBLL bll = null;
            ProduceTrack info = new ProduceTrack();
            try
            {
                bll = BLLFactory.CreateBLL<ProduceTrackBLL>();
                if (string.IsNullOrEmpty(id) == false)
                {
                    info.PID = id;
                    info = bll.Get(info);
                    UIBindHelper.BindForm(this.Page, info);
                    this.PNAME.Text = info.PNAME;
                    this.CBNAME.Text = info.CBNAME;
                    this.GXNAME.Text = info.GXNAME;
                    this.hiID.Value = info.PID;
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                    this.hiCB.Value = info.EQUID;
                    this.hiGX.Value = info.WPID;
                    this.hiPid.Value =  info.PRODUCTIONID;
                    this.WORKINGSTARTTIME.Value = info.WORKINGSTARTTIME.ToString();
                    if (info.WORKINGSTARTTIME != DateTime.MinValue)
                    {
                        this.StartTime.Text = info.WORKINGSTARTTIME.Hour + ":" + info.WORKINGSTARTTIME.Minute + ":" + info.WORKINGSTARTTIME.Second;
                        this.CurrentTime.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                        TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                        TimeSpan ts2 = new TimeSpan(info.WORKINGSTARTTIME.Ticks);
                        TimeSpan ts = ts1.Subtract(ts2).Duration();
                        this.SpendTime.Text = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
                    }
                    else
                    {
                        this.StartTime.Text = "";
                        this.CurrentTime.Text = "";
                        this.SpendTime.Text = "";
                    }
                }
                else
                {
                    info = new ProduceTrack();
                }
                SetBtnState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 保存

        protected void btStart_Click(object sender, EventArgs e)
        {
            LoginInfo user = (Session["UserInfo"] as LoginInfo) as LoginInfo;
            ProduceTrack info = new ProduceTrack();
            ProduceTrackBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<ProduceTrackBLL>();
                info.PRODUCTIONID=this.PRODUCTIONID.Value;
                if (this.hiID.Value == "")
                {
                    info.PID = Guid.NewGuid().ToString();
                    info.STATUS = "0";
                    info.WPID = this.hiGX.Value;
                    info.EQUID = this.hiCB.Value;
                    info.WSID = this.hiGW.Value;
                    info.FACTORYPID = user.OrgaID;
                    info.WORKINGSTARTTIME = DateTime.Now;
                    //校验是否跳序
                    string result = new ProcessCheckBLL().CheckSkipProcess(info.BATCHNUMBER, info.PRODUCTIONID, info.WPID);
                    if (result != "")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('"+result+"');", true);
                        return;
                    }

                    bll.Insert(info);
                    this.HiCREATEUSER.Value = info.CREATEUSER;
                    this.HiCREATETIME.Value = info.CREATETIME.ToString();
                }
                else
                {
                    info.PID = this.hiID.Value;
                    info.WORKINGSTARTTIME = DateTime.Now;
                    info.STATUS = "0";
                    info.WPID = this.hiGX.Value;
                    info.EQUID = this.hiCB.Value;
                    info.WSID = this.hiGW.Value;
                    info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                    info.CREATEUSER = this.HiCREATEUSER.Value;
                    bll.Update(info);
                }
                BindData(info.PID);
                //获取产品信息
                if (string.IsNullOrEmpty(this.BatchNumber.SelectedValue) == false)
                {
                    ProducePlanBLL ppbll = BLLFactory.CreateBLL<ProducePlanBLL>();
                    ProductInfo result = ppbll.GetPNameByIDBatchNumber(this.BatchNumber.SelectedValue);
                    this.PRODUCTIONID.Value = result.PID;
                    this.PNAME.Text = result.PNAME;
                }
                this.hiID.Value = info.PID;
                //ClientScript.RegisterStartupScript(this.GetType(), "myjs", "window.location.href='MaterialTrace.aspx';", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btEnd_Click(object sender, EventArgs e)
        {
            ProduceTrack info = new ProduceTrack();
            ProduceTrackBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<ProduceTrackBLL>();
                info.PID = this.hiID.Value;
                info.WORKINGENDTIME = DateTime.Now;
                info.STATUS = "1";
                info.WPID = this.hiGX.Value;
                info.EQUID = this.hiCB.Value;
                info.WSID = this.hiGW.Value;
                info.PRODUCTIONID = this.hiPid.Value;
                info.CREATETIME = DateTime.Parse(this.HiCREATETIME.Value);
                info.CREATEUSER = this.HiCREATEUSER.Value;
                bll.Update(info);


                ClientScript.RegisterStartupScript(this.GetType(), "myjs", "alert('完成');window.location.href='MaterialTrace.aspx';", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btReset_Click(object sender, EventArgs e)
        {
            ProduceTrack info = new ProduceTrack();
            ProduceTrackBLL bll = null;
            try
            {
                UIBindHelper.BindModelByControls(this.Page, info);

                bll = BLLFactory.CreateBLL<ProduceTrackBLL>();

                info.PID = this.hiID.Value;
                info.STATUS = "0";
                info.WPID = this.hiGX.Value;
                info.EQUID = this.hiCB.Value;
                info.WSID = this.hiGW.Value;
                info.PRODUCTIONID = this.hiPid.Value;
                info.WORKINGSTARTTIME = DateTime.MinValue;
                bll.Update(info);
                BindData(info.PID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 返回

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../HDDefault.aspx");
        }

        #endregion
    }
}