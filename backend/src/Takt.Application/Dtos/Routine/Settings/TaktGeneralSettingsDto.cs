//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktGeneralSettingsDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 系统设置数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.Settings
{
    /// <summary>
    /// 系统设置基础DTO
    /// </summary>
    public class TaktGeneralSettingsDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralSettingsDto()
        {
            SettingsName = string.Empty;
            SettingsKey = string.Empty;
            SettingsValue = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 设置ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long SettingsId { get; set; }

        /// <summary>
        /// 设置名称
        /// </summary>
        public string SettingsName { get; set; }

        /// <summary>
        /// 设置键名
        /// </summary>
        public string SettingsKey { get; set; }

        /// <summary>
        /// 设置键值
        /// </summary>
        public string SettingsValue { get; set; }

        /// <summary>
        /// 设置类别（0是前端，1是后端）
        /// </summary>
        public int SettingsType { get; set; } = 1;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;


        /// <summary>
        /// 排序
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
    /// 系统设置查询DTO
    /// </summary>
    public class TaktGeneralSettingsQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralSettingsQueryDto()
        {
            SettingsName = string.Empty;
            SettingsKey = string.Empty;
            SettingsValue = string.Empty;
        }

        /// <summary>
        /// 设置名称
        /// </summary>

        public string? SettingsName { get; set; }

        /// <summary>
        /// 设置键名
        /// </summary>

        public string? SettingsKey { get; set; }

        /// <summary>
        /// 设置键值
        /// </summary>

        public string? SettingsValue { get; set; }

        /// <summary>
        /// 设置类别（0是前端，1是后端）
        /// </summary>
        public int? SettingsType { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>

        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>

        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>

        public int IsEncrypted { get; set; } = 0;
    }

    /// <summary>
    /// 通用设置创建DTO
    /// </summary>
    public class TaktGeneralSettingsCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralSettingsCreateDto()
        {
            SettingsName = string.Empty;
            SettingsKey = string.Empty;
            SettingsValue = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        public string SettingsName { get; set; }

        /// <summary>
        /// 设置键名
        /// </summary>
        public string SettingsKey { get; set; }

        /// <summary>
        /// 设置键值
        /// </summary>
        public string SettingsValue { get; set; }

        /// <summary>
        /// 设置类别（0是前端，1是后端）
        /// </summary>
        public int SettingsType { get; set; } = 1;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;


        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 通用设置更新DTO
    /// </summary>
    public class TaktGeneralSettingsUpdateDto : TaktGeneralSettingsCreateDto
    {
        /// <summary>
        /// 设置ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long SettingsId { get; set; }
    }

    /// <summary>
    /// 通用设置导入DTO
    /// </summary>
    public class TaktGeneralSettingsImportDto : TaktBaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralSettingsImportDto()
        {
            SettingsName = string.Empty;
            SettingsKey = string.Empty;
            SettingsValue = string.Empty;
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        public string SettingsName { get; set; }

        /// <summary>
        /// 设置键名
        /// </summary>
        public string SettingsKey { get; set; }

        /// <summary>
        /// 设置键值
        /// </summary>
        public string SettingsValue { get; set; }

        /// <summary>
        /// 设置类别（0是前端，1是后端）
        /// </summary>
        public int SettingsType { get; set; } = 1;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;


        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 重写Id属性，使其不可设置
        /// </summary>
        public new long Id { get; }
    }

    /// <summary>
    /// 通用设置导出DTO
    /// </summary>
    public class TaktGeneralSettingsExportDto 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralSettingsExportDto()
        {
            SettingsName = string.Empty;
            SettingsKey = string.Empty;
            SettingsValue = string.Empty;
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        public string SettingsName { get; set; }

        /// <summary>
        /// 设置键名
        /// </summary>
        public string SettingsKey { get; set; }

        /// <summary>
        /// 设置键值
        /// </summary>
        public string SettingsValue { get; set; }

        /// <summary>
        /// 设置类别（0是前端，1是后端）
        /// </summary>
        public int SettingsType { get; set; } = 1;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 通用设置模板DTO
    /// </summary>
    public class TaktGeneralSettingsTemplateDto 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktGeneralSettingsTemplateDto()
        {
            SettingsName = string.Empty;
            SettingsKey = string.Empty;
            SettingsValue = string.Empty;
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        public string SettingsName { get; set; }

        /// <summary>
        /// 设置键名
        /// </summary>
        public string SettingsKey { get; set; }

        /// <summary>
        /// 设置键值
        /// </summary>
        public string SettingsValue { get; set; }

        /// <summary>
        /// 设置类别（0是前端，1是后端）
        /// </summary>
        public int SettingsType { get; set; } = 1;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 系统内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; } = 0;

        /// <summary>
        /// 是否加密
        /// </summary>
        public int IsEncrypted { get; set; } = 0;

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 通用设置状态DTO
    /// </summary>
    public class TaktGeneralSettingsStatusDto
    {
        /// <summary>
        /// 设置ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long SettingsId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }
    }
}


