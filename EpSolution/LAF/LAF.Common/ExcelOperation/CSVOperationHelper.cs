using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// CSV操作工具
    /// </summary>
    public partial class CSVOperationHelper : IFileOperationHelper
    {
        #region 将数据生成为CSV文件

        /// <summary>
        /// 将数据生成为CSV文件
        /// <param name="info">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// </summary>    
        public void WriteWorkbook(SheetInfo info, string fileName)
        {
            try
            {
                using (System.IO.FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));

                    //输出列头
                    foreach (CellInfo cell in info.ColInfos)
                    {
                        sw.Write("\""+cell.ColumnTitle+"\"");
                        sw.Write(",");
                    }
                    sw.WriteLine("");

                    //输出数据
                    foreach (DataRow row in info.Dt.Rows)
                    {
                        foreach (CellInfo cell in info.ColInfos)
                        {
                            if (info.Dt.Columns.Contains(cell.ColumnName) == true)
                            {
                                if (row[cell.ColumnName] != System.DBNull.Value)
                                {
                                    sw.Write("\"" + row[cell.ColumnName].ToString() + "\"");
                                }
                                else
                                {
                                    sw.Write("\"\"");
                                }
                            }
                            else
                            {
                                sw.Write("\"\"");
                            }
                            sw.Write(",");
                        }
                        sw.WriteLine("");
                    }

                    sw.Close();
                    sw.Dispose();

                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        // 添加：重载生成CSV文件方法，扩展支持多表、表头，单元格样式等
        /// <summary>
        /// 将数据生成为CSV文件
        /// <param name="infos">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// </summary>
        public void WriteWorkbook(List<SheetInfo> infos, string fileName)
        {
            throw new Exception("功能未实现");
        }
        

        #endregion

        #region 从CSV读取数据

        /// <summary>
        /// 从CSV读取数据
        /// </summary>
        /// <param name="info">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// <returns>数据ArrarList(List<DataInfoItem>)</returns>
        public ArrayList ReadWorkbook(SheetInfo info, string fileName)
        {
            throw new Exception("功能未实现");
        }

        #endregion


    }
}
