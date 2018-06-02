using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using LAF.Data.DbHelper;

namespace LAF.Data
{
    /// <summary>
    /// 数据查询工具
    /// </summary>
    public class DataQueryHelper
    {
        /// <summary>
        /// 表属性列表
        /// </summary>
        private Dictionary<string, DBTableAttribute> _tables = new Dictionary<string, DBTableAttribute>();

        /// <summary>
        /// Sql工具
        /// </summary>
        public IDbHelper DbHelper { get; set; }

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">实体</param>
        public DataQueryHelper()
        {
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 获取表属性
        /// </summary>
        private DBTableAttribute GetTableAttribute(Type t)
        {
            Type QEntity = t;
            //获取表明
            object[] attrsClassAtt = QEntity.GetCustomAttributes(typeof(DBTableAttribute), true);

            if (attrsClassAtt.Count() == 0)
            {
                throw new Exception("此实体无法添加到数据库请添加(DBTableAttribute)");
            }
            DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];

            return tableAtt;

        }
        ///// <summary>
        ///// 获取查询字段
        ///// </summary>
        ///// <returns></returns>
        //private string ToSelectString()
        //{
        //    List<string> strSelect = new List<string>();
        //    Type QEntity = typeof(TModel);
        //    string _FieldName = string.Empty;
        //    //获取字段
        //    foreach (var item in QEntity.GetProperties())
        //    {
        //        object[] attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), true);
        //        var columnItem = string.Empty;
        //        if (attrs.Count() > 0)
        //        {
        //            DBColumnAttribute temp = (DBColumnAttribute)attrs[0];
        //            columnItem = temp.ColumnName;                  
        //        }

        //        if (!string.IsNullOrEmpty(columnItem))
        //        {
        //            strSelect.Add(columnItem);
        //        }               
        //    }

        //    return string.Join(",", strSelect.ToArray());
        //}

        #endregion

        #region 获取主键信息

        /// <summary>
        /// 获取主键信息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>主键信息</returns>
        public Dictionary<string, object> GetPkColumns<T>(T model) where T : new()
        {
            Dictionary<string, object> pkColumns = new Dictionary<string, object>();

            try
            {
                Type modelType = typeof(T);

                //获取字段属性
                foreach (var item in modelType.GetProperties())
                {
                    object[] attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (attrs.Count() == 0)
                    {
                        continue;
                    }

                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];//字段属性


                    if (ca.IsKey == true)
                    {
                        object value = item.GetValue(model, null);//获取值

                        if (this.DbHelper != null)
                        {
                            pkColumns.Add(this.DbHelper.GetDbObjectName(ca.ColumnName), value);

                        }
                        else
                        {
                            pkColumns.Add(ca.ColumnName, value);
                        }
                    }
                }

                return pkColumns;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取查询字段

        /// <summary>
        /// 获取查询字段
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>     
        /// <returns>以逗号间隔的查询字段</returns>
        public string GetSelectColumns<T>() where T : new()
        {
            List<string> columns = new List<string>();
            Type modelType = null;
            try
            {
                modelType = typeof(T);

                //获取字段属性
                foreach (var item in modelType.GetProperties())
                {
                    object[] attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (attrs.Count() == 0)
                    {
                        continue;
                    }

                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];//字段属性

                    if (this.DbHelper != null)
                    {
                        columns.Add(this.DbHelper.GetDbObjectName(ca.ColumnName));
                    }
                    else
                    {
                        columns.Add(ca.ColumnName);
                    }
                }

                return string.Join(",", columns.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取查询条件

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>查询条件</returns>
        public void GetWhere<T>(T model, out string whereSql, out List<DataParameter> parameters)
        {
            Type type = null;
            StringBuilder whereBuilder = new StringBuilder();

            try
            {
                //初始化
                whereSql = "";
                parameters = new List<DataParameter>();

                type = typeof(T);

                //构成条件
                foreach (var item in type.GetProperties())
                {
                    object[] attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (attrs.Count() == 0)
                    {
                        continue;
                    }

                    //字段属性
                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];

                    //获取值
                    object value = item.GetValue(model, null);


                    if (value == null)
                        continue;

                    if (value.GetType().ToString() == "System.DateTime" 
                        && ((DateTime)value == new DateTime()||value.ToString().Substring(0,4)=="0001"))
                    {
                        continue;
                    }



                    if (this.DbHelper != null)
                    {
                        whereBuilder.Append(string.Format(" and {0} = {1}{2}"
                                                    , this.DbHelper.GetDbObjectName(ca.ColumnName)
                                                    , this.DbHelper.GetParameterPrefix()
                                                    , ca.ColumnName));
                    }
                    else
                    {
                        whereBuilder.Append(string.Format(" and {0} = {1}{2}"
                            , ca.ColumnName, "@", ca.ColumnName));
                    }

                    parameters.Add(new DataParameter { ParameterName = ca.ColumnName, Value = value, DataType = ca.DataType });
                }

                whereSql = whereBuilder.ToString();

                if (whereSql != "")
                    whereSql = whereSql.Substring(4);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 添加查询条件

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <typeparam name="TModel">实体类型</typeparam>
        /// <param name="expression">查询条件</param>
        public void AddCondition<TModel>(Expression<Func<TModel, bool>> expression)
        {
            if (expression.Parameters.Count > 0)
            {
                this._tables.Add(expression.Parameters[0].Name, this.GetTableAttribute(typeof(TModel)));
            }
        }

        #endregion

    }
}
