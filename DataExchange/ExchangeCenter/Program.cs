using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using ExchangeEntity;
using LAF.Data;
using LAF.MongoDBClient;
using System.Collections.Concurrent;
using ExchangeCenter.Quartz;
using System.Configuration;

namespace ExchangeCenter
{
    class Program
    {
        static void Main(string[] args)
        {

            //初始化数据库连接串
            ConnectionManager.MongodbConectionStr = ConfigurationManager.AppSettings["MongodbConectionStr"];

            //装配数据工厂
            LAF.Data.DataFactory.Configure(true);

            //每次获取记录数据
            //string eachHandleCount = ConfigurationManager.AppSettings["eachHandleCount"];

            //每次获取数据的时间间隔
            string eachHandleTime = ConfigurationManager.AppSettings["eachHandleTime"];

            //默认转发最后时间值.一分钟之前
            string eachHandleLastTime = ConfigurationManager.AppSettings["eachHandleLastTime"];
            
            
            ////每次查询记录的数量
            //DataExchangeJob.eachHandleCount = Convert.ToInt32(eachHandleCount == null ? "100" : eachHandleCount);

            DataExchangeJob.eachHandleTime = Convert.ToInt32(eachHandleTime == null ? "600000" : eachHandleTime);

            DataExchangeJob.eachHandleLastTime = Convert.ToInt32(eachHandleLastTime == null ? "1000" : eachHandleLastTime);

            #region 获取所有企业列表
            //获取所有企业列表放入缓存中
            try
            {
                List<Orgaization> organList = null;
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = "SELECT * FROM aifishingep.t_organization T WHERE T.DELFLAG = '0'";
                    List<DataParameter> dataParameter = new List<DataParameter>();
                    //插入基本信息
                    organList = session.GetList<Orgaization>(sql, dataParameter.ToArray()).ToList<Orgaization>();
                }

                if (organList != null)
                {
                    if (CommonCache.organList == null)
                    {
                        CommonCache.organList = new ConcurrentDictionary<String, Orgaization>();
                    }

                    foreach (Orgaization di in organList)
                    {
                        CommonCache.organList.TryAdd(di.OrganID, di);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region 获取机构的服务信息
            //机构的服务信息放入缓存中
            try
            {
                List<OrganInfo> organServiceList = null;
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    string sql = "SELECT * FROM aifishingep.organinfo";
                    List<DataParameter> dataParameter = new List<DataParameter>();
                    //插入基本信息
                    organServiceList = session.GetList<OrganInfo>(sql, dataParameter.ToArray()).ToList<OrganInfo>();
                }

                if (organServiceList != null)
                {
                    if (CommonCache.organServiceList == null)
                    {
                        CommonCache.organServiceList = new ConcurrentDictionary<String, OrganInfo>();
                    }

                    foreach (OrganInfo di in organServiceList)
                    {
                        CommonCache.organServiceList.TryAdd(di.OrganID, di);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region 定时任务

            // 从工厂里获取调度实例
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //按小时执行
            // 创建一个任务
            IJobDetail dataExchangeJob = JobBuilder.Create<DataExchangeJob>()
                .WithIdentity("dataExchangeJob", "dataExchangeGroup")
                .Build();

            //*/20 * * * * ? 每20秒执行一次
            // 创建一个触发器
            ITrigger dataExchangeTrigger = TriggerBuilder.Create()
                .WithIdentity("dataExchangeTrigger", "dataExchangeGroup").WithCronSchedule("0 */1 * * * ?") 
                .StartNow() // 无限次执行
                .Build();

            scheduler.ScheduleJob(dataExchangeJob, dataExchangeTrigger);

            #endregion

            #region 定时更新设备缓存

            //更新设备缓存
            // 创建一个任务
            IJobDetail cacheJob = JobBuilder.Create<OrganCacheJob>()
                .WithIdentity("cacheJob", "cacheGroup")
                .Build();

            // 创建一个触发器
            ITrigger cacheTrigger = TriggerBuilder.Create()
                .WithIdentity("cacheTrigger", "cacheGroup").WithCronSchedule("*/10 * * * * ?") //每10分钟更新一次缓存
                .StartNow()
                .Build();

            //测试时临时注释
            //scheduler.ScheduleJob(cacheJob, cacheTrigger);

            #endregion

            Console.ReadKey();
        }
    }
}
