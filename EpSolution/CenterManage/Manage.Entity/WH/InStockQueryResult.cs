using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.WH
{
    /// <summary>
    /// 入库查询结果
    /// </summary>
    public class InStockQueryResult
    {
        /// <summary>
        /// 仓库
        /// </summary>
        public string WarehouseID { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// 入库方式
        /// </summary>
        public string InStockMode { get; set; }

        /// <summary>
        /// 货品类别
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 货品主键
        /// </summary>
        public string MatID { get; set; }

        /// <summary>
        /// 货品条码
        /// </summary>
        public string MatBarCode { get; set; }

        /// <summary>
        /// 货品编号
        /// </summary>
        public string MatCode { get; set; }

        /// <summary>
        /// 货品名称
        /// </summary>
        public string MatName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string SpecName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string MatSpec { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 主计量单位
        /// </summary>
        public string MainUnitName { get; set; }

        /// <summary>
        /// 仓位
        /// </summary>
        public string SaveSite { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        public string InDate { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public string EndDate { get; set; }
    }
}
