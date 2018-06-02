using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace LAF.Common.WorkFlow
{
    /// <summary>
    /// 节点类型
    /// </summary>
    [Description("节点类型")]
    public enum FlowNodeTypes
    {
        [Description("开始")]
        Begin,
        [Description("中间")]
        Middle,
        [Description("结束")]
        End
    }

    /// <summary>
    /// 流程状态
    /// </summary>
    [Description("流程状态")]
    public enum FlowStatuss
    {
        [Description("激活")]
        Activation,
        [Description("未激活")]
        UnActivation
    }

    /// <summary>
    /// 流程使用方式"
    /// </summary>
    [Description("流程使用方式")]
    public enum FlowUseModes
    {
        [Description("管理")]
        Manage,
        [Description("使用")]
        Use,
        [Description("浏览")]
        View
    }

    /// <summary>
    /// 流程操作行为
    /// </summary>
    [Description("流程操作行为")]
    public enum FlowActions
    {
        [Description("创建")]
        Create,
        /// <summary>
        /// </summary>
        [Description("业务信息编辑")]
        InfoEdit,
        [Description("跳转")]
        Transfer,
        [Description("终止")]
        Abort,
        [Description("完成")]
        Completed
    }

    /// <summary>
    /// 流程过滤条件
    /// </summary>
    [Description("流程过滤条件")]
    public enum FilerConditions
    {
        [Description("全部")]
        All,
        [Description("待处理")]
        Dealing,
        [Description("已处理")]
        Dealed
    }

}
