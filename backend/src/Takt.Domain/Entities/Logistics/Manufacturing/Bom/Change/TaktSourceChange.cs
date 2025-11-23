#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Change
{
    /// <summary>
    /// 源设变实体类
    /// </summary>
    [SugarTable("Takt_logistics_bom_source_change", "源设变")]
    [SugarIndex("ix_change_number", nameof(ChangeNumber), OrderByType.Asc, false)]
    public class TaktSourceChange : TaktBaseEntity
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设变号码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        [SugarColumn(ColumnName = "model", ColumnDescription = "机种", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Model { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [SugarColumn(ColumnName = "title", ColumnDescription = "标题", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 发行日期
        /// </summary>
        [SugarColumn(ColumnName = "issue_date", ColumnDescription = "发行日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// TCJ担当
        /// </summary>
        [SugarColumn(ColumnName = "tcj_owner", ColumnDescription = "TCJ担当", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TcjOwner { get; set; }

        /// <summary>
        /// TCJ依赖
        /// </summary>
        [SugarColumn(ColumnName = "tcj_depend", ColumnDescription = "TCJ依赖", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TcjDepend { get; set; }

        /// <summary>
        /// 设变会议
        /// </summary>
        [SugarColumn(ColumnName = "change_meeting", ColumnDescription = "设变会议", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeMeeting { get; set; }

        /// <summary>
        /// PP署号
        /// </summary>
        [SugarColumn(ColumnName = "pp_number", ColumnDescription = "PP署号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PpNumber { get; set; }

        /// <summary>
        /// 技联书
        /// </summary>
        [SugarColumn(ColumnName = "tech_doc", ColumnDescription = "技联书", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechDoc { get; set; }

        /// <summary>
        /// 实施
        /// </summary>
        [SugarColumn(ColumnName = "implement", ColumnDescription = "实施", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Implement { get; set; }

        /// <summary>
        /// 主要变更理由
        /// </summary>
        [SugarColumn(ColumnName = "main_reason", ColumnDescription = "主要变更理由", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MainReason { get; set; }

        /// <summary>
        /// 次变更理由
        /// </summary>
        [SugarColumn(ColumnName = "sub_reason", ColumnDescription = "次变更理由", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SubReason { get; set; }

        /// <summary>
        /// 变规
        /// </summary>
        [SugarColumn(ColumnName = "change_rule", ColumnDescription = "变规", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeRule { get; set; }

        /// <summary>
        /// 进行状况
        /// </summary>
        [SugarColumn(ColumnName = "progress", ColumnDescription = "进行状况", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Progress { get; set; }

        /// <summary>
        /// 机番管理
        /// </summary>
        [SugarColumn(ColumnName = "machine_manage", ColumnDescription = "机番管理", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MachineManage { get; set; }

        /// <summary>
        /// 客户承认
        /// </summary>
        [SugarColumn(ColumnName = "customer_approve", ColumnDescription = "客户承认", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerApprove { get; set; }

        /// <summary>
        /// 服务手册订正
        /// </summary>
        [SugarColumn(ColumnName = "service_manual_modify", ColumnDescription = "服务手册订正", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceManualModify { get; set; }

        /// <summary>
        /// 用户手册订正
        /// </summary>
        [SugarColumn(ColumnName = "user_manual_modify", ColumnDescription = "用户手册订正", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UserManualModify { get; set; }

        /// <summary>
        /// 宣传手册订正
        /// </summary>
        [SugarColumn(ColumnName = "promo_manual_modify", ColumnDescription = "宣传手册订正", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PromoManualModify { get; set; }

        /// <summary>
        /// 标准书订正
        /// </summary>
        [SugarColumn(ColumnName = "standard_modify", ColumnDescription = "标准书订正", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StandardModify { get; set; }

        /// <summary>
        /// 情报发行
        /// </summary>
        [SugarColumn(ColumnName = "info_issue", ColumnDescription = "情报发行", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InfoIssue { get; set; }

        /// <summary>
        /// 成本变动
        /// </summary>
        [SugarColumn(ColumnName = "cost_change", ColumnDescription = "成本变动", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? CostChange { get; set; }

        /// <summary>
        /// 成本单位
        /// </summary>
        [SugarColumn(ColumnName = "cost_unit", ColumnDescription = "成本单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostUnit { get; set; }

        /// <summary>
        /// 模具取修费
        /// </summary>
        [SugarColumn(ColumnName = "mould_fee", ColumnDescription = "模具取修费", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? MouldFee { get; set; }

        /// <summary>
        /// 相关图纸
        /// </summary>
        [SugarColumn(ColumnName = "related_drawing", ColumnDescription = "相关图纸", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDrawing { get; set; }

        /// <summary>
        /// 设变内容
        /// </summary>
        [SugarColumn(ColumnName = "change_content", ColumnDescription = "设变内容", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeContent { get; set; }

        /// <summary>
        /// 设变明细集合
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktSourceChangeDetail.ChangeNumber))]
        public List<TaktSourceChangeDetail>? Details { get; set; }
    }
} 
