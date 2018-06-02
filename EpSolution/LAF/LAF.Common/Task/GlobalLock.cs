using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LAF.Common.Task
{
    #region 示例

    //using(GlobalLock taskLock=new GlobalLock("锁名称"))
    //{
    //    你的代码
    //}

    #endregion

    /// <summary>
    /// 全局锁
    /// 功    能：该类实现指定代码段同一时间只允许一个进程调用
    /// 创 建 者：黄顺权
    /// 创建日期：2015.12.28
    /// </summary>
    public class GlobalLock : IDisposable
    {
        //同步基元
        private EventWaitHandle _waitHandle = null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lockName">锁名称</param>
        public GlobalLock(string lockName)//定义的锁名，不要和别人重复
        {
            if (!lockName.StartsWith("Global\\")) lockName = "Global\\" + lockName;
            _waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, lockName);
            _waitHandle.WaitOne();
        }

        #endregion

        #region 释放

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_waitHandle == null) return;
            _waitHandle.Set();
            _waitHandle.Dispose();
            _waitHandle = null;
        }

        #endregion
    }
}
