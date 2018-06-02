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
    /// 监测设备
    /// </summary>
    public class InspectDeviceDAL
    {
        #region 获取监测设备列表

        /// <summary>
        /// 获取监测设备列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(InspectDeviceEntity condition, DataPage page)
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
                    page.SortExpression = "DeviceCode ASC";
                }

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<InspectDeviceEntity>(sql, parameters.ToArray(), page);
                }

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取监测设备列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<InspectDeviceEntity> GetAllItemInfo(InspectDeviceEntity condition)
        {
            List<InspectDeviceEntity> itemInfo = null;
            string sql = null;
            List<DataParameter> parameters=new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    itemInfo = session.GetList<InspectDeviceEntity>(sql, parameters.ToArray()).ToList();
                }

                return itemInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取监测设备列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<InspectDeviceEntity> GetAllDeviceByOrgAndType(InspectDeviceEntity condition)
        {
            List<InspectDeviceEntity> itemInfo = null;
            string sql = null;
            List<DataParameter> parameters=new List<DataParameter>();
            try
            {
                StringBuilder sqlBuilder = new StringBuilder();
                StringBuilder whereBuilder = new StringBuilder();

                sqlBuilder.Append(" SELECT Id,DeviceCode,DeviceName FROM deviceinfo A ");

                //查询条件
                if (!string.IsNullOrEmpty(condition.OrganID))
                {
                    whereBuilder.Append(" AND A.OrganID  = @OrganID");
                    parameters.Add(new DataParameter { ParameterName = "OrganID", DataType = DbType.String, Value = condition.OrganID  });
                }

                //查询条件
                if (string.IsNullOrEmpty(condition.DeviceType) == false)
                {
                    whereBuilder.Append(" AND DeviceType = @DeviceType");
                    parameters.Add(new DataParameter { ParameterName = "DeviceType", DataType = DbType.String, Value = condition.DeviceType });
                }

                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
                }

                sqlBuilder.Append(" ORDER BY A.DeviceCode ASC ");

                sql = sqlBuilder.ToString();

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    itemInfo = session.GetList<InspectDeviceEntity>(sql, parameters.ToArray()).ToList();
                }

                return itemInfo;
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

            sqlBuilder.Append(" SELECT Id,DeviceCode,  DeviceName,DeviceIP,DevicePort,LanIP,LanPort, ");
            sqlBuilder.Append(" LastLoginTime,LastRegisterTime,Lon,Lat,A.OrganID,O.ORGANDESC AS OrganDESC,DeviceType,Remark, ");
            sqlBuilder.Append(" A.CREATEUSER,A.CREATETIME,A.UPDATEUSER,A.UPDATETIME FROM deviceinfo A LEFT JOIN t_organization O ON A.OrganID = O.OrganID ");

            //查询条件
            if (!string.IsNullOrEmpty(condition.DeviceCode))
            {
                whereBuilder.Append(" AND DeviceCode like @DeviceCode");
                parameters.Add(new DataParameter { ParameterName = "DeviceCode", DataType = DbType.String, Value = "%" + condition.DeviceCode + "%" });
            }

            //查询条件
            if (string.IsNullOrEmpty(condition.DeviceName) == false)
            {
                whereBuilder.Append(" AND DeviceName like @DeviceName");
                parameters.Add(new DataParameter { ParameterName = "DeviceName", DataType = DbType.String, Value = "%" + condition.DeviceName + "%" });
            }

            if (string.IsNullOrEmpty(condition.OrganID) == false)
            {
                whereBuilder.Append(" AND A.OrganID = @OrganID");
                parameters.Add(new DataParameter { ParameterName = "OrganID", DataType = DbType.String, Value = condition.OrganID });
            }

            if (whereBuilder.Length > 0)
            {
                sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
            }

            return sqlBuilder.ToString();
        }

        #endregion

        #region 获取监测设备信息

        /// <summary>
        /// 获取监测设备信息
        /// </summary>
        /// <param name="itemInfo">条件</param>
        /// <returns>获取监测项目信息</returns>
        public InspectDeviceEntity Get(InspectDeviceEntity itemInfo)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    sqlBuilder.Append(" SELECT Id,DeviceCode, ");
                    sqlBuilder.Append(" DeviceName,DeviceIP,DevicePort,LanIP,LanPort, ");
                    sqlBuilder.Append(" LastLoginTime,LastRegisterTime,Lon,Lat,A.OrganID,O.ORGANDESC AS OrganDESC,DeviceType,Remark, ");
                    sqlBuilder.Append(" A.CREATEUSER,A.CREATETIME,A.UPDATEUSER,A.UPDATETIME  ");
                    sqlBuilder.Append(" FROM deviceinfo A LEFT JOIN t_organization O ON A.OrganID = O.OrganID WHERE Id = @Id ");

                    parameters.Add(new DataParameter { ParameterName = "Id", DataType = DbType.String, Value = itemInfo.Id });

                    itemInfo = session.Get<InspectDeviceEntity>(sqlBuilder.ToString(), parameters.ToArray());
                }

                return itemInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region 插入监测设备信息

        /// <summary>
        /// 插入监测设备信息
        /// </summary>
        /// <param name="itemInfo">监测设备信息</param>
        /// <returns>插入数</returns>
        public int Insert(InspectDeviceEntity itemInfo)
        {
            int count = 0;

            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();
                    
                    //插入基本信息
                    session.Insert<InspectDeviceEntity>(itemInfo);

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

        #region 删除监测设备信息

        /// <summary>
        /// 删除监测设备信息
        /// </summary>
        /// <param name="itemInfo">监测设备信息</param>
        /// <returns>删除个数</returns>
        public int Delete(InspectDeviceEntity itemInfo)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //删除监测项目信息
                    string sql = "DELETE FROM deviceinfo WHERE Id=@Id";
                    count = session.ExecuteSql(sql, new DataParameter { ParameterName = "Id", DataType = DbType.String, Value = itemInfo.Id });
                   
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

        #region 更新监测设备信息

        /// <summary>
        /// 更新监测设备信息
        /// </summary>
        /// <param name="itemInfo">监测设备信息</param>
        /// <returns>更新个数</returns>
        public int Update(InspectDeviceEntity itemInfo)
        {
            int count = 0;

            try
            {
                using (var session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //更新基本信息
                    count = session.Update<InspectDeviceEntity>(itemInfo);

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

        #region 信息是否重复
        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsInspectDevice(InspectDeviceEntity model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM deviceinfo");
                whereBuilder.Append(" AND Id <> @Id ");
                parameters.Add(new DataParameter { ParameterName = "Id", DataType = DbType.String, Value = model.Id });
                if (!string.IsNullOrEmpty(model.DeviceCode))
                {
                    whereBuilder.Append(" AND DeviceCode = @DeviceCode ");
                    parameters.Add(new DataParameter { ParameterName = "DeviceCode", DataType = DbType.String, Value = model.DeviceCode });
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

    }
}
