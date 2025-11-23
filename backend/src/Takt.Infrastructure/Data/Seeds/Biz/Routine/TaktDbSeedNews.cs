//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedNews.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻管理种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.News;

namespace Takt.Infrastructure.Data.Seeds.Biz.Routine;

/// <summary>
/// 新闻管理种子数据提供类
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 提供新闻管理的默认种子数据
/// 2. 包含示例新闻记录、新闻评论记录和新闻话题记录
/// 3. 用于系统初始化和测试
/// </remarks>
public class TaktDbSeedNews
{
    /// <summary>
    /// 获取默认新闻数据
    /// </summary>
    /// <returns>新闻数据列表</returns>
    public List<TaktNews> GetDefaultNews()
    {
        return new List<TaktNews>
        {
            new TaktNews
            {
                NewsTitle = "Takt.Xp管理系统正式上线",
                NewsSubtitle = "全新的企业级管理系统，助力企业数字化转型",
                NewsContent = @"<p>经过数月的精心开发和测试，Takt.Xp管理系统正式上线！</p>
                <p>本系统采用最新的技术架构，提供以下核心功能：</p>
                <ul>
                    <li>用户身份认证与权限管理</li>
                    <li>企业组织架构管理</li>
                    <li>业务流程自动化</li>
                    <li>数据统计与分析</li>
                    <li>移动端支持</li>
                </ul>
                <p>系统具有以下特点：</p>
                <ul>
                    <li>界面简洁美观，操作便捷</li>
                    <li>数据安全可靠，支持多级权限控制</li>
                    <li>提供丰富的API接口，支持第三方集成</li>
                </ul>
                <p>欢迎各位用户使用并提出宝贵意见！</p>",
                NewsSummary = "Takt.Xp管理系统正式上线，提供全面的企业级管理功能，助力企业数字化转型。",
                NewsCategory = "system", // 系统新闻
                NewsTags = "系统上线,企业管理,数字化转型",
                NewsAuthor = "系统管理员",
                NewsSource = "Takt.Xp官方",
                NewsSourceUrl = "https://www.Takt365.com",
                NewsCoverImage = "/images/news/system-launch.jpg",
                Status = 1, // 1-已发布
                NewsPublishTime = DateTime.Now,
                NewsIsTop = 1, // 1-置顶
                NewsIsRecommend = 1, // 1-推荐
                NewsIsHot = 1, // 1-热门
                NewsReadCount = 0,
                NewsLikeCount = 0,
                NewsCommentCount = 0,
                NewsShareCount = 0,
                NewsRecommendCount = 0,
                NewsAuditStatus = 1, // 1-审核通过
                NewsAuditedBy = "系统管理员",
                NewsAuditedTime = DateTime.Now,
                NewsSeoTitle = "Takt.Xp管理系统正式上线 - 企业级管理解决方案",
                NewsSeoKeywords = "管理系统,企业级,数字化转型,Takt.Xp",
                NewsSeoDescription = "Takt.Xp管理系统正式上线，提供全面的企业级管理功能，助力企业数字化转型。",
                OrderNum = 1,
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktNews
            {
                NewsTitle = "系统功能更新说明",
                NewsSubtitle = "新增多项实用功能，提升用户体验",
                NewsContent = @"<p>为了提供更好的用户体验，我们对系统进行了以下更新：</p>
                <h3>新增功能</h3>
                <ul>
                    <li>新闻管理模块：支持新闻发布、评论、话题等功能</li>
                    <li>车辆管理模块：支持车辆预约、驾驶员管理等功能</li>
                    <li>会议管理模块：支持会议安排、会议室预订等功能</li>
                    <li>通知公告模块：支持系统通知、公告发布等功能</li>
                </ul>
                <h3>功能优化</h3>
                <ul>
                    <li>优化了用户界面，提升了操作体验</li>
                    <li>增强了数据安全性，支持更细粒度的权限控制</li>
                    <li>改进了系统性能，提升了响应速度</li>
                    <li>完善了移动端适配，支持手机和平板访问</li>
                </ul>
                <p>感谢您的使用，如有任何问题请及时反馈！</p>",
                NewsSummary = "系统新增多项实用功能，包括新闻管理、车辆管理、会议管理等模块，并优化了用户体验。",
                NewsCategory = "update", // 更新新闻
                NewsTags = "功能更新,用户体验,系统优化",
                NewsAuthor = "产品经理",
                NewsSource = "Takt.Xp官方",
                NewsSourceUrl = "https://www.Takt365.com",
                NewsCoverImage = "/images/news/system-update.jpg",
                Status = 1, // 1-已发布
                NewsPublishTime = DateTime.Now.AddDays(-1),
                NewsIsTop = 0, // 0-不置顶
                NewsIsRecommend = 1, // 1-推荐
                NewsIsHot = 0, // 0-普通
                NewsReadCount = 0,
                NewsLikeCount = 0,
                NewsCommentCount = 0,
                NewsShareCount = 0,
                NewsRecommendCount = 0,
                NewsAuditStatus = 1, // 1-审核通过
                NewsAuditedBy = "系统管理员",
                NewsAuditedTime = DateTime.Now.AddDays(-1),
                NewsSeoTitle = "系统功能更新说明 - 新增多项实用功能",
                NewsSeoKeywords = "系统更新,新功能,用户体验,Takt.Xp",
                NewsSeoDescription = "系统新增多项实用功能，包括新闻管理、车辆管理、会议管理等模块，并优化了用户体验。",
                OrderNum = 2,
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-1),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-1)
            }
        };
    }

    /// <summary>
    /// 获取默认新闻评论数据
    /// </summary>
    /// <returns>新闻评论数据列表</returns>
    public List<TaktNewsComment> GetDefaultNewsComments()
    {
        return new List<TaktNewsComment>
        {
            new TaktNewsComment
            {
                NewsId = 1, // 关联到第一条新闻
                CommentContent = "恭喜系统上线！期待更多功能！",
                CommentUserId = 1958104363528491008, // admin用户ID
                CommentUserName = "admin",
                CommentUserAvatar = "/images/avatars/admin.jpg",
                ParentCommentId = 0, // 一级评论
                ReplyUserId = 0,
                ReplyUserName = null,
                CommentStatus = 1, // 1-已通过
                AuditedBy = "系统管理员",
                AuditedTime = DateTime.Now,
                AuditComments = "评论内容健康，审核通过",
                AuditType = 1, // 1-责任编辑审核
                LikeCount = 0,
                ReplyCount = 0,
                IpAddress = "127.0.0.1",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36",
                OrderNum = 1,
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktNewsComment
            {
                NewsId = 1, // 关联到第一条新闻
                CommentContent = "系统界面很美观，操作也很流畅！",
                CommentUserId = 1958104363528491008, // admin用户ID
                CommentUserName = "admin",
                CommentUserAvatar = "/images/avatars/admin.jpg",
                ParentCommentId = 0, // 一级评论
                ReplyUserId = 0,
                ReplyUserName = null,
                CommentStatus = 1, // 1-已通过
                AuditedBy = "系统管理员",
                AuditedTime = DateTime.Now,
                AuditComments = "评论内容健康，审核通过",
                AuditType = 1, // 1-责任编辑审核
                LikeCount = 0,
                ReplyCount = 0,
                IpAddress = "127.0.0.1",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36",
                OrderNum = 2,
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取默认新闻话题数据
    /// </summary>
    /// <returns>新闻话题数据列表</returns>
    public List<TaktNewsTopic> GetDefaultNewsTopics()
    {
        return new List<TaktNewsTopic>
        {
            new TaktNewsTopic
            {
                TopicName = "系统上线",
                TopicDescription = "关于Takt.Xp管理系统上线的相关讨论",
                TopicKeywords = "系统上线,管理系统,企业级",
                TopicCategory = "system", // 系统类话题
                TopicTags = "系统,上线,管理",
                TopicIcon = "/images/topics/system-icon.png",
                TopicCover = "/images/topics/system-cover.jpg",
                TopicColor = "#1890ff",
                Status = 1, // 1-活跃
                TopicIsHot = 1, // 1-热门话题
                TopicIsRecommend = 1, // 1-推荐话题
                TopicIsTop = 1, // 1-置顶话题
                TopicType = 1, // 1-普通话题
                TopicStartTime = DateTime.Now.AddDays(-7),
                TopicEndTime = DateTime.Now.AddDays(30),
                TopicParticipantCount = 0,
                TopicNewsCount = 0,
                TopicCommentCount = 0,
                TopicLikeCount = 0,
                TopicShareCount = 0,
                TopicReadCount = 0,
                TopicCreatorId = 1958104363528491008, // admin用户ID
                TopicCreatorName = "admin",
                TopicAdminIds = "1958104363528491008",
                TopicAdminNames = "admin",
                TopicRules = "请文明发言，遵守社区规范",
                TopicSettings = "{\"allowComment\":true,\"allowShare\":true,\"autoAudit\":false}",
                TopicSeoTitle = "系统上线话题 - Takt.Xp管理系统讨论",
                TopicSeoKeywords = "系统上线,话题讨论,Takt.Xp",
                TopicSeoDescription = "关于Takt.Xp管理系统上线的相关讨论话题",
                OrderNum = 1,
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktNewsTopic
            {
                TopicName = "功能更新",
                TopicDescription = "关于系统功能更新和优化的讨论",
                TopicKeywords = "功能更新,系统优化,用户体验",
                TopicCategory = "update", // 更新类话题
                TopicTags = "更新,功能,优化",
                TopicIcon = "/images/topics/update-icon.png",
                TopicCover = "/images/topics/update-cover.jpg",
                TopicColor = "#52c41a",
                Status = 1, // 1-活跃
                TopicIsHot = 0, // 0-普通话题
                TopicIsRecommend = 1, // 1-推荐话题
                TopicIsTop = 0, // 0-不置顶
                TopicType = 1, // 1-普通话题
                TopicStartTime = DateTime.Now.AddDays(-3),
                TopicEndTime = DateTime.Now.AddDays(60),
                TopicParticipantCount = 0,
                TopicNewsCount = 0,
                TopicCommentCount = 0,
                TopicLikeCount = 0,
                TopicShareCount = 0,
                TopicReadCount = 0,
                TopicCreatorId = 1958104363528491008, // admin用户ID
                TopicCreatorName = "admin",
                TopicAdminIds = "1958104363528491008",
                TopicAdminNames = "admin",
                TopicRules = "请文明发言，遵守社区规范",
                TopicSettings = "{\"allowComment\":true,\"allowShare\":true,\"autoAudit\":false}",
                TopicSeoTitle = "功能更新话题 - 系统优化讨论",
                TopicSeoKeywords = "功能更新,系统优化,话题讨论",
                TopicSeoDescription = "关于系统功能更新和优化的讨论话题",
                OrderNum = 2,
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-1),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-1)
            }
        };
    }
}


