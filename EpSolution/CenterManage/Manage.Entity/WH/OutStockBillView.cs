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
    ///　模块名称：出库单
    ///　作    者：李炳海
    ///　编写日期：2017年07月11日
    /// </summary>
    public class OutStockBillView : BaseEntity
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
        ///出库日期
        ///</summary>
        public DateTime BillDate { get; set; }

        ///<summary>
        ///收货单位
        ///</summary>
        public string Client { get; set; }

        ///<summary>
        ///出库方式
        ///</summary>
        public string OutStockMode { get; set; }

        ///<summary>
        ///仓库
        ///</summary>
        public string Warehouse { get; set; }

        ///<summary>
        ///负责人
        ///</summary>
        public string Header { get; set; }

        ///<summary>
        ///仓库负责人
        ///</summary>
        public string WHHeader { get; set; }

        ///<summary>
        ///经手人
        ///</summary>
        public string HandlePerson { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        public string Remark { get; set; }

        /// <summary>
        /// 出库单明细
        /// </summary>
        public List<OutStockDetailView> Details { get; set; }
    }

}
