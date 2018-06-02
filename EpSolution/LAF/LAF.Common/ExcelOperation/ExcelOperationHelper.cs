using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// Excel操作工具
    /// </summary>
    public partial class ExcelOperationHelper
    {
        #region 将数据生成为excel文件

        /// <summary>
        /// 将数据生成为excel文件
        /// <param name="info">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// </summary>    
        public void WriteWorkbook(SheetInfo info, string fileName)
        {
            ISheet sheet = null;
            XSSFWorkbook workbook1 = null;
            HSSFWorkbook workbook2 = null;
            try
            {
                //创建工作簿
                if (string.IsNullOrEmpty(info.TemplateFile) == true)
                {

                    //不使用模板                        
                    if (fileName.IndexOf(".xlsx") > 0)
                    {
                        workbook1 = new XSSFWorkbook();
                        sheet = workbook1.CreateSheet(info.SheetName);
                    }
                    else
                    {
                        workbook2 = new HSSFWorkbook();
                        sheet = workbook2.CreateSheet(info.SheetName);
                    }
                }
                else
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        //使用模板                   
                        if (fileName.IndexOf(".xlsx") > 0)
                        {
                            workbook1 = new XSSFWorkbook(fs);
                            sheet = workbook1.GetSheet(info.SheetName);
                        }
                        else
                        {
                            workbook2 = new HSSFWorkbook(fs);

                            sheet = workbook2.GetSheet(info.SheetName);
                        }
                    }

                    File.Delete(fileName);

                }

                //写入文件
                if (string.IsNullOrEmpty(info.TemplateFile) == true)
                {
                    FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                    try
                    {
                        #region 无模板

                        //写入列标题
                        IRow headRow = sheet.CreateRow(0);
                        for (int i = 0; i < info.ColInfos.Count; i++)
                        {
                            headRow.CreateCell(i).SetCellValue(info.ColInfos[i].ColumnTitle);
                            headRow.Cells[i].CellStyle.Alignment = HorizontalAlignment.Center;
                            if (info.ColInfos[i].ColumnWidth!=0)
                                sheet.SetColumnWidth(i, Convert.ToInt32((info.ColInfos[i].ColumnWidth / 3) * 0.4374 * 256));
                        }

                        //写入行数据
                        int rowsNum = 1;
                        IRow dataRow = null;
                        foreach (List<CellInfo> items in info.DataArray)
                        {
                            dataRow = sheet.CreateRow(rowsNum);
                            for (int i = 0; i < items.Count; i++)
                            {
                                dataRow.CreateCell(i).SetCellValue(items[i].Value);
                            }
                            rowsNum++;
                        }

                        #endregion

                        info.DataArray.Clear();
                        info.Dispose();

                        if (fileName.IndexOf(".xlsx") > 0)
                        {
                            workbook1.Write(fs);
                        }
                        else
                        {
                            workbook2.Write(fs);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        fs.Close();
                        fs.Dispose();
                        GC.Collect();
                    }
                }
                else
                {
                    FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    try
                    {
                        #region 有模板
                        int rowIndex = 0;
                        List<ICell> firstCells = new List<ICell>();

                        int count = info.DataArray.Count;

                        for (int k = 0; k < count; k++)
                        {
                            List<CellInfo> items = info.DataArray[0] as List<CellInfo>;
                            rowIndex++;                            
                            for (int i = 0; i < items.Count; i++)
                            {
                                IRow row = sheet.GetRow(int.Parse(items[i].YPosition));

                                if (row == null)
                                    row = sheet.CreateRow(int.Parse(items[i].YPosition));

                                ICell cell = row.GetCell(int.Parse(items[i].XPosition));

                                if (rowIndex == 1)
                                {
                                    firstCells.Add(cell);
                                }

                                if (cell == null)
                                    cell = row.CreateCell(int.Parse(items[i].XPosition));

                                if (firstCells[i] != null)
                                    cell.CellStyle = firstCells[i].CellStyle;

                                if (items[i].Value != "")
                                {
                                    switch (items[i].DataType.ToLower())
                                    {
                                        case "datetime":
                                            cell.SetCellValue(DateTime.Parse(items[i].Value));
                                            break;
                                        case "number":
                                            cell.SetCellValue(double.Parse(items[i].Value));
                                            break;
                                        default:
                                            cell.SetCellValue(items[i].Value);
                                            break;
                                    }
                                }
                            }
                            items.Clear();
                            items = null;
                            info.DataArray.RemoveAt(0);

                            #region 分段保存

                            if ((rowIndex % 20000) == 0)
                            {
                                if (fileName.IndexOf(".xlsx") > 0)
                                {
                                    workbook1.Write(fs);
                                }
                                else
                                {
                                    workbook2.Write(fs);
                                }
                                fs.Close();
                                fs.Dispose();
                                GC.Collect();
                                if (k <= count - 1)
                                {
                                    fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                }
                            }

                            #endregion
                        }

                        #endregion
                        info.DataArray.Clear();
                        info.Dispose();

                        if (fileName.IndexOf(".xlsx") > 0)
                        {
                            workbook1.Write(fs);
                        }
                        else
                        {
                            workbook2.Write(fs);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (fs != null)
                        {
                            fs.Close();
                            fs.Dispose();
                            GC.Collect();
                        }
                    }
                }
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        // 添加：重载生成excel文件方法，扩展支持多表、表头，单元格样式等 李鹏飞 2014-03-31 开始
        /// <summary>
        /// 将数据生成为excel文件
        /// <param name="infos">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// </summary>
        public void WriteWorkbook(List<SheetInfo> infos, string fileName)
        {
            ISheet sheet = null;
            XSSFWorkbook workbook1 = null;
            HSSFWorkbook workbook2 = null;
            try
            {
                //不使用模板
                if (fileName.IndexOf(".xlsx") > 0)
                {
                    workbook1 = new XSSFWorkbook();
                    sheet = workbook1.CreateSheet(infos[0].SheetName);
                }
                else
                {
                    workbook2 = new HSSFWorkbook();
                    sheet = workbook2.CreateSheet(infos[0].SheetName);
                }

                //写入文件
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    IRow dataRow = null;
                    ICell contentCell = null;


                    int rowsNum = 0;
                    for (int m = 0; m < infos.Count; m++)
                    {
                        ICellStyle headStyle = workbook1 == null ? workbook2.CreateCellStyle() : workbook1.CreateCellStyle();
                        IFont headFont = workbook1 == null ? workbook2.CreateFont() : workbook1.CreateFont();

                        ICellStyle cellStyle = workbook1 == null ? workbook2.CreateCellStyle() : workbook1.CreateCellStyle();
                        IFont cellFont = workbook1 == null ? workbook2.CreateFont() : workbook1.CreateFont();

                        #region  写入表标题
                        if (infos[m].IsShowTableCaption)
                        {
                            IRow tableCaptionRow = sheet.CreateRow(rowsNum++);
                            ICell tableCaptionCell = tableCaptionRow.CreateCell(0);

                            tableCaptionCell.SetCellValue(infos[m].TableCaption);

                            ICellStyle tableCaptionStyle = workbook1 == null ? workbook2.CreateCellStyle() : workbook1.CreateCellStyle();
                            tableCaptionStyle.BorderTop = infos[m].TableCaptionStyle.BorderStyle;
                            tableCaptionStyle.BorderBottom = infos[m].TableCaptionStyle.BorderStyle;
                            tableCaptionStyle.BorderLeft = infos[m].TableCaptionStyle.BorderStyle;
                            tableCaptionStyle.BorderRight = infos[m].TableCaptionStyle.BorderStyle;

                            tableCaptionStyle.FillForegroundColor = infos[m].TableCaptionStyle.FillForegroundColor;

                            IFont tableCaptionFont = workbook1 == null ? workbook2.CreateFont() : workbook1.CreateFont();

                            tableCaptionFont.FontName = infos[m].TableCaptionStyle.Font.FontName;
                            tableCaptionFont.FontHeightInPoints = infos[m].TableCaptionStyle.Font.FontHeightInPoints;
                            tableCaptionFont.Boldweight = infos[m].TableCaptionStyle.Font.Boldweight;
                            tableCaptionFont.Color = infos[m].TableCaptionStyle.Font.Color;

                            tableCaptionStyle.SetFont(tableCaptionFont);
                            tableCaptionCell.CellStyle = tableCaptionStyle;
                            // 合并单元格
                            // sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowsNum-1, rowsNum-1, 0, infos[m].Items.Count - 1));
                        }
                        #endregion

                        #region 写入表头
                        // 先判断是否显示表头
                        if (!infos[m].IsHideColumnHeader)
                        {
                            IRow headRow = sheet.CreateRow(rowsNum++);
                            for (int i = 0; i < infos[m].ColInfos.Count; i++)
                            {
                                ICell cell = headRow.CreateCell(i);
                                cell.SetCellValue(infos[m].ColInfos[i].ColumnTitle);

                                headStyle.FillPattern = infos[m].ColumnHeaderStyle.FillPattern;
                                headStyle.FillForegroundColor = infos[m].ColumnHeaderStyle.FillForegroundColor;
                                headStyle.BorderTop = infos[m].ColumnHeaderStyle.BorderStyle;
                                headStyle.BorderBottom = infos[m].ColumnHeaderStyle.BorderStyle;
                                headStyle.BorderLeft = infos[m].ColumnHeaderStyle.BorderStyle;
                                headStyle.BorderRight = infos[m].ColumnHeaderStyle.BorderStyle;

                                headFont.FontName = infos[m].ColumnHeaderStyle.Font.FontName;
                                headFont.FontHeightInPoints = infos[m].ColumnHeaderStyle.Font.FontHeightInPoints;
                                headFont.Boldweight = infos[m].ColumnHeaderStyle.Font.Boldweight;
                                headStyle.SetFont(headFont);

                                cell.CellStyle = headStyle;

                                if (infos[m].ColInfos[i].ColumnWidth > 0 && m == 0)
                                {
                                    sheet.SetColumnWidth(i, infos[m].ColInfos[i].ColumnWidth * 256);
                                }
                            }
                        }
                        #endregion

                        #region 写入行数据
                        foreach (List<CellInfo> items in infos[m].DataArray)
                        {
                            dataRow = sheet.CreateRow(rowsNum++);
                            for (int i = 0; i < items.Count; i++)
                            {
                                contentCell = dataRow.CreateCell(i);
                                contentCell.SetCellValue(items[i].Value);

                                cellStyle.FillPattern = infos[m].ContentCellStyle.FillPattern;
                                cellStyle.FillForegroundColor = infos[m].ContentCellStyle.FillForegroundColor;
                                cellStyle.BorderTop = infos[m].ContentCellStyle.BorderStyle;
                                cellStyle.BorderBottom = infos[m].ContentCellStyle.BorderStyle;
                                cellStyle.BorderLeft = infos[m].ContentCellStyle.BorderStyle;
                                cellStyle.BorderRight = infos[m].ContentCellStyle.BorderStyle;

                                cellFont.FontName = infos[m].ContentCellStyle.Font.FontName;
                                cellFont.FontHeightInPoints = infos[m].ContentCellStyle.Font.FontHeightInPoints;
                                cellFont.Boldweight = infos[m].ContentCellStyle.Font.Boldweight;
                                cellStyle.SetFont(cellFont);

                                contentCell.CellStyle = cellStyle;
                            }

                        }
                        #endregion

                        rowsNum++;
                    }

                    if (fileName.IndexOf(".xlsx") > 0)
                    {
                        workbook1.Write(fs);
                    }
                    else
                    {
                        workbook2.Write(fs);
                    }

                    fs.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // 添加：重载生成excel文件方法，扩展支持多表、表头，单元格样式等 李鹏飞 2014-03-31 结束
        

        #endregion

        #region 从excel读取数据

        /// <summary>
        /// 从excel读取数据
        /// </summary>
        /// <param name="info">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// <returns>数据ArrarList(List<DataInfoItem>)</returns>
        public ArrayList ReadWorkbook(SheetInfo info, string fileName)
        {
            ArrayList array = new ArrayList();
            ISheet sheet = null;
            XSSFWorkbook workbook1 = null;
            HSSFWorkbook workbook2 = null;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    //获取工作表
                    if (fileName.IndexOf(".xlsx") > 0)
                    {
                        workbook1 = new XSSFWorkbook(fs);
                        sheet = workbook1.GetSheet(info.SheetName);
                    }
                    else
                    {
                        workbook2 = new HSSFWorkbook(fs);
                        sheet = workbook2.GetSheet(info.SheetName);
                    }

                    if (sheet == null)
                    {
                        throw new Exception("数据文件中的sheet页名称与配置文件中的不一致。");
                    }

                    if (info.RecordCount == "n")
                    {
                        //多条
                        int startRow = info.ColInfos.Min(p => int.Parse(p.YPosition)) - 1;
                        int lastRow = sheet.LastRowNum;

                        for (int i = startRow; i <= lastRow; i++)
                        {
                            //行循环
                            IRow row = sheet.GetRow(i);

                            //列循环
                            List<CellInfo> list = new List<CellInfo>();
                            foreach (CellInfo item in info.ColInfos)
                            {
                                CellInfo m = new CellInfo();
                                m.ColumnName = item.ColumnName;

                                if (row != null)
                                {
                                    int colIndex = NameToIndex(item.XPosition);

                                    ICell cell = row.GetCell(colIndex);

                                    if (cell != null)
                                    {
                                        m.Value = cell.ToString();
                                    }
                                }
                                else
                                {
                                    m.Value = "";
                                }
                                list.Add(m);
                            }

                            int count = list.Count(p => string.IsNullOrEmpty(p.Value) == false);
                            if (count > 0)
                                array.Add(list);
                        }
                    }
                    else
                    {
                        //单条
                        List<CellInfo> list = new List<CellInfo>();
                        foreach (CellInfo item in info.ColInfos)
                        {
                            int rowIndex = int.Parse(item.YPosition) - 1;
                            int colIndex = NameToIndex(item.XPosition);

                            CellInfo m = new CellInfo();
                            m.ColumnName = item.ColumnName;

                            IRow row = sheet.GetRow(rowIndex);

                            if (row == null)
                            {
                                m.Value = "";
                                list.Add(m);
                                continue;
                            }

                            ICell cell = row.GetCell(colIndex);

                            if (cell != null)
                            {
                                m.Value = cell.ToString();
                            }
                        }

                        array.Add(list);
                    }

                }
                return array;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 列名列号转换

        /// <summary>
        /// 列名转换成列号
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns>列号</returns>
        public static int NameToIndex(string columnName)
        {
            if (!Regex.IsMatch(columnName.ToUpper(), @"[A-Z]+")) { throw new Exception("invalid parameter"); }
            int index = 0;
            char[] chars = columnName.ToUpper().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                index += ((int)chars[i] - (int)'A' + 1) * (int)Math.Pow(26, chars.Length - i - 1);
            }
            return index - 1;
        }

        /// <summary>
        /// 列号转换成列名
        /// </summary>
        /// <param name="index">列号</param>
        /// <returns>列名</returns>
        public static string IndexToName(int index)
        {
            if (index < 0) { throw new Exception("invalid parameter"); }
            List<string> chars = new List<string>();
            do
            {
                if (chars.Count > 0) index--;
                chars.Insert(0, ((char)(index % 26 + (int)'A')).ToString());
                index = (int)((index - index % 26) / 26);
            } while (index > 0);
            return String.Join(string.Empty, chars.ToArray());
        }

        #endregion



    }
}
