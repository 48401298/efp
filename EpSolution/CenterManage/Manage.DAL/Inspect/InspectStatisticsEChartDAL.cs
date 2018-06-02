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
    /// 监测统计
    /// </summary>
    public class InspectStatisticsEChartDAL
    {
        #region 获取监测统计列表

        /// <summary>
        /// 获取监测统计列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<InspectResultEntity> GetList(InspectResultEntity condition)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                List<InspectResultEntity> resultList = null;
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    resultList = session.GetList<InspectResultEntity>(sql, parameters.ToArray()).ToList();
                }

                return resultList;
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
        private string GetQuerySql(InspectResultEntity condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();

            sqlBuilder.Append(" SELECT A.ID AS Id,A.DeviceCode,D.DeviceName,A.OrganID,O.ORGANDESC AS OrganDESC,A.UpdateTime,A.ResultType, ");
            sqlBuilder.Append(" A.ItemCode,T.ItemName,A.InspectTime,A.MaxDataValue,A.MinDataValue,A.AvgValue ");
            sqlBuilder.Append(" FROM inspectcalcresult A  ");
            sqlBuilder.Append(" LEFT JOIN deviceinfo D ON A.DeviceCode = D.DeviceCode ");
            sqlBuilder.Append(" LEFT JOIN inspectiteminfo T ON A.ItemCode = T.ItemCode ");
            sqlBuilder.Append(" LEFT JOIN t_organization O ON A.OrganID = O.OrganID ");

            //查询条件
            if (!string.IsNullOrEmpty(condition.DeviceCode))
            {
                whereBuilder.Append(" AND A.DeviceCode = @DeviceCode");
                parameters.Add(new DataParameter { ParameterName = "DeviceCode", DataType = DbType.String, Value = condition.DeviceCode });
            }

            if (string.IsNullOrEmpty(condition.ItemCode) == false)
            {
                whereBuilder.Append(" AND A.ItemCode = @ItemCode");
                parameters.Add(new DataParameter { ParameterName = "ItemCode", DataType = DbType.String, Value = condition.ItemCode });
            }

            if (string.IsNullOrEmpty(condition.DeviceType) == false)
            {
                whereBuilder.Append(" AND D.DeviceType = @DeviceType");
                parameters.Add(new DataParameter { ParameterName = "DeviceType", DataType = DbType.String, Value = condition.DeviceType });
            }

            if (string.IsNullOrEmpty(condition.ResultType) == false)
            {
                whereBuilder.Append(" AND A.ResultType = @ResultType");
                parameters.Add(new DataParameter { ParameterName = "ResultType", DataType = DbType.String, Value = condition.ResultType });
            }

            if (string.IsNullOrEmpty(condition.OrganID) == false)
            {
                whereBuilder.Append(" AND O.OrganID = @OrganID");
                parameters.Add(new DataParameter { ParameterName = "OrganID", DataType = DbType.String, Value = condition.OrganID });
            }

            //按小时统计
            if (condition.ResultType == "1")
            {
                whereBuilder.Append(" AND A.InspectTime >= @StartTime");
                parameters.Add(new DataParameter("StartTime", DateTime.Parse(condition.StartTime)));
                whereBuilder.Append(" AND A.InspectTime < @EndTime");
                parameters.Add(new DataParameter("EndTime", DateTime.Parse(condition.StartTime + " 23:59:59")));
            }
            else if (condition.ResultType == "2")//按天统计
            {
                DateTime st = DateTime.Parse(condition.StartTime.Substring(0, 7) + "-01 00:00:00");
                DateTime et = st.AddMonths(1).AddSeconds(-1);
                whereBuilder.Append(" AND A.InspectTime >= @StartTime");
                parameters.Add(new DataParameter("StartTime", st));
                whereBuilder.Append(" AND A.InspectTime < @EndTime");
                parameters.Add(new DataParameter("EndTime", et));
            }
            else
            {
                DateTime st = DateTime.Parse(condition.StartTime.Substring(0, 4) + "-01-01 00:00:00");
                DateTime et = st.AddYears(1).AddSeconds(-1);
                whereBuilder.Append(" AND A.InspectTime >= @StartTime");
                parameters.Add(new DataParameter("StartTime", st));
                whereBuilder.Append(" AND A.InspectTime < @EndTime");
                parameters.Add(new DataParameter("EndTime", et));
            }
            

            if (whereBuilder.Length > 0)
            {
                sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
            }

            sqlBuilder.Append(" ORDER BY A.InspectTime ");

            return sqlBuilder.ToString();
        }

        #endregion
    }
}
