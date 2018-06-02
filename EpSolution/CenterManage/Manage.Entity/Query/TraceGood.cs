using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.Query
{
    /// <summary>
    /// 产品追溯信息
    /// </summary>
    public class TraceGood
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string PID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string SPECIFICATION { get; set; }

        /// <summary>
        /// 生产时间
        /// </summary>
        public DateTime ProduceDate { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        public string FactoryName { get; set; }

        ///<summary>
        ///制造商
        ///</summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string LineName { get; set; }

        /// <summary>
        /// 工艺
        /// </summary>
        public string FlowName { get; set; }

        /// <summary>
        /// 班组
        /// </summary>
        public string WorkGroupName { get; set; }

        ///<summary>
        ///生产许可证号
        ///</summary>
        public string ProductionLicense { get; set; }

        ///<summary>
        ///产品标准号
        ///</summary>
        public string ProductStandard { get; set; }

        ///<summary>
        ///生产地址
        ///</summary>
        public string ProductionAddress { get; set; }

        ///<summary>
        ///保质期
        ///</summary>
        public string QualityPeriod { get; set; }
    }
}
