using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAF.Data
{
    /// <summary>
    /// 参数
    /// </summary>
    public class DataParameter
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public DbType DataType { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataParameter()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        public DataParameter(string parameterName,object value)
        {
            ParameterName = parameterName;
            Value = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dataType">参数类型</param>
        /// <param name="value">参数值</param>
        public DataParameter(string parameterName,DbType dataType, object value)
        {
            ParameterName = parameterName;
            DataType = dataType;
            Value = value;
        }
    }
}
