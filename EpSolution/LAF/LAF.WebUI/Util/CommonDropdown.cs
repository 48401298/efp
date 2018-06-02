using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.WebUI.Util
{

    public class CommonDropdown
    {
        /// <summary>
        /// 获取监测类型下拉列表
        /// </summary>
        /// <returns></returns>
        public List<DictInfo> getInspectDeviceType()
        {
            List<DictInfo> list = new List<DictInfo>();
            list.Add(new DictInfo() { ID = "TH10W", Des = "无线温湿度记录仪" });
            list.Add(new DictInfo() { ID = "TH11S", Des = "通讯型温湿度变送器" });
            list.Add(new DictInfo() { ID = "ANE", Des = "风速风向仪" });
            list.Add(new DictInfo() { ID = "TW", Des = "ACTW-CAR小型清洁刷式温盐传感器" });
            list.Add(new DictInfo() { ID = "LW", Des = "ACLW-CAR小型清洁刷式绿浊传感器" });
            list.Add(new DictInfo() { ID = "LLS", Des = "百叶箱光照传感器" });
            return list;
        }

        /// <summary>
        /// 获取监测结果类型下拉列表
        /// </summary>
        /// <returns></returns>
        public List<DictInfo> getInspectResultType()
        {
            List<DictInfo> list = new List<DictInfo>();
            list.Add(new DictInfo() { ID = "1", Des = "小时" });
            list.Add(new DictInfo() { ID = "2", Des = "天" });
            list.Add(new DictInfo() { ID = "3", Des = "月" });
            return list;
        }
    }
}
