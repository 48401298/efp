using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Common.Balance
{
    /// <summary>
    /// 哈希计算抽象类
    /// </summary>
    public abstract class HashingBase<TKey>
    {
        public abstract void Add(int dummyNodeNum, string server);
        public abstract string Get(TKey key);
        public abstract void Remove(string server);
    }
}
