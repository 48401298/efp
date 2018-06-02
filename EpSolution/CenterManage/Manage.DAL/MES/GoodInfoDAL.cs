using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Entity.MES;
using LAF.Data;
using System.Data;

namespace Manage.DAL.MES
{
    /// <summary>
    ///　模块名称：产成品记录
    ///　作    者：wanglu
    ///　编写日期：2017年11月18日
    /// </summary>
    public class GoodInfoDAL : BaseDAL
    {
        #region 插入信息
        /// <summary>
        /// 插入信息(单表)
        /// </summary>
        /// <param name="">信息</param>
        /// <returns>插入行数</returns>
        public int Insert(GoodInfo model)
        {
            int count = 0;
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    session.OpenTs();

                    //插入基本信息
                    count = session.Insert<GoodInfo>(model);
                    string sql = "UPDATE T_FP_ProducePlan SET FactAmount = FactAmount+@OfflineNum WHERE PID =@PID";
                    parameters.Add(new DataParameter { ParameterName = "OfflineNum", DataType = DbType.String, Value = model.OfflineNum });
                    parameters.Add(new DataParameter { ParameterName = "PID", DataType = DbType.String, Value = model.PLANID });
                    count = session.ExecuteSql(sql, parameters.ToArray());

                    //产品组成批次号
                    sql = "update T_FP_GoodPackingForm set BatchNumber = @BatchNumber where GoodBarCode = @GoodBarCode";
                    session.ExecuteSql(sql, new DataParameter("BatchNumber", model.BatchNumber), new DataParameter("GoodBarCode",model.GoodBarCode));

                    session.CommitTs(); 
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
