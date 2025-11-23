export default {
  signalr: {
    message: {
      title: '消息中心',
      refresh: '刷新',
      refreshResult: {
        success: '数据刷新成功',
        failed: '数据刷新失败'
      },
      // 查询字段
      query: {
        messageType: '消息类型',
        messageTypePlaceholder: '请选择消息类型',
        isRead: '消息状态',
        isReadPlaceholder: '请选择消息状态',
        startTime: '开始时间',
        startTimePlaceholder: '请选择开始时间',
        endTime: '结束时间',
        endTimePlaceholder: '请选择结束时间',
        userId: '用户ID',
        userIdPlaceholder: '请输入用户ID'
      },
      // 表格列
      table: {
        signalRMessageId: '消息ID',
        messageType: '消息类型',
        title: '标题',
        content: '消息内容',
        senderId: '发送者ID',
        senderName: '发送者',
        receiverId: '接收者ID',
        receiverName: '接收者',
        isRead: '消息状态',
        sendTime: '发送时间',
        readTime: '阅读时间',
        createBy: '创建人',
        createTime: '创建时间',
        updateBy: '更新人',
        updateTime: '更新时间',
        operation: '操作'
      },
      // 状态
      status: {
        read: '已读',
        unread: '未读'
      },
      // 消息类型
      type: {
        system: '系统消息',
        user: '用户消息',
        notification: '通知',
        chat: '聊天',
        alert: '提醒',
        broadcast: '广播'
      },
      // 操作
      operation: {
        add: '发送消息',
        edit: '编辑消息',
        delete: '删除消息',
        markRead: '标记已读',
        sendTest: '发送测试消息',
        viewDetail: '查看详情'
      },
      // 消息
      message: {
        sendSuccess: '消息发送成功',
        sendFailed: '消息发送失败',
        editSuccess: '消息编辑成功',
        editFailed: '消息编辑失败',
        deleteSuccess: '消息删除成功',
        deleteFailed: '消息删除失败',
        markReadSuccess: '标记已读成功',
        markReadFailed: '标记已读失败',
        exportSuccess: '导出成功',
        exportFailed: '导出失败',
        selectMessageFirst: '请选择要操作的消息',
        selectOneMessageFirst: '请选择一条消息进行编辑',
        confirmDelete: '确定要删除选中的消息吗？',
        confirmBatchDelete: '确定要删除选中的消息吗？'
      },
      // 列设置
      columnSetting: {
        title: '列设置',
        columnSettingItem: '列设置项'
      },
      // 全屏
      fullscreen: {
        enter: '进入全屏',
        exit: '退出全屏'
      },
      empty: '暂无消息'
    }
  }
} 