
namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// 字体样式（与NPOI组件匹配）
    /// </summary>
    public class FontStyle
    {
        /// <summary>
        /// 字体
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        /// 字号
        /// </summary>
        public short FontHeightInPoints { get; set; }

        /// <summary>
        /// 加粗
        /// </summary>
        public short Boldweight { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public short Color { get; set; }

    }
}
