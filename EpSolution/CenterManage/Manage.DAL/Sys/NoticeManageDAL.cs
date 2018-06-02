using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Data; 
using LAF.Data; 
using Manage.Entity.Sys; 

namespace Manage.DAL.Sys
{     
    /// <summary>
    /// 公告管理
    /// </summary>
    public class NoticeManageDAL
    {
        #region 获取公告信息

        /// <summary>
        /// 获取公告信息
        /// </summary>
        /// <param name="user">条件</param>
        /// <returns>公告信息</returns>
        public NoticeInfo Get(NoticeInfo notice)
        {
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //获取公告信息
                    notice = session.Get<NoticeInfo>(notice);
                }

                return notice;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
  
        #endregion

        #region 获取公告列表

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">数据页</param>
        /// <returns>数据页</returns>
        public DataPage GetList(NoticeInfo condition, DataPage page)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder whereBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            try
            {
                //构成查询语句
                sqlBuilder.Append("SELECT NOTICEID,NOTICETITLE,NOTICECONTEXT,USETIME,ATTACHFILE,T_QM_NOTICE.CREATETIME,T_QM_NOTICE.CREATEUSER,T_QM_NOTICE.UPDATETIME,T_QM_NOTICE.UPDATEUSER,T_QM_USER.USERNAME AS UserName ");
                sqlBuilder.Append("FROM T_QM_NOTICE INNER JOIN T_QM_USER ON T_QM_NOTICE.CREATEUSER = T_QM_USER.USERID ");

                //查询条件
                if (string.IsNullOrEmpty(condition.NoticeTitle) == false)
                {
                    whereBuilder.Append(" AND NOTICETITLE LIKE @NOTICETITLE");
                    parameters.Add(new DataParameter { ParameterName = "NOTICETITLE", DataType = DbType.String, Value = "%" + condition.NoticeTitle + "%" });
                }

                if (string.IsNullOrEmpty(condition.StartTime) == false)
                {
                    whereBuilder.Append(" AND USETIME >= @STARTTIME");
                    parameters.Add(new DataParameter { ParameterName = "STARTTIME", DataType = DbType.String, Value = condition.StartTime });
                }

                if (string.IsNullOrEmpty(condition.EndTime) == false)
                {
                    whereBuilder.Append(" AND USETIME <= @ENDTIME");
                    parameters.Add(new DataParameter { ParameterName = "ENDTIME", DataType = DbType.String, Value = condition.EndTime });
                }

                if (whereBuilder.Length > 0)
                {
                    sqlBuilder.Append(" WHERE " + whereBuilder.ToString().Substring(4));
                }

                //分页关键字段及排序
                page.KeyName = "NOTICEID";
                if (string.IsNullOrEmpty(page.SortExpression))
                {
                    page.SortExpression = "USETIME DESC";
                }

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {   
                    page = session.GetDataPage<NoticeInfo>(sqlBuilder.ToString(), parameters.ToArray(), page);
                }

                return page;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 公告信息是否重复
 
        /// <summary>
        /// 判断公告标题是否存在
        /// </summary>
        /// <param name="info">信息</param>
        /// <returns>true@已存在；fasel@不存在。</returns>
        public bool ExistsNotice(NoticeInfo notice)
        {
            string noticeID = "";
            int count = 0;
            string sql = null;
            try
            {
                if (string.IsNullOrEmpty(notice.NoticeID) == false)
                {
                    noticeID = notice.NoticeID;
                }

                sql = "SELECT COUNT(*) FROM T_QM_NOTICE WHERE NOTICEID <> @NOTICEID AND NOTICETITLE=@NOTICETITLE";

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    count = Convert.ToInt32(session.ExecuteSqlScalar(sql, new DataParameter("NOTICEID", noticeID), new DataParameter { ParameterName = "NOTICETITLE", Value = notice.NoticeTitle }));
                }

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 插入公告

        /// <summary>
        /// 插入公告
        /// </summary>
        /// <param name="user">公告信息</param>
        /// <returns>插入数</returns>
        public int Insert(NoticeInfo user)
        {
            int count = 0;

            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //插入基本信息
                    count = session.Insert<NoticeInfo>(user); 
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 删除公告

        /// <summary>
        /// 删除公告信息
        /// </summary>
        /// <param name="user">公告信息</param>
        /// <returns>删除个数</returns>
        public int Delete(NoticeInfo notice)
        {
            int count = 0; 
            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //删除基本信息
                    count = session.Delete<NoticeInfo>(notice);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 更新公告

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="user">公告信息</param>
        /// <returns>更新个数</returns>
        public int Update(NoticeUpdateInfo notice)
        {
            int count = 0;

            try
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    //更新基本信息
                    count = session.Update<NoticeUpdateInfo>(notice);
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 获取未读有通知信息
        /// <summary>
        /// 获取未读有通知信息
        /// </summary>
        /// <returns></returns>
        public List<NoticeInfo> GetNotReadNotice()
        {
            StringBuilder sqlBuilder = new StringBuilder();
            List<DataParameter> parameters = new List<DataParameter>();
            List<NoticeInfo> resultList = null;
            try
            {
                //构成查询语句
                sqlBuilder.Append(" SELECT TOP 5 NOTICEID,NOTICETITLE,USETIME,OUTTIME ");
                sqlBuilder.Append(" FROM T_QM_NOTICE WHERE USETIME <= GETDATE() AND OUTTIME >= CONVERT(VARCHAR(100), GETDATE(), 112)");

                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    resultList = (List<NoticeInfo>)session.GetList<NoticeInfo>(sqlBuilder.ToString(), parameters.ToArray());
                }

                return resultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
