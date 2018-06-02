using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Manage.BLL.WH;
using LAF.WebUI;

namespace Manage.Web.WH.Report
{
    /// <summary>
    /// 库存台账计算作业
    /// </summary>
    public class StockAccountComputeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                new StockAccountBLL().ComputeAccount();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}