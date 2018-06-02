using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAF.Data.Attributes;
using System.Data;

namespace ExchangeEntity
{
    /// <summary>
    /// 机构服务信息
    /// </summary>
    [Serializable]
    [DBTable(TableName = "OrganInfo")]
    public class OrganInfo
    {
        /// <summary>
        /// 组织机构编号
        /// </summary>
        [DBColumn(ColumnName = "OrganID", DataType = DbType.String, IsKey = true)]
        public string OrganID { get; set; }

        /// <summary>
        /// 组织机构描述
        /// </summary>
        [DBColumn(ColumnName = "OrganName", DataType = DbType.String)]
        public string OrganName { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        [DBColumn(ColumnName = "ServiceAddress", DataType = DbType.String)]
        public string ServiceAddress { get; set; }
    }
}
