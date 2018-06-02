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
    public class InspectStatisticsDAL
    {
        #region 获取监测统计列表

        /// <summary>
        /// 获取监测统计列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(InspectResultEntity condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);

                //分页关键字段及排序
                page.KeyName = "Id";
                if (string.IsNullOrEmpty(page.SortExpression))
                {
                    page.SortExpression = " A.UpdateTime DESC ";
                }

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<InspectResultEntity>(sql, parameters.ToArray(), page);
                }

                return page;
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

            if (!string.IsNullOrEmpty(condition.StartTime))
            {
                whereBuilder.Append(" AND A.InspectTime >= @StartTime");
                parameters.Add(new DataParameter("StartTime", DateTime.Parse(condition.StartTime)));
            }

            if (!string.IsNullOrEmpty(condition.EndTime))
            {
                whereBuilder.Append(" AND A.InspectTime < @EndTime");
                parameters.Add(new DataParameter("EndTime", DateTime.Parse(condition.EndTime + " 23:59:59")));
            }

            if (whereBuilder.Length > 0)
            {
                sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
            }

            return sqlBuilder.ToString();
        }

        #endregion

    }
}
