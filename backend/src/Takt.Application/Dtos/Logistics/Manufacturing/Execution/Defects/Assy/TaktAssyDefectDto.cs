//===================================================================
// 项目名: Takt.Application
// 文件名: TaktAssyDefectDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 装配不良主表数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Execution.Defects.Assy
{
    /// <summary>
    /// 装配不良主表基础DTO
    /// </summary>
    public class TaktAssyDefectDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            DefectType = string.Empty;
            DefectCategory = string.Empty;
            DefectLevel = string.Empty;
            AssyDefectDetails = new List<TaktAssyDefectDetailDto>();
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long AssyDefectId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 不良类型
        /// </summary>
        public string DefectType { get; set; } = string.Empty;

        /// <summary>
        /// 不良类别
        /// </summary>
        public string DefectCategory { get; set; } = string.Empty;

        /// <summary>
        /// 不良等级
        /// </summary>
        public string DefectLevel { get; set; } = string.Empty;

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;

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

        /// <summary>
        /// 装配不良明细列表
        /// </summary>
        public List<TaktAssyDefectDetailDto> AssyDefectDetails { get; set; }
    }

    /// <summary>
    /// 装配不良主表查询DTO
    /// </summary>
    public class TaktAssyDefectQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectQueryDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            DefectType = string.Empty;
            DefectCategory = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 生产日期开始
        /// </summary>
        public DateTime? ProdDateStart { get; set; }

        /// <summary>
        /// 生产日期结束
        /// </summary>
        public DateTime? ProdDateEnd { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string? ProdLine { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        public int? ShiftNo { get; set; }

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string? ProdOrderCode { get; set; }

        /// <summary>
        /// 机种
        /// </summary>
        public string? ModelCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// 不良类型
        /// </summary>
        public string? DefectType { get; set; }

        /// <summary>
        /// 不良类别
        /// </summary>
        public string? DefectCategory { get; set; }
    }

    /// <summary>
    /// 装配不良主表创建DTO
    /// </summary>
    public class TaktAssyDefectCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectCreateDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            DefectType = string.Empty;
            DefectCategory = string.Empty;
            DefectLevel = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 不良类型
        /// </summary>
        public string DefectType { get; set; } = string.Empty;

        /// <summary>
        /// 不良类别
        /// </summary>
        public string DefectCategory { get; set; } = string.Empty;

        /// <summary>
        /// 不良等级
        /// </summary>
        public string DefectLevel { get; set; } = string.Empty;

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 装配不良主表更新DTO
    /// </summary>
    public class TaktAssyDefectUpdateDto : TaktAssyDefectCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long AssyDefectId { get; set; }
    }

    /// <summary>
    /// 装配不良主表导入DTO
    /// </summary>
    public class TaktAssyDefectImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectImportDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            DefectType = string.Empty;
            DefectCategory = string.Empty;
            DefectLevel = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 不良类型
        /// </summary>
        public string DefectType { get; set; } = string.Empty;

        /// <summary>
        /// 不良类别
        /// </summary>
        public string DefectCategory { get; set; } = string.Empty;

        /// <summary>
        /// 不良等级
        /// </summary>
        public string DefectLevel { get; set; } = string.Empty;

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 装配不良主表导出DTO
    /// </summary>
    public class TaktAssyDefectExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectExportDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            DefectType = string.Empty;
            DefectCategory = string.Empty;
            DefectLevel = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 不良类型
        /// </summary>
        public string DefectType { get; set; } = string.Empty;

        /// <summary>
        /// 不良类别
        /// </summary>
        public string DefectCategory { get; set; } = string.Empty;

        /// <summary>
        /// 不良等级
        /// </summary>
        public string DefectLevel { get; set; } = string.Empty;

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;
    }

    /// <summary>
    /// 装配不良主表模板DTO
    /// </summary>
    public class TaktAssyDefectTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectTemplateDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            DefectType = string.Empty;
            DefectCategory = string.Empty;
            DefectLevel = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 不良类型
        /// </summary>
        public string DefectType { get; set; } = string.Empty;

        /// <summary>
        /// 不良类别
        /// </summary>
        public string DefectCategory { get; set; } = string.Empty;

        /// <summary>
        /// 不良等级
        /// </summary>
        public string DefectLevel { get; set; } = string.Empty;

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 装配不良主表状态更新DTO
    /// </summary>
    public class TaktAssyDefectStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long AssyDefectId { get; set; }

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


