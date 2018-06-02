using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.Entity.Video;

namespace Manage.Web.DecisionSupport
{
    public partial class MonitorMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitData();
        }

        private void InitData()
        {
            List<VDPosition> positions = new List<VDPosition>();

            positions.Add(new VDPosition { PositionName = "总部", LO = decimal.Parse("121.624268"), LA = decimal.Parse("38.87897"),PositionCode="01" });
            positions.Add(new VDPosition { PositionName = "食品一厂", LO = decimal.Parse("121.623268"), LA = decimal.Parse("38.84897"), PositionCode = "02" });
            positions.Add(new VDPosition { PositionName = "食品二厂", LO = decimal.Parse("121.665268"), LA = decimal.Parse("38.87997"), PositionCode = "03" });

            string jsData = "";
            jsData = "var markerArr=[";
            for (int i = 0; i < positions.Count; i++)
            {
                jsData += "{title:\"" + positions[i].PositionName + "\",point:\"" + positions[i].LO.ToString() + "," +
                          positions[i].LA.ToString() + "\",id:\"" + positions[i].PositionCode + "\"}";
                if (i < positions.Count - 1)
                {
                    jsData += ",";
                }
            }
            jsData += "];";
            
            ClientScript.RegisterClientScriptBlock(GetType(), "initData", jsData, true);
        }
    }
}