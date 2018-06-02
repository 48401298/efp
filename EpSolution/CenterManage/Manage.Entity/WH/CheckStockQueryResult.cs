using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.WH
{
    /// <summary>
    /// 盘点查询结果
    /// </summary>
    public class CheckStockQueryResult
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
        /// 货品类别
        /// </summary>
        public string ProductType { get; set; }

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
        /// 规格
        /// </summary>
        public string SpecName { get; set; }

        /// <summary>
        /// 盘盈数量
        /// </summary>
        public decimal ProfitAmount { get; set; }

        /// <summary>
        /// 盘亏数量
        /// </summary>
        public decimal LossAmount { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

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
