using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.XSSF.UserModel;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.OpenXmlFormats.Spreadsheet;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// </summary>
    public partial class ExcelOperationHelper
    {
        /// <summary>
        /// Excel保护密码
        /// </summary>
        private const string EXCELPWD = "111111";

        /// <summary>
        /// 验证委托
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>错误信息</returns>
        public delegate Tuple<List<string[]>> DelegateCheck(DataTable dt);

        /// <summary>
        /// 编辑Excel
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
        public delegate bool DelegateEditExcel(IWorkbook wb);

        #region 定义变量
        public enum stylexls
        {
            Title,
            url,
            DateTime,
            Integer,
            Currency,
            Percent,
            CEN,
            Scientific,
            Default
        }
        #endregion

        #region ------->EXCEL操作方法<-------
        /// <summary>
        /// 定义单元格常用到样式
        /// 创建者：戚鹏
        /// 创建日期：2013.5.20
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private static ICellStyle GetCellStyle(IWorkbook wb, stylexls str)
        {
            ICellStyle cellStyle = wb.CreateCellStyle();

            //定义几种字体  
            //也可以一种字体，写一些公共属性，然后在下面需要时加特殊的  
            IFont font12 = wb.CreateFont();
            font12.FontHeightInPoints = 12;
            font12.FontName = "Arial";
            font12.Boldweight = 600;

            IFont font = wb.CreateFont();
            font.FontName = "微软雅黑";
            font.Color = HSSFColor.OliveGreen.Red.Index;
            //font.Underline = 1;下划线  

            IFont fontcolorblue = wb.CreateFont();
            fontcolorblue.Color = HSSFColor.OliveGreen.Blue.Index;
            fontcolorblue.IsItalic = true;//下划线  
            fontcolorblue.FontName = "微软雅黑";

            //边框  
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Dotted;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Dotted;

            //边框颜色  
            cellStyle.BottomBorderColor = HSSFColor.OliveGreen.Black.Index;
            cellStyle.TopBorderColor = HSSFColor.OliveGreen.Black.Index;

            //背景图形
            //cellStyle.FillBackgroundColor = HSSFColor.OliveGreen.BLUE.index;  
            //cellStyle.FillForegroundColor = HSSFColor.OliveGreen.BLUE.index;  
            cellStyle.FillForegroundColor = HSSFColor.White.Index;
            // cellStyle.FillPattern = FillPatternType.NO_FILL;  
            //cellStyle.FillBackgroundColor = HSSFColor.OliveGreen.BRIGHT_GREEN.index;

            //水平对齐  
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;

            //垂直对齐  
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            cellStyle.FillBackgroundColor = HSSFColor.OliveGreen.Blue.Index;

            //自动换行  
            cellStyle.WrapText = true;

            //缩进;当设置为1时，前面留的空白太大了。希旺官网改进。或者是我设置的不对  
            cellStyle.Indention = 0;

            //上面基本都是设共公的设置  
            //下面列出了常用的字段类型  
            switch (str)
            {
                case stylexls.Title:
                    cellStyle.SetFont(font12);
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
                    cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                    cellStyle.FillPattern = FillPattern.FineDots;
                    cellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightCornflowerBlue.Index;
                    cellStyle.IsLocked = false;
                    break;
                case stylexls.DateTime:
                    IDataFormat datastyle = wb.CreateDataFormat();
                    cellStyle.DataFormat = datastyle.GetFormat("yyyy/mm/dd");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.Integer:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.Currency:
                    IDataFormat format = wb.CreateDataFormat();
                    cellStyle.DataFormat = format.GetFormat("￥#,##0");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.url:
                    fontcolorblue.Underline = FontUnderlineType.Single;
                    cellStyle.SetFont(fontcolorblue);
                    break;
                case stylexls.Percent:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.CEN:
                    IDataFormat format1 = wb.CreateDataFormat();
                    cellStyle.DataFormat = format1.GetFormat("[DbNum2][$-804]0");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.Scientific:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.Default:
                    cellStyle.SetFont(font);
                    break;
            }
            return cellStyle;
        }

        /// <summary>
        /// 设置单元格边框
        /// 创建者：戚鹏
        /// 创建日期：2013.5.20
        /// </summary>
        /// <param name="wb">工作簿</param>
        /// <param name="isLock">是否锁定单元格</param>
        /// <returns></returns>
        private static ICellStyle SetCellBorder(IWorkbook wb, bool isLock)
        {
            ICellStyle cellBorder = wb.CreateCellStyle();
            cellBorder.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellBorder.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellBorder.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellBorder.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

            if (isLock)
            {
                cellBorder.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
                cellBorder.FillPattern = FillPattern.FineDots;
                cellBorder.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
            }

            cellBorder.IsLocked = isLock;
            return cellBorder;
        }

        /// <summary>
        /// 设置单元格样式(未通过验证)
        /// 创建者：戚鹏
        /// 创建日期：2013.5.20
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
        private static ICellStyle GetCellBorderException(IWorkbook wb)
        {
            return GetCellBorderException(wb, null);
        }

        /// <summary>
        /// 设置单元格样式(未通过验证)
        /// 创建者：戚鹏
        /// 创建日期：2013.5.20
        /// </summary>
        private static ICellStyle GetCellBorderException(IWorkbook wb, ICell cell)
        {
            ICellStyle cellBorder = wb.CreateCellStyle();
            if (cell != null)
            {
                cellBorder.DataFormat = cell.CellStyle.DataFormat;
            }
            cellBorder.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellBorder.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellBorder.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellBorder.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellBorder.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
            cellBorder.FillPattern = FillPattern.FineDots;
            cellBorder.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            cellBorder.IsLocked = false;
            return cellBorder;
        }

        /// <summary>
        /// 创建批注
        /// 创建者：戚鹏
        /// 创建日期：2013.5.20
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="Comment"></param>
        /// <returns></returns>
        private static IComment GetCellComment(ISheet sheet, string strComment)
        {
            if (sheet != null)
            {
                IDrawing patr = sheet.CreateDrawingPatriarch();
                ICreationHelper helper = sheet.Workbook.GetCreationHelper();
                var anchor = helper.CreateClientAnchor();
                anchor.AnchorType = 2;
                IComment comment = patr.CreateCellComment(anchor);
                comment.String = helper.CreateRichTextString(strComment);
                return comment;
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// 获取单元格范围
        /// </summary>
        /// <param name="RowCount"></param>
        /// <param name="ColNum"></param>
        /// <returns></returns>
        private static CellRangeAddressList getCellRangeAddressList(int RowCount, int ColNum)
        {
            CellRangeAddressList regions = new CellRangeAddressList(2, 65535, ColNum, ColNum);
            return regions;
        }

        #endregion

        #region ------->EXCEL数据导出<-------
        /// <summary>
        /// Excel数据列表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strfileName"></param>
        /// <param name="dCheck"></param>
        public int WriteWorkSheetException(DataSet ds, string strfileName, string sheetName)
        {
            try
            {
                int count = 0;
                using (FileStream fs = new FileStream(strfileName, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = null;
                    sheet = workbook.GetSheet(sheetName);

                    //工作簿解密
                    sheet.ProtectSheet(null);

                    ICellStyle style = GetCellBorderException(workbook);
                    using (FileStream fsm = new FileStream(strfileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                        {
                            int RowCount = sheet.GetRow(0).LastCellNum - 1;
                            for (int i = 0; i < RowCount; i++)
                            {
                                ICell Cell = sheet.GetRow(iRow + 1).GetCell(i);
                                if (Cell == null)
                                {
                                    Cell = sheet.GetRow(iRow + 1).CreateCell(i);
                                }
                                Cell.SetCellValue(ds.Tables[0].Rows[iRow][i].ToString());
                            }
                        }

                        for (int iRow = 0; iRow < ds.Tables[1].Rows.Count; iRow++)
                        {
                            ICell Cell = sheet.GetRow(iRow + 1).GetCell(4);
                            if (Cell == null)
                            {
                                Cell = sheet.GetRow(iRow + 1).CreateCell(4);
                            }
                            Cell.SetCellValue(ds.Tables[1].Rows[iRow][0].ToString());
                        }

                        //工作簿加密
                        sheet.ProtectSheet(EXCELPWD);

                        workbook.SetSheetHidden(1, SheetState.VeryHidden);
                        workbook.Write(fsm);
                        fsm.Close();
                    }
                }

                return count;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Excel错误列表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strfileName"></param>
        /// <param name="dCheck"></param>
        public int WriteWorkSheetException(List<ExcelExceptionModel> ExcelException, string strfileName)
        {
            try
            {
                int count = 0;
                using (FileStream fs = new FileStream(strfileName, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = null;
                    sheet = workbook.GetSheet(workbook.GetSheetName(workbook.ActiveSheetIndex));

                    //工作簿解密
                    sheet.ProtectSheet(null);
                    using (FileStream fsm = new FileStream(strfileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        for (int i = 0; i < ExcelException.Count; i++)
                        {
                            ICell Cell = sheet.GetRow(ExcelException[i].RowNum).GetCell(ExcelException[i].ColNum);
                            Cell.CellStyle = GetCellBorderException(workbook, Cell);
                            Cell.CellComment = null;
                            Cell.CellComment = GetCellComment(sheet, ExcelException[i].Comment);
                        }
                        //工作簿加密
                        sheet.ProtectSheet(EXCELPWD);

                        workbook.Write(fsm);
                        fsm.Close();
                    }
                }

                return count;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Excel错误列表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strfileName"></param>
        /// <param name="dCheck"></param>
        public int WriteWorkArraySheetException(List<ExcelExceptionModel> ExcelException, string strfileName, List<ExcelExceptionModel> ExcelComment)
        {
            try
            {
                int count = 0;
                using (FileStream fs = new FileStream(strfileName, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = null;

                    foreach (var item in ExcelException.Select(p => p.SheetName).Distinct())
                    {
                        sheet = workbook.GetSheet(item);

                        //工作簿解密
                        sheet.ProtectSheet(null);

                        using (FileStream fsm = new FileStream(strfileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            List<ExcelExceptionModel> SheetComment =
                                ExcelComment.Where(p => p.SheetName.Equals(item)).ToList<ExcelExceptionModel>();
                            for (int i = 0; i < SheetComment.Count; i++)
                            {
                                ICell Cell = sheet.GetRow(SheetComment[i].RowNum).GetCell(SheetComment[i].ColNum);
                                if (Cell != null)
                                {
                                    if (Cell.CellComment != null)
                                    {
                                        Cell.CellComment = null;
                                    }
                                }
                            }

                            List<ExcelExceptionModel> SheetException =
                                ExcelException.Where(p => p.SheetName.Equals(item)).ToList<ExcelExceptionModel>();

                            for (int i = 0; i < SheetException.Count; i++)
                            {
                                ICell Cell = sheet.GetRow(SheetException[i].RowNum).GetCell(SheetException[i].ColNum);
                                if (Cell.CellComment == null)
                                {
                                    Cell.CellStyle = GetCellBorderException(workbook);
                                    Cell.CellComment = GetCellComment(sheet, SheetException[i].Comment);
                                }
                            }

                            //工作簿加密
                            sheet.ProtectSheet(EXCELPWD);

                            workbook.Write(fsm);
                            fsm.Close();
                        }
                    }
                }
                return count;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将数据生成为excel文件
        /// 创建者：戚鹏
        /// 创建日期：2013.5.20
        /// <param name="info">配置信息</param>
        /// <param name="tempFileFullPath">文件名</param>
        /// </summary>    
        public bool WriteWorkbookAuto(string tempFileFullPath, SheetInfo info, DataTable dt,
            LAF.Common.ExcelOperation.ExcelOperationHelper.DelegateCheck dCheck, params string[] Edition)
        {
            try
            {
                XSSFWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet(info.SheetName);

                //冻结列
                sheet.CreateFreezePane(0, 2, 0, 2);
                //工作簿加密
                //TODO:暂时不加密 为了标题能够过滤
                if (info.Protect)
                {
                    sheet.ProtectSheet(EXCELPWD);
                }

                #region 主体内容
                #region Title样式
                IRow rows = sheet.CreateRow(1);
                rows.HeightInPoints = 30;
                #endregion

                #region 隐藏行
                IRow rowHidden = sheet.CreateRow(0);
                rowHidden.Height = 1;
                #endregion
                for (int i = 0; i < info.ColInfos.Count; i++)
                {
                    int ColumnWidth = info.ColInfos[i].ColumnWidth == 0 ? 20 : info.ColInfos[i].ColumnWidth;
                    if (info.ColInfos[i].ColumnHidden)
                    {
                        sheet.SetColumnWidth(i, 0);
                    }
                    else
                    {
                        sheet.SetColumnWidth(i, ColumnWidth * 256);
                    }

                    #region Title样式
                    ICell cell1 = rows.CreateCell(i);
                    cell1.CellStyle = GetCellStyle(workbook, stylexls.Title);

                    string titleStr = info.ColInfos[i].ColumnTitle;
                    //标题*号进行样式设定
                    IRichTextString rt = workbook.GetCreationHelper().CreateRichTextString(titleStr);
                    if (titleStr.EndsWith("*"))
                    {
                        IFont fontStar = workbook.CreateFont();
                        fontStar.Boldweight = 600;
                        fontStar.Color = HSSFColor.OliveGreen.Red.Index;
                        rt.ApplyFont(titleStr.Length - 1, titleStr.Length, fontStar);
                    }

                    cell1.SetCellValue(rt);

                    #endregion

                    #region 写入数据KEY
                    ICell cell = rowHidden.CreateCell(i);
                    cell.SetCellValue(info.ColInfos[i].ColumnName);
                    cell.CellStyle.IsLocked = true;
                    #endregion

                    #region 添加验证
                    IDataValidationHelper helper = sheet.GetDataValidationHelper();
                    CellRangeAddressList range = getCellRangeAddressList(info.DataArray.Count, i);
                    if (info.ColInfos[i].ColumnRangeValues != null)
                    {
                        sheet.AddValidationData(ExcelOperationValidationHelper.getValidationExplicitList(helper, range, info.ColInfos[i].ColumnRangeValues));
                    }
                    if (info.ColInfos[i].ColValMaxLength.HasValue)
                    {
                        sheet.AddValidationData(ExcelOperationValidationHelper.getValidationTextLength(helper, range,
                            1, info.ColInfos[i].ColValMaxLength.GetValueOrDefault()));
                    }
                    switch (info.ColInfos[i].ColumnValidation)
                    {
                        case EmuExcelCellType.Integer:
                            sheet.AddValidationData(ExcelOperationValidationHelper.getValidationInt(helper, range));
                            break;
                        case EmuExcelCellType.Decimal:
                            sheet.AddValidationData(ExcelOperationValidationHelper.getValidationInt(helper, range));
                            break;
                        case EmuExcelCellType.YearMonth:
                            sheet.AddValidationData(ExcelOperationValidationHelper.getValidationYearMonth(helper, range));
                            break;
                        default:
                            break;
                    }

                    #endregion

                    #region 筛选
                    CellRangeAddress CellRange = new CellRangeAddressList(1, 1, 0, i).CellRangeAddresses[0];
                    sheet.SetAutoFilter(CellRange);

                    #endregion
                }

                ICellStyle style = SetCellBorder(workbook, false);
                ICellStyle styleLock = SetCellBorder(workbook, true);

                foreach (List<CellInfo> items in info.DataArray)
                {
                    foreach (CellInfo item in items)
                    {
                        IRow row = sheet.GetRow(int.Parse(item.YPosition));
                        if (row == null)
                        {
                            row = sheet.CreateRow(int.Parse(item.YPosition));
                        }
                        ICell cell = row.GetCell(int.Parse(item.XPosition));
                        if (cell == null)
                        {
                            cell = row.CreateCell(int.Parse(item.XPosition));
                        }

                        double cellValue = 0;
                        //如果是数值 把单元格格式设置为数值
                        if (double.TryParse(item.Value, out cellValue))
                        {
                            cell.SetCellType(CellType.Numeric);
                            cell.SetCellValue(cellValue);
                            //XSSFCellStyle cellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                            //IDataFormat dataFormat = workbook.CreateDataFormat();
                            //cellStyle.DataFormat = dataFormat;
                            //cellStyle.

                        }
                        else
                        {
                            cell.SetCellType(CellType.String);
                            cell.SetCellValue(item.Value);
                        }
                        cell.CellStyle = item.ColumnLock ? styleLock : style;


                    }
                }

                #endregion

                #region 数据校验
                bool hasErr = false;
                if (dCheck != null)
                {
                    //复制dt结构（不复制数据）
                    DataTable dtTemp = dt.Clone();
                    DataRow[] rules = dt.Select();

                    foreach (var dr in rules)
                    {
                        //int RowCount = sheet.GetRow(dt.Rows.IndexOf(dr) + 2).LastCellNum;
                        //for (int i = 2; i < RowCount; i++)
                        //{
                        //    ICell Cell = sheet.GetRow(dt.Rows.IndexOf(dr) + 2).Cells[i];
                        //    Cell.CellComment = null;
                        //    Cell.CellStyle = style;
                        //}

                        dtTemp.Rows.Add(dr.ItemArray);
                        Tuple<List<string[]>> tu = dCheck.Invoke(dtTemp);
                        if (tu.Item1.Count > 0)
                        {
                            hasErr = true;
                            for (int i = 0; i < tu.Item1.Count; i++)
                            {
                                int iConNum = int.Parse(tu.Item1[i][0]);
                                string strMsg = tu.Item1[i][1];

                                ICell Cell = sheet.GetRow(dt.Rows.IndexOf(dr) + 2).GetCell(iConNum);
                                Cell.CellStyle = GetCellBorderException(workbook);
                                Cell.CellComment = null;
                                Cell.CellComment = GetCellComment(sheet, strMsg);
                            }
                        }
                        else
                        {
                            //隐藏正确行
                            sheet.GetRow(dt.Rows.IndexOf(dr) + 2).Height = 1;
                        }
                        dtTemp.Rows.Clear();
                    }
                }

                #endregion

                #region 版本号
                ISheet sheetEdition = null;
                sheetEdition = workbook.CreateSheet("Property");
                for (int r = 0; r < Edition.Length; r++)
                {
                    sheetEdition.CreateRow(r).CreateCell(0).SetCellValue(Edition[r]);
                }
                workbook.SetSheetHidden(1, SheetState.VeryHidden);
                #endregion

                //写入文件
                using (FileStream fs = new FileStream(tempFileFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    workbook.Write(fs);
                    fs.Close();
                }

                return hasErr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 写入Excel模板
        public bool WriteWorkbookTemplate(string tempFileFullPath, LAF.Common.ExcelOperation.ExcelOperationHelper.DelegateEditExcel edit)
        {
            try
            {
                IWorkbook wb;
                using (FileStream fs = new FileStream(tempFileFullPath, FileMode.Open, FileAccess.Read))
                {
                    wb = WorkbookFactory.Create(fs);

                }
                edit(wb);
                using (FileStream fsm = new FileStream(tempFileFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    wb.Write(fsm);
                    fsm.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region ------->Excel读取数据<-------
        /// <summary>
        /// 从excel读取数据
        /// </summary>
        /// <param name="info">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// <returns>数据ArrarList(List<DataInfoItem>)</returns>
        public Tuple<ArrayList, List<string>> ReadWorkbook(string fileName)
        {
            ArrayList array = new ArrayList();
            ISheet sheet = null;
            ISheet sheetEdition = null;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = WorkbookFactory.Create(fs);//使用接口，自动识别excel2003/2007格式
                    sheet = workbook.GetSheet(workbook.GetSheetName(workbook.ActiveSheetIndex));
                    sheetEdition = workbook.GetSheet("Property");
                    if (sheetEdition == null)
                    {
                        return null;
                    }
                    if (sheet.LastRowNum > 1)
                    {
                        int startRow = 2;
                        int lastRow = sheet.LastRowNum;

                        for (int i = startRow; i <= lastRow; i++)
                        {
                            //行循环
                            IRow row = sheet.GetRow(i);
                            //列名
                            IRow TitleRow = sheet.GetRow(0);
                            //列循环
                            List<CellInfo> list = new List<CellInfo>();
                            for (int t = 0; t < TitleRow.LastCellNum; t++)
                            {
                                CellInfo m = new CellInfo();
                                //读取列名
                                ICell CellTitle = TitleRow.GetCell(t);
                                if (CellTitle != null)
                                {
                                    m.ColumnName = CellTitle.ToString();
                                }

                                ICell CellValue = row.GetCell(t);
                                if (CellValue != null)
                                {
                                    m.Value = CellValue.ToString();
                                }
                                list.Add(m);
                            }
                            array.Add(list);
                        }
                    }
                }

                #region 获取版本号
                List<string> li = new List<string>();
                for (int y = 0; y <= sheetEdition.LastRowNum; y++)
                {
                    if (sheetEdition.GetRow(y) == null)
                    {
                        li.Add("");
                    }
                    else
                    {
                        li.Add(sheetEdition.GetRow(y).GetCell(0).ToString());
                    }
                }
                #endregion

                return new Tuple<ArrayList, List<string>>(array, li);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}