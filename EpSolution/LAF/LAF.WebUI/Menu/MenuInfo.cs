using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.WebUI.Menu
{
    /// <summary>
    /// 菜单信息
    /// 创建者：李炳海
    /// 创建日期：2013.2.20
    /// </summary>
    public class MenuInfo
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string MenuID { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuDes { get; set; }

        /// <summary>
        /// 菜单类型
        /// menu:菜单;action:功能
        /// </summary>
        public string MenuType { get; set; }

        /// <summary>
        /// 是否校验权限
        /// </summary>
        public bool IsPower { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Ico { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 行为方式
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        public string PowerID { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 上级菜单编号
        /// </summary>
        public string SuperID { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public string SystemID { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuInfo> ChildMenus { get; set; }

        /// <summary>
        /// 行为列表
        /// </summary>
        public List<ActionInfo> Actions { get; set; }
    }
}
