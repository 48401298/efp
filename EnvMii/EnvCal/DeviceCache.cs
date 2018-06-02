using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvMII.VO;
using System.Collections.Concurrent;

namespace EnvCal
{
    public class DeviceCache
    {
        public static ConcurrentDictionary<String, DeviceInfo> deviceCacheList = new ConcurrentDictionary<String, DeviceInfo>();

        /// <summary>
        /// 获取设备的组织ID
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string getOrganIdByCode(string code)
        {
            if (deviceCacheList.ContainsKey(code))
            {
                return deviceCacheList[code] == null ? "" : deviceCacheList[code].OrganID;
            }
            return "";
        }
    }
}
