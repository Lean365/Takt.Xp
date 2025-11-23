#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Change
{
    /// <summary>
    /// 设计变更计执行主表
    /// </summary>
    [SugarTable("Takt_logistics_bom_change_execution", "设变执行")]
    public class TaktDesignChangeExecution : TaktBaseEntity
    {
        /// <summary>
        /// 发行日期
        /// </summary>
        [SugarColumn(ColumnName = "issue_date", ColumnDescription = "发行日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// 设计变更号码
        /// </summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设计变更号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeNumber { get; set; } = string.Empty;


        /// <summary>
        /// 标题
        /// </summary>
        [SugarColumn(ColumnName = "title", ColumnDescription = "标题", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Title { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        [SugarColumn(ColumnName = "change_description", ColumnDescription = "变更内容", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeDescription { get; set; }

        /// <summary>
        /// 技术担当
        /// </summary>
        [SugarColumn(ColumnName = "tech_owner", ColumnDescription = "技术担当", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechOwner { get; set; }

        /// <summary>
        /// 仕损金额
        /// </summary>
        [SugarColumn(ColumnName = "loss_amount", ColumnDescription = "仕损金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? LossAmount { get; set; }

        /// <summary>
        /// 管理区分(0=未分配 1=全部 2=部管 3=技术 4=内部)
        /// </summary>
        [SugarColumn(ColumnName = "manage_division", ColumnDescription = "管理区分", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ManageDivision { get; set; } = 0;

        /// <summary>
        /// 设变文档
        /// </summary>
        [SugarColumn(ColumnName = "change_doc", ColumnDescription = "设变文档", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeDoc { get; set; }

        /// <summary>
        /// 联络文档
        /// </summary>
        [SugarColumn(ColumnName = "contact_doc", ColumnDescription = "联络文档", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactDoc { get; set; }

        /// <summary>
        /// 技联文档
        /// </summary>
        [SugarColumn(ColumnName = "tech_union_doc", ColumnDescription = "技联文档", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechUnionDoc { get; set; }

        /// <summary>
        /// P番文档
        /// </summary>
        [SugarColumn(ColumnName = "p_number_doc", ColumnDescription = "P番文档", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PNumberDoc { get; set; }

        /// <summary>
        /// 外部文档
        /// </summary>
        [SugarColumn(ColumnName = "external_doc", ColumnDescription = "外部文档", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExternalDoc { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        [SugarColumn(ColumnName = "login_date", ColumnDescription = "登录日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 状态(1=工作的 2=取消的 3=发行的 4=PP中 5=固定的 6=挂起的 7=拒绝的)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
        public int Status { get; set; } = 5;

        /// <summary>
        /// 设计变更明细集合
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktDesignChangeExecutionDetail.ChangeNumber))]
        public List<TaktDesignChangeExecutionDetail>? Details { get; set; }



    }
} 
