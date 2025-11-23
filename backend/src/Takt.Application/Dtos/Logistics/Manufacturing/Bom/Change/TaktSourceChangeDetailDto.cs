#nullable enable

using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom.Change
{
    /// <summary>
    /// 源设变明细DTO
    /// </summary>
    public class TaktSourceChangeDetailDto : TaktPagedQuery
    {
        /// <summary>
        /// 源设变明细ID
        /// </summary>
        [AdaptMember("Id")]
        public string SourceChangeDetailId { get; set; } = string.Empty;

        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 成品料号
        /// </summary>
        public string? FinishedMaterial { get; set; }

        /// <summary>
        /// 上阶料号
        /// </summary>
        public string? ParentMaterial { get; set; }

        /// <summary>
        /// 旧料号
        /// </summary>
        public string? OldMaterialCode { get; set; }

        /// <summary>
        /// 料名（旧）
        /// </summary>
        public string? OldMaterialName { get; set; }

        /// <summary>
        /// 数量（旧）
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 位置（旧）
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 新料号
        /// </summary>
        public string? NewMaterialCode { get; set; }

        /// <summary>
        /// 料名（新）
        /// </summary>
        public string? NewMaterialName { get; set; }

        /// <summary>
        /// 数量（新）
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 位置（新）
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// BOM番号
        /// </summary>
        public string? BomNumber { get; set; }

        /// <summary>
        /// 互换性
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }
    }

    /// <summary>
    /// 源设变明细查询DTO
    /// </summary>
    public class TaktSourceChangeDetailQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string? ChangeNumber { get; set; }

        /// <summary>
        /// 成品料号
        /// </summary>
        public string? FinishedMaterial { get; set; }

        /// <summary>
        /// 上阶料号
        /// </summary>
        public string? ParentMaterial { get; set; }

        /// <summary>
        /// 旧料号
        /// </summary>
        public string? OldMaterialCode { get; set; }

        /// <summary>
        /// 新料号
        /// </summary>
        public string? NewMaterialCode { get; set; }

        /// <summary>
        /// BOM番号
        /// </summary>
        public string? BomNumber { get; set; }
    }

    /// <summary>
    /// 源设变明细创建DTO
    /// </summary>
    public class TaktSourceChangeDetailCreateDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 成品料号
        /// </summary>
        public string? FinishedMaterial { get; set; }

        /// <summary>
        /// 上阶料号
        /// </summary>
        public string? ParentMaterial { get; set; }

        /// <summary>
        /// 旧料号
        /// </summary>
        public string? OldMaterialCode { get; set; }

        /// <summary>
        /// 料名（旧）
        /// </summary>
        public string? OldMaterialName { get; set; }

        /// <summary>
        /// 数量（旧）
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 位置（旧）
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 新料号
        /// </summary>
        public string? NewMaterialCode { get; set; }

        /// <summary>
        /// 料名（新）
        /// </summary>
        public string? NewMaterialName { get; set; }

        /// <summary>
        /// 数量（新）
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 位置（新）
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// BOM番号
        /// </summary>
        public string? BomNumber { get; set; }

        /// <summary>
        /// 互换性
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }
    }

    /// <summary>
    /// 源设变明细更新DTO
    /// </summary>
    public class TaktSourceChangeDetailUpdateDto : TaktSourceChangeDetailCreateDto
    {
        /// <summary>
        /// 源设变明细ID
        /// </summary>
        [AdaptMember("Id")]
        public string SourceChangeDetailId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 源设变明细导入DTO
    /// </summary>
    public class TaktSourceChangeDetailImportDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 成品料号
        /// </summary>
        public string? FinishedMaterial { get; set; }

        /// <summary>
        /// 上阶料号
        /// </summary>
        public string? ParentMaterial { get; set; }

        /// <summary>
        /// 旧料号
        /// </summary>
        public string? OldMaterialCode { get; set; }

        /// <summary>
        /// 料名（旧）
        /// </summary>
        public string? OldMaterialName { get; set; }

        /// <summary>
        /// 数量（旧）
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 位置（旧）
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 新料号
        /// </summary>
        public string? NewMaterialCode { get; set; }

        /// <summary>
        /// 料名（新）
        /// </summary>
        public string? NewMaterialName { get; set; }

        /// <summary>
        /// 数量（新）
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 位置（新）
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// BOM番号
        /// </summary>
        public string? BomNumber { get; set; }

        /// <summary>
        /// 互换性
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }
    }

    /// <summary>
    /// 源设变明细导出DTO
    /// </summary>
    public class TaktSourceChangeDetailExportDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 成品料号
        /// </summary>
        public string? FinishedMaterial { get; set; }

        /// <summary>
        /// 上阶料号
        /// </summary>
        public string? ParentMaterial { get; set; }

        /// <summary>
        /// 旧料号
        /// </summary>
        public string? OldMaterialCode { get; set; }

        /// <summary>
        /// 料名（旧）
        /// </summary>
        public string? OldMaterialName { get; set; }

        /// <summary>
        /// 数量（旧）
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 位置（旧）
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 新料号
        /// </summary>
        public string? NewMaterialCode { get; set; }

        /// <summary>
        /// 料名（新）
        /// </summary>
        public string? NewMaterialName { get; set; }

        /// <summary>
        /// 数量（新）
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 位置（新）
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// BOM番号
        /// </summary>
        public string? BomNumber { get; set; }

        /// <summary>
        /// 互换性
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }
    }

    /// <summary>
    /// 源设变明细模板DTO
    /// </summary>
    public class TaktSourceChangeDetailTemplateDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 成品料号
        /// </summary>
        public string? FinishedMaterial { get; set; }

        /// <summary>
        /// 上阶料号
        /// </summary>
        public string? ParentMaterial { get; set; }

        /// <summary>
        /// 旧料号
        /// </summary>
        public string? OldMaterialCode { get; set; }

        /// <summary>
        /// 料名（旧）
        /// </summary>
        public string? OldMaterialName { get; set; }

        /// <summary>
        /// 数量（旧）
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 位置（旧）
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 新料号
        /// </summary>
        public string? NewMaterialCode { get; set; }

        /// <summary>
        /// 料名（新）
        /// </summary>
        public string? NewMaterialName { get; set; }

        /// <summary>
        /// 数量（新）
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 位置（新）
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// BOM番号
        /// </summary>
        public string? BomNumber { get; set; }

        /// <summary>
        /// 互换性
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }
    }

    /// <summary>
    /// 源设变明细状态DTO
    /// </summary>
    public class TaktSourceChangeDetailStatusDto
    {
        /// <summary>
        /// 源设变明细ID
        /// </summary>
        [AdaptMember("Id")]
        public string SourceChangeDetailId { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}


