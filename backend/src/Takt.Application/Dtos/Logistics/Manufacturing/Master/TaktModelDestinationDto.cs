#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktModelDestinationDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 机种仕向数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 机种仕向基础DTO（与TaktModelDestination实体字段严格对应）
    /// </summary>
    public class TaktModelDestinationDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktModelDestinationDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            ModelCode = string.Empty;
            DestinationCode = string.Empty;
        }

        /// <summary>
        /// 机种仕向ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long ModelDestinationId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种编码
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种名称
        /// </summary>
        public string? ModelName { get; set; }

        /// <summary>
        /// 仕向地编码
        /// </summary>
        public string DestinationCode { get; set; } = string.Empty;

        /// <summary>
        /// 仕向地名称
        /// </summary>
        public string? DestinationName { get; set; }

        /// <summary>
        /// 仕向地类型(1=日本 2=美国 3=澳洲 4=德国 5=加拿大 6=国内 7=其他)
        /// </summary>
        public int? DestinationType { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string? ProductSpecification { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }
    }

    /// <summary>
    /// 机种仕向查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktModelDestinationQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktModelDestinationQueryDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            ModelCode = string.Empty;
            DestinationCode = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// 机种编码
        /// </summary>
        public string? ModelCode { get; set; }

        /// <summary>
        /// 机种名称
        /// </summary>
        public string? ModelName { get; set; }

        /// <summary>
        /// 仕向地编码
        /// </summary>
        public string? DestinationCode { get; set; }

        /// <summary>
        /// 仕向地名称
        /// </summary>
        public string? DestinationName { get; set; }

        /// <summary>
        /// 仕向地类型
        /// </summary>
        public int? DestinationType { get; set; }
    }

    /// <summary>
    /// 机种仕向创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktModelDestinationCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktModelDestinationCreateDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            ModelCode = string.Empty;
            DestinationCode = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种编码
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种名称
        /// </summary>
        public string? ModelName { get; set; }

        /// <summary>
        /// 仕向地编码
        /// </summary>
        public string DestinationCode { get; set; } = string.Empty;

        /// <summary>
        /// 仕向地名称
        /// </summary>
        public string? DestinationName { get; set; }

        /// <summary>
        /// 仕向地类型(1=日本 2=美国 3=澳洲 4=德国 5=加拿大 6=国内 7=其他)
        /// </summary>
        public int? DestinationType { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string? ProductSpecification { get; set; }
    }

    /// <summary>
    /// 机种仕向更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktModelDestinationUpdateDto : TaktModelDestinationCreateDto
    {
        /// <summary>
        /// 机种仕向ID
        /// </summary>
        [AdaptMember("Id")]
        public long ModelDestinationId { get; set; }
    }

    /// <summary>
    /// 机种仕向导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktModelDestinationImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktModelDestinationImportDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            ModelCode = string.Empty;
            DestinationCode = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种编码
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种名称
        /// </summary>
        public string? ModelName { get; set; }

        /// <summary>
        /// 仕向地编码
        /// </summary>
        public string DestinationCode { get; set; } = string.Empty;

        /// <summary>
        /// 仕向地名称
        /// </summary>
        public string? DestinationName { get; set; }

        /// <summary>
        /// 仕向地类型(1=日本 2=美国 3=澳洲 4=德国 5=加拿大 6=国内 7=其他)
        /// </summary>
        public int? DestinationType { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string? ProductSpecification { get; set; }
    }

    /// <summary>
    /// 机种仕向导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktModelDestinationExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktModelDestinationExportDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            ModelCode = string.Empty;
            DestinationCode = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种编码
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种名称
        /// </summary>
        public string? ModelName { get; set; }

        /// <summary>
        /// 仕向地编码
        /// </summary>
        public string DestinationCode { get; set; } = string.Empty;

        /// <summary>
        /// 仕向地名称
        /// </summary>
        public string? DestinationName { get; set; }

        /// <summary>
        /// 仕向地类型
        /// </summary>
        public string? DestinationTypeText { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string? ProductSpecification { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }
    }

    /// <summary>
    /// 机种仕向模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktModelDestinationTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktModelDestinationTemplateDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            ModelCode = string.Empty;
            DestinationCode = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种编码
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种名称
        /// </summary>
        public string? ModelName { get; set; }

        /// <summary>
        /// 仕向地编码
        /// </summary>
        public string DestinationCode { get; set; } = string.Empty;

        /// <summary>
        /// 仕向地名称
        /// </summary>
        public string? DestinationName { get; set; }

        /// <summary>
        /// 仕向地类型(1=日本 2=美国 3=澳洲 4=德国 5=加拿大 6=国内 7=其他)
        /// </summary>
        public int? DestinationType { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string? ProductSpecification { get; set; }
    }
}



