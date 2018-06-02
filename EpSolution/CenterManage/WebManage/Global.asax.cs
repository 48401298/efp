using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Quartz;
using Quartz.Impl;
using Manage.Web.WH.Report;

namespace Manage.Web
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 数据分页尺寸
        /// </summary>
        public static int PageSize { get; set; }

        /// <summary>
        /// 应用程序物理路径
        /// </summary>
        public static string PhysicsRootPath { get; set; }

        /// <summary>
        /// 临时文件路径
        /// </summary>
        public static string TempPath { get; set; }

        void Application_Start(object sender, EventArgs e)
        {
            //配置日志处理组件
            LAF.Log.LogManager.Configure("log4net");

            try
            {
                //设置网站根路径
                LAF.WebUI.WebUIGlobal.SiteRoot = "http://" + System.Web.HttpContext.Current.Request.Url.Authority;
                if (System.Web.HttpContext.Current.Request.ApplicationPath != "/")
                    LAF.WebUI.WebUIGlobal.SiteRoot += System.Web.HttpContext.Current.Request.ApplicationPath + "/";
                else
                    LAF.WebUI.WebUIGlobal.SiteRoot += "/";

                //应用程序物理路径
                PhysicsRootPath = Server.MapPath(HttpRuntime.AppDomainAppVirtualPath);

                //临时文件路径
                TempPath = PhysicsRootPath + "Temp\\";

                if (System.IO.Directory.Exists(PhysicsRootPath + "Temp") == false)
                {
                    System.IO.Directory.CreateDirectory(PhysicsRootPath + "Temp");
                }

                //设置菜单文件路径
                LAF.WebUI.Menu.MenuHelper.MenuFilePath = Server.MapPath("~/App_Data/Menu.xml");

                //装配数据工厂
                LAF.Data.DataFactory.Configure(true);

                //设置数据分页尺寸
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["PageSize"]) == false)
                    PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
                else
                    PageSize = 15;

                //启动台账计算
                StartStockAccountCompute();
            }
            catch (Exception ex)
            {
                if (LAF.Log.LogManager.LogHelper != null)
                {
                    LAF.Log.LogManager.LogHelper.Error(
                        new LAF.Log.LogInfo { ClientIP = "localhost", UserName = "admin", Info = "应用启动", ErrorInfo = ex });
                }
                throw ex;
            }

        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            HttpContext HC = ((HttpApplication)sender).Context;

            HttpApplication ha = ((HttpApplication)sender);
            if (HC.Server.GetLastError().GetBaseException() != null)
            {
                Exception ex = HC.Server.GetLastError().GetBaseException();

                if (LAF.Log.LogManager.LogHelper != null)
                {
                    LAF.Log.LogManager.LogHelper.Error(
                        new LAF.Log.LogInfo { ClientIP = "localhost", UserName = "admin", Info = "", ErrorInfo = ex });
                }
            }

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

        private void StartStockAccountCompute()
        {
            // 从工厂里获取调度实例
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            // 创建一个任务
            IJobDetail job = JobBuilder.Create<StockAccountComputeJob>()
                .WithIdentity("MyJob", "group1")
                .Build();

            // 每天执行的触发器
            ITrigger t = TriggerBuilder.Create()
               .WithIdentity("myTrigger", "group2")
               .ForJob(job)
               .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(21, 5)) 
               .Build();

            // 每天执行的将触发器换成天的就可以了
            scheduler.ScheduleJob(job, t);
        }

    }
}
