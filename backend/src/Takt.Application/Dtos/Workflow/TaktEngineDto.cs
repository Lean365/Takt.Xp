//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktEngineDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流引擎相关DTO类
//===================================================================

namespace Takt.Application.Dtos.Workflow;


/// <summary>
/// 工作流节点DTO
/// </summary>
public class TaktNodeDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNodeDto()
    {
        NodeId = string.Empty;
        NodeName = string.Empty;
        NodeType = string.Empty;
    }

    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 节点名称
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// 节点类型
    /// </summary>
    public string NodeType { get; set; } = string.Empty;

    /// <summary>
    /// 节点配置(JSON格式)
    /// </summary>
    public string? NodeConfig { get; set; }

    /// <summary>
    /// 审批人类型(1:指定用户 2:角色 3:部门 4:动态)
    /// </summary>
    public int ApproverType { get; set; }

    /// <summary>
    /// 审批人配置(JSON格式)
    /// </summary>
    public string? ApproverConfig { get; set; }
}

/// <summary>
/// 工作流审批DTO
/// </summary>
public class TaktWorkflowApproveDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkflowApproveDto()
    {
        NodeId = string.Empty;
        OperOpinion = string.Empty;
    }

    /// <summary>
    /// 工作流实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 操作类型(1:同意 2:拒绝 3:退回 4:转办 5:委托)
    /// </summary>
    public int OperType { get; set; }

    /// <summary>
    /// 操作意见
    /// </summary>
    public string OperOpinion { get; set; } = string.Empty;

    /// <summary>
    /// 操作数据(JSON格式)
    /// </summary>
    public string? OperData { get; set; }

    /// <summary>
    /// 目标用户ID(转办/委托时使用)
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? TargetUserId { get; set; }

    /// <summary>
    /// 驳回步骤，即驳回到的节点ID（退回时使用）
    /// </summary>
    public string? NodeRejectStep { get; set; }

    /// <summary>
    /// 驳回类型（退回时使用）
    /// null:使用节点配置的驳回类型
    /// 0:前一步
    /// 1:第一步
    /// 2:指定节点，使用NodeRejectStep
    /// </summary>
    public string? NodeRejectType { get; set; }

    /// <summary>
    /// 表单数据（如果该节点有可以修改的表单项时，会提交表单数据信息）
    /// </summary>
    public string? FormData { get; set; }

    /// <summary>
    /// 下一个节点执行权限类型（运行时指定执行者时使用）
    /// RUNTIME_SPECIAL_ROLE:运行时指定角色
    /// RUNTIME_SPECIAL_USER:运行时指定用户
    /// </summary>
    public string? NodeDesignateType { get; set; }

    /// <summary>
    /// 下一个节点执行者（运行时指定执行者时使用）
    /// 如果NodeDesignateType为RUNTIME_SPECIAL_ROLE，则该值为指定的角色ID数组
    /// 如果NodeDesignateType为RUNTIME_SPECIAL_USER，则该值为指定的用户ID数组
    /// </summary>
    public string[]? NodeDesignates { get; set; }
}

/// <summary>
/// 工作流启动DTO
/// </summary>
public class TaktWorkflowStartDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkflowStartDto()
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
    /// 发起人ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InitiatorId { get; set; }

    /// <summary>
    /// 流程变量(JSON格式)
    /// </summary>
    public string? Variables { get; set; }
}


#region 并行流程相关DTO

/// <summary>
/// 并行分支配置DTO
/// </summary>
public class TaktParallelBranchConfigDto
{
    /// <summary>
    /// 分支列表
    /// </summary>
    public List<TaktParallelBranchDto> Branches { get; set; } = new();

    /// <summary>
    /// 汇聚策略(1:全部完成 2:任一完成 3:指定数量完成)
    /// </summary>
    public int JoinStrategy { get; set; }

    /// <summary>
    /// 汇聚数量(当JoinStrategy为3时使用)
    /// </summary>
    public int? JoinCount { get; set; }

    /// <summary>
    /// 超时时间(分钟)
    /// </summary>
    public int? TimeoutMinutes { get; set; }
}

/// <summary>
/// 并行分支DTO
/// </summary>
public class TaktParallelBranchDto
{
    /// <summary>
    /// 分支名称
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// 分支键
    /// </summary>
    public string BranchKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义ID
    /// </summary>
    public long SchemeId { get; set; }

    /// <summary>
    /// 开始节点ID
    /// </summary>
    public string StartNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 开始节点名称
    /// </summary>
    public string StartNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 分支变量
    /// </summary>
    public string? Variables { get; set; }

    /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// 紧急程度
    /// </summary>
    public int Urgency { get; set; }
}

/// <summary>
/// 并行分支状态DTO
/// </summary>
public class TaktParallelBranchStatusDto
{
    /// <summary>
    /// 实例ID
    /// </summary>
    public long InstanceId { get; set; }

    /// <summary>
    /// 总分支数
    /// </summary>
    public int TotalBranches { get; set; }

    /// <summary>
    /// 已完成分支数
    /// </summary>
    public int CompletedBranches { get; set; }

    /// <summary>
    /// 运行中分支数
    /// </summary>
    public int RunningBranches { get; set; }

    /// <summary>
    /// 已暂停分支数
    /// </summary>
    public int SuspendedBranches { get; set; }

    /// <summary>
    /// 已终止分支数
    /// </summary>
    public int TerminatedBranches { get; set; }

    /// <summary>
    /// 是否全部完成
    /// </summary>
    public bool IsAllCompleted { get; set; }

    /// <summary>
    /// 完成百分比
    /// </summary>
    public double CompletionPercentage { get; set; }
}

/// <summary>
/// 并行分支信息DTO
/// </summary>
public class TaktParallelBranchInfoDto
{
    /// <summary>
    /// 分支实例ID
    /// </summary>
    public long BranchInstanceId { get; set; }

    /// <summary>
    /// 分支名称
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 进度
    /// </summary>
    public double Progress { get; set; }
}

/// <summary>
/// 并行工作流信息DTO
/// </summary>
public class TaktParallelWorkflowInfoDto
{
    /// <summary>
    /// 主实例ID
    /// </summary>
    public long MainInstanceId { get; set; }

    /// <summary>
    /// 主实例标题
    /// </summary>
    public string MainInstanceTitle { get; set; } = string.Empty;

    /// <summary>
    /// 主实例状态
    /// </summary>
    public int MainInstanceStatus { get; set; }

    /// <summary>
    /// 分支列表
    /// </summary>
    public List<TaktParallelBranchInfoDto> Branches { get; set; } = new();

    /// <summary>
    /// 总分支数
    /// </summary>
    public int TotalBranches { get; set; }

    /// <summary>
    /// 已完成分支数
    /// </summary>
    public int CompletedBranches { get; set; }

    /// <summary>
    /// 运行中分支数
    /// </summary>
    public int RunningBranches { get; set; }
}

#endregion

#region 定时任务相关DTO

/// <summary>
/// 工作流定时任务DTO
/// </summary>
public class TaktWorkflowScheduledTaskDto
{
    /// <summary>
    /// 任务ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 任务名称
    /// </summary>
    public string TaskName { get; set; } = string.Empty;

    /// <summary>
    /// 任务类型(1:启动工作流 2:审批提醒 3:超时处理 4:定期检查)
    /// </summary>
    public int TaskType { get; set; }

    /// <summary>
    /// Cron表达式
    /// </summary>
    public string CronExpression { get; set; } = string.Empty;

    /// <summary>
    /// 工作流实例ID
    /// </summary>
    public long? WorkflowInstanceId { get; set; }

    /// <summary>
    /// 工作流方案ID
    /// </summary>
    public long? WorkflowSchemeId { get; set; }

    /// <summary>
    /// 节点ID
    /// </summary>
    public string? NodeId { get; set; }

    /// <summary>
    /// 节点名称
    /// </summary>
    public string? NodeName { get; set; }

    /// <summary>
    /// 任务数据
    /// </summary>
    public Dictionary<string, object>? TaskData { get; set; }

    /// <summary>
    /// 状态(0:暂停 1:启用)
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 下次运行时间
    /// </summary>
    public DateTime? NextRunTime { get; set; }
}

/// <summary>
/// 工作流定时任务查询DTO
/// </summary>
public class TaktWorkflowScheduledTaskQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 工作流实例ID
    /// </summary>
    public long? WorkflowInstanceId { get; set; }

    /// <summary>
    /// 工作流方案ID
    /// </summary>
    public long? WorkflowSchemeId { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    public string? TaskName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }
}

/// <summary>
/// 工作流超时配置DTO
/// </summary>
public class TaktWorkflowTimeoutConfigDto
{
    /// <summary>
    /// 超时时间(分钟)
    /// </summary>
    public int TimeoutMinutes { get; set; }

    /// <summary>
    /// 超时动作(auto_approve:自动审批 auto_reject:自动拒绝 escalate:升级 suspend:暂停)
    /// </summary>
    public string TimeoutAction { get; set; } = string.Empty;

    /// <summary>
    /// 升级级别配置
    /// </summary>
    public List<TaktWorkflowEscalationLevelDto> EscalationLevels { get; set; } = new();
}

/// <summary>
/// 工作流升级级别DTO
/// </summary>
public class TaktWorkflowEscalationLevelDto
{
    /// <summary>
    /// 级别
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 超时时间(分钟)
    /// </summary>
    public int TimeoutMinutes { get; set; }

    /// <summary>
    /// 动作
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// 审批人ID列表
    /// </summary>
    public List<long> ApproverIds { get; set; } = new();
}

#endregion

#region 通知相关DTO

/// <summary>
/// 工作流通知基础DTO
/// </summary>
public class TaktWorkflowNotificationDto
{
    /// <summary>
    /// 通知ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 工作流实例ID
    /// </summary>
    public long WorkflowInstanceId { get; set; }

    /// <summary>
    /// 收件人ID
    /// </summary>
    public long RecipientId { get; set; }

    /// <summary>
    /// 收件人姓名
    /// </summary>
    public string RecipientName { get; set; } = string.Empty;

    /// <summary>
    /// 通知类型
    /// </summary>
    public string NotificationType { get; set; } = string.Empty;

    /// <summary>
    /// 通知数据
    /// </summary>
    public Dictionary<string, object>? Data { get; set; }
}

/// <summary>
/// 工作流状态变更通知DTO
/// </summary>
public class TaktWorkflowStatusNotificationDto : TaktWorkflowNotificationDto
{
    /// <summary>
    /// 旧状态
    /// </summary>
    public int OldStatus { get; set; }

    /// <summary>
    /// 新状态
    /// </summary>
    public int NewStatus { get; set; }

    /// <summary>
    /// 状态变更原因
    /// </summary>
    public string? StatusChangeReason { get; set; }
}

/// <summary>
/// 工作流审批通知DTO
/// </summary>
public class TaktWorkflowApprovalNotificationDto : TaktWorkflowNotificationDto
{
    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 节点名称
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// 操作类型
    /// </summary>
    public int OperType { get; set; }

    /// <summary>
    /// 操作意见
    /// </summary>
    public string? OperOpinion { get; set; }
}

/// <summary>
/// 工作流超时通知DTO
/// </summary>
public class TaktWorkflowTimeoutNotificationDto : TaktWorkflowNotificationDto
{
    /// <summary>
    /// 超时时间(分钟)
    /// </summary>
    public int TimeoutMinutes { get; set; }

    /// <summary>
    /// 超时动作
    /// </summary>
    public string TimeoutAction { get; set; } = string.Empty;

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? CurrentNodeName { get; set; }
}

/// <summary>
/// 工作流完成通知DTO
/// </summary>
public class TaktWorkflowCompletionNotificationDto : TaktWorkflowNotificationDto
{
    /// <summary>
    /// 完成时间
    /// </summary>
    public DateTime CompletionTime { get; set; }

    /// <summary>
    /// 总耗时(分钟)
    /// </summary>
    public int TotalDuration { get; set; }

    /// <summary>
    /// 审批人列表
    /// </summary>
    public List<string> Approvers { get; set; } = new();
}

/// <summary>
/// 工作流异常通知DTO
/// </summary>
public class TaktWorkflowExceptionNotificationDto : TaktWorkflowNotificationDto
{
    /// <summary>
    /// 异常类型
    /// </summary>
    public string ExceptionType { get; set; } = string.Empty;

    /// <summary>
    /// 异常消息
    /// </summary>
    public string ExceptionMessage { get; set; } = string.Empty;

    /// <summary>
    /// 异常堆栈
    /// </summary>
    public string? ExceptionStackTrace { get; set; }
}

/// <summary>
/// 通知结果DTO
/// </summary>
public class TaktNotificationResultDto
{
    /// <summary>
    /// 通知ID
    /// </summary>
    public string NotificationId { get; set; } = string.Empty;

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
}

/// <summary>
/// 工作流通知历史DTO
/// </summary>
public class TaktWorkflowNotificationHistoryDto
{
    /// <summary>
    /// ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 工作流实例ID
    /// </summary>
    public long WorkflowInstanceId { get; set; }

    /// <summary>
    /// 通知类型
    /// </summary>
    public string NotificationType { get; set; } = string.Empty;

    /// <summary>
    /// 收件人ID
    /// </summary>
    public long RecipientId { get; set; }

    /// <summary>
    /// 收件人姓名
    /// </summary>
    public string RecipientName { get; set; } = string.Empty;

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime? SentTime { get; set; }
}

/// <summary>
/// 工作流通知查询DTO
/// </summary>
public class TaktWorkflowNotificationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 工作流实例ID
    /// </summary>
    public long? WorkflowInstanceId { get; set; }

    /// <summary>
    /// 通知类型
    /// </summary>
    public string? NotificationType { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

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
/// 工作流通知模板DTO
/// </summary>
public class TaktWorkflowNotificationTemplateDto
{
    /// <summary>
    /// ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 模板类型
    /// </summary>
    public string TemplateType { get; set; } = string.Empty;

    /// <summary>
    /// 模板名称
    /// </summary>
    public string TemplateName { get; set; } = string.Empty;

    /// <summary>
    /// 主题
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 邮件模板
    /// </summary>
    public string? EmailTemplate { get; set; }

    /// <summary>
    /// 短信模板
    /// </summary>
    public string? SmsTemplate { get; set; }

    /// <summary>
    /// 推送模板
    /// </summary>
    public string? PushTemplate { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 通知测试结果DTO
/// </summary>
public class TaktNotificationTestResultDto
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
}

#endregion

#region 新增工作流接口相关DTO

/// <summary>
/// 工作流拒绝DTO
/// </summary>
public class TaktWorkflowRejectDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkflowRejectDto()
    {
        NodeId = string.Empty;
        RejectReason = string.Empty;
        RejectData = string.Empty;
    }

    /// <summary>
    /// 工作流实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 拒绝原因
    /// </summary>
    public string RejectReason { get; set; } = string.Empty;

    /// <summary>
    /// 拒绝数据
    /// </summary>
    public string RejectData { get; set; } = string.Empty;
}

/// <summary>
/// 工作流转换DTO
/// </summary>
public class TaktWorkflowTransitDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkflowTransitDto()
    {
        FromNodeId = string.Empty;
        ToNodeId = string.Empty;
        TransitReason = string.Empty;
        TransitData = string.Empty;
    }

    /// <summary>
    /// 工作流实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 源节点ID
    /// </summary>
    public string FromNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 转换原因
    /// </summary>
    public string TransitReason { get; set; } = string.Empty;

    /// <summary>
    /// 转换数据
    /// </summary>
    public string TransitData { get; set; } = string.Empty;
}

/// <summary>
/// 条件评估DTO
/// </summary>
public class TaktConditionEvaluateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktConditionEvaluateDto()
    {
        Expression = string.Empty;
        Variables = new Dictionary<string, object>();
    }

    /// <summary>
    /// 条件表达式
    /// </summary>
    public string Expression { get; set; } = string.Empty;

    /// <summary>
    /// 变量字典
    /// </summary>
    public Dictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();
}

/// <summary>
/// 条件评估结果DTO
/// </summary>
public class TaktConditionEvaluateResultDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktConditionEvaluateResultDto()
    {
        Expression = string.Empty;
        ErrorMessage = string.Empty;
    }

    /// <summary>
    /// 条件表达式
    /// </summary>
    public string Expression { get; set; } = string.Empty;

    /// <summary>
    /// 评估结果
    /// </summary>
    public bool Result { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
}

/// <summary>
/// 工作流待办任务查询DTO
/// </summary>
public class TaktWorkflowTodoQueryDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkflowTodoQueryDto()
    {
        Keyword = string.Empty;
        SchemeId = string.Empty;
        NodeId = string.Empty;
    }

    /// <summary>
    /// 关键词
    /// </summary>
    public string Keyword { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义ID
    /// </summary>
    public string SchemeId { get; set; } = string.Empty;

    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 页码
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 页大小
    /// </summary>
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// 工作流已办任务查询DTO
/// </summary>
public class TaktWorkflowDoneQueryDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkflowDoneQueryDto()
    {
        Keyword = string.Empty;
        SchemeId = string.Empty;
        NodeId = string.Empty;
        OperType = string.Empty;
    }

    /// <summary>
    /// 关键词
    /// </summary>
    public string Keyword { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义ID
    /// </summary>
    public string SchemeId { get; set; } = string.Empty;

    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 操作类型
    /// </summary>
    public string OperType { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 页码
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 页大小
    /// </summary>
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// 工作流任务DTO
/// </summary>
public class TaktWorkflowTaskDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkflowTaskDto()
    {
        InstanceId = string.Empty;
        InstanceName = string.Empty;
        SchemeId = string.Empty;
        SchemeName = string.Empty;
        NodeId = string.Empty;
        NodeName = string.Empty;
        NodeType = string.Empty;
        StartUserName = string.Empty;
        CurrentUserName = string.Empty;
        Status = string.Empty;
    }

    /// <summary>
    /// 任务ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TaskId { get; set; }

    /// <summary>
    /// 工作流实例ID
    /// </summary>
    public string InstanceId { get; set; } = string.Empty;

    /// <summary>
    /// 工作流实例名称
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义ID
    /// </summary>
    public string SchemeId { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义名称
    /// </summary>
    public string SchemeName { get; set; } = string.Empty;

    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 节点名称
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// 节点类型
    /// </summary>
    public string NodeType { get; set; } = string.Empty;

    /// <summary>
    /// 发起人
    /// </summary>
    public string StartUserName { get; set; } = string.Empty;

    /// <summary>
    /// 当前处理人
    /// </summary>
    public string CurrentUserName { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}

#endregion 

