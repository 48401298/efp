using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LAF.WebUI.Menu;

namespace Manage.Web.Util
{
    /// <summary>
    /// Outlook式菜单
    /// </summary>
    public class OutLookMenu
    {
        #region 获取菜单html

        /// <summary>
        /// 获取菜单html
        /// </summary>
        /// <param name="menus">菜单信息</param>
        /// <returns>菜单html</returns>
        public string GetMenuHtml(List<MenuInfo> menus)
        {
            string html = "";
            StringBuilder sBuilder = new StringBuilder();

            try
            {
                //style=\"overflow: scroll\"
                sBuilder.Append("<div class='no-skin sidebar' style='width: 99%'> ");
                sBuilder.Append("<ul class='nav nav-list'>");

                //一级菜单               
                foreach (MenuInfo menu in menus)
                {
                    string labelID = "menu_" + menu.MenuID;
                    string labelText = GetLanguageLabel(labelID);
                    menu.MenuDes = (string.IsNullOrEmpty(labelText) == false ? labelText : menu.MenuDes);

                    var hasSub = menu.ChildMenus != null && menu.ChildMenus.Count != 0;
                    sBuilder.Append("<li>");

                    sBuilder.AppendFormat("<a href='#' id=\"f{0}\" class='dropdown-toggle'", menu.MenuID);
                    //如果有动作 添加动作
                    if (!string.IsNullOrWhiteSpace(menu.Action))
                    {
                        //多系统用
                        string action;
                        if (string.IsNullOrEmpty(menu.SystemID) == false)
                        {
                            action = ConfigurationManager.AppSettings[menu.SystemID.ToUpper() + "Root"] + menu.Action + (menu.Action.IndexOf("?") > 0 ? "&" : "?") + "sessionID=" + System.Web.HttpContext.Current.Session.SessionID;
                        }
                        else
                        {
                            action = menu.Action;
                        }
                        if (menu.Target == "url")
                        {
                            sBuilder.AppendFormat(" onclick=\"javascript:openUrl('f{0}','{2}','{3}','{1}');\" ",
                                menu.MenuID, menu.MenuDes, action, menu.Target);
                        }

                        if (menu.Target == "func")
                        {
                            sBuilder.AppendFormat(" onclick=\"javascript:{0}\"", action);
                        }
                    }
                    sBuilder.AppendFormat(">");

                    sBuilder.AppendFormat("	<span class='menu-text'>{0}</span>", menu.MenuDes);
                    //如果有字菜单，添加下箭头
                    if (hasSub)
                    {
                        sBuilder.Append("	<b class='arrow fa fa-angle-down'></b>");
                    }
                    sBuilder.Append("</a>");
                    if (hasSub)
                    {
                        sBuilder.Append("<b class='arrow'></b>");
                        sBuilder.Append(this.GetSubMenu(menu.ChildMenus));
                    }

                    sBuilder.Append("</li>");
                }
                sBuilder.Append("</ul>");
                sBuilder.Append("</div> ");

                html = sBuilder.ToString();
                return html;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取菜单组html

        private string GetSubMenu(List<MenuInfo> Menus)
        {

            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append("<ul class='submenu'>");
            foreach (var menu in Menus)
            {
                string labelID = "menu_" + menu.MenuID;
                string labelText = GetLanguageLabel(labelID);
                menu.MenuDes = (string.IsNullOrEmpty(labelText) == false ? labelText : menu.MenuDes);

                var hasSub = menu.ChildMenus != null && menu.ChildMenus.Count != 0;
                sBuilder.Append("<li>");

                sBuilder.AppendFormat("<a href='#' id=\"f{0}\" class='dropdown-toggle'", menu.MenuID);
                //如果有动作 添加动作
                if (!string.IsNullOrWhiteSpace(menu.Action))
                {
                    //多系统用
                    string action;
                    if (string.IsNullOrEmpty(menu.SystemID) == false)
                    {
                        action = ConfigurationManager.AppSettings[menu.SystemID.ToUpper() + "Root"] + menu.Action + (menu.Action.IndexOf("?") > 0 ? "&" : "?") + "sessionID=" + System.Web.HttpContext.Current.Session.SessionID;
                    }
                    else
                    {
                        action = menu.Action;
                    }                   

                    if (menu.Target == "url")
                    {
                        sBuilder.AppendFormat(" onclick=\"javascript:openUrl('f{0}','{2}','{3}','{1}');\" ",
                        menu.MenuID, menu.MenuDes, action, menu.Target);
                    }

                    if (menu.Target == "func")
                    {
                        sBuilder.AppendFormat(" onclick=\"javascript:{0}\"", action);
                    }
                }
                sBuilder.AppendFormat(">");
                sBuilder.AppendFormat("<i class='menu-icon fa fa-caret-right'></i>");
                sBuilder.AppendFormat("{0}", menu.MenuDes);
                //如果有字菜单，添加下箭头
                if (hasSub)
                {
                    sBuilder.Append("	<b class='arrow fa fa-angle-down'></b>");
                }
                sBuilder.Append("</a>");
                sBuilder.Append("<b class='arrow'></b>");
                if (hasSub)
                {
                    sBuilder.Append(this.GetSubMenu(menu.ChildMenus));
                }
                sBuilder.Append("</li>");
            }
            sBuilder.Append("</ul>");

            return sBuilder.ToString();
        }

        #endregion

        #region 获取多语言标签
        private static string GetLanguageLabel(string labelID)
        {
        //    if (LanguageHelper.Instance == null)
        //        return "";

        //    string labelText = "";

        //    labelText = LanguageHelper.Instance.Language.GetItem(labelID.ToUpper());

            return "";
        }
        #endregion
    }
}