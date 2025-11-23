//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktRoleMenuDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-06-xx
// 版本号 : V0.0.1
// 描述   : 角色菜单关联DTO
//===================================================================

using System;
using System.Collections.Generic;

namespace Takt.Application.Dtos.Identity
{
    /// <summary>
    /// 角色菜单关联DTO
    /// </summary>
    public class TaktRoleMenuDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long RoleMenuId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long MenuId { get; set; }

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
        /// 角色名称（导航属性）
        /// </summary>
        public string? RoleName { get; set; }

        /// <summary>
        /// 菜单名称（导航属性）
        /// </summary>
        public string? MenuName { get; set; }

        /// <summary>
        /// 菜单ID集合（用于批量操作）
        /// </summary>
        public long[] MenuIds { get; set; } = Array.Empty<long>();

        /// <summary>
        /// 已分配菜单列表
        /// </summary>
        public List<TaktMenuDto> AssignedMenus { get; set; } = new();

        /// <summary>
        /// 可选菜单列表
        /// </summary>
        public List<TaktMenuDto> OptionalMenus { get; set; } = new();
    }
} 


