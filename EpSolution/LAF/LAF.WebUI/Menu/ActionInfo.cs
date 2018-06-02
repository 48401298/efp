using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.WebUI.Menu
{
    /// <summary>
    /// 行为信息
    /// 创建者：李炳海
    /// 创建日期：2013.2.20
    /// </summary>
    [Serializable]
    public class ActionInfo
    {
        /// <summary>
        /// 控制器
        /// </summary>
        public string ContorllerName { get; set; }

        /// <summary>
        /// 行为名
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        public string PowerID { get; set; }
    }
}
