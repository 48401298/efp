using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Common.Balance
{
    /// <summary>
    /// 一致性哈希算法
    /// </summary>
    /// <typeparam name="TKey">键值类型</typeparam>
    public class ConsistentHashingAlgorithm<TKey> : HashingBase<TKey>
    {
        #region Fields

        private object _sync = new object();
        private IEqualityComparer<TKey> _comparer;
        private SortedDictionary<int, string> _dict;

        #endregion

        #region Methods

        public ConsistentHashingAlgorithm(int dummyNodeNum, IList<string> values)
        {
            _comparer = EqualityComparer<TKey>.Default;
            _dict = new SortedDictionary<int, string>();

            int c = 1;
            foreach (string value in values)
            {
                int node = (c++) * dummyNodeNum & 0x7fffffff;
                _dict.Add(node, value);
            }
        }

        public override void Remove(string value)
        {
            lock (_sync)
            {
                foreach (KeyValuePair<int, string> kv in _dict)
                {
                    if (kv.Value.Equals(value))
                    {
                        _dict.Remove(kv.Key);
                        return;
                    }
                }
            }
        }

        public override void Add(int dummyNodeNum, string value)
        {
            dummyNodeNum = dummyNodeNum & 0x7fffffff;
            lock (_sync) _dict.Add(dummyNodeNum, value);
        }

        public override string Get(TKey key)
        {
            int keyHash = _comparer.GetHashCode(key) & 0x7fffffff;
            lock (_sync)
            {
                if (keyHash > _dict.Last().Key)
                    return _dict.First().Value;

                return _dict.First(index => index.Key >= keyHash).Value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, string> kv in _dict)
                sb.AppendFormat("start:{0},server:{1}\r\n", kv.Key, kv.Value);

            return sb.ToString();
        }

        #endregion
    }
}
