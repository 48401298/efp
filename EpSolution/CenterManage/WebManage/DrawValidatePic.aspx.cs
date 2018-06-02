using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Manage.Web
{
    public partial class DrawValidatePic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string checkCode = CreateRandomCode(4);
            Session["CheckCode"] = checkCode;
            CreateImage(checkCode);

        }

        /// <summary>
        /// 参数代表验证码位数

        /// </summary>
        /// <param name="codeCount"></param>
        /// <returns></returns>
        private string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(35);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        /// <summary>
        /// 用于生成图

        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 16 + 5);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 30);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(Color.Black);
            g.Clear(Color.FromArgb(124, 196, 234));

            //添加验证文字
            Point p = new Point(3, 3);
            for (int i = 0; i < checkCode.Length; i++)
            {
                if ((char.Parse("0") <= checkCode[i]) && (checkCode[i] <= char.Parse("9")))
                {
                    b = new System.Drawing.SolidBrush(Color.Red);
                }
                else
                {
                    b = new System.Drawing.SolidBrush(Color.Black);
                }
                p.X = (int)(3 + 11.5 * i);
                g.DrawString(checkCode[i].ToString(), f, b, p);
            }

            //画图片的前景噪音线

            Random rand = new Random();
            //for (int i = 0; i < 2; i++)
            //{
            //    Pen blackPen = new Pen(Color.FromArgb(DateTime.Now.Millisecond % 256 , DateTime.Now.Millisecond % 256 , DateTime.Now.Millisecond % 256) , 0);
            //    int x = rand.Next(DateTime.Now.Millisecond) % image.Width;
            //    int y = rand.Next(DateTime.Now.Millisecond) % image.Height;
            //    g.DrawLine(blackPen , x , y , image.Width - x , y);
            //}

            //画图片的前景噪音点

            for (int i = 0; i < 3; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(rand.Next()));
            }


            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }
    }
}
