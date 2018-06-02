using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Manage.Web.MES.Tracking
{
    public partial class OutputQRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> qrCodeList = Session["QRCodeLisr"] as List<string>;

            int index = 0;

            string html = "<table>";

            int row = qrCodeList.Count / 8;

            int m = qrCodeList.Count % 8;

            if (m != 0)
                row += 1;

            for (int i = 0; i < row; i++)
            {
                html += "<tr>";

                for (int j = 0; j < 8; j++)
                {
                    html += string.Format("<td style=\"padding: 10px\"><img width=\"99\" height=\"88\" src=\"{0}\" /></td>", LAF.WebUI.WebUIGlobal.SiteRoot + "qrcodes/" + qrCodeList[index]);

                    index++;

                    if (index > qrCodeList.Count - 1)
                    {
                        break;
                    }
                }

                html += "</tr>";
            }

            html += "</table>";

            //Response.Write(html);

            Random rd = new Random();
            string fileName = Guid.NewGuid().ToString() + ".doc";
            //存储路径
            string path = Server.MapPath("~/qrcodes/"+fileName);

            //创建字符输出流
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.UnicodeEncoding.UTF8))
            {
                //写入
                sw.Write(html);
                sw.Close();
            }
            Response.Clear();
            Response.Buffer = true;
            this.EnableViewState = false;
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(path);
            Response.Flush();
            Response.Close();
            Response.End();
        }
    }
}