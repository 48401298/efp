using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.Sys;
using System.Data;
using LAF.Common.Encrypt;
using Manage.Entity.Inspect;

namespace Manage.DAL.Inspect
{
    /// <summary>
    /// 监测项目
    /// </summary>
    public class InspectItemDAL
    {
        #region 获取监测项目列表

        /// <summary>
        /// 获取监测项目列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(InspectItemEntity condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);

                //分页关键字段及排序
                page.KeyName = "Id";
                if (string.IsNullOrEmpty(page.SortExpression))
                {
                    page.SortExpression = "ItemCode ASC";
                }

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<InspectItemEntity>(sql, parameters.ToArray(), page);
                }

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取监测项目信息列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<InspectItemEntity> GetAllItemInfo(InspectItemEntity condition)
        {
            List<InspectItemEntity> itemInfo = null;
            string sql = null;
            List<DataParameter> parameters=new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    itemInfo = session.GetList<InspectItemEntity>(sql, parameters.ToArray()).ToList();
                }

                return itemInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="parameters">查询</param>
        /// <returns>查询语句</returns>
        private string GetQuerySql(InspectItemEntity condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();

            sqlBuilder.Append(" SELECT Id,ItemCode, ");
            sqlBuilder.Append(" ItemName,Unit,PointCount,Remark, ");
            sqlBuilder.Append(" CREATEUSER,CREATETIME,UPDATEUSER,UPDATETIME  ");
            sqlBuilder.Append(" FROM InspectItemInfo A ");

            //查询条件
            if (!string.IsNullOrEmpty(condition.ItemCode))
            {
                whereBuilder.Append(" AND ItemCode like @ItemCode");
                parameters.Add(new DataParameter { ParameterName = "ItemCode", DataType = DbType.String, Value = "%" + condition.ItemCode + "%" });
            }

            //查询条件
            if (string.IsNullOrEmpty(condition.ItemName) == false)
            {
                whereBuilder.Append(" AND ItemName like @ItemName");
                parameters.Add(new DataParameter { ParameterName = "ItemName", DataType = DbType.String, Value = "%" + condition.ItemName + "%" });
            }

            if (whereBuilder.Length > 0)
            {
                sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
            }

            return sqlBuilder.ToString();
        }

        #endregion

        #region 获取监测项目信息

        /// <summary>
        /// 获取监测项目信息
        /// </summary>
        /// <param name="itemInfo">条件</param>
        /// <returns>获取监测项目信息</returns>
        public InspectItemEntity Get(InspectItemEntity itemInfo)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {                    
                    sqlBuilder.Append(" SELECT Id,ItemCode, ");
                    sqlBuilder.Append(" ItemName,Unit,PointCount,Remark, ");
                    sqlBuilder.Append(" CREATEUSER,CREATETIME,UPDATEUSER,UPDATETIME  ");
                    sqlBuilder.Append(" FROM InspectItemInfo A WHERE Id = @Id ");

                    parameters.Add(new DataParameter { ParameterName = "Id", DataType = DbType.String, Value = itemInfo.Id });

                    itemInfo = session.Get<InspectItemEntity>(sqlBuilder.ToString(), parameters.ToArray());
                }

                return itemInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region 插入监测项目信息

        /// <summary>
        /// 插入监测项目信息
        /// </summary>
        /// <param name="itemInfo">监测项目信息</param>
        /// <returns>插入数</returns>
        public int Insert(InspectItemEntity itemInfo)
        {
            int count = 0;

            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    
                    //插入基本信息
                    session.Insert<InspectItemEntity>(itemInfo);

                    session.CommitTs();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 删除监测项目信息

        /// <summary>
        /// 删除监测项目信息
        /// </summary>
        /// <param name="itemInfo">监测项目信息</param>
        /// <returns>删除个数</returns>
        public int Delete(InspectItemEntity itemInfo)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //删除监测项目信息
                    string sql = "DELETE FROM InspectItemInfo WHERE Id=@Id";
                    count = session.ExecuteSql(sql, new DataParameter { ParameterName = "Id", DataType = DbType.String, Value = itemInfo.Id });
                   
                    session.CommitTs();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新监测项目信息

        /// <summary>
        /// 更新监测项目信息
        /// </summary>
        /// <param name="itemInfo">监测项目信息</param>
        /// <returns>更新个数</returns>
        public int Update(InspectItemEntity itemInfo)
        {
            int count = 0;

            try
            {
                using (var session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //更新基本信息
                    count = session.Update<InspectItemEntity>(itemInfo);

                    session.CommitTs();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 信息是否重复
        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsInspectItem(InspectItemEntity model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM inspectiteminfo");
                whereBuilder.Append(" AND Id <> @Id ");
                parameters.Add(new DataParameter { ParameterName = "Id", DataType = DbType.String, Value = model.Id });
                if (!string.IsNullOrEmpty(model.ItemCode))
                {
                    whereBuilder.Append(" AND ItemCode = @ItemCode ");
                    parameters.Add(new DataParameter { ParameterName = "ItemCode", DataType = DbType.String, Value = model.ItemCode });
                }
                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
                }
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    count = Convert.ToInt32(session.ExecuteSqlScalar(sqlBuilder.ToString(), parameters.ToArray()));
                }
                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
