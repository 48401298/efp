using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Entity;

namespace Manage.Entity.Sys
{
    /// <summary>
    /// 字典信息基类
    /// </summary>
    public class BaseDictInfo : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Des { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Seq { get; set; }
    }
}
