using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace LAF.Common.Util
{
    /// <summary>
    /// 绑定工具
    /// </summary>
    public class BindHelper
    {
        #region 把业务对象绑定到DataTable


        /// <summary>
        /// 把一个业务对象的属性值绑定到一个DataRow中
        /// </summary>
        /// <param name="model">业务对象的实例</param>
        /// <param name="dr">一个DataRow</param>
        /// <param name="isThrow">是否抛出异常,True抛出，False不抛出</param>
        public static void BindDataTable(object model, DataRow dr, bool isThrow)
        {
            if (model == null || dr == null) return;

            //得到列名集合
            int columnCount = dr.Table.Columns.Count;
            string[] columnNames = new string[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                columnNames[i] = dr.Table.Columns[i].ColumnName;
            }
            //
            PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);


            foreach (PropertyInfo property in properties)
            {
                if (StringHelper.FindString(property.Name, columnNames, false) == false)
                    continue;
                try
                {
                    object value = property.GetValue(model, null);
                    if (value == null)
                        continue;
                    DataColumn dc = dr.Table.Columns[property.Name];
                    if (dc.DataType == typeof(System.DateTime) && value.ToString() == "")
                        continue;
                    //孙旭2005-6-2:增加对Indentity列的处理
                    if (dc.AutoIncrement || dc.ReadOnly)
                        continue;
                    //---------------------------
                    if (dc.DataType == typeof(System.DateTime) && value is string)
                        dr[property.Name] = DateTime.Parse(value.ToString());
                    else
                        dr[property.Name] = ChangeType(property.GetValue(model, null), dc.DataType);
                }
                catch (Exception ex)
                {
                    if (isThrow)
                        throw ex;
                    else
                        continue;
                }
            }


        }

        /// <summary>
        ///  把一个业务对象集合中业务对象的属性值分别绑定到DataTable中的数据行中，
        ///  本方法自动向DataTable中添加行
        /// </summary>
        /// <param name="list">业务对象集合</param>
        /// <param name="dt">DataTable的实例</param>
        /// <param name="isThrow">是否抛出异常,True抛出，False不抛出</param>
        public static void BindDataTable(IList list, DataTable dt, bool isThrow)
        {
            if (list.Count == 0)
                return;

            for (int i = 0; i < list.Count; i++)
            {
                DataRow row = dt.NewRow();
                BindDataTable(list[i], row, isThrow);
                dt.Rows.Add(row);
            }


        }
        #endregion 把业务对象绑定到DataTable

        #region	把DataTable中的数据绑定的业务对象集合中

        /// <summary>
        /// 把DataRow中的数据绑定到数据模型中
        /// </summary>
        /// <param name="model">业务对象的实例</param>
        /// <param name="dr">一个DataRow</param>
        /// <param name="isThrow">是否抛出异常,True抛出，False不抛出</param>
        public static void BindModelByDataTable(object model, DataRow dr, bool isThrow)
        {
            if (model == null || dr == null) return;

            //得到列名集合
            int columnCount = dr.Table.Columns.Count;
            string[] columnNames = new string[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                columnNames[i] = dr.Table.Columns[i].ColumnName;
            }
            //
            PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                if (StringHelper.FindString(property.Name, columnNames, false) == false)
                    continue;
                try
                {
                    if (!(dr[property.Name] is DBNull))
                        property.SetValue(model, ChangeType(dr[property.Name], property.PropertyType), null);
                }
                catch (Exception ex)
                {
                    if (isThrow)
                        throw ex;
                    else
                        continue;
                }
            }

        }

        /// <summary>
        ///  把DataTable中的数据绑定的业务对象集合中
        /// </summary>
        /// <param name="list">业务对象集合</param>
        /// <param name="modelType">业务对象的类型，形如typeof(System.DateTime)</param>
        /// <param name="dt">DataTable的实例</param>
        /// <param name="isThrow">是否抛出异常,True抛出，False不抛出</param>
        public static void BindModelByDataTable(IList list, System.Type modelType, DataTable dt, bool isThrow)
        {
            if (dt.Rows.Count == 0)
                return;

            Assembly assem = Assembly.Load(modelType.Assembly.FullName);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                object model = assem.CreateInstance(modelType.FullName, true);
                BindModelByDataTable(model, dt.Rows[i], isThrow);
                list.Add(model);
            }

        }
        #endregion

        #region 转换

        /// <summary>
        /// 将数据行转换成实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="row">数据行</param>
        /// <returns>实体</returns>
        public static T ConvertToModel<T>(DataRow row) where T : new()
        {
            //判断行是否为空
            if (row == null)
                return default(T);

            T model = new T();

            PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            //赋值
            foreach (PropertyInfo p in properties)
            {
                if (row.Table.Columns.IndexOf(p.Name) < 0)
                    continue;

                if (!(row[p.Name] is DBNull))
                    p.SetValue(model, ChangeType(row[p.Name], p.PropertyType), null);
            }

            return model;
        }

        private static object ChangeType(object value, Type conversionType)
        {
            if (null == value)
            {
                return conversionType;
            }
            if (!conversionType.IsGenericType)
            {
                return Convert.ChangeType(value, conversionType);
            }
            else
            {
                Type genericTypeDefinition = conversionType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return Convert.ChangeType(value, Nullable.GetUnderlyingType(conversionType));
                }
            }
            throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", value.GetType().FullName, conversionType.FullName));
        }

        /// <summary>
        /// 将数据表转换为实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <returns>实体列表</returns>
        public static IList<T> ConvertToList<T>(DataTable table) where T : new()
        {
            IList<T> list = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(ConvertToModel<T>(row));
            }

            return list;
        }



        #endregion

        #region 基本操作

        /// <summary>
        /// 从对象中取出指定属性的值
        /// </summary>
        /// <param name="target">实体</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>值</returns>
        public static object GetPropertyValue(object target, string propertyName)
        {
            PropertyInfo propertyInfo =
                target.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
                return null;

            object value = propertyInfo.GetValue(target, null);

            return value;
        }

        /// <summary>
        /// 把值设置到对象中
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        public static void SetPropertyValue(object target, string propertyName, object value)
        {
            PropertyInfo propertyInfo =
                target.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
                return;

            if (value == null)
                propertyInfo.SetValue(target, value, null);
            else
            {
                Type pType = propertyInfo.PropertyType;
                Type vType = value.GetType();
                if (pType.Equals(vType))
                {
                    // types match, just copy value
                    propertyInfo.SetValue(target, value, null);
                }
                else
                {
                    // types don't match, try to coerce
                    if (pType == typeof(Guid))
                        propertyInfo.SetValue(
                                target, new Guid(value.ToString()), null);
                    else if (pType.IsEnum && vType == typeof(string))
                        propertyInfo.SetValue(target, Enum.Parse(pType, value.ToString()), null);
                    //modify by mayi 2010-10-15 日期类型转换为字符串赋值

                    else if (pType == typeof(string) && vType == typeof(DateTime))
                        propertyInfo.SetValue(target, value.ToString(), null);
                    //modify end
                    else
                        propertyInfo.SetValue(
                                target, value, null);
                }
            }
        }

        /// <summary>
        /// 判断对象中是否存在指定属性
        /// </summary>
        /// <param name="target">对象</param>
        /// <param name="propertyName">指定属性名</param>
        /// <returns>true:存在；false:不存在</returns>
        public static bool IsValidProperty(object target, string propertyName)
        {
            PropertyInfo propertyInfo =
                target.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
                return false;
            else
                return true;
        }

        #endregion

        #region 对象复制

        /// <summary>
        ///  拷贝原类对象到目标类
        /// </summary>
        /// <typeparam name="T">目标类类型</typeparam>
        /// <typeparam name="TModel">原类类型</typeparam>
        /// <param name="model">原类类型对象</param>
        /// <returns>目标类</returns>
        public static T CopyToModel<T, TModel>(TModel model) where T : new()
        {
            return CopyToModel<T, TModel>(model, false);
        }

        /// <summary>
        ///  拷贝原类对象到目标类
        /// </summary>
        /// <typeparam name="T">目标类类型</typeparam>
        /// <typeparam name="TModel">原类类型</typeparam>
        /// <param name="model">原类类型对象</param>
        /// <returns>目标类</returns>
        public static T CopyToModel<T, TModel>(TModel model, bool formatDictionary) where T : new()
        {
            try
            {
                T myModel = new T();

                ObjectsMapper<TModel, T> mapper = ObjectMapperManager.DefaultInstance.GetMapper<TModel, T>(
                 new DefaultMapConfig().ConvertUsing<String, DateTime>(o => String.IsNullOrEmpty(o) ? new DateTime() : Convert.ToDateTime(o)));

                myModel = mapper.Map(model);

                return myModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 复制对象中值到目标对象
        /// </summary>
        /// <typeparam name="T">目标类类型</typeparam>
        /// <typeparam name="TModel">原类类型</typeparam>
        /// <param name="model">原类类型对象</param>
        /// <param name="entity">目标对象</param>
        /// <returns>目标类</returns>
        public static T CopyToModel<T, TModel>(TModel model,T entity, bool formatDictionary) where T : new()
        {
            try
            {
                ObjectsMapper<TModel, T> mapper = ObjectMapperManager.DefaultInstance.GetMapper<TModel, T>(
                 new DefaultMapConfig().ConvertUsing<String, DateTime>(o => String.IsNullOrEmpty(o) ? new DateTime() : Convert.ToDateTime(o)));

                entity = mapper.Map(model);

                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

    }
}
