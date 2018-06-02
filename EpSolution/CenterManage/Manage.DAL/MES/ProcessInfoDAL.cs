using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using Manage.Entity.MES;
using LAF.Data;
using System.Data;

namespace Manage.DAL.MES
{
    /// <summary>
    ///　模块名称：工序信息
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    public class ProcessInfoDAL : BaseDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public ProcessInfo Get(ProcessInfo model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<ProcessInfo>(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据工位条码获取加工工序
        /// </summary>
        /// <param name="wscode"></param>
        /// <returns></returns>
        public ProcessInfo GetInfoByWS(string wscode)
        {
            List<ProcessInfo> list = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "select t3.*,t1.PID AS GWID from T_FP_WORKSTATION t1,T_FP_STATIONREF t2 ,T_FP_PROCESSINFO t3 "
                    + " WHERE t1.PID = t2.STID AND t2.PRID = t3.PID ANd t1.WSCODE =@WSCODE";
                parameters.Add(new DataParameter("WSCODE", wscode));
                list = session.GetList<ProcessInfo>(sql, parameters.ToArray()).ToList<ProcessInfo>();
            }
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public ProcessInfo GetInfoByBarCodeAndBatchNumber(string wscode, string batchNumber)
        {
            List<ProcessInfo> list = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT T3.* FROM T_FP_EQUIPMENT T1,T_FP_EQUIPMENTREF T2 ,T_FP_PROCESSINFO T3,T_FP_PRODUCEPLAN T4
WHERE T1.PID = T2.EQID AND T2.PRID = T3.PID AND T3.FACTORYPID = T4.FACTORYPID AND T4.PRID = T3.PRODUCTLINEPID 
AND T4.BATCHNUMBER =@BATCHNUMBER AND T1.BARCODE =@BARCODE";
                parameters.Add(new DataParameter("BATCHNUMBER", batchNumber));
                parameters.Add(new DataParameter("BARCODE", wscode));
                list = session.GetList<ProcessInfo>(sql, parameters.ToArray()).ToList<ProcessInfo>();
            }
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 获取列表
        public List<EquipmentRef> GetEList(string id)
        {
            List<EquipmentRef> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT t1.*,t2.ENAME AS EQNAME FROM T_FP_EQUIPMENTREF t1 LEFT OUTER JOIN T_FP_EQUIPMENT t2 ON t1.EQID = t2.PID WHERE t1.PRID='" + id + "'";
                list = session.GetList<EquipmentRef>(sql, new List<DataParameter>().ToArray()).ToList<EquipmentRef>();
            }

            return list;
        }

        public List<StationRef> GetSList(string id)
        {
            List<StationRef> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT t1.*,t2.WSNAME AS STNAME FROM T_FP_STATIONREF t1 LEFT OUTER JOIN T_FP_WORKSTATION t2 ON t1.STID = t2.PID WHERE PRID='" + id + "'";
                list = session.GetList<StationRef>(sql, new List<DataParameter>().ToArray()).ToList<StationRef>();
            }

            return list;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(ProcessInfo condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "PID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "PCODE";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<ProcessInfo>(sql, parameters.ToArray(), page);
                }
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<ProcessInfo> GetList(ProcessInfo condition)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            List<ProcessInfo> list = null;
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                sql += " order by PCODE";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    list = session.GetList<ProcessInfo>(sql, parameters.ToArray()).ToList();
                }
                return list;
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
        private string GetQuerySql(ProcessInfo condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT * FROM T_FP_PROCESSINFO t1 ");

                if (!string.IsNullOrEmpty(condition.PCODE))
                {
                    whereBuilder.Append(" AND t1.PCODE like @PCODE");
                    parameters.Add(new DataParameter("PCODE", "%" + condition.PCODE + "%"));
                }

                if (!string.IsNullOrEmpty(condition.PNAME))
                {
                    whereBuilder.Append(" AND t1.PNAME like @PNAME");
                    parameters.Add(new DataParameter("PNAME", "%" + condition.PNAME + "%"));
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
                if (!string.IsNullOrEmpty(condition.FLOWID))
                {
                    whereBuilder.Append(" AND t1.FLOWID = @FLOWID");
                    parameters.Add(new DataParameter("FLOWID", condition.FLOWID));
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
        public DataTable GetExportData(ProcessInfo model)
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
                    dt.TableName = "ProcessInfo";
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
        public bool ExistsProcessInfo(ProcessInfo model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_PROCESSINFO");
                whereBuilder.Append(" AND PID <> @PID ");
                parameters.Add(new DataParameter { ParameterName = "PID", DataType = DbType.String, Value = model.PID });
                if (!string.IsNullOrEmpty(model.PCODE))
                {
                    whereBuilder.Append(" AND (PCODE = @PCODE OR PNAME = @PNAME)");
                    parameters.Add(new DataParameter { ParameterName = "PCODE", DataType = DbType.String, Value = model.PCODE });
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
        public int Insert(ProcessInfo model)
        {
            int count = 0;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<ProcessInfo>(model);
                    string sql = "DELETE FROM T_FP_EQUIPMENTREF WHERE PRID = @PRID";
                    parameters.Add(new DataParameter { ParameterName = "PRID", DataType = DbType.String, Value = model.PID });
                    session.ExecuteSqlScalar(sql, parameters.ToArray());
                    if (model.Details!=null)
                    {
                        foreach (EquipmentRef detail in model.Details)
                        {
                            EquipmentRef eref = new EquipmentRef();
                            eref.PID = Guid.NewGuid().ToString();
                            eref.EQID = detail.EQID;
                            eref.PRID = model.PID;
                            session.Insert<EquipmentRef>(eref);
                        }
                    }
                    
                    string sql2 = "DELETE FROM T_FP_STATIONREF WHERE PRID = @PRID";
                    session.ExecuteSqlScalar(sql2, parameters.ToArray());
                    if (model.Details2 != null)
                    {
                        foreach (StationRef detail in model.Details2)
                        {
                            StationRef eref = new StationRef();
                            eref.PID = Guid.NewGuid().ToString();
                            eref.STID = detail.STID;
                            eref.PRID = model.PID;
                            session.Insert<StationRef>(eref);
                        }
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

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>更新行数</returns>
        public int Update(ProcessInfo model)
        {
            int count = 0;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<ProcessInfo>(model);
                    string sql = "DELETE FROM T_FP_EQUIPMENTREF WHERE PRID = @PRID";
                    parameters.Add(new DataParameter { ParameterName = "PRID", DataType = DbType.String, Value = model.PID });
                    session.ExecuteSqlScalar(sql, parameters.ToArray());
                    if (model.Details != null)
                    {
                        foreach (EquipmentRef detail in model.Details)
                        {
                            EquipmentRef eref = new EquipmentRef();
                            eref.PID = Guid.NewGuid().ToString();
                            eref.EQID = detail.EQID;
                            eref.PRID = model.PID;
                            session.Insert<EquipmentRef>(eref);
                        }
                    }
                    string sql2 = "DELETE FROM T_FP_STATIONREF WHERE PRID = @PRID";
                    session.ExecuteSqlScalar(sql2, parameters.ToArray());
                    if (model.Details2 != null)
                    {
                        foreach (StationRef detail in model.Details2)
                        {
                            StationRef eref = new StationRef();
                            eref.PID = Guid.NewGuid().ToString();
                            eref.STID = detail.STID;
                            eref.PRID = model.PID;
                            session.Insert<StationRef>(eref);
                        }
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
        public int Delete(ProcessInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除基本信息
                    count = session.Delete<ProcessInfo>(model);
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
