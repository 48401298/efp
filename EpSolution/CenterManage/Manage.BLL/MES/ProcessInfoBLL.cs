using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.BLL;
using Manage.Entity.MES;
using Manage.DAL.MES;
using LAF.Data;
using Manage.Entity;

namespace Manage.BLL.MES
{
    public class ProcessInfoBLL : BaseBLL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public ProcessInfo Get(ProcessInfo model)
        {
            try
            {
                return new ProcessInfoDAL().Get(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcessInfo GetInfoByWS(string wscode)
        {
            try
            {
                return new ProcessInfoDAL().GetInfoByWS(wscode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcessInfo GetInfoByBarCodeAndBatchNumber(string wscode, string batchNumber)
        {
            try
            {
                return new ProcessInfoDAL().GetInfoByBarCodeAndBatchNumber(wscode, batchNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取列表
        public List<EquipmentRef> GetEList(string id)
        {
            return new ProcessInfoDAL().GetEList(id);
        }

        public List<StationRef> GetSList(string id)
        {
            return new ProcessInfoDAL().GetSList(id);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(ProcessInfo condition, DataPage page)
        {
            try
            {
                return new ProcessInfoDAL().GetList(condition, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public List<ProcessInfo> GetList(ProcessInfo condition)
        {
            return new ProcessInfoDAL().GetList(condition);
        }

        #endregion

        #region 信息是否重复
        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsProcessInfo(ProcessInfo model)
        {
            try
            {
                return new ProcessInfoDAL().ExistsProcessInfo(model);
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
        public DataResult<int> Insert(ProcessInfo model)
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
                ProcessInfoDAL cmdDAL = new ProcessInfoDAL();
                if (ExistsProcessInfo(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                result.Result = new ProcessInfoDAL().Insert(model);
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
        public DataResult<int> Update(ProcessInfo model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (ExistsProcessInfo(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                model.UPDATEUSER = this.LoginUser.UserID;
                result.Result = new ProcessInfoDAL().Update(model);
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
                    count += this.DeleteProcessInfo(new ProcessInfo { PID = str });
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
        public int DeleteProcessInfo(ProcessInfo model)
        {
            try
            {
                return new ProcessInfoDAL().Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}

