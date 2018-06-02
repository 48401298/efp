using System;
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
    ///　模块名称：货品信息管理逻辑层
    ///　作    者：
    ///　编写日期：2017年07月06日
    /// </summary>
    public class WHMatBLL : BaseBLL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>信息</returns>
        public WHMat Get(WHMat info)
        {
            try
            {
                return new WHMatDAL().Get(info);
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
        /// <returns>仓库列表</returns>
        public List<WHMat> GetList()
        {
            return new WHMatDAL().GetList(null);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(WHMat condition, DataPage page)
        {
            try
            {
                return new WHMatDAL().GetList(condition, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 判断是否被使用

        /// <summary>
        /// 判断是否被使用
        /// </summary>
        /// <param name="info">货品</param>
        /// <returns>true:已使用;false:未使用</returns>
        public bool IsUse(WHMat info)
        {
            return new WHMatDAL().IsUse(info);
        }

        #endregion

        #region 信息是否重复
        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool Exists(WHMat model)
        {
            try
            {
                return new WHMatDAL().Exists(model);
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
        public DataResult<int> Insert(WHMat model)
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
                    result.Msg = "编号已存在";
                    result.Result = -1;
                    return result;
                }
                result.Result = new WHMatDAL().Insert(model);
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
        public DataResult<int> Update(WHMat model)
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
                result.Result = new WHMatDAL().Update(model);
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
                    count += this.Delete(new WHMat { ID = str });
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
        public int Delete(WHMat model)
        {
            try
            {
                return new WHMatDAL().Delete(model);
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
        public DataTable GetExportData(WHMat model)
        {
            try
            {
                return new WHMatDAL().GetExportData(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 条码管理

        /// <summary>
        /// 获取货品唯一识别码列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetMatIDCodeList(WHMat condition, DataPage page)
        {
            return new WHMatDAL().GetMatIDCodeList(condition, page);
        }
        
        /// <summary>
        /// 生成物料唯一识别码
        /// </summary>
        /// <param name="count">个数</param>
        public void BuildMatIDCode(WHMat info,int count)
        {
            string newSeq = new WHMatDAL().GetNewIDCodeSeq(info);

            if (newSeq == null)
            {
                newSeq = "0";
            }

            List<MatIDCode> list=new List<MatIDCode>();

            for (int i = 0; i < count; i++)
            {
                MatIDCode idCode = new MatIDCode();
                idCode.MatID = info.ID;
                idCode.IDCode = info.MatCode + DateTime.Now.ToString("yyyyMMdd").Substring(2) + (int.Parse(newSeq) + (i+1)).ToString("000");
                idCode.BuildDate = DateTime.Now.ToString("yyyyMMdd");
                idCode.Seq = (int.Parse(newSeq) + (i+1));
                idCode.Status = 0;
                idCode.MatSpec = info.SpecCode;

                list.Add(idCode);
            }
            new WHMatDAL().SaveMatIDCode(list);

        }
        
        /// <summary>
        /// 根据货品编号获取货品信息
        /// </summary>
        /// <param name="matCode">货品编号</param>
        /// <returns>货品信息</returns>
        public WHMat GetMatByMatCode(string matCode)
        {
            try
            {
                return new WHMatDAL().GetMatByMatCode(matCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 根据货品唯一识别码获取货品信息
        /// </summary>
        /// <param name="matIDCode">唯一识别码</param>
        /// <returns>货品信息</returns>
        public WHMat GetMatByIDCode(string matIDCode)
        {
            try
            {
                WHMat info = new WHMatDAL().GetMatByIDCode(matIDCode);

                if (info == null)
                    return info;

                //处理货品规格
                List<MatUnit> matUnits = new WHMatSpecBLL().GetMayUnits(info.MatID);
                MatUnit find = matUnits.Find(p => p.ID == info.MatSpecID);
                if (find != null)
                {
                    info.UnitCode = find.ID;
                    info.OperateUnitName = find.Description;
                    info.OperateSpecName = find.Remark;
                }
                //计算核算数量
                List<WHMatSpec> matSpecList = new WHMatSpecBLL().GetList(new WHMatSpec { MatID = info.MatID });

                WHMatSpec findMatSpec = matSpecList.Find(p => p.ID == info.MatSpecID);

                decimal mainUnitAmount = 1;
                while (findMatSpec!=null&&findMatSpec.ChangeUnit != info.UnitCode)
                {
                    mainUnitAmount = mainUnitAmount * findMatSpec.Amount;

                    findMatSpec = matSpecList.Find(p => p.ID == findMatSpec.ChangeUnit);
                    if (findMatSpec == null)
                        break;
                }

                if (findMatSpec != null && findMatSpec.ChangeUnit == info.UnitCode)
                {
                    mainUnitAmount = mainUnitAmount * findMatSpec.Amount;
                }
                info.MainUnitAmount = mainUnitAmount;

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 记录货品唯一识别码打印次数
        /// </summary>
        /// <param name="idCodeList">识别码列表</param>
        public void SignMatIDCodePrintCount(List<string> idCodeList)
        {
            try
            {
                new WHMatDAL().SignMatIDCodePrintCount(idCodeList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除条码
        /// </summary>
        /// <param name="idcode"></param>
        public void DeleteMatIDCode(MatIDCode idcode)
        {
            new WHMatDAL().DeleteMatIDCode(idcode);
        }

        #endregion

        
    }

}
