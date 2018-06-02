using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LAF.Data;
using Manage.Entity.MES;

namespace Manage.DAL.MES
{
    /// <summary>
    ///　模块名称：设备管理
    ///　作    者：wanglu
    ///　编写日期：2017年07月15日
    /// </summary>
    public class EquipmentDAL : BaseDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public EquipmentInfo Get(EquipmentInfo model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<EquipmentInfo>(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public EquipmentInfo GetInfoByBarCode(string barCode)
        {

            List<EquipmentInfo> list = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT * FROM T_FP_EQUIPMENT WHERE BARCODE = @BARCODE";
                parameters.Add(new DataParameter("BARCODE", barCode));
                list = session.GetList<EquipmentInfo>(sql, parameters.ToArray()).ToList<EquipmentInfo>();
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
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public List<EquipmentInfo> GetList()
        {
            List<EquipmentInfo> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT PID,ECODE,ENAME FROM T_FP_EQUIPMENT ORDER BY ECODE";
                list = session.GetList<EquipmentInfo>(sql, new List<DataParameter>().ToArray()).ToList<EquipmentInfo>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(EquipmentInfo condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "t1.PID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "t1.ECODE";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<EquipmentInfo>(sql, parameters.ToArray(), page);
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
        private string GetQuerySql(EquipmentInfo condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT t1.* FROM T_FP_EQUIPMENT t1 ");

                if (!string.IsNullOrEmpty(condition.ECODE))
                {
                    whereBuilder.Append(" AND t1.ECODE like @ECODE");
                    parameters.Add(new DataParameter("ECODE", "%" + condition.ECODE + "%"));
                }

                if (!string.IsNullOrEmpty(condition.ENAME))
                {
                    whereBuilder.Append(" AND t1.ENAME like @ENAME");
                    parameters.Add(new DataParameter("ENAME", "%" + condition.ENAME + "%"));
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
        public DataTable GetExportData(EquipmentInfo model)
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
                    dt.TableName = "EquipmentInfo";
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
        /// 判断名称是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsEquipment(EquipmentInfo model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_EQUIPMENT");
                whereBuilder.Append(" AND PID <> @PID ");
                parameters.Add(new DataParameter { ParameterName = "PID", DataType = DbType.String, Value = model.PID });
                if (!string.IsNullOrEmpty(model.ECODE))
                {
                    whereBuilder.Append(" AND ECODE = @ECODE ");
                    parameters.Add(new DataParameter { ParameterName = "ECODE", DataType = DbType.String, Value = model.ECODE });
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
        public int Insert(EquipmentInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<EquipmentInfo>(model);
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
        public int Update(EquipmentInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<EquipmentInfo>(model);
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
        public int Delete(EquipmentInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除基本信息
                    count = session.Delete<EquipmentInfo>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设备删除时校验是否使用过
        /// <summary>
        /// 设备删除时校验是否使用过
        /// </summary>
        /// <param name="eqid"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool HasUsed(string eqid)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_EQUIPMENTREF WHERE EQID=@EQID");
                parameters.Add(new DataParameter { ParameterName = "EQID", DataType = DbType.String, Value = eqid });
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
