//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktDictTypeDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 字典类型数据传输对象
//===================================================================
namespace Takt.Application.Dtos.Routine.DataDictionary
{
    /// <summary>
    /// 字典类型基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktDictTypeDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDictTypeDto()
        {
            DictName = string.Empty;
            DictType = string.Empty;
            SqlScript = string.Empty;
            Remark = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            DeleteBy = string.Empty;
            DictDataList = new List<TaktDictDataDto>();
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long DictTypeId { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典数据源（0系统 1SQL）
        /// </summary>
        public int? DictSource { get; set; }

        /// <summary>
        /// 字典内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 字典状态（0正常 1停用）
        /// </summary>
        public int DictStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 字典数据列表
        /// </summary>
        public List<TaktDictDataDto>? DictDataList { get; set; }
    }

    /// <summary>
    /// 字典类型查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktDictTypeQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string? DictName { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string? DictType { get; set; }

        /// <summary>
        /// 字典数据源（0系统 1SQL）
        /// </summary>
        public int? DictSource { get; set; }

        /// <summary>
        /// 字典状态（0正常 1停用）
        /// </summary>
        public int? DictStatus { get; set; }

        /// <summary>
        /// 字典内置（0否 1是）
        /// </summary>
        public int? IsBuiltin { get; set; }
    }

    /// <summary>
    /// 字典类型创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktDictTypeCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDictTypeCreateDto()
        {
            DictName = string.Empty;
            DictType = string.Empty;
            SqlScript = string.Empty;
            Remark = string.Empty;
            DictDataList = new List<TaktDictDataCreateDto>();
        }

        /// <summary>
        /// 字典名称
        /// </summary>

        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>

        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典数据源（0系统 1SQL）
        /// </summary>

        public int DictSource { get; set; }

        /// <summary>
        /// 字典内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>

        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>

        public int DictStatus { get; set; }


        /// <summary>
        /// 备注
        /// </summary>

        public string? Remark { get; set; }

        /// <summary>
        /// 字典数据列表
        /// </summary>
        public List<TaktDictDataCreateDto>? DictDataList { get; set; }
    }

    /// <summary>
    /// 字典类型更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktDictTypeUpdateDto : TaktDictTypeCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long DictTypeId { get; set; }
    }

    /// <summary>
    /// 字典类型导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktDictTypeImportDto
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典数据源（0系统 1SQL）
        /// </summary>
        public int DictSource { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 字典状态（0正常 1停用）
        /// </summary>
        public int DictStatus { get; set; }

        /// <summary>
        /// 字典数据列表
        /// </summary>
        public List<TaktDictDataImportDto>? DictDataList { get; set; }
    }

    /// <summary>
    /// 字典类型导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktDictTypeExportDto
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典数据源（0系统 1SQL）
        /// </summary>
        public int DictSource { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 字典状态（0正常 1停用）
        /// </summary>
        public int DictStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 字典类型模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktDictTypeTemplateDto
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典数据源（0系统 1SQL）
        /// </summary>
        public int DictSource { get; set; }

        /// <summary>
        /// 字典内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; }

        /// <summary>
        /// SQL脚本
        /// </summary>
        public string? SqlScript { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 字典状态（0正常 1停用）
        /// </summary>
        public int DictStatus { get; set; }



        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 字典类型状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktDictTypeStatusDto
    {
        /// <summary>
        /// ID
        /// </summary>

        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long DictTypeId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>

        public int DictStatus { get; set; }
    }
}


