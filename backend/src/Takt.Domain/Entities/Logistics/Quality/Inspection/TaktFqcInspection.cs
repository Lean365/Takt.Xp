#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Logistics.Quality.Master;

namespace Takt.Domain.Entities.Logistics.Quality.Inspection
{
    /// <summary>
    /// 最终检验（FQC）实体
    /// </summary>
    [SugarTable("Takt_logistics_quality_inspection_fqc", "最终检验(FQC)表")]
    [SugarIndex("ix_inspection_no", nameof(InspectionNo), OrderByType.Asc, false)]
    public class TaktFqcInspection : TaktBaseEntity
    {
        /// <summary>
        /// 检验单号
        /// </summary>
        [SugarColumn(ColumnName = "inspection_no", ColumnDescription = "检验单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string InspectionNo { get; set; } = string.Empty;

        /// <summary>
        /// 检验日期
        /// </summary>
        [SugarColumn(ColumnName = "inspection_date", ColumnDescription = "检验日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? InspectionDate { get; set; }

        /// <summary>
        /// 检验员
        /// </summary>
        [SugarColumn(ColumnName = "inspector", ColumnDescription = "检验员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Inspector { get; set; }

        /// <summary>
        /// 对象ID（物料/产品/批次号等）
        /// </summary>
        [SugarColumn(ColumnName = "object_id", ColumnDescription = "对象ID", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ObjectId { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        [SugarColumn(ColumnName = "object_name", ColumnDescription = "对象名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ObjectName { get; set; }

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
        /// 导航属性：检验明细列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktFqcInspectionDetail.InspectionNo), nameof(InspectionNo))]
        public List<TaktFqcInspectionDetail>? Details { get; set; }



    }
} 
