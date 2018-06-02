using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using LAF.WebUI;
using Manage.BLL.MES;
using Manage.Entity.MES;
using Manage.BLL.Inspect;
using Manage.Entity.Inspect;
using LAF.WebUI.Util;

namespace Manage.Web.Pub
{
    /// <summary>
    /// GetInspectStatisticsEChart 的摘要说明
    /// </summary>
    public class GetInspectStatisticsEChart : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";
            string ItemCode = context.Request.QueryString["ItemCode"];
            string DeviceCode = context.Request.QueryString["DeviceCode"];
            string OrganID = context.Request.QueryString["OrganID"];
            string DeviceType = context.Request.QueryString["DeviceType"];
            string ResultType = context.Request.QueryString["ResultType"];
            string StartTime = context.Request.QueryString["StartTime"];

            InspectStatisticsEChartBLL bll = BLLFactory.CreateBLL<InspectStatisticsEChartBLL>();
            InspectResultEntity paramEntity = new InspectResultEntity();
            paramEntity.ResultType = ResultType;
            paramEntity.StartTime = StartTime;
            paramEntity.ItemCode = ItemCode;
            paramEntity.DeviceCode = DeviceCode;
            paramEntity.OrganID = OrganID;
            paramEntity.DeviceType = DeviceType;

            List<InspectResultEntity> list = bll.GetList(paramEntity);

            CommonDropdown deviceTypeDropdown = new CommonDropdown();
            List<DictInfo> deviceTypeList = deviceTypeDropdown.getInspectDeviceType();

            CommonDropdown resultTypeDropdown = new CommonDropdown();
            List<DictInfo> resultTypeList = resultTypeDropdown.getInspectResultType();

            StringBuilder sb = new StringBuilder();

            if (list != null && list.Count > 0)
            {
                //记录项目种类列表
                List<string> legendList = new List<string>();
                legendList.Add("最大值");
                legendList.Add("最小值");
                legendList.Add("平均值");
                List<string> xList = new List<string>();
                foreach (InspectResultEntity ire in list)
                {
                    //统计X轴的时间列表
                    bool exist1 = false;
                    foreach (string ll in xList)
                    {
                        string temp = getStrByResultType(ire.InspectTime, ResultType);

                        if (temp == ll)
                        {
                            exist1 = true;
                            break;
                        }
                    }
                    if (!exist1)
                    {
                        xList.Add(getStrByResultType(ire.InspectTime, ResultType));
                    }
                }

                
                string echartTitle = "采集数据";
                echartTitle += resultTypeList.Find(p => p.ID == ResultType).Des;
                echartTitle += "变化";

                ///子标题
                string subText = StartTime;

                sb.Append(" {      ");
                sb.Append(" \"title\" : {     ");
                sb.Append("     \"text\": \"" + echartTitle + "\",     ");
                sb.Append("     \"subtext\": \"" + subText + "\"     ");
                sb.Append(" },     ");
                sb.Append(" \"tooltip\" : {     ");
                sb.Append("     \"trigger\": \"axis\"     ");
                sb.Append(" },     ");
                sb.Append(" \"legend\": {     ");
                string data = "";
                int index = 0;
                foreach (string d in legendList)
                {
                    data += ("\"" + d  + "\"");
                    if(index < legendList.Count - 1)
                    {
                        data += ",";
                    }
                    index++;
                }
                sb.Append("     \"data\":[" + data + "]     ");
                sb.Append(" },     ");
                sb.Append(" \"toolbox\": {     ");
                sb.Append("     \"show\" : true,     ");
                sb.Append("     \"feature\" : {     ");
                sb.Append("        \"mark\" : {\"show\": true},     ");
                sb.Append("         \"magicType\" : {\"show\": true, \"type\": [\"line\", \"bar\"]},     ");
                sb.Append("         \"saveAsImage\" : {\"show\": true}     ");
                sb.Append("     }     ");
                sb.Append(" },     ");
                sb.Append(" \"calculable\" : \"true\",     ");
                sb.Append(" \"xAxis\" : [     ");
                sb.Append("     {     ");
                sb.Append("         \"type\" : \"category\",     ");
                sb.Append("         \"boundaryGap\" : \"false\",     ");

                string xdata = "";
                int xindex = 0;
                foreach (string d in xList)
                {
                    xdata += ("\"" + d + "\"");
                    if (xindex < xList.Count - 1)
                    {
                        xdata += ",";
                    }
                    xindex++;
                }

                sb.Append("         \"data\" : [" + xdata + "]     ");
                sb.Append("    }     ");
                sb.Append(" ],     ");
                sb.Append(" \"yAxis\" : [     ");
                sb.Append("     {     ");
                sb.Append("         \"type\" : \"value\"     ");
                sb.Append("     }     ");
                sb.Append(" ],     ");
                sb.Append(" \"series\" : [     ");

                int aa = 0;
                foreach (string d in legendList)
                {
                   
                    string values = "";
                    int kk = 0;
                    foreach (InspectResultEntity ire in list)
                    {
                        if (d == "最大值")
                        {
                            values += ("\"" + ire.MaxDataValue + "\"");
                        }
                        else if (d == "最小值")
                        {
                            values += ("\"" + ire.MinDataValue + "\"");
                        }
                        else
                        {
                            values += ("\"" + ire.AvgValue + "\"");
                        }
                        
                        if (kk < list.Count - 1)
                        {
                            values += ",";
                        }
                        kk++;
                    }
                    
                    sb.Append(" {     ");
                    sb.Append("     \"name\":\"" + d + "\",     ");
                    sb.Append("     \"type\":\"line\",     ");
                    sb.Append("     \"data\":[" + values + "]     ");
                    //sb.Append("     \"markPoint\" : {     ");
                    //sb.Append("         \"data\" : [     ");
                    //sb.Append("             {\"type\" : \"max\", \"name\": \"最大值\"},     ");
                    //sb.Append("             {\"type\" : \"min\", \"name\": \"最小值\"}     ");
                    //sb.Append("         ]     ");
                    //sb.Append("    }       ");
                    sb.Append(" }     ");
                    if (aa < legendList.Count - 1)
                    {
                        sb.Append(",");
                    }
                    aa++;

                }

                
                //sb.Append(" {    ");
                //sb.Append(" \"name\":\"最低气温\",    ");
                //sb.Append(" \"type\":\"line\",    ");
                //sb.Append(" \"data\":[1, -2, 2, 5, 3, 2, 0],    ");
                //sb.Append(" \"markPoint\" : {    ");
                //sb.Append("     \"data\" : [    ");
                //sb.Append("         {\"name\" : \"周最低\", \"value\" : -2, \"xAxis\": 1, \"yAxis\": -1.5}    ");
                //sb.Append("     ]    ");
                //sb.Append(" },    ");
                //sb.Append(" \"markLine\" : {    ");
                //sb.Append("     \"data\" : [    ");
                //sb.Append("         {\"type\" : \"average\", \"name\" : \"平均值\"}    ");
                //sb.Append("     ]    ");
                //sb.Append(" }    ");
                //sb.Append(" }    ");
                sb.Append(" ]     ");
                sb.Append(" }     ");
            }

            context.Response.Write(sb.ToString());
        }

        public string getStrByResultType(DateTime dt, string ResultType)
        {
            string result = "";
            if (ResultType == "1")
            {
                result = dt.Hour + "";
            }
            else if (ResultType == "2")
            {
                result = dt.Day + "";
            }
            else
            {
                result = dt.Month + "";
            }

            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}