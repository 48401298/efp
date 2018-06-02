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
    /// 库存移动
    /// </summary>
    public class MoveStockBLL:BaseBLL
    {
        #region 获取库内移动单列表

        /// <summary>
        /// 获取库内移动单列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage GetInsideList(MoveStockBill condition, DataPage page)
        {
            condition.MoveMode = "1";

            return new MoveStockDAL().GetList(condition, page);
        }

        #endregion

        #region 库内移动

        /// <summary>
        /// 库内移动
        /// </summary>
        /// <param name="bill">移动信息</param>
        public void InsideMoveStock(MoveStockBill bill)
        {
            MoveStockDAL msDal = new MoveStockDAL();
            StockDAL sDal = new StockDAL();

            bill.ID = Guid.NewGuid().ToString();
            bill.BillNO = this.GetNewBillNO();
            bill.FromWarehouse = bill.ToWarehouse;
            bill.FromWHHeader = this.LoginUser.UserID;
            bill.ToWHHeader = this.LoginUser.UserID;
            bill.MoveMode = "1";
            bill.CREATEUSER = this.LoginUser.UserID;
            bill.CREATETIME = DateTime.Now;
            bill.UPDATEUSER = bill.CREATEUSER;
            bill.UPDATETIME = bill.CREATETIME;

            foreach (MoveStockDetail detail in bill.Details)
            {
                detail.ID = Guid.NewGuid().ToString();
                detail.FromSaveSite = detail.ToSaveSite;
                detail.BillID = bill.ID;
            }

            List<MoveStockRecord> records = bill.Details.Select(p => new MoveStockRecord() { IDCode = p.IDCode, MatID = p.MatID, ToSaveSite = p.ToSaveSite, ToWarehouse=bill.ToWarehouse }).ToList();

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.OpenTs();
                msDal.Session = session;
                sDal.Session = session;

                msDal.Insert(bill);
                sDal.MoveStock(records);

                session.CommitTs();
            }
        }

        #endregion

        #region 生成新库存移动单号

        /// <summary>
        /// 生成新库存移动单号
        /// </summary>
        /// <returns></returns>
        public string GetNewBillNO()
        {
            string maxNo = new MoveStockDAL().GetMaxBillNO();
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
    }
}
