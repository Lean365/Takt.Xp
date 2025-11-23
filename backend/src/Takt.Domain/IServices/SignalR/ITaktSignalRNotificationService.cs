//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 实时通讯
// 命名空间 : Takt.Domain.IServices.SignalR
// 文件名 : ITaktSignalRNotificationService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : SignalR业务通知服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Takt.Shared.Models;

namespace Takt.Domain.IServices.SignalR
{
  /// <summary>
  /// SignalR业务通知服务接口 - 专注于业务通知发送和状态管理
  /// </summary>
  /// <remarks>
  /// 创建者: Lean365
  /// 创建时间: 2024-06-01
  /// </remarks>
  public interface ITaktSignalRNotificationService
  {
    #region 通用通知方法

    /// <summary>
    /// 发送通用业务通知
    /// </summary>
    /// <param name="notification">通知对象</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendBusinessNotificationAsync(TaktRealTimeNotification notification, List<long> userIds);

    /// <summary>
    /// 发送通用状态变更通知
    /// </summary>
    /// <param name="entityId">实体ID</param>
    /// <param name="entityName">实体名称</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="oldStatus">原状态</param>
    /// <param name="newStatus">新状态</param>
    /// <param name="operatorName">操作人姓名</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendStatusChangeNotificationAsync(long entityId, string entityName, string title, string content, string oldStatus, string newStatus, string operatorName, List<long> userIds);

    /// <summary>
    /// 发送通用已读状态通知
    /// </summary>
    /// <param name="entityId">实体ID</param>
    /// <param name="entityName">实体名称</param>
    /// <param name="title">标题</param>
    /// <param name="userId">用户ID</param>
    /// <param name="isRead">是否已读</param>
    /// <returns>是否成功</returns>
    Task<bool> SendReadStatusNotificationAsync(long entityId, string entityName, string title, long userId, bool isRead);

    /// <summary>
    /// 发送通用失败通知
    /// </summary>
    /// <param name="entityId">实体ID</param>
    /// <param name="entityName">实体名称</param>
    /// <param name="title">标题</param>
    /// <param name="errorMessage">错误消息</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendFailureNotificationAsync(long entityId, string entityName, string title, string errorMessage, List<long> userIds);

    #endregion

    #region 工作流通知

    /// <summary>
    /// 发送工作流启动通知
    /// </summary>
    /// <param name="workflowId">工作流实例ID</param>
    /// <param name="workflowName">工作流名称</param>
    /// <param name="initiatorName">发起人姓名</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendWorkflowStartedAsync(long workflowId, string workflowName, string initiatorName, List<long> userIds);

    /// <summary>
    /// 发送工作流审批通知
    /// </summary>
    /// <param name="workflowId">工作流实例ID</param>
    /// <param name="workflowName">工作流名称</param>
    /// <param name="nodeName">当前节点名称</param>
    /// <param name="initiatorName">发起人姓名</param>
    /// <param name="userIds">审批人ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendWorkflowApprovalAsync(long workflowId, string workflowName, string nodeName, string initiatorName, List<long> userIds);

    /// <summary>
    /// 发送工作流状态变更通知
    /// </summary>
    /// <param name="workflowId">工作流实例ID</param>
    /// <param name="workflowName">工作流名称</param>
    /// <param name="oldStatus">原状态</param>
    /// <param name="newStatus">新状态</param>
    /// <param name="operatorName">操作人姓名</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendWorkflowStatusChangedAsync(long workflowId, string workflowName, string oldStatus, string newStatus, string operatorName, List<long> userIds);

    /// <summary>
    /// 发送工作流完成通知
    /// </summary>
    /// <param name="workflowId">工作流实例ID</param>
    /// <param name="workflowName">工作流名称</param>
    /// <param name="initiatorName">发起人姓名</param>
    /// <param name="result">完成结果</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendWorkflowCompletedAsync(long workflowId, string workflowName, string initiatorName, string result, List<long> userIds);

    /// <summary>
    /// 发送工作流异常通知
    /// </summary>
    /// <param name="workflowId">工作流实例ID</param>
    /// <param name="workflowName">工作流名称</param>
    /// <param name="errorMessage">错误信息</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendWorkflowExceptionAsync(long workflowId, string workflowName, string errorMessage, List<long> userIds);

    /// <summary>
    /// 发送工作流节点到达通知
    /// </summary>
    /// <param name="workflowId">工作流实例ID</param>
    /// <param name="workflowName">工作流名称</param>
    /// <param name="nodeName">节点名称</param>
    /// <param name="initiatorName">发起人姓名</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendWorkflowNodeReachedAsync(long workflowId, string workflowName, string nodeName, string initiatorName, List<long> userIds);

    /// <summary>
    /// 发送工作流超时提醒
    /// </summary>
    /// <param name="workflowId">工作流实例ID</param>
    /// <param name="workflowName">工作流名称</param>
    /// <param name="nodeName">超时节点名称</param>
    /// <param name="timeoutHours">超时小时数</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendWorkflowTimeoutAsync(long workflowId, string workflowName, string nodeName, int timeoutHours, List<long> userIds);

    /// <summary>
    /// 发送工作流委托通知
    /// </summary>
    /// <param name="workflowId">工作流实例ID</param>
    /// <param name="workflowName">工作流名称</param>
    /// <param name="nodeName">节点名称</param>
    /// <param name="originalApproverName">原审批人姓名</param>
    /// <param name="delegateApproverName">委托审批人姓名</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendWorkflowDelegateAsync(long workflowId, string workflowName, string nodeName, string originalApproverName, string delegateApproverName, List<long> userIds);

    /// <summary>
    /// 发送工作流转办通知
    /// </summary>
    /// <param name="workflowId">工作流实例ID</param>
    /// <param name="workflowName">工作流名称</param>
    /// <param name="nodeName">节点名称</param>
    /// <param name="originalApproverName">原审批人姓名</param>
    /// <param name="transferApproverName">转办审批人姓名</param>
    /// <param name="reason">转办原因</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendWorkflowTransferAsync(long workflowId, string workflowName, string nodeName, string originalApproverName, string transferApproverName, string reason, List<long> userIds);

    #endregion

    #region 通信通知

    /// <summary>
    /// 发送邮件通知
    /// </summary>
    /// <param name="mailId">邮件ID</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendMailNotificationAsync(long mailId, string title, string content, List<long> userIds);

    /// <summary>
    /// 发送消息通知
    /// </summary>
    /// <param name="messageId">消息ID</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendMessageNotificationAsync(long messageId, string title, string content, List<long> userIds);

    /// <summary>
    /// 发送通知公告
    /// </summary>
    /// <param name="noticeId">通知公告ID</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendNoticeNotificationAsync(long noticeId, string title, string content, List<long> userIds);

    #endregion

    #region 业务通知

    /// <summary>
    /// 发送日程提醒通知
    /// </summary>
    /// <param name="scheduleId">日程ID</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendScheduleReminderAsync(long scheduleId, string title, string content, List<long> userIds);

    /// <summary>
    /// 发送会议通知
    /// </summary>
    /// <param name="meetingId">会议ID</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="participantIds">参与者ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendMeetingNotificationAsync(long meetingId, string title, string content, List<long> participantIds);

    /// <summary>
    /// 发送用车通知
    /// </summary>
    /// <param name="vehicleBookingId">用车预约ID</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendVehicleNotificationAsync(long vehicleBookingId, string title, string content, List<long> userIds);

    /// <summary>
    /// 发送ISO文档通知
    /// </summary>
    /// <param name="isoDocumentId">ISO文档ID</param>
    /// <param name="title">标题</param>
    /// <param name="content">内容</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> SendIsoDocumentNotificationAsync(long isoDocumentId, string title, string content, List<long> userIds);

    #endregion
  }
}

