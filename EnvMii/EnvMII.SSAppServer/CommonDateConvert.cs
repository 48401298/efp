using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvMII.SSAppServer
{
    /// <summary>
    /// 共通类
    /// </summary>
    public class CommonDateConvert
    {
        /// <summary>
        /// 格林威治时间转北京时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ConvertGMTToUTC(DateTime dt)
        {
            return dt.AddHours(8);
        }

        /// <summary>
        /// 北京时间转格林威治
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ConvertUTCToGMT(DateTime dt)
        {
            return dt.AddHours(-8);
        }
    }
}
