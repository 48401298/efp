using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using LAF.Data;
using LAF.Data.Attributes;
using LAF.Common.Util;
using Manage.Entity;
using Manage.DAL.Sys;
using Manage.Entity.Sys;
using LAF.Entity;

namespace Manage.DAL
{
    /// <summary>
    /// 数据层基类 
    /// 作    者：李炳海
    /// 编写日期：2015年01月07日
    /// </summary>
    public class BaseDAL
    {
        /// <summary>
        /// 数据会话
        /// </summary>
        public IDataSession BaseSession = null;

        /// <summary>
        /// 登录信息
        /// </summary>
        public LoginInfo LoginUser { get; set; }

        #region 逻辑删除

        /// <summary>
        ///  逻辑删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="info">实体</param>
        /// <returns>删除行数</returns>
        public int LogicDelete<T>(T info) where T : new()
        {
            int count = 0;
            string tableName = "";
            string sql = "";
            string where = "";
            string UPDATEUSR = "";
            T oldInfo = default(T);
            DataChangeManageDAL markManager = new DataChangeManageDAL();
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                //获取表名
                Type type = typeof(T);
                object[] attrsClassAtt = type.GetCustomAttributes(typeof(DBTableAttribute), true);
                DBTableAttribute tableAtt = (DBTableAttribute)attrsClassAtt[0];
                tableName = tableAtt.TableName;

                //获取主键信息
                Dictionary<string, object> pkColumns = new DataQueryHelper().GetPkColumns<T>(info);

                UPDATEUSR = BindHelper.GetPropertyValue(info, "UPDATEUSER") == null ? "" : BindHelper.GetPropertyValue(info, "UPDATEUSER").ToString();

                //获取原实体
                oldInfo = (T)(info as BaseEntity).Clone();
                oldInfo = this.BaseSession.Get<T>(info);

                //获取数据主键
                string dataID = this.GetEntityDataID<T>(info);
                
                //逻辑删除
                sql = string.Format("UPDATE {0} SET FLGDEL='1',UPDATEDATE=GETDATE(),UPDATEUSER=@UPDATEUSER WHERE ", tableName, UPDATEUSR);

                parameters.Add(new DataParameter("UPDATEUSER", UPDATEUSR));

                foreach (string key in pkColumns.Keys)
                {
                    where += " AND " + key + " = @" + key;
                    parameters.Add(new DataParameter(key, pkColumns[key]));
                }

                sql += where.Substring(4);

                sql = this.ChangeSqlByDB(sql, this.BaseSession);
                count = this.BaseSession.ExecuteSql(sql, parameters.ToArray());

                //记录痕迹
                markManager.Session = this.BaseSession;
                //markManager.RecordDataChangeMarkDetail<T>(DataOprType.Delete, UPDATEUSR, dataID, oldInfo, info);


                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 变更痕迹保留

        /// <summary>
        /// 新增痕迹保留
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="info">实体信息</param>
        /// <returns>影响行数</returns>
        public int InsertWithMark<T>(T info) where T : new()
        {
            int count = 0;
            DataChangeManageDAL markManager = new DataChangeManageDAL();
            try
            {
                //获取操作人
                object oprUserValue = LAF.Common.Util.BindHelper.GetPropertyValue(info, "CREATEUSER");

                string oprUser = oprUserValue == null ? "" : oprUserValue.ToString();

                //获取数据主键
                string dataID = this.GetEntityDataID<T>(info);

                //插入记录
                count=this.BaseSession.Insert<T>(info);

                //记录痕迹
                markManager.Session = this.BaseSession;
                //markManager.RecordDataChangeMark<T>(DataOprType.Insert,oprUser,dataID, default(T), info);

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新痕迹保留
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="info">实体信息</param>
        /// <returns>影响行数</returns>
        public int UpdateWithMark<T>(T info) where T : new()
        {
            int count = 0;
            T oldInfo = default(T);
            DataChangeManageDAL markManager = new DataChangeManageDAL();
            try
            {
                //获取原实体
                oldInfo = (T)(info as BaseEntity).Clone();
                oldInfo = this.BaseSession.Get<T>(oldInfo);
                
                //获取数据主键
                string dataID = this.GetEntityDataID<T>(oldInfo);

                //更新
                count = this.BaseSession.Update<T>(info);

                //获取操作人
                object oprUserValue = LAF.Common.Util.BindHelper.GetPropertyValue(info, "UPDATEUSER");

                string oprUser = oprUserValue == null ? "" : oprUserValue.ToString();

                //记录痕迹
                markManager.Session = this.BaseSession;
                //markManager.RecordDataChangeMark<T>(DataOprType.Update, oprUser, dataID, oldInfo, info);

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除痕迹保留
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="info">实体信息</param>
        /// <returns>影响行数</returns>
        public int DeleteWithMark<T>(T info) where T : new()
        {
            int count = 0;
            List<DataParameter> parameters = new List<DataParameter>();
            T oldInfo = default(T);
            DataChangeManageDAL markManager = new DataChangeManageDAL();
            try
            {
                //获取原实体
                oldInfo = (T)(info as BaseEntity).Clone();
                oldInfo = this.BaseSession.Get<T>(info);

                //获取数据主键
                string dataID = this.GetEntityDataID<T>(oldInfo);

                //删除
                count = this.BaseSession.Delete<T>(info);

                //获取操作人
                object oprUserValue = LAF.Common.Util.BindHelper.GetPropertyValue(info, "UPDATEUSER");

                string oprUser = oprUserValue == null ? "" : oprUserValue.ToString();

                //记录痕迹
                markManager.Session = this.BaseSession;
                //markManager.RecordDataChangeMark<T>(DataOprType.Delete, oprUser, dataID, oldInfo, info);

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Sql转换

        /// <summary>
        /// Sql转换
        /// </summary>
        /// <param name="sql">转换前Sql</param>
        /// <param name="session">session</param>
        /// <returns>转换后sql</returns>
        public string ChangeSqlByDB(string sql, IDataSession session)
        {
            return sql.Replace("@", session.DbHelper.GetParameterPrefix());
        }

        #endregion

        #region 获取数据库时间

        /// <summary>
        /// 获取数据库时间
        /// </summary>
        /// <returns>数据库时间</returns>
        public DateTime GetDbNowTime()
        {
            DateTime nowTime;
            string sql = null;
            try
            {
                sql = "SELECT GETDATE()";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    nowTime = (DateTime)session.ExecuteSqlScalar(sql);
                }

                return nowTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取权限过滤语句

        /// <summary>
        /// 获取权限过滤语句
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="tblName">表名(别名)</param>
        /// <param name="indexName">关联字段</param>
        /// <returns>过滤语句</returns>
        public string GetFilterByPowerSql<T>(string tblName, string indexName)
        {
            return this.GetFilterByPowerSql<T>(tblName, indexName, this.LoginUser);
        }

        /// <summary>
        /// 获取权限过滤语句
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="tblName">表名(别名)</param>
        /// <param name="indexName">关联字段</param>
        /// <param name="userID">用户主键</param>
        /// <returns>过滤语句</returns>
        public string GetFilterByPowerSql<T>(string tblName, string indexName, string userID)
        {
            return this.GetFilterByPowerSql<T>(tblName, indexName, new LoginInfo { UserID = userID });
        }

        /// <summary>
        /// 获取权限过滤语句
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="tblName">表名(别名)</param>
        /// <param name="indexName">关联字段</param>
        /// <param name="login">登录信息</param>
        /// <returns>过滤语句</returns>
        public string GetFilterByPowerSql<T>(string tblName, string indexName, LoginInfo login)
        {
            string where = "";

            ////判断是否为管理员
            //if (!string.IsNullOrEmpty(login.IsAdmin) && login.IsAdmin.ToLower() == "true"
            //    &&login.LimitSuperPower==false)
            //{
            //    return where;
            //}

            ////权限过滤语句
            //switch (indexName.ToUpper())
            //{
            //    case "ORGANID":
            //        where = string.Format("SELECT ORGAID FROM T_QM_USERORGAIZATION WHERE  USERID = '{0}' AND ORGAID=" + tblName + "." + indexName, login.UserID);
            //        break;
            //    default:
            //        where = string.Format("SELECT ORGAID FROM T_QM_USERORGAIZATION WHERE  USERID = '{0}' AND ORGAID=" + tblName + "." + indexName, login.UserID);
            //        break;
            //}

            //if (where != "")
            //{
            //    where = " AND EXISTS(" + where + ")";
            //}

            return where;
        }

        #endregion

        #region 获取数据主键

        /// <summary>
        /// 获取数据主键
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="info">实体</param>
        /// <returns>数据主键</returns>
        public string GetEntityDataID<T>(T info)
        {
            string dataID = "";
            string whereSql = "";
            List<DataParameter> parameters = new List<DataParameter>();

            ((DataSession)this.BaseSession).GetSingleWhere<T>(info, out whereSql, out parameters);

            if (parameters.Count > 0)
            {
                if (parameters[0].Value!=null)
                    dataID = parameters[0].Value.ToString();
            }

            return dataID;
        }

        #endregion

    }
}
