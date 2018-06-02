using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Quartz;
using LAF.Data;
using ExchangeEntity;
using ExchangeCenter;

namespace ExchangeCenter.Quartz
{
    public class OrganCacheJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {

            List<Orgaization> organList = null;
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                string sql = "SELECT * FROM aifishingep.t_organization T WHERE T.DELFLAG = '0'";
                List<DataParameter> dataParameter = new List<DataParameter>();
                //插入基本信息
                organList = session.GetList<Orgaization>(sql, dataParameter.ToArray()).ToList<Orgaization>();
            }

            if (organList != null && organList.Count > 0)
            {
                foreach (Orgaization di in organList)
                {
                    CommonCache.organList.AddOrUpdate(di.OrganID, di, (oldKey, oldValue) => di);
                }
            }
            
        }
    }
}
