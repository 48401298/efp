using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using NPOI.SS.UserModel;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// 导出Excel
    /// </summary>
    public class IEExcelHelper
    {
        /// <summary>
        /// 配置文件及模板路径
        /// </summary>
        public static string FilePath { get; set; }

        /// <summary>
        /// 配置文件
        /// </summary>
        public string ConfigFile { get; set; }

        #region 获取配置信息

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns>配置信息</returns>
        public SheetInfo GetMainInfo(string infoName)
        {
            SheetInfo info = new SheetInfo();
            string configFile = FilePath + infoName + ".xml";

            try
            {
                ConfigFile = configFile;

                #region 读取配置信息

                XElement xel = XElement.Load(configFile);

                var datas = from x in xel.Descendants("DataInfoMain")
                            select x;

                var data = datas.First();

                info.InfoName = infoName;
                info.SheetName = data.Attribute("InfoName").Value;
                info.TemplateFile = data.Attribute("TemplateFile").Value;
                info.RecordCount = data.Attribute("RecordCount").Value;
                // 添加：重载生成excel文件方法，扩展支持多表、表头，单元格样式等 李鹏飞 2014-03-31 开始
                // 是否显示表标题
                if (data.Attribute("IsShowTableCaption") != null && data.Attribute("IsShowTableCaption").Value == "1")
                {
                    info.IsShowTableCaption = true;
                }
                // 是否因此列头
                if (data.Attribute("IsHideColumnHeader") != null && data.Attribute("IsHideColumnHeader").Value == "1")
                {
                    info.IsHideColumnHeader = true;
                }
                // 样式
                var style = data.Element("Style");
                if (style != null)
                {
                    Hashtable hsColors = NPOI.HSSF.Util.HSSFColor.GetIndexHash();

                    #region 表标题样式

                    var tableCaptionCellStyleXel = style.Element("TableCaptionCellStyle");
                    if (tableCaptionCellStyleXel != null)
                    {
                        CellStyle tableCaptionCellStyle = new CellStyle();
                        #region 单元格
                        if (tableCaptionCellStyleXel.Attribute("FillForegroundColor") != null 
                            && !string.IsNullOrEmpty(tableCaptionCellStyleXel.Attribute("FillForegroundColor").Value))
                        {
                            tableCaptionCellStyle.FillForegroundColor = GetColorIndex(hsColors, tableCaptionCellStyleXel.Attribute("FillForegroundColor").Value);
                        }
                        if (tableCaptionCellStyleXel.Attribute("BorderStyle") != null 
                            && !string.IsNullOrEmpty(tableCaptionCellStyleXel.Attribute("BorderStyle").Value))
                        {
                            tableCaptionCellStyle.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), tableCaptionCellStyleXel.Attribute("BorderStyle").Value);
                        }
                        #endregion

                        var tableCaptionFontXel = tableCaptionCellStyleXel.Element("Font");
                        if (tableCaptionFontXel != null)
                        {
                            FontStyle tableCaptionFontStyle = new FontStyle();
                            #region 字体
                            if (tableCaptionFontXel.Attribute("FontName") != null && !string.IsNullOrEmpty(tableCaptionFontXel.Attribute("FontName").Value))
                            {
                                tableCaptionFontStyle.FontName = tableCaptionFontXel.Attribute("FontName").Value;
                            }
                            if (tableCaptionFontXel.Attribute("FontHeightInPoints") != null && !string.IsNullOrEmpty(tableCaptionFontXel.Attribute("FontHeightInPoints").Value))
                            {
                                tableCaptionFontStyle.FontHeightInPoints = (short)Convert.ToInt32((tableCaptionFontXel.Attribute("FontHeightInPoints").Value));
                            }
                            if (tableCaptionFontXel.Attribute("Boldweight") != null && !string.IsNullOrEmpty(tableCaptionFontXel.Attribute("Boldweight").Value))
                            {
                                tableCaptionFontStyle.Boldweight = (short)Convert.ToInt32((tableCaptionFontXel.Attribute("Boldweight").Value));
                            }
                            if (tableCaptionFontXel.Attribute("Color") != null && !string.IsNullOrEmpty(tableCaptionFontXel.Attribute("Color").Value))
                            {
                                tableCaptionFontStyle.Color = GetColorIndex(hsColors, tableCaptionFontXel.Attribute("Color").Value);
                            }
                            #endregion

                            tableCaptionCellStyle.Font = tableCaptionFontStyle;
                        }
                        info.TableCaptionStyle = tableCaptionCellStyle;
                    }
                    #endregion

                    #region 列头样式

                    var columnHeaderCellStyleXel = style.Element("ColumnHeaderCellStyle");
                    if (columnHeaderCellStyleXel != null)
                    {
                        CellStyle columnHeaderCellStyle = new CellStyle();

                        //设置样式
                        this.SetCellStyle(columnHeaderCellStyle, columnHeaderCellStyleXel);
                  

                        var columnHeaderFontXel = columnHeaderCellStyleXel.Element("Font");
                        if (columnHeaderFontXel != null)
                        {
                            FontStyle columnHeaderFontStyle = new FontStyle();

                            //设置字体
                            this.SetCellFont(columnHeaderFontStyle, columnHeaderFontXel);

                            columnHeaderCellStyle.Font = columnHeaderFontStyle;
                        }
                        info.ColumnHeaderStyle = columnHeaderCellStyle;
                    }
                    #endregion

                    #region 内容单元格样式

                    var contentCellStyleXel = style.Element("ContentCellStyle");
                    if (contentCellStyleXel != null)
                    {
                        CellStyle contentCellStyle = new CellStyle();

                        //设置样式
                        this.SetCellStyle(contentCellStyle, contentCellStyleXel);
                       

                        var contentCellFontXel = contentCellStyleXel.Element("Font");
                        if (contentCellFontXel != null)
                        {
                            FontStyle contentCellFontStyle = new FontStyle();
                            //设置字体
                            this.SetCellFont(contentCellFontStyle, contentCellFontXel);

                            contentCellStyle.Font = contentCellFontStyle;
                        }
                        info.ContentCellStyle = contentCellStyle;
                    }
                    #endregion
                }
                // 添加：重载生成excel文件方法，扩展支持多表、表头，单元格样式等 李鹏飞 2014-03-31 结束
                info.ColInfos = new List<CellInfo>();
                foreach (XElement c in data.Element("DataInfoItems").Descendants("DataInfoItem"))
                {
                    CellInfo item = new CellInfo();

                    item.ColumnName = c.Attribute("ColumnName").Value;
                    item.ColumnTitle = c.Attribute("ColumnTitle").Value;
                    item.XPosition = c.Attribute("XPosition").Value;
                    item.YPosition = c.Attribute("YPosition").Value;
                    item.DataType = c.Attribute("DataType") != null ? c.Attribute("DataType").Value : "";

                    // 添加：重载生成excel文件方法，扩展支持多表、表头，单元格样式等 李鹏飞 2014-03-31 开始
                    // 列宽
                    if (c.Attribute("ColumnWidth") != null && !string.IsNullOrEmpty(c.Attribute("ColumnWidth").Value))
                    {
                        item.ColumnWidth = Convert.ToInt32(c.Attribute("ColumnWidth").Value);
                    }
                    // 添加：重载生成excel文件方法，扩展支持多表、表头，单元格样式等 李鹏飞 2014-03-31 结束

                    info.ColInfos.Add(item);
                }

                #endregion

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="cellStyle">单元格</param>
        /// <param name="styleElement">样式</param>
        private void SetCellStyle(CellStyle cellStyle,XElement styleElement)
        {
            Hashtable hsColors = NPOI.HSSF.Util.HSSFColor.GetIndexHash();

            if (styleElement.Attribute("FillForegroundColor") != null 
                && !string.IsNullOrEmpty(styleElement.Attribute("FillForegroundColor").Value))
            {
                cellStyle.FillForegroundColor = GetColorIndex(hsColors, styleElement.Attribute("FillForegroundColor").Value);
            }
            if (styleElement.Attribute("FillPattern") != null 
                && !string.IsNullOrEmpty(styleElement.Attribute("FillPattern").Value))
            {
                cellStyle.FillPattern = (FillPattern)Enum.Parse(typeof(FillPattern), styleElement.Attribute("FillPattern").Value);
            }
            if (styleElement.Attribute("BorderStyle") != null 
                && !string.IsNullOrEmpty(styleElement.Attribute("BorderStyle").Value))
            {
                cellStyle.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), styleElement.Attribute("BorderStyle").Value);
            }
        }

        /// <summary>
        /// 设置表格字体
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="fontXel">样式</param>
        private void SetCellFont(FontStyle font, XElement fontXel)
        {
            Hashtable hsColors = NPOI.HSSF.Util.HSSFColor.GetIndexHash();

            if (fontXel.Attribute("FontName") != null && !string.IsNullOrEmpty(fontXel.Attribute("FontName").Value))
            {
                font.FontName = fontXel.Attribute("FontName").Value;
            }
            if (fontXel.Attribute("FontHeightInPoints") != null && !string.IsNullOrEmpty(fontXel.Attribute("FontHeightInPoints").Value))
            {
                font.FontHeightInPoints = (short)Convert.ToInt32((fontXel.Attribute("FontHeightInPoints").Value));
            }
            if (fontXel.Attribute("Boldweight") != null && !string.IsNullOrEmpty(fontXel.Attribute("Boldweight").Value))
            {
                font.Boldweight = (short)Convert.ToInt32((fontXel.Attribute("Boldweight").Value));
            }
            if (fontXel.Attribute("Color") != null && !string.IsNullOrEmpty(fontXel.Attribute("Color").Value))
            {
                font.Color = GetColorIndex(hsColors, fontXel.Attribute("Color").Value);
            }
        }

        // 添加：重载生成excel文件方法，扩展支持多表、表头，单元格样式等 李鹏飞 2014-03-31 开始
        /// <summary>
        /// 获取颜色的 short值
        /// </summary>
        /// <param name="hs">Hashtable short与颜色名称</param>
        /// <param name="colorName">颜色名称</param>
        /// <returns>颜色short值</returns>
        private short GetColorIndex(Hashtable hs, string colorName)
        {
            foreach (System.Collections.DictionaryEntry item in hs)
            {
                if (item.Value.ToString().Substring(item.Value.ToString().LastIndexOf('+') + 1).Equals(colorName))
                {
                    return (short)Convert.ToInt32(item.Key);
                }
            }

            return (short)0;
        }        

        #endregion        

        #region 将datatable数据生成为excel文件

        /// <summary>
        /// 将datatable数据生成为excel文件
        /// </summary>
        /// <param name="info">导出配置信息</param>
        /// <param name="dt">导出数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="isBuild">是否通过模板生成</param>        
        public void ExportExcel(SheetInfo info, DataTable dt, string fileName, bool isBuild)
        {           

            if (fileName.LastIndexOf(".") < 0)
                throw new Exception("文件未设置扩展名");

            string extensions = fileName.Substring(fileName.LastIndexOf(".")+1);

            switch (extensions.ToLower())
            {
                case "csv":
                    info.Dt = dt;
                    new CSVOperationHelper().WriteWorkbook(info, fileName);
                    break;
                default:
                    #region 数据转换

                    info.DataArray = this.GetDataArray(info, dt);

                    #endregion

                    //xls、xlsx
                    if (isBuild == true && string.IsNullOrEmpty(info.TemplateFile) == false)
                    {
                        //根据模板生成文件
                        string templateFile = FilePath + info.TemplateFile;

                        System.IO.File.Copy(templateFile, fileName);
                    }

                    new ExcelOperationHelper().WriteWorkbook(info, fileName);
                    break;
            }

            dt.Rows.Clear();
            dt.Dispose();
            GC.Collect();


        }

        // 添加：重载生成excel文件方法，扩展支持多表、表头，单元格样式等 李鹏飞 2014-03-31 开始
        /// <summary>
        /// 将datatable数据生成为excel文件
        /// </summary>
        /// <param name="infos">导出配置信息</param>
        /// <param name="ds">导出数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="isBuild">是否通过模板生成</param>
        public void ExportExcel(List<SheetInfo> infos, DataSet ds, string fileName, bool isBuild)
        {
            if (isBuild == true && string.IsNullOrEmpty(infos[0].TemplateFile) == false)
            {
                //根据模板生成文件
                string templateFile = FilePath + infos[0].TemplateFile;

                System.IO.File.Copy(templateFile, fileName);
            }

            #region 数据转换

            for (int i = 0; i < infos.Count; i++)
            {
                infos[i].DataArray = this.GetDataArray(infos[i], ds.Tables[i]);
            }

            #endregion

            new ExcelOperationHelper().WriteWorkbook(infos, fileName);
        }
        // 添加：重载生成excel文件方法，扩展支持多表、表头，单元格样式等 李鹏飞 2014-03-31 结束

        /// <summary>
        /// 拷贝模板
        /// </summary>
        /// <param name="fileName">下载文件名</param>
        /// <param name="tf">模板名</param>
        public int ExportExcelModel(string fileName, string tf)
        {
            int count = 0;
            try
            {
                if (string.IsNullOrEmpty(tf) == false)
                {
                    //根据模板生成文件
                    string templateFile = FilePath + tf;
                    System.IO.File.Copy(templateFile, fileName);
                }
            }
            catch
            {
                count = -1;
            }
            return count;
        }

        /// <summary>
        /// 拷贝临时模板
        /// </summary>
        /// <param name="fileName">下载文件名</param>
        /// <param name="templateFile">模板名</param>
        public int ExportExcelTempModel(string fileName, string templateFile)
        {
            int count = 0;
            try
            {
                if (string.IsNullOrEmpty(templateFile) == false)
                {
                    System.IO.File.Copy(templateFile, fileName);
                }
            }
            catch
            {
                count = -1;
            }
            return count;
        }

        /// <summary>
        /// 将异常信息导入到Excel中
        /// </summary>
        /// <param name="excelException">异常欣喜</param>
        /// <param name="fileName">目标文件</param>
        /// <param name="strfileName">源文件</param>
        public int ExportExcel(List<ExcelExceptionModel> excelException, string fileName, string strfileName)
        {
            if (string.IsNullOrEmpty(strfileName) == false)
            {
                //根据模板生成文件
                System.IO.File.Copy(fileName, strfileName);
            }

            return new ExcelOperationHelper().WriteWorkSheetException(excelException, strfileName);
        }

        /// <summary>
        /// 将异常信息导入到Excel中
        /// </summary>
        /// <param name="excelException">异常信息</param>
        /// <param name="fileName">目标文件</param>
        /// <param name="strfileName">源文件</param>
        /// <param name="excelComment">异常数据</param>
        public int ExportExcelArraySheet(List<ExcelExceptionModel> excelException, string fileName,
                                         string strfileName, List<ExcelExceptionModel> excelComment)
        {
            if (string.IsNullOrEmpty(strfileName) == false)
            {
                //根据模板生成文件
                System.IO.File.Copy(fileName, strfileName);
            }

            return new ExcelOperationHelper().WriteWorkArraySheetException(excelException, strfileName, excelComment);
        }

        #endregion        

        #region 获取数据
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns>配置信息</returns>
        public SheetInfo GetMainInfoAuto(string strSheetName, List<ColumnInfo> li)
        {
            SheetInfo info = new SheetInfo();
            try
            {
                #region 读取配置信息
                //info.TemplateFile = TemplateFile;
                info.SheetName = string.IsNullOrEmpty(strSheetName) ? "sheet" : strSheetName;
                info.ColInfos = new List<CellInfo>();
                int i = 0;
                foreach (ColumnInfo c in li)
                {
                    CellInfo item = new CellInfo()
                    {
                        ColumnHidden = c.ColumnHidden,
                        ColumnValidation = c.ColumnValidation,
                        ColumnLock = c.ColumnLock,
                        ColumnWidth = c.ColumnWidth,
                        ColumnName = c.ColumnName,
                        ColumnTitle = c.ColumnTitle,
                        XPosition = i.ToString(),
                        YPosition = "3",
                        ColumnRangeValues = c.ColumnRangeValues,
                        ColValMaxLength = c.ColValMaxLength,
                    };

                    info.ColInfos.Add(item);
                    i++;
                }

                #endregion
                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据配置文件创建Excel列表

        /// <summary>
        /// 将datatable数据生成为excel文件
        /// </summary>
        /// <param name="info">导出配置信息/param>
        /// <param name="dt">导出数据</param>
        /// <param name="fileName">文件名</param>     
        public bool ExportExcelAuto(string tempFileFullPath, SheetInfo info, DataTable dt,
            LAF.Common.ExcelOperation.ExcelOperationHelper.DelegateCheck dCheck, params string[] Edition)
        {

            info.DataArray = this.ConvertDtToList(info, dt);
            return new ExcelOperationHelper().WriteWorkbookAuto(tempFileFullPath, info, dt, dCheck, Edition);
        }

        #endregion

        #region 根据模板生成Excel文件

        public bool ExportExcelTemplate(string tempFileFullPath, LAF.Common.ExcelOperation.ExcelOperationHelper.DelegateEditExcel edit)
        {
            return new ExcelOperationHelper().WriteWorkbookTemplate(tempFileFullPath, edit);
        }

        #endregion

        #region 将excel数据导入到datatable

        /// <summary>
        /// 将excel数据导入到datatable
        /// </summary>
        /// <param name="dt">带结构的空表</param>
        /// <param name="fileName">数据文件</param>
        /// <returns>数据表</returns>
        public Tuple<DataTable, List<string>> ImportExcelTuple(string fileName)
        {
            DataTable infoDt = null;
            ArrayList dataArray = null;
            try
            {
                var data = new ExcelOperationHelper().ReadWorkbook(fileName);
                if (data == null)
                {
                    return null;
                }
                //获取数据
                dataArray = data.Item1;
                List<string> li = data.Item2;
                //数据格式转换
                infoDt = this.GetDataTable(dataArray);

                return new Tuple<DataTable, List<string>>(infoDt, li);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将excel数据导入到datatable
        /// </summary>
        /// <param name="info">配置信息</param>
        /// <param name="dt">带结构的空表</param>
        /// <param name="fileName">数据文件</param>
        /// <returns>数据表</returns>
        public DataTable ImportExcel(SheetInfo info, DataTable dt, string fileName)
        {
            DataTable infoDt = null;
            ArrayList dataArray = null;
            try
            {

                //获取数据
                dataArray = new ExcelOperationHelper().ReadWorkbook(info, fileName);

                //数据格式转换
                infoDt = this.GetDataTable(info, dt, dataArray);

                return infoDt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        #endregion

        #region 数据格式转换

        /// <summary>
        /// 数据格式转换(datatable to arraylist)
        /// </summary>
        /// <param name="info">导出配置信息</param>
        /// <param name="dt">导出数据</param>
        /// <returns>格式转换后数据</returns>
        private ArrayList ConvertDtToList(SheetInfo info, DataTable dt)
        {
            ArrayList array = new ArrayList();

            int count = 0;
            int rowIndex = 0;
            int colIndex = 0;
            foreach (DataRow row in dt.Rows)
            {
                List<CellInfo> list = new List<CellInfo>();
                foreach (CellInfo item in info.ColInfos)
                {
                    rowIndex = int.Parse(item.YPosition) + count - 1;
                    colIndex = int.Parse(item.XPosition);

                    CellInfo m = new CellInfo();
                    m.XPosition = colIndex.ToString();
                    m.YPosition = rowIndex.ToString();


                    if (dt.Columns.IndexOf(item.ColumnName) < 0)
                    {
                        continue;
                    }

                    if (row[item.ColumnName] != System.DBNull.Value)
                    {
                        m.Value = row[item.ColumnName].ToString();
                        m.ColumnTitle = item.ColumnTitle;
                        m.ColumnLock = item.ColumnLock;
                    }
                    else
                    {
                        m.Value = "";
                        m.ColumnTitle = item.ColumnTitle;
                        m.ColumnLock = item.ColumnLock;
                    }

                    list.Add(m);
                }
                array.Add(list);
                count++;

                if (info.RecordCount == "1" && count == 1)
                {
                    break;
                }
            }
            
            return array;
        }

        /// <summary>
        /// 数据格式转换(datatable to arraylist)
        /// </summary>
        /// <param name="info">导出配置信息</param>
        /// <param name="dt">导出数据</param>
        /// <returns>格式转换后数据</returns>
        private ArrayList GetDataArray(SheetInfo info, DataTable dt)
        {
            ArrayList array = new ArrayList();

            // 添加：扩展支持 表标题显示 李鹏飞 2014-03-31 开始
            // 表标题
            info.TableCaption = dt.TableName;
            // 添加：扩展支持 表标题显示  李鹏飞 2014-03-31 结束

            int count = 0;
            int rowIndex = 0;
            int colIndex = 0;
            foreach (DataRow row in dt.Rows)
            {
                List<CellInfo> list = new List<CellInfo>();
                foreach (CellInfo item in info.ColInfos)
                {                    

                    rowIndex = int.Parse(item.YPosition) + count - 1;
                    colIndex = ExcelOperationHelper.NameToIndex(item.XPosition);

                    CellInfo m = new CellInfo();
                    m.XPosition = colIndex.ToString();
                    m.YPosition = rowIndex.ToString();
                    m.ColumnName = item.ColumnName;
                    m.DataType = item.DataType;

                    if (dt.Columns.Contains(item.ColumnName) == true)
                    {
                        if (row[item.ColumnName] != System.DBNull.Value)
                            m.Value = row[item.ColumnName].ToString();
                        else
                            m.Value = "";
                    }
                    else
                    {
                        m.Value = "";
                    }

                    list.Add(m);
                }
                array.Add(list);
                count++;

                if (info.RecordCount == "1" && count == 1)
                {
                    break;
                }
            }
            return array;
        }


        /// <summary>
        /// 数据格式转换(arraylist to datatable)
        /// </summary>
        /// <param name="info">导出配置信息</param>
        /// <param name="dt">导出数据</param>
        /// <returns>格式转换后数据</returns>
        private DataTable GetDataTable(SheetInfo info, DataTable dt, ArrayList array)
        {
            DataTable infoDt = null;

            try
            {
                if (dt != null)
                {
                    infoDt = dt;
                }
                else
                {
                    infoDt = new DataTable();

                    //初始化结构
                    foreach (CellInfo item in info.ColInfos)
                    {
                        infoDt.Columns.Add(new DataColumn(item.ColumnName));
                    }
                }

                //填充数据
                foreach (List<CellInfo> items in array)
                {
                    DataRow dr = infoDt.NewRow();
                    foreach (CellInfo item in items)
                    {
                        if (infoDt.Columns.IndexOf(item.ColumnName) >= 0)
                        {
                            dr[item.ColumnName] = item.Value;
                        }
                    }
                    infoDt.Rows.Add(dr);
                }

                return infoDt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 数据格式转换(arraylist to datatable)
        /// </summary>
        /// <param name="info">导出配置信息</param>
        /// <param name="dt">导出数据</param>
        /// <returns>格式转换后数据</returns>
        private DataTable GetDataTable(ArrayList array)
        {
            DataTable infoDt = null;

            try
            {
                infoDt = new DataTable("InfoData");

                List<CellInfo> InfoItem = (List<CellInfo>)array[0];

                //初始化结构
                foreach (CellInfo item in InfoItem)
                {
                    infoDt.Columns.Add(new DataColumn(item.ColumnName));
                }

                //填充数据
                foreach (List<CellInfo> items in array)
                {
                    DataRow dr = infoDt.NewRow();
                    foreach (CellInfo item in items)
                    {
                        if (infoDt.Columns.IndexOf(item.ColumnName) >= 0)
                        {
                            dr[item.ColumnName] = item.Value;
                        }
                    }
                    infoDt.Rows.Add(dr);
                }

                return infoDt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
