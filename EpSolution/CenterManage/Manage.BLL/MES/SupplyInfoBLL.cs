using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.BLL;
using LAF.Data;
using Manage.Entity.MES;
using Manage.Entity;
using Manage.DAL.MES;

namespace Manage.BLL.MES
{
    public class SupplyInfoBLL : BaseBLL
    {

        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public SupplyInfo Get(SupplyInfo model)
        {
            try
            {
                return new SupplyInfoDAL().Get(model);
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
        public List<SupplyMaterialInfo> GetList(string bomID)
        {
            return new SupplyInfoDAL().GetMaterialList(bomID);
        }

        /// <summary>
        /// 根据产品bom获取要货明细
        /// </summary>
        /// <param name="pdID">产品主键</param>
        /// <returns>要货明细</returns>
        public List<SupplyMaterialInfo> GetMaterialListByBOM(string pdID)
        {
            return new SupplyInfoDAL().GetMaterialListByBOM(pdID);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(SupplyInfo condition, DataPage page)
        {
            try
            {
                return new SupplyInfoDAL().GetList(condition, page);
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
        public bool ExistsMaterial(SupplyMaterialInfo model)
        {
            try
            {
                return new SupplyInfoDAL().ExistsMaterial(model);
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
        public DataResult<int> Insert(SupplyInfo model)
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
                result.Result = new SupplyInfoDAL().Insert(model);
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
        public DataResult<int> Update(SupplyInfo model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                model.UPDATEUSER = this.LoginUser.UserID;
                result.Result = new SupplyInfoDAL().Update(model);
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
        public int Delete(SupplyInfo model)
        {
            try
            {
                return new SupplyInfoDAL().Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}
