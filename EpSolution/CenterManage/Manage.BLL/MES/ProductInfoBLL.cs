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
    public class ProductInfoBLL : BaseBLL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public ProductInfo Get(ProductInfo model)
        {
            try
            {
                return new ProductInfoDAL().Get(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>工厂列表</returns>
        public List<ProductInfo> GetList()
        {
            return new ProductInfoDAL().GetList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(ProductInfo condition, DataPage page)
        {
            try
            {
                return new ProductInfoDAL().GetList(condition, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 信息是否重复
        /// <summary>
        /// 判断编号是否存在
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsCode(ProductInfo model)
        {
            try
            {
                return new ProductInfoDAL().ExistsCode(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool ExistsName(ProductInfo model)
        {
            try
            {
                return new ProductInfoDAL().ExistsName(model);
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
        public DataResult<int> Insert(ProductInfo model)
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
                ProductInfoDAL cmdDAL = new ProductInfoDAL();
                if (ExistsCode(model))
                {
                    result.Msg = "编号已存在";
                    result.Result = -1;
                    return result;
                }
                if (ExistsName(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                result.Result = new ProductInfoDAL().Insert(model);
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
        public DataResult<int> Update(ProductInfo model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (ExistsCode(model))
                {
                    result.Msg = "编号已存在";
                    result.Result = -1;
                    return result;
                }
                if (ExistsName(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                model.UPDATEUSER = this.LoginUser.UserID;
                model.UPDATETIME = DateTime.Now;
                result.Result = new ProductInfoDAL().Update(model);
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
                    count += this.DeleteProductInfo(new ProductInfo { PID = str });
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
        public int DeleteProductInfo(ProductInfo model)
        {
            try
            {
                return new ProductInfoDAL().Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 产品删除时校验是否在产品计划使用过
        /// <summary>
        /// 产品删除时校验是否在产品计划使用过
        /// </summary>
        /// <param name="pid"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool HasProducePlan(string pid)
        {
            try
            {
                return new ProductInfoDAL().HasProducePlan(pid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}


