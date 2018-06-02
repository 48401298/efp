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
    ///　模块名称：货品规格管理
    ///　作    者：
    ///　编写日期：2018年01月05日
    /// </summary>
    public class WHMatSpecBLL:BaseBLL
    {
        #region 获取信息

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public WHMatSpec Get(WHMatSpec model)
        {
            return new WHMatSpecDAL().Get(model);
        }

        #endregion

        #region 获取列表

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public List<WHMatSpec> GetList(WHMatSpec condition)
        {
            return new WHMatSpecDAL().GetList(condition);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>仓库列表</returns>
        public DataPage GetList(WHMatSpec condition, DataPage page)
        {
            page = new WHMatSpecDAL().GetList(condition, page);

            List<WHMatSpec> list = page.Result as List<WHMatSpec>;

            //获取货品规格单位
            List<WHMatSpec> matSpecList = this.GetList(new WHMatSpec { MatID = condition.MatID });

            foreach (WHMatSpec item in list)
            {
                WHMatSpec find = matSpecList.Find(p => p.ID == item.ChangeUnit);
                if (find != null)
                {
                    item.ChangeUnitName = find.UnitName;
                }
            }

            return page;
        }

        #endregion

        #region 获取货品可用单位

        /// <summary>
        /// 获取货品可用单位
        /// </summary>
        /// <param name="matID">货品主键</param>
        /// <returns>货品可用单位</returns>
        public List<MatUnit> GetMayUnits(string matID)
        {
            List<MatUnit> units = new List<MatUnit>();

            //获取货品规格单位
            List<WHMatSpec> matSpecList = this.GetList(new WHMatSpec { MatID = matID });
            foreach (WHMatSpec ms in matSpecList)
            {
                units.Add(new MatUnit { ID = ms.ID, Description = ms.UnitName,Remark=ms.Description });
            }

            //获取主计量单位
            MatUnit mainUnit = new WHMatDAL().GetMainUnit(matID);
            units.Add(new MatUnit { ID = mainUnit.ID, Description = mainUnit.Description, Remark="" });

            return units;
        }

        #endregion

        #region 信息是否重复

        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <returns>true:已存在；fasel:不存在。</returns>
        public bool Exists(WHMatSpec model)
        {
            return new WHMatSpecDAL().Exists(model);
        }

        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息(单表)
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>插入行数</returns>
        public int Insert(WHMatSpec model)
        {
            model.ID = Guid.NewGuid().ToString();
            return new WHMatSpecDAL().Insert(model);
        }

        #endregion

        #region 更新信息

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>更新行数</returns>
        public int Update(WHMatSpec model)
        {
            return new WHMatSpecDAL().Update(model);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name=""></param>
        /// <returns>删除个数</returns>
        public int Delete(WHMatSpec model)
        {
            return new WHMatSpecDAL().Delete(model);
        }

        #endregion
    }
}
