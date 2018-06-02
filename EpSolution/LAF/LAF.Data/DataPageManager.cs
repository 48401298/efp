using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.DbHelper;

namespace LAF.Data
{
    /// <summary>
    /// 获取数据页管理
    /// </summary>
    public class DataPageManager
    {
        #region 属性

        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection Connection { get; set; }

        /// <summary>
        /// 数据库事务
        /// </summary>
        public IDbTransaction Transaction { get; set; }

        /// <summary>
        /// 数据处理工具
        /// </summary>
        public IDbHelper DbHelper { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DbType { get; set; }

        /// <summary>
        /// 连接信息
        /// </summary>
        public ConnectionInfo ConnInfo { get; set; }

        #endregion

        #region 获取数据页

        /// <summary>
        /// 获取数据页
        /// </summary>
        /// <param name="querySql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="page">分页信息</param>
        /// <returns>数据页</returns>
        public DataPage GetDataPage(string querySql, List<DataParameter> parameters, DataPage page)
        {

            try
            {
                if (page.AccuratePartition==true&&
                    string.IsNullOrEmpty(page.KeyName) == true)
                    throw new Exception("精确分页时,未指定分页主键。");

                page.Result = null;

                Action<IDbConnection, IDbTransaction> GetCountMethod = new Action<IDbConnection, IDbTransaction>((conn, trans) =>
                {
                    //得到记录总数
                    string sqlCount;
                    if (string.IsNullOrEmpty(page.CountSql) == true)
                        sqlCount = "select count(*) from (" + querySql + ") page_tbl";
                    else
                        sqlCount = page.CountSql;

                    object returnObject = this.DbHelper.ExecuteScalar(sqlCount, parameters, conn, trans);

                    //设置总记录数            
                    if (returnObject == null)
                    {
                        page.RecordCount = 0;
                    }
                    else
                    {
                        page.RecordCount = Convert.ToInt32(returnObject);
                    }

                    #region 设置分页属性

                    if (page.RecordCount == 0)
                    {
                        page.PageIndex = 1;
                    }

                    if (page.RecordCount % page.PageSize != 0)
                    {
                        page.PageCount = page.RecordCount / page.PageSize + 1;
                        if (page.PageCount < page.PageIndex)
                        {
                            page.PageIndex = page.PageCount;
                        }

                    }
                    else
                    {
                        page.PageCount = page.RecordCount / page.PageSize;
                    }

                    if (page.PageCount < page.PageIndex)
                    {
                        page.PageIndex = page.PageCount;
                    }

                    #endregion
                });

                Action<IDbConnection, IDbTransaction> GetDataMethod = new Action<IDbConnection, IDbTransaction>((conn, trans) =>
                {
                    //获取分页语句
                    string sqlData = "";

                    if (string.IsNullOrEmpty(page.SortExpression) == true)
                        page.SortExpression = page.KeyName;

                    if (string.IsNullOrEmpty(page.SortExpression) == true)
                        throw new Exception("未设置排序表达式");

                    if (page.AccuratePartition == true
                        &&page.SortExpression.IndexOf(page.KeyName) < 0)
                    {
                        page.SortExpression += "," + page.KeyName;
                    }

                    switch (this.DbType)
                    {
                        case DataBaseType.SqlServer:
                            sqlData = this.GetSqlServerSql(querySql, page, page.SortExpression);
                            break;
                        case DataBaseType.Oracle:
                            sqlData = this.GetOracleSql(querySql, page, page.SortExpression);
                            break;
                        case DataBaseType.MySql:
                            sqlData = this.GetMySqlSql(querySql, page, page.SortExpression);
                            break;
                    }

                    if (string.IsNullOrEmpty(sqlData) == true)
                    {
                        throw new Exception("不支持对" + this.DbType.ToString() + "数据库的后台分页查询！");
                    }

                    //获取数据
                    DataTable resultTable = new DataTable();
                    resultTable.TableName = "PageTable";
                    this.DbHelper.FillDataTable(resultTable, sqlData, parameters, conn, null);
                    page.Result = resultTable;
                });

                if (page.IsParallel)
                {
                    using (IDbConnection conn1 = DbManager.GetCon(this.ConnInfo.DbKey, true))
                    {
                        IAsyncResult iarGetCount = GetCountMethod.BeginInvoke(conn1, null, null, null);
                        IAsyncResult iarGetData = GetDataMethod.BeginInvoke(this.Connection, null, null, null);
                        System.Threading.WaitHandle[] events = { iarGetCount.AsyncWaitHandle, iarGetData.AsyncWaitHandle };
                        System.Threading.WaitHandle.WaitAll(events);
                        GetDataMethod.EndInvoke(iarGetData);
                        GetCountMethod.EndInvoke(iarGetCount);
                    }                    
                }
                else
                {
                    GetCountMethod(this.Connection, this.Transaction);
                    if (page.RecordCount > 0) GetDataMethod(this.Connection, this.Transaction);
                }
                
                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取sqlserver分页语句

        /// <summary>
        /// 获取sqlserver分页语句
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="sort">排序表达式</param>
        /// <returns>sqlserver分页语句</returns>
        public string GetSqlServerSql(string querySql, DataPage page, string sort)
        {
            StringBuilder sBuilder = new StringBuilder();

            sBuilder.Append("SELECT * FROM (");
            sBuilder.AppendFormat("SELECT ROW_NUMBER() OVER (ORDER BY {0} ) AS ROWINDEX,*", sort);
            sBuilder.AppendFormat(" FROM ({0}) QMDATAPAGE1 ", querySql);
            sBuilder.Append(") QMDATAPAGE2 ");
            sBuilder.AppendFormat(" WHERE ROWINDEX BETWEEN {0}", Convert.ToString(page.PageSize * (page.PageIndex - 1) + 1));
            sBuilder.AppendFormat(" AND {0}", Convert.ToString(page.PageSize * page.PageIndex));

            return sBuilder.ToString();
        }

        #endregion

        #region 获取MySql分页语句

        /// <summary>
        /// 获取MySql分页语句
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="sort">排序表达式</param>
        /// <returns>sqlserver分页语句</returns>
        public string GetMySqlSql(string querySql, DataPage page, string sort)
        {
            StringBuilder sBuilder = new StringBuilder();

            //sBuilder.Append("SELECT * FROM (");
            //sBuilder.AppendFormat("SELECT ROW_NUMBER() OVER (ORDER BY {0} ) AS ROWINDEX,*", sort);
            //sBuilder.AppendFormat(" FROM ({0}) QMDATAPAGE1 ", querySql);
            //sBuilder.Append(") QMDATAPAGE2 ");
            //sBuilder.AppendFormat(" WHERE ROWINDEX BETWEEN {0}", Convert.ToString(page.PageSize * (page.PageIndex - 1) + 1));
            //sBuilder.AppendFormat(" AND {0}", Convert.ToString(page.PageSize * page.PageIndex));

            sBuilder.Append(querySql+" ORDER BY "+sort + " LIMIT " + (page.PageSize * (page.PageIndex - 1)).ToString() + "," + page.PageSize);

            return sBuilder.ToString();
        }

        #endregion

        #region 获取Oracle分页语句

        /// <summary>
        /// 获取Oracle分页语句
        /// </summary>
        /// <param name="querySql">查询语句</param>
        /// <param name="page">分页信息</param>
        /// <param name="sort">排序表达式</param>        
        public string GetOracleSql(string querySql, DataPage page, string sort)
        {
            StringBuilder sBuilder = new StringBuilder();

            if (sort.ToUpper() == "ROWNUM")
            {
                sBuilder.Append(" SELECT * FROM ( SELECT   ROWNUM AS ROWINDEX,  " + querySql.Substring(7));
                sBuilder.Append(" AND ROWNUM<= " + Convert.ToString(page.PageSize * page.PageIndex) + " ) where " + "  ROWINDEX>" + Convert.ToString((page.PageIndex - 1) * page.PageSize));
            }
            else
            {
                sBuilder.Append("SELECT * FROM (");
                sBuilder.AppendFormat("SELECT ROW_NUMBER() OVER (ORDER BY {0} ) AS ROWINDEX,QMDATAPAGE1.*", sort);
                sBuilder.Append(" FROM (" + querySql + ") QMDATAPAGE1 ");
                sBuilder.Append(") QMDATAPAGE2");
                sBuilder.Append(" WHERE ROWINDEX>" + Convert.ToString((page.PageIndex - 1) * page.PageSize) + " AND ROWINDEX<= " + Convert.ToString(page.PageSize * page.PageIndex));
            }
            return sBuilder.ToString();
        }

        #endregion

        #region 对DataTable中的数据进行分页提取

        /// <summary>
        /// 对DataTable中的数据进行分页提取
        /// </summary>
        /// <param name="dataDt">需要分页的数据</param>
        /// <param name="pageIndex">当前页索引，第一页的索引为0</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>具有当前数据的DataTable</returns>
        public DataTable SplitPageByDataTable(DataTable dataDt, ref int pageIndex, int pageSize, ref int pageCount)
        {
            DataTable lastDt = null;
            int recordCount = 0;

            pageIndex = pageIndex - 1;
            //获取总记录数
            recordCount = dataDt.Rows.Count;

            //计算相关参数
            if (recordCount == 0)
                pageIndex = 0;
            else if (pageIndex * pageSize - recordCount == pageSize)
            {
                pageIndex = pageIndex - 1;
                pageCount = 2;
            }
            else if (recordCount % pageSize == 0)
            {
                int C = recordCount / pageSize;
                if (C - 1 < pageIndex)
                {
                    pageIndex = C - 1;
                }
                pageCount = C;
            }
            else if (recordCount % pageSize != 0)
            {
                int C = recordCount / pageSize + 1;
                if (C - 1 < pageIndex)
                {
                    pageIndex = C - 1;
                }
                pageCount = C;
            }

            DataRow[] drs = dataDt.Select();

            //提取从第1行开始到当前页面最后一行的数据
            drs = drs.Take(pageSize * (pageIndex + 1)).ToArray<DataRow>();

            //提取当前页面的数据
            drs = drs.Skip(pageSize * pageIndex).ToArray<DataRow>();

            //生成存放当前页数据的DataTable
            if (drs.Length != 0)
                lastDt = drs.CopyToDataTable<DataRow>();
            else
                lastDt = dataDt.Clone();

            pageIndex = pageIndex + 1;

            return lastDt;
        }

        #endregion

        #region 对List<T>中的数据进行分页提取

        /// <summary>
        /// 对List<T>中的数据进行分页提取
        /// </summary>
        /// <typeparam name="Entity">实体类型</typeparam>
        /// <param name="datas">数据</param>
        /// <param name="page">分页信息</param>
        /// <returns>分页后数据</returns>
        public List<Entity> SplitPageByDataTable<Entity>(List<Entity> datas, DataPage page) where Entity : new()
        {
            List<Entity> results = null;
            int recordCount = 0;

            int pageIndex = page.PageIndex - 1;
            //获取总记录数
            recordCount = datas.Count;

            //计算相关参数
            if (recordCount == 0)
                pageIndex = 0;
            else if (pageIndex * page.PageSize - recordCount == page.PageSize)
            {
                pageIndex = pageIndex - 1;
                page.PageCount = 2;
            }
            else if (recordCount % page.PageSize == 0)
            {
                int C = recordCount / page.PageSize;
                if (C - 1 < pageIndex)
                {
                    pageIndex = C - 1;
                }
                page.PageCount = C;
            }
            else if (recordCount % page.PageSize != 0)
            {
                int C = recordCount / page.PageSize + 1;
                if (C - 1 < pageIndex)
                {
                    pageIndex = C - 1;
                }
                page.PageCount = C;
            }

            //提取从第1行开始到当前页面最后一行的数据
            results = datas.Take(page.PageSize * (pageIndex + 1)).ToList<Entity>();

            //提取当前页面的数据
            results = results.Skip(page.PageSize * pageIndex).ToList<Entity>();

            page.PageIndex = pageIndex + 1;

            return results;
        }

        #endregion
    }
}
