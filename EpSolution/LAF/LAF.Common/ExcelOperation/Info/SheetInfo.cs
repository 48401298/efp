using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// 主体信息
    /// </summary>
    public class SheetInfo : IDisposable
    {
        /// <summary>
        /// 信息名
        /// </summary>
        public string InfoName { get; set; }

        /// <summary>
        /// 信息名称
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 模板文件
        /// </summary>
        public string TemplateFile { get; set; }

        /// <summary>
        /// 输出记录数
        /// </summary>
        public string RecordCount { get; set; }

        /// <summary>
        /// 数据项定义
        /// </summary>
        public List<CellInfo> ColInfos { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public ArrayList DataArray { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public DataTable Dt { get; set; }


        /// <summary>
        /// 是否保护（默认否）
        /// </summary>
        public bool Protect { get; set; }

        // 添加：是否显示表标题，是否隐藏表头，表标题样式，列头样式，内容单元格样式 李鹏飞 2014-03-31 开始
        /// <summary>
        /// 表标题(通过TableName获取)
        /// </summary>
        public string TableCaption { get; set; }

        /// <summary>
        /// 是否显示表标题
        /// </summary>
        public bool IsShowTableCaption { get; set; }

        /// <summary>
        /// 是否隐藏表头
        /// </summary>
        public bool IsHideColumnHeader { get; set; }

        /// <summary>
        /// 表标题样式
        /// </summary>
        public CellStyle TableCaptionStyle { get; set; }

        /// <summary>
        /// 列头样式
        /// </summary>
        public CellStyle ColumnHeaderStyle { get; set; }

        /// <summary>
        /// 内容单元格样式
        /// </summary>
        public CellStyle ContentCellStyle { get; set; }
        // 添加：是否显示表标题，是否隐藏表头，表标题样式，列头样式，内容单元格样式 李鹏飞 2014-03-31 结束

        //是否被释放       
        private bool _alreadyDisposed = false;

        #region IDisposable

        public void Dispose()
        {

            Dispose(true);

        }

        protected virtual void Dispose(bool isDisposing)
        {

            // Don't dispose more than one

            if (_alreadyDisposed)
                return;

            if (isDisposing)
            {
                GC.SuppressFinalize(true);
            }

            // TODO: free unmanaged resources here

            // Set disposed flag

            _alreadyDisposed = true;

        }

        #endregion

    }
}
