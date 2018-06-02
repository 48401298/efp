using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.XSSF.UserModel;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.OpenXmlFormats.Spreadsheet;

namespace LAF.Common.ExcelOperation
{
    /// <summary>
    /// </summary>
    public class ExcelOperationValidationHelper
    {

        /// <summary>
        /// 设置单元格验证(文本)
        /// 创建者：戚鹏
        /// 创建日期：2013.5.20
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
        internal static IDataValidation getValidationTextLength(IDataValidationHelper helper, CellRangeAddressList range,
            int from, int to)
        {

            IDataValidationConstraint constraint = helper.CreateTextLengthConstraint(
                ST_DataValidationOperator.between.GetHashCode(), from.ToString(), to.ToString());
            IDataValidation validation = helper.CreateValidation(constraint, range);
            //validation.CreateErrorBox("error", "You must input a text between 1 and 50.");
            validation.ShowErrorBox = true;
            return validation;
        }

        /// <summary>
        /// 设置单元格验证(数字)
        /// 创建者：戚鹏
        /// 创建日期：2013.5.20
        /// </summary>
        /// <param name="RowCount"></param>
        /// <param name="ColNum"></param>
        /// <param name="FirstValue"></param>
        /// <param name="LastValue"></param>
        /// <returns></returns>
        internal static IDataValidation getValidationInt(IDataValidationHelper helper, CellRangeAddressList range,
            int from = 0, int to = 99999999)
        {
            IDataValidationConstraint constraint = helper.CreateintConstraint(
                ST_DataValidationOperator.between.GetHashCode(), from.ToString(), to.ToString());
            IDataValidation validation = helper.CreateValidation(constraint, range);
            //validation.CreateErrorBox("error", "You must input a numeric between 1 and 50.");
            validation.ShowErrorBox = true;
            return validation;

        }

        internal static IDataValidation getValidationYearMonth(IDataValidationHelper helper, CellRangeAddressList range)
        {
            IDataValidationConstraint constraint = helper.CreateintConstraint(
                ST_DataValidationOperator.between.GetHashCode(), "200001", "202012");
            IDataValidation validation = helper.CreateValidation(constraint, range);
            validation.CreateErrorBox("error", "请输入YYYYMM样式的年月，如2012年8月为【201208】");
            validation.ShowErrorBox = true;
            return validation;

        }

        /// <summary>
        /// 创建列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="ListData"></param>
        /// <returns></returns>
        internal static IDataValidation getValidationExplicitList(IDataValidationHelper helper, CellRangeAddressList range, string[] ListData)
        {
            IDataValidationConstraint constraint = helper.CreateExplicitListConstraint(ListData);
            IDataValidation validation = helper.CreateValidation(constraint, range);
            //validation.ShowPromptBox = true;
            validation.ShowErrorBox = true;
            return validation;
        }


    }
}