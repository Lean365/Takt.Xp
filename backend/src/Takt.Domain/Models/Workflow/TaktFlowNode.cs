#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : FlowNode.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流节点模型 - 基于 OpenAuth.Net 标准
//===================================================================

namespace Takt.Domain.Models.Workflow
{
    /// <summary>
    /// 工作流节点模型
    /// </summary>
    public class TaktFlowNode
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 节点类型
        /// start: 开始节点
        /// approval: 审批节点
        /// branch: 网关开始节点
        /// join: 网关结束节点
        /// parallel: 会签节点
        /// end: 结束节点
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 节点位置（用于前端显示）
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// 节点位置（用于前端显示）
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// 节点宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 节点高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 节点配置信息（运行时状态）
        /// </summary>
        public SetInfo? SetInfo { get; set; }
    }

    /// <summary>
    /// 节点运行时状态信息
    /// </summary>
    public class SetInfo
    {
        /// <summary>
        /// 审批结果
        /// 1: 通过
        /// 2: 不通过
        /// 3: 驳回
        /// </summary>
        public int? Taged { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 审批时间
        /// </summary>
        public string TagedTime { get; set; } = string.Empty;

        /// <summary>
        /// 节点执行权限类型（审批人指定方式）
        /// </summary>
        public string NodeDesignate { get; set; } = string.Empty;

        /// <summary>
        /// 节点执行人数据
        /// </summary>
        public NodeDesignateData? NodeDesignateData { get; set; }

        /// <summary>
        /// 节点代码
        /// </summary>
        public string NodeCode { get; set; } = string.Empty;

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; } = string.Empty;

        /// <summary>
        /// 驳回节点类型
        /// 0: 前一步
        /// 1: 第一步
        /// 2: 某一步
        /// 3: 不处理
        /// </summary>
        public string NodeRejectType { get; set; } = string.Empty;

        /// <summary>
        /// 网关审批通过的方式
        /// all/空: 默认为全部通过
        /// one: 至少有一个通过
        /// </summary>
        public string NodeConfluenceType { get; set; } = "all";

        /// <summary>
        /// 网关通过的个数
        /// </summary>
        public int? ConfluenceOk { get; set; }

        /// <summary>
        /// 网关拒绝的个数
        /// </summary>
        public int? ConfluenceNo { get; set; }

        /// <summary>
        /// 可写的表单项ID
        /// </summary>
        public string[]? CanWriteFormItemIds { get; set; }
    }

    /// <summary>
    /// 节点执行人数据
    /// </summary>
    public class NodeDesignateData
    {
        /// <summary>
        /// 执行人ID列表
        /// </summary>
        public string[] Datas { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// 节点执行结果标签
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// 审批结果
        /// 1: 通过
        /// 2: 不通过
        /// 3: 驳回
        /// </summary>
        public int Taged { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 审批时间
        /// </summary>
        public string TagedTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 审批状态枚举
    /// </summary>
    public enum TagState
    {
        /// <summary>
        /// 通过
        /// </summary>
        Ok = 1,

        /// <summary>
        /// 不通过
        /// </summary>
        No = 2,

        /// <summary>
        /// 驳回
        /// </summary>
        Reject = 3
    }
}

