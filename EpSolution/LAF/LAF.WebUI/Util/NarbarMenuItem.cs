using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.WebUI.Util
{
    public class NarbarMenuItem
    {
        private string itemID;
        private string text;
        private string href;
        private string imageUrl;
        private string value;
        private string cssClass;
        private string target;
        private string click;
        private string superID;
        private bool visible=true;
        private List<NarbarMenuItem> subItems = new List<NarbarMenuItem>();

        /// <summary>
        /// 菜单项ID
        /// </summary>
        public string ItemID
        {
            get { return this.itemID; }
            set { this.itemID = value; }
        }

        /// <summary>
        /// 显示的文本

        /// </summary>
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Href
        {
            get { return this.href; }
            set { this.href = value; }
        }

        /// <summary>
        /// 图标地址
        /// </summary>
        public string ImageUrl
        {
            get { return this.imageUrl; }
            set { this.imageUrl = value; }
        }

        /// <summary>
        /// 菜单项的存储值

        /// </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }


        /// <summary>
        /// 菜单项的样式名称
        /// </summary>
        public string CssClass
        {
            get { return this.cssClass; }
            set { this.cssClass = value; }
        }

        /// <summary>
        /// 链接frame/iframe的名称

        /// </summary>
        public string Target
        {
            get { return this.target; }
            set { this.target = value; }
        }

        /// <summary>
        /// 点击事件脚本
        /// </summary>
        public string Click
        {
            get { return this.click; }
            set { this.click = value; }
        }

        /// <summary>
        /// 上级菜单ID
        /// </summary>
        public string SuperID
        {
            get { return this.superID; }
            set { this.superID = value; }
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        /// <summary>
        /// 下一级菜单项
        /// </summary>
        public List<NarbarMenuItem> SubItems
        {
            get { return this.subItems; }
            set { this.subItems = value; }
        }
    }
}
