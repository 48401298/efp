using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using EnvCal.Quartz;
using System.Collections.Specialized;
using System.Configuration;
using LAF.MongoDBClient;
using EnvMII.VO;
using LAF.Data;
using System.Collections.Concurrent;

namespace EnvCal
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("定时计算程序执行开始!");

            //初始化数据库连接串
            ConnectionManager.MongodbConectionStr = ConfigurationManager.AppSettings["MongodbConectionStr"];

            //装配数据工厂
            LAF.Data.DataFactory.Configure(true);

            #region 获取设备缓存信息
            //获取所有设备列表放入到字典表中
            try
            {
                List<DeviceInfo> deviceList = null;

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = "SELECT * FROM deviceinfo T ORDER BY DeviceCode";
                    List<DataParameter> dataParameter = new List<DataParameter>();
                    //插入基本信息
                    deviceList = session.GetList<DeviceInfo>(sql, dataParameter.ToArray()).ToList<DeviceInfo>();
                }

                if (deviceList != null)
                {
                    if (DeviceCache.deviceCacheList == null)
                    {
                        DeviceCache.deviceCacheList = new ConcurrentDictionary<String, DeviceInfo>();
                    }

                    foreach (DeviceInfo di in deviceList)
                    {
                        DeviceCache.deviceCacheList.TryAdd(di.DeviceSN, di);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion


            // 从工厂里获取调度实例
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //按小时执行
            // 创建一个任务
            IJobDetail hourJob = JobBuilder.Create<HourCalJob>()
                .WithIdentity("hourJob", "hourGroup")
                .Build();

            //*/20 * * * * ? 每20秒执行一次
            // 创建一个触发器
            ITrigger hourTrigger = TriggerBuilder.Create()
                .WithIdentity("hourTrigger", "hourGroup").WithCronSchedule("0 10 */1 * * ?") //0 10 */1 * * ?每小时的30分执行执行
                .StartNow() // 无限次执行
                .Build();

            scheduler.ScheduleJob(hourJob, hourTrigger);

            //按天执行
            // 创建一个任务
            IJobDetail dayJob = JobBuilder.Create<DayCalJob>()
                .WithIdentity("dayJob", "dayGroup")
                .Build();

            // 创建一个触发器
            ITrigger dayTrigger = TriggerBuilder.Create()
                .WithIdentity("dayTrigger", "dayGroup").WithCronSchedule("0 0 1 * * ?") //每天1点"0 0 1 * * ?"
                .StartNow()
                .Build();

            scheduler.ScheduleJob(dayJob, dayTrigger);

            //按月执行
            // 创建一个任务
            IJobDetail monthJob = JobBuilder.Create<MonthCalJob>()
                .WithIdentity("monthJob", "monthGroup")
                .Build();

            // 创建一个触发器
            ITrigger monthTrigger = TriggerBuilder.Create()
                .WithIdentity("monthTrigger", "monthGroup").WithCronSchedule("0 0 1 1 * ?") //每月1号1点执行"0 0 1 1 * ?"
                .StartNow()
                .Build();

            scheduler.ScheduleJob(monthJob, monthTrigger);

            //更新设备缓存
            // 创建一个任务
            IJobDetail cacheJob = JobBuilder.Create<CacheJob>()
                .WithIdentity("cacheJob", "cacheGroup")
                .Build();

            // 创建一个触发器
            ITrigger cacheTrigger = TriggerBuilder.Create()
                .WithIdentity("cacheTrigger", "cacheGroup").WithCronSchedule("*/10 * * * * ?") //每10分钟更新一次缓存
                .StartNow()
                .Build();

            scheduler.ScheduleJob(cacheJob, cacheTrigger); 

            Console.ReadKey();
            Console.WriteLine("定时计算程序执行结束!");
        }
    }
}
