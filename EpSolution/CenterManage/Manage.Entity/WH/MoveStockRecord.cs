using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.WH
{
    /// <summary>
    /// 库存移动记录
    /// </summary>
    public class MoveStockRecord
    {
        /// <summary>
        /// 货品唯一识别码
        /// </summary>
        public string IDCode { get; set; }

        /// <summary>
        /// 货品主键
        /// </summary>
        public string MatID { get; set; }

        /// <summary>
        /// 移入仓位
        /// </summary>
        public string ToSaveSite { get; set; }

        /// <summary>
        /// 移入仓库
        /// </summary>
        public string ToWarehouse { get; set; }
    }
}
