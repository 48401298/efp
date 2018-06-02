using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.Data.Attributes;
using LAF.BLL;
using Manage.DAL.Query;
using Manage.Entity.Query;

namespace Manage.BLL.Query
{
    /// <summary>
    /// 追溯查询
    /// </summary>
    public class QualityTraceQueryBLL:BaseBLL
    {
        #region 获取质量追溯产品列表

        /// <summary>
        /// 获取质量追溯产品列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(QualityTraceCondition condition, DataPage page)
        {
            return new QualityTraceQueryDAL().GetList(condition, page);
        }

        #endregion

        #region 获取质量追溯产品列表

        /// <summary>
        /// 获取质量追溯产品列表
        /// </summary>
        /// <param name="user">查询条件</param>
        /// <returns>数据</returns>
        public DataTable GetExportData(QualityTraceCondition condition)
        {
            return new QualityTraceQueryDAL().GetExportData(condition);
        }

        #endregion

        #region 获取产品基本信息

        public TraceGood GetTraceGood(string PID)
        {
            return new QualityTraceQueryDAL().GetTraceGood(PID);
        }

        /// <summary>
        /// 根据批次号获取产品基本信息
        /// </summary>
        /// <param name="batchNumber">批次号</param>
        /// <returns>产品基本信息</returns>
        public TraceGood GetTraceGoodByBN(string batchNumber)
        {
            return new QualityTraceQueryDAL().GetTraceGoodByBN(batchNumber);
        }

        #endregion

        #region 获取物料组成信息

        public List<TraceMaterial> GetTraceMaterial(TraceGood condition)
        {
            return new QualityTraceQueryDAL().GetTraceMaterial(condition);
        }

        #endregion 

        #region 获取加工工序信息

        public List<TraceProcess> GetTraceProcess(TraceGood condition)
        {
            return new QualityTraceQueryDAL().GetTraceProcess(condition);
        }

        #endregion
    }
}
