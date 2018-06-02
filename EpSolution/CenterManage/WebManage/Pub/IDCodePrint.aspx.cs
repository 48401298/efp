using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manage.BLL.WH;
using LAF.WebUI;
using LAF.WebUI.Util;

namespace Manage.Web.WH.Pub
{
    /// <summary>
    /// 货品识别码打印
    /// </summary>
    public partial class IDCodePrint : System.Web.UI.Page
    {
        public string PrintContent { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> idCodeList = Session["idCodeList"] as List<string>;
            string idcodetext = Session["idcodetext"] as string;

            if (idCodeList != null)
            {
                PrintContent += "LODOP.SET_PRINT_PAGESIZE(1,600,400,\"\");";
                for (int i = 0; i < idCodeList.Count; i++)
                {                    
                    PrintContent += "LODOP.ADD_PRINT_TEXT(5, 20, " + System.Configuration.ConfigurationManager.AppSettings["MLabelWidth"]
                        + ", " + System.Configuration.ConfigurationManager.AppSettings["MLabelHeight"]
                        + ",\"" + idcodetext + "\");";
                    PrintContent += "LODOP.ADD_PRINT_BARCODE(30, 20, " + System.Configuration.ConfigurationManager.AppSettings["MLabelWidth"]
                        + ", " + System.Configuration.ConfigurationManager.AppSettings["MLabelHeight"]
                        + ", \"128A\", \"" + idCodeList[i] + "\");";
                    PrintContent += "LODOP.ADD_PRINT_TEXT(130, 20, " + System.Configuration.ConfigurationManager.AppSettings["MLabelWidth"]
                        + ", " + System.Configuration.ConfigurationManager.AppSettings["MLabelHeight"]
                        + ",\"" + System.Configuration.ConfigurationManager.AppSettings["rootOrgan"] + "\");";
                    PrintContent += "LODOP.NewPage();";
                }
                WHMatBLL bll = BLLFactory.CreateBLL<WHMatBLL>();
                bll.SignMatIDCodePrintCount(idCodeList);
            }           

            Session["idCodeList"] = null;
        }
    }
}