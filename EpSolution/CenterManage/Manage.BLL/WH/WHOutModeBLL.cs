﻿using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data;
using LAF.BLL;
using Manage.Entity.WH;
using Manage.DAL.WH;
using Manage.Entity;

namespace Manage.BLL.WH
{
    /// <summary>
    ///　模块名称：出库方式逻辑层对象
    ///　作    者：
    ///　编写日期：2017年07月02日
    /// </summary>
    public class WHOutModeBLL:BaseBLL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public WHOutMode Get(WHOutMode model)
        {
            try
            {
                return new WHOutModeDAL().Get(model);
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
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public List<WHOutMode> GetList()
        {
            return new WHOutModeDAL().GetList(null);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(WHOutMode condition, DataPage page)
        {
            try
            {
                return new WHOutModeDAL().GetList(condition, page);
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
        public bool ExistsWHOutMode(WHOutMode model)
        {
            try
            {
                return new WHOutModeDAL().ExistsWHOutMode(model);
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
        public DataResult<int> Insert(WHOutMode model)
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
                if (ExistsWHOutMode(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                result.Result = new WHOutModeDAL().Insert(model);
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
        public DataResult<int> Update(WHOutMode model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (ExistsWHOutMode(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                model.UPDATEUSER = this.LoginUser.UserID;
                result.Result = new WHOutModeDAL().Update(model);
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
                    count += this.DeleteWHOutMode(new WHOutMode { ID = str });
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
        public int DeleteWHOutMode(WHOutMode model)
        {
            try
            {
                return new WHOutModeDAL().Delete(model);
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
        public DataTable GetExportData(WHOutMode model)
        {
            try
            {
                return new WHOutModeDAL().GetExportData(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 导入数据
        ///// <summary>
        ///// 导入数据
        ///// </summary>
        ///// <param name="list">数据</param>
        ///// <returns>导入结果</returns>
        //public DataResult<ImportMessage> GetImportData(List<WHInMode> list)
        //{
        //    DataResult<ImportMessage> result = new DataResult<ImportMessage>();
        //    WHInModeDAL cmDal = new WHInModeDAL();
        //    List<WHInMode> List = new List<WHInMode>();
        //    int index = 0;
        //    try
        //    {
        //        result.Result = new ImportMessage();
        //        result.Result.Errors = new List<RowError>();
        //        using (IDataSession session = AppDataFactory.CreateMainSession())
        //        {
        //            //状态判断
        //            foreach (WHInMode ma in list)
        //            {
        //                index++;
        //                if (!string.IsNullOrEmpty(ma.InfoError))
        //                {
        //                    ma.ID = null;
        //                    result.Result.failureNum += 1;
        //                    continue;
        //                }
        //                //修改改时根据主键等信息获取详细内容信息
        //                WHInMode oldInfo = cmDal.Get(ma);
        //                if (oldInfo != null)
        //                {
        //                    //更新
        //                    ma.ID = oldInfo.ID;
        //                    ma.CREATEUSER = oldInfo.CREATEUSER;
        //                    ma.CREATEDATE = oldInfo.CREATEDATE;
        //                    ma.UPDATEUSER = this.LoginUser.UserID;
        //                    ma.UPDATEDATE = oldInfo.UPDATEDATE;
        //                    ma.IsNewInfo = false;
        //                    result.Result.updateNum += 1;
        //                }
        //                else
        //                {
        //                    //新增
        //                    oldInfo = new WHInMode();
        //                    ma.ID = Guid.NewGuid().ToString();
        //                    ma.CREATEUSER = this.LoginUser.UserID;
        //                    ma.CREATEDATE = DateTime.Now;
        //                    ma.UPDATEUSER = ma.CREATEUSER;
        //                    ma.UPDATEDATE = ma.UPDATEDATE;
        //                    ma.IsNewInfo = true;
        //                    result.Result.insertNum += 1;
        //                }
        //                List.Add(ma);
        //            }
        //        }
        //        //导入
        //        cmDal.GetImportData(List);
        //        result.Msg = "导入成功";
        //        result.IsSuccess = true;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsSuccess = false;
        //        result.Ex = ex;
        //        return result;
        //    }
        //}
        #endregion

    }
}
