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
    /// 出库管理
    /// </summary>
    public class OutStockBLL : BaseBLL
    {
        #region 判断出库单号是否存在

        /// <summary>
        /// 判断出库单号是否存在
        /// </summary>
        /// <param name="billNO">入库单号</param>
        /// <returns>true:存在;false:不存在</returns>
        public bool ExistsBillNO(string billNO)
        {
            return new OutStockDAL().ExistsBillNO(billNO);
        }

        #endregion

        #region 获取出库信息列表

        /// <summary>
        /// 获取出库信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(OutStockBill condition, DataPage page)
        {
            if (this.LoginUser.UserName.ToLower() != "admin")
            {
                condition.CREATEUSER = this.LoginUser.UserID;
            }
            return new OutStockDAL().GetList(condition,page);
        }

        #endregion

        #region 获取出库信息

        /// <summary>
        /// 获取出库单
        /// </summary>
        /// <param name="info">出库信息</param>
        /// <returns></returns>
        public OutStockBill GetInfo(OutStockBill info)
        {
            return new OutStockDAL().GetInfo(info);
        }

        #endregion

        #region 原材料出库

        /// <summary>
        /// 原材料出库
        /// </summary>
        /// <param name="bill">出库单</param>
        /// <returns></returns>
        public string MLOutStorage(OutStockBill bill)
        {
            bill.ID = Guid.NewGuid().ToString();
            bill.BillNO = this.GetNewBillNO();
            bill.BillDate = DateTime.Now;
            //bill.OutStockMode = "ml";
            bill.WHHeader = this.LoginUser.UserID;  

            return OutStorage(bill);
        }

        #endregion

        #region 产成品出库

        /// <summary>
        /// 产成品出库
        /// </summary>
        /// <param name="bill">出库单</param>
        /// <returns></returns>
        public string FGOutStorage(OutStockBill bill)
        {
            bill.ID = Guid.NewGuid().ToString();
            bill.BillNO = this.GetNewBillNO();
            bill.BillDate = DateTime.Now;
            bill.OutStockMode = "fg";
            bill.WHHeader = this.LoginUser.UserID;

            return OutStorage(bill);
        }

        #endregion

        #region 出库(货品编号)

        /// <summary>
        /// 出库(货品编号)
        /// </summary>
        /// <param name="bill">出库单</param>
        /// <returns></returns>
        public string OutMatStorage(OutStockBill bill)
        {
            bill.ID = Guid.NewGuid().ToString();
            bill.BillNO = this.GetNewBillNO();
            bill.BillDate = DateTime.Now;
            bill.WHHeader = this.LoginUser.UserID;

            string result = "";

            bill.ID = Guid.NewGuid().ToString();
            bill.CREATEUSER = this.LoginUser.UserID;
            bill.CREATETIME = DateTime.Now;
            bill.UPDATEUSER = this.LoginUser.UserID;
            bill.UPDATETIME = bill.CREATETIME;

            //设置明细
            int seq = 1;
            foreach (OutStockDetail detail in bill.Details)
            {
                detail.ID = Guid.NewGuid().ToString("N");
                detail.Seq = seq;
                seq++;
                detail.BillID = bill.ID;
                detail.IDCode = "";
                detail.OutAmount = detail.MainUnitAmount;
                detail.CREATEUSER = bill.CREATEUSER;
                detail.CREATETIME = bill.CREATETIME;
                detail.UPDATEUSER = bill.UPDATEUSER;
                detail.UPDATETIME = bill.UPDATETIME;
            }

            StockDAL stockDal = new StockDAL();
            OutStockDAL outDal = new OutStockDAL();

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                try
                {
                    stockDal.Session = session;
                    outDal.Session = session;

                    foreach (OutStockDetail detail in bill.Details)
                    {
                        WHMatAmount outStock = new WHMatAmount();
                        outStock.Warehouse = bill.Warehouse;
                        outStock.SaveSite = detail.SaveSite;
                        outStock.MatBarCode = detail.IDCode;
                        outStock.MatID = detail.MatID;
                        outStock.ProductAmount = detail.OutAmount;
                        outStock.ProductPrice = detail.OutPrice;
                        outStock.ProductSum = detail.OutSum;
                        outStock.Unit = detail.UnitCode;
                        outStock.MainAmount = detail.MainUnitAmount;
                        outStock.UpdateUser = this.LoginUser.UserID;
                        outStock.UpdateTime = DateTime.Now;

                        result = stockDal.OutStock(outStock);

                        if (result != "")
                            break;
                    }

                    if (result != "")
                    {
                        session.RollbackTs();

                        return result;
                    }

                    outDal.Insert(bill);

                    session.CommitTs();
                }
                catch (Exception ex)
                {
                    session.RollbackTs();
                    throw ex;
                }
            }

            return result;
        }

        #endregion

        #region 出库

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="bill">出库单</param>
        /// <returns></returns>
        public string OutStorage(OutStockBill bill)
        {
            string result = "";

            bill.ID = Guid.NewGuid().ToString();
            bill.CREATEUSER = this.LoginUser.UserID;
            bill.CREATETIME = DateTime.Now;
            bill.UPDATEUSER = this.LoginUser.UserID;
            bill.UPDATETIME = bill.CREATETIME;

            //设置明细
            int seq = 1;
            foreach (OutStockDetail detail in bill.Details)
            {
                detail.ID = Guid.NewGuid().ToString("N");
                detail.Seq = seq;
                seq++;
                detail.BillID = bill.ID;
                detail.CREATEUSER = bill.CREATEUSER;
                detail.CREATETIME = bill.CREATETIME;
                detail.UPDATEUSER = bill.UPDATEUSER;
                detail.UPDATETIME = bill.UPDATETIME;
            }

            StockDAL stockDal = new StockDAL();
            OutStockDAL outDal = new OutStockDAL();

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                try
                {
                    stockDal.Session = session;
                    outDal.Session = session;

                    foreach (OutStockDetail detail in bill.Details)
                    {
                        WHMatAmount outStock = new WHMatAmount();
                        outStock.Warehouse = bill.Warehouse;
                        outStock.SaveSite = detail.SaveSite;
                        outStock.MatBarCode = detail.IDCode;
                        outStock.MatID = detail.MatID;
                        outStock.ProductAmount = detail.OutAmount;
                        outStock.ProductPrice = detail.OutPrice;
                        outStock.ProductSum = detail.OutSum;
                        outStock.Unit = detail.UnitCode;
                        outStock.MainAmount = detail.MainUnitAmount;

                        result = stockDal.OutStock(outStock);

                        if (result != "")
                            break;
                    }

                    if (result != "")
                    {
                        session.RollbackTs();

                        return result;
                    }

                    outDal.Insert(bill);

                    session.CommitTs();
                }
                catch (Exception ex)
                {
                    session.RollbackTs();
                    throw ex;
                }
            }

            return result;
        }

        #endregion

        #region 生成新出库单号

        /// <summary>
        /// 生成新出库单号
        /// </summary>
        /// <returns></returns>
        public string GetNewBillNO()
        {
            string maxNo = new OutStockDAL().GetMaxBillNO();
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

        #region 获取出库单浏览信息

        /// <summary>
        /// 获取出库单浏览信息
        /// </summary>
        /// <param name="info">获取条件</param>
        /// <returns>入库单浏览信息</returns>
        public OutStockBillView GetViewInfo(OutStockBillView info)
        {
            return new OutStockDAL().GetViewInfo(info);
        }

        #endregion

         #region 更新出库单信息

        /// <summary>
        /// 更新出库单信息
        /// </summary>
        /// <param name="info"></param>
        public void Update(OutStockBill info)
        {
            new OutStockDAL().Update(info);
        }

        #endregion
    }
}
