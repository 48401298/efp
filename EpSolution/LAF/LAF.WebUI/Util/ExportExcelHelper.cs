
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace LAF.WebUI.Util
{
    /// <summary>
    /// 导出Excel工具
    /// </summary>
    public class ExportExcelHelper
    {
        public ExportExcelHelper()
        {

        }


        //#region 用WEB服务器控件的RenderControl方法导出自身内容

        ///// <summary>
        ///// 把WEB控件的内容输出到文件中

        ///// <para>目前只针对DataGrid做了测试,如果DataGrid中有校验控件,则不能正常使用</para>
        ///// </summary>
        ///// <param name="Response">页面输出流访问对象</param>
        ///// <param name="CTRL">web控件</param>
        ///// <param name="FileName">文件名</param>
        ///// <param name="exportType">文件类型</param>
        //public static void ExportByRender(HttpResponse Response, Control CTRL, string FileName, ExportType exportType)
        //{

        //    if (CTRL == null || Response == null || FileName == "") return;
        //    if (exportType != ExportType.Excel
        //        && exportType != ExportType.Word
        //        && exportType != ExportType.Html)
        //        throw (new Exception("该方法导出的类型目前只能为Excel或Word格式或HTML格式"));

        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ContentType = GetContentType(exportType);

        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8) + GetExtension(exportType) + "");
        //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        //    Response.Charset = "UTF-8";
        //    System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //    ClearControls(CTRL);
        //    CTRL.RenderControl(oHtmlTextWriter);
        //    Response.Write(oStringWriter.ToString());
        //    Response.End();
        //}

        //private static string GetExtension(ExportType exportType)
        //{
        //    string Extension = String.Empty;
        //    switch (exportType)
        //    {
        //        case ExportType.Excel:
        //            Extension = ".xls";
        //            break;
        //        case ExportType.Word:
        //            Extension = ".doc";
        //            break;
        //        case ExportType.TXT:
        //            Extension = ".txt";
        //            break;
        //        case ExportType.XML:
        //            Extension = ".xml";
        //            break;
        //        case ExportType.Html:
        //            Extension = ".Html";
        //            break;

        //    }
        //    return Extension;
        //}
        //private static string GetContentType(ExportType exportType)
        //{
        //    string contentType = String.Empty;
        //    switch (exportType)
        //    {
        //        case ExportType.Excel:
        //            contentType = "application/vnd.ms-excel";
        //            break;
        //        case ExportType.Word:
        //            contentType = "application/vnd.ms-word";
        //            break;
        //        case ExportType.TXT:
        //            contentType = "text/html";
        //            break;
        //        case ExportType.XML:
        //            contentType = "text/xml";
        //            break;
        //        case ExportType.Html:
        //            contentType = "text/html";
        //            break;
        //    }
        //    return contentType;
        //}


        ///// <summary>
        ///// 清除可能产生回发的子控件变成文本控件,如果不这样做的话,调用RenderControl会产生错误

        ///// Reference:http://www.c-sharpcorner.com/Code/2003/Sept/ExportASPNetDataGridToExcel.asp
        ///// </summary>
        ///// <param name="control"></param>
        //private static void ClearControls(Control control)
        //{
        //    for (int i = control.Controls.Count - 1; i >= 0; i--)
        //    {
        //        ClearControls(control.Controls[i]);
        //    }
        //    if (control is TableCell)
        //    {
        //        for (int j = 0; j < control.Controls.Count; j++)
        //        {
        //            if (!(control.Controls[j] is Label || control.Controls[j] is LiteralControl))
        //            {
        //                Control c = control.Controls[j];
        //                if (c.GetType().GetProperty("Text") != null)
        //                {
        //                    LiteralControl literal = new LiteralControl();
        //                    literal.Text = c.GetType().GetProperty("Text").GetValue(c, null).ToString();
        //                    control.Controls.Add(literal);
        //                }
        //                control.Controls.Remove(c);
        //            }
        //        }
        //    }
        //    return;
        //}

        //#endregion
    }
}
