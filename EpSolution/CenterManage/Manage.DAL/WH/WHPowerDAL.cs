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
    /// 仓库权限管理
    /// </summary>
    public class WHPowerDAL:BaseDAL
    {
        #region 仓库权限

        /// <summary>
        /// 获取仓库权限
        /// </summary>
        /// <param name="userID">用户主键</param>
        /// <returns>权限列表</returns>
        public List<WarehousePower> GetWHPowers(string userID)
        {
            List<WarehousePower> list = new List<WarehousePower>();
            List<DataParameter> parameters = new List<DataParameter>();

            string sql = "select WarehouseID from T_WH_WHPower where UserID = @UserID";
            parameters.Add(new DataParameter("UserID", userID));
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                list = session.GetList<WarehousePower>(sql, parameters.ToArray()).ToList();
            }

            return list;
        }

        /// <summary>
        /// 删除仓库权限
        /// </summary>
        /// <param name="userID">用户主键</param>
        public void DeleteWHPowers(string userID)
        {
            string sql = "delete from T_WH_WHPower where UserID = @UserID";
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.ExecuteSql(sql, new DataParameter("UserID", userID));
            }
        }

        /// <summary>
        /// 保存仓库权限
        /// </summary>
        /// <param name="userID">用户主键</param>
        /// <param name="powers">仓库权限</param>
        public void SaveWHPowers(string userID,List<WarehousePower> powers)
        {
            string sql = "delete from T_WH_WHPower where UserID = @UserID";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.OpenTs();

                //删除
                session.ExecuteSql(sql, new DataParameter("UserID", userID));

                //新增
                session.Insert<WarehousePower>(powers);

                session.CommitTs();
            }
        }

        #endregion
    }
}
