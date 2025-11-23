#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Logistics.Quality.Master;

namespace Takt.Domain.Entities.Logistics.Quality.Inspection
{
    /// <summary>
    /// 来料检验明细（IQC）实体
    /// </summary>
    [SugarTable("Takt_logistics_quality_inspection_iqc_detail", "来料检验明细(IQC)表")]
    [SugarIndex("ix_inspection_no", nameof(InspectionNo), OrderByType.Asc, false)]
    public class TaktIqcInspectionDetail : TaktBaseEntity
    {
        /// <summary>
        /// 检验单号（外键）
        /// </summary>
        [SugarColumn(ColumnName = "inspection_no", ColumnDescription = "检验单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string InspectionNo { get; set; } = string.Empty;

        /// <summary>
        /// 检验项目
        /// </summary>
        [SugarColumn(ColumnName = "item", ColumnDescription = "检验项目", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Item { get; set; }

        /// <summary>
        /// 检验标准
        /// </summary>
        [SugarColumn(ColumnName = "standard", ColumnDescription = "检验标准", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Standard { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        [SugarColumn(ColumnName = "result", ColumnDescription = "检验结果", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Result { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        [SugarColumn(ColumnName = "defect_desc", ColumnDescription = "不良描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectDesc { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        [SugarColumn(ColumnName = "action", ColumnDescription = "处理措施", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Action { get; set; }

        /// <summary>
        /// 导航属性：关联主表
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(InspectionNo), nameof(TaktIqcInspection.InspectionNo))]
        public TaktIqcInspection? IqcInspection { get; set; }



    }
} 
