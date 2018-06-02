using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.DAL.MES;
using Manage.Entity.MES;
using Manage.Entity;
using LAF.BLL;
using LAF.Data;

namespace Manage.BLL.MES
{
    public class EquipmentBLL : BaseBLL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public EquipmentInfo Get(EquipmentInfo model)
        {
            try
            {
                return new EquipmentDAL().Get(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EquipmentInfo GetInfoByBarCode(string barCode)
        {
            try
            {
                return new EquipmentDAL().GetInfoByBarCode(barCode);
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
        /// <returns>设备列表</returns>
        public List<EquipmentInfo> GetList()
        {
            return new EquipmentDAL().GetList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(EquipmentInfo condition, DataPage page)
        {
            try
            {
                return new EquipmentDAL().GetList(condition, page);
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
        public bool ExistsEquipment(EquipmentInfo model)
        {
            try
            {
                return new EquipmentDAL().ExistsEquipment(model);
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
        public DataResult<int> Insert(EquipmentInfo model)
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
                EquipmentDAL cmdDAL = new EquipmentDAL();
                if (ExistsEquipment(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                result.Result = new EquipmentDAL().Insert(model);
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
        public DataResult<int> Update(EquipmentInfo model)
        {
            DataResult<int> result = new DataResult<int>();
            try
            {
                if (ExistsEquipment(model))
                {
                    result.Msg = "名称已存在";
                    result.Result = -1;
                    return result;
                }
                model.UPDATEUSER = this.LoginUser.UserID;
                result.Result = new EquipmentDAL().Update(model);
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
                    count += this.DeleteEquipment(new EquipmentInfo { PID = str });
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
        public int DeleteEquipment(EquipmentInfo model)
        {
            try
            {
                return new EquipmentDAL().Delete(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 设备删除时校验是否使用过
        /// <summary>
        /// 设备删除时校验是否使用过
        /// </summary>
        /// <param name="eqid"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool HasUsed(string eqid)
        {
            try
            {
                return new EquipmentDAL().HasUsed(eqid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
