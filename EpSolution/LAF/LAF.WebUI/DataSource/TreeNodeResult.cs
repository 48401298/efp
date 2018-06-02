using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.WebUI.DataSource
{
    /// <summary>
    /// 树控件数据源
    /// </summary>
    public class TreeNodeResult
    {
        List<TreeNodeResult> _node = new List<TreeNodeResult>();

        /// <summary>
        /// 绑定到节点的标识值
        /// </summary>
        public string Tid { get; set; } //。 

        /// <summary>
        /// 显示的文字
        /// </summary>
        public string Ttext { get; set; }    //。 

        /// <summary>
        /// 是否节点被选中
        /// </summary>
        public bool TChecked { get; set; }    //。

        /// <summary>
        /// 绑定到节点的自定义属性
        /// </summary>
        public string Url { get; set; }    //。

        /// <summary>
        /// 带单位的名称张三（单位1）
        /// </summary>
        public string FullName { get; set; }    //。

        /// <summary>
        /// 目标的 DOM 对象
        /// </summary>
        public string Ttarget { get; set; }   //。


        public TreeNodeResult parentNode { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public TreeNodeResult[] Tchildren
        {
            get { return _node.ToArray(); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public bool TState { get; set; }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="pNode"></param>
        public void AddchildNode(TreeNodeResult pNode)
        {
            _node.Add(pNode);

        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="pNode"></param>
        public void RemovechildNode(TreeNodeResult pNode)
        {
            _node.Remove(pNode);

        }


        public static string GetResultJosnS(TreeNodeResult[] Nodes)
        {
            List<string> reslut = new List<string>();
            foreach (var item in Nodes)
            {
                reslut.Add(GetResultJosnStr(item));
            }
            return string.Format("[{0}]", string.Join(",", reslut.ToArray()));
        }


        private static string GetResultJosnStr(TreeNodeResult node)
        {
            try
            {
                StringBuilder reslut = new StringBuilder();
                if (node != null)
                {
                    reslut.AppendFormat("\"id\":\"{0}\"", node.Tid);//	"text":"Java")

                    reslut.AppendFormat(",\"text\":\"{0}\"", node.Ttext);
                    if (node.TChecked)
                    {
                        reslut.Append(",\"checked\":\"true\"");
                    }
                    if (node.TState)
                    {
                        reslut.AppendFormat(",\"state\":\"{0}\"", "closed");
                    }
                    if (!string.IsNullOrEmpty(node.Url))
                    {
                        reslut.Append(",\"attributes\":{\"url\":\"" + node.Url + "\",\"fullname\":\"" + node.FullName + "\"}");
                    }

                    if (node.Tchildren.Length > 0)
                    {
                        reslut.AppendFormat(",\"children\":{0}", GetResultJosnS(node.Tchildren));
                    }

                }
                return "{" + reslut.ToString().TrimStart(',') + "}";
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
