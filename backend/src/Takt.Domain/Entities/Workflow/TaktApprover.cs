#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktApprover.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流审批人实体类 - 用于会签和加签
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Workflow
{
  /// <summary>
  /// 工作流审批人实体类
  /// </summary>
  [SugarTable("Takt_workflow_approver", "工作流审批人表")]
  [SugarIndex("ix_approver_instance_activity", nameof(InstanceId), OrderByType.Asc, nameof(ActivityId), OrderByType.Asc)]
  [SugarIndex("ix_approver_status", nameof(Status), OrderByType.Asc)]
  public class TaktApprover : TaktBaseEntity
  {
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "实例ID", ColumnDataType = "bigint", IsNullable = false)]
    public long InstanceId { get; set; }

    /// <summary>
    /// 节点ID
    /// </summary>
    [SugarColumn(ColumnName = "activity_id", ColumnDescription = "节点ID", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ActivityId { get; set; } = string.Empty;

    /// <summary>
    /// 审批人ID
    /// </summary>
    [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", ColumnDataType = "bigint", IsNullable = false)]
    public long ApproverId { get; set; }

    /// <summary>
    /// 审批人姓名
    /// </summary>
    [SugarColumn(ColumnName = "approver_name", ColumnDescription = "审批人姓名", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ApproverName { get; set; }

    /// <summary>
    /// 顺序号（用于顺序会签）
    /// </summary>
    [SugarColumn(ColumnName = "order_no", ColumnDescription = "顺序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNo { get; set; }

    /// <summary>
    /// 审批类型
    /// sequential: 顺序会签
    /// parallel_and: 并行会签（且）- 所有人都要通过
    /// parallel_or: 并行会签（或）- 任意一人通过即可
    /// </summary>
    [SugarColumn(ColumnName = "approve_type", ColumnDescription = "审批类型", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "parallel_and")]
    public string ApproveType { get; set; } = "parallel_and";

    /// <summary>
    /// 状态
    /// 0: 待审批
    /// 1: 已通过
    /// 2: 已拒绝
    /// 3: 驳回
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    [SugarColumn(ColumnName = "verify_comment", ColumnDescription = "审批意见", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? VerifyComment { get; set; }

    /// <summary>
    /// 审批时间
    /// </summary>
    [SugarColumn(ColumnName = "verify_date", ColumnDescription = "审批时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? VerifyDate { get; set; }

    /// <summary>
    /// 是否返回原节点（加签完成后是否返回原节点审批）
    /// </summary>
    [SugarColumn(ColumnName = "return_to_sign_node", ColumnDescription = "是否返回原节点", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
    public bool ReturnToSignNode { get; set; }

    /// <summary>
    /// 原因（加签原因等）
    /// </summary>
    [SugarColumn(ColumnName = "reason", ColumnDescription = "原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Reason { get; set; }

    /// <summary>
    /// 创建人ID（加签人ID）
    /// </summary>
    [SugarColumn(ColumnName = "create_user_id", ColumnDescription = "创建人ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? CreateUserId { get; set; }

    /// <summary>
    /// 创建人姓名（加签人姓名）
    /// </summary>
    [SugarColumn(ColumnName = "create_user_name", ColumnDescription = "创建人姓名", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? CreateUserName { get; set; }
  }
}

