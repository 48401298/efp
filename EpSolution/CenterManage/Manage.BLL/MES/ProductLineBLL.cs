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
    public class ProductLineBLL : BaseBLL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public ProductLine Get(ProductLine model)
        {
            try
            {
                return new ProductLineDAL().Get(model);
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
        /// <returns>列表</returns>
        public List<ProductLine> GetList()
        {
            return new ProductLineDAL().GetList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(ProductLine condition, DataPage page)
        {
            try
            {
                return new ProductLineDAL().GetList(condition, page);
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
        public bool ExistsCode(ProductLine model)
        {
            try
            {
                return new ProductLineDAL().ExistsCode(model);
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
        public bool ExistsName(ProductLine model)
        {
            try
            {
                return new ProductLineDAL().ExistsName(model);
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
        public DataResult<int> Insert(ProductLine model)
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
                ProductLineDAL cmdDAL = new ProductLineDAL();
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
                result.Result = new ProductLineDAL().Insert(model);
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
        public DataResult<int> Update(ProductLine model)
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
                result.Result = new ProductLineDAL().Update(model);
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
                    count += this.DeleteProductLine(new ProductLine { PID = str });
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
        public int DeleteProductLine(ProductLine model)
        {
            try
            {
                return new ProductLineDAL().Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 生产线删除时校验是否有工位
        /// <summary>
        /// 生产线删除时校验是否有工位
        /// </summary>
        /// <param name="pid"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool HasWorkStation(string pid)
        {
            try
            {
                return new ProductLineDAL().HasWorkStation(pid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}


