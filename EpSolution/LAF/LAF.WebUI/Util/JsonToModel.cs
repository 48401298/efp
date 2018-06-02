using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.WebUI.Attribute;

namespace LAF.WebUI.Util
{
    /// <summary>
    /// 实现实体与json的转换
    /// 作者：狄迪
    /// </summary>
    public static class JsonToModel
    {
        /// <summary>
        /// 是否把空格替换为&nbsp;
        /// </summary>
        public static bool EmptyReplace { get; set; }

        /// <summary>
        /// 实体转josn字符串 此方法实体类属性不能为复杂类型。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ConvertToJosn<T>(T model)
        {
            List<string> returnValue = new List<string>();
            Type t = typeof(T);
            object obj = Activator.CreateInstance(t);
            var properties = t.GetProperties();

            foreach (var item in properties)
            {
                object[] attrs = item.GetCustomAttributes(typeof(DisJsonDateAttribute), true);
                if (attrs.Length == 0)
                {
                    var name = item.Name;
                    var value = item.GetValue(model, null);

                    //正常显示“双引号”
                    if (value != null
                        &&value.GetType().ToString().ToLower()=="system.string")
                    {
                        //编码转换
                        string valueStr = value.ToString();
                        valueStr = valueStr.Replace(@"\", @"\\");
                        valueStr = System.Web.HttpUtility.HtmlEncode(valueStr);
                        if (EmptyReplace==true)
                            valueStr = valueStr.Replace(@" ", @"&nbsp;");
                        value = valueStr;
                    }                    

                    if(value is DateTime)
                        returnValue.Add(string.Format("\"{0}\":\"{1}\"", name, value != null ? ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") : string.Empty));
                    else
                        returnValue.Add(string.Format("\"{0}\":\"{1}\"", name, value != null ? value : string.Empty));
                }


            }
            if (returnValue.Count > 0)
            {
                return "{" + string.Join(",", returnValue.ToArray()) + "}";
            }
            else
            {
                return string.Empty;
            }


        }

        /// <summary>
        ///  实体转josn字符串 此方法实体类属性不能为复杂类型。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ConvertJosnForDataGird<T>(T model)
        {

            List<string> returnValue = new List<string>();
            Type t = typeof(T);
            object obj = Activator.CreateInstance(t);
            var properties = t.GetProperties();

            foreach (var item in properties)
            {
                var name = item.Name;
                var value = item.GetValue(model, null);

                
                if (value != null
                        && value.GetType().ToString().ToLower() == "system.string")
                {
                    //编码转换
                    string valueStr = value.ToString();
                    valueStr = valueStr.Replace(@"\", @"\\");
                    valueStr = System.Web.HttpUtility.HtmlEncode(valueStr);
                    if (EmptyReplace == true)
                        valueStr = valueStr.Replace(@" ", @"&nbsp;");
                    value = valueStr;
                }

                if (value is DateTime)
                    returnValue.Add(string.Format("\"{0}\":\"{1}\"", name, value != null ? ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") : string.Empty));
                else
                    returnValue.Add(string.Format("\"{0}\":\"{1}\"", name, value != null ? value : string.Empty));
               

            }
            if (returnValue.Count > 0)
            {
                return "{" + string.Join(",", returnValue.ToArray()) + "}";
            }
            else
            {
                return string.Empty;
            }


        }


        /// <summary>
        /// 单条json数据转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">字符窜（格式为{a:'',b:''}）</param>
        /// <returns></returns>
        private static T ConvertToEntity<T>(string str)
        {
            Type t = typeof(T);
            object obj = Activator.CreateInstance(t);
            var properties = t.GetProperties();
            string m = str.Trim('{').Trim('}');
            string[] arr = m.Split(',');
            for (int i = 0; i < arr.Count(); i++)
            {
                for (int k = 0; k < properties.Count(); k++)
                {
                    string Name = arr[i].Substring(0, arr[i].IndexOf(":"));
                    object Value = arr[i].Substring(arr[i].IndexOf(":") + 1);
                    if (properties[k].Name.Equals(Name))
                    {
                        if (properties[k].PropertyType.Equals(typeof(int)))
                        {
                            properties[k].SetValue(obj, Convert.ToInt32(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(string)))
                        {
                            properties[k].SetValue(obj, Convert.ToString(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(long)))
                        {
                            properties[k].SetValue(obj, Convert.ToInt64(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(decimal)))
                        {
                            properties[k].SetValue(obj, Convert.ToDecimal(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(double)))
                        {
                            properties[k].SetValue(obj, Convert.ToDouble(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<int>)))
                        {
                            properties[k].SetValue(obj, Convert.ToInt32(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<decimal>)))
                        {
                            properties[k].SetValue(obj, Convert.ToDecimal(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<long>)))
                        {
                            properties[k].SetValue(obj, Convert.ToInt64(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<double>)))
                        {
                            properties[k].SetValue(obj, Convert.ToDouble(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<DateTime>)))
                        {
                            properties[k].SetValue(obj, Convert.ToDateTime(Value), null);
                        }

                    }
                }

            }
            return (T)obj;
        }

        /// <summary>
        /// 多条Json数据转换为泛型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonArr">字符窜（格式为[{a:'',b:''},{a:'',b:''},{a:'',b:''}]）</param>
        /// <returns></returns>
        public static List<T> ConvertTolist<T>(this string jsonArr)
        {
            if (!string.IsNullOrEmpty(jsonArr) && jsonArr.StartsWith("[") && jsonArr.EndsWith("]"))
            {
                Type t = typeof(T);
                var proPerties = t.GetProperties();
                List<T> list = new List<T>();
                string recive = jsonArr.Trim('[').Trim(']').Replace("'", "").Replace("\"", "");
                string[] reciveArr = recive.Replace("},{", "};{").Split(';');
                foreach (var item in reciveArr)
                {
                    T obj = ConvertToEntity<T>(item);
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
    }
}
