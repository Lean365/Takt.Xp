//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktUserDeptDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-06-xx
// 版本号 : V0.0.1
// 描述   : 用户部门关联DTO
//===================================================================

namespace Takt.Application.Dtos.Identity
{
    /// <summary>
    /// 用户部门关联DTO
    /// </summary>
    public class TaktUserDeptDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long UserDeptId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long UserId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long DeptId { get; set; }

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
        /// 部门名称（导航属性）
        /// </summary>
        public string? DeptName { get; set; }

        /// <summary>
        /// 部门ID集合（用于批量操作）
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public long[] DeptIds { get; set; } = Array.Empty<long>();

        /// <summary>
        /// 已分配部门列表
        /// </summary>
        public List<TaktDeptDto> AssignedDepts { get; set; } = new();

        /// <summary>
        /// 可选部门列表
        /// </summary>
        public List<TaktDeptDto> OptionalDepts { get; set; } = new();
    }
}


