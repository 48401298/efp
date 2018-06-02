using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.BLL;
using Manage.Entity;
using Manage.Entity.WH;
using Manage.DAL.WH;

namespace Manage.BLL.WH
{
    /// <summary>
    ///　模块名称：库存预警设置逻辑层对象
    ///　作    者：
    ///　编写日期：2017年07月10日
    /// </summary>
    public class StockLimitBLL : BaseBLL
    {
        #region 获取信息

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public WHStockLimit Get(WHStockLimit info)
        {
            try
            {
                return new StockLimitDAL().Get(info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取列表

        /// <summary>
        /// 根据仓库列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>列表</returns>
        public List<WHStockLimit> GetList(string whID)
        {
            return new StockLimitDAL().GetList(whID);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(WHStockLimit condition, DataPage page)
        {
            try
            {
                return new StockLimitDAL().GetList(condition, page);
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
        /// <param name="">信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool Exists(WHStockLimit info)
        {
            try
            {
                return new StockLimitDAL().Exists(info);
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
        public DataResult<int> Insert(WHStockLimit info)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                //基本信息
                info.ID = Guid.NewGuid().ToString();
                info.CREATEUSER = this.LoginUser.UserID;
                info.CREATETIME = DateTime.Now;
                info.UPDATEUSER = info.CREATEUSER;
                info.UPDATETIME = info.CREATETIME;
                if (Exists(info))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                result.Result = new StockLimitDAL().Insert(info);
                result.IsSuccess = true;
                return result;
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
        /// <param name="">信息</param>
        /// <returns>更新行数</returns>
        public DataResult<int> Update(WHStockLimit info)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (Exists(info))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                info.UPDATEUSER = this.LoginUser.UserID;
                result.Result = new StockLimitDAL().Update(info);
                result.IsSuccess = true;
                return result;
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
        /// <param name="">主键串</param>
        /// <returns>删除个数</returns>
        public DataResult<int> DeleteArray(string strs)
        {
            int count = 0;
            DataResult<int> result = new DataResult<int>();
            string[] list = strs.Split(":".ToCharArray());
            try
            {
                foreach (string str in list)
                {
                    count += this.Delete(new WHStockLimit { ID = str });
                }
                result.Result = count;
                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>删除个数</returns>
        public int Delete(WHStockLimit info)
        {
            try
            {
                return new StockLimitDAL().Delete(info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 导出数据
        /// <summary>
        /// 获取导出的数据
        /// </summary>
        /// <param name="">查询条件</param>
        /// <returns>数据</returns>
        public DataTable GetExportData(WHStockLimit info)
        {
            try
            {
                return new StockLimitDAL().GetExportData(info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
