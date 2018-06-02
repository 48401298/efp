using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace EnvMII.VO
{
    [BsonIgnoreExtraElements]
    public class InspectItemData
    {

        /// <summary>
        /// 设备识别码
        /// </summary>
        public string DeviceSN { get; set; }

        /// <summary>
        /// 监测项目编号
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 监测值
        /// </summary>
        public string InspectData { get; set; }

        /// <summary>
        /// 监测时间
        /// </summary>
        public DateTime InspectTime { get; set; }
    }
}
