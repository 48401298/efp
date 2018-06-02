using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAF.Data;

namespace LAF.WebUI.Util
{
    /// <summary>
    /// 数据分页
    /// </summary>
    public class PagerHelper
    {
        public PagerHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region 初始化设置分页控件和分页信息类


        /// <summary>
        /// 初始化设置分页控件和分页信息类

        /// </summary>
        /// <param name="aspNetPager">分页控件</param>
        /// <param name="pager">分页信息类</param>
        /// <param name="pagerSize">页数</param>
        public static void InitPageControl(Wuqi.Webdiyer.AspNetPager aspNetPager, DataPage page, bool isUseDefault)
        {
            //对控件的基本设置	
            if (isUseDefault)
            {
                aspNetPager.AlwaysShow = false;
                aspNetPager.FirstPageText = " 首 页 ";
                aspNetPager.NextPageText = " 下一页 ";
                aspNetPager.LastPageText = "末 页 ";
                aspNetPager.PrevPageText = "上一页 ";
                aspNetPager.CustomInfoClass = "PageCustomInfo";
                //aspNetPager.PageIndexBoxClass = "PageInputBox";
                aspNetPager.CustomInfoTextAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                aspNetPager.CustomInfoSectionWidth = new System.Web.UI.WebControls.Unit("40%");
                aspNetPager.SubmitButtonClass = "PageInputButton";
                aspNetPager.SubmitButtonText = "";
                //aspNetPager.ShowPageIndexBox = Wuqi.Webdiyer.ShowPageIndexBox.Never;
            }
            if (page.PageSize == 0)
                page.PageSize = 10;
            if (page.PageIndex != aspNetPager.CurrentPageIndex)
                page.PageIndex = aspNetPager.CurrentPageIndex;
            if (aspNetPager.PageSize != page.PageSize)
                aspNetPager.PageSize = page.PageSize;
        }

        /// <summary>
        /// 初始化设置分页控件和分页信息类

        /// </summary>
        /// <param name="aspNetPager">分页控件</param>
        /// <param name="pager">分页信息类</param>
        /// <param name="pagerSize">页数</param>
        public static void InitPageControl(Wuqi.Webdiyer.AspNetPager aspNetPager, DataPage page, int pagerSize, bool isUseDefault)
        {
            //对控件的基本设置	
            if (isUseDefault)
            {
                aspNetPager.AlwaysShow = false;
                aspNetPager.FirstPageText = " 首 页 ";
                aspNetPager.NextPageText = " 下一页 ";
                aspNetPager.LastPageText = "末 页 ";
                aspNetPager.PrevPageText = "上一页 ";
                aspNetPager.CustomInfoClass = "PageCustomInfo";
                //aspNetPager.PageIndexBoxClass = "PageInputBox";
                aspNetPager.CustomInfoTextAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                aspNetPager.CustomInfoSectionWidth = new System.Web.UI.WebControls.Unit("40%");
                aspNetPager.SubmitButtonClass = "PageInputButton";
                aspNetPager.SubmitButtonText = "";
                //aspNetPager.ShowPageIndexBox = Wuqi.Webdiyer.ShowPageIndexBox.Never;
            }
            if (page.PageSize != pagerSize)
                page.PageSize = pagerSize;
            if (page.PageIndex != aspNetPager.CurrentPageIndex)
                page.PageIndex = aspNetPager.CurrentPageIndex;
            if (aspNetPager.PageSize != page.PageSize)
                aspNetPager.PageSize = page.PageSize;
        }

        #endregion

        #region 依据取回的记录设置分页控件


        /// <summary>
        /// 依据取回的记录设置分页控件

        /// </summary>
        /// <param name="aspNetPager">分页控件</param>
        /// <param name="pager">分页信息类</param>
        public static void SetPageControl(Wuqi.Webdiyer.AspNetPager aspNetPager, int recordCount, int pageIndex)
        {
            if (aspNetPager.RecordCount != recordCount)
                aspNetPager.RecordCount = recordCount;           
            if (aspNetPager.CurrentPageIndex != pageIndex)
                aspNetPager.CurrentPageIndex = pageIndex;
            //aspNetPager.CustomInfoText = "";        
        }

        /// <summary>
        /// 依据取回的记录设置分页控件

        /// </summary>
        /// <param name="aspNetPager">分页控件</param>
        /// <param name="pager">分页信息类</param>
        public static void SetPageControl(Wuqi.Webdiyer.AspNetPager aspNetPager, DataPage page, bool isText)
        {
            if (aspNetPager.RecordCount != page.RecordCount)
                aspNetPager.RecordCount = page.RecordCount;            
            if (aspNetPager.CurrentPageIndex != page.PageIndex)
                aspNetPager.CurrentPageIndex = page.PageIndex;
            if (isText == true)
            {
                aspNetPager.CustomInfoText = "记录总数：<b>" + aspNetPager.RecordCount.ToString() + "</b>";
                aspNetPager.CustomInfoText += " 总页数：<b>" + aspNetPager.PageCount.ToString() + "</b>";
                aspNetPager.CustomInfoText += " 当前页：<font color=\"red\"><b>" + page.PageIndex.ToString() + "</b></font>";
                aspNetPager.ShowCustomInfoSection = Wuqi.Webdiyer.ShowCustomInfoSection.Left;
            }
        }

        #endregion
    }

}

