//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInstanceTrans.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例流转历史实体类
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Workflow
{
  /// <summary>
  /// 工作流实例流转历史实体类
  /// </summary>
  [SugarTable("Takt_workflow_instance_trans", "实例流转")]
  [SugarIndex("ix_workflow_instance_trans_instance", nameof(InstanceId), OrderByType.Asc)]
  [SugarIndex("ix_workflow_instance_trans_node", nameof(StartNodeId), OrderByType.Asc)]
  [SugarIndex("ix_workflow_instance_trans_time", nameof(CreateTime), OrderByType.Desc)]
  public class TaktInstanceTrans : TaktBaseEntity
  {
    /// <summary>
    /// 实例ID
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false)]
    public long InstanceId { get; set; }

    /// <summary>
    /// 开始节点ID
    /// </summary>
    [SugarColumn(ColumnName = "start_node_id", ColumnDescription = "开始节点ID", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string StartNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 开始节点类型
    /// </summary>
    [SugarColumn(ColumnName = "start_node_type", ColumnDescription = "开始节点类型", ColumnDataType = "int", IsNullable = false)]
    public int StartNodeType { get; set; }

    /// <summary>
    /// 开始节点名称
    /// </summary>
    [SugarColumn(ColumnName = "start_node_name", ColumnDescription = "开始节点名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string StartNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点ID
    /// </summary>
    [SugarColumn(ColumnName = "to_node_id", ColumnDescription = "目标节点ID", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ToNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点类型
    /// </summary>
    [SugarColumn(ColumnName = "to_node_type", ColumnDescription = "目标节点类型", ColumnDataType = "int", IsNullable = false)]
    public int ToNodeType { get; set; }

    /// <summary>
    /// 目标节点名称
    /// </summary>
    [SugarColumn(ColumnName = "to_node_name", ColumnDescription = "目标节点名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ToNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 转化时间
    /// </summary>
    [SugarColumn(ColumnName = "trans_time", ColumnDescription = "转化时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime TransTime { get; set; }

    /// <summary>
    /// 状态
    /// 0:草稿
    /// 1:运行中
    /// 2:已完成
    /// 3:已停用
    /// 4:已终止
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; }

    /// <summary>
    /// 工作流实例
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(InstanceId))]
    public TaktInstance? WorkflowInstance { get; set; }
  }
}

