export default {
  common: {
    // ==================== 系统信息 ====================
    system: {
      title: '黑冰平台',
      slogan: '专业、高效、安全的企业管理系统',
      description: '基于 .NET 8 和 Vue 3 的现代化企业管理系统',
      copyright: '© 2024Takt(Claude AI). All rights reserved.'
    },

    // ==================== 错误信息 ====================
    error: {
      clientError: '客户端请求错误，请检查请求参数',
      systemRestart: '系统维护中，请稍后再登录',
      network: '网络连接失败，请检查网络设置',
      unauthorized: '未授权，请重新登录',
      forbidden: '访问被拒绝',
      notFound: '请求的资源不存在',
      badRequest: '请求参数错误',
      serverError: '服务器内部错误',
      serviceUnavailable: '服务暂时不可用',
      badGateway: '错误的网关',
      gatewayTimeout: '网关超时',
      unknown: '未知错误'
    },

    // ==================== 基础操作 ====================
    // 基础标题
    title: {
      list: '列表',
      detail: '详情',
      create: '新增',
      edit: '编辑',
      preview: '预览'
    },

    // ==================== 状态定义 ====================
    status: {
      // 基础状态
      base: {
        label: '状态',
        normal: '正常',
        disabled: '停用',
        placeholder: '请选择状态'
      },

      // 是否状态
      yesNo: {
        all: '全部',
        yes: '是',
        no: '否'
      },

      // 显示状态
      visible: {
        show: '显示',
        hide: '隐藏'
      },

      // 缓存状态
      cache: {
        enabled: '已启用',
        disabled: '已禁用'
      },

      // 在线状态
      online: {
        online: '在线',
        offline: '离线'
      },

      // 处理状态
      process: {
        pending: '待处理',
        processing: '处理中',
        completed: '已完成',
        failed: '处理失败'
      },

      // 验证状态
      verify: {
        unverified: '未验证',
        verified: '已验证',
        failed: '验证失败'
      },

      // 锁定状态
      lock: {
        locked: '已锁定',
        unlocked: '未锁定'
      },

      // 发布状态
      publish: {
        draft: '草稿',
        published: '已发布',
        unpublished: '未发布'
      },

      // 同步状态
      sync: {
        synced: '已同步',
        unsynced: '未同步',
        syncing: '同步中'
      }
    },

    // ==================== 时间操作 ====================
    datetime: {
      date: '日期',
      time: '时间',
      year: '年',
      month: '月',
      day: '日',
      hour: '时',
      minute: '分',
      second: '秒',
      startDate: '开始日期',
      endDate: '结束日期',
      startTime: '开始时间',
      endTime: '结束时间',
      createTime: '创建时间',
      updateTime: '更新时间',
      formatError: '格式化时间失败',
      relativeTimeFormatError: '格式化相对时间失败',
      parseError: '解析日期失败',
      rangeSeparator: ' 至 '
    },

    // ==================== 文件操作 ====================
    // 导入导出
    import: {
      title: '导入数据',
      file: '选择文件',
      select: '选择文件',
      template: '下载模板',
      download: '下载模板',
      note: '导入说明',
      tips: '请严格按照导入模板的格式填写数据，否则可能导致导入失败',
      format: '仅支持导入Excel文件！',
      size: '文件大小不能超过{size}MB！',
      total: '总记录数',
      success: '成功数',
      failed: '失败数',
      message: '失败原因',
      dragText: '点击或拖拽文件到此区域上传',
      dragHint: '支持 .xlsx 格式的 Excel 文件',
      sheetName: '请确保 Excel 文件的 sheet 名称为：{sheetName}',
      allSuccess: '导入成功{count}条，全部成功！',
      partialSuccess: '导入成功{success}条，失败{fail}条',
      allFailed: '全部导入失败，共{count}条',
      noData: '未读取到任何数据',
      empty: '文件为空，无法上传',
      importFailed: '导入失败',
      templateFileName: '导入模板_{time}.xlsx',
      limits: {
        title: '导入限制',
        fileCount: '文件个数限制：{count}个文件',
        fileSize: '文件大小限制：{size}MB',
        recordCount: '记录数限制：{count}条',
        fileFormat: '文件格式：仅支持 .xlsx 格式'
      },
      recordLimit: '导入记录数({actual}条)超过限制({max}条)，建议分批导入'
    },

    // 上传
    upload: {
      text: '将文件拖到此处，或点击上传',
      picture: '点击上传图片',
      file: '点击上传文件',
      icon: '点击上传图标',
      limit: {
        size: '文件大小不能超过 {size}',
        type: '仅支持 {type} 格式'
      },
      success: '上传成功',
      failed: '上传失败',
      fileEmpty: '文件为空，无法上传',
      fileType: '文件类型不支持',
      fileSize: '文件大小超出限制',
      fileExists: '文件已存在',
      fileNameRequired: '文件名不能为空',
      fileSelect: '请选择要上传的文件'
    },

    // ==================== 功能按钮 ====================
    actions: {
      // === 基础操作按钮 ===
      add: '新增',           // @btn-add-color
      edit: '编辑',          // @btn-edit-color
      delete: '删除',        // @btn-delete-color
      batchDelete: '批量删除', // @btn-batch-delete-color
      view: '查看',          // @btn-view-color
      clear: '清空',         // @btn-clear-color
      forceOffline: '强制下线', // @btn-force-offline-color
      onlineStatus: '在线状态', // @btn-online-status-color
      loginHistory: '登录历史', // @btn-login-history-color
      sendMail: '发送邮件',   // @btn-send-mail-color
      viewMail: '查看邮件',   // @btn-view-mail-color
      mailTemplate: '邮件模板', // @btn-mail-template-color
      sendNotification: '发送通知', // @btn-send-notification-color
      viewNotification: '查看通知', // @btn-view-notification-color
      notificationSetting: '通知设置', // @btn-notification-setting-color
      sendMessage: '发送消息', // @btn-send-message-color
      viewMessage: '查看消息', // @btn-view-message-color
      messageSetting: '消息设置', // @btn-message-setting-color

      // === 数据操作按钮 ===
      import: '导入',        // @btn-import-color
      export: '导出',        // @btn-export-color
      template: '模板',      // @btn-template-color
      preview: '预览',       // @btn-preview-color
      download: '下载',      // @btn-download-color
      batchImport: '批量导入', // @btn-batch-import-color
      batchExport: '批量导出', // @btn-batch-export-color
      batchPrint: '批量打印', // @btn-batch-print-color
      batchEdit: '批量编辑',  // @btn-batch-edit-color
      batchUpdate: '批量更新', // @btn-batch-update-color

      // === 状态操作按钮 ===
      logging: '审核',         // @btn-audit-color
      revoke: '撤销',        // @btn-revoke-color
      stop: '停止',          // @btn-stop-color
      run: '运行',           // @btn-run-color
      force: '强制',         // @btn-forced-color
      start: '开始',         // @btn-start-color
      suspend: '暂停',       // @btn-suspend-color
      resume: '恢复',        // @btn-resume-color
      submit: '提交',        // @btn-submit-color
      withdraw: '撤回',      // @btn-withdraw-color
      terminate: '终止',     // @btn-terminate-color

      // === 系统功能按钮 ===
      generate: '生成',      // @btn-generate-color

      refresh: '刷新',       // @btn-refresh-color
      info: '信息',          // @btn-info-color
      log: '日志',           // @btn-log-color
      chat: '消息',          // @btn-chat-color
      copy: '复制',          // @btn-copy-color
      clone: '克隆',          // @btn-clone-color
      execute: '执行',       // @btn-execute-color
      resetPwd: '重置密码',   // @btn-reset-pwd-color
      open: '打开',          // @btn-open-color
      close: '关闭',         // @btn-close-color
      more: '更多',          // @btn-more-color
      density: '密度',       // @btn-density-color
      columnSetting: '列设置', // @btn-column-setting-color

      // === 扩展功能按钮 ===
      search: '搜索',        // @btn-search-color
      filter: '筛选',        // @btn-filter-color
      sort: '排序',          // @btn-sort-color
      config: '配置',        // @btn-config-color
      save: '保存',          // @btn-save-color
      cancel: '取消',        // @btn-cancel-color
      upload: '上传',        // @btn-upload-color
      print: '打印',         // @btn-print-color
      help: '帮助',          // @btn-help-color
      share: '分享',         // @btn-share-color
      lock: '锁定',          // @btn-lock-color
      sync: '同步',          // @btn-sync-color
      initialize: '初始化',   // @btn-initialize-color
      expand: '展开',        // @btn-expand-color
      collapse: '收起',      // @btn-collapse-color
      approve: '审批',       // @btn-approve-color
      reject: '拒绝',        // @btn-reject-color
      comment: '评论',       // @btn-comment-color
      attach: '附件',        // @btn-attach-color

      // === 语言支持按钮 ===
      translate: '翻译',     // @btn-translate-color
      langSwitch: '切换语言', // @btn-lang-switch-color
      dict: '字典',          // @btn-dict-color

      // === 数据分析按钮 ===
      analyze: '分析',       // @btn-analyze-color
      chart: '图表',         // @btn-chart-color
      report: '报表',        // @btn-report-color
      dashboard: '仪表盘',    // @btn-dashboard-color
      statistics: '统计',    // @btn-statistics-color
      forecast: '预测',      // @btn-forecast-color
      compare: '对比',       // @btn-compare-color

      // === 工作流按钮 ===
      startFlow: '启动流程',  // @btn-start-flow-color
      pauseFlow: '暂停流程',  // @btn-pause-flow-color
      resumeFlow: '恢复流程',  // @btn-resume-flow-color
      forceEndFlow: '强制结束', // @btn-force-end-flow-color
      endFlow: '结束流程',    // @btn-end-flow-color
      suspendFlow: '暂停流程', // @btn-suspend-flow-color
      transfer: '转办',       // @btn-transfer-color
      delegate: '委托',       // @btn-delegate-color
      notify: '通知',        // @btn-notify-color
      urge: '催办',          // @btn-urge-color
      sign: '签名',          // @btn-sign-color
      countersign: '会签',    // @btn-countersign-color

      // === 移动端专用按钮 ===
      scan: '扫描',          // @btn-scan-color
      location: '定位',      // @btn-location-color
      call: '呼叫',          // @btn-call-color
      photo: '拍照',         // @btn-photo-color
      voice: '语音',         // @btn-voice-color
      faceId: '人脸识别',     // @btn-face-id-color
      fingerPrint: '指纹',    // @btn-finger-print-color

      // === 社交协作按钮 ===
      follow: '关注',        // @btn-follow-color
      collect: '收藏',       // @btn-collect-color
      like: '点赞',          // @btn-like-color
      forward: '转发',       // @btn-forward-color
      at: '@',              // @btn-at-color
      group: '群组',         // @btn-group-color
      team: '团队',          // @btn-team-color

      // === 安全认证按钮 ===
      verifyCode: '验证码',   // @btn-verify-code-color
      bind: '绑定',          // @btn-bind-color
      unbind: '解绑',        // @btn-unbind-color
      authorize: '授权',      // @btn-authorize-color
      deauthorize: '取消授权', // @btn-deauthorize-color

      // === 高级功能按钮 ===
      version: '版本',       // @btn-version-color
      history: '历史',       // @btn-history-color
      restore: '还原',       // @btn-restore-color
      archive: '归档',       // @btn-archive-color
      unarchive: '取消归档',  // @btn-unarchive-color
      merge: '合并',         // @btn-merge-color
      split: '拆分',         // @btn-split-color

      // === 系统管理按钮 ===
      backup: '备份',        // @btn-backup-color
      restoreSys: '系统还原', // @btn-restore-sys-color
      clean: '清理',         // @btn-clean-color
      optimize: '优化',      // @btn-optimize-color
      monitor: '监控',       // @btn-monitor-color
      diagnose: '诊断',      // @btn-diagnose-color
      maintain: '维护',       // @btn-maintain-color

      // === 通用按钮 ===

      send: '发送'
    },

    // ==================== 结果与消息 ====================
    // 结果状态
    result: {
      warning: '警告',
      info: '提示',
      error: '错误'
    },

    // 消息提示
    message: {
      loading: '处理中...',
      saving: '保存中...',
      submitting: '提交中...',
      deleting: '删除中...',
      operationSuccess: '操作成功',
      operationFailed: '操作失败',
      deleteConfirm: '确定要删除吗？',
      deleteSuccess: '删除成功',
      deleteFailed: '删除失败',
      createSuccess: '新增成功',
      createFailed: '新增失败',
      updateSuccess: '更新成功',
      updateFailed: '更新失败',
      forceOfflineConfirm: '确定要强制下线该用户吗？',
      forceOfflineSuccess: '强制下线成功',
      forceOfflineFailed: '强制下线失败',
      networkError: '网络连接失败，请检查网络',
      systemError: '系统错误',
      timeout: '请求超时',
      invalidResponse: '响应数据格式错误',
      backendNotStarted: '后端服务未启动，请检查服务状态',
      invalidRequest: '请求参数错误',
      unauthorized: '未授权，请重新登录',
      forbidden: '拒绝访问',
      notFound: '请求的资源不存在',
      serverError: '服务器内部错误',
      httpError: {
        400: '请求参数错误',
        401: '未授权，请重新登录',
        403: '拒绝访问',
        404: '请求的资源不存在',
        500: '服务器内部错误',
        502: '网关错误',
        503: '服务不可用',
        504: '网关超时'
      },
      loadFailed: '加载失败'
    },



    // 通用操作
    selectOne: '请选择一条记录',
    selectAtLeastOne: '请至少选择一条记录',
    failed: '操作失败',
    
    // 删除相关
    delete: {
      success: '删除成功',
      failed: '删除失败'
    },
    
    // 导出相关
    export: {
      success: '导出成功',
      failed: '导出失败'
    },

    // 通用字段
    fields: {
      createBy: '创建人',
      createTime: '创建时间',
      updateBy: '更新人',
      updateTime: '更新时间',
      operation: '操作'
    },

    // ==================== 组件文本 ====================
    // 标签页
    tab: {
      // === 基础信息 ===
      basic: '基本信息',
      profile: '个人资料',
      avatar: '头像设置',
      password: '密码设置',
      security: '安全设置',

      // === 代码生成 ===
      codegen: '代码生成',
      codegenBasic: '生成配置',
      codegenField: '字段配置',
      codegenPreview: '生成预览',
      codegenTemplate: '模板配置',
      codegenImport: '导入配置',
      
      // === 工作流程 ===
      workflow: '工作流程',
      flowDesign: '流程设计',
      flowDeploy: '流程部署',
      flowInstance: '流程实例',
      flowTask: '任务管理',
      flowForm: '表单配置',
      flowNotify: '消息通知',
      
      // === 系统管理 ===
      permission: '数据权限',
      log: '操作日志',
      dept: '部门岗位',
      role: '角色权限',
      config: '参数配置',
      task: '定时任务',
      monitor: '系统监控'
    },

    // 按钮文本
    button: {
      submit: '提交',
      confirm:'确定',
      ok: '确定',
      cancel: '取消',
      close: '关闭',
      reset: '重置',
      clear: '清空',
      save: '保存',
      delete: '删除',
      edit: '编辑',
      view: '查看'
    }
  },
  tabs:{
    closeOthers: '关闭其他',
    closeRight: '关闭右侧',
    closeAll: '关闭所有',
    // 标签页限制提示
    maxTabsReached: '标签页数量已达上限({count})，请先关闭一些标签页再打开新页面。您可以使用标签页右侧的下拉菜单快速关闭多个标签页。',
    tabsTruncated: '检测到{total}个标签页，已自动保留前{count}个标签页'
  },
  // ==================== 选择器组件 ====================
  select: {
    loadMore: '加载更多',
    loading: '加载中...',
    notFound: '暂无数据',
    selected: '已选择 {count} 项',
    selectedTotal: '共 {total} 项',
    clear: '清空',
    search: '搜索',
    all: '全部',
    // 错误消息
    loadError: '加载数据失败',
    searchError: '搜索失败',
    networkError: '网络连接失败',
    serverError: '服务器错误',
    unknownError: '未知错误'
  },

  memorial: {
    title: '纪念模式',
    mode: '纪念模式',
    holidays: '节日主题',
    customTheme: '自定义主题',
    autoMode: '自动模式',
    clearTheme: '清除主题',
    colorPrimary: '主色调',
    colorBgContainer: '容器背景色',
    colorBgLayout: '布局背景色',
    colorText: '文本颜色',
    colorTextSecondary: '次要文本颜色',
    colorBorder: '边框颜色',
    colorSplit: '分割线颜色',
    // 中国节日
    chineseNewYear: {
      name: '春节'
    },
    qingming: {
      name: '清明节'
    },
    dragonBoat: {
      name: '端午节'
    },
    midAutumn: {
      name: '中秋节'
    },
    // 日本节日
    cherryBlossom: {
      name: '樱花节'
    },
    tanabata: {
      name: '七夕节'
    },
    autumnLeaves: {
      name: '红叶节'
    },
    // 全球节日
    newYear: {
      name: '新年'
    },
    valentines: {
      name: '情人节'
    },
    easter: {
      name: '复活节'
    },
    halloween: {
      name: '万圣节'
    },
    thanksgiving: {
      name: '感恩节'
    },
    christmas: {
      name: '圣诞节'
    }
  }
} 

