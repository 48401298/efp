using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAF.Data;
using LAF.Data.Attributes;
using Manage.Entity.WH;
using LAF.Entity;

namespace Manage.DAL.WH
{
    /// <summary>
    ///　模块名称：仓库信息管理
    ///　作    者：
    ///　编写日期：2017年07月06日
    /// </summary>
    public class WarehouseDAL:BaseDAL
    {

        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public Warehouse Get(Warehouse model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<Warehouse>(model);
                }
                return model;
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
        /// <param name="info">仓库</param>
        /// <returns>true:已使用;false:未使用</returns>
        public bool IsUse(Warehouse info)
        {
            int count = 0;
            string sql = null;

            sql = "select count(ID) from T_WH_Site where WHID = @WHID";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                count = int.Parse(session.ExecuteSqlScalar(sql, new DataParameter("WHID", info.ID)).ToString());
            }

            if (count > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region 获取列表

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public List<Warehouse> GetList(Warehouse condition)
        {
            List<Warehouse> list = null;
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT ID,Code,Description FROM T_WH_Warehouse order by Code";
                list = session.GetList<Warehouse>(sql, new List<DataParameter>().ToArray()).ToList<Warehouse>();
            }

            return list;
        }

        /// <summary>
        /// 获取获取具有权限的仓库
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public List<Warehouse> GetListByUserID(LoginInfo user)
        {
            List<Warehouse> list = null;
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT ID,Code,Description FROM T_WH_Warehouse";

                if (string.IsNullOrEmpty(user.UserID) == false)
                {
                    sql += " where exists(select WarehouseID from T_WH_WHPower where UserID=@userID and WarehouseID=T_WH_Warehouse.ID)";
                    parameters.Add(new DataParameter("userID", user.UserID));
                }
                sql += " order by Code";
                list = session.GetList<Warehouse>(sql, parameters.ToArray()).ToList<Warehouse>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(Warehouse condition, DataPage page)
        {
            string sql = null;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                sql = this.GetQuerySql(condition, ref parameters);
                //分页关键字段及排序
                page.KeyName = "ID";
                if (string.IsNullOrEmpty(page.SortExpression))
                    page.SortExpression = "UPDATETIME DESC";
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    page = session.GetDataPage<Warehouse>(sql, parameters.ToArray(), page);
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
        private string GetQuerySql(Warehouse condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append("SELECT ID,Code,Description,Remark,CREATEUSER,CREATETIME,UPDATEUSER,UPDATETIME ");
                sqlBuilder.Append("FROM T_WH_Warehouse ");

                if (string.IsNullOrEmpty(condition.Code) == false)
                {
                    whereBuilder.Append(" AND Code like @Code");
                    parameters.Add(new DataParameter("Code", "%"+condition.Code+"%"));
                }
                if (string.IsNullOrEmpty(condition.Description) == false)
                {
                    whereBuilder.Append(" AND Description like @Description");
                    parameters.Add(new DataParameter("Description", "%"+condition.Description+"%"));
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
        public DataTable GetExportData(Warehouse model)
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
        public bool ExistsWarehouse(Warehouse model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_WH_Warehouse");
                sqlBuilder.Append(" WHERE ID <> @ID AND (Code = @Code or Description = @Description)");
                parameters.Add(new DataParameter { ParameterName = "ID", DataType = DbType.String, Value = model.ID });
                parameters.Add(new DataParameter { ParameterName = "Code", DataType = DbType.String, Value = model.Code });
                parameters.Add(new DataParameter { ParameterName = "Description", DataType = DbType.String, Value = model.Description });
                
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
        public int Insert(Warehouse model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<Warehouse>(model);
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
        public int Update(Warehouse model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<Warehouse>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 逻辑删除
        /// <summary>
        /// 逻辑删除信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>删除个数</returns>
        public int Delete(Warehouse model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    count = session.Delete<Warehouse>(model);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //#region 导入
        //public ImportMessage GetImportData(List<Warehouse> list)
        //{
        //    ImportMessage em = new ImportMessage();
        //    List<DataParameter> parameters = new List<DataParameter>();
        //    try
        //    {
        //        using (IDataSession session = AppDataFactory.CreateMainSession())
        //        {
        //            //设置祖先对象数据会话
        //            session.OpenTs();
        //            foreach (Warehouse info in list)
        //            {
        //                if (info.IsNewInfo)
        //                {
        //                    //插入信息
        //                    int count = session.Insert<Warehouse>(info);
        //                    em.insertNum++;
        //                }
        //                else
        //                {
        //                    //更新信息
        //                    int count = session.Update<Warehouse>(info);
        //                    em.updateNum++;
        //                }
        //            }
        //            session.CommitTs();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return em;
        //}
        //#endregion

    }

}
