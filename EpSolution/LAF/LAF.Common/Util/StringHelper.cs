using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace LAF.Common.Util
{
    /// <summary>
    /// 字符串工具
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 返回指定数量的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="len"></param>
        public static string GetStringByCount(string s, int len)
        {
            string ss = "";

            for (int i = 0; i < len; i++)
            {
                ss += s;
            }

            return ss;
        }

        /// <summary>
        /// 把一个byte数组拼接成一个字符串
        /// </summary>
        /// <param name="byteArray">byte数组</param>
        /// <param name="spliter">拼接时使用的间隔符，输入空字符串，则意味着不使用间隔符</param>
        /// <returns>拼接后的字符串</returns>
        public static string GetStringFromByteArray(byte[] byteArray, string spliter)
        {
            string msReturn = "";
            for (int i = 0; i < byteArray.Length; i++)
                msReturn += byteArray[i].ToString() + spliter;

            msReturn = msReturn.Substring(0, msReturn.Length - spliter.Length);
            return msReturn;

        }

        /// <summary>
        /// 把字符串分解成byte数组
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>byte数组</returns>
        public static byte[] SplitStringToByteArray(string value)
        {

            string[] a = new string[] { };
            ArrayList d = new ArrayList();


            byte[] b;

            char[] spliter = new char[1] { ',' };

            try
            {
                a = value.Split(spliter);

                for (int i = 0; i < a.Length; i++)
                {
                    d.Add(Byte.Parse(a[i]));

                }

                b = (Byte[])(d.ToArray(typeof(Byte)));

                return b;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //返回一个对象的字符串表示
        public static string DefaultString(Object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return "";
            }
            else
            {
                return obj.ToString().Trim(); ;
            }
        }
        /// <summary>
        /// 判断字符串数组中是否有一个元素等于指字符串
        /// </summary>
        /// <param name="stringToFind">被查找的字符串</param>
        /// <param name="where">待搜索字符串数组</param>
        /// <param name="isCase">是否区分大小写，True表示区分，False表示不区分</param>
        /// <returns>True包含，False不包含</returns>
        public static bool FindString(string stringToFind, string[] where, bool isCase)
        {
            if (isCase)
                foreach (string a in where)
                {
                    if (a == stringToFind)
                        return true;
                    else
                        continue;

                }
            else
                foreach (string a in where)
                {
                    if (a.ToLower().Equals(stringToFind.ToLower()))
                        return true;
                    else
                        continue;

                }

            return false;


        }
        /// <summary>
        /// 从字符串中的尾部删除指定的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="removedString"></param>
        /// <returns></returns>
        public static string Remove(string sourceString, string removedString)
        {
            try
            {
                if (sourceString.IndexOf(removedString) < 0)
                    throw new Exception("原字符串中不包含移除字符串！");
                string result = sourceString;
                int lengthOfSourceString = sourceString.Length;
                int lengthOfRemovedString = removedString.Length;
                int startIndex = lengthOfSourceString - lengthOfRemovedString;
                string tempSubString = sourceString.Substring(startIndex);
                if (tempSubString.ToUpper() == removedString.ToUpper())
                {
                    result = sourceString.Remove(startIndex, lengthOfRemovedString);
                }
                return result;
            }
            catch
            {
                return sourceString;
            }
        }

        /// <summary>
        /// 获取拆分符右边的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string RightSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[tempString.Length - 1].ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取拆分符左边的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string LeftSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[0].ToString();
            }
            return result;
        }

        /// <summary>
        /// 去掉最后一个逗号
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static string DelLastComma(string origin)
        {
            if (origin.IndexOf(",") == -1)
            {
                return origin;
            }
            return origin.Substring(0, origin.LastIndexOf(","));
        }

        /// <summary>
        /// 删除不可见字符
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DeleteUnVisibleChar(string sourceString)
        {
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder(131);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int Unicode = sourceString[i];
                if (Unicode >= 16)
                {
                    sBuilder.Append(sourceString[i].ToString());
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 获取数组元素的合并字符串
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public static string GetArrayString(string[] stringArray)
        {
            string totalString = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                totalString = totalString + stringArray[i];
            }
            return totalString;
        }

        /// <summary>
        ///		获取某一字符串在字符串数组中出现的次数
        /// </summary>
        /// <param name="stringArray" type="string[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="findString" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetStringCount(string[] stringArray, string findString)
        {
            int count = -1;
            string totalString = GetArrayString(stringArray);
            string subString = totalString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = totalString.Substring(subString.IndexOf(findString));
                count += 1;
            }
            return count;
        }

        /// <summary>
        ///     获取某一字符串在字符串中出现的次数
        /// </summary>
        /// <param name="stringArray" type="string">
        ///     <para>
        ///         原字符串
        ///     </para>
        /// </param>
        /// <param name="findString" type="string">
        ///     <para>
        ///         匹配字符串
        ///     </para>
        /// </param>
        /// <returns>
        ///     匹配字符串数量
        /// </returns>
        public static int GetStringCount(string sourceString, string findString)
        {
            int count = 0;
            int findStringLength = findString.Length;
            string subString = sourceString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = subString.Substring(subString.IndexOf(findString) + findStringLength);
                count += 1;
            }
            return count;
        }

        /// <summary>
        /// 截取从startString开始到原字符串结尾的所有字符   
        /// </summary>
        /// <param name="sourceString" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="startString" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetSubString(string sourceString, string startString)
        {
            try
            {
                int index = sourceString.ToUpper().IndexOf(startString);
                if (index > 0)
                {
                    return sourceString.Substring(index);
                }
                return sourceString;
            }
            catch
            {
                return "";
            }
        }

        public static string GetSubString(string sourceString, string beginRemovedString, string endRemovedString)
        {
            try
            {
                if (sourceString.IndexOf(beginRemovedString) != 0)
                    beginRemovedString = "";

                if (sourceString.LastIndexOf(endRemovedString, sourceString.Length - endRemovedString.Length) < 0)
                    endRemovedString = "";

                int startIndex = beginRemovedString.Length;
                int length = sourceString.Length - beginRemovedString.Length - endRemovedString.Length;
                if (length > 0)
                {
                    return sourceString.Substring(startIndex, length);
                }
                return sourceString;
            }
            catch
            {
                return sourceString; ;
            }
        }

        /// <summary>
        /// 格式化日期 返回 yyyy-MM or yyyy-MM-dd 样式日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetFormatedDate(string date)
        {
            return GetFormatedDate(date, "-");
        }

        /// <summary>
        /// 格式化日期 返回 yyyy{splitChar}MM or yyyy{splitChar}MM{splitChar}dd 样式日期
        /// </summary>
        /// <param name="date"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string GetFormatedDate(string date, string splitChar)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return "";
            }
            if (date.Length == 6)
            {
                return date.Insert(4, splitChar);
            }
            if (date.Length == 8)
            {
                return date.Insert(6, splitChar).Insert(4, splitChar);
            }

            return "";
        }

        public static string GetFormatDateFromYM(string yearMonth)
        {
            if (string.IsNullOrEmpty(yearMonth) || yearMonth.Length != 6)
            {
                return string.Empty;
            }

            return string.Format("{0}年{1}月", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));


        }

        /// <summary>
        /// 取得日期中的年度 返回 yyyy
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetYearFromDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return string.Empty;
            }

            if (date.Length > 4)
            {
                return date.Substring(0, 4);
            }
            return string.Empty;
        }

        /// <summary>
        /// 按字节数取出字符串的长度
        /// </summary>
        /// <param name="strTmp">要计算的字符串</param>
        /// <returns>字符串的字节数</returns>
        public static int GetByteCount(string strTmp)
        {
            int intCharCount = 0;
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (System.Text.UTF8Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intCharCount = intCharCount + 2;
                }
                else
                {
                    intCharCount = intCharCount + 1;
                }
            }
            return intCharCount;
        }

        //判断是否为数字
        public static bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 按字节数要在字符串的位置
        /// </summary>
        /// <param name="intIns">字符串的位置</param>
        /// <param name="strTmp">要计算的字符串</param>
        /// <returns>字节的位置</returns>
        public static int GetByteIndex(int intIns, string strTmp)
        {
            int intReIns = 0;
            if (strTmp.Trim() == "")
            {
                return intIns;
            }
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (System.Text.UTF8Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intReIns = intReIns + 2;
                }
                else
                {
                    intReIns = intReIns + 1;
                }
                if (intReIns >= intIns)
                {
                    intReIns = i + 1;
                    break;
                }
            }
            return intReIns;
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (i == 0 && str.Substring(0, 1).Equals("-"))
                {
                    continue;
                }
                if ((str[i] < '0' || str[i] > '9') && str[i] != '.')
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断是否是年月
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsYearMonth(string str)
        {
            Regex regex = new Regex(@"^[12]\d{3}(0[1-9])|1[0-2]$");

            Match m = regex.Match(str);
            if (!m.Success)
                return false;
            return true;
        }

        /// <summary>
        /// 从百分数转成正常数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal SetFromPerValue(decimal? value)
        {
            if (!value.HasValue)
            {
                return 0;
            }

            return value.Value / 100;

        }


        /// <summary>
        /// 从正常数字转成百分数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal SetToPerValue(decimal? value)
        {
            if (!value.HasValue)
            {
                return 0;
            }

            return Math.Round(value.Value * 100);

        }

        
    }
}
