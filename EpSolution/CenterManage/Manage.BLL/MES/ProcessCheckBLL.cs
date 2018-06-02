using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Entity.MES;
using Manage.BLL.Query;
using Manage.Entity.Query;
using LAF.BLL;

namespace Manage.BLL.MES
{
    /// <summary>
    /// 工序跟踪校验
    /// </summary>
    public class ProcessCheckBLL:BaseBLL
    {
        #region 防跳序

        /// <summary>
        /// 防跳序
        /// </summary>
        /// <param name="batchNumber">生产批次号</param>
        /// <param name="productID">产品主键</param>
        /// <param name="startProcessID">当前工序</param>
        /// <returns>校验结果</returns>
        public string CheckSkipProcess(string batchNumber,string productID,string startProcessID)
        {
            string result = "";

            //获取所有工序信息
            List<ProcessInfo> allProcess = this.GetProductProcess(productID);

            //获取已完成工序信息
            List<TraceProcess> finishedProcess = new QualityTraceQueryBLL().GetTraceProcess(new TraceGood { BatchNumber = batchNumber });

            //定位当前工序
            int startProcessIndex = allProcess.FindIndex(p => p.PID == startProcessID);

            if (startProcessIndex == 0)
            {
                return result;
            }

            if (startProcessIndex == -1)
            {
                result = "无此工序";
                return result;
            }

            //获取当前开始工序的上一个工序编号  
            ProcessInfo prevProcess = allProcess[startProcessIndex - 1];

            //判断上一工序是否完成
            bool isFinished = finishedProcess.Exists(p => p.ProcessCode == prevProcess.PCODE);

            //如完成返回空串，如未完成，提示应完成工序
            return isFinished == true ? "" : "请先完成"+prevProcess.PNAME+"工序";
        }

        #endregion

        #region 防漏序

        /// <summary>
        /// 防漏序
        /// </summary>
        /// <param name="batchNumber">生产批次号</param>
        /// <param name="productID">产品主键</param>
        /// <returns>校验结果</returns>
        public string CheckMissingProcess(string batchNumber, string productID)
        {
            string result = "";
            //获取所有工序信息
            List<ProcessInfo> allProcess=this.GetProductProcess(productID);

            //获取已完成工序信息
            List<TraceProcess> finishedProcess=new QualityTraceQueryBLL().GetTraceProcess(new TraceGood{BatchNumber=batchNumber});            

            //判断工序所有是否全部完成
            foreach (ProcessInfo pi in allProcess)
            {
                bool f = finishedProcess.Exists(p => p.ProcessCode == pi.PCODE);
                if (f == false)
                {
                    result = pi.PNAME+"工序"+"未完成";
                    break;
                }
            }
            return result;
        }

        #endregion

        #region 获取产品所有工序信息

        /// <summary>
        /// 获取产品所有工序信息
        /// </summary>
        /// <param name="productID">产品主键</param>
        /// <returns>所有工序信息</returns>
        public List<ProcessInfo> GetProductProcess(string productID)
        {
            ProductInfo product = new ProductInfoBLL().Get(new ProductInfo {PID=productID });

            List<ProcessInfo> list = new ProcessInfoBLL().GetList(new ProcessInfo {FLOWID=product.PRID });

            return list;
        }

        #endregion
    }
}
