using NPOI.SS.UserModel;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// 单元格样式（与NPOI组件匹配）
    /// </summary>
    public class CellStyle
    {
        public CellStyle()
        {
            BorderStyle = NPOI.SS.UserModel.BorderStyle.None;
            Font = new FontStyle();
            Font.FontName = "宋体";
            Font.FontHeightInPoints = (short)11;
            Font.Color = (short)8;
            FillPattern = FillPattern.NoFill;
        }

        /// <summary>
        /// 填充色
        /// </summary>
        public short FillForegroundColor { get; set; }

        /// <summary>
        /// 填充样式
        /// </summary>
        public FillPattern FillPattern { get; set; }

        /// <summary>
        /// 边框样式
        /// </summary>
        public BorderStyle BorderStyle { get; set; }

        /// <summary>
        /// 边框样式
        /// </summary>
        public FontStyle Font { get; set; }

    }
}
