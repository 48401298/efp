﻿using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.BLL;
using Manage.Entity;
using Manage.Entity.WH;
using Manage.DAL.WH;

namespace Manage.BLL.WH
{
    /// <summary>
    ///　模块名称：供货单位逻辑对象
    ///　作    者：
    ///　编写日期：2017年07月06日
    /// </summary>
    public class WHProviderBLL : BaseBLL
    {
        #region 获取信息

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>供货单位</returns>
        public WHProvider Get(WHProvider model)
        {
            try
            {
                return new WHProviderDAL().Get(model);
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
        /// <returns>供货单位列表</returns>
        public List<WHProvider> GetList()
        {
            return new WHProviderDAL().GetList(null);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(WHProvider condition, DataPage page)
        {
            try
            {
                return new WHProviderDAL().GetList(condition, page);
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
        public bool Exists(WHProvider model)
        {
            try
            {
                return new WHProviderDAL().Exists(model);
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
        public DataResult<int> Insert(WHProvider model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                //基本信息
                model.ID = Guid.NewGuid().ToString();
                model.CREATEUSER = this.LoginUser.UserID;
                model.CREATETIME = DateTime.Now;
                model.UPDATEUSER = model.CREATEUSER;
                model.UPDATETIME = model.CREATETIME;
                if (Exists(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                result.Result = new WHProviderDAL().Insert(model);
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
        public DataResult<int> Update(WHProvider model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (Exists(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                model.UPDATEUSER = this.LoginUser.UserID;
                result.Result = new WHProviderDAL().Update(model);
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
                    count += this.Delete(new WHProvider { ID = str });
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
        public int Delete(WHProvider model)
        {
            try
            {
                return new WHProviderDAL().Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 导出数据
        /// <summary>
        /// 获取导出的数据
        /// </summary>
        /// <param name="">查询条件</param>
        /// <returns>数据</returns>
        public DataTable GetExportData(WHProvider model)
        {
            try
            {
                return new WHProviderDAL().GetExportData(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }

}
