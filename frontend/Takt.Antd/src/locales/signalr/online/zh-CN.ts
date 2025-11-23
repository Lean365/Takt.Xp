export default {
  signalr: {
    online: {
      title: '在线用户管理',
      
      // 查询字段定义
      fields: {
        userId: {
          label: '用户ID',
          placeholder: '请输入用户ID'
        },
        startTime: {
          label: '开始时间',
          placeholder: '请选择开始时间'
        },
        endTime: {
          label: '结束时间',
          placeholder: '请选择结束时间'
        },
        onlineStatus: {
          label: '在线状态',
          placeholder: '请选择在线状态',
          options: {
            online: '在线',
            offline: '离线'
          }
        },
        // 表格列字段（页面特有）
        userName: {
          label: '用户名'
        },
        nickName: {
          label: '昵称'
        },
        connectionId: {
          label: '连接ID'
        },
        deviceId: {
          label: '设备ID'
        },
        ipAddress: {
          label: '客户端IP'
        },
        userAgent: {
          label: '用户代理'
        },
        lastActivity: {
          label: '最后活动时间'
        },
        deviceType: {
          label: '设备类型',
          options: {
            pc: 'PC',
            mobile: '移动设备',
            tablet: '平板',
            other: '其他',
            unknown: '未知'
          }
        },
        deviceName: {
          label: '设备名称'
        },
        deviceModel: {
          label: '设备型号'
        },
        osType: {
          label: '操作系统',
          options: {
            windows: 'Windows',
            macos: 'macOS',
            linux: 'Linux',
            android: 'Android',
            ios: 'iOS',
            other: '其他',
            unknown: '未知'
          }
        },
        osVersion: {
          label: '系统版本'
        },
        browserType: {
          label: '浏览器',
          options: {
            chrome: 'Chrome',
            firefox: 'Firefox',
            safari: 'Safari',
            edge: 'Edge',
            ie: 'IE',
            other: '其他',
            unknown: '未知'
          }
        },
        browserVersion: {
          label: '浏览器版本'
        },
        resolution: {
          label: '分辨率'
        },
        deviceMemory: {
          label: '设备内存'
        },
        webGLRenderer: {
          label: 'WebGL渲染器'
        },
        location: {
          label: '位置信息'
        },
        deviceFingerprint: {
          label: '设备指纹'
        },
        timezone: {
          label: '时区'
        },
        language: {
          label: '语言'
        },
        remark: {
          label: '备注'
        }
      },

      // 操作按钮
      actions: {
        refresh: '刷新',
        forceLogout: '强制下线',
        batchLogout: '批量下线',
        sendMessage: '发送消息',
        export: '导出数据'
      },

      // 消息提示
      messages: {
        confirmForceLogout: '是否确认强制下线该用户？',
        confirmBatchLogout: '是否确认批量下线选中的用户？',
        forceLogoutSuccess: '强制下线成功',
        forceLogoutFailed: '强制下线失败',
        batchLogoutSuccess: '批量下线成功',
        batchLogoutFailed: '批量下线失败',
        selectUsersFirst: '请选择要下线的用户',
        forceLogoutWarning: '您已被强制下线，请重新登录',
        sendMessageSuccess: '消息发送成功',
        exportSuccess: '导出成功',
        exportFailed: '导出失败',
        refreshSuccess: '数据刷新成功',
        refreshFailed: '数据刷新失败'
      },

      // 列设置
      columnSetting: {
        title: '列设置',
        item: '列设置项'
      },

      // 全屏
      fullscreen: {
        enter: '进入全屏',
        exit: '退出全屏'
      },

      // 空状态
      empty: '暂无在线用户'
    }
  }
} 