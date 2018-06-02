using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using EnvCal.Quartz;
using Quartz;
using EnvMII.VO;
using EnvMII.Service;

namespace EnvCal.Quartz
{
    public class MonthCalJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            DateTime time = DateTime.Now.AddMonths(-1);
            DateTime stime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM" + "-01"));
            DateTime eTime = stime.AddMonths(1).AddSeconds(-1);
            string startTime = stime.ToString("yyyy-MM-dd") + " " + "00:00:00";
            string endTime = eTime.ToString("yyyy-MM-dd") + " " + "23:59:59";

            InspectOriginalData inspectOriginalData = new InspectOriginalData();
            inspectOriginalData.StartTime = startTime;
            inspectOriginalData.EndTime = endTime;

            ////测试数据
            //inspectOriginalData.StartTime = "2018-03-23 00:00:00";
            //inspectOriginalData.EndTime = "2018-03-23 23:59:59";

            //返回结果集。计算最大最小值。平均值
            List<InspectItemData> itemDataList = new InspectDataService().GetItemDatas(inspectOriginalData);

            List<InspectResultData> resultList = new List<InspectResultData>();
            if (itemDataList != null && itemDataList.Count > 0)
            {
                decimal total = 0;
                decimal max = 0;
                decimal min = 0;

                string DeviceSN = "";
                foreach (InspectItemData iid in itemDataList)
                {
                    string groupId = iid.DeviceSN + "_" + iid.ItemCode;
                    if (DeviceSN != groupId)
                    {
                        if (DeviceSN != "")
                        {
                            InspectResultData ird = new InspectResultData();
                            ird.ID = Guid.NewGuid().ToString();
                            ird.DeviceCode = DeviceSN.Split('_')[0];
                            ird.ItemCode = DeviceSN.Split('_')[1];
                            ird.MaxDataValue = max;
                            ird.MinDataValue = min;
                            ird.AvgValue = Convert.ToDecimal((total / itemDataList.Count).ToString("0.00"));
                            ird.ResultType = "3";
                            ird.UpdateTime = DateTime.Now;
                            ird.InspectTime = DateTime.Parse(inspectOriginalData.StartTime.Substring(0, 14).Replace("T", ""));
                            ird.OrganID = DeviceCache.getOrganIdByCode(ird.DeviceCode);
                            resultList.Add(ird);
                        }
                        max = 0;
                        min = 0;
                        total = 0;
                        DeviceSN = groupId;
                    }

                    decimal id = Convert.ToDecimal(iid.InspectData);
                    //计算最大值
                    if (id > max)
                    {
                        max = id;
                    }
                    //计算最小值
                    if (id < min)
                    {
                        min = id;
                    }
                    //汇总
                    total += id;
                }

                InspectResultData ird1 = new InspectResultData();
                ird1.ID = Guid.NewGuid().ToString();
                ird1.DeviceCode = DeviceSN.Split('_')[0];
                ird1.ItemCode = DeviceSN.Split('_')[1];
                ird1.MaxDataValue = max;
                ird1.MinDataValue = min;
                ird1.AvgValue = Convert.ToDecimal((total / itemDataList.Count).ToString("0.00"));
                ird1.ResultType = "3";
                ird1.UpdateTime = DateTime.Now;
                ird1.InspectTime = DateTime.Parse(inspectOriginalData.StartTime.Substring(0, 14).Replace("T", ""));
                ird1.OrganID = DeviceCache.getOrganIdByCode(ird1.DeviceCode);
                resultList.Add(ird1);
            }

            Console.WriteLine("每月定时计算平均值！");

            foreach (InspectResultData ird in resultList)
            {
                //插入到数据库
                Console.WriteLine("按月-设备号：" + ird.DeviceCode + ";项目号：" + ird.ItemCode + ";最大值：" + ird.MaxDataValue + ";最小值" + ird.MinDataValue + ";平均值：" + ird.AvgValue);
            }
        }
    }
}
