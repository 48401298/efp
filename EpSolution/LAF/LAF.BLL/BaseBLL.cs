

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Entity;


namespace LAF.BLL
{
    /// <summary>
    /// 业务逻辑层基类
    /// </summary>
    public class BaseBLL
    {
        /// <summary>
        /// 访问用户信息
        /// </summary>
        public LoginInfo LoginUser { get; set; }
    }
}
