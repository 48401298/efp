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
    ///　模块名称：入库单
    ///　作    者：李炳海
    ///　编写日期：2017年07月11日
    /// </summary>
    public class InStockBillView : BaseEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        public string ID { get; set; }

        ///<summary>
        ///入库单号
        ///</summary>
        public string BillNO { get; set; }

        ///<summary>
        ///入库日期
        ///</summary>
        public DateTime BillDate { get; set; }

        ///<summary>
        ///供货单位
        ///</summary>
        public string Provider { get; set; }

        ///<summary>
        ///入库方式
        ///</summary>
        public string InStockMode { get; set; }

        ///<summary>
        ///仓库
        ///</summary>
        public string Warehouse { get; set; }

        ///<summary>
        ///交货人
        ///</summary>
        public string DeliveryPerson { get; set; }

        ///<summary>
        ///验收人
        ///</summary>
        public string Receiver { get; set; }

        ///<summary>
        ///仓库负责人
        ///</summary>
        public string WHHeader { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [DBColumn(ColumnName = "Remark", DataType = DbType.String)]
        public string Remark { get; set; }

        /// <summary>
        /// 入库单明细
        /// </summary>
        public List<InStockDetailView> Details { get; set; }
    }

}
