using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LAF.Common.MLanguage
{
    /// <summary>
    /// 多语言工具
    /// </summary>
    public class LanguageHelper
    {
        private LanguageHelper()
        {
        }

        private static LanguageHelper mInstance;
        public static LanguageHelper Instance
        {
            get
            {                
                return mInstance;
            }
        }
        private Dictionary<string, Language> mAllLanguages;
        /// <summary>
        /// 返回多语言实体集合。Name为语言的名称，Value为该语言对应的Language对象。
        /// </summary>
        public Dictionary<string, Language> AllLanguages
        {
            get
            {
                if (this.mAllLanguages == null)
                {
                    this.mAllLanguages = new Dictionary<string, Language>();
                }
                return this.mAllLanguages;
            }
        }

        private string mLanguageSessionName = "MULT_LANG";
        /// <summary>
        /// 获取或设置用于保存多语言Id的Session名称，默认为：MULT_LANG
        /// </summary>
        public string LanguageSessionName
        {
            get { return mLanguageSessionName; }
            set { mLanguageSessionName = value; }
        }

        /// <summary>
        /// 获取当前用户的语言包
        /// </summary>
        public Language Language
        {
            get
            {
                string langSessionName = this.LanguageSessionName;
                if (string.IsNullOrEmpty(langSessionName))
                {
                    langSessionName = "MULT_LANG";
                }
                string langId = System.Web.HttpContext.Current.Session[langSessionName] as string;
                if (string.IsNullOrEmpty(langId))
                    langId = "zh-CN";
                if (this.AllLanguages.ContainsKey(langId))
                {
                    return this.AllLanguages[langId];
                }
                else
                {
                    this.AllLanguages.Add(langId, this.LoadLanguage(langId));
                    return this.AllLanguages[langId];
                }
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        public static void Config()
        {
            if (mInstance == null)
            {
                mInstance = new LanguageHelper();
                //加载多语言配置
                LanguageConfig.Instance.LoadConfig();
                //逐个加载多语言包
                foreach (LanguageConfigItem item in LanguageConfig.Instance.LanguageItems)
                {
                    mInstance.AllLanguages.Add(item.Id, mInstance.LoadLanguage(item.Id));
                }
            }
        }

        private Language LoadLanguage(string langId)
        {
            if (string.IsNullOrEmpty(langId))
                langId = "zh-CN";

            Language lang = new Language();
            string langFileName = string.Format("lang_{0}.xml", langId);
            string rootPath = System.Web.HttpContext.Current.Server.MapPath("\\");
            string langFilePath = rootPath + "\\App_Data\\Language\\" + langFileName;
            if (!System.IO.File.Exists(langFilePath))
                return lang;

            XmlDocument newDoc = new XmlDocument();
            newDoc.Load(langFilePath);
            XmlElement rootEle = newDoc.DocumentElement;
            string name = null;
            string value = null;

            foreach (XmlNode currentNode in rootEle.ChildNodes)
            {
                if (currentNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement currentElement = (XmlElement)currentNode;
                    if (currentElement.HasAttribute("Name"))
                    {
                        name = currentElement.GetAttribute("Name");
                    }
                    if (currentElement.HasAttribute("Value"))
                    {
                        value = currentElement.GetAttribute("Value");
                    }
                    if (!string.IsNullOrEmpty(name))
                    {
                        name = name.ToUpper();
                        lang.Items[name] = value;
                    }
                }
            }
            return lang;
        }

        //获取多语言标签
        public static string GetLanguageLabel(string labelID)
        {
            if (LanguageHelper.Instance == null)
                return "";

            string labelText = "";

            labelText = Instance.Language.GetItem(labelID.ToUpper());

            return labelText;
        }
    }
}
