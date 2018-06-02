using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using LAF.Data.DbHelper;

namespace LAF.Data
{
    /// <summary>
    /// 数据映射会话接口
    /// </summary>
    public interface IDataSession : IDisposable
    {
        /// <summary>
        /// Sql工具
        /// </summary>
        IDbHelper DbHelper { get; set; }

        /// <summary>
        /// 打开回话
        /// </summary>
        void OpenCon();

        /// <summary>
        /// 关闭回话
        /// </summary>
        void CloseCon();

        /// <summary>
        /// 打开事务
        /// </summary>
        void OpenTs();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTs();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTs();

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>实体主键为自增时，返回主键值；否则返回插入行数。</returns>
        int Insert<T>(T model) where T : new();

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体列表</param>
        /// <returns>实体主键为自增时，返回主键值；否则返回插入行数。</returns>
        int Insert<T>(List<T> list) where T : new();

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>删除行数</returns>
        int Delete<T>(T model) where T : new();

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体列表</param>
        /// <returns>删除行数</returns>
        int Delete<T>(List<T> list) where T : new();

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="Model">实体</param>
        /// <returns>更新行数</returns>
        int Update<T>(T model) where T : new();

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="Model">实体</param>
        /// <param name="updateItems">需要更新的字段</param>
        /// <returns>更新行数</returns>
        int Update<T>(T model,List<string> updateItems) where T : new();

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体列表</param>
        /// <returns>更新行数</returns>
        int Update<T>(List<T> list) where T : new();

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>实体数据</returns>
        T Get<T>(string sql, params DataParameter[] parameters) where T : new();

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>实体数据</returns>
        T Get<T>(T model) where T : new();

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="condition">查询条件</param>      
        /// <returns>实体列表</returns>
        IList<T> GetList<T, C>(C condition) where T : new();


        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>实体列表</returns>
        IList<T> GetList<T>(string sql, DataParameter[] parameters) where T : new();

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回影响行数</returns>
        int ExecuteSql(string sql, params DataParameter[] parameters);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回影响行数</returns>
        int ExecuteSql(string sql, CommandType commandType, params DataParameter[] parameters);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回结果</returns>
        object ExecuteSqlScalar(string sql, params DataParameter[] parameters);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回结果</returns>
        object ExecuteSqlScalar(string sql, CommandType commandType, params DataParameter[] parameters);

        /// <summary>
        /// 更新表
        /// </summary>
        /// <param name="table">表</param>
        void UpdateTable(DataTable table);

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        DataTable GetTable(string sql, DataParameter[] parameters);

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandType">commandType</param>
        /// <returns>DataTable</returns>
        DataTable GetTable(string sql, CommandType commandType, DataParameter[] parameters);

        /// <summary>
        /// 填充DataTable
        /// </summary>
        /// <param name="table">datatable</param>
        /// <param name="sql">查询语句</param>        
        /// <param name="parameters">参数</param>       
        void FillTable(DataTable table, string sql, DataParameter[] parameters);

        /// <summary>
        /// 填充DataTable
        /// </summary>
        /// <param name="table">datatable</param>
        /// <param name="sql">查询语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">参数</param>       
        void FillTable(DataTable table, string sql, CommandType commandType, DataParameter[] parameters);


        /// <summary>
        /// 按页获取数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="C">查询条件</typeparam>
        /// <param name="condition">查询条件</param>      
        /// <param name="page">分页信息</param>
        /// <returns>数据页(实体列表)</returns>
        DataPage GetDataPage<T, C>(C condition, DataPage page) where T : new();

        /// <summary>
        /// 按页获取数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="page">分页信息</param>
        /// <returns>数据页</returns>
        DataPage GetDataPage<T>(string sql, DataParameter[] parameters, DataPage page) where T : new();

        /// <summary>
        /// 按页获取数据
        /// </summary>       
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="page">分页信息</param>
        /// <returns>数据页</returns>
        DataPage GetDataPage(string sql, DataParameter[] parameters, DataPage page);


    }
}
