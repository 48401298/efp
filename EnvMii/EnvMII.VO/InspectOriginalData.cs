using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvMII.VO
{
    /// <summary>
    /// 监测原始数据
    /// </summary>
    public class InspectOriginalData
    {
        /// <summary>
        /// 设备识别码
        /// </summary>
        public string DeviceSN { get; set; }

        /// <summary>
        /// 原始监测数据
        /// </summary>
        public string InspectData { get; set; }

        /// <summary>
        /// 监测时间
        /// </summary>
        public DateTime InspectTime { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

    }
}
