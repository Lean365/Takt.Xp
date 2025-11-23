#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktCommentOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 评论配置选项
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// 评论配置选项
    /// </summary>
    public class TaktCommentOptions
    {
        /// <summary>
        /// 配置节名称
        /// </summary>
        public const string SectionName = "Comment";

        /// <summary>
        /// 评论内容最大字数限制
        /// </summary>
        public int MaxCommentLength { get; set; } = 1000;

        /// <summary>
        /// 评论内容最小字数限制
        /// </summary>
        public int MinCommentLength { get; set; } = 1;

        /// <summary>
        /// 是否启用敏感词过滤
        /// </summary>
        public bool EnableSensitiveWordFilter { get; set; } = true;

        /// <summary>
        /// 是否启用字数限制
        /// </summary>
        public bool EnableLengthLimit { get; set; } = true;

        /// <summary>
        /// 是否启用评论审核
        /// </summary>
        public bool EnableCommentAudit { get; set; } = true;

        /// <summary>
        /// 评论审核状态（0=待审核，1=自动通过，2=需要人工审核）
        /// </summary>
        public int DefaultAuditStatus { get; set; } = 0;

        /// <summary>
        /// 是否允许匿名评论
        /// </summary>
        public bool AllowAnonymousComment { get; set; } = false;

        /// <summary>
        /// 评论频率限制（秒）
        /// </summary>
        public int CommentRateLimit { get; set; } = 30;

        /// <summary>
        /// 是否启用IP限制
        /// </summary>
        public bool EnableIpLimit { get; set; } = true;

        /// <summary>
        /// 单个IP每日评论数量限制
        /// </summary>
        public int DailyIpCommentLimit { get; set; } = 50;

        /// <summary>
        /// 是否启用用户评论数量限制
        /// </summary>
        public bool EnableUserCommentLimit { get; set; } = true;

        /// <summary>
        /// 单个用户每日评论数量限制
        /// </summary>
        public int DailyUserCommentLimit { get; set; } = 20;

        /// <summary>
        /// 是否启用评论内容重复检测
        /// </summary>
        public bool EnableDuplicateCheck { get; set; } = true;

        /// <summary>
        /// 重复检测时间范围（小时）
        /// </summary>
        public int DuplicateCheckHours { get; set; } = 24;
    }
} 




