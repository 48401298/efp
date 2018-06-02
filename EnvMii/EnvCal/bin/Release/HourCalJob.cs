using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using EnvCal.Quartz;
using Quartz;
using EnvMII.Service;
using EnvMII.VO;
using LAF.Data;

namespace EnvCal.Quartz
{
    public class HourCalJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            DateTime time = DateTime.Now.AddHours(-1);
            string startTime = time.ToString("yyyy-MM-dd") + " " + time.ToString("HH") + ":00:00";
            string endTime = time.ToString("yyyy-MM-dd") + " " + time.ToString("HH") + ":59:59";

            InspectOriginalData inspectOriginalData = new InspectOriginalData();
            inspectOriginalData.StartTime = startTime;
            inspectOriginalData.EndTime = endTime;

            ////测试数据
            //inspectOriginalData.StartTime = "2018-03-30 21:00:00";
            //inspectOriginalData.EndTime = "2018-03-30 21:59:59";

            //返回结果集。计算最大最小值。平均值
            List<InspectItemData> itemDataList = new InspectDataService().GetItemDatas(inspectOriginalData);

            List<InspectResultData> resultList = new List<InspectResultData>();
            if (itemDataList != null && itemDataList.Count > 0)
            {
                decimal total = 0;
                decimal max = 0;
                decimal min = 0;
                int groupNum = 0;

                string DeviceSN = "";
                foreach (InspectItemData iid in itemDataList)
                {
                    string groupId = iid.DeviceSN + "^" + iid.ItemCode;
                    if (DeviceSN != groupId)
                    {
                        if (DeviceSN != "")
                        {
                            InspectResultData ird = new InspectResultData();
                            ird.ID = Guid.NewGuid().ToString();
                            ird.DeviceCode = DeviceSN.Split('^')[0];
                            ird.ItemCode = DeviceSN.Split('^')[1];
                            ird.MaxDataValue = max;
                            ird.MinDataValue = min;
                            ird.AvgValue = Convert.ToDecimal((total / groupNum).ToString("0.00"));
                            ird.ResultType = "1";
                            ird.UpdateTime = DateTime.Now;
                            ird.InspectTime = DateTime.Parse(inspectOriginalData.StartTime);
                            ird.OrganID = DeviceCache.getOrganIdByCode(ird.DeviceCode);
                            resultList.Add(ird);
                        }
                        max = Convert.ToDecimal(iid.InspectData);
                        min = Convert.ToDecimal(iid.InspectData);
                        groupNum = 0;
                        total = 0;
                        DeviceSN = groupId;
                    }

                    decimal id = 0;
                    try
                    {
                        //非数字格式转换出异常时.此条数据丢弃
                        id = Convert.ToDecimal(iid.InspectData);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                        
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
                    groupNum++;
                }

                InspectResultData ird1 = new InspectResultData();
                ird1.ID = Guid.NewGuid().ToString();
                ird1.DeviceCode = DeviceSN.Split('^')[0];
                ird1.ItemCode = DeviceSN.Split('^')[1];
                ird1.MaxDataValue = max;
                ird1.MinDataValue = min;
                ird1.AvgValue = Convert.ToDecimal((total / groupNum).ToString("0.00"));
                ird1.ResultType = "1";
                ird1.UpdateTime = DateTime.Now;
                ird1.InspectTime = DateTime.Parse(inspectOriginalData.StartTime);
                ird1.OrganID = DeviceCache.getOrganIdByCode(ird1.DeviceCode);
                resultList.Add(ird1);
            }

            Console.WriteLine("每小时定时计算平均值！");

            if (resultList.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            foreach (InspectResultData ird in resultList)
            {
                Console.WriteLine("按小时-设备号：" + ird.DeviceCode + ";项目号：" + ird.ItemCode + ";最大值：" + ird.MaxDataValue + ";最小值" + ird.MinDataValue + ";平均值：" + ird.AvgValue);
            }

            //插入到数据库中

            int count = 0;
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<InspectResultData>(resultList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
