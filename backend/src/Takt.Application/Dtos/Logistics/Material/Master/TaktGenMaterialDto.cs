//===================================================================
// 项目名: Takt.Application
// 文件名: TaktGeneralMaterialDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 集团级物料主数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Material.Master
{
    /// <summary>
    /// 集团级物料主数据基础DTO
    /// </summary>
    public class TaktGeneralMaterialDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralMaterialDto()
        {
            GroupCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            MaterialType = string.Empty;
            IndustrySector = string.Empty;
            MaterialGroup = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            WeightUnit = string.Empty;
            VolumeUnit = string.Empty;
            DimensionUnit = string.Empty;
            GrossWeight = 0;
            NetWeight = 0;
            Volume = 0;
            Length = 0;
            Width = 0;
            Height = 0;
            Status = 1;
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long MaterialId { get; set; }

        /// <summary>
        /// 集团代码
        /// </summary>
        public string GroupCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 行业领域
        /// </summary>
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        public string? WeightUnit { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary>
        public string? VolumeUnit { get; set; }

        /// <summary>
        /// 长度/宽度/高度的尺寸单位
        /// </summary>
        public string? DimensionUnit { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public decimal GrossWeight { get; set; } = 0;

        /// <summary>
        /// 净重
        /// </summary>
        public decimal NetWeight { get; set; } = 0;

        /// <summary>
        /// 业务量
        /// </summary>
        public decimal Volume { get; set; } = 0;

        /// <summary>
        /// 长度
        /// </summary>
        public decimal Length { get; set; } = 0;

        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Width { get; set; } = 0;

        /// <summary>
        /// 高度
        /// </summary>
        public decimal Height { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 集团级物料主数据查询DTO
    /// </summary>
    public class TaktGeneralMaterialQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralMaterialQueryDto()
        {
            GroupCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            MaterialType = string.Empty;
            MaterialGroup = string.Empty;
        }

        /// <summary>
        /// 集团代码
        /// </summary>
        public string? GroupCode { get; set; }

        /// <summary>
        /// 物料号
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 状态（0停用 1正常）
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 集团级物料主数据创建DTO
    /// </summary>
    public class TaktGeneralMaterialCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralMaterialCreateDto()
        {
            GroupCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            MaterialType = string.Empty;
            IndustrySector = string.Empty;
            MaterialGroup = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            WeightUnit = string.Empty;
            VolumeUnit = string.Empty;
            DimensionUnit = string.Empty;
            GrossWeight = 0;
            NetWeight = 0;
            Volume = 0;
            Length = 0;
            Width = 0;
            Height = 0;
            Status = 1;
            Remark = string.Empty;
        }

        /// <summary>
        /// 集团代码
        /// </summary>
        public string GroupCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 行业领域
        /// </summary>
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        public string? WeightUnit { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary>
        public string? VolumeUnit { get; set; }

        /// <summary>
        /// 长度/宽度/高度的尺寸单位
        /// </summary>
        public string? DimensionUnit { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public decimal GrossWeight { get; set; } = 0;

        /// <summary>
        /// 净重
        /// </summary>
        public decimal NetWeight { get; set; } = 0;

        /// <summary>
        /// 业务量
        /// </summary>
        public decimal Volume { get; set; } = 0;

        /// <summary>
        /// 长度
        /// </summary>
        public decimal Length { get; set; } = 0;

        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Width { get; set; } = 0;

        /// <summary>
        /// 高度
        /// </summary>
        public decimal Height { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 集团级物料主数据更新DTO
    /// </summary>
    public class TaktGeneralMaterialUpdateDto : TaktGeneralMaterialCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralMaterialUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long MaterialId { get; set; }
    }

    /// <summary>
    /// 集团级物料主数据导入DTO
    /// </summary>
    public class TaktGeneralMaterialImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralMaterialImportDto()
        {
            GroupCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            MaterialType = string.Empty;
            IndustrySector = string.Empty;
            MaterialGroup = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            WeightUnit = string.Empty;
            VolumeUnit = string.Empty;
            DimensionUnit = string.Empty;
            GrossWeight = 0;
            NetWeight = 0;
            Volume = 0;
            Length = 0;
            Width = 0;
            Height = 0;
            Status = 1;
            Remark = string.Empty;
        }

        /// <summary>
        /// 集团代码
        /// </summary>
        public string GroupCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 行业领域
        /// </summary>
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        public string? WeightUnit { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary>
        public string? VolumeUnit { get; set; }

        /// <summary>
        /// 长度/宽度/高度的尺寸单位
        /// </summary>
        public string? DimensionUnit { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public decimal GrossWeight { get; set; } = 0;

        /// <summary>
        /// 净重
        /// </summary>
        public decimal NetWeight { get; set; } = 0;

        /// <summary>
        /// 业务量
        /// </summary>
        public decimal Volume { get; set; } = 0;

        /// <summary>
        /// 长度
        /// </summary>
        public decimal Length { get; set; } = 0;

        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Width { get; set; } = 0;

        /// <summary>
        /// 高度
        /// </summary>
        public decimal Height { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 集团级物料主数据导出DTO
    /// </summary>
    public class TaktGeneralMaterialExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralMaterialExportDto()
        {
            GroupCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            MaterialType = string.Empty;
            MaterialGroup = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            Status = 1;
        }

        /// <summary>
        /// 集团代码
        /// </summary>
        public string GroupCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 1;
    }

    /// <summary>
    /// 集团级物料主数据模板DTO
    /// </summary>
    public class TaktGeneralMaterialTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralMaterialTemplateDto()
        {
            GroupCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            MaterialType = string.Empty;
            IndustrySector = string.Empty;
            MaterialGroup = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            WeightUnit = string.Empty;
            VolumeUnit = string.Empty;
            DimensionUnit = string.Empty;
            GrossWeight = 0;
            NetWeight = 0;
            Volume = 0;
            Length = 0;
            Width = 0;
            Height = 0;
            Status = 1;
            Remark = string.Empty;
        }

        /// <summary>
        /// 集团代码
        /// </summary>
        public string GroupCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 行业领域
        /// </summary>
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        public string? WeightUnit { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary>
        public string? VolumeUnit { get; set; }

        /// <summary>
        /// 长度/宽度/高度的尺寸单位
        /// </summary>
        public string? DimensionUnit { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        public decimal GrossWeight { get; set; } = 0;

        /// <summary>
        /// 净重
        /// </summary>
        public decimal NetWeight { get; set; } = 0;

        /// <summary>
        /// 业务量
        /// </summary>
        public decimal Volume { get; set; } = 0;

        /// <summary>
        /// 长度
        /// </summary>
        public decimal Length { get; set; } = 0;

        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Width { get; set; } = 0;

        /// <summary>
        /// 高度
        /// </summary>
        public decimal Height { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 集团级物料主数据状态更新DTO
    /// </summary>
    public class TaktGeneralMaterialStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralMaterialStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long MaterialId { get; set; }

        /// <summary>
        /// 状态（0停用 1正常）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}


