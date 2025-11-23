//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktRoleDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V0.0.1
// 描述   : 角色数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Identity
{
    /// <summary>
    /// 角色基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktRoleDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktRoleDto()
        {
            RoleName = string.Empty;
            RoleKey = string.Empty;
            MenuIds = new List<long>();
            DeptIds = new List<long>();
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
        public long RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
        /// </summary>
        public int DataScope { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int RoleStatus { get; set; }


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

        /// <summary>
        /// 菜单ID列表
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public List<long>? MenuIds { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public List<long>? DeptIds { get; set; }
    }

    /// <summary>
    /// 角色查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktRoleQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 角色名称
        /// </summary>

        public string? RoleName { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>

        public string? RoleKey { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? RoleStatus { get; set; }
    }

    /// <summary>
    /// 角色创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktRoleCreateDto
    {
        /// <summary>
        /// 角色编码
        /// </summary>

        public string RoleCode { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称
        /// </summary>

        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色标识
        /// </summary>

        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>

        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
        /// </summary>
        public int DataScope { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int RoleStatus { get; set; }


        /// <summary>
        /// 备注
        /// </summary>

        public string? Remark { get; set; }

        /// <summary>
        /// 菜单ID列表
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public List<long>? MenuIds { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public List<long>? DeptIds { get; set; }
    }

    /// <summary>
    /// 角色更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktRoleUpdateDto : TaktRoleCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long RoleId { get; set; }
    }

    /// <summary>
    /// 角色导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktRoleExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktRoleExportDto()
        {
            RoleName = string.Empty;
            RoleKey = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
        /// </summary>
        public int DataScope { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int RoleStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 角色状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktRoleStatusDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long RoleId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>

        public int RoleStatus { get; set; }
    }

    /// <summary>
    /// 角色导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktRoleImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktRoleImportDto()
        {
            RoleName = string.Empty;
            RoleKey = string.Empty;
            DataScope = string.Empty;
            RoleStatus = 0;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色标识
        /// </summary>
        [Required(ErrorMessage = "角色标识不能为空")]
        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
        /// </summary>
        [Required(ErrorMessage = "数据范围不能为空")]
        public string DataScope { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int RoleStatus { get; set; } = 0;
    }

    /// <summary>
    /// 角色导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktRoleTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktRoleTemplateDto()
        {
            RoleName = "系统管理员";
            RoleKey = "Takt365";
            OrderNum = 1;
            DataScope = 0;
            RoleStatus = 0;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        public string RoleName { get; set; } = "系统管理员";

        /// <summary>
        /// 角色标识
        /// </summary>
        [Required(ErrorMessage = "角色标识不能为空")]
        public string RoleKey { get; set; } = "Takt365";

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 1;

        /// <summary>
        /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
        /// </summary>
        [Required(ErrorMessage = "数据范围不能为空")]
        public int DataScope { get; set; } = 0;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public int RoleStatus { get; set; } = 0;
    }
}


