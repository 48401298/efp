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
    ///　模块名称：货品信息管理
    ///　作    者：
    ///　编写日期：2017年07月06日
    /// </summary>
    public class WHMatDAL : BaseDAL
    {

        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public WHMat Get(WHMat info)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    info = session.Get<WHMat>(info);
                }
                return info;
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
        public List<WHMat> GetList(WHMat condition)
        {
            List<WHMat> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT * FROM T_WH_Mat";
                list = session.GetList<WHMat>(sql, new List<DataParameter>().ToArray()).ToList<WHMat>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(WHMat condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "ID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "t1.UPDATETIME DESC";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<WHMat>(sql, parameters.ToArray(), page);
                }
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 判断是否被使用

        /// <summary>
        /// 判断是否被使用
        /// </summary>
        /// <param name="info">货品</param>
        /// <returns>true:已使用;false:未使用</returns>
        public bool IsUse(WHMat info)
        {
            int count = 0;
            string sql = null;

            sql = "select count(ID) from T_WH_InStockDetail where MatID = @MatID";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                count = int.Parse(session.ExecuteSqlScalar(sql, new DataParameter("MatID", info.ID)).ToString());
            }

            if (count > 0)
                return true;
            else
                return false;
        }

        #endregion


        #region 获取主计量单位

        /// <summary>
        /// 获取主计量单位
        /// </summary>
        /// <param name="matID">货品主键</param>
        /// <returns>主计量单位</returns>
        public MatUnit GetMainUnit(string matID)
        {
            MatUnit unit = null;
            string sql = null;

            sql = @"select u.ID,u.Description from T_WH_Mat m
                    inner join T_WH_MatUnit u on m.UnitCode =  u.ID
                    where m.ID = @MatID";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                unit=session.Get<MatUnit>(sql, new DataParameter("MatID", matID));
            }

            return unit;
        }

        #endregion

        #region 获取查询语句
        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询语句</returns>
        private string GetQuerySql(WHMat condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT t1.ID,t1.MatCode,t1.MatName,t2.Description as ProductType
                                    ,t3.Description as UnitCode,t4.Description as SpecCode
                                    FROM T_WH_Mat t1
                                    left outer join T_WH_MatType t2 on t1.ProductType=t2.ID
                                    left outer join T_WH_MatUnit t3 on t1.UnitCode=t3.ID
                                    left outer join T_WH_Spec t4 on t1.SpecCode=t4.ID
                                    ");
                
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
        public DataTable GetExportData(WHMat model)
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
                    dt.TableName = "Warehouse";
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
        public bool Exists(WHMat info)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_WH_Mat");
                whereBuilder.Append(" AND ID <> @ID ");
                parameters.Add(new DataParameter { ParameterName = "ID", DataType = DbType.String, Value = info.ID });
                if (!string.IsNullOrEmpty(info.MatCode))
                {
                    whereBuilder.Append(" AND MatCode = @MatCode ");
                    parameters.Add(new DataParameter { ParameterName = "MatCode", DataType = DbType.String, Value = info.MatCode });
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
        public int Insert(WHMat info)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<WHMat>(info);
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
        public int Update(WHMat info)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新信息
                    count = session.Update<WHMat>(info);
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
        public int Delete(WHMat info)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    count = session.Delete<WHMat>(info);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据货品编号获取货品信息

        /// <summary>
        /// 根据货品编号获取货品信息
        /// </summary>
        /// <param name="matCode">货品编号</param>
        /// <returns>货品信息</returns>
        public WHMat GetMatByMatCode(string matCode)
        {
            WHMat mat = null;

            string sql = null;

            sql = @"select t1.ID,t1.ID as MatID,t1.MatCode,t1.MatName,t3.Description as SpecCode,t4.Description as UnitName,t4.ID as UnitCode
                    from T_WH_Mat t1
                    left join T_WH_Spec t3 on t1.SpecCode=t3.ID
                    left join T_WH_MatUnit t4 on t1.UnitCode=t4.ID
                    where t1.MatCode = @MatCode";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = this.ChangeSqlByDB(sql, session);
                mat = session.Get<WHMat>(sql, new DataParameter("MatCode", matCode));
            }

            return mat;
        }

        #endregion       

        #region 条码管理

        /// <summary>
        /// 获取货品最新唯一识别码
        /// </summary>
        /// <param name="info">货品信息</param>
        public string GetNewIDCodeSeq(WHMat info)
        {
            string no = null;
            object value = null;
            string sql = "select max(Seq) from T_WH_MatIDCode where MatID = @MatID and BuildDate = @BuildDate";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = this.ChangeSqlByDB(sql, session);
                value = session.ExecuteSqlScalar(sql, new DataParameter("MatID", info.ID)
                    , new DataParameter("BuildDate", DateTime.Now.ToString("yyyyMMdd")));
            }

            if (value != null && value != System.DBNull.Value)
                no = value.ToString();

            return no;
        }

        /// <summary>
        /// 获取货品唯一识别码列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetMatIDCodeList(WHMat condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = "select * from T_WH_MatIDCode where MatID = @MatID";

                if (condition.IDCodeStatus == "0")
                {
                    sql += " and PrintCount = 0";
                }
                else
                {
                    sql += " and PrintCount > 0";
                }

                //分页关键字段及排序
                page.KeyName = "IDCode";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "PrintCount,BuildDate DESC,IDCode";

                parameters.Add(new DataParameter("MatID", condition.ID));

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    sql = this.ChangeSqlByDB(sql, session);
                    page = session.GetDataPage<MatIDCode>(sql, parameters.ToArray(), page);
                }
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存货品唯一识别码
        /// </summary>
        /// <param name="list">物料唯一识别码列表</param>
        public void SaveMatIDCode(List<MatIDCode> list)
        {
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.Insert<MatIDCode>(list);
            }
        }

        /// <summary>
        /// 根据货品唯一识别码获取货品信息
        /// </summary>
        /// <param name="matIDCode">唯一识别码</param>
        /// <returns>货品信息</returns>
        public WHMat GetMatByIDCode(string matIDCode)
        {
            WHMat mat = null;

            string sql = null;

            sql = @"select t1.ID,t1.ID as MatID,t1.MatCode,t1.MatName,t2.IDCode,t3.Description as SpecCode,t1.UnitCode,t4.Description as UnitName,
                    t2.MatSpec as MatSpecID
                    from T_WH_Mat t1
                    inner join T_WH_MatIDCode t2 on t1.ID=t2.MatID
                    left join T_WH_Spec t3 on t1.SpecCode=t3.ID
                    left join T_WH_MatUnit t4 on t1.UnitCode=t4.ID
                    where IDCode = @IDCode";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = this.ChangeSqlByDB(sql, session);
                mat = session.Get<WHMat>(sql, new DataParameter("IDCode",matIDCode));
            }

            return mat;
        }
        

        /// <summary>
        /// 记录货品唯一识别码打印次数
        /// </summary>
        /// <param name="idCodeList">识别码列表</param>
        public void SignMatIDCodePrintCount(List<string> idCodeList)
        {
            string sql = "update T_WH_MatIDCode set PrintCount=PrintCount+1 where IDCode = ?IDCode";
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.OpenTs();

                foreach (string idCode in idCodeList)
                {
                    session.ExecuteSql(sql, new DataParameter("IDCode",idCode));
                }

                session.CommitTs();
            }
        }

        /// <summary>
        /// 删除货品条码
        /// </summary>
        /// <param name="idcode"></param>
        public void DeleteMatIDCode(MatIDCode idcode)
        {
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.Delete<MatIDCode>(idcode);
            }
        }

        /// <summary>
        /// 更新条码生产日期
        /// </summary>
        /// <param name="info"></param>
        /// <param name="produceDate"></param>
        public void UpdateIDCodeProduceDate(MatIDCode info)
        {
            string sql = "";

            sql = "update T_WH_MatIDCode set ProduceDate = @ProduceDate where IDCode = @IDCode";
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.ExecuteSql(sql, new DataParameter("IDCode", info.IDCode), new DataParameter("ProduceDate", info.ProduceDate));
            }
        }

        #endregion

        

    }

}
