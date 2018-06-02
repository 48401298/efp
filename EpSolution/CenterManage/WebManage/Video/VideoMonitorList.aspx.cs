using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAF.Data;
using LAF.WebUI;
using LAF.WebUI.Util;
using LAF.WebUI.DataSource;
using Manage.BLL.Video;
using Manage.Entity.Video;

namespace Manage.Web.WH.Video
{
    /// <summary>
    /// 视频实时监控列表
    /// </summary>
    public partial class VideoMonitorList : ParentPage
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
            VDPositionBLL bll = null;
            VDCameraBLL cbll = null;
            VDPosition condition = new VDPosition();
            List<VDPosition> list = null;
            List<VDCamera> cameraList = null;
            try
            {
                //获取位置列表
                bll = BLLFactory.CreateBLL<VDPositionBLL>();
                list = bll.GetList();

                //获取摄像头列表
                cbll = BLLFactory.CreateBLL<VDCameraBLL>();
                cameraList = cbll.GetList();

                //绑定摄像头
                foreach (VDPosition postion in list)
                {
                    postion.CameraList = cameraList.Where(p => p.PostionID == postion.ID).ToList<VDCamera>();
                }

                //输出位置信息

                foreach (VDPosition position in list)
                {
                    this.PositionList.Text += "<div class=\"easyui-panel\" title=\""+position.PositionName+"\" style=\"width:500px;padding:10px;\">";

                    foreach (VDCamera camera in position.CameraList)
                    {
                        this.PositionList.Text
                            += string.Format("<a href=\"#\" onclick=\"openRTMonitor('{0}','{1}')\">{1}<a>&nbsp;&nbsp;"
                            , camera.ID, camera.CameraName);
                    }
                    
                    this.PositionList.Text+="</div>";
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