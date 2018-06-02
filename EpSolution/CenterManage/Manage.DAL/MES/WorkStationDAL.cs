using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using System.Data;
using Manage.Entity.MES;

namespace Manage.DAL.MES
{
    /// </summary>
    ///　模块名称：工位信息
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    public class WorkStationDAL : BaseDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public WorkStation Get(WorkStation model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<WorkStation>(model);
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
        public List<WorkStation> GetList()
        {
            List<WorkStation> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT PID,WSCODE,WSNAME FROM T_FP_WORKSTATION ORDER BY WSCODE";
                list = session.GetList<WorkStation>(sql, new List<DataParameter>().ToArray()).ToList<WorkStation>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(WorkStation condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "PID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "WSCODE";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<WorkStation>(sql, parameters.ToArray(), page);
                }
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取查询语句
        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询语句</returns>
        private string GetQuerySql(WorkStation condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT t1.*,t2.PNAME as FNAME,t3.PLNAME as PLNAME,t4.ENAME FROM T_FP_WORKSTATION t1 
                                  LEFT OUTER JOIN T_FP_FACTORYINFO t2 on t1.FACTORYPID = t2.PID 
                                  LEFT OUTER JOIN T_FP_PRODUCTLINE t3 on t1.PRODUCTLINEPID = t3.PID
                                  LEFT OUTER JOIN T_FP_EQUIPMENT t4 on t1.EQUIPMENTPID = t4.PID");

                if (!string.IsNullOrEmpty(condition.WSCODE))
                {
                    whereBuilder.Append(" AND t1.PCODE like @WSCODE");
                    parameters.Add(new DataParameter("WSCODE", "%" + condition.WSCODE + "%"));
                }

                if (!string.IsNullOrEmpty(condition.WSNAME))
                {
                    whereBuilder.Append(" AND t1.PNAME like @WSNAME");
                    parameters.Add(new DataParameter("WSNAME", "%" + condition.WSNAME + "%"));
                }

                if (!string.IsNullOrEmpty(condition.FACTORYPID))
                {
                    whereBuilder.Append(" AND t1.FACTORYPID = @FACTORYPID");
                    parameters.Add(new DataParameter("FACTORYPID", condition.FACTORYPID));
                }
                if (!string.IsNullOrEmpty(condition.PRODUCTLINEPID))
                {
                    whereBuilder.Append(" AND t1.PRODUCTLINEPID = @PRODUCTLINEPID");
                    parameters.Add(new DataParameter("PRODUCTLINEPID", condition.PRODUCTLINEPID));
                }
                //查询条件
                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
                }
                return sqlBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取导出的数据
        /// <summary>
        /// 获取导出的数据
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <returns>数据</returns>
        public DataTable GetExportData(WorkStation model)
        {
            DataTable dt = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                //构成查询语句
                sql = this.GetQuerySql(model, ref parameters);
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    dt = session.GetTable(sql, parameters.ToArray());
                    dt.TableName = "WorkGroup";
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 信息是否重复
        /// <summary>
        /// 判断编号是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsCode(WorkStation model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_WORKSTATION");
                whereBuilder.Append(" AND PID <> @PID ");
                parameters.Add(new DataParameter { ParameterName = "PID", DataType = DbType.String, Value = model.PID });
                if (!string.IsNullOrEmpty(model.WSCODE) || !string.IsNullOrEmpty(model.WSNAME))
                {
                    whereBuilder.Append(" AND WSCODE = @WSCODE ");
                    parameters.Add(new DataParameter { ParameterName = "WSCODE", DataType = DbType.String, Value = model.WSCODE });
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

        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsName(WorkStation model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_WORKSTATION");
                whereBuilder.Append(" AND PID <> @PID ");
                parameters.Add(new DataParameter { ParameterName = "PID", DataType = DbType.String, Value = model.PID });
                if (!string.IsNullOrEmpty(model.WSCODE) || !string.IsNullOrEmpty(model.WSNAME))
                {
                    whereBuilder.Append(" AND WSNAME = @WSNAME ");
                    parameters.Add(new DataParameter { ParameterName = "WSNAME", DataType = DbType.String, Value = model.WSNAME });
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

        #region 插入信息
        /// <summary>
        /// 插入信息(单表)
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>插入行数</returns>
        public int Insert(WorkStation model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<WorkStation>(model);
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
        public int Update(WorkStation model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<WorkStation>(model);
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
        /// 删除信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>删除个数</returns>
        public int Delete(WorkStation model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除基本信息
                    count = session.Delete<WorkStation>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 工位删除时校验是否使用过
        /// <summary>
        /// 工位删除时校验是否使用过
        /// </summary>
        /// <param name="pid"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool HasUsed(string stid)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_STATIONREF WHERE STID=@STID");
                parameters.Add(new DataParameter { ParameterName = "STID", DataType = DbType.String, Value = stid });
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

