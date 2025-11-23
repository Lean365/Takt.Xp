//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedNotice.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 通知公告种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.Notice;

namespace Takt.Infrastructure.Data.Seeds.Biz.Routine;

/// <summary>
/// 通知公告种子数据提供类
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 提供通知公告的默认种子数据
/// 2. 包含一条示例通知记录
/// 3. 用于系统初始化和测试
/// </remarks>
public class TaktDbSeedNotice
{
    /// <summary>
    /// 获取默认通知数据
    /// </summary>
    /// <returns>通知数据列表</returns>
    public List<TaktNotice> GetDefaultNotices()
    {
        return new List<TaktNotice>
        {
            new TaktNotice
            {
                NoticeTitle = "系统上线通知",
                NoticeContent = "欢迎使用Takt.Xp管理系统！本系统已正式上线，为各位用户提供全面的业务管理服务。",
                NoticeType = 1, // 1-系统通知
                Status = 0, // 0-已发布
                NoticePublishTime = DateTime.Now,
                NoticeReceiverIds = "all", // 接收人ID列表，all表示所有用户
                NoticePriority = 1, // 1-重要
                NoticeRequireConfirm = false, // 不需要确认
                NoticeAttachments = null, // 附件列表
                NoticeAccessUrl = null, // 访问地址
                NoticeReadCount = 0, // 已读人数
                NoticeReadIds = null, // 已读人ID列表
                NoticeConfirmCount = 0, // 已确认人数
                NoticeConfirmIds = null, // 已确认人ID列表
                NoticeLastReceiptTime = null, // 最后回执时间

            }
        };
    }
}


