using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvMII.SSAppServer
{
    /// <summary>
    /// 工具
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// 转化bytes成16进制的字符
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHexStr(byte[] bytes, int count)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < count; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        /// <summary>
        /// 将16进制字符串转10进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal HexStrToDecimal(string value)
        {
            return decimal.Parse(Convert.ToInt32(value, 16).ToString());
        }

        /// <summary>
        /// 将16进制字符串转10进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int HexStrToInt(string value)
        {
            return Convert.ToInt32(value, 16);
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] hexStrToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符
            {
                result += Convert.ToString(b[i], 16);
            }
            return result;
        }

        public static string HexStringToString(string hs, Encoding encode)
        {
            string strTemp = "";
            byte[] b = new byte[hs.Length / 2];
            for (int i = 0; i < hs.Length / 2; i++)
            {
                strTemp = hs.Substring(i * 2, 2);
                b[i] = Convert.ToByte(strTemp, 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }
    }

}
