#nullable enable

using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom.Change
{
    /// <summary>
    /// 设计变更计执行明细DTO
    /// </summary>
    public class TaktChangeExecutionDetailDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public string ChangeExecutionDetailId { get; set; } = string.Empty;

        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 完成品
        /// </summary>
        public string? FinishedProduct { get; set; }

        /// <summary>
        /// 上阶
        /// </summary>
        public string? ParentProduct { get; set; }

        /// <summary>
        /// 旧品
        /// </summary>
        public string? OldProduct { get; set; }

        /// <summary>
        /// 旧描述
        /// </summary>
        public string? OldDescription { get; set; }

        /// <summary>
        /// 旧用量
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 旧位置
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 旧库存
        /// </summary>
        public decimal? OldStock { get; set; }

        /// <summary>
        /// 新品
        /// </summary>
        public string? NewProduct { get; set; }

        /// <summary>
        /// 新描述
        /// </summary>
        public string? NewDescription { get; set; }

        /// <summary>
        /// 新用量
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 新位置
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// 新库存
        /// </summary>
        public decimal? NewStock { get; set; }

        /// <summary>
        /// EOL标记
        /// </summary>
        public int EolFlag { get; set; } = 0;

        /// <summary>
        /// 采购区分
        /// </summary>
        public int PurchaseDivision { get; set; } = 0;

        /// <summary>
        /// 检验区分
        /// </summary>
        public int InspectionDivision { get; set; } = 0;

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? StoragePosition { get; set; }

        /// <summary>
        /// SOP更新
        /// </summary>
        public int SopUpdate { get; set; } = 0;

        /// <summary>
        /// 互换
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }

        /// <summary>
        /// 指示
        /// </summary>
        public string? Instruction { get; set; }

        /// <summary>
        /// 处理
        /// </summary>
        public string? Process { get; set; }

        /// <summary>
        /// bom生效
        /// </summary>
        public DateTime? BomEffectiveDate { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 跟踪记录集合
        /// </summary>
        public List<TaktChangeExecutionTrackDto>? Tracks { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// 设计变更计执行明细查询DTO
    /// </summary>
    public class TaktChangeExecutionDetailQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string? ChangeNumber { get; set; }

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 完成品
        /// </summary>
        public string? FinishedProduct { get; set; }

        /// <summary>
        /// 上阶
        /// </summary>
        public string? ParentProduct { get; set; }

        /// <summary>
        /// 旧品
        /// </summary>
        public string? OldProduct { get; set; }

        /// <summary>
        /// 新品
        /// </summary>
        public string? NewProduct { get; set; }

        /// <summary>
        /// EOL标记
        /// </summary>
        public int? EolFlag { get; set; }

        /// <summary>
        /// 采购区分
        /// </summary>
        public int? PurchaseDivision { get; set; }

        /// <summary>
        /// 检验区分
        /// </summary>
        public int? InspectionDivision { get; set; }

        /// <summary>
        /// SOP更新
        /// </summary>
        public int? SopUpdate { get; set; }

        /// <summary>
        /// bom生效开始日期
        /// </summary>
        public DateTime? BomEffectiveDateStart { get; set; }

        /// <summary>
        /// bom生效结束日期
        /// </summary>
        public DateTime? BomEffectiveDateEnd { get; set; }

        /// <summary>
        /// 登录开始日期
        /// </summary>
        public DateTime? LoginDateStart { get; set; }

        /// <summary>
        /// 登录结束日期
        /// </summary>
        public DateTime? LoginDateEnd { get; set; }
    }

    /// <summary>
    /// 设计变更计执行明细创建DTO
    /// </summary>
    public class TaktChangeExecutionDetailCreateDto
    {
        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 完成品
        /// </summary>
        public string? FinishedProduct { get; set; }

        /// <summary>
        /// 上阶
        /// </summary>
        public string? ParentProduct { get; set; }

        /// <summary>
        /// 旧品
        /// </summary>
        public string? OldProduct { get; set; }

        /// <summary>
        /// 旧描述
        /// </summary>
        public string? OldDescription { get; set; }

        /// <summary>
        /// 旧用量
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 旧位置
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 旧库存
        /// </summary>
        public decimal? OldStock { get; set; }

        /// <summary>
        /// 新品
        /// </summary>
        public string? NewProduct { get; set; }

        /// <summary>
        /// 新描述
        /// </summary>
        public string? NewDescription { get; set; }

        /// <summary>
        /// 新用量
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 新位置
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// 新库存
        /// </summary>
        public decimal? NewStock { get; set; }

        /// <summary>
        /// EOL标记
        /// </summary>
        public int EolFlag { get; set; } = 0;

        /// <summary>
        /// 采购区分
        /// </summary>
        public int PurchaseDivision { get; set; } = 0;

        /// <summary>
        /// 检验区分
        /// </summary>
        public int InspectionDivision { get; set; } = 0;

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? StoragePosition { get; set; }

        /// <summary>
        /// SOP更新
        /// </summary>
        public int SopUpdate { get; set; } = 0;

        /// <summary>
        /// 互换
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }

        /// <summary>
        /// 指示
        /// </summary>
        public string? Instruction { get; set; }

        /// <summary>
        /// 处理
        /// </summary>
        public string? Process { get; set; }

        /// <summary>
        /// bom生效
        /// </summary>
        public DateTime? BomEffectiveDate { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }
    }

    /// <summary>
    /// 设计变更计执行明细更新DTO
    /// </summary>
    public class TaktChangeExecutionDetailUpdateDto : TaktChangeExecutionDetailCreateDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public string ChangeExecutionDetailId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 设计变更计执行明细导入DTO
    /// </summary>
    public class TaktChangeExecutionDetailImportDto
    {
        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 完成品
        /// </summary>
        public string? FinishedProduct { get; set; }

        /// <summary>
        /// 上阶
        /// </summary>
        public string? ParentProduct { get; set; }

        /// <summary>
        /// 旧品
        /// </summary>
        public string? OldProduct { get; set; }

        /// <summary>
        /// 旧描述
        /// </summary>
        public string? OldDescription { get; set; }

        /// <summary>
        /// 旧用量
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 旧位置
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 旧库存
        /// </summary>
        public decimal? OldStock { get; set; }

        /// <summary>
        /// 新品
        /// </summary>
        public string? NewProduct { get; set; }

        /// <summary>
        /// 新描述
        /// </summary>
        public string? NewDescription { get; set; }

        /// <summary>
        /// 新用量
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 新位置
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// 新库存
        /// </summary>
        public decimal? NewStock { get; set; }

        /// <summary>
        /// EOL标记
        /// </summary>
        public int EolFlag { get; set; } = 0;

        /// <summary>
        /// 采购区分
        /// </summary>
        public int PurchaseDivision { get; set; } = 0;

        /// <summary>
        /// 检验区分
        /// </summary>
        public int InspectionDivision { get; set; } = 0;

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? StoragePosition { get; set; }

        /// <summary>
        /// SOP更新
        /// </summary>
        public int SopUpdate { get; set; } = 0;

        /// <summary>
        /// 互换
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }

        /// <summary>
        /// 指示
        /// </summary>
        public string? Instruction { get; set; }

        /// <summary>
        /// 处理
        /// </summary>
        public string? Process { get; set; }

        /// <summary>
        /// bom生效
        /// </summary>
        public DateTime? BomEffectiveDate { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }
    }

    /// <summary>
    /// 设计变更计执行明细导出DTO
    /// </summary>
    public class TaktChangeExecutionDetailExportDto
    {
        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 完成品
        /// </summary>
        public string? FinishedProduct { get; set; }

        /// <summary>
        /// 上阶
        /// </summary>
        public string? ParentProduct { get; set; }

        /// <summary>
        /// 旧品
        /// </summary>
        public string? OldProduct { get; set; }

        /// <summary>
        /// 旧描述
        /// </summary>
        public string? OldDescription { get; set; }

        /// <summary>
        /// 旧用量
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 旧位置
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 旧库存
        /// </summary>
        public decimal? OldStock { get; set; }

        /// <summary>
        /// 新品
        /// </summary>
        public string? NewProduct { get; set; }

        /// <summary>
        /// 新描述
        /// </summary>
        public string? NewDescription { get; set; }

        /// <summary>
        /// 新用量
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 新位置
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// 新库存
        /// </summary>
        public decimal? NewStock { get; set; }

        /// <summary>
        /// EOL标记
        /// </summary>
        public int EolFlag { get; set; } = 0;

        /// <summary>
        /// 采购区分
        /// </summary>
        public int PurchaseDivision { get; set; } = 0;

        /// <summary>
        /// 检验区分
        /// </summary>
        public int InspectionDivision { get; set; } = 0;

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? StoragePosition { get; set; }

        /// <summary>
        /// SOP更新
        /// </summary>
        public int SopUpdate { get; set; } = 0;

        /// <summary>
        /// 互换
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }

        /// <summary>
        /// 指示
        /// </summary>
        public string? Instruction { get; set; }

        /// <summary>
        /// 处理
        /// </summary>
        public string? Process { get; set; }

        /// <summary>
        /// bom生效
        /// </summary>
        public DateTime? BomEffectiveDate { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdatedBy { get; set; }
    }

    /// <summary>
    /// 设计变更计执行明细模板DTO
    /// </summary>
    public class TaktChangeExecutionDetailTemplateDto
    {
        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 完成品
        /// </summary>
        public string? FinishedProduct { get; set; }

        /// <summary>
        /// 上阶
        /// </summary>
        public string? ParentProduct { get; set; }

        /// <summary>
        /// 旧品
        /// </summary>
        public string? OldProduct { get; set; }

        /// <summary>
        /// 旧描述
        /// </summary>
        public string? OldDescription { get; set; }

        /// <summary>
        /// 旧用量
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 旧位置
        /// </summary>
        public string? OldPosition { get; set; }

        /// <summary>
        /// 旧库存
        /// </summary>
        public decimal? OldStock { get; set; }

        /// <summary>
        /// 新品
        /// </summary>
        public string? NewProduct { get; set; }

        /// <summary>
        /// 新描述
        /// </summary>
        public string? NewDescription { get; set; }

        /// <summary>
        /// 新用量
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 新位置
        /// </summary>
        public string? NewPosition { get; set; }

        /// <summary>
        /// 新库存
        /// </summary>
        public decimal? NewStock { get; set; }

        /// <summary>
        /// EOL标记
        /// </summary>
        public int EolFlag { get; set; } = 0;

        /// <summary>
        /// 采购区分
        /// </summary>
        public int PurchaseDivision { get; set; } = 0;

        /// <summary>
        /// 检验区分
        /// </summary>
        public int InspectionDivision { get; set; } = 0;

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? StoragePosition { get; set; }

        /// <summary>
        /// SOP更新
        /// </summary>
        public int SopUpdate { get; set; } = 0;

        /// <summary>
        /// 互换
        /// </summary>
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        public string? Division { get; set; }

        /// <summary>
        /// 指示
        /// </summary>
        public string? Instruction { get; set; }

        /// <summary>
        /// 处理
        /// </summary>
        public string? Process { get; set; }

        /// <summary>
        /// bom生效
        /// </summary>
        public DateTime? BomEffectiveDate { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }
    }

    /// <summary>
    /// 设计变更计执行明细状态DTO
    /// </summary>
    public class TaktChangeExecutionDetailStatusDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public string ChangeExecutionDetailId { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}


