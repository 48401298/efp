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
    ///　模块名称：流程追踪
    ///　作    者：wanglu
    ///　编写日期：2017年09月15日
    /// </summary>
    public class ProduceTrackDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public ProduceTrack Get(ProduceTrack model)
        {

            List<ProduceTrack> list = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT T1.*,T2.PNAME,T3.BARCODE AS CBBARCODE,T3.ENAME AS CBNAME,T4.WSCODE,T5.PNAME AS GXNAME FROM T_FP_PRODUCETRACK T1
LEFT OUTER JOIN T_FP_PRODUCTINFO T2 ON T1.PRODUCTIONID = T2.PRID 
LEFT OUTER JOIN T_FP_EQUIPMENT T3 ON T1.EQUID = T3.PID 
LEFT OUTER JOIN T_FP_WORKSTATION T4 ON T1.WSID = T4.PID 
LEFT OUTER JOIN T_FP_PROCESSINFO T5 ON T1.WPID = T5.PID 
WHERE T1.PID =@PID";
                parameters.Add(new DataParameter("PID", model.PID));
                list = session.GetList<ProduceTrack>(sql, parameters.ToArray()).ToList<ProduceTrack>();
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
        public List<ProduceTrack> GetList()
        {
            List<ProduceTrack> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT PID,ECODE,ENAME FROM T_FP_PRODUCETRACK ORDER BY ECODE";
                list = session.GetList<ProduceTrack>(sql, new List<DataParameter>().ToArray()).ToList<ProduceTrack>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(ProduceTrack condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "t1.PID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "t1.UPDATETIME DESC";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<ProduceTrack>(sql, parameters.ToArray(), page);
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
        private string GetQuerySql(ProduceTrack condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT t1.* FROM T_FP_PRODUCETRACK t1");


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
        public DataTable GetExportData(ProduceTrack model)
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



        #region 插入信息
        /// <summary>
        /// 插入信息(单表)
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>插入行数</returns>
        public int Insert(ProduceTrack model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<ProduceTrack>(model);
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
        public int Update(ProduceTrack model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<ProduceTrack>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public List<OnProcessingInfo> GetOnProcessingList(string barCode)
        {

            List<OnProcessingInfo> list = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = @"SELECT T.PID,T.WORKINGSTARTTIME,T1.MATNAME,T2.ENAME,T3.PNAME AS PROCESSNAME,T4.PNAME AS PRODUCTNAME FROM T_FP_PRODUCETRACK T
LEFT OUTER JOIN T_WH_MAT T1 ON T1.ID = T.MATID 
LEFT OUTER JOIN T_FP_EQUIPMENT T2 ON T2.PID = T.EQUID 
LEFT OUTER JOIN T_FP_PROCESSINFO T3 ON T3.PID = T.WPID 
LEFT OUTER JOIN T_FP_PRODUCTINFO T4 ON T4.PID = T.PRODUCTIONID
WHERE STATUS ='0'";
                if (!string.IsNullOrEmpty(barCode))
                {
                    sql += " AND T2.BARCODE =@BARCODE";
                }
                parameters.Add(new DataParameter("BARCODE", barCode));
                list = session.GetList<OnProcessingInfo>(sql, parameters.ToArray()).ToList<OnProcessingInfo>();
            }
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }

        }

    }
}
