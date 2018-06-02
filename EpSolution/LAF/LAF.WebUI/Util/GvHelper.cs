using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace LAF.WebUI.Util
{
    public class GvHelper
    {

        public static string OperButtonsText = "操作";

        public GvHelper()
        {
        }

        #region 初始设置表格控件

        public static void DatagridSkin(GridView gv)
        {
            DatagridSkin(gv, null);
        }

        public static void DatagridSkin(GridView gv, string sTitle)
        {
            if (gv.Rows.Count > 0)
            {
                string sDataOptions = "";
                if (gv.AllowPaging)
                {
                    int iPageNumber = 0;
                    if (int.TryParse(gv.Page.Request.QueryString[gv.ID + "_pn"], out iPageNumber))
                        gv.PageIndex = iPageNumber - 1;
                    int iPageSize = 10;
                    if (int.TryParse(gv.Page.Request.QueryString[gv.ID + "_ps"], out iPageSize))
                        gv.PageSize = iPageSize;
                    sDataOptions += ",pagination:true";
                }


                gv.CssClass = "easyui-datagrid";
                gv.Attributes.Add("data-options", sDataOptions);
                gv.Attributes.Add("dg-name", gv.ID);
                if (sTitle != null)
                    gv.Attributes.Add("title", sTitle);
                gv.BorderWidth = 1;

                int i = 0;
                foreach (TableCell cell in gv.HeaderRow.Cells)
                {
                    cell.Attributes.Add("data-options", "field:'item" + i.ToString() + "'");
                    i++;
                }
                //使用<TH>替换<TD> 
                gv.UseAccessibleHeader = true;
                //HeaderRow将被<thead>包裹，数据行将被<tbody>包裹
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                //FooterRow将被<tfoot>包裹
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        public static void DatagridSkinUpdate(GridViewUpdateEventArgs e)
        {
            // 清除莫名其妙多出的来的OldValue
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                string oldValue = (string)e.OldValues[i];
                string newValue = (string)e.NewValues[i];
                if (oldValue == null || oldValue == "")
                    e.NewValues[i] = newValue.Substring(0, newValue.Length - 1);
                else if (oldValue.Length < newValue.Length)
                {
                    int iLength = newValue.Length - oldValue.Length;
                    if (newValue.LastIndexOf(oldValue) == iLength)
                        e.NewValues[i] = newValue.Substring(0, iLength - 1);
                }
            }
        }

        #endregion

        //#region 填充表格序号
        ///// <summary>
        ///// 填充表格序号
        ///// </summary>
        ///// <param name="dataGrid">表格</param>
        ///// <param name="columnIndex">序号列</param>
        ///// <param name="pageSize">每页记录数</param>
        ///// <param name="pageIndex">当前页索引</param>
        //public static void FillSeq(LBHGridView dataGrid, int columnIndex, int pageSize, int pageIndex)
        //{
        //    if (dataGrid.Rows.Count > 0)
        //    {
        //        //序号居中
        //        dataGrid.Columns[columnIndex].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        //    }
        //    for (int i = 0; i < dataGrid.Rows.Count; i++)
        //    {
        //        dataGrid.Rows[i].Cells[columnIndex].Text = Convert.ToString(((pageIndex - 1) * pageSize) + i + 1);
        //    }
        //}
        //#endregion

        //#region 初始排序

        //#endregion

        //#region 取消排序

        

        //#endregion

        //#region 设置排序

        
        //#endregion

        //#region 填充表格序号        

        ///// <summary>
        ///// 填充表格序号
        ///// </summary>
        ///// <param name="dataGrid">表格</param>
        ///// <param name="columnIndex">序号列</param>
        ///// <param name="pageSize">每页记录数</param>
        ///// <param name="pageIndex">当前页索引</param>
        //public static void FillSeq(GridView dataGrid, int columnIndex, int pageSize, int pageIndex)
        //{
        //    if (dataGrid.Rows.Count > 0)
        //    {
        //        //序号居中
        //        dataGrid.Columns[columnIndex].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        //    }
        //    for (int i = 0; i < dataGrid.Rows.Count; i++)
        //    {
        //        dataGrid.Rows[i].Cells[columnIndex].Text = Convert.ToString(((pageIndex - 1) * pageSize) + i + 1);
        //    }
        //}

        //#endregion

        //#region 获取选中行的指定键值



        /// <summary>
        /// 获取选中行的指定键值
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="columnIndex"></param>
        /// <param name="checkBox"></param>
        /// <param name="keyIndex"></param>
        /// <returns></returns>
        public static ArrayList GetPKValueByChk(GridView dataGrid, int columnIndex, string checkBox, int keyIndex)
        {
            int count = 0;
            count = dataGrid.Rows.Count;
            ArrayList pkList = new ArrayList();
            for (int i = 0; i < count; i++)
            {
                CheckBox item;
                item = (CheckBox)dataGrid.Rows[i].Cells[columnIndex].FindControl(checkBox);

                if (item != null && item.Checked == true)
                {
                    pkList.Add(dataGrid.DataKeys[i][keyIndex]);

                }
            }
            return pkList;
        }

        //#endregion

        //#region 列头合并
        ///// <summary>
        ///// 列头合并
        ///// </summary>
        ///// <param name="gv">GridView</param>
        ///// <param name="startIndex">开始列</param>
        ///// <param name="endIndex">结束列</param>
        ///// <param name="text">列头名称</param>
        //public static void UniteTableHead(GridView gv, int startIndex, int endIndex,string text)
        //{
        //    if (gv.HeaderRow == null)
        //        return;

        //    endIndex++;
        //    TableCell oldTc = gv.HeaderRow.Cells[startIndex];

        //    gv.HeaderRow.Cells[startIndex].Text = text;

        //    for (int i = 1; i < endIndex - startIndex; i++)
        //    {
        //        TableCell tc = gv.HeaderRow.Cells[i + startIndex];

        //        tc.Visible = false;

        //        if (oldTc.ColumnSpan == 0)
        //        {
        //            oldTc.ColumnSpan = 1;
        //        }

        //        oldTc.ColumnSpan++;

        //        oldTc.VerticalAlign = VerticalAlign.Middle;

        //    }
        //}

        //#endregion

    }
}
