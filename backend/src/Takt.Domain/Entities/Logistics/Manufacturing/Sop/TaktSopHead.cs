#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSopHead.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 标准作业程序主表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Sop
{
    /// <summary>
    /// 标准作业程序主表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 描述: SOP主数据管理
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_sop_head", "标准作业程序主表")]
    [SugarIndex("ix_sop_id", nameof(SopId), OrderByType.Asc, true)]
    [SugarIndex("ix_model_type", nameof(ModelType), OrderByType.Asc, false)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    public class TaktSopHead : TaktBaseEntity
    {
        /// <summary>
        /// SOP编号
        /// </summary>
        [SugarColumn(ColumnName = "sop_id", ColumnDescription = "SOP编号", Length = 20, ColumnDataType = "varchar", IsNullable = false, IsPrimaryKey = true)]
        public string SopId { get; set; } = string.Empty;

        /// <summary>
        /// SOP名称
        /// </summary>
        [SugarColumn(ColumnName = "sop_name", ColumnDescription = "SOP名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SopName { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        [SugarColumn(ColumnName = "model_type", ColumnDescription = "机种", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ModelType { get; set; }

        /// <summary>
        /// 仕向
        /// </summary>
        [SugarColumn(ColumnName = "destination", ColumnDescription = "仕向", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Destination { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialCode { get; set; }

        /// <summary>
        /// 版本号(如V1.0.0)
        /// </summary>
        [SugarColumn(ColumnName = "version", ColumnDescription = "版本号", Length = 10, ColumnDataType = "varchar", IsNullable = false)]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// 状态(0=草稿 1=发布)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        // ==================== 担当信息 ====================

        /// <summary>
        /// 工艺担当
        /// </summary>
        [SugarColumn(ColumnName = "process_manager", ColumnDescription = "工艺担当", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessManager { get; set; }

        /// <summary>
        /// 电器担当
        /// </summary>
        [SugarColumn(ColumnName = "electrical_manager", ColumnDescription = "电器担当", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ElectricalManager { get; set; }

        /// <summary>
        /// 机构担当
        /// </summary>
        [SugarColumn(ColumnName = "mechanical_manager", ColumnDescription = "机构担当", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MechanicalManager { get; set; }

        // ==================== 流程管控 ====================

        /// <summary>
        /// 确认人
        /// </summary>
        [SugarColumn(ColumnName = "confirmed_by", ColumnDescription = "确认人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ConfirmedBy { get; set; }

        /// <summary>
        /// 确认日期
        /// </summary>
        [SugarColumn(ColumnName = "confirm_date", ColumnDescription = "确认日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ConfirmDate { get; set; }

        /// <summary>
        /// 承认人
        /// </summary>
        [SugarColumn(ColumnName = "acknowledged_by", ColumnDescription = "承认人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AcknowledgedBy { get; set; }

        /// <summary>
        /// 承认日期
        /// </summary>
        [SugarColumn(ColumnName = "acknowledge_date", ColumnDescription = "承认日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? AcknowledgeDate { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        [SugarColumn(ColumnName = "approved_by", ColumnDescription = "审批人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovedBy { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        [SugarColumn(ColumnName = "approve_date", ColumnDescription = "审批日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ApproveDate { get; set; }

        /// <summary>
        /// 审批状态(0=待确认 1=已确认 2=已承认 3=已审批 4=已发布)
        /// </summary>
        [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审批状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ApprovalStatus { get; set; } = 0;

        // ==================== 导航属性 ====================

        /// <summary>
        /// SOP内容列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<TaktSopContent>? SopContents { get; set; }

        /// <summary>
        /// SOP变更记录列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<TaktSopChange>? SopChanges { get; set; }

        /// <summary>
        /// SOP工序流程列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<TaktSopProcess>? SopProcesses { get; set; }
    }
}



