using System;
using LAF.BLL;
using LAF.Entity;

namespace LAF.WebUI
{
    /// <summary>
    /// 业务逻辑类工厂
    /// </summary>
    public class BLLFactory
    {
        /// <summary>
        /// 创建逻辑处理对象
        /// </summary>
        /// <typeparam name="T">逻辑处理对象类型</typeparam>
        /// <returns>逻辑处理对象</returns>
        public static T CreateBLL<T>() where T : new()
        {
            T bll = new T();

            try
            {                
                //设置登录信息
                (bll as BaseBLL).LoginUser = System.Web.HttpContext.Current.Session["userInfo"] as LoginInfo;

                return bll;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}