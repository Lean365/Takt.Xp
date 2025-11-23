//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktDictDataDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.DataDictionary
{
    /// <summary>
    /// 字典数据DTO
    /// </summary>
    public class TaktDictDataDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDictDataDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
            ExtLabel = string.Empty;
            ExtValue = string.Empty;
            TransKey = string.Empty;
            Remark = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            DeleteBy = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long DictDataId { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典标签
        /// </summary>
        public string DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 扩展标签
        /// </summary>
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }


        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

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
    }

    /// <summary>
    /// 字典数据查询DTO
    /// </summary>
    public class TaktDictDataQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDictDataQueryDto()
        {
            DictType = string.Empty;
            DictLabel = string.Empty;
            DictValue = string.Empty;
        }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string? DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        public string? DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        public string? DictValue { get; set; }

    }

    /// <summary>
    /// 字典数据创建DTO
    /// </summary>
    public class TaktDictDataCreateDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典标签
        /// </summary>
        public string DictLabel { get; set; } = string.Empty;

        /// <summary>
        /// 字典键值
        /// </summary>
        public string DictValue { get; set; } = string.Empty;

        /// <summary>
        /// 扩展标签
        /// </summary>
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 字典数据更新DTO
    /// </summary>
    public class TaktDictDataUpdateDto : TaktDictDataCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long DictDataId { get; set; }

    }

    /// <summary>
    /// 字典数据导入DTO
    /// </summary>
    public class TaktDictDataImportDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典标签
        /// </summary>
        public string DictLabel { get; set; } = string.Empty;

        /// <summary>
        /// 字典键值
        /// </summary>
        public string DictValue { get; set; } = string.Empty;

        /// <summary>
        /// 扩展标签
        /// </summary>
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 字典数据导出DTO
    /// </summary>
    public class TaktDictDataExportDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string? DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        public string? DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        public string? DictValue { get; set; }

        /// <summary>
        /// 扩展标签
        /// </summary>
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 字典数据模板DTO
    /// </summary>
    public class TaktDictDataTemplateDto
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string? DictType { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        public string? DictLabel { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        public string? DictValue { get; set; }

        /// <summary>
        /// 扩展标签
        /// </summary>
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

}


