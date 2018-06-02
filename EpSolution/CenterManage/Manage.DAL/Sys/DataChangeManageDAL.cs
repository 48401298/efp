using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data;
using LAF.Data.Attributes;
using Manage.Entity.Sys;
using System.Reflection;

namespace Manage.DAL.Sys
{
    /// <summary>
    /// 数据变更痕迹管理
    /// </summary>
    public class DataChangeManageDAL
    {
        /// <summary>
        /// 数据会话
        /// </summary>
        public IDataSession Session { get; set; }

        #region 获取数据变更列表

        /// <summary>
        /// 获取数据变更列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="page">分页设置</param>
        /// <returns>数据页</returns>
        public DataPage GetList(DataMark condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                //获取查询语句
                sql = this.GetQuerySql(condition, ref parameters);

                //分页关键字段及排序
                page.KeyName = "USERID";
                if (string.IsNullOrEmpty(page.SortExpression) == true)
                    page.SortExpression = "OPERATETIME DESC";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<DataMark>(sql, parameters.ToArray(), page);
                }

                return page;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 获取查询条件

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询语句</returns>
        private string GetQuerySql(DataMark condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();

            try
            {
                sqlBuilder.Append("SELECT T1.MARKID,T1.OPERATETIME,T2.USERDES AS OPERATEUSER,T1.OPERATETYPE,T3.COMMENTS AS DATAKIND ");
                sqlBuilder.Append("FROM T_BD_DATAMARK T1 ");
                sqlBuilder.Append("left JOIN T_QM_USERINFO T2 ON t1.OPERATEUSER=T2.USERID ");
                sqlBuilder.Append("left JOIN ALL_TAB_COMMENTS T3 ON T3.OWNER='PLP' AND T1.DATAKIND=T3.TABLE_NAME ");

                if (string.IsNullOrEmpty(condition.STARTOPERATETIME)==false)
                {
                    whereBuilder.Append(" AND T1.OPERATETIME >= :DATE1");
                    parameters.Add(new DataParameter("DATE1", DateTime.Parse(condition.STARTOPERATETIME)));
                }

                if (string.IsNullOrEmpty(condition.ENDOPERATETIME)==false)
                {
                    whereBuilder.Append(" AND T1.OPERATETIME <= :DATE2");
                    parameters.Add(new DataParameter("DATE2",DateTime.Parse(condition.ENDOPERATETIME).AddDays(1).AddSeconds(-1)));
                }

                if (string.IsNullOrEmpty(condition.OPERATEUSER) == false)
                {
                    whereBuilder.Append(" AND (T2.USERNAME LIKE :OPERATEUSER OR T2.USERDES LIKE :OPERATEUSER)");
                    parameters.Add(new DataParameter("OPERATEUSER", "%"+condition.OPERATEUSER+"%"));
                }

                if (string.IsNullOrEmpty(condition.DATAKIND) == false)
                {
                    whereBuilder.Append(" AND T1.DATAKIND=:DATAKIND");
                    parameters.Add(new DataParameter("DATAKIND",condition.DATAKIND));
                }

                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
                }

                return sqlBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 记录数据变更新信息

        /// <summary>
        /// 记录数据变更新信息
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="oprType">操作类型</param>
        /// <param name="OperateUser">操作者</param>
        /// <param name="oldInfo">原数据</param>
        /// <param name="newInfo">新数据</param>
        public void RecordDataChangeMark<T>(DataOprType oprType,string OperateUser,T oldInfo,T newInfo) where T:new()
        {
            string tableName = "";
            DataMark mark = new DataMark();
            try
            {
                Type type = typeof(T);

                //获取表名
                object[] attrsClassAtt = type.GetCustomAttributes(typeof(DBTableAttribute), true);
                DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                tableName = tableAtt.TableName;

                //创建痕迹信息
                mark.MARKID = Guid.NewGuid().ToString();
                mark.OPERATEUSER = OperateUser;
                mark.DATAKIND = tableName;
                mark.OPERATETIME = DateTime.Now;
                mark.OPERATETYPE = oprType.ToString();
                mark.DATAID = "";

                mark.ORIGINALDATA = LAF.Common.Serialization.JsonConvertHelper.GetSerializes(oldInfo);

                if (oprType == DataOprType.Update)
                {
                    mark.CHANGEDDATA = LAF.Common.Serialization.JsonConvertHelper.GetSerializes(newInfo);
                }                        

                //保存痕迹信息
                Session.Insert<DataMark>(mark);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 获取数据变更痕迹数据

        /// <summary>
        /// 获取数据变更痕迹数据
        /// </summary>
        /// <param name="info">变更痕迹</param>     
        /// <returns>变更痕迹</returns>
        public DataMark GetInfo(DataMark info,ref DataTable columnDt)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    sqlBuilder.Append("SELECT T1.MARKID,T1.OPERATETIME,T2.USERDES AS OPERATEUSER,T1.OPERATETYPE,T1.DATAKIND,T3.COMMENTS AS DATAKINDDES,T1.OriginalData,T1.ChangedData ");
                    sqlBuilder.Append("FROM T_BD_DATAMARK T1 ");
                    sqlBuilder.Append("left JOIN T_QM_USERINFO T2 ON t1.OPERATEUSER=T2.USERID ");
                    sqlBuilder.Append("left JOIN ALL_TAB_COMMENTS T3 ON T3.OWNER='PLP' AND T1.DATAKIND=T3.TABLE_NAME ");
                    sqlBuilder.Append("WHERE T1.MARKID=:MARKID");
                    //获取变更痕迹信息 
                    info = session.Get<DataMark>(sqlBuilder.ToString(), new DataParameter("MARKID",info.MARKID));

                    //获取字段描述信息
                    if (columnDt != null)
                    {
                        sqlBuilder.Clear();
                        sqlBuilder.Append("SELECT T1.COLUMN_NAME AS COLUMNNAME, T1.COMMENTS AS COLUMNDES ");
                        sqlBuilder.Append("FROM USER_COL_COMMENTS T1, ALL_TAB_COMMENTS T2 ");
                        sqlBuilder.Append("WHERE T1.TABLE_NAME = T2.TABLE_NAME  AND T1.TABLE_NAME = :TABLENAME");
                        parameters.Add(new DataParameter("TABLENAME",info.DATAKIND));

                        session.FillTable(columnDt, sqlBuilder.ToString(), parameters.ToArray());
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
