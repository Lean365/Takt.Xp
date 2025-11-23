//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPostDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:45
// 版本号 : V.0.0.1
// 描述    : 岗位数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Identity
{
    /// <summary>
    /// 岗位基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktPostDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPostDto()
        {
            PostCode = string.Empty;
            PostName = string.Empty;
            Remark = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            DeleteBy = string.Empty;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long PostId { get; set; }

        /// <summary>
        /// 岗位编码
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int PostStatus { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteTime { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string DeleteBy { get; set; } = string.Empty;
    }

    /// <summary>
    /// 岗位查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class TaktPostQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 岗位编码
        /// </summary>
        [MaxLength(50, ErrorMessage = "岗位编码长度不能超过50个字符")]
        public string? PostCode { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "岗位名称长度不能超过50个字符")]
        public string? PostName { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? PostStatus { get; set; }
    }

    /// <summary>
    /// 岗位创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class TaktPostCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPostCreateDto()
        {
            PostCode = string.Empty;
            PostName = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 岗位编码
        /// </summary>
        [Required(ErrorMessage = "岗位编码不能为空")]
        [MaxLength(50, ErrorMessage = "岗位编码长度不能超过50个字符")]
        public string PostCode { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [Required(ErrorMessage = "岗位名称不能为空")]
        [MaxLength(50, ErrorMessage = "岗位名称长度不能超过50个字符")]
        public string PostName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序不能为空")]
        [Range(0, 9999, ErrorMessage = "显示顺序必须在0-9999之间")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int PostStatus { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 岗位更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class TaktPostUpdateDto : TaktPostCreateDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long PostId { get; set; }
    }

    /// <summary>
    /// 岗位导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktPostImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPostImportDto()
        {
            PostCode = string.Empty;
            PostName = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 岗位编码
        /// </summary>
        [Required(ErrorMessage = "岗位编码不能为空")]
        public string PostCode { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [Required(ErrorMessage = "岗位名称不能为空")]
        public string PostName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int PostStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 岗位导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktPostExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPostExportDto()
        {
            PostCode = string.Empty;
            PostName = string.Empty;
            Remark = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 岗位编码
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int PostStatus { get; set; }

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
    /// 岗位导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktPostTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPostTemplateDto()
        {
            PostCode = "Takt365";
            PostName = "系统管理员";
            OrderNum = 1;
            Remark = "系统内置岗位";
        }

        /// <summary>
        /// 岗位编码
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int PostStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 岗位状态更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktPostStatusDto
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long PostId { get; set; }

        /// <summary>
        /// 状态(0正常 1停用)
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int PostStatus { get; set; }
    }
}



