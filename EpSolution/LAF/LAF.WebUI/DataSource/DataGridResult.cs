using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.WebUI.Util;

namespace LAF.WebUI.DataSource
{
    /// <summary>
    /// 表格数据源
    /// </summary>
    public class DataGridResult<T>
    {
        public int Total { get; set; }

        public List<T> Rows { get; set; }

        public DataGridResult()
        {
        }

        public DataGridResult(List<T> list, int totalCount)
        {
            Total = totalCount;
            Rows = list;
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns>json格式结果</returns>
        public string GetJsonSource()
        {
            string pa = ",";
            try
            {
                StringBuilder returnValue = new StringBuilder("{ \"total\":\" " + Total.ToString() + "\", \"rows\":[");

                if (Rows.Count > 0)
                {
                    foreach (var item in Rows)
                    {
                        string josnItem = JsonToModel.ConvertJosnForDataGird<T>(item);
                        josnItem=josnItem.Replace("\r\n", "");
                        returnValue.AppendLine(josnItem);

                        if (!Rows.Last().Equals(item))
                        {
                            returnValue.AppendLine(pa);
                        }

                    }
                    //returnValue.Remove(rows.Count - 1, 1);

                }

                returnValue.Append("]}");

                return returnValue.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="dt">表</param>
        /// <returns>json格式结果</returns>
        public string GetJsonSource(System.Data.DataTable dt)
        {
            string pa = ",";
            try
            {
                StringBuilder returnValue = new StringBuilder("{ \"total\":\" " + dt.Rows.Count.ToString() + "\", \"rows\":[");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i != 0)
                        {
                            returnValue.AppendLine(pa);
                        }

                        string josnItem = "";

                        List<string> itemValue = new List<string>();

                        foreach (DataColumn item in dt.Columns)
                        {
                            object value = dt.Rows[i][item.ColumnName];
                            itemValue.Add(string.Format("\"{0}\":\"{1}\"", item.ColumnName, value != null && value != System.DBNull.Value ? value : string.Empty));

                        }
                        if (itemValue.Count > 0)
                        {
                            josnItem = "{" + string.Join(",", itemValue.ToArray()) + "}";
                        }
                        else
                        {
                            josnItem = string.Empty;
                        }

                        returnValue.AppendLine(josnItem);

                    }

                }
                returnValue.Append("]");


                returnValue.Append("}");

                return returnValue.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
