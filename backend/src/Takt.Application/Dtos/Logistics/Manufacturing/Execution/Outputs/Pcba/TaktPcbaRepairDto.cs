//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPcbaRepairDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: PCBA维修主表数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Execution.Outputs.Pcba
{
    /// <summary>
    /// PCBA维修主表基础DTO
    /// </summary>
    public class TaktPcbaRepairDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            RepairType = string.Empty;
            RepairCategory = string.Empty;
            RepairPerson = string.Empty;
            PcbaRepairDetails = new List<TaktPcbaRepairDetailDto>();
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaRepairId { get; set; }

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
        /// 维修类型
        /// </summary>
        public string RepairType { get; set; } = string.Empty;

        /// <summary>
        /// 维修类别
        /// </summary>
        public string RepairCategory { get; set; } = string.Empty;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string RepairPerson { get; set; } = string.Empty;

        /// <summary>
        /// 维修开始时间
        /// </summary>
        public DateTime? RepairStartTime { get; set; }

        /// <summary>
        /// 维修结束时间
        /// </summary>
        public DateTime? RepairEndTime { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修成功数量
        /// </summary>
        public decimal SuccessQty { get; set; } = 0;

        /// <summary>
        /// 维修失败数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 维修成功率
        /// </summary>
        public decimal SuccessRate { get; set; } = 0;

        /// <summary>
        /// 维修成本
        /// </summary>
        public decimal RepairCost { get; set; } = 0;

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
        /// PCBA维修明细列表
        /// </summary>
        public List<TaktPcbaRepairDetailDto> PcbaRepairDetails { get; set; }
    }

    /// <summary>
    /// PCBA维修主表查询DTO
    /// </summary>
    public class TaktPcbaRepairQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairQueryDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            RepairType = string.Empty;
            RepairCategory = string.Empty;
            RepairPerson = string.Empty;
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
        /// 维修类型
        /// </summary>
        public string? RepairType { get; set; }

        /// <summary>
        /// 维修类别
        /// </summary>
        public string? RepairCategory { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }
    }

    /// <summary>
    /// PCBA维修主表创建DTO
    /// </summary>
    public class TaktPcbaRepairCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairCreateDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            RepairType = string.Empty;
            RepairCategory = string.Empty;
            RepairPerson = string.Empty;
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
        /// 维修类型
        /// </summary>
        public string RepairType { get; set; } = string.Empty;

        /// <summary>
        /// 维修类别
        /// </summary>
        public string RepairCategory { get; set; } = string.Empty;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string RepairPerson { get; set; } = string.Empty;

        /// <summary>
        /// 维修开始时间
        /// </summary>
        public DateTime? RepairStartTime { get; set; }

        /// <summary>
        /// 维修结束时间
        /// </summary>
        public DateTime? RepairEndTime { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修成功数量
        /// </summary>
        public decimal SuccessQty { get; set; } = 0;

        /// <summary>
        /// 维修失败数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 维修成功率
        /// </summary>
        public decimal SuccessRate { get; set; } = 0;

        /// <summary>
        /// 维修成本
        /// </summary>
        public decimal RepairCost { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA维修主表更新DTO
    /// </summary>
    public class TaktPcbaRepairUpdateDto : TaktPcbaRepairCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaRepairId { get; set; }
    }

    /// <summary>
    /// PCBA维修主表导入DTO
    /// </summary>
    public class TaktPcbaRepairImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairImportDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            RepairType = string.Empty;
            RepairCategory = string.Empty;
            RepairPerson = string.Empty;
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
        /// 维修类型
        /// </summary>
        public string RepairType { get; set; } = string.Empty;

        /// <summary>
        /// 维修类别
        /// </summary>
        public string RepairCategory { get; set; } = string.Empty;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string RepairPerson { get; set; } = string.Empty;

        /// <summary>
        /// 维修开始时间
        /// </summary>
        public DateTime? RepairStartTime { get; set; }

        /// <summary>
        /// 维修结束时间
        /// </summary>
        public DateTime? RepairEndTime { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修成功数量
        /// </summary>
        public decimal SuccessQty { get; set; } = 0;

        /// <summary>
        /// 维修失败数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 维修成功率
        /// </summary>
        public decimal SuccessRate { get; set; } = 0;

        /// <summary>
        /// 维修成本
        /// </summary>
        public decimal RepairCost { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA维修主表导出DTO
    /// </summary>
    public class TaktPcbaRepairExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairExportDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            RepairType = string.Empty;
            RepairCategory = string.Empty;
            RepairPerson = string.Empty;
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
        /// 维修类型
        /// </summary>
        public string RepairType { get; set; } = string.Empty;

        /// <summary>
        /// 维修类别
        /// </summary>
        public string RepairCategory { get; set; } = string.Empty;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string RepairPerson { get; set; } = string.Empty;

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修成功数量
        /// </summary>
        public decimal SuccessQty { get; set; } = 0;

        /// <summary>
        /// 维修失败数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 维修成功率
        /// </summary>
        public decimal SuccessRate { get; set; } = 0;

        /// <summary>
        /// 维修成本
        /// </summary>
        public decimal RepairCost { get; set; } = 0;
    }

    /// <summary>
    /// PCBA维修主表模板DTO
    /// </summary>
    public class TaktPcbaRepairTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairTemplateDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            RepairType = string.Empty;
            RepairCategory = string.Empty;
            RepairPerson = string.Empty;
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
        /// 维修类型
        /// </summary>
        public string RepairType { get; set; } = string.Empty;

        /// <summary>
        /// 维修类别
        /// </summary>
        public string RepairCategory { get; set; } = string.Empty;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string RepairPerson { get; set; } = string.Empty;

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA维修主表状态更新DTO
    /// </summary>
    public class TaktPcbaRepairStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaRepairId { get; set; }

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


