#nullable enable

using Mapster;
using Takt.Shared.Models;
using Takt.Domain.Entities.Routine.Schedule;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTeamScheduleDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 团队协作日程DTO类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Application.Dtos.Routine.Schedule
{
    /// <summary>
    /// 团队协作日程DTO
    /// </summary>
    public class TaktTeamScheduleDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTeamScheduleDto()
        {
            TeamScheduleCode = string.Empty;
            TeamScheduleTitle = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long TeamScheduleId { get; set; }

        /// <summary>
        /// 团队协作日程编号
        /// </summary>
        public string TeamScheduleCode { get; set; }

        /// <summary>
        /// 团队协作日程标题
        /// </summary>
        public string TeamScheduleTitle { get; set; }

        /// <summary>
        /// 团队协作日程描述
        /// </summary>
        public string? TeamScheduleDescription { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 协作地点
        /// </summary>
        public string? CollaborationLocation { get; set; }

        /// <summary>
        /// 团队负责人
        /// </summary>
        public string? TeamLeader { get; set; }

        /// <summary>
        /// 参与人员
        /// </summary>
        public string? Participants { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 团队协作日程状态(0=未开始 1=进行中 2=已完成 3=已暂停 4=已取消)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 关联项目ID
        /// </summary>
        public long? RelatedProjectId { get; set; }

        /// <summary>
        /// 关联会议ID
        /// </summary>
        public long? RelatedMeetingId { get; set; }

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
        /// 是否删除
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
    /// 团队协作日程查询DTO
    /// </summary>
    public class TaktTeamScheduleQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 团队协作日程标题
        /// </summary>
        public string? TeamScheduleTitle { get; set; }

        /// <summary>
        /// 团队协作日程编号
        /// </summary>
        public string? TeamScheduleCode { get; set; }

        /// <summary>
        /// 团队协作日程状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int? Priority { get; set; }

        /// <summary>
        /// 团队负责人
        /// </summary>
        public string? TeamLeader { get; set; }

        /// <summary>
        /// 协作地点
        /// </summary>
        public string? CollaborationLocation { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime? PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime? PlannedEndTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }
    }

    /// <summary>
    /// 团队协作日程创建DTO
    /// </summary>
    public class TaktTeamScheduleCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTeamScheduleCreateDto()
        {
            TeamScheduleTitle = string.Empty;
            TeamScheduleDescription = string.Empty;
            CollaborationLocation = string.Empty;
            TeamLeader = string.Empty;
            Participants = string.Empty;
        }

        /// <summary>
        /// 团队协作日程标题
        /// </summary>
        public string TeamScheduleTitle { get; set; }

        /// <summary>
        /// 团队协作日程描述
        /// </summary>
        public string TeamScheduleDescription { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 协作地点
        /// </summary>
        public string CollaborationLocation { get; set; }

        /// <summary>
        /// 团队负责人
        /// </summary>
        public string TeamLeader { get; set; }

        /// <summary>
        /// 参与人员
        /// </summary>
        public string Participants { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 团队协作日程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 关联项目ID
        /// </summary>
        public long? RelatedProjectId { get; set; }

        /// <summary>
        /// 关联会议ID
        /// </summary>
        public long? RelatedMeetingId { get; set; }
    }

    /// <summary>
    /// 团队协作日程更新DTO
    /// </summary>
    public class TaktTeamScheduleUpdateDto : TaktTeamScheduleCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long TeamScheduleId { get; set; }
    }

    /// <summary>
    /// 团队协作日程删除DTO
    /// </summary>
    public class TaktTeamScheduleDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long TeamScheduleId { get; set; }
    }

    /// <summary>
    /// 团队协作日程批量删除DTO
    /// </summary>
    public class TaktTeamScheduleBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> TeamScheduleIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 团队协作日程导入DTO
    /// </summary>
    public class TaktTeamScheduleImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTeamScheduleImportDto()
        {
            TeamScheduleTitle = string.Empty;
            TeamScheduleDescription = string.Empty;
            CollaborationLocation = string.Empty;
            TeamLeader = string.Empty;
            Participants = string.Empty;
        }

        /// <summary>
        /// 团队协作日程标题
        /// </summary>
        public string TeamScheduleTitle { get; set; }

        /// <summary>
        /// 团队协作日程描述
        /// </summary>
        public string TeamScheduleDescription { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 协作地点
        /// </summary>
        public string CollaborationLocation { get; set; }

        /// <summary>
        /// 团队负责人
        /// </summary>
        public string TeamLeader { get; set; }

        /// <summary>
        /// 参与人员
        /// </summary>
        public string Participants { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 团队协作日程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 关联项目ID
        /// </summary>
        public long? RelatedProjectId { get; set; }

        /// <summary>
        /// 关联会议ID
        /// </summary>
        public long? RelatedMeetingId { get; set; }
    }

    /// <summary>
    /// 团队协作日程导出DTO
    /// </summary>
    public class TaktTeamScheduleExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTeamScheduleExportDto()
        {
            TeamScheduleTitle = string.Empty;
            TeamScheduleDescription = string.Empty;
            CollaborationLocation = string.Empty;
            TeamLeader = string.Empty;
            Participants = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 团队协作日程标题
        /// </summary>
        public string TeamScheduleTitle { get; set; }

        /// <summary>
        /// 团队协作日程描述
        /// </summary>
        public string TeamScheduleDescription { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 协作地点
        /// </summary>
        public string CollaborationLocation { get; set; }

        /// <summary>
        /// 团队负责人
        /// </summary>
        public string TeamLeader { get; set; }

        /// <summary>
        /// 参与人员
        /// </summary>
        public string Participants { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 团队协作日程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 关联项目ID
        /// </summary>
        public long? RelatedProjectId { get; set; }

        /// <summary>
        /// 关联会议ID
        /// </summary>
        public long? RelatedMeetingId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 团队协作日程模板DTO
    /// </summary>
    public class TaktTeamScheduleTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTeamScheduleTemplateDto()
        {
            TeamScheduleTitle = string.Empty;
            TeamScheduleDescription = string.Empty;
            CollaborationLocation = string.Empty;
            TeamLeader = string.Empty;
            Participants = string.Empty;
        }

        /// <summary>
        /// 团队协作日程标题
        /// </summary>
        public string TeamScheduleTitle { get; set; }

        /// <summary>
        /// 团队协作日程描述
        /// </summary>
        public string TeamScheduleDescription { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 协作地点
        /// </summary>
        public string CollaborationLocation { get; set; }

        /// <summary>
        /// 团队负责人
        /// </summary>
        public string TeamLeader { get; set; }

        /// <summary>
        /// 参与人员
        /// </summary>
        public string Participants { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 团队协作日程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 关联项目ID
        /// </summary>
        public long? RelatedProjectId { get; set; }

        /// <summary>
        /// 关联会议ID
        /// </summary>
        public long? RelatedMeetingId { get; set; }
    }
} 




