//===================================================================
// 项目名: Takt.Application
// 文件名: TaktInstanceDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 工作流实例数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Takt.Application.Dtos.Workflow
{
    /// <summary>
    /// 工作流实例基础DTO（与TaktInstance实体字段严格对应）
    /// </summary>
    public class TaktInstanceDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktInstanceDto()
        {
            InstanceTitle = string.Empty;
            CreateBy = string.Empty;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long InstanceId { get; set; }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long SchemeId { get; set; }

        /// <summary>
        /// 流程内容(JSON格式)
        /// </summary>
        public string? SchemeConfig { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long InitiatorId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string? CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string? CurrentNodeName { get; set; }

        /// <summary>
        /// 前一个节点ID
        /// </summary>
        public string? PreviousNodeId { get; set; }

        /// <summary>
        /// 前一个节点名称
        /// </summary>
        public string? PreviousNodeName { get; set; }

        /// <summary>
        /// 优先级(1:低 2:普通 3:高 4:紧急 5:特急)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 紧急程度(1:普通 2:加急 3:特急)
        /// </summary>
        public int Urgency { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }

        /// <summary>
        /// 表单ID（关联表单定义）
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long? FormId { get; set; }

        /// <summary>
        /// 表单类型(0:动态表单 1:Web自定义表单 2:URL表单)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:运行中 2:已完成 3:已停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 工作流实例查询DTO
    /// </summary>
    public class TaktInstanceQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktInstanceQueryDto()
        {
            InstanceTitle = string.Empty;
            BusinessKey = string.Empty;
        }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string? InstanceTitle { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int? Priority { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public int? Urgency { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 工作流实例创建DTO
    /// </summary>
    public class TaktInstanceCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktInstanceCreateDto()
        {
            InstanceTitle = string.Empty;
        }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long SchemeId { get; set; }

        /// <summary>
        /// 流程内容(JSON格式)
        /// </summary>
        public string? SchemeConfig { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long InitiatorId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string? CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string? CurrentNodeName { get; set; }

        /// <summary>
        /// 前一个节点ID
        /// </summary>
        public string? PreviousNodeId { get; set; }

        /// <summary>
        /// 前一个节点名称
        /// </summary>
        public string? PreviousNodeName { get; set; }

        /// <summary>
        /// 优先级(1:低 2:普通 3:高 4:紧急 5:特急)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 紧急程度(1:普通 2:加急 3:特急)
        /// </summary>
        public int Urgency { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }

        /// <summary>
        /// 表单ID（关联表单定义）
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long? FormId { get; set; }

        /// <summary>
        /// 表单类型(0:动态表单 1:Web自定义表单 2:URL表单)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:运行中 2:已完成 3:已停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 工作流实例更新DTO
    /// </summary>
    public class TaktInstanceUpdateDto : TaktInstanceCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktInstanceUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long InstanceId { get; set; }
    }

    /// <summary>
    /// 工作流实例状态DTO
    /// </summary>
    public class TaktInstanceStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktInstanceStatusDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long InstanceId { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:运行中 2:已完成 3:已暂停 4:已终止)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 工作流启动DTO
    /// </summary>
    public class TaktInstanceStartDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktInstanceStartDto()
        {
            InstanceTitle = string.Empty;
        }

        /// <summary>
        /// 流程定义ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long SchemeId { get; set; }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }
    }

    /// <summary>
    /// 工作流实例模板DTO
    /// </summary>
    public class TaktInstanceTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktInstanceTemplateDto()
        {
            InstanceTitle = string.Empty;
        }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long SchemeId { get; set; }

        /// <summary>
        /// 流程内容(JSON格式)
        /// </summary>
        public string? SchemeConfig { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long InitiatorId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string? CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string? CurrentNodeName { get; set; }

        /// <summary>
        /// 前一个节点ID
        /// </summary>
        public string? PreviousNodeId { get; set; }

        /// <summary>
        /// 前一个节点名称
        /// </summary>
        public string? PreviousNodeName { get; set; }

        /// <summary>
        /// 优先级(1:低 2:普通 3:高 4:紧急 5:特急)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 紧急程度(1:普通 2:加急 3:特急)
        /// </summary>
        public int Urgency { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }

        /// <summary>
        /// 表单ID（关联表单定义）
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long? FormId { get; set; }

        /// <summary>
        /// 表单类型(0:动态表单 1:Web自定义表单 2:URL表单)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:运行中 2:已完成 3:已停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 工作流实例导入DTO
    /// </summary>
    public class TaktInstanceImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktInstanceImportDto()
        {
            InstanceTitle = string.Empty;
        }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long SchemeId { get; set; }

        /// <summary>
        /// 流程内容(JSON格式)
        /// </summary>
        public string? SchemeConfig { get; set; }

        /// <summary>
        /// 业务键
        /// </summary>
        public string? BusinessKey { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long InitiatorId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string? CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string? CurrentNodeName { get; set; }

        /// <summary>
        /// 前一个节点ID
        /// </summary>
        public string? PreviousNodeId { get; set; }

        /// <summary>
        /// 前一个节点名称
        /// </summary>
        public string? PreviousNodeName { get; set; }

        /// <summary>
        /// 优先级(1:低 2:普通 3:高 4:紧急 5:特急)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 紧急程度(1:普通 2:加急 3:特急)
        /// </summary>
        public int Urgency { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 流程变量(JSON格式)
        /// </summary>
        public string? Variables { get; set; }

        /// <summary>
        /// 表单ID（关联表单定义）
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long? FormId { get; set; }

        /// <summary>
        /// 表单类型(0:动态表单 1:Web自定义表单 2:URL表单)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单数据(JSON格式)
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:运行中 2:已完成 3:已停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 工作流实例导出DTO
    /// </summary>
    public class TaktInstanceExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktInstanceExportDto()
        {
            InstanceTitle = string.Empty;
            BusinessKey = string.Empty;
            Status = string.Empty;
            Priority = string.Empty;
            Urgency = string.Empty;
        }

        /// <summary>
        /// 实例标题
        /// </summary>
        public string InstanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 流程定义ID
        /// </summary>
        public string SchemeId { get; set; } = string.Empty;

        /// <summary>
        /// 流程内容
        /// </summary>
        public string SchemeConfig { get; set; } = string.Empty;

        /// <summary>
        /// 业务键
        /// </summary>
        public string BusinessKey { get; set; } = string.Empty;

        /// <summary>
        /// 发起人ID
        /// </summary>
        public string InitiatorId { get; set; } = string.Empty;

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string CurrentNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string CurrentNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 前一个节点ID
        /// </summary>
        public string PreviousNodeId { get; set; } = string.Empty;

        /// <summary>
        /// 前一个节点名称
        /// </summary>
        public string PreviousNodeName { get; set; } = string.Empty;

        /// <summary>
        /// 优先级
        /// </summary>
        public string Priority { get; set; } = string.Empty;

        /// <summary>
        /// 紧急程度
        /// </summary>
        public string Urgency { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 流程变量
        /// </summary>
        public string Variables { get; set; } = string.Empty;

        /// <summary>
        /// 表单ID
        /// </summary>
        public string FormId { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型
        /// </summary>
        public string FormType { get; set; } = string.Empty;

        /// <summary>
        /// 表单数据
        /// </summary>
        public string FormData { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 
