using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using ExchangeEntity;
using System.Threading;

namespace ExchangeCenter
{
    public class CommonCache
    {
        /// <summary>
        /// 企业缓存列表
        /// </summary>
        public static ConcurrentDictionary<String, Orgaization> organList = new ConcurrentDictionary<String, Orgaization>();

        /// <summary>
        /// 机构服务信息
        /// </summary>
        public static ConcurrentDictionary<String, OrganInfo> organServiceList = new ConcurrentDictionary<String, OrganInfo>();

        /// <summary>
        /// 线程列表
        /// </summary>
        public static ConcurrentDictionary<String, Thread> threadList = new ConcurrentDictionary<String, Thread>();
    }
}
