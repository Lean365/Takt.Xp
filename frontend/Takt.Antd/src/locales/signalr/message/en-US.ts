export default {
  signalr: {
    message: {
      title: 'Message Center',
      refresh: 'Refresh',
      refreshResult: {
        success: 'Data refreshed successfully',
        failed: 'Data refresh failed'
      },
      // Query fields
      query: {
        messageType: 'Message Type',
        messageTypePlaceholder: 'Please select message type',
        isRead: 'Message Status',
        isReadPlaceholder: 'Please select message status',
        startTime: 'Start Time',
        startTimePlaceholder: 'Please select start time',
        endTime: 'End Time',
        endTimePlaceholder: 'Please select end time',
        userId: 'User ID',
        userIdPlaceholder: 'Please enter user ID'
      },
      // Table columns
      table: {
        signalRMessageId: 'Message ID',
        messageType: 'Message Type',
        title: 'Title',
        content: 'Message Content',
        senderId: 'Sender ID',
        senderName: 'Sender',
        receiverId: 'Receiver ID',
        receiverName: 'Receiver',
        isRead: 'Message Status',
        sendTime: 'Send Time',
        readTime: 'Read Time',
        createBy: 'Created By',
        createTime: 'Created Time',
        updateBy: 'Updated By',
        updateTime: 'Updated Time',
        operation: 'Operation'
      },
      // Status
      status: {
        read: 'Read',
        unread: 'Unread'
      },
      // Message types
      type: {
        system: 'System Message',
        user: 'User Message',
        notification: 'Notification',
        chat: 'Chat',
        alert: 'Alert',
        broadcast: 'Broadcast'
      },
      // Operations
      operation: {
        add: 'Send Message',
        edit: 'Edit Message',
        delete: 'Delete Message',
        markRead: 'Mark as Read',
        sendTest: 'Send Test Message',
        viewDetail: 'View Detail'
      },
      // Messages
      message: {
        sendSuccess: 'Message sent successfully',
        sendFailed: 'Message send failed',
        editSuccess: 'Message edited successfully',
        editFailed: 'Message edit failed',
        deleteSuccess: 'Message deleted successfully',
        deleteFailed: 'Message delete failed',
        markReadSuccess: 'Marked as read successfully',
        markReadFailed: 'Mark as read failed',
        exportSuccess: 'Export successful',
        exportFailed: 'Export failed',
        selectMessageFirst: 'Please select messages to operate',
        selectOneMessageFirst: 'Please select one message to edit',
        confirmDelete: 'Are you sure to delete the selected message?',
        confirmBatchDelete: 'Are you sure to delete the selected messages?'
      },
      // Column settings
      columnSetting: {
        title: 'Column Settings',
        columnSettingItem: 'Column Setting Item'
      },
      // Fullscreen
      fullscreen: {
        enter: 'Enter Fullscreen',
        exit: 'Exit Fullscreen'
      },
      empty: 'No messages'
    }
  }
} 