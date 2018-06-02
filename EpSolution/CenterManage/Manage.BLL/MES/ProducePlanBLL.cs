using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.BLL;
using Manage.Entity;
using Manage.Entity.MES;
using Manage.Entity.Query;
using Manage.DAL.MES;
using LAF.Data;
using Manage.BLL.Query;

namespace Manage.BLL.MES
{
    public class ProducePlanBLL : BaseBLL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public ProducePlan Get(ProducePlan model)
        {
            try
            {
                return new ProducePlanDAL().Get(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         /// <summary>
        /// 通过生产批次号获取生产计划
        /// </summary>
        /// <param name="batchNumber">生产批次号</param>
        /// <returns>生产计划</returns>
        public ProducePlan GetByBatchNumber(string batchNumber)
        {
            try
            {
                return new ProducePlanDAL().GetByBatchNumber(batchNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取列表
        public List<ProducePlan> GetList()
        {
            return new ProducePlanDAL().GetList();
        }

        public List<ProducePlan> GetCList()
        {
            return new ProducePlanDAL().GetCList();
        }

        public List<ProducePlan> GetDDList()
        {
            return new ProducePlanDAL().GetDDList();
        }

        /// <summary>
        /// 获取未完成生产计划
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public List<ProducePlan> GetUnFinishedPlans(ProducePlan condition)
        {
            List<ProducePlan> list = new ProducePlanDAL().GetUnFinishedPlans(condition);

            foreach(ProducePlan plan in list)
            {
                //获取物料组成
                List<TraceMaterial> materials = new QualityTraceQueryBLL().GetTraceMaterial(new TraceGood { BatchNumber = plan.BATCHNUMBER });
                plan.Materials = "";

                foreach (TraceMaterial m in materials)
                {
                    plan.Materials += "," + m.MatName;
                }

                if (string.IsNullOrEmpty(plan.Materials) == false)
                {
                    plan.Materials = plan.Materials.Substring(1);
                }

                //获取工序信息

                //获取已完成工序信息
                List<TraceProcess> processList = new QualityTraceQueryBLL().GetTraceProcess(new TraceGood { BatchNumber = plan.BATCHNUMBER });
                foreach (TraceProcess p in processList)
                {
                    plan.FinishedProcess += "," + p.ProcessName;
                }

                if (string.IsNullOrEmpty(plan.FinishedProcess) == false)
                {
                    plan.FinishedProcess = plan.FinishedProcess.Substring(1);
                }

                if (processList.Count > 0)
                {
                    plan.StartTime = processList[0].WorkingStartTime;
                }

                //生成未完成工序信息
            }

            return list;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(ProducePlan condition, DataPage page)
        {
            try
            {
                return new ProducePlanDAL().GetList(condition, page);
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
        public DataResult<int> Insert(ProducePlan model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                //基本信息
                model.PID = Guid.NewGuid().ToString();
                model.FACTAMOUNT = "0";
                model.CREATEUSER = this.LoginUser.UserID;
                model.CREATETIME = DateTime.Now;
                model.UPDATEUSER = model.CREATEUSER;
                model.UPDATETIME = model.CREATETIME;
                result.Result = new ProducePlanDAL().Insert(model);
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
        public DataResult<int> Update(ProducePlan model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                model.UPDATEUSER = this.LoginUser.UserID;
                model.UPDATETIME = DateTime.Now;
                result.Result = new ProducePlanDAL().Update(model);
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
                    count += this.DeleteProducePlan(new ProducePlan { PID = str });
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
        public int DeleteProducePlan(ProducePlan model)
        {
            try
            {
                return new ProducePlanDAL().Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设置计划已完成

        public void SignPlanFinished(ProducePlan plan)
        {
            new ProducePlanDAL().SignPlanFinished(plan);
        }

        #endregion


        public ProductInfo GetPNameByIDBatchNumber(string batchNumber)
        {
            try
            {
                return new ProducePlanDAL().GetPNameByIDBatchNumber(batchNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GoodInfo GetGoodInfoByBatchNumber(string batchNumber)
        {
            try
            {
                return new ProducePlanDAL().GetGoodInfoByBatchNumber(batchNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据ID获取产品信息（要货信息）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SupplyInfo GetProducePlanInfoByID(string id)
        {
            try
            {
                return new ProducePlanDAL().GetProducePlanInfoByID(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}