//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktUserRoleDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-06-xx
// 版本号 : V0.0.1
// 描述   : 用户角色关联DTO
//===================================================================



namespace Takt.Application.Dtos.Identity
{
    /// <summary>
    /// 用户角色关联DTO
    /// </summary>
    public class TaktUserRoleDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long UserRoleId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long RoleId { get; set; }



        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

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
        /// 用户名称（导航属性）
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 角色名称（导航属性）
        /// </summary>
        public string? RoleName { get; set; }



        /// <summary>
        /// 角色ID集合（用于批量操作）
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long[] RoleIds { get; set; } = Array.Empty<long>();

        /// <summary>
        /// 已分配角色列表
        /// </summary>
        public List<TaktRoleDto> AssignedRoles { get; set; } = new();

        /// <summary>
        /// 可选角色列表
        /// </summary>
        public List<TaktRoleDto> OptionalRoles { get; set; } = new();
    }
}


