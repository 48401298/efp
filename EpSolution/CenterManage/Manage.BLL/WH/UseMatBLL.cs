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
using Manage.Entity.MES;
using Manage.BLL.MES;

namespace Manage.BLL.WH
{
    /// <summary>
    /// 领料管理
    /// </summary>
    public class UseMatBLL:BaseBLL
    {
        #region 生成领料单

        public UseMatBill Build(string supplyID)
        {
            UseMatBill bill = new UseMatBill();

            SupplyInfo supply = new SupplyInfo() {PID=supplyID };

            //获取要货信息
            supply = new SupplyInfoBLL().Get(supply);
            supply.Details = new SupplyInfoBLL().GetList(supply.PID);

            bill.PID = Guid.NewGuid().ToString();
            bill.SUPPLYID = supply.PID;
            bill.Amounts = new List<UseMatAmount>();
            bill.Details = new List<UseMatDetail>();

            UseMatDAL umDAL = new UseMatDAL();

            foreach (SupplyMaterialInfo detail in supply.Details)
            {
                bill.Amounts.Add(new UseMatAmount { PID = Guid.NewGuid().ToString(), USEID = bill.PID, MATRIALID = detail.MATRIALID, AMOUNT = detail.AMOUNT, Unit = detail.Unit });
                //获取可用库存
                List<WHMatAmount> stockList = new UseMatDAL().GetMayUseStock(new WHMatAmount { Warehouse = supply.Warehouse, MatID = detail.MATRIALID });

                if (stockList.Count == 0)
                    continue;

                int index = 0;
                while (detail.AMOUNT > 0)
                {
                    if (index > stockList.Count - 1)
                        break;

                    detail.AMOUNT = detail.AMOUNT - Convert.ToInt32(stockList[0].MainAmount);

                    //更新库存
                    stockList[index].MainAmount = 0;
                    stockList[index].ProductAmount = 0;

                    bill.Details.Add(new UseMatDetail
                    {
                        PID = Guid.NewGuid().ToString(),
                        USEID = bill.PID,
                        SaveSite = stockList[index].SaveSite,
                        MatBarCode = stockList[index].MatBarCode,
                        MATRIALID = stockList[index].MatID,
                        AMOUNT = stockList[index].MainAmount,
                        Unit = stockList[index].Unit
                    });

                    index++;
                }

            }

            this.Insert(bill);

            return bill;
        }

        #endregion

        #region 获取领料单信息

        /// <summary>
        /// 获取领料单信息
        /// </summary>
        /// <param name="">条件</param>
        /// <returns>*信息</returns>
        public UseMatBill Get(UseMatBill model)
        {
            return new UseMatDAL().Get(model);
        }

        #endregion

        #region 新增领料单

        public void Insert(UseMatBill bill)
        {
            bill.PID = Guid.NewGuid().ToString();
            bill.CREATEUSER = this.LoginUser.UserID;
            bill.CREATETIME = DateTime.Now;
            bill.UPDATEUSER = this.LoginUser.UserID;
            bill.UPDATETIME = DateTime.Now;
            foreach (UseMatAmount item in bill.Amounts)
            {
                item.USEID = bill.PID;
            }

            foreach (UseMatDetail item in bill.Details)
            {
                item.USEID = bill.PID;
            }

            new UseMatDAL().Insert(bill);
        }

        #endregion

        #region 更新领料单

        public void Update(UseMatBill bill)
        {
            bill.UPDATEUSER = this.LoginUser.UserID;
            bill.UPDATETIME = DateTime.Now;
            foreach(UseMatAmount item in bill.Amounts)
            {
                item.USEID=bill.PID;
            }

            foreach (UseMatDetail item in bill.Details)
            {
                item.USEID=bill.PID;
            }

            new UseMatDAL().Update(bill);
        }

        #endregion

        #region 删除领料单

        public void Delete(UseMatBill bill)
        {
            new UseMatDAL().Delete(bill);
        }

        #endregion

        #region 获取领料单列表

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(UseMatBill condition, DataPage page)
        {
            return new UseMatDAL().GetList(condition, page);
        }

        #endregion
    }
}
