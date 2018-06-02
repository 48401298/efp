using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Common.Balance
{
    /// <summary>
    /// 均衡管理器
    /// </summary>
    /// <typeparam name="TKey">键值类型</typeparam>
    public class HashingBalanceManager<TKey>
    {
        private HashingBase<TKey> _consistentHashing;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public void Initialize(IList<string> values)
        {
            _consistentHashing = new ConsistentHashingAlgorithm<TKey>(Int32.MaxValue / values.Count, values);
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="dummyNodeNum"></param>
        /// <param name="values"></param>
        public void Add(int dummyNodeNum, string values)
        {
            _consistentHashing.Add(dummyNodeNum, values);
        }


        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(TKey key)
        {
            return _consistentHashing.Get(key);
        }

        /// <summary>
        /// 移出节点
        /// </summary>
        /// <param name="value"></param>
        public void Remove(string value)
        {
            _consistentHashing.Remove(value);
        }
    }
}
