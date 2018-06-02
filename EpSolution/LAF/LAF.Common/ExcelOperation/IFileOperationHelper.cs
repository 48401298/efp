using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// 文件导入导出接口
    /// </summary>
    public interface IFileOperationHelper
    {
        /// <summary>
        /// 将数据生成为CSV文件
        /// <param name="info">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// </summary>    
        void WriteWorkbook(SheetInfo info, string fileName);

        // 添加：重载生成CSV文件方法，扩展支持多表、表头，单元格样式等
        /// <summary>
        /// 将数据生成为CSV文件
        /// <param name="infos">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// </summary>
        void WriteWorkbook(List<SheetInfo> infos, string fileName);

        /// <summary>
        /// 从CSV读取数据
        /// </summary>
        /// <param name="info">配置信息</param>
        /// <param name="fileName">文件名</param>
        /// <returns>数据ArrarList(List<DataInfoItem>)</returns>
        ArrayList ReadWorkbook(SheetInfo info, string fileName);
    }
}
