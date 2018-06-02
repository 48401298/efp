using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace LAF.WebUI.Menu
{
    /// <summary>
    /// 菜单工具
    /// 创建者：李炳海
    /// 创建日期：2013.2.20
    /// </summary>
    public class MenuHelper
    {
        /// <summary>
        /// 菜单文件路径
        /// </summary>
        public static string MenuFilePath { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<string> Powers { get; set; }

        /// <summary>
        /// 菜单信息
        /// </summary>
        public List<MenuInfo> Menus { get; set; }

        /// <summary>
        /// 行为列表
        /// </summary>
        private Hashtable ActionList { get; set; }

        public MenuHelper()
        {
            this.ActionList = new Hashtable();
        }

        #region 获取action列表

        public Hashtable GetAllActions()
        {
            Hashtable actionList = new Hashtable();

            if (this.Menus != null)
            {
                List<MenuInfo> actions = this.Menus.Where(p => p.MenuType.ToLower() == "action"
                    ||string.IsNullOrEmpty(p.Action)==false).ToList();

                foreach (MenuInfo action in actions)
                {
                    actionList.Add(action.Action, action.MenuDes);
                }
            }
            else
            {   
                #region 从配置文件获取全部菜单

                XElement xel = XElement.Load(MenuFilePath);

                var datas = from x in xel.Descendants("MenuItem")
                            select x;

                foreach (XElement d in datas)
                {
                    if (d.Attribute("Action").Value == "")
                        continue;

                    if (actionList.ContainsKey(d.Attribute("Action").Value.Substring(1)) == false)
                        actionList.Add(d.Attribute("Action").Value.Substring(1), d.Attribute("MenuDes").Value);

                    if (d.Element("Actions") == null)
                        continue;

                    foreach (XElement c in d.Element("Actions").Descendants("Action"))
                    {
                        string action = c.Attribute("ContorllerName").Value + "/" + c.Attribute("ActionName").Value;

                        if (actionList.ContainsKey(action) == false)
                            actionList.Add(action, c.Value);
                    }
                }

                #endregion
            }
            return actionList;
        }

        #endregion

        #region 获取菜单列表

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns>菜单列表</returns>
        public List<MenuInfo> GetMenuInfos()
        {
            return this.GetMenuInfos("");
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns>菜单列表</returns>
        public List<MenuInfo> GetMenuInfos(string menuFilePath)
        {
            List<MenuInfo> list = new List<MenuInfo>();
            List<MenuInfo> menus = new List<MenuInfo>();

            if (string.IsNullOrEmpty(menuFilePath) == true)
            {
                menuFilePath = MenuFilePath;
            }

            if (this.Menus != null)
            {
                list = this.Menus;
            }
            else
            {
                #region 从配置文件获取全部菜单

                XElement xel = XElement.Load(menuFilePath);

                var datas = from x in xel.Descendants("MenuItem")
                            select x;

                foreach (XElement d in datas)
                {
                    if (d.Attribute("Visible").Value.ToLower() == "false")
                    {
                        continue;
                    }

                    MenuInfo menu = new MenuInfo();
                    menu.MenuID = d.Attribute("MenuID").Value;
                    menu.MenuDes = d.Attribute("MenuDes").Value;

                    if (d.Attribute("MenuType") != null)
                    {
                        menu.MenuType = d.Attribute("MenuType").Value;
                    }
                    else
                    {
                        menu.MenuType = "";
                    }

                    if (d.Attribute("IsPower") != null)
                    {
                        menu.IsPower = bool.Parse(d.Attribute("IsPower").Value);
                    }
                    else
                    {
                        menu.IsPower = false;
                    }

                    if (d.Attribute("SystemID") != null)
                    {
                        menu.SystemID = d.Attribute("SystemID").Value;
                    }
                    else
                    {
                        menu.SystemID = "";
                    }
                    menu.Action = d.Attribute("Action").Value;
                    if (d.Attribute("Param") != null
                        && !string.IsNullOrWhiteSpace(d.Attribute("Param").Value))
                    {
                        menu.Action += "?" + d.Attribute("Param").Value.Replace("'", "%22");
                    }

                    if (d.Attribute("Target") != null)
                    {
                        menu.Target = d.Attribute("Target").Value;
                    }

                    menu.PowerID = d.Attribute("PowerID").Value;
                    menu.SuperID = d.Attribute("SuperID").Value;
                    if (d.Attribute("Seq") != null && !string.IsNullOrWhiteSpace(d.Attribute("Seq").Value))
                    {
                        menu.Seq = int.Parse(d.Attribute("Seq").Value);
                    }
                    else
                    {
                        menu.Seq = 1;
                    }
                    if (d.Attribute("Ico") != null)
                        menu.Ico = d.Attribute("Ico").Value;

                    menu.Actions = new List<ActionInfo>();
                    if (d.Element("Actions") != null)
                    {
                        foreach (XElement c in d.Element("Actions").Descendants("Action"))
                        {
                            ActionInfo action = new ActionInfo();
                            action.ContorllerName = c.Attribute("ContorllerName").Value;
                            action.ActionName = c.Attribute("ActionName").Value;
                            action.PowerID = c.Attribute("PowerID").Value;
                            menu.Actions.Add(action);
                        }
                    }

                    list.Add(menu);
                }

                #endregion
            }

            #region 添加一级菜单

            var array = list.Where(i => i.SuperID == "");
            foreach (MenuInfo childMenu in array)
            {
                //添加子权限
                this.BuildChildItems(childMenu, list);

                if (this.IsPower(childMenu) == false)
                    continue;

                if (string.IsNullOrEmpty(childMenu.Action) == false)
                {
                    if (this.ActionList.ContainsKey(childMenu.Action.Substring(1)) == false)
                        this.ActionList.Add(childMenu.Action.Substring(1), childMenu.Action.Substring(1));
                }

                this.AddAction(childMenu.Actions);
                menus.Add(childMenu);
            }
            #endregion

            return menus;
        }

        #endregion

        #region 获取菜单信息

        public List<MenuInfo> GetAllMenus()
        {
            List<MenuInfo> list = new List<MenuInfo>();

            if (this.Menus != null)
            {
                return this.Menus;
            }

            string menuFilePath = "";
            if (string.IsNullOrEmpty(menuFilePath) == true)
            {
                menuFilePath = MenuFilePath;
            }

            #region 从配置文件获取全部菜单

            XElement xel = XElement.Load(menuFilePath);

            var datas = from x in xel.Descendants("MenuItem")
                        select x;

            foreach (XElement d in datas)
            {
                if (d.Attribute("Visible").Value.ToLower() == "false")
                {
                    continue;
                }

                MenuInfo menu = new MenuInfo();
                menu.MenuID = d.Attribute("MenuID").Value;
                menu.MenuDes = d.Attribute("MenuDes").Value;
                if (d.Attribute("SystemID") != null)
                {
                    menu.SystemID = d.Attribute("SystemID").Value;
                }
                else
                {
                    menu.SystemID = "";
                }
                menu.Action = d.Attribute("Action").Value;
                if (d.Attribute("Param") != null
                    && !string.IsNullOrWhiteSpace(d.Attribute("Param").Value))
                {
                    menu.Action += "?" + d.Attribute("Param").Value.Replace("'", "%22");
                }

                if (d.Attribute("Target") != null)
                {
                    menu.Target = d.Attribute("Target").Value;
                }

                menu.PowerID = d.Attribute("PowerID").Value;
                menu.SuperID = d.Attribute("SuperID").Value;
                if (d.Attribute("Seq") != null && !string.IsNullOrWhiteSpace(d.Attribute("Seq").Value))
                {
                    menu.Seq = int.Parse(d.Attribute("Seq").Value);
                }
                else
                {
                    menu.Seq = 1;
                }
                if (d.Attribute("Ico") != null)
                    menu.Ico = d.Attribute("Ico").Value;

                menu.Actions = new List<ActionInfo>();
                if (d.Element("Actions") != null)
                {
                    foreach (XElement c in d.Element("Actions").Descendants("Action"))
                    {
                        ActionInfo action = new ActionInfo();
                        action.ContorllerName = c.Attribute("ContorllerName").Value;
                        action.ActionName = c.Attribute("ActionName").Value;
                        action.PowerID = c.Attribute("PowerID").Value;
                        menu.Actions.Add(action);
                    }
                }

                list.Add(menu);
            }

            #endregion

            return list;
        }

        #endregion

        #region 创建子菜单项

        /// <summary>
        /// 创建子菜单项
        /// </summary>
        /// <param name="parentItem">父权限</param〉
        /// <param name="list">子权限</param>
        private void BuildChildItems(MenuInfo parentItem, List<MenuInfo> list)
        {
            var array = list.Where(i => i.SuperID == parentItem.MenuID);

            parentItem.ChildMenus = new List<MenuInfo>();

            //如果查询不到下级菜单就结束
            if (array.Count() == 0)
            {
                return;
            }
            else
            {
                foreach (MenuInfo childMenu in array)
                {
                    //添加子菜单
                    this.BuildChildItems(childMenu, list);

                    if (this.IsPower(childMenu) == false)
                        continue;

                    if (string.IsNullOrEmpty(childMenu.Action) == false)
                    {
                        if (this.ActionList.ContainsKey(childMenu.Action.Substring(1)) == false)
                            this.ActionList.Add(childMenu.Action.Substring(1), childMenu.Action.Substring(1));
                    }


                    this.AddAction(childMenu.Actions);
                    parentItem.ChildMenus.Add(childMenu);
                }
            }
        }

        private void AddAction(List<ActionInfo> actions)
        {
            if (actions == null)
                return;

            foreach (ActionInfo action in actions)
            {
                if (this.Powers != null && this.Powers.IndexOf(action.PowerID) < 0)
                    continue;

                string actionName = action.ContorllerName + "/" + action.ActionName;
                if (!ActionList.ContainsKey(actionName))
                {
                    this.ActionList.Add(actionName, actionName);
                }
            }
        }

        #endregion

        #region 判断菜单是否有权限

        /// <summary>
        /// IsPower(MenuInfo menu)
        /// </summary>
        /// <param name="menu">菜单</param>
        /// <returns>true:有权限；false:无权限</returns>
        private bool IsPower(MenuInfo menu)
        {
            bool r = false;
            if (this.Powers == null)
            {
                return true;
            }

            if (this.Powers.IndexOf(menu.PowerID) >= 0)
                return true;

            if (menu.Actions != null)
            {
                foreach (ActionInfo action in menu.Actions)
                {
                    if (this.Powers.IndexOf(action.PowerID) >= 0)
                    {
                        r = true;
                        break;
                    }
                }
                if (r == true)
                    return r;
            }

            if (menu.ChildMenus == null)
                return false;

            //判断子菜单是否有权限
            foreach (MenuInfo cMenu in menu.ChildMenus)
            {
                bool flag = this.IsPower(cMenu);

                if (flag == true)
                {
                    r = true;
                    break;
                }
            }

            return r;
        }

        #endregion

        #region 获取可访问的行为列表

        public Hashtable GetActionList()
        {
            return this.ActionList;
        }

        #endregion
    }
}
