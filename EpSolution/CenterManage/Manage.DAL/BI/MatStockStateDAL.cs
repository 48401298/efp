using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Entity.MES;
using LAF.Data;
using System.Data;
using Manage.Entity.WH;

namespace Manage.DAL.BI
{
    /// <summary>
    /// 货品库存分析
    /// </summary>
    public class MatStockStateDAL
    {
        #region 获取指定仓库货品库存信息

        /// <summary>
        /// 获取指定仓库货品库存信息
        /// </summary>
        /// <param name="whID">仓库主键</param>
        /// <returns></returns>
        public List<WHMatAmount> GetMatStockListByWH(string whID)
        {
            List<WHMatAmount> list = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();

            sql = @"select t2.MatName,sum(t1.MainAmount) as MainAmount 
                    from T_WH_MatAmount t1
                    left outer join T_WH_Mat t2 on t1.MatID = t2.ID
                    where t1.Warehouse = @whID
                    group by t2.MatName";
            parameters.Add(new DataParameter("whID",whID));
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<WHMatAmount>(sql, parameters.ToArray()).ToList();
            }

            return list;
        }

        #endregion
    }
}
