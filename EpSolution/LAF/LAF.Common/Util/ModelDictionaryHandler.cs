using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Data;

namespace LAF.Common.Util
{
    /// <summary>
    /// 对xml字典管理
    /// 作者：狄迪
    /// </summary>
    public class ModelDictionaryHandler
    {
        private static Dictionary<string, Dictionary<string, string>> _toolModelDictionary = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<string, Dictionary<string, XmlNode>> _toolModelDictionaryXMLNode = new Dictionary<string, Dictionary<string, XmlNode>>();
        static string congstr = ConfigurationManager.AppSettings.Get("Dictionary");

        private ModelDictionaryHandler()
        {

        }

        /// <summary>
        /// 加载配置
        /// </summary>
        public static void Configure()
        {
            _WriteModelDictionary(congstr, false);
        }

        /// <summary>
        /// 通过路径加载配置文件
        /// </summary>
        /// <param name="strPath">配置文件路径</param>
        /// <param name="overwrite">是否覆盖已加载信息。默认FALSE。场景：通常以根配置信息为主</param>
        public static void Configure(string strPath, bool overwrite = false)
        {
            _WriteModelDictionary(strPath, overwrite);
        }

        private static void _WriteModelDictionary(string strPath, bool overwrite)
        {
            //string congstr=ConfigurationManager.AppSettings.Get("Dictionary");

            if (!string.IsNullOrEmpty(strPath))
            {
                //系统目录
                string atr = System.AppDomain.CurrentDomain.BaseDirectory;

                XmlDocument doc = new XmlDocument();
                if (System.IO.File.Exists(strPath) == false)
                    doc.Load(atr + strPath);
                else
                    doc.Load(strPath);

                XmlNodeList xn = doc.GetElementsByTagName("Dictionarys");

                if (xn.Count == 0)
                {
                    throw new Exception("配置文件中未指定<Dictionarys>节点");
                }

                //_toolModelDictionary.Clear();

                foreach (XmlNode Dictionary in xn[0].ChildNodes)
                {
                    if (Dictionary.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }

                    Dictionary<string, string> _dictionary = new Dictionary<string, string>();
                    Dictionary<string, XmlNode> _dictionaryXMLNode = new Dictionary<string, XmlNode>();
                    foreach (XmlNode item in Dictionary.ChildNodes)
                    {
                        if (item.NodeType != XmlNodeType.Element)
                        {
                            continue;
                        }
                        _dictionary.Add(item.Attributes["key"].Value, item.Attributes["value"].Value);
                        _dictionaryXMLNode.Add(item.Attributes["key"].Value, item);
                    }

                    string dicKey = Dictionary.Attributes["Name"].Value;
                    if (_toolModelDictionary.ContainsKey(dicKey))
                    {
                        bool blnISOmport = false;
                        foreach (var item in Dictionary.Attributes)
                        {
                            if ("Import".Equals(item))
                            {
                                blnISOmport = true;
                                break;
                            }
                        }

                        if (overwrite || blnISOmport)
                        {
                            _toolModelDictionary[dicKey] = _dictionary;
                            _toolModelDictionaryXMLNode[dicKey] = _dictionaryXMLNode;
                        }
                    }
                    else
                    {
                        _toolModelDictionary.Add(dicKey, _dictionary);
                        _toolModelDictionaryXMLNode.Add(dicKey, _dictionaryXMLNode);
                    }


                }
            }
            else
            {
                throw new Exception("未在webconfig中指定配置文件的路径");
            }
        }

        /// <summary>
        /// 获取数据字典。 
        /// </summary>
        /// <param name="dictionaryName">字典名</param>
        /// <param name="dictionary">字典信息</param>
        /// <returns>true:获取成功；false:获取失败。</returns>
        public static bool TryGetModelDictionary(string dictionaryName, out Dictionary<string, string> dictionary)
        {
            if (_toolModelDictionary.Count == 0)
            {
                Configure();
            }
            string buttonText = string.Empty;
            return _toolModelDictionary.TryGetValue(dictionaryName, out dictionary);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionaryName"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static bool TryGetModelDictionary(string dictionaryName, out Dictionary<string, XmlNode> dictionary)
        {
            if (_toolModelDictionaryXMLNode.Count == 0)
            {
                Configure();
            }
            string buttonText = string.Empty;
            return _toolModelDictionaryXMLNode.TryGetValue(dictionaryName, out dictionary);

        }


        /// <summary>
        /// 获取指定数据字典的值
        /// </summary>        
        /// <param name="dictionary">字典名</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetModelDictionaryValue(string dictionaryName, string key)
        {
            string text = string.Empty;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (TryGetModelDictionary(dictionaryName, out dictionary))
            {
                if (dictionary.TryGetValue(key, out  text))
                {
                    return text;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 获取字典信息以DataTable返回
        /// </summary>
        /// <param name="dictionaryName">字典类型名称</param>
        /// <returns>Data数据表</returns>
        public static DataTable GetDataTableFromDic(string dictionaryName)
        {
            //定义表结构
            DataTable dicDt = new DataTable();

            //添加列属性
            dicDt.Columns.Add("ID");
            dicDt.Columns.Add("Text");


            //获取字典信息
            if (_toolModelDictionary.Count == 0)
            {
                Configure();
            }
            string buttonText = string.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            bool isSuccess = _toolModelDictionary.TryGetValue(dictionaryName, out dic);

            //获取字典成功
            if (isSuccess)
            {
                //遍历字典，将字典信息加到数据表中
                foreach (var item in dic)
                {
                    DataRow dr = dicDt.NewRow();
                    dr["ID"] = item.Key;
                    dr["Text"] = item.Value;
                    dicDt.Rows.Add(dr);
                }
            }

            return dicDt;

        }

    }
}