using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data.Distribute
{
    /// <summary>
    /// 分布式引擎接口
    /// </summary>
    public interface IShareEngine
    {
        /// <summary>
        /// 获取数据库关键字
        /// </summary>
        /// <param name="shareKey">分布关键字</param>
        /// <returns>数据库关键字</returns>
        string GetDbKey(string shareKey);
    }
}
