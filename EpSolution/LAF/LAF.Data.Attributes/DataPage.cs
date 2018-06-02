using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data
{
    /// <summary>
    /// 数据页
    /// </summary>
    [Serializable]
    public class DataPage
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 分页主键(可以是组合的)
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 排序表达式
        /// </summary>
        public string SortExpression { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 获取总记录数sql
        /// </summary>
        public string CountSql { get; set; }

        /// <summary>
        /// 是否并行
        /// </summary>
        public bool IsParallel { get; set; }

        /// <summary>
        /// 是否精确分页
        /// </summary>
        public bool AccuratePartition { get; set; }

    }
}
