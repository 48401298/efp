using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LAF.Common.MLanguage
{
    /// <summary>
    /// 多语言配置管理
    /// </summary>
    public class LanguageConfig
    {
        private LanguageConfig()
        { }

        private static LanguageConfig mInstance;
        public static LanguageConfig Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new LanguageConfig();
                    mInstance.LoadConfig();
                }
                return mInstance;
            }
        }

        private List<LanguageConfigItem> mLanguageItems;
        public List<LanguageConfigItem> LanguageItems
        {
            get
            {
                if (this.mLanguageItems == null)
                {
                    this.mLanguageItems = new List<LanguageConfigItem>();
                }
                return this.mLanguageItems;
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        public void LoadConfig()
        {
            XmlElement configElement = null;
            configElement = System.Configuration.ConfigurationManager.GetSection("languages") as XmlElement;
            this.LanguageItems.Clear();
            if (configElement == null)
            {
                this.LanguageItems.Add(new LanguageConfigItem() { Id = "zh-CN", Name = "中文（中国）" });
            }
            else
            {
                XmlDocument newDoc = new XmlDocument();
                XmlElement newElement = (XmlElement)newDoc.AppendChild(newDoc.ImportNode(configElement, true));
                string id = null;
                string name = null;

                foreach (XmlNode currentNode in newElement.ChildNodes)
                {
                    if (currentNode.NodeType == XmlNodeType.Element)
                    {
                        XmlElement currentElement = (XmlElement)currentNode;

                        if (currentElement.LocalName == "language")
                        {
                            id = null;
                            name = null;

                            if (currentElement.HasAttribute("Id"))
                            {
                                id = currentElement.GetAttribute("Id");
                            }
                            if (currentElement.HasAttribute("Name"))
                            {
                                name = currentElement.GetAttribute("Name");
                            }
                            if (!string.IsNullOrEmpty(id))
                            {
                                this.LanguageItems.Add(new LanguageConfigItem() { Id = id, Name = name });
                            }
                        }
                    }
                }
            }
        }
    }
}
