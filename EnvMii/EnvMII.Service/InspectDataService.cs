using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.MongoDBClient;
using MongoDB.Driver;
using EnvMII.VO;
using MongoDB.Bson;

namespace EnvMII.Service
{
    /// <summary>
    /// 监测数据服务
    /// </summary>
    public class InspectDataService
    {
        public void Save(InspectOriginalData oData, List<InspectItemData> itemDatas)
        {
            //创建数据库链接
            MongoClient mc = new MongoClient(ConnectionManager.MongodbConectionStr);
            MongoServer server = mc.GetServer();
            server.Connect();

            //获得数据库
            MongoDatabase db = server.GetDatabase("InspectDB");
            MongoCollection colOdata = db.GetCollection("InspectOriginalData");
            MongoCollection colItemdata = db.GetCollection("InspectItemData");
            try
            {
                //插入原始监测数据
                colOdata.Insert<InspectOriginalData>(oData);

                //插入监测项目数据
                colItemdata.InsertBatch<InspectItemData>(itemDatas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库连接
                if (server!=null)
                    server.Disconnect();
            }
        }

        public List<InspectOriginalData> GetOriginalDatas(InspectOriginalData oData)
        {
            //创建数据库链接
            MongoClient mc = new MongoClient(ConnectionManager.MongodbConectionStr);
            MongoServer server = mc.GetServer();
            server.Connect();

            //获得数据库
            MongoDatabase db = server.GetDatabase("InspectDB");
            MongoCollection colOdata = db.GetCollection("InspectOriginalData");
            
            try
            {
                //插入原始监测数据
                var query = new QueryDocument { };

                //查询全部集合里的数据
                MongoCursor<InspectOriginalData> inspectOriginalData = colOdata.FindAs<InspectOriginalData>(query);

                return inspectOriginalData.ToList<InspectOriginalData>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库连接
                if (server != null)
                    server.Disconnect();
            }
        }

        public List<InspectItemData> GetItemDatas(InspectOriginalData oData)
        {
            //创建数据库链接
            MongoClient mc = new MongoClient(ConnectionManager.MongodbConectionStr);
            MongoServer server = mc.GetServer();
            server.Connect();

            //获得数据库
            MongoDatabase db = server.GetDatabase("InspectDB");
            MongoCollection colItemdata = db.GetCollection("InspectItemData");

            try
            {
                //QueryDocument query = new QueryDocument();
                //BsonDocument b = new BsonDocument();
               
                //if(oData.StartTime != null && !"".Equals(oData.StartTime))
                //{
                //    b.Add("$gt", DateTime.Parse(oData.StartTime));
                //}

                //if (oData.EndTime != null && !"".Equals(oData.EndTime))
                //{
                //    b.Add("$lt", DateTime.Parse(oData.EndTime));
                //}

                //query.Add("InspectTime", b);

                //查询全部集合里的数据
                List<InspectItemData> inspectItemData = colItemdata.FindAllAs<InspectItemData>()
                    .Where(p => p.InspectTime > DateTime.Parse(oData.StartTime)
                    && p.InspectTime <= DateTime.Parse(oData.EndTime)).OrderBy(x => x.DeviceSN).ThenBy(x => x.ItemCode).ToList<InspectItemData>();

                return inspectItemData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库连接
                if (server != null)
                    server.Disconnect();
            }
        }
    }
}
