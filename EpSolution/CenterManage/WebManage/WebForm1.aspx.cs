using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.Entity.Inspect;
using LAF.Data;

namespace Manage.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Parse("2018-5-1");
            DateTime dt2 = DateTime.Parse("2018-5-26");

            //删除数据
            using (IDataSession session = AppDataFactory.CreateMainSession())
            {
                session.ExecuteSql("delete from inspectdatainfo");
            }

            List<string> salinitys = new List<string> {"30","30.1","31.1","31.2","31.3","31.4" };

            List<string> chlorophyls = new List<string> { "8", "9", "10", "11", "12", "13" };

            List<string> turbiditys = new List<string> { "200", "210", "220", "230", "240", "250" };

            Random rm = new Random();

            //插入数据
            while (dt1 < dt2)
            {
                using (IDataSession session = AppDataFactory.CreateMainSession())
                {
                    
                    //温度值
                    InspectDataEntity infotemp = new InspectDataEntity();
                    infotemp.Id = Guid.NewGuid().ToString();
                    infotemp.DeviceCode = "ACTW-CAR1";
                    infotemp.ItemCode = "temp";
                    infotemp.InspectTime = dt1;
                    infotemp.InspectData = (13.2+double.Parse(dt1.Day.ToString())*1.5/30).ToString();
                    infotemp.OrganID = "b3a97409-f63f-46db-8277-b47014657217";
                    infotemp.UpdateTime = dt1;

                    session.Insert<InspectDataEntity>(infotemp);

                    //盐度值
                    InspectDataEntity infosalinity = new InspectDataEntity();
                    infosalinity.Id = Guid.NewGuid().ToString();
                    infosalinity.DeviceCode = "ACTW-CAR1";
                    infosalinity.ItemCode = "salinity";
                    infosalinity.InspectTime = dt1;
                    infosalinity.InspectData = salinitys[rm.Next(0, 5)];
                    infosalinity.OrganID = "b3a97409-f63f-46db-8277-b47014657217";
                    infosalinity.UpdateTime = dt1;
                    session.Insert<InspectDataEntity>(infosalinity);

                    //叶绿素值
                    InspectDataEntity infchlorophyl = new InspectDataEntity();
                    infchlorophyl.Id = Guid.NewGuid().ToString();
                    infchlorophyl.DeviceCode = "ACLW-CAR1";
                    infchlorophyl.ItemCode = "chlorophyl";
                    infchlorophyl.InspectTime = dt1;
                    infchlorophyl.InspectData = chlorophyls[rm.Next(0, 5)];
                    infchlorophyl.OrganID = "b3a97409-f63f-46db-8277-b47014657217";
                    infchlorophyl.UpdateTime = dt1;
                    session.Insert<InspectDataEntity>(infchlorophyl);

                    //浊度值
                    InspectDataEntity infturbidity = new InspectDataEntity();
                    infturbidity.Id = Guid.NewGuid().ToString();
                    infturbidity.DeviceCode = "ACLW-CAR1";
                    infturbidity.ItemCode = "turbidity";
                    infturbidity.InspectTime = dt1;
                    infturbidity.InspectData = turbiditys[rm.Next(0, 5)];
                    infturbidity.OrganID = "b3a97409-f63f-46db-8277-b47014657217";
                    infturbidity.UpdateTime = dt1;

                    session.Insert<InspectDataEntity>(infturbidity);
                }

                dt1=dt1.AddMinutes(60);
            }

            Response.Write("end");
        }
    }
}