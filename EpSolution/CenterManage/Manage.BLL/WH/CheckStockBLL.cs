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
    /// 库存盘点
    /// </summary>
    public class CheckStockBLL:BaseBLL
    {
        #region 生成盘点单

        /// <summary>
        /// 生成盘点单
        /// </summary>
        /// <param name="condition">生成条件</param>
        /// <returns>盘点单</returns>
        public CheckStockBill BuildBill(CheckStockBill condition)
        {
            CheckStockBill info = new CheckStockDAL().BuildBill(condition);

            
            info.BillNO = this.GetNewBillNO();
            info.Warehouse = condition.Warehouse;
            info.AreaID = condition.AreaID;
            info.CheckHeader = this.LoginUser.UserID;

            int seq = 1;
            foreach (CheckStockDetail detail in info.Details)
            {
                detail.ID = Guid.NewGuid().ToString();
                detail.BillID = info.ID;
                detail.FactAmount = detail.StockAmount;
                detail.Seq = seq;
                seq++;
            }

            return info;
        }

        #endregion

        #region 新增盘点单

        /// <summary>
        /// 新增盘点单
        /// </summary>
        /// <param name="info"></param>
        public void Insert(CheckStockBill info)
        {
            info.ID = Guid.NewGuid().ToString();
            info.BillNO = this.GetNewBillNO();
            info.CREATEUSER = this.LoginUser.UserID;
            info.CREATETIME = DateTime.Now;
            info.UPDATEUSER = this.LoginUser.UserID;
            info.UPDATETIME = info.CREATETIME;

            int seq = 1;
            foreach (CheckStockDetail detail in info.Details)
            {
                detail.ID = Guid.NewGuid().ToString();
                detail.BillID = info.ID;
                detail.Seq = seq;
                seq++;
            }

            new CheckStockDAL().Insert(info);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name=""></param>
        /// <returns>删除个数</returns>
        public int Delete(CheckStockBill info)
        {
            try
            {
                return new CheckStockDAL().Delete(info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 更新盘点单

        /// <summary>
        /// 更新盘点单
        /// </summary>
        /// <param name="info"></param>
        public void Update(CheckStockBill info)
        {
            info.UPDATEUSER = this.LoginUser.UserID;
            info.UPDATETIME = info.CREATETIME;

            int seq = 1;
            foreach (CheckStockDetail detail in info.Details)
            {
                detail.ID = Guid.NewGuid().ToString();
                detail.BillID = info.ID;
                detail.Seq = seq;
                seq++;
            }

            CheckStockDAL dal = new CheckStockDAL();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                dal.Session = session;
                dal.Update(info);
            }
        }

        #endregion

        #region 盘点确认

        /// <summary>
        /// 盘点确认
        /// </summary>
        /// <param name="info">盘点单</param>
        public void ConfirmCheck(CheckStockBill info)
        {
            CheckStockDAL dal = new CheckStockDAL();
            StockDAL sDal = new StockDAL();

            int seq = 1;
            foreach (CheckStockDetail detail in info.Details)
            {
                detail.ID = Guid.NewGuid().ToString();
                detail.BillID = info.ID;
                detail.Seq = seq;
                seq++;
            }

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.OpenTs();

                dal.Session = session;
                sDal.Session = session;

                //更新盘点单信息
                info.IsConfirm = 1;
                dal.Update(info);

                foreach(CheckStockDetail detail in info.Details)
                {
                    //更新库存信息
                    WHMatAmount matStock = sDal.GetStock(new WHMatAmount() {
                        Warehouse = info.Warehouse
                        , SaveSite=detail.SaveSite
                        ,MatID=detail.MatID
                        ,MatBarCode=detail.IDCode
                    });

                    matStock.MainAmount = matStock.MainAmount * detail.FactAmount / matStock.ProductAmount;
                    matStock.ProductAmount = detail.FactAmount;

                    sDal.Update(matStock);
                }

                session.CommitTs();
            }
        }

        #endregion

        #region 获取盘点单信息

        /// <summary>
        /// 获取盘点单信息
        /// </summary>
        /// <param name="info">入库信息</param>
        /// <returns></returns>
        public CheckStockBill GetInfo(CheckStockBill info)
        {
            return new CheckStockDAL().GetInfo(info);
        }

        #endregion

        #region 获取盘点单列表

        /// <summary>
        /// 获取盘点单列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(CheckStockBill condition, DataPage page)
        {
            return new CheckStockDAL().GetList(condition, page);
        }

        #endregion

        #region 生成新盘点单号

        /// <summary>
        /// 生成新盘点单号
        /// </summary>
        /// <returns></returns>
        public string GetNewBillNO()
        {
            string maxNo = new CheckStockDAL().GetMaxBillNO();
            string newNo = DateTime.Now.ToString("yyyyMMdd");

            if (string.IsNullOrEmpty(maxNo) || maxNo.Length < 12)
            {
                newNo = newNo + "0001";
            }
            else
            {
                if (newNo == maxNo.Substring(0, 8))
                {
                    newNo = (double.Parse(maxNo) + 1).ToString();
                }
                else
                {
                    newNo = newNo + "0001";
                }
            }

            return newNo;
        }

        #endregion

        #region 获取盘点数量列表

        public List<CheckStockQueryResult> GetCheckAmountList(CheckStockQueryResult condition)
        {
            return new CheckStockDAL().GetCheckAmountList(condition);
        }

        #endregion
    }
}
