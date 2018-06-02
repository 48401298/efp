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
    ///　模块名称：库位管理数据层对象
    ///　作    者：
    ///　编写日期：2017年07月10日
    /// </summary>
    public class WHSiteDAL:BaseDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public WHSite Get(WHSite model)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取信息
                    model = session.Get<WHSite>(model);
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
        /// 根据仓库获取仓位列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓位列表</returns>
        public List<WHSite> GetList(string whID)
        {
            List<WHSite> list = null;
            List<DataParameter> parameters = new List<DataParameter>();
            string sql = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                sql = "SELECT ID,Code,Description FROM T_WH_Site where WHID = @WHID order by Code";
                parameters.Add(new DataParameter("WHID", whID));

                list = session.GetList<WHSite>(sql, parameters.ToArray()).ToList<WHSite>();
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(WHSite condition, DataPage page)
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
                    page = session.GetDataPage<WHSite>(sql, parameters.ToArray(), page);
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
        /// <param name="info">仓位</param>
        /// <returns>true:已使用;false:未使用</returns>
        public bool IsUse(WHSite info)
        {
            int count = 0;
            string sql = null;

            sql = "select count(ID) from T_WH_MatAmount where SaveSite = @SaveSite";

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                count = int.Parse(session.ExecuteSqlScalar(sql, new DataParameter("SaveSite",info.ID)).ToString());
            }

            if (count > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region 获取查询语句
        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询语句</returns>
        private string GetQuerySql(WHSite condition, ref List<DataParameter> parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                //构成查询语句
                sqlBuilder.Append(@"SELECT t1.ID,t1.Code,t1.Description,t1.WHID,t2.Description as WarehourseName
                    ,t1.Place,t1.Remark,t1.CREATEUSER,t1.CREATETIME,t1.UPDATEUSER,t1.UPDATETIME,area.Description as AreaName
                    FROM T_WH_Site t1
                    Inner join T_WH_Warehouse t2 on t1.WHID=t2.ID
                    left outer join T_WH_AREA area on t1.AreaID = area.ID");

                if (string.IsNullOrEmpty(condition.WHID) == false)
                {
                    whereBuilder.Append(" AND t1.WHID = @WHID");
                    parameters.Add(new DataParameter("WHID", condition.WHID));
                }                

                if (string.IsNullOrEmpty(condition.Code) == false)
                {
                    whereBuilder.Append(" AND t1.Code like @Code");
                    parameters.Add(new DataParameter("Code", "%"+condition.Code+"%"));
                }
                if (string.IsNullOrEmpty(condition.Description) == false)
                {
                    whereBuilder.Append(" AND t1.Description like @Description");
                    parameters.Add(new DataParameter("Description", "%" + condition.Description + "%"));
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
        public DataTable GetExportData(WHSite model)
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
                    dt.TableName = "WHSite";
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
        public bool ExistsWHSite(WHSite model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            int count = 0;
            try
            {
                sqlBuilder.Append("SELECT COUNT(0) FROM T_WH_Site");
                sqlBuilder.Append(" WHERE ID <> @ID AND WHID = @WHID AND (Code = @Code or Description = @Description)");
                parameters.Add(new DataParameter { ParameterName = "ID", DataType = DbType.String, Value = model.ID });
                parameters.Add(new DataParameter { ParameterName = "WHID", DataType = DbType.String, Value = model.WHID });
                parameters.Add(new DataParameter { ParameterName = "Code", DataType = DbType.String, Value = model.Code });
                parameters.Add(new DataParameter { ParameterName = "Description", DataType = DbType.String, Value = model.Description });
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
        public int Insert(WHSite model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<WHSite>(model);
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
        public int Update(WHSite model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<WHSite>(model);
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
        public int Delete(WHSite model)
        {
            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除基本信息
                    count = session.Delete<WHSite>(model);
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
        //public ImportMessage GetImportData(List<WHSite> list)
        //{
        //    ImportMessage em = new ImportMessage();
        //    List<DataParameter> parameters = new List<DataParameter>();
        //    try
        //    {
        //        using (IDataSession session = AppDataFactory.CreateMainSession())
        //        {
        //            //设置祖先对象数据会话
        //            session.OpenTs();
        //            foreach (WHSite info in list)
        //            {
        //                if (info.IsNewInfo)
        //                {
        //                    //插入信息
        //                    int count = session.Insert<WHSite>(info);
        //                    em.insertNum++;
        //                }
        //                else
        //                {
        //                    //更新信息
        //                    int count = session.Update<WHSite>(info);
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
