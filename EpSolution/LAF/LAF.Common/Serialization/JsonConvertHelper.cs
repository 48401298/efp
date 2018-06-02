using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LAF.Common.Serialization
{
    /// <summary>
    /// Json与对象转换工具
    /// </summary>
    public class JsonConvertHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns>字符串</returns>
        public static string GetSerializes(object model)
        {
            try
            {
                string output = JsonConvert.SerializeObject(model);

                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 反序列化
        /// <typeparam name="T">对象类型</typeparam>   
        /// <param name="info">字符串</param>        
        /// </summary>
        /// <returns>对象</returns>
        public static T DeserializeObject<T>(string info)
        {
            try
            {
                if (string.IsNullOrEmpty(info))
                {
                    return default(T);
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(info);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        /// <summary>
        /// 反序列化
        /// <typeparam name="T">对象类型</typeparam>   
        /// <param name="info">字符串</param>        
        /// </summary>
        /// <returns>对象</returns>
        public static T GetDeserialize<T>(string info) where T : new()
        {
            try
            {
                T model = JsonConvert.DeserializeObject<T>(info);

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 反序列化
        /// <typeparam name="T">对象类型</typeparam>   
        /// <param name="info">字符串</param>        
        /// </summary>
        /// <returns>对象</returns>
        public static object GetDeserialize(Type t,string info)
        {
            try
            {
                object model = JsonConvert.DeserializeObject(info,t);

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
