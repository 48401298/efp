using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.MES;

namespace Manage.DAL.MES
{
    /// <summary>
    /// 成品包装
    /// </summary>
    public class GoodPackingDAL
    {
        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="list"></param>
        public void Packing(List<GoodPackingForm> list)
        {
            string sql = "delete from T_FP_GoodPackingForm where GoodBarCode = @GoodBarCode";
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.OpenTs();

                //删除原装箱信息
                session.ExecuteSql(sql, new DataParameter("GoodBarCode", list[0].GoodBarCode));

                //添加装箱信息
                session.Insert<GoodPackingForm>(list);

                session.CommitTs();
            }
        }

        public string GetBatchNumberByBarCode(string barCode)
        {
            object value = null;
            string batchNumber = null;
            string sql = "select BatchNumber from T_FP_GoodPackingForm where DetailBarCode = @BarCode";
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                value = session.ExecuteSqlScalar(sql, new DataParameter("BarCode",barCode));
            }

            if (value != null)
            {
                batchNumber = value.ToString();
            }

            return batchNumber;
        }
    }
}
