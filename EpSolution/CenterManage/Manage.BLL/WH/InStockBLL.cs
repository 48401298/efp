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
    /// 入库管理
    /// </summary>
    public class InStockBLL:BaseBLL
    {

        #region 判断入库单号是否存在

        /// <summary>
        /// 判断入库单号是否存在
        /// </summary>
        /// <param name="billNO">入库单号</param>
        /// <returns>true:存在;false:不存在</returns>
        public bool ExistsBillNO(string billNO)
        {
            return new InStockDAL().ExistsBillNO(billNO);
        }

        #endregion

        #region 获取入库单列表

        /// <summary>
        /// 获取入库信息列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetList(InStockBill condition,DataPage page)
        {
            if (this.LoginUser.UserName.ToLower() != "admin")
            {
                condition.CREATEUSER = this.LoginUser.UserID;
            }
            return new InStockDAL().GetList(condition,page);
        }

        #endregion

        #region 获取入库单

        /// <summary>
        /// 获取入库单
        /// </summary>
        /// <param name="info">入库信息</param>
        /// <returns></returns>
        public InStockBill GetInfo(InStockBill info)
        {
            return new InStockDAL().GetInfo(info);
        }

        #endregion

        #region 原材料入库

        /// <summary>
        /// 原材料入库
        /// </summary>
        /// <param name="bill">入库单</param>
        /// <returns></returns>
        public void MLInStorage(InStockBill bill)
        {
            //设置基本信息
            bill.BillNO = this.GetNewBillNO();
            bill.BillDate = DateTime.Now;
            //bill.InStockMode = "ml";
            bill.Receiver = this.LoginUser.UserID;

            InStorage(bill);

        }

        #endregion

        #region 产成品入库

        /// <summary>
        /// 产成品入库
        /// </summary>
        /// <param name="bill">入库单</param>
        /// <returns></returns>
        public void FGInStorage(InStockBill bill)
        {
            //设置基本信息
            bill.BillNO = this.GetNewBillNO();
            bill.BillDate = DateTime.Now;
            bill.InStockMode = "fg";
            bill.Receiver = this.LoginUser.UserID;

            InStorage(bill);

        }

        #endregion

        #region 入库(货品编号)

        /// <summary>
        /// 入库(货品编号)
        /// </summary>
        /// <param name="bill">入库单</param>
        /// <returns></returns>
        public void InMatStorage(InStockBill bill)
        {
            //设置基本信息
            bill.BillNO = this.GetNewBillNO();
            bill.BillDate = DateTime.Now;
            bill.Receiver = this.LoginUser.UserID;
            
            bill.ID = Guid.NewGuid().ToString();
            bill.CREATEUSER = this.LoginUser.UserID;
            bill.CREATETIME = DateTime.Now;
            bill.UPDATEUSER = this.LoginUser.UserID;
            bill.UPDATETIME = bill.CREATETIME;

            int seq = 1;
            foreach (InStockDetail detail in bill.Details)
            {
                detail.ID = Guid.NewGuid().ToString();
                detail.BillID = bill.ID;
                detail.InAmount = detail.MainUnitAmount;
                detail.CREATEUSER = bill.CREATEUSER;
                detail.CREATETIME = bill.CREATETIME;
                detail.UPDATEUSER = bill.UPDATEUSER;
                detail.UPDATETIME = bill.UPDATETIME;
                detail.Seq = seq;
                seq++;
            }

            StockDAL stockDal = new StockDAL();
            InStockDAL isDal = new InStockDAL();

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                try
                {
                    stockDal.Session = session;
                    isDal.Session = session;

                    foreach (InStockDetail detail in bill.Details)
                    {
                        WHMatAmount inStock = new WHMatAmount();
                        inStock.Warehouse = bill.Warehouse;
                        inStock.SaveSite = detail.SaveSite;
                        inStock.MatBarCode = detail.MatBarCode;
                        inStock.MatID = detail.MatID;
                        inStock.ProductAmount = detail.InAmount;
                        inStock.ProductPrice = detail.InPrice;
                        inStock.ProductSum = detail.InSum;
                        inStock.Unit = detail.UnitCode;
                        inStock.CreateUser = this.LoginUser.UserID;
                        inStock.UpdateUser = this.LoginUser.UserID;
                        inStock.MainAmount = detail.MainUnitAmount;

                        stockDal.InStock(inStock);
                    }

                    isDal.Insert(bill);

                    session.CommitTs();
                }
                catch (Exception ex)
                {
                    session.RollbackTs();
                    throw ex;
                }
            }

        }

        #endregion

        #region 入库

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="bill">入库单</param>
        /// <returns></returns>
        public string InStorage(InStockBill bill)
        {
            string result = "";

            bill.ID = Guid.NewGuid().ToString();
            bill.CREATEUSER = this.LoginUser.UserID;
            bill.CREATETIME = DateTime.Now;
            bill.UPDATEUSER = this.LoginUser.UserID;
            bill.UPDATETIME = bill.CREATETIME;

            int seq = 1;
            foreach (InStockDetail detail in bill.Details)
            {
                detail.ID = Guid.NewGuid().ToString();
                detail.BillID = bill.ID;
                detail.CREATEUSER = bill.CREATEUSER;
                detail.CREATETIME = bill.CREATETIME;
                detail.UPDATEUSER = bill.UPDATEUSER;
                detail.UPDATETIME = bill.UPDATETIME;
                detail.Seq = seq;
                seq++;
            }

            StockDAL stockDal = new StockDAL();
            InStockDAL isDal = new InStockDAL();

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                try
                {
                    stockDal.Session = session;
                    isDal.Session = session;

                    foreach (InStockDetail detail in bill.Details)
                    {
                        WHMatAmount inStock = new WHMatAmount();
                        inStock.Warehouse = bill.Warehouse;
                        inStock.SaveSite = detail.SaveSite;
                        inStock.MatBarCode = detail.MatBarCode;
                        inStock.MatID = detail.MatID;
                        inStock.ProductAmount = detail.InAmount;
                        inStock.ProductPrice = detail.InPrice;
                        inStock.ProductSum = detail.InSum;
                        inStock.Unit = detail.UnitCode;
                        inStock.MainAmount = detail.MainUnitAmount;

                        if (string.IsNullOrEmpty(detail.MatBarCode)==false&&string.IsNullOrEmpty(detail.ProduceDate) == false)
                        {
                            inStock.ProduceDate = DateTime.Parse(detail.ProduceDate);
                            //更新条码信息的生产日期
                            new WHMatDAL().UpdateIDCodeProduceDate(new MatIDCode { IDCode = detail.MatBarCode, ProduceDate = detail.ProduceDate });
                        }


                        stockDal.InStock(inStock);
                    }

                    isDal.Insert(bill);

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

        #region 更新入库单信息

        /// <summary>
        /// 更新入库单信息
        /// </summary>
        /// <param name="info"></param>
        public void Update(InStockBill info)
        {
            InStockDAL isDal = new InStockDAL();
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                isDal.Session = session;
                isDal.Update(info);
            }
        }

        #endregion

        #region 生成新入库单号

        /// <summary>
        /// 生成新入库单号
        /// </summary>
        /// <returns></returns>
        public string GetNewBillNO()
        {
            string maxNo = new InStockDAL().GetMaxBillNO();
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

        #region 获取入库单浏览信息

        /// <summary>
        /// 获取入库单浏览信息
        /// </summary>
        /// <param name="info">获取条件</param>
        /// <returns>入库单浏览信息</returns>
        public InStockBillView GetViewInfo(InStockBillView info)
        {
            return new InStockDAL().GetViewInfo(info);
        }

        #endregion

    }
}
