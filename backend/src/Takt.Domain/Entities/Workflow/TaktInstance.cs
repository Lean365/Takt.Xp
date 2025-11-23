//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInstance.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例实体类
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Workflow
{
  /// <summary>
  /// 工作流实例实体类
  /// </summary>
  [SugarTable("Takt_workflow_instance", "工作流实例表")]
  [SugarIndex("ix_workflow_instance_scheme", nameof(SchemeId), OrderByType.Asc)]
  [SugarIndex("ix_workflow_instance_status", nameof(Status), OrderByType.Asc)]
  [SugarIndex("ix_workflow_instance_priority", nameof(Priority), OrderByType.Asc)]
  [SugarIndex("ix_workflow_instance_urgency", nameof(Urgency), OrderByType.Asc)]
  [SugarIndex("ix_workflow_instance_initiator", nameof(InitiatorId), OrderByType.Asc)]
  public class TaktInstance : TaktBaseEntity
  {

    /// <summary>
    /// 实例标题
    /// </summary>
    [SugarColumn(ColumnName = "instance_title", ColumnDescription = "实例标题", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string InstanceTitle { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义ID
    /// </summary>
    [SugarColumn(ColumnName = "scheme_id", ColumnDescription = "流程定义ID", ColumnDataType = "bigint", IsNullable = false)]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程内容(JSON格式)
    /// </summary>
    [SugarColumn(ColumnName = "scheme_config", ColumnDescription = "流程内容", ColumnDataType = "text", IsNullable = true)]
    public string? SchemeConfig { get; set; }

    /// <summary>
    /// 业务键
    /// </summary>
    [SugarColumn(ColumnName = "business_key", ColumnDescription = "业务键", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 发起人ID
    /// </summary>
    [SugarColumn(ColumnName = "initiator_id", ColumnDescription = "发起人ID", ColumnDataType = "bigint", IsNullable = false)]
    public long InitiatorId { get; set; }

    /// <summary>
    /// 当前节点ID
    /// </summary>
    [SugarColumn(ColumnName = "current_node_id", ColumnDescription = "当前节点ID", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CurrentNodeId { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    [SugarColumn(ColumnName = "current_node_name", ColumnDescription = "当前节点名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 前一个节点ID
    /// </summary>
    [SugarColumn(ColumnName = "previous_node_id", ColumnDescription = "前一个节点ID", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PreviousNodeId { get; set; }

    /// <summary>
    /// 前一个节点名称
    /// </summary>
    [SugarColumn(ColumnName = "previous_node_name", ColumnDescription = "前一个节点名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? PreviousNodeName { get; set; }

    /// <summary>
    /// 优先级(1:低 2:普通 3:高 4:紧急 5:特急)
    /// </summary>
    [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
    public int Priority { get; set; }

    /// <summary>
    /// 紧急程度(1:普通 2:加急 3:特急)
    /// </summary>
    [SugarColumn(ColumnName = "urgency", ColumnDescription = "紧急程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Urgency { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 流程变量(JSON格式)
    /// </summary>
    [SugarColumn(ColumnName = "variables", ColumnDescription = "流程变量", ColumnDataType = "text", IsNullable = true)]
    public string? Variables { get; set; }

    /// <summary>
    /// 表单ID（关联表单定义）
    /// </summary>
    [SugarColumn(ColumnName = "form_id", ColumnDescription = "表单ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? FormId { get; set; }

    /// <summary>
    /// 表单类型(0:动态表单 1:Web自定义表单 2:URL表单)
    /// </summary>
    [SugarColumn(ColumnName = "form_type", ColumnDescription = "表单类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FormType { get; set; }

    /// <summary>
    /// 表单数据(JSON格式)
    /// </summary>
    [SugarColumn(ColumnName = "form_data", ColumnDescription = "表单数据", ColumnDataType = "text", IsNullable = true)]
    public string? FormData { get; set; }

    /// <summary>
    /// 数据库名称
    /// 当表单类型为自定义表单时，用于直接向对应数据库表中写入数据
    /// </summary>
    [SugarColumn(ColumnName = "db_name", ColumnDescription = "数据库名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DbName { get; set; }

    /// <summary>
    /// 所属部门ID
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "所属部门ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? DeptId { get; set; }

    /// <summary>
    /// 状态(0:草稿 1:运行中 2:已完成 3:已停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; }

    /// <summary>
    /// 流程定义
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(SchemeId))]
    public TaktScheme? WorkflowScheme { get; set; }


    /// <summary>
    /// 流程历史列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktInstanceTrans.InstanceId))]
    public List<TaktInstanceTrans>? WorkflowHistories { get; set; }

    /// <summary>
    /// 操作记录列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktInstanceOper.InstanceId))]
    public List<TaktInstanceOper>? WorkflowOperations { get; set; }
  }
}

