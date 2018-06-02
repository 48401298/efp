using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// 主体信息
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 列标题
        /// </summary>
        public string ColumnTitle { get; set; }

        /// <summary>
        /// 列KEY
        /// </summary>
        public string ColumnKey { get; set; }

        /// <summary>
        /// 列隐藏标识
        /// </summary>
        public bool ColumnHidden { get; set; }

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

    }

    public enum EmuExcelCellType
    {
        /// <summary>
        /// 默认值
        /// </summary>
        Nothing,

        Integer,

        Decimal,

        YearMonth,
    }

}