using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Common.Balance;

namespace LAF.Data.Distribute
{
    /// <summary>
    /// 平均分布式引擎
    /// </summary>
    public class AvgShareEngine : IShareEngine
    {
        private HashingBalanceManager<string> _balanceManager = new HashingBalanceManager<string>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public void Initialize(IList<string> values)
        {
            _balanceManager.Initialize(values);
        }

        /// <summary>
        /// 获取数据库关键字
        /// </summary>
        /// <param name="shareKey">分布关键字</param>
        /// <returns>数据库关键字</returns>
        public string GetDbKey(string shareKey)
        {
            string dbkey = _balanceManager.Get(shareKey);
            return dbkey;
        }
    }
}
