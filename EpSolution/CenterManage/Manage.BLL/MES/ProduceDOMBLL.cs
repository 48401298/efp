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
    public class ProduceDOMBLL : BaseBLL
    {

        #region 获取信息

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="produceID">产品主键</param>
        /// <returns>*信息</returns>
        public ProduceBOM GetByProduceID(string produceID)
        {
            return new ProduceDOMDAL().GetByProduceID(produceID);
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public ProduceBOM Get(ProduceBOM model)
        {
            try
            {
                return new ProduceDOMDAL().Get(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取bom明细

        /// <summary>
        /// 获取bom明细
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>bom明细</returns>
        public List<BOMDetail> GetList(string bomID)
        {
            return new ProduceDOMDAL().GetList(bomID);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(ProduceBOM condition, DataPage page)
        {
            try
            {
                return new ProduceDOMDAL().GetList(condition, page);
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
        public bool ExistsBOM(ProduceBOM model)
        {
            try
            {
                return new ProduceDOMDAL().ExistsBOM(model);
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
        public DataResult<int> Insert(ProduceBOM model)
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
                if (ExistsBOM(model))
                {
                    result.Msg = "产品已存在";
                    result.Result = -1;
                    return result;
                }
                result.Result = new ProduceDOMDAL().Insert(model);
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
        public DataResult<int> Update(ProduceBOM model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (ExistsBOM(model))
                {
                    result.Msg = "产品已存在";
                    result.Result = -1;
                    return result;
                }
                model.UPDATEUSER = this.LoginUser.UserID;
                result.Result = new ProduceDOMDAL().Update(model);
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
        public int Delete(ProduceBOM model)
        {
            try
            {
                return new ProduceDOMDAL().Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}
