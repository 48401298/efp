using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Entity.MES;
using LAF.Data;
using System.Data;

namespace Manage.DAL.MES
{
    /// <summary>
    /// 要货信息数据层
    /// </summary>
    public class SupplyInfoDAL : BaseDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public SupplyInfo Get(SupplyInfo model)
        {
            SupplyInfo info = null;
            StringBuilder sqlBuilder = new StringBuilder();

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sqlBuilder.Append(@"SELECT si.*,fi.PNAME AS FACTORYNAME,pi.PNAME AS ProduceName,pl.PLNAME AS PLName
                FROM T_FP_SUPPLYINFO si 
                LEFT OUTER JOIN T_FP_ProducePlan pp ON  si.PLANID = pp.PID 
                LEFT OUTER JOIN T_FP_FACTORYINFO fi ON pp.FACTORYPID =fi.PID
                LEFT OUTER JOIN T_FP_PRODUCTINFO pi ON pp.PRODUCTIONID = pi.PID
                LEFT OUTER JOIN T_FP_PRODUCTLINE pl on pp.PRID = pl.PID
                where si.PID = @SUPPLYID");

                info = session.Get<SupplyInfo>(sqlBuilder.ToString(), new DataParameter("SUPPLYID", model.PID));
            }
            return info;
        }
        #endregion

        #region 获取列表

        /// <summary>
        /// 物料列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓位列表</returns>
        public List<SupplyMaterialInfo> GetMaterialList(string supplyID)
        {
            List<SupplyMaterialInfo> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT smi.PID,smi.MATRIALID,mat.MatName AS MATRIALNAME, smi.Amount,smi.Unit,mu.Description AS UNITNAME 
                FROM T_FP_SUPPLYMATERIALINFO smi
                LEFT OUTER JOIN T_WH_Mat mat ON smi.MATRIALID = mat.ID 
                LEFT OUTER JOIN T_WH_MatUnit mu ON smi.Unit = mu.ID 
                where SUPPLYID = @SUPPLYID ";
                parameters.Add(new DataParameter("SUPPLYID", supplyID));

                list = session.GetList<SupplyMaterialInfo>(sql, parameters.ToArray()).ToList<SupplyMaterialInfo>();
            }

            return list;
        }

        /// <summary>
        /// 根据产品bom获取要货明细
        /// </summary>
        /// <param name="pdID">产品主键</param>
        /// <returns>要货明细</returns>
        public List<SupplyMaterialInfo> GetMaterialListByBOM(string pdID)
        {
            List<SupplyMaterialInfo> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT bd.MATRIALID,mat.MatName AS MATRIALNAME, bd.Amount,bd.Unit,mu.Description AS UNITNAME 
                    FROM t_fp_producebom pb,T_FP_BOMDETAIL bd   
                    LEFT OUTER JOIN T_WH_Mat mat ON bd.MATRIALID = mat.ID 
                    LEFT OUTER JOIN T_WH_MatUnit mu ON bd.Unit = mu.ID 
                    WHERE bd.BOMID = pb.PID AND pb.PRODUCEID = @PRODUCEID";
                parameters.Add(new DataParameter("PRODUCEID", pdID));

                list = session.GetList<SupplyMaterialInfo>(sql, parameters.ToArray()).ToList<SupplyMaterialInfo>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(SupplyInfo condition, DataPage page)
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
                    page = session.GetDataPage<SupplyInfo>(sql, parameters.ToArray(), page);
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
        private string GetQuerySql(SupplyInfo condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT si.PID,si.UPDATETIME,si.BatchNumber,si.DELIVERYDATE,fi.PNAME AS FACTORYNAME,
                    pi.PNAME AS ProduceName,pl.PLNAME AS PLNAME,wh.Description as WarehouseName
                    FROM T_FP_SUPPLYINFO si 
                    LEFT OUTER JOIN T_FP_ProducePlan pp ON  si.PLANID = pp.PID 
                    LEFT OUTER JOIN T_FP_FACTORYINFO fi ON pp.FACTORYPID =fi.PID
                    LEFT OUTER JOIN T_FP_PRODUCTINFO pi ON pp.PRODUCTIONID = pi.PID
                    LEFT OUTER JOIN T_FP_PRODUCTLINE pl on pp.PRID = pl.PID
                    LEFT OUTER JOIN T_WH_Warehouse wh on si.Warehouse=wh.ID");

                if (string.IsNullOrEmpty(condition.BATCHNUMBER) == false)
                {
                    whereBuilder.Append(" AND si.BatchNumber like @BatchNumber");
                    parameters.Add(new DataParameter("BatchNumber", "%" + condition.BATCHNUMBER + "%"));
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
        public bool ExistsMaterial(SupplyMaterialInfo model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_FP_SUPPLYMATERIALINFO");
                whereBuilder.Append(" AND PID <> @PID ");
                parameters.Add(new DataParameter { ParameterName = "PID", DataType = DbType.String, Value = model.PID });
                if (!string.IsNullOrEmpty(model.MATRIALID))
                {
                    whereBuilder.Append(" AND MATRIALID = @MATRIALID ");
                    parameters.Add(new DataParameter { ParameterName = "MATRIALID", DataType = DbType.String, Value = model.MATRIALID });
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
        public int Insert(SupplyInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    //插入基本信息
                    count = session.Insert<SupplyInfo>(model);
                    foreach (SupplyMaterialInfo detail in model.Details)
                    {
                        detail.PID = Guid.NewGuid().ToString();
                        detail.SUPPLYID = model.PID;
                        session.Insert<SupplyMaterialInfo>(detail);
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
        public int Update(SupplyInfo model)
        {
            int count = 0;
            string sql = "delete from T_FP_SUPPLYMATERIALINFO where SUPPLYID = @SUPPLYID";
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    //更新BOM信息
                    count = session.Update<SupplyInfo>(model);
                    //删除BOM明细信息
                    session.ExecuteSql(sql, new DataParameter("SUPPLYID", model.PID));
                    foreach (SupplyMaterialInfo detail in model.Details)
                    {
                        detail.PID = Guid.NewGuid().ToString();
                        detail.SUPPLYID = model.PID;
                        session.Insert<SupplyMaterialInfo>(detail);
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
        public int Delete(SupplyInfo model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除BOM信息
                    count = session.Delete<SupplyInfo>(model);
                    //删除BOM明细信息
                    string sql = "delete from T_FP_SUPPLYMATERIALINFO where SUPPLYID = @SUPPLYID";
                    session.ExecuteSql(sql, new DataParameter("SUPPLYID", model.PID));
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
