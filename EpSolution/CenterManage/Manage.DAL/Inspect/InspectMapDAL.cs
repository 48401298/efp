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
    /// 监测地图
    /// </summary>
    public class InspectMapDAL
    {
        #region 获取监测设备列表

        /// <summary>
        /// 获取监测设备列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<InspectDeviceEntity> GetList(InspectDeviceEntity condition)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);

                List<InspectDeviceEntity> list = null;
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    list = session.GetList<InspectDeviceEntity>(sql, parameters.ToArray()).ToList();
                }

                return list;
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
        private string GetQuerySql(InspectDeviceEntity condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();

            sqlBuilder.Append(" SELECT Id,DeviceCode,DeviceName,DeviceIP,DevicePort,LanIP,LanPort, ");
            sqlBuilder.Append(" LastLoginTime,LastRegisterTime,Lon,Lat,A.OrganID,O.ORGANDESC AS OrganDESC,DeviceType,Remark ");
            sqlBuilder.Append(" FROM deviceinfo A LEFT JOIN t_organization O ON A.OrganID = O.OrganID ");

            sqlBuilder.Append(" ORDER BY DeviceCode ");

            return sqlBuilder.ToString();
        }

        #endregion

    }
}
