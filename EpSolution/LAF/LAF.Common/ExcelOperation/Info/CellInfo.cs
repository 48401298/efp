using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// 信息项
    /// </summary>
    public class CellInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnTitle { get; set; }

        /// <summary>
        /// 所在列
        /// </summary>
        public string XPosition { get; set; }

        /// <summary>
        /// 所在行
        /// </summary>
        public string YPosition { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public int ColumnWidth { get; set; }

        /// <summary>
        /// 锁定列
        /// </summary>
        public bool ColumnLock { get; set; }

        /// <summary>
        /// 验证列
        /// </summary>
        public EmuExcelCellType ColumnValidation { get; set; }

        public string[] ColumnRangeValues { get; set; }

        /// <summary>
        /// 最大输入长度
        /// </summary>
        public int? ColValMaxLength { get; set; }

        /// <summary>
        /// 列隐藏标识
        /// </summary>
        public bool ColumnHidden { get; set; }
    }
}
