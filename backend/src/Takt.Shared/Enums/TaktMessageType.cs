namespace Takt.Shared.Enums
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum TaktMessageType
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        System = 0,

        /// <summary>
        /// 用户消息
        /// </summary>
        User = 1,

        /// <summary>
        /// 通知消息
        /// </summary>
        Notification = 2,

        /// <summary>
        /// 警告消息
        /// </summary>
        Warning = 3,

        /// <summary>
        /// 错误消息
        /// </summary>
        Error = 4,

        /// <summary>
        /// 邮件已读
        /// </summary>
        MailRead = 5,

        /// <summary>
        /// 邮件未读
        /// </summary>
        MailUnread = 6,

        /// <summary>
        /// 消息已读
        /// </summary>
        MessageRead = 7,

        /// <summary>
        /// 消息未读
        /// </summary>
        MessageUnread = 8,

        /// <summary>
        /// 日程提醒
        /// </summary>
        ScheduleReminder = 9,

        /// <summary>
        /// 会议通知
        /// </summary>
        MeetingNotification = 10,

        /// <summary>
        /// 用车通知
        /// </summary>
        VehicleNotification = 11,

        /// <summary>
        /// ISO文档通知
        /// </summary>
        IsoDocumentNotification = 12,

        /// <summary>
        /// 邮件发送通知
        /// </summary>
        MailSent = 13,

        /// <summary>
        /// 邮件接收通知
        /// </summary>
        MailReceived = 14,

        /// <summary>
        /// 消息发送通知
        /// </summary>
        MessageSent = 15,

        /// <summary>
        /// 消息接收通知
        /// </summary>
        MessageReceived = 16,

        /// <summary>
        /// 通知公告发布
        /// </summary>
        NoticePublished = 17,

        /// <summary>
        /// 通知公告更新
        /// </summary>
        NoticeUpdated = 18,

        /// <summary>
        /// 通知公告阅读通知
        /// </summary>
        NoticeRead = 19,

        /// <summary>
        /// 通知公告未读提醒
        /// </summary>
        NoticeUnread = 20,



        /// <summary>
        /// 邮件发送失败通知
        /// </summary>
        MailSendFailed = 21,

        /// <summary>
        /// 消息发送失败通知
        /// </summary>
        MessageSendFailed = 22,

        /// <summary>
        /// 通知公告发布失败通知
        /// </summary>
        NoticePublishFailed = 23,

        /// <summary>
        /// 工作流启动通知
        /// </summary>
        WorkflowStarted = 24,

        /// <summary>
        /// 工作流审批通知
        /// </summary>
        WorkflowApproval = 25,

        /// <summary>
        /// 工作流状态变更通知
        /// </summary>
        WorkflowStatusChanged = 26,

        /// <summary>
        /// 工作流完成通知
        /// </summary>
        WorkflowCompleted = 27,

        /// <summary>
        /// 工作流异常通知
        /// </summary>
        WorkflowException = 28,

        /// <summary>
        /// 工作流节点到达通知
        /// </summary>
        WorkflowNodeReached = 29,

        /// <summary>
        /// 工作流超时提醒
        /// </summary>
        WorkflowTimeout = 30,

        /// <summary>
        /// 工作流委托通知
        /// </summary>
        WorkflowDelegate = 31,

        /// <summary>
        /// 工作流转办通知
        /// </summary>
        WorkflowTransfer = 32,

        /// <summary>
        /// ISO文档状态更新通知
        /// </summary>
        IsoDocumentStatusUpdate = 33,

        /// <summary>
        /// ISO文档签收提醒通知
        /// </summary>
        IsoDocumentSignReminder = 34
    }
}

