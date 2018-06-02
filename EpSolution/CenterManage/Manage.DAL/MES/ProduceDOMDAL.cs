using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Entity.MES;
using LAF.Data;
using System.Data;

namespace Manage.DAL.MES
{
    public class ProduceDOMDAL:BaseDAL
    {
        #region 获取信息

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="produceID">产品主键</param>
        /// <returns>*信息</returns>
        public ProduceBOM GetByProduceID(string produceID)
        {
            ProduceBOM info = null;
            try
            {
                string sql = "select * from T_FP_PRODUCEBOM where PRODUCEID = @PRODUCEID";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    info = session.Get<ProduceBOM>(sql, new DataParameter("PRODUCEID", produceID));
                }
                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public ProduceBOM Get(ProduceBOM model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<ProduceBOM>(model);
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
        /// 根据产品BOM明细列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓位列表</returns>
        public List<BOMDetail> GetList(string bomId)
        {
            List<BOMDetail> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT bd.MATRIALID,mat.MatName AS MATRIALNAME,bd.Unit,bd.Amount,mu.Description AS UNITNAME FROM T_FP_BOMDETAIL bd 
LEFT OUTER JOIN T_WH_Mat mat ON bd.MATRIALID = mat.ID 
LEFT OUTER JOIN T_WH_MatUnit mu ON bd.Unit = mu.ID where BOMID = @BOMID order by BOMID";
                parameters.Add(new DataParameter("BOMID", bomId));

                list = session.GetList<BOMDetail>(sql, parameters.ToArray()).ToList<BOMDetail>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(ProduceBOM condition, DataPage page)
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
                    page = session.GetDataPage<ProduceBOM>(sql, parameters.ToArray(), page);
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
        private string GetQuerySql(ProduceBOM condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT pi.PNAME AS PRODUCENAME,pb.Amount,mu.Description AS MainUnit,pb.PID,pb.UPDATETIME FROM T_FP_PRODUCEBOM pb 
LEFT OUTER JOIN T_FP_PRODUCTINFO pi ON pb.PRODUCEID = pi.PID 
LEFT OUTER JOIN T_WH_MatUnit mu ON pb.MainUnit = mu.ID");

                if (string.IsNullOrEmpty(condition.PRODUCENAME) == false)
                {
                    whereBuilder.Append(" AND pi.PNAME like @PNAME");
                    parameters.Add(new DataParameter("PNAME", "%" + condition.PRODUCENAME + "%"));
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


        #region 信息是否重复
        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsBOM(ProduceBOM model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_PRODUCEBOM");
                whereBuilder.Append(" AND PID <> @PID ");
                parameters.Add(new DataParameter { ParameterName = "PID", DataType = DbType.String, Value = model.PID });
                if (!string.IsNullOrEmpty(model.PRODUCEID))
                {
                    whereBuilder.Append(" AND PRODUCEID = @PRODUCEID ");
                    parameters.Add(new DataParameter { ParameterName = "PRODUCEID", DataType = DbType.String, Value = model.PRODUCEID });
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
        public int Insert(ProduceBOM model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    //插入基本信息
                    count = session.Insert<ProduceBOM>(model);
                    foreach (BOMDetail detail in model.Details)
                    {
                        detail.PID = Guid.NewGuid().ToString();
                        detail.BOMID = model.PID;
                        session.Insert<BOMDetail>(detail);
                    }
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

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>更新行数</returns>
        public int Update(ProduceBOM model)
        {
            int count = 0;
            string sql = "delete from T_FP_BOMDETAIL where BOMID = @BOMID";
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    //更新BOM信息
                    count = session.Update<ProduceBOM>(model);
                    //删除BOM明细信息
                    session.ExecuteSql(sql, new DataParameter("BOMID", model.PID));
                    foreach (BOMDetail detail in model.Details)
                    {
                        detail.PID = Guid.NewGuid().ToString();
                        detail.BOMID = model.PID;
                        session.Insert<BOMDetail>(detail);
                    }
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

        #region 删除
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>删除个数</returns>
        public int Delete(ProduceBOM model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除BOM信息
                    count = session.Delete<ProduceBOM>(model);
                    //删除BOM明细信息
                    string sql = "delete from T_FP_BOMDETAIL where BOMID = @BOMID";
                    session.ExecuteSql(sql, new DataParameter("BOMID", model.PID));
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
