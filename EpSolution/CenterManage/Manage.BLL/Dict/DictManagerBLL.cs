using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.BLL.Dict
{
    /// <summary>
    /// 字典管理
    /// </summary>
    public class DictManagerBLL
    {
        /// <summary>
        /// 字典信息
        /// </summary>
        private Dictionary<string, string> _dicts = null;

        #region 构造函数

        public DictManagerBLL()
        {

        }

        public DictManagerBLL(DictKind kind)
        {
            this._dicts = this.GetModelDictionary(kind);
        }

        #endregion

        #region 获取字典值

        /// <summary>
        /// 获取字典值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public string GetDictValue(string key)
        {
            string value = key;

            if (string.IsNullOrEmpty(key) == true)
                return key;

            if (this._dicts == null)
                throw new Exception("未初始化字典信息。");

            this._dicts.TryGetValue(key, out value);

            return value;
        }

        #endregion

        #region 获取字典信息

        /// <summary>
        /// 获取字典信息
        /// <param name="kind">字典类别</param>
        /// </summary>
        /// <returns>字典信息</returns>
        public Dictionary<string, string> GetModelDictionary(DictKind kind)
        {
            Dictionary<string, string> dicts = null;

            try
            {
                LAF.Common.Util.ModelDictionaryHandler.TryGetModelDictionary(kind.ToString(), out dicts);
                return dicts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        
    }
}
