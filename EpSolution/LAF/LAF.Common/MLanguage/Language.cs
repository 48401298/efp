using System.Collections.Generic;

namespace LAF.Common.MLanguage
{
    /// <summary>
    /// 语言包
    /// </summary>
    public class Language
    {
        private Dictionary<string, string> mItems;
        public Dictionary<string, string> Items
        {
            get
            {
                if (this.mItems == null)
                {
                    this.mItems = new Dictionary<string, string>();
                }
                return this.mItems;
            }
        }

        /// <summary>
        /// 获取多语言信息
        /// </summary>
        /// <param name="id">关键字</param>
        /// <returns>多语言信息</returns>
        public string GetItem(string id)
        {
            if (this.Items.ContainsKey(id))
                return Items[id];
            else
                return string.Empty;
        }
    }
}
