using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data.Distribute
{
    /// <summary>
    /// 索引分布式引擎
    /// </summary>
    public class IndexShareEngine : IShareEngine
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IndexShareEngine()
        {
            DistributeIndexs = new Dictionary<string, string>();
        }

        /// <summary>
        /// 分布式索引
        /// </summary>
        private Dictionary<string, string> DistributeIndexs { get; set; }

        /// <summary>
        /// 添加索引
        /// </summary>
        /// <param name="shareKey"></param>
        /// <param name="dbkey"></param>
        public void AddIndex(string shareKey, string dbkey)
        {
            this.DistributeIndexs.Add(shareKey, dbkey);
        }

        /// <summary>
        /// 清除索引
        /// </summary>
        public void ClearIndex()
        {
            DistributeIndexs.Clear();
        }

        /// <summary>
        /// 获取数据库关键字
        /// </summary>
        /// <param name="shareKey">分布关键字</param>
        /// <returns>数据库关键字</returns>
        public string GetDbKey(string shareKey)
        {
            bool e = this.DistributeIndexs.ContainsKey(shareKey);

            if (e == true)
            {
                return this.DistributeIndexs[shareKey];
            }
            else
            {
                throw new Exception("没有找到对应的分布索引。");
            }
        }
    }
}
