#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Change
{
    /// <summary>
    /// 设计变更计执行明细表
    /// </summary>
    [SugarTable("Takt_logistics_bom_change_execution_detail", "设变执行明细")]
    public class TaktDesignChangeExecutionDetail : TaktBaseEntity
    {
        /// <summary>
        /// 设计变更号码
        /// </summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设计变更号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        [SugarColumn(ColumnName = "model", ColumnDescription = "机种", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Model { get; set; }

        /// <summary>
        /// 完成品
        /// </summary>
        [SugarColumn(ColumnName = "finished_product", ColumnDescription = "完成品", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FinishedProduct { get; set; }

        /// <summary>
        /// 上阶
        /// </summary>
        [SugarColumn(ColumnName = "parent_product", ColumnDescription = "上阶", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParentProduct { get; set; }

        /// <summary>
        /// 旧品
        /// </summary>
        [SugarColumn(ColumnName = "old_product", ColumnDescription = "旧品", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldProduct { get; set; }

        /// <summary>
        /// 旧描述
        /// </summary>
        [SugarColumn(ColumnName = "old_description", ColumnDescription = "旧描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldDescription { get; set; }

        /// <summary>
        /// 旧用量
        /// </summary>
        [SugarColumn(ColumnName = "old_quantity", ColumnDescription = "旧用量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 旧位置
        /// </summary>
        [SugarColumn(ColumnName = "old_position", ColumnDescription = "旧位置", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPosition { get; set; }

        /// <summary>
        /// 旧库存
        /// </summary>
        [SugarColumn(ColumnName = "old_stock", ColumnDescription = "旧库存", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? OldStock { get; set; }

        /// <summary>
        /// 新品
        /// </summary>
        [SugarColumn(ColumnName = "new_product", ColumnDescription = "新品", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewProduct { get; set; }

        /// <summary>
        /// 新描述
        /// </summary>
        [SugarColumn(ColumnName = "new_description", ColumnDescription = "新描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewDescription { get; set; }

        /// <summary>
        /// 新用量
        /// </summary>
        [SugarColumn(ColumnName = "new_quantity", ColumnDescription = "新用量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 新位置
        /// </summary>
        [SugarColumn(ColumnName = "new_position", ColumnDescription = "新位置", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPosition { get; set; }

        /// <summary>
        /// 新库存
        /// </summary>
        [SugarColumn(ColumnName = "new_stock", ColumnDescription = "新库存", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? NewStock { get; set; }

        /// <summary>
        /// EOL标记
        /// </summary>
        [SugarColumn(ColumnName = "eol_flag", ColumnDescription = "EOL标记", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int EolFlag { get; set; } = 0;

        /// <summary>
        /// 采购区分
        /// </summary>
        [SugarColumn(ColumnName = "purchase_division", ColumnDescription = "采购区分", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PurchaseDivision { get; set; } = 0;

        /// <summary>
        /// 检验区分
        /// </summary>
        [SugarColumn(ColumnName = "inspection_division", ColumnDescription = "检验区分", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int InspectionDivision { get; set; } = 0;

        /// <summary>
        /// 存放位置
        /// </summary>
        [SugarColumn(ColumnName = "storage_position", ColumnDescription = "存放位置", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StoragePosition { get; set; }

        /// <summary>
        /// SOP更新
        /// </summary>
        [SugarColumn(ColumnName = "sop_update", ColumnDescription = "SOP更新", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SopUpdate { get; set; } = 0;

        /// <summary>
        /// 互换
        /// </summary>
        [SugarColumn(ColumnName = "interchangeable", ColumnDescription = "互换", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        [SugarColumn(ColumnName = "division", ColumnDescription = "区分", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Division { get; set; }

        /// <summary>
        /// 指示
        /// </summary>
        [SugarColumn(ColumnName = "instruction", ColumnDescription = "指示", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Instruction { get; set; }

        /// <summary>
        /// 处理
        /// </summary>
        [SugarColumn(ColumnName = "process", ColumnDescription = "处理", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Process { get; set; }

        /// <summary>
        /// bom生效
        /// </summary>
        [SugarColumn(ColumnName = "bom_effective_date", ColumnDescription = "bom生效", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? BomEffectiveDate { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        [SugarColumn(ColumnName = "login_date", ColumnDescription = "登录日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 跟踪记录集合
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktChangeExecutionTrack.ChangeNumber))]
        public List<TaktChangeExecutionTrack>? Tracks { get; set; }
    }
} 
