using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Entity;

namespace Manage.Entity.WH
{
    /// </summary>
    ///　模块名称：入库单明细
    ///　作    者：李炳海
    ///　编写日期：2017年07月11日
    /// </summary>
    public class InStockDetailView : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string BillID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 货品唯一识别码
        /// </summary>
        public string MatBarCode { get; set; }

        /// <summary>
        /// 货品主键
        /// </summary>
        public string MatID { get; set; }

        /// <summary>
        /// 货品编号
        /// </summary>
        public string MatCode { get; set; } 


        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 货品名称
        /// </summary>
        public string MatName { get; set; }

        /// <summary>
        /// 货品规格
        /// </summary>
        public string MatSpec { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal InAmount { get; set; }
        
        /// <summary>
        /// 单价
        /// </summary>
        public decimal InPrice { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal InSum { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string ProduceDate { get; set; }

        /// <summary>
        /// 仓位
        /// </summary>
        public string SaveSite { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 主计量单位
        /// </summary>
        public string MainUnitName { get; set; }

    }

}
