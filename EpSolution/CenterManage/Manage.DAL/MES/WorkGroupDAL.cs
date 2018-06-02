using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.MES;
using System.Data;

namespace Manage.DAL.MES
{
    /// </summary>
    ///　模块名称：班组信息
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    public class WorkGroupDAL : BaseDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public WorkGroup Get(WorkGroup model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<WorkGroup>(model);
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
        /// <returns>列表</returns>
        public List<WorkGroup> GetList()
        {
            List<WorkGroup> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT PID,PNAME FROM T_FP_WORKGROUP ORDER BY PNAME";
                list = session.GetList<WorkGroup>(sql, new List<DataParameter>().ToArray()).ToList<WorkGroup>();
            }

            return list;
        }

        public List<WorkGroupRef> GetPList(string id)
        {
            List<WorkGroupRef> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT * FROM T_FP_WORKGROUPREF WHERE WOID='" + id + "'";
                list = session.GetList<WorkGroupRef>(sql, new List<DataParameter>().ToArray()).ToList<WorkGroupRef>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(WorkGroup condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "PID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "UPDATETIME DESC";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<WorkGroup>(sql, parameters.ToArray(), page);
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
        private string GetQuerySql(WorkGroup condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT t1.*,t2.PNAME AS FNAME FROM T_FP_WORKGROUP t1 LEFT OUTER JOIN T_FP_FACTORYINFO t2 on t1.FAID = t2.PID ");

                if (!string.IsNullOrEmpty(condition.PNAME))
                {
                    whereBuilder.Append(" AND t1.PNAME like @PNAME");
                    parameters.Add(new DataParameter("PNAME", "%" + condition.PNAME + "%"));
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
        public DataTable GetExportData(WorkGroup model)
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
        /// 判断名称或者编号是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsWorkGroup(WorkGroup model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_WORKGROUP");
                whereBuilder.Append(" AND PID <> @PID ");
                parameters.Add(new DataParameter { ParameterName = "PID", DataType = DbType.String, Value = model.PID });
                if (!string.IsNullOrEmpty(model.PNAME))
                {
                    whereBuilder.Append(" AND PNAME = @PNAME");
                    parameters.Add(new DataParameter { ParameterName = "PNAME", DataType = DbType.String, Value = model.PNAME });
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
        public int Insert(WorkGroup model)
        {
            int count = 0;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = "DELETE FROM T_FP_WORKGROUPREF WHERE WOID = @WOID";
                    parameters.Add(new DataParameter { ParameterName = "WOID", DataType = DbType.String, Value = model.PID });
                    session.ExecuteSqlScalar(sql, parameters.ToArray());
                    //插入基本信息
                    count = session.Insert<WorkGroup>(model);
                    foreach (WorkGroupRef detail in model.Details)
                    {
                        WorkGroupRef workGroupRef = new WorkGroupRef();
                        workGroupRef.PID = Guid.NewGuid().ToString();
                        workGroupRef.PEID = detail.PEID;
                        workGroupRef.WOID = model.PID;
                        session.Insert<WorkGroupRef>(workGroupRef);
                    }
                    
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 插入信息(单表)
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>插入行数</returns>
        public int InsertRef(WorkGroupRef model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<WorkGroupRef>(model);
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
        public int Update(WorkGroup model)
        {
            int count = 0;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<WorkGroup>(model);
                    string sql = "DELETE FROM T_FP_WORKGROUPREF WHERE WOID = @WOID";
                    parameters.Add(new DataParameter { ParameterName = "WOID", DataType = DbType.String, Value = model.PID });
                    session.ExecuteSqlScalar(sql, parameters.ToArray());
                    foreach (WorkGroupRef detail in model.Details)
                    {
                        WorkGroupRef workGroupRef = new WorkGroupRef();
                        workGroupRef.PID = Guid.NewGuid().ToString();
                        workGroupRef.PEID = detail.PEID;
                        workGroupRef.WOID = model.PID;
                        session.Insert<WorkGroupRef>(workGroupRef);
                    }
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
        public int Delete(WorkGroup model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除基本信息
                    count = session.Delete<WorkGroup>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 班组删除时校验是否在排班使用过
        /// <summary>
        /// 班组删除时校验是否在排班使用过
        /// </summary>
        /// <param name="pid"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool HasProducePlan(string pid)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_SCHEDULING WHERE WOID=@WOID");
                parameters.Add(new DataParameter { ParameterName = "WOID", DataType = DbType.String, Value = pid });
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

