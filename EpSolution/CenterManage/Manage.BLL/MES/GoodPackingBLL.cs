using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.DAL.MES;
using Manage.Entity.MES;
using LAF.BLL;

namespace Manage.BLL.MES
{
    /// <summary>
    /// 成品包装
    /// </summary>
    public class GoodPackingBLL:BaseBLL
    {
        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="list"></param>
        public void Packing(List<GoodPackingForm> list)
        {
            new GoodPackingDAL().Packing(list);
        }

        public string GetBatchNumberByBarCode(string barCode)
        {
            return new GoodPackingDAL().GetBatchNumberByBarCode(barCode);
        }
    }
}
