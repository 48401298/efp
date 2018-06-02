using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LAF.Common.Serialization
{
    /// <summary>
    /// 二进制数据与对象转换工具
    /// </summary>
    public class BinaryConvertHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns>二进制数据</returns>
        public static byte[] GetSerializes(object model)
        {

            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                bf.Serialize(ms, model);

                byte[] btArray = ms.ToArray();

                return btArray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
            }
        }

        /// <summary>
        /// 反序列化
        /// <typeparam name="T">对象类型</typeparam>   
        /// <param name="btArray">二机制数据</param>        
        /// </summary>
        /// <returns>对象</returns>
        public static T GetDeserialize<T>(byte[] btArray) where T : new()
        {
            MemoryStream ms = new MemoryStream(btArray);
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                T model = (T)bf.Deserialize(ms);

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
            }
        }
    }
}
