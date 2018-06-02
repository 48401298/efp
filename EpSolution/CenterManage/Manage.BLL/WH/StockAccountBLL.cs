using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Entity.WH;
using Manage.DAL.WH;
using LAF.BLL;
using LAF.Data;

namespace Manage.BLL.WH
{
    /// <summary>
    /// 库存台账管理
    /// </summary>
    public class StockAccountBLL:BaseBLL
    {
        #region 计算月台账

        public void ComputeAccount()
        {
            try
            {
                DateTime dataEndDate = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));

                int year = dataEndDate.Year;
                int month = dataEndDate.Month;
                string startDate = dataEndDate.ToString("yyyy-MM-01");
                string endDate = DateTime.Parse(startDate).AddMonths(1).ToString("yyyy-MM-dd");

                //获取仓库列表
                List<Warehouse> whList = new WarehouseBLL().GetList();

                //获取货品列表
                List<WHMat> matList = new WHMatBLL().GetList();

                //获取月台账列表
                List<WHMonthAccount> accountList = new StockAccountDAL().GetList(new WHMonthAccount { AccountYear = year, AccountMonth = month });

                //获取入库数量列表
                List<InStockQueryResult> inAmountList = new InStockQueryBLL().GetInAmountList(new InStockQueryResult { StartDate = startDate, EndDate = endDate });

                //获取出库数量列表
                List<OutStockQueryResult> outAmountList = new OutStockQueryBLL().GetOutAmountList(new OutStockQueryResult { StartDate = startDate, EndDate = endDate });

                //获取盘点数量列表
                List<CheckStockQueryResult> checkAmountList = new CheckStockBLL().GetCheckAmountList(new CheckStockQueryResult { StartDate = startDate, EndDate = endDate });

                //按仓库+货品逐个计算
                foreach (Warehouse wh in whList)
                {
                    foreach (WHMat mat in matList)
                    {
                        WHMonthAccount currentAccount = accountList.Find(p => p.Warehouse == wh.ID && p.MatID == mat.ID && p.AccountYear == year && p.AccountMonth == month);

                        if (currentAccount == null)
                        {
                            //无当前月份台账
                            currentAccount = new WHMonthAccount();

                            //初始设置
                            currentAccount.Warehouse = wh.ID;
                            currentAccount.MatID = mat.ID;
                            currentAccount.MainUnitID = mat.UnitCode;
                            currentAccount.AccountYear = year;
                            currentAccount.AccountMonth = month;

                            //获取上月台帐
                            int prevYear = dataEndDate.AddMonths(-1).Year;
                            int prevMonth = dataEndDate.AddMonths(-1).Month;

                            WHMonthAccount prevAccount = new StockAccountDAL().Get(new WHMonthAccount { Warehouse = wh.ID, MatID = mat.ID, AccountYear = prevYear, AccountMonth = prevMonth });

                            if (prevAccount != null)
                            {
                                currentAccount.PrimeAmount = prevAccount.LateAmount;
                            }
                        }

                        //计算入库数量
                        InStockQueryResult inAmount = inAmountList.Find(p => p.WarehouseID == wh.ID && p.MatID == mat.ID);
                        currentAccount.InAmount = inAmount != null ? inAmount.Amount : 0;

                        //计算入库数量
                        OutStockQueryResult outAmount = outAmountList.Find(p => p.WarehouseID == wh.ID && p.MatID == mat.ID);
                        currentAccount.OutAmount = outAmount != null ? outAmount.Amount : 0;

                        //计算盘点数量
                        CheckStockQueryResult checkAmount = checkAmountList.Find(p => p.WarehouseID == wh.ID && p.MatID == mat.ID);
                        currentAccount.GainAmount = checkAmount != null ? checkAmount.ProfitAmount : 0;
                        currentAccount.LossAmount = checkAmount != null ? checkAmount.LossAmount : 0;

                        //计算期末余额
                        currentAccount.LateAmount = currentAccount.PrimeAmount + currentAccount.InAmount - currentAccount.OutAmount + currentAccount.GainAmount - currentAccount.LossAmount;

                        if (string.IsNullOrEmpty(currentAccount.ID) == false)
                        {
                            //更新
                            //currentAccount.UPDATEUSER = this.LoginUser.UserID;
                            currentAccount.UPDATETIME = currentAccount.CREATETIME;
                            new StockAccountDAL().Update(currentAccount);
                        }
                        else
                        {
                            if (currentAccount.LateAmount == 0
                                && currentAccount.InAmount == 0
                                && currentAccount.OutAmount == 0
                                && currentAccount.GainAmount == 0
                                && currentAccount.LossAmount == 0
                                && currentAccount.LateAmount == 0)
                            {
                                continue;
                            }
                            //插入
                            currentAccount.ID = Guid.NewGuid().ToString();
                            //currentAccount.CREATEUSER = this.LoginUser.UserID;
                            currentAccount.CREATETIME = DateTime.Now;
                            //currentAccount.UPDATEUSER = this.LoginUser.UserID;
                            currentAccount.UPDATETIME = currentAccount.CREATETIME;
                            new StockAccountDAL().Insert(currentAccount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region 库存台账查询

        /// <summary>
        /// 库存台账查询
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataPage QueryMonthAccount(WHMonthAccount condition, DataPage page)
        {
            if (this.LoginUser.UserName.ToLower() != "admin")
            {
                condition.UserID = this.LoginUser.UserID;
            }
            return new StockAccountDAL().QueryMonthAccount(condition, page);
        }

        #endregion
    }
}
