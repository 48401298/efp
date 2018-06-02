using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Common.Task
{
    #region 示例

    //using(MutexLock taskLock=new MutexLock("锁名称"))
    //{
    //    你的代码
    //}

    #endregion

    /// <summary>
    /// 进 程 锁
    /// 功    能：该类实现指定代码段同一时间只允许一个进程调用
    /// 创 建 者：李炳海
    /// 创建日期：2012.10.25
    /// </summary>
    public class MutexLock : IDisposable
    {
        //同步基元
        private System.Threading.Mutex _Mx = null;
        /// <summary>
        /// 是否全局锁
        /// </summary>
        public static bool GlobalLock { get; set; }

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lockName">锁名称</param>
        public MutexLock(string lockName)//定义的锁名，不要和别人重复
        {
            //监听
            while (true)
            {
                bool isCreate = true;
                _Mx = new System.Threading.Mutex(true, GlobalLock==true?@"Global\"+lockName:lockName, out isCreate);
                if (!isCreate)
                {
                    _Mx.Close();
                    _Mx = null;
                    System.Threading.Thread.Sleep(1);
                }
                else
                    break;
            }

        }

        #endregion

        #region 释放

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_Mx == null) return;
            _Mx.ReleaseMutex();
            _Mx.Close();
            _Mx = null;
        }

        #endregion
    }
}
