using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.BLL;
using Manage.Entity.WH;
using Manage.DAL.WH;
using LAF.Data;
using Manage.Entity;

namespace Manage.BLL.WH
{
    /// <summary>
    /// 质检管理
    /// </summary>
    public class QualityCheckBLL : BaseBLL
    {

        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public WHQualityCheck Get(WHQualityCheck model)
        {
            try
            {
                return new QualityCheckDAL().Get(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 质量检查单结果

        /// <summary>
        /// 质量检查单结果
        /// </summary>
        /// <param name="checkID"></param>
        /// <returns></returns>
        public List<WHQualityCheckResult> GetResultList(string checkID)
        {
            return new QualityCheckDAL().GetResultList(checkID);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(QualityCheckCondition condition, DataPage page)
        {
            try
            {
                return new QualityCheckDAL().GetList(condition, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 信息是否重复
        /// <summary>
        /// 判断单号是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsBillNO(WHQualityCheck model)
        {
            try
            {
                return new QualityCheckDAL().ExistsBillNO(model);
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
        public DataResult<int> Insert(WHQualityCheck model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                //基本信息
                model.ID = Guid.NewGuid().ToString();
                model.BillNO = this.GetNewBillNO();
                model.CREATEUSER = this.LoginUser.UserID;
                model.CREATETIME = DateTime.Now;
                model.UPDATEUSER = model.CREATEUSER;
                model.UPDATETIME = model.CREATETIME;
                if (ExistsBillNO(model))
                {
                    result.Msg = "质检单号已存在";
                    result.Result = -1;
                    return result;
                }
                result.Result = new QualityCheckDAL().Insert(model);
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
        public DataResult<int> Update(WHQualityCheck model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (ExistsBillNO(model))
                {
                    result.Msg = "质检单号已存在";
                    result.Result = -1;
                    return result;
                }
                model.UPDATEUSER = this.LoginUser.UserID;
                result.Result = new QualityCheckDAL().Update(model);
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
        public int Delete(WHQualityCheck model)
        {
            try
            {
                return new QualityCheckDAL().Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region 生成新单号

        /// <summary>
        /// 生成新单号
        /// </summary>
        /// <returns></returns>
        public string GetNewBillNO()
        {
            string maxNo = new QualityCheckDAL().GetMaxBillNO();
            string newNo = DateTime.Now.ToString("yyyyMMdd");

            if (string.IsNullOrEmpty(maxNo) || maxNo.Length < 12)
            {
                newNo = newNo + "0001";
            }
            else
            {
                if (newNo == maxNo.Substring(0, 8))
                {
                    newNo = (double.Parse(maxNo) + 1).ToString();
                }
                else
                {
                    newNo = newNo + "0001";
                }
            }

            return newNo;
        }

        #endregion

        #region 获取需质检入库单

        /// <summary>
        /// 获取需质检入库单
        /// </summary>
        /// <returns>入库单列表</returns>
        public List<InStockBill> GetNeedInStockBills()
        {
            return new QualityCheckDAL().GetNeedInStockBills();
        }

        #endregion

    }
}
