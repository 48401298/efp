using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Reflection;
using LAF.Data;
using LAF.Data.Attributes;
using LAF.BLL;
using LAF.Common.Util;
using LAF.Common.Serialization;
using Manage.Entity.Sys;
using Manage.DAL.Sys;

namespace Manage.BLL.Sys
{
    /// <summary>
    /// 数据变更痕迹管理
    /// </summary>
    public class DataChangeManageBLL:BaseBLL
    {
        #region 获取数据变更信息

        public DataMark GetInfo(DataMark info)
        {
            DataTable columnDt = new DataTable();
            object oldInfo=null;
            object newInfo=null;
            Type entityType;
            try
            {
                //获取数据
                info=new DataChangeManageDAL().GetInfo(info,ref columnDt);

                //获取实体类型
                entityType = this.GetEntityTypeByTable(info.DATAKIND);

                info.DATAKIND = info.DATAKINDDES;

                //json格式数据转换为实体
                if (string.IsNullOrEmpty(info.ORIGINALDATA) == false)
                {
                    oldInfo = JsonConvertHelper.GetDeserialize(entityType, info.ORIGINALDATA);
                }

                if (string.IsNullOrEmpty(info.CHANGEDDATA) == false)
                {
                    newInfo = JsonConvertHelper.GetDeserialize(entityType, info.CHANGEDDATA);
                }

                //生成字段明细数据
                PropertyInfo[] pArray = entityType.GetProperties();

                info.Details = new List<DataMarkDetail>();
                //获取字段信息
                foreach (var item in pArray)
                {
                    object[] attrs = item.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (attrs.Count() == 0)
                    {
                        continue;
                    }

                    DBColumnAttribute ca = (DBColumnAttribute)attrs[0];//字段属性

                    DataMarkDetail detail = new DataMarkDetail();

                    //原值
                    object oldValue=null;
                    if(oldInfo!=null)
                        oldValue=BindHelper.GetPropertyValue(oldInfo, item.Name);

                    detail.OldValue = oldValue==null?"":oldValue.ToString();

                    //变更后值
                    object newValue = null;
                    if (newInfo != null)
                        newValue = BindHelper.GetPropertyValue(newInfo, item.Name);

                    detail.NewValue = newValue == null ? "" : newValue.ToString();

                    DataRow[] rows=columnDt.Select("COLUMNNAME='"+ca.ColumnName+"'");
                    if(rows.Length>0)
                        detail.ColumnDes = rows[0]["COLUMNDES"].ToString();

                    if (string.IsNullOrEmpty(detail.ColumnDes) == true)
                        continue;

                    info.Details.Add(detail);
                }

                return info;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取数据变更列表

        /// <summary>
        /// 获取数据变更列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="page">分页设置</param>
        /// <returns>数据页</returns>
        public DataPage GetList(DataMark condition, DataPage page)
        {
            try
            {
                if (string.IsNullOrEmpty(condition.DATAKIND)==false)
                    condition.DATAKIND = this.GetTableName(condition.DATAKIND);
                return new DataChangeManageDAL().GetList(condition, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取实体对应数据表名称

        /// <summary>
        /// 获取实体对应数据表名称
        /// </summary>
        /// <param name="entityName">实体名</param>
        /// <returns>数据表名称</returns>
        private string GetTableName(string entityName)
        {
            string tableName = "";
            try
            {
                Type entityType = this.GetEntityType(entityName);
                //获取表信息
                object[] attrsClassAtt = entityType.GetCustomAttributes(typeof(DBTableAttribute), true);

                if (attrsClassAtt == null)
                {
                    throw new Exception("当前实体没有添加属性DBTableAttribute！");
                }
                DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                tableName = tableAtt.TableName;

                return tableName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取实体类型

        /// <summary>
        /// 获取实体类型
        /// </summary>
        /// <param name="entityName">实体名</param>
        /// <returns>实体类型</returns>
        private Type GetEntityType(string entityName)
        {
            try
            {
                Assembly asse = Assembly.GetAssembly(typeof(LAF.Entity.BaseEntity));
                Type[] types = asse.GetTypes();
                foreach (Type type in types)
                {
                    if (type.Name.EndsWith(entityName))
                    {
                        return type;
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception("不存在名为“QMAPP.Model.Dict." + entityName+"”的实体");
            }
        }

        /// <summary>
        /// 获取实体类型
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>实体类型</returns>
        private Type GetEntityTypeByTable(string tableName)
        {
            Type t=null;
            try
            {
                Assembly asse = Assembly.GetAssembly(typeof(LAF.Entity.BaseEntity));
                Type[] types = asse.GetExportedTypes();
                foreach (Type entityType in types)
                {

                    //获取表信息
                    object[] attrsClassAtt = entityType.GetCustomAttributes(typeof(DBTableAttribute), true);

                    if (attrsClassAtt != null && attrsClassAtt.Length>0)
                    {
                        DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                        if (tableAtt.TableName == tableName)
                        {
                            t = entityType;
                            break;
                        }
                    }
                    
                }

                if (t == null)
                {
                    throw new Exception("通过表名获取实体类型失败");
                }

                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
