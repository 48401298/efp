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
    public class CacheJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {

            List<DeviceInfo> deviceList = null;

            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                string sql = "SELECT * FROM deviceinfo T ORDER BY DeviceCode";
                List<DataParameter> dataParameter = new List<DataParameter>();
                //插入基本信息
                deviceList = session.GetList<DeviceInfo>(sql, dataParameter.ToArray()).ToList<DeviceInfo>();
            }

            if (deviceList != null && deviceList.Count > 0)
            {
                foreach (DeviceInfo di in deviceList)
                {
                    DeviceCache.deviceCacheList.AddOrUpdate(di.DeviceSN, di, (oldKey, oldValue) => di);
                }
            }
            
        }
    }
}
