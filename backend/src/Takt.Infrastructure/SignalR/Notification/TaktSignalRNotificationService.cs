//===================================================================
// 项目名: Takt.Infrastructure
// 文件名: TaktSignalRNotificationService.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-06-01
// 版本号: V0.0.1
// 描述: SignalR业务通知服务实现 - 专注于业务通知发送和状态管理
//===================================================================

using Takt.Application.Services;
using Takt.Shared.Models;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Takt.Shared.Enums;

namespace Takt.Infrastructure.SignalR.Notification
{
    /// <summary>
    /// SignalR业务通知服务实现 - 专注于业务通知发送和状态管理
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-06-01
    /// </remarks>
    public class TaktSignalRNotificationService : TaktBaseService, ITaktSignalRNotificationService
    {
        /// <summary>
        /// SignalR Hub上下文
        /// </summary>
        private readonly IHubContext<TaktSignalRHub> _hubContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="hubContext">SignalR Hub上下文</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktSignalRNotificationService(
            ITaktLogger logger,
            IHubContext<TaktSignalRHub> hubContext,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _hubContext = hubContext;
        }

        #region 通用通知方法

        /// <summary>
        /// 发送通用业务通知
        /// </summary>
        public async Task<bool> SendBusinessNotificationAsync(TaktRealTimeNotification notification, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通用业务通知 - 类型: {Type}, 用户数量: {UserCount}", notification.Type, userIds.Count);

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("通用业务通知发送成功");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通用业务通知失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通用状态变更通知
        /// </summary>
        public async Task<bool> SendStatusChangeNotificationAsync(long entityId, string entityName, string title, string content, string oldStatus, string newStatus, string operatorName, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通用状态变更通知 - 实体: {EntityName}, 用户数量: {UserCount}", entityName, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { entityId, entityName, oldStatus, newStatus, operatorName }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("通用状态变更通知发送成功");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通用状态变更通知失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通用已读状态通知
        /// </summary>
        public async Task<bool> SendReadStatusNotificationAsync(long entityId, string entityName, string title, long userId, bool isRead)
        {
            try
            {
                _logger.Info("开始发送通用已读状态通知 - 实体: {EntityName}, 用户ID: {UserId}, 已读: {IsRead}", entityName, userId, isRead);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = $"实体 {entityName} 已{(isRead ? "读" : "未读")}",
                    Timestamp = DateTime.Now,
                    Data = new { entityId, entityName, userId, isRead }
                };

                await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                _logger.Info("通用已读状态通知发送成功");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通用已读状态通知失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通用失败通知
        /// </summary>
        public async Task<bool> SendFailureNotificationAsync(long entityId, string entityName, string title, string errorMessage, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通用失败通知 - 实体: {EntityName}, 用户数量: {UserCount}", entityName, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = $"操作失败: {errorMessage}",
                    Timestamp = DateTime.Now,
                    Data = new { entityId, entityName, errorMessage }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("通用失败通知发送成功");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通用失败通知失败", ex);
                return false;
            }
        }

        #endregion

        #region 工作流通知

        /// <summary>
        /// 发送工作流启动通知
        /// </summary>
        public async Task<bool> SendWorkflowStartedAsync(long workflowId, string workflowName, string initiatorName, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送工作流启动通知 - 工作流ID: {WorkflowId}, 用户数量: {UserCount}", workflowId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = "工作流启动通知",
                    Content = $"工作流 {workflowName} 已启动，发起人: {initiatorName}",
                    Timestamp = DateTime.Now,
                    Data = new { workflowId, workflowName, initiatorName }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("工作流启动通知发送成功 - 工作流ID: {WorkflowId}", workflowId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送工作流启动通知失败 - 工作流ID: {WorkflowId}", workflowId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送工作流审批通知
        /// </summary>
        public async Task<bool> SendWorkflowApprovalAsync(long workflowId, string workflowName, string nodeName, string initiatorName, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送工作流审批通知 - 工作流ID: {WorkflowId}, 用户数量: {UserCount}", workflowId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = "工作流审批通知",
                    Content = $"工作流 {workflowName} 需要您的审批，当前节点: {nodeName}，发起人: {initiatorName}",
                    Timestamp = DateTime.Now,
                    Data = new { workflowId, workflowName, nodeName, initiatorName }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("工作流审批通知发送成功 - 工作流ID: {WorkflowId}", workflowId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送工作流审批通知失败 - 工作流ID: {WorkflowId}", workflowId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送工作流状态变更通知
        /// </summary>
        public async Task<bool> SendWorkflowStatusChangedAsync(long workflowId, string workflowName, string oldStatus, string newStatus, string operatorName, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送工作流状态变更通知 - 工作流ID: {WorkflowId}, 用户数量: {UserCount}", workflowId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = "工作流状态变更通知",
                    Content = $"工作流 {workflowName} 状态已从 {oldStatus} 变更为 {newStatus}，操作人: {operatorName}",
                    Timestamp = DateTime.Now,
                    Data = new { workflowId, workflowName, oldStatus, newStatus, operatorName }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("工作流状态变更通知发送成功 - 工作流ID: {WorkflowId}", workflowId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送工作流状态变更通知失败 - 工作流ID: {WorkflowId}", workflowId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送工作流完成通知
        /// </summary>
        public async Task<bool> SendWorkflowCompletedAsync(long workflowId, string workflowName, string initiatorName, string result, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送工作流完成通知 - 工作流ID: {WorkflowId}, 用户数量: {UserCount}", workflowId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = "工作流完成通知",
                    Content = $"工作流 {workflowName} 已完成，结果: {result}，发起人: {initiatorName}",
                    Timestamp = DateTime.Now,
                    Data = new { workflowId, workflowName, initiatorName, result }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("工作流完成通知发送成功 - 工作流ID: {WorkflowId}", workflowId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送工作流完成通知失败 - 工作流ID: {WorkflowId}", workflowId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送工作流异常通知
        /// </summary>
        public async Task<bool> SendWorkflowExceptionAsync(long workflowId, string workflowName, string errorMessage, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送工作流异常通知 - 工作流ID: {WorkflowId}, 用户数量: {UserCount}", workflowId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = "工作流异常通知",
                    Content = $"工作流 {workflowName} 发生异常: {errorMessage}",
                    Timestamp = DateTime.Now,
                    Data = new { workflowId, workflowName, errorMessage }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("工作流异常通知发送成功 - 工作流ID: {WorkflowId}", workflowId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送工作流异常通知失败 - 工作流ID: {WorkflowId}", workflowId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送工作流节点到达通知
        /// </summary>
        public async Task<bool> SendWorkflowNodeReachedAsync(long workflowId, string workflowName, string nodeName, string initiatorName, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送工作流节点到达通知 - 工作流ID: {WorkflowId}, 用户数量: {UserCount}", workflowId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = "工作流节点到达通知",
                    Content = $"工作流 {workflowName} 已到达节点: {nodeName}，发起人: {initiatorName}",
                    Timestamp = DateTime.Now,
                    Data = new { workflowId, workflowName, nodeName, initiatorName }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("工作流节点到达通知发送成功 - 工作流ID: {WorkflowId}", workflowId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送工作流节点到达通知失败 - 工作流ID: {WorkflowId}", workflowId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送工作流超时提醒
        /// </summary>
        public async Task<bool> SendWorkflowTimeoutAsync(long workflowId, string workflowName, string nodeName, int timeoutHours, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送工作流超时提醒 - 工作流ID: {WorkflowId}, 用户数量: {UserCount}", workflowId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = "工作流超时提醒",
                    Content = $"工作流 {workflowName} 在节点 {nodeName} 已超时 {timeoutHours} 小时，请及时处理",
                    Timestamp = DateTime.Now,
                    Data = new { workflowId, workflowName, nodeName, timeoutHours }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("工作流超时提醒发送成功 - 工作流ID: {WorkflowId}", workflowId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送工作流超时提醒失败 - 工作流ID: {WorkflowId}", workflowId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送工作流委托通知
        /// </summary>
        public async Task<bool> SendWorkflowDelegateAsync(long workflowId, string workflowName, string nodeName, string originalApproverName, string delegateApproverName, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送工作流委托通知 - 工作流ID: {WorkflowId}, 用户数量: {UserCount}", workflowId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = "工作流委托通知",
                    Content = $"工作流 {workflowName} 在节点 {nodeName} 已委托，原审批人: {originalApproverName}，委托审批人: {delegateApproverName}",
                    Timestamp = DateTime.Now,
                    Data = new { workflowId, workflowName, nodeName, originalApproverName, delegateApproverName }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("工作流委托通知发送成功 - 工作流ID: {WorkflowId}", workflowId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送工作流委托通知失败 - 工作流ID: {WorkflowId}", workflowId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送工作流转办通知
        /// </summary>
        public async Task<bool> SendWorkflowTransferAsync(long workflowId, string workflowName, string nodeName, string originalApproverName, string transferApproverName, string reason, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送工作流转办通知 - 工作流ID: {WorkflowId}, 用户数量: {UserCount}", workflowId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = "工作流转办通知",
                    Content = $"工作流 {workflowName} 在节点 {nodeName} 已转办，原审批人: {originalApproverName}，转办审批人: {transferApproverName}，转办原因: {reason}",
                    Timestamp = DateTime.Now,
                    Data = new { workflowId, workflowName, nodeName, originalApproverName, transferApproverName, reason }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("工作流转办通知发送成功 - 工作流ID: {WorkflowId}", workflowId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送工作流转办通知失败 - 工作流ID: {WorkflowId}", workflowId, ex);
                return false;
            }
        }

        #endregion

        #region 通信通知

        /// <summary>
        /// 发送邮件通知
        /// </summary>
        public async Task<bool> SendMailNotificationAsync(long mailId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送邮件通知 - 邮件ID: {MailId}, 用户数量: {UserCount}", mailId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { mailId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("邮件通知发送成功 - 邮件ID: {MailId}", mailId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送邮件通知失败 - 邮件ID: {MailId}", mailId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送消息通知
        /// </summary>
        public async Task<bool> SendMessageNotificationAsync(long messageId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送消息通知 - 消息ID: {MessageId}, 用户数量: {UserCount}", messageId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { messageId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("消息通知发送成功 - 消息ID: {MessageId}", messageId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送消息通知失败 - 消息ID: {MessageId}", messageId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送通知公告
        /// </summary>
        public async Task<bool> SendNoticeNotificationAsync(long noticeId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送通知公告 - 通知公告ID: {NoticeId}, 用户数量: {UserCount}", noticeId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { noticeId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("通知公告发送成功 - 通知公告ID: {NoticeId}", noticeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送通知公告失败 - 通知公告ID: {NoticeId}", noticeId, ex);
                return false;
            }
        }

        #endregion

        #region 业务通知

        /// <summary>
        /// 发送日程提醒通知
        /// </summary>
        public async Task<bool> SendScheduleReminderAsync(long scheduleId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送日程提醒通知 - 日程ID: {ScheduleId}, 用户数量: {UserCount}", scheduleId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { scheduleId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("日程提醒通知发送成功 - 日程ID: {ScheduleId}", scheduleId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送日程提醒通知失败 - 日程ID: {ScheduleId}", scheduleId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送会议通知
        /// </summary>
        public async Task<bool> SendMeetingNotificationAsync(long meetingId, string title, string content, List<long> participantIds)
        {
            try
            {
                _logger.Info("开始发送会议通知 - 会议ID: {MeetingId}, 参与者数量: {ParticipantCount}", meetingId, participantIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { meetingId, title, content }
                };

                foreach (var userId in participantIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("会议通知发送成功 - 会议ID: {MeetingId}", meetingId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送会议通知失败 - 会议ID: {MeetingId}", meetingId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送用车通知
        /// </summary>
        public async Task<bool> SendVehicleNotificationAsync(long vehicleBookingId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送用车通知 - 用车预约ID: {VehicleBookingId}, 用户数量: {UserCount}", vehicleBookingId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { vehicleBookingId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("用车通知发送成功 - 用车预约ID: {VehicleBookingId}", vehicleBookingId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送用车通知失败 - 用车预约ID: {VehicleBookingId}", vehicleBookingId, ex);
                return false;
            }
        }

        /// <summary>
        /// 发送ISO文档通知
        /// </summary>
        public async Task<bool> SendIsoDocumentNotificationAsync(long isoDocumentId, string title, string content, List<long> userIds)
        {
            try
            {
                _logger.Info("开始发送ISO文档通知 - ISO文档ID: {IsoDocumentId}, 用户数量: {UserCount}", isoDocumentId, userIds.Count);

                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.User,
                    Title = title,
                    Content = content,
                    Timestamp = DateTime.Now,
                    Data = new { isoDocumentId, title, content }
                };

                foreach (var userId in userIds)
                {
                    await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceivePersonalNotice", notification);
                }

                _logger.Info("ISO文档通知发送成功 - ISO文档ID: {IsoDocumentId}", isoDocumentId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("发送ISO文档通知失败 - ISO文档ID: {IsoDocumentId}", isoDocumentId, ex);
                return false;
            }
        }

        #endregion
    }
}



