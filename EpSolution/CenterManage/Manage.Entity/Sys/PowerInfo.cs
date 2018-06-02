using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Entity.Sys
{
    /// <summary>
    /// 权限信息
    /// </summary>
    public class PowerInfo
    {
        /// <summary>
        /// 权限主键
        /// </summary>
        public string PowerID { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string PowerDes { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public string PowerType { get; set; }

        /// <summary>
        /// 菜单主键
        /// </summary>
        public string MenuID { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public string SystemID { get; set; }

        /// <summary>
        /// 行为列表
        /// </summary>
        public List<string> ActionList { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 上级权限主键
        /// </summary>
        public string SuperID { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// 子权限
        /// </summary>
        public List<PowerInfo> ChildPowers { get; set; }
    }
}
