using System;
using System.IO;


namespace LAF.WebUI.Util
{
    public class ExceptionHelper
    {        
        private static string _FailureHTML = null;//异常页面html代码

        /// <summary>
        /// 异常页面html代码
        /// </summary>
        public static string FailureHTML
        {
            set
            {
                _FailureHTML = value;
            }
            get
            {
                return _FailureHTML;
            }

        }

        public ExceptionHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }

        /// <summary>
        /// 初始化消息页模板内容
        /// </summary>
        public static void InitHTML(string path)
        {
            using (StreamReader SR = new StreamReader(path, System.Text.Encoding.GetEncoding("gb2312")))
            {
                _FailureHTML = SR.ReadToEnd();

                _FailureHTML = _FailureHTML.Replace("$SiteRoot", WebUIGlobal.SiteRoot);
            }
        }

        /// <summary>
        /// 返回错误页html代码
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string GetErrorHtml(Exception ex)
        {
            string html = "";

            html = _FailureHTML.Replace("$Msg", ex.Message);

            return html;
        }
    }
}
