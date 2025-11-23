#nullable enable

//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktNewsTopicParticipantDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 话题参与者数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.News
{
    /// <summary>
    /// 话题参与者DTO
    /// </summary>
    public class TaktNewsTopicParticipantDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsTopicParticipantDto()
        {
            UserName = string.Empty;
        }

        /// <summary>
        /// 参与者ID
        /// </summary>
        [AdaptMember("Id")]
        public long ParticipantId { get; set; }

        /// <summary>
        /// 话题ID
        /// </summary>
        public long TopicId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 参与类型
        /// </summary>
        public int ParticipationType { get; set; }

        /// <summary>
        /// 参与时间
        /// </summary>
        public DateTime ParticipationTime { get; set; }

        /// <summary>
        /// 参与状态
        /// </summary>
        public int ParticipationStatus { get; set; }

        /// <summary>
        /// 贡献度
        /// </summary>
        public int ContributionScore { get; set; }

        /// <summary>
        /// 发布内容数量
        /// </summary>
        public int ContentCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 分享数量
        /// </summary>
        public int ShareCount { get; set; }

        /// <summary>
        /// 最后活跃时间
        /// </summary>
        public DateTime? LastActiveTime { get; set; }

        /// <summary>
        /// 参与备注
        /// </summary>
        public string? ParticipationRemark { get; set; }

        /// <summary>
        /// 是否接收通知
        /// </summary>
        public int ReceiveNotification { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public int NotificationType { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

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

        /// <summary>
        /// 关联的话题
        /// </summary>
        public TaktNewsTopicDto? Topic { get; set; }
    }

    /// <summary>
    /// 话题参与者查询DTO
    /// </summary>
    public class TaktNewsTopicParticipantQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 话题ID
        /// </summary>
        public long? TopicId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 参与类型
        /// </summary>
        public int? ParticipationType { get; set; }

        /// <summary>
        /// 参与状态
        /// </summary>
        public int? ParticipationStatus { get; set; }

        /// <summary>
        /// 是否接收通知
        /// </summary>
        public int? ReceiveNotification { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public int? NotificationType { get; set; }
    }

    /// <summary>
    /// 话题参与者创建DTO
    /// </summary>
    public class TaktNewsTopicParticipantCreateDto
    {
        /// <summary>
        /// 话题ID
        /// </summary>
        public long TopicId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 参与类型
        /// </summary>
        public int ParticipationType { get; set; } = 1;

        /// <summary>
        /// 参与状态
        /// </summary>
        public int ParticipationStatus { get; set; } = 1;

        /// <summary>
        /// 贡献度
        /// </summary>
        public int ContributionScore { get; set; }

        /// <summary>
        /// 发布内容数量
        /// </summary>
        public int ContentCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 分享数量
        /// </summary>
        public int ShareCount { get; set; }

        /// <summary>
        /// 参与备注
        /// </summary>
        public string? ParticipationRemark { get; set; }

        /// <summary>
        /// 是否接收通知
        /// </summary>
        public int ReceiveNotification { get; set; } = 1;

        /// <summary>
        /// 通知类型
        /// </summary>
        public int NotificationType { get; set; } = 1;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 话题参与者更新DTO
    /// </summary>
    public class TaktNewsTopicParticipantUpdateDto : TaktNewsTopicParticipantCreateDto
    {
        /// <summary>
        /// 参与者ID
        /// </summary>
        [AdaptMember("Id")]
        public long ParticipantId { get; set; }
    }

    /// <summary>
    /// 话题参与者状态DTO
    /// </summary>
    public class TaktNewsTopicParticipantStatusDto
    {
        /// <summary>
        /// 参与者ID
        /// </summary>
        [AdaptMember("Id")]
        public long ParticipantId { get; set; }

        /// <summary>
        /// 参与状态
        /// </summary>
        public int ParticipationStatus { get; set; }
    }
}
