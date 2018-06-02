
namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// 主体信息
    /// </summary>
    public class ExcelExceptionModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 行坐标
        /// </summary>
        public int RowNum { get; set; }

        /// <summary>
        /// 列坐标
        /// </summary>
        public int ColNum { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 页面上控件的ID值
        /// </summary>
        public string ControlID { get; set; }

        /// <summary>
        /// Sheet名字
        /// </summary>
        public string SheetName { get; set; }

    }
}
