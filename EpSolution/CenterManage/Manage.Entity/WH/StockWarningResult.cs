using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.WH
{
    /// <summary>
    /// 库存预警查询结果
    /// </summary>
    public class StockWarningResult
    {
        ///<summary>
        ///主键
        ///</summary>
        public string ID { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// 货品主键
        /// </summary>
        public string MatID { get; set; }

        /// <summary>
        /// 货品编号
        /// </summary>
        public string MatCode { get; set; }

        /// <summary>
        /// 货品名称
        /// </summary>
        public string MatName { get; set; }

        /// <summary>
        /// 货品类别
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string MainUnitName { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal StockAmount { get; set; }

        /// <summary>
        /// 库存上线
        /// </summary>
        public decimal MaxAmount { get; set; }

        /// <summary>
        /// 库存线下
        /// </summary>
        public decimal MinAmount { get; set; }

        /// <summary>
        /// 预警模式，1:高储;2:低储
        /// </summary>
        public string WarningMode { get; set; }
        
    }
}
