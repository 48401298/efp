using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvMII.VO
{
    /// <summary>
    /// 监测消息
    /// </summary>
    public class InspectMsg
    {
        /// <summary>
        /// 原始数据
        /// </summary>
        public InspectOriginalData OriginalData { get; set; }

        /// <summary>
        /// 监测项目信息
        /// </summary>
        public List<InspectItemData> ItemDatas { get; set; }
    }
}
