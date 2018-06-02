using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Reflection;
using System.Web.Configuration;
//using LBHFrameWork.WebControls.OptGroupDropDownList;

namespace LAF.WebUI.Util
{
    /// <summary>
    /// UI工具
    /// </summary>
    public class Tools
    {
        #region 属性

        private static string _RecordDDLFirstItem = "请选择";//登记页面下拉列表首项
        /// <summary>
        /// 登记页面下拉列表首项
        /// </summary>
        public static string RecordDDLFirstItem
        {
            set
            {
                if (_RecordDDLFirstItem != value)
                    _RecordDDLFirstItem = value;
            }
            get
            {
                return _RecordDDLFirstItem;
            }
        }

        private static string _QueryDDLFirstItem = "";//查询页面下拉列表首选项
        /// <summary>
        /// 查询页面下拉列表首选项
        /// </summary>
        public static string QueryDDLFirstItem
        {
            set
            {
                if (_QueryDDLFirstItem != value)
                    _QueryDDLFirstItem = value;
            }
            get
            {
                return _QueryDDLFirstItem;
            }
        }

        #endregion

        #region 设置控件输入方式

        /// <summary>
        /// 设置数值控件
        /// </summary>
        /// <param name="webCtrl">文本控件</param>
        /// <param name="intBit">整数位</param>
        /// <param name="decBit">小数位</param>
        /// <param name="ifRight">是否居右</param>
        public static void SetNumberControl(System.Web.UI.WebControls.TextBox webCtrl, int intBit, int decBit)
        {
            SetNumberControl(webCtrl, intBit, decBit, true);
        }

        /// <summary>
        /// 设置数值控件
        /// </summary>
        /// <param name="webCtrl">文本控件</param>
        /// <param name="intBit">整数位</param>
        /// <param name="decBit">小数位</param>
        /// <param name="ifRight">是否居右</param>
        public static void SetNumberControl(System.Web.UI.WebControls.TextBox webCtrl, int intBit, int decBit, bool ifRight)
        {
            if (true == ifRight)
            {
                webCtrl.Style.Add("TEXT-ALIGN", "right");
            }

            webCtrl.Attributes.Add("onkeypress", "javascript:return ComVerifyControl_onkeypress(this,1," + intBit.ToString() + "," + decBit.ToString() + ");");
            webCtrl.Attributes.Add("onpropertychange", "javascript:return ComVerifyControl_onpropertychange(this,1," + intBit.ToString() + "," + decBit.ToString() + ");");
            //webCtrl.Attributes.Add("onkeydown", "javascript:javascript:return ComVerifyControl_onpropertychange(this," + inputType.ToString() + "," + intBit.ToString() + "," + decBit.ToString() + ");");
        }

        /// <summary>
        /// 设置日期控件
        /// </summary>
        /// <param name="webCtrl">文本控件</param>
        public static void SetDateTimeControlY(System.Web.UI.WebControls.TextBox webCtrl)
        {
            SetDateTimeControl(webCtrl, "yyyy", null);
        }

        /// <summary>
        /// 设置日期控件
        /// </summary>
        /// <param name="webCtrl">文本控件</param>
        public static void SetDateTimeControlYM(System.Web.UI.WebControls.TextBox webCtrl)
        {
            SetDateTimeControl(webCtrl, "yyyy-MM", null);
        }

        /// <summary>
        /// 设置日期控件
        /// </summary>
        /// <param name="webCtrl">文本控件</param>
        public static void SetDateTimeControl(System.Web.UI.WebControls.TextBox webCtrl)
        {
            SetDateTimeControl(webCtrl, "yyyy-MM-dd", null);
        }

        /// <summary>
        /// 设置日期控件
        /// </summary>
        /// <param name="webCtrl">文本控件</param>
        public static void SetDateTimeControlFull(System.Web.UI.WebControls.TextBox webCtrl)
        {
            SetDateTimeControl(webCtrl, "yyyy-MM-dd HH:mm:ss", null);
        }

        /// <summary>
        /// 设置日期控件
        /// </summary>
        /// <param name="webCtrl">文本控件</param>
        /// <param name="dateFormate">日期时间格式</param>
        /// <param name="propertyArray">自定义属性</param>
        public static void SetDateTimeControl(System.Web.UI.WebControls.TextBox webCtrl, string dateFormate, Dictionary<string, string> propertyArray)
        {
            string onFocus = "isShowClear:false";

            //添加样式
            webCtrl.Attributes.Add("class", "Wdate");

            //添加日期格式
            if (string.IsNullOrEmpty(dateFormate) == false)
                onFocus += ",dateFmt:'" + dateFormate + "'";

            if (propertyArray != null)
            {
                //添加自定义属性
                foreach (string key in propertyArray.Keys)
                {
                    onFocus += "," + key + ":" + propertyArray[key];
                }
            }

            //添加控件属性
            onFocus = "WdatePicker({" + onFocus + ",readOnly:true})";
            webCtrl.Attributes.Add("onFocus", onFocus);
        }

        #endregion

        #region 截取长度字符串

        /// <summary>
        /// 截取长度字符串
        /// </summary>
        /// <param name="strVal">文本</param>
        /// <param name="iLength">长度</param>
        /// <returns>文本</returns>
        /// <remarks>全角字符占2位</remarks>
        public static string LengthChar(string strVal, int iLength)
        {
            int iCnt = 0;
            int i_index;
            int i_len;
            System.Text.StringBuilder strRet = new System.Text.StringBuilder();
            i_len = strVal.Length;
            byte[] chrbyte;
            System.Text.Encoding encoding = System.Text.Encoding.Unicode;
            chrbyte = encoding.GetBytes(strVal);
            for (i_index = 1; i_index < (chrbyte.Length); i_index = i_index + 2)
            {
                iCnt++;
                if (chrbyte[i_index] != 0)
                {
                    iCnt++;
                }
                if (iCnt <= iLength)
                {
                    byte[] va = new byte[2];
                    va[0] = chrbyte[i_index - 1];
                    va[1] = chrbyte[i_index];
                    strRet.Append(encoding.GetString(va));
                }
                else
                {
                    if (i_index < chrbyte.Length + 1)
                    {
                        strRet.Append("...");
                    }
                    break;
                }
            }
            return strRet.ToString();
        }

        #endregion

        #region 绑定下拉列表数据

        /// <summary>
        /// 绑定下拉列表数据
        /// </summary>
        /// <param name="ddl">下拉列表控件</param>
        /// <param name="dictArray">字典数据</param>
        /// <param name="firstItem">首项</param>
        public static void BindDataToDDL(DropDownList ddl, List<DictInfo> dictArray, string firstItem)
        {
            if (firstItem != null)
                ddl.Items.Add(new ListItem(firstItem, ""));

            foreach (DictInfo dict in dictArray)
            {
                ddl.Items.Add(new ListItem(dict.Des, dict.ID));
            }
        }

        #endregion

    }
    

}
