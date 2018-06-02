using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data;
using LAF.Data.Attributes;
using Manage.Entity.WH;

namespace Manage.DAL.WH
{
    /// <summary>
    ///　模块名称：货品规格管理
    ///　作    者：
    ///　编写日期：2017年07月06日
    /// </summary>
    public class WHMatSpecDAL : BaseDAL
    {

        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public WHMatSpec Get(WHMatSpec model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<WHMatSpec>(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取列表

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public List<WHMatSpec> GetList(WHMatSpec condition)
        {
            string sql = null;
            List<WHMatSpec> list = null;
            List<DataParameter> parameters = new List<DataParameter>();

            sql = "SELECT * FROM T_WH_MatSpec where MatID = @MatID order by UnitName";
            parameters.Add(new DataParameter("MatID", condition.MatID));

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<WHMatSpec>(sql, parameters.ToArray()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public DataPage GetList(WHMatSpec condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();

            sql = @"SELECT t1.*,t2.Description as ChangeUnitName FROM T_WH_MatSpec t1
                    left outer join T_WH_MatUnit t2 on t1.ChangeUnit = t2.ID
                    where MatID = @MatID";
            parameters.Add(new DataParameter("MatID", condition.MatID));

            //分页关键字段及排序
            page.KeyName = "ID";
            if (string.IsNullOrEmpty(page.SortExpression))
                page.SortExpression = "UnitName";
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                page = session.GetDataPage<WHMatSpec>(sql, parameters.ToArray(), page);
            }
            return page;
        }

        #endregion

        
        
        #region 信息是否重复

        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool Exists(WHMatSpec model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_WH_MatSpec");
                whereBuilder.Append(" AND ID <> @ID ");
                parameters.Add(new DataParameter { ParameterName = "ID", DataType = DbType.String, Value = model.ID });
                if (!string.IsNullOrEmpty(model.Description))
                {
                    whereBuilder.Append(" AND UnitName = @UnitName ");
                    parameters.Add(new DataParameter { ParameterName = "UnitName", DataType = DbType.String, Value = model.UnitName });
                }
                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
                }
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = this.ChangeSqlByDB(sqlBuilder.ToString(), session);
                    count = Convert.ToInt32(session.ExecuteSqlScalar(sql, parameters.ToArray()));
                }
                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 插入信息

        /// <summary>
        /// 插入信息(单表)
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>插入行数</returns>
        public int Insert(WHMatSpec model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<WHMatSpec>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新信息

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>更新行数</returns>
        public int Update(WHMatSpec model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新信息
                    count = session.Update<WHMatSpec>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name=""></param>
        /// <returns>删除个数</returns>
        public int Delete(WHMatSpec model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    count = session.Delete<WHMatSpec>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

}
