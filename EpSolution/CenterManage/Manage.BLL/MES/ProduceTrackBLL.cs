using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.BLL;
using Manage.Entity;
using Manage.DAL.MES;
using Manage.Entity.MES;
using Manage.Entity.Query;
using Manage.BLL.Query;

namespace Manage.BLL.MES
{
    public class ProduceTrackBLL : BaseBLL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public ProduceTrack Get(ProduceTrack model)
        {
            try
            {
                return new ProduceTrackDAL().Get(model);
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
        public DataResult<int> Insert(ProduceTrack model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                //基本信息
                model.PID = Guid.NewGuid().ToString();
                model.CREATEUSER = this.LoginUser.UserID;
                model.CREATETIME = DateTime.Now;
                model.UPDATEUSER = model.CREATEUSER;
                model.UPDATETIME = model.CREATETIME;

                //获取生产计划信息
                ProducePlan plan = new ProducePlanBLL().GetByBatchNumber(model.BATCHNUMBER);
                if (plan != null)
                {
                    model.FACTORYPID = plan.FACTORYPID;
                    model.PRID = plan.PRID;
                }

                result.Result = new ProduceTrackDAL().Insert(model);
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
        public DataResult<int> Update(ProduceTrack model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                //获取生产计划信息
                ProducePlan plan = new ProducePlanBLL().GetByBatchNumber(model.BATCHNUMBER);
                if (plan != null)
                {
                    model.FACTORYPID = plan.FACTORYPID;
                    model.PRID = plan.PRID;
                }

                model.UPDATEUSER = this.LoginUser.UserID;
                model.UPDATETIME = DateTime.Now;
                result.Result = new ProduceTrackDAL().Update(model);
                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<OnProcessingInfo> GetOnProcessingList(string barCode)
        {
            try
            {
                return new ProduceTrackDAL().GetOnProcessingList(barCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
