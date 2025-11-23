#nullable enable

using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom.Change
{
    /// <summary>
    /// 源设变DTO
    /// </summary>
    public class TaktSourceChangeDto : TaktPagedQuery
    {
        /// <summary>
        /// 源设变ID
        /// </summary>
        [AdaptMember("Id")]
        public string SourceChangeId { get; set; } = string.Empty;

        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// TCJ担当
        /// </summary>
        public string? TcjOwner { get; set; }

        /// <summary>
        /// TCJ依赖
        /// </summary>
        public string? TcjDepend { get; set; }

        /// <summary>
        /// 设变会议
        /// </summary>
        public string? ChangeMeeting { get; set; }

        /// <summary>
        /// PP署号
        /// </summary>
        public string? PpNumber { get; set; }

        /// <summary>
        /// 技联书
        /// </summary>
        public string? TechDoc { get; set; }

        /// <summary>
        /// 实施
        /// </summary>
        public string? Implement { get; set; }

        /// <summary>
        /// 主要变更理由
        /// </summary>
        public string? MainReason { get; set; }

        /// <summary>
        /// 次变更理由
        /// </summary>
        public string? SubReason { get; set; }

        /// <summary>
        /// 变规
        /// </summary>
        public string? ChangeRule { get; set; }

        /// <summary>
        /// 进行状况
        /// </summary>
        public string? ProgressStatus { get; set; }

        /// <summary>
        /// 源设变明细集合
        /// </summary>
        public List<TaktSourceChangeDetailDto> SourceChangeDetails { get; set; } = new();
    }

    /// <summary>
    /// 源设变查询DTO
    /// </summary>
    public class TaktSourceChangeQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string? ChangeNumber { get; set; }

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 发行日期开始
        /// </summary>
        public DateTime? IssueDateStart { get; set; }

        /// <summary>
        /// 发行日期结束
        /// </summary>
        public DateTime? IssueDateEnd { get; set; }

        /// <summary>
        /// TCJ担当
        /// </summary>
        public string? TcjOwner { get; set; }
    }

    /// <summary>
    /// 源设变创建DTO
    /// </summary>
    public class TaktSourceChangeCreateDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// TCJ担当
        /// </summary>
        public string? TcjOwner { get; set; }

        /// <summary>
        /// TCJ依赖
        /// </summary>
        public string? TcjDepend { get; set; }

        /// <summary>
        /// 设变会议
        /// </summary>
        public string? ChangeMeeting { get; set; }

        /// <summary>
        /// PP署号
        /// </summary>
        public string? PpNumber { get; set; }

        /// <summary>
        /// 技联书
        /// </summary>
        public string? TechDoc { get; set; }

        /// <summary>
        /// 实施
        /// </summary>
        public string? Implement { get; set; }

        /// <summary>
        /// 主要变更理由
        /// </summary>
        public string? MainReason { get; set; }

        /// <summary>
        /// 次变更理由
        /// </summary>
        public string? SubReason { get; set; }

        /// <summary>
        /// 变规
        /// </summary>
        public string? ChangeRule { get; set; }

        /// <summary>
        /// 进行状况
        /// </summary>
        public string? ProgressStatus { get; set; }
    }

    /// <summary>
    /// 源设变更新DTO
    /// </summary>
    public class TaktSourceChangeUpdateDto : TaktSourceChangeCreateDto
    {
        /// <summary>
        /// 源设变ID
        /// </summary>
        [AdaptMember("Id")]
        public string SourceChangeId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 源设变导入DTO
    /// </summary>
    public class TaktSourceChangeImportDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// TCJ担当
        /// </summary>
        public string? TcjOwner { get; set; }

        /// <summary>
        /// TCJ依赖
        /// </summary>
        public string? TcjDepend { get; set; }

        /// <summary>
        /// 设变会议
        /// </summary>
        public string? ChangeMeeting { get; set; }

        /// <summary>
        /// PP署号
        /// </summary>
        public string? PpNumber { get; set; }

        /// <summary>
        /// 技联书
        /// </summary>
        public string? TechDoc { get; set; }

        /// <summary>
        /// 实施
        /// </summary>
        public string? Implement { get; set; }

        /// <summary>
        /// 主要变更理由
        /// </summary>
        public string? MainReason { get; set; }

        /// <summary>
        /// 次变更理由
        /// </summary>
        public string? SubReason { get; set; }

        /// <summary>
        /// 变规
        /// </summary>
        public string? ChangeRule { get; set; }

        /// <summary>
        /// 进行状况
        /// </summary>
        public string? ProgressStatus { get; set; }
    }

    /// <summary>
    /// 源设变导出DTO
    /// </summary>
    public class TaktSourceChangeExportDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// TCJ担当
        /// </summary>
        public string? TcjOwner { get; set; }

        /// <summary>
        /// TCJ依赖
        /// </summary>
        public string? TcjDepend { get; set; }

        /// <summary>
        /// 设变会议
        /// </summary>
        public string? ChangeMeeting { get; set; }

        /// <summary>
        /// PP署号
        /// </summary>
        public string? PpNumber { get; set; }

        /// <summary>
        /// 技联书
        /// </summary>
        public string? TechDoc { get; set; }

        /// <summary>
        /// 实施
        /// </summary>
        public string? Implement { get; set; }

        /// <summary>
        /// 主要变更理由
        /// </summary>
        public string? MainReason { get; set; }

        /// <summary>
        /// 次变更理由
        /// </summary>
        public string? SubReason { get; set; }

        /// <summary>
        /// 变规
        /// </summary>
        public string? ChangeRule { get; set; }

        /// <summary>
        /// 进行状况
        /// </summary>
        public string? ProgressStatus { get; set; }
    }

    /// <summary>
    /// 源设变模板DTO
    /// </summary>
    public class TaktSourceChangeTemplateDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// TCJ担当
        /// </summary>
        public string? TcjOwner { get; set; }

        /// <summary>
        /// TCJ依赖
        /// </summary>
        public string? TcjDepend { get; set; }

        /// <summary>
        /// 设变会议
        /// </summary>
        public string? ChangeMeeting { get; set; }

        /// <summary>
        /// PP署号
        /// </summary>
        public string? PpNumber { get; set; }

        /// <summary>
        /// 技联书
        /// </summary>
        public string? TechDoc { get; set; }

        /// <summary>
        /// 实施
        /// </summary>
        public string? Implement { get; set; }

        /// <summary>
        /// 主要变更理由
        /// </summary>
        public string? MainReason { get; set; }

        /// <summary>
        /// 次变更理由
        /// </summary>
        public string? SubReason { get; set; }

        /// <summary>
        /// 变规
        /// </summary>
        public string? ChangeRule { get; set; }

        /// <summary>
        /// 进行状况
        /// </summary>
        public string? ProgressStatus { get; set; }
    }

    /// <summary>
    /// 源设变状态DTO
    /// </summary>
    public class TaktSourceChangeStatusDto
    {
        /// <summary>
        /// 源设变ID
        /// </summary>
        [AdaptMember("Id")]
        public string SourceChangeId { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}


