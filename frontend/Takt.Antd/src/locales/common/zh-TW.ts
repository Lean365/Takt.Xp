export default {
  common: {
    // ==================== 系統資訊 ====================
    system: {
      title: '黑冰平台',
      slogan: '專業、高效、安全的企業管理系統',
      description: '基於 .NET 8 和 Vue 3 的現代化企業管理系統',
      copyright: '© 2024Takt(Claude AI). All rights reserved.'
    },

    // ==================== 錯誤訊息 ====================
    error: {
      clientError: '客戶端請求錯誤，請檢查請求參數',
      systemRestart: '系統維護中，請稍後再登入',
      network: '網路連接失敗，請檢查網路設定',
      unauthorized: '未授權，請重新登入',
      forbidden: '存取被拒絕',
      notFound: '請求的資源不存在',
      badRequest: '請求參數錯誤',
      serverError: '伺服器內部錯誤',
      serviceUnavailable: '服務暫時不可用',
      badGateway: '錯誤的閘道',
      gatewayTimeout: '閘道超時',
      unknown: '未知錯誤'
    },

    // ==================== 基本操作 ====================
    // 基本標題
    title: {
      list: '列表',
      detail: '詳情',
      create: '新增',
      edit: '編輯',
      preview: '預覽'
    },

    // ==================== 狀態定義 ====================
    status: {
      // 基本狀態
      base: {
        label: '狀態',
        normal: '正常',
        disabled: '停用',
        placeholder: '請選擇狀態'
      },

      // 是/否狀態
      yesNo: {
        all: '全部',
        yes: '是',
        no: '否'
      },

      // 顯示狀態
      visible: {
        show: '顯示',
        hide: '隱藏'
      },

      // 快取狀態
      cache: {
        enabled: '啟用',
        disabled: '停用'
      },

      // 線上狀態
      online: {
        online: '線上',
        offline: '離線'
      },

      // 處理狀態
      process: {
        pending: '待處理',
        processing: '處理中',
        completed: '已完成',
        failed: '失敗'
      },

      // 驗證狀態
      verify: {
        unverified: '未驗證',
        verified: '已驗證',
        failed: '驗證失敗'
      },

      // 鎖定狀態
      lock: {
        locked: '已鎖定',
        unlocked: '已解鎖'
      },

      // 發布狀態
      publish: {
        draft: '草稿',
        published: '已發布',
        unpublished: '未發布'
      },

      // 同步狀態
      sync: {
        synced: '已同步',
        unsynced: '未同步',
        syncing: '同步中'
      }
    },

    // ==================== 表單操作 ====================
    form: {
      required: '必填',
      optional: '選填',
      invalid: '無效',
      // 表單提示文字
      placeholder: {
        select: '請選擇',
        input: '請輸入',
        date: '請選擇日期',
        time: '請選擇時間'
      },
      // 使用者表單
      user: {
        // 基本資訊
        userId: {
          label: '使用者 ID',
          placeholder: '請輸入使用者 ID'
        },
        userName: {
          label: '使用者名稱',
          placeholder: '請輸入使用者名稱'
        },
        nickName: {
          label: '暱稱',
          placeholder: '請輸入暱稱'
        },
        realName: {
          label: '真實姓名',
          placeholder: '請輸入真實姓名'
        },
        englishName: {
          label: '英文姓名',
          placeholder: '請輸入英文姓名'
        },
        password: {
          label: '密碼',
          placeholder: '請輸入密碼'
        },
        confirmPassword: {
          label: '確認密碼',
          placeholder: '請再次輸入密碼'
        },
        
        // 個人資訊
        avatar: {
          label: '頭像',
          placeholder: '請上傳頭像'
        },
        gender: {
          label: '性別',
          placeholder: '請選擇性別',
          options: {
            male: '男',
            female: '女',
            other: '其他'
          }
        },
        birthday: {
          label: '生日',
          placeholder: '請選擇生日'
        },
        
        // 聯絡資訊
        email: {
          label: '電子郵件',
          placeholder: '請輸入電子郵件'
        },
        phoneNumber: {
          label: '手機',
          placeholder: '請輸入手機號碼'
        },
        telephone: {
          label: '電話',
          placeholder: '請輸入電話號碼'
        },
        
        // 組織資訊
        deptId: {
          label: '部門',
          placeholder: '請選擇部門'
        },
        roleId: {
          label: '角色',
          placeholder: '請選擇角色'
        },
        postId: {
          label: '職位',
          placeholder: '請選擇職位'
        },
        
        // 帳號資訊
        userType: {
          label: '使用者類型',
          placeholder: '請選擇使用者類型',
          options: {
            system: '系統使用者',
            normal: '一般使用者'
          }
        },
        status: {
          label: '狀態',
          placeholder: '請選擇狀態'
        },
        loginIp: {
          label: '最後登入 IP',
          placeholder: '最後登入 IP'
        },
        loginDate: {
          label: '最後登入時間',
          placeholder: '最後登入時間'
        },
        
        // 其他資訊
        remark: {
          label: '備註',
          placeholder: '請輸入備註'
        },
        sort: {
          label: '排序',
          placeholder: '請輸入排序號碼'
        }
      },
      // 備註資訊
      remark: {
        label: '備註',
        placeholder: '請輸入備註'
      }
    },

    // ==================== 表格操作 ====================
    table: {
      header: {
        operation: '操作'
      },
      config: {
        density: {
          default: '預設',
          middle: '中等',
          small: '緊湊'
        },
        columnSetting: '欄位設定'
      },
      pagination: {
        total: '共 {total} 筆',
        current: '第 {current} 頁',
        pageSize: '每頁 {pageSize} 筆',
        jump: '跳轉'
      },
      empty: '無資料',
      loading: '載入中...',
      selectAll: '全選',
      selected: '已選 {total} 筆'
    },

    // ==================== 時間操作 ====================
    datetime: {
      date: '日期',
      time: '時間',
      year: '年',
      month: '月',
      day: '日',
      hour: '時',
      minute: '分',
      second: '秒',
      startDate: '開始日期',
      endDate: '結束日期',
      startTime: '開始時間',
      endTime: '結束時間',
      createTime: '建立時間',
      updateTime: '更新時間',
      formatError: '時間格式轉換失敗',
      relativeTimeFormatError: '相對時間格式轉換失敗',
      parseError: '日期解析失敗',
      rangeSeparator: ' ~ '
    },

    // ==================== 檔案操作 ====================
    // 匯入/匯出
    import: {
      title: '資料匯入',
      file: '選擇檔案',
      select: '請選擇檔案',
      template: '下載範本',
      download: '請下載匯入範本',
      note: '匯入說明',
      tips: '請嚴格遵循匯入範本格式，否則匯入可能失敗',
      format: '僅支援 Excel 檔案！',
      size: '檔案大小不能超過{size}MB！',
      total: '總筆數',
      success: '成功筆數',
      failed: '失敗筆數',
      message: '失敗原因',
      dragText: '點擊或拖曳檔案到此區域上傳',
      dragHint: '支援 .xlsx 格式的 Excel 檔案',
      sheetName: '請確保 Excel 檔案的 sheet 名稱為：{sheetName}',
      allSuccess: '匯入成功{count}筆，全部成功！',
      partialSuccess: '匯入成功{success}筆，失敗{fail}筆',
      allFailed: '全部匯入失敗，共{count}筆',
      noData: '未讀取到任何資料',
      empty: '檔案為空，無法上傳',
      importFailed: '匯入失敗',
      templateFileName: '匯入範本_{time}.xlsx',
      limits: {
        title: '匯入限制',
        fileCount: '檔案個數限制：{count}個檔案',
        fileSize: '檔案大小限制：{size}MB',
        recordCount: '記錄數限制：{count}筆',
        fileFormat: '檔案格式：僅支援 .xlsx 格式'
      },
      recordLimit: '匯入記錄數({actual}筆)超過限制({max}筆)，建議分批匯入'
    },

    // 上傳
    upload: {
      text: '拖曳檔案至此處或點擊上傳',
      picture: '點擊上傳圖片',
      file: '點擊上傳檔案',
      icon: '點擊上傳圖示',
      limit: {
        size: '檔案大小不能超過 {size}',
        type: '僅支援 {type} 格式'
      }
    },

    // ==================== 功能按鈕 ====================
    actions: {
      // === 基本操作按鈕 ===
      add: '新增',           // @btn-add-color
      edit: '編輯',         // @btn-edit-color
      delete: '刪除',      // @btn-delete-color
      batchDelete: '批次刪除', // @btn-batch-delete-color
      view: '查看',          // @btn-view-color
      clear: '清空',         // @btn-clear-color
      forceOffline: '強制下線', // @btn-force-offline-color
      onlineStatus: '在線狀態', // @btn-online-status-color
      loginHistory: '登錄歷史', // @btn-login-history-color
      sendMail: '發送郵件',   // @btn-send-mail-color
      viewMail: '查看郵件',   // @btn-view-mail-color
      mailTemplate: '郵件模板', // @btn-mail-template-color
      sendNotification: '發送通知', // @btn-send-notification-color
      viewNotification: '查看通知', // @btn-view-notification-color
      notificationSetting: '通知設置', // @btn-notification-setting-color
      sendMessage: '發送消息', // @btn-send-message-color
      viewMessage: '查看消息', // @btn-view-message-color
      messageSetting: '消息設置', // @btn-message-setting-color

      // === 資料操作按鈕 ===
      import: '匯入',       // @btn-import-color
      export: '匯出',       // @btn-export-color
      template: '範本',       // @btn-template-color
      preview: '預覽',        // @btn-preview-color
      download: '下載',  // @btn-download-color
      batchImport: '批次匯入', // @btn-batch-import-color
      batchExport: '批次匯出', // @btn-batch-export-color
      batchPrint: '批次列印', // @btn-batch-print-color
      batchEdit: '批次編輯',  // @btn-batch-edit-color
      batchUpdate: '批次更新', // @btn-batch-update-color

      // === 狀態操作按鈕 ===
      logging: '審核',         // @btn-audit-color
      revoke: '撤銷',        // @btn-revoke-color
      stop: '停止',          // @btn-stop-color
      run: '運行',           // @btn-run-color
      force: '強制',         // @btn-forced-color
      start: '啟動',         // @btn-start-color
      suspend: '暫停',       // @btn-suspend-color
      resume: '恢復',        // @btn-resume-color
      submit: '提交',        // @btn-submit-color
      withdraw: '撤回',      // @btn-withdraw-color
      terminate: '終止',      // @btn-terminate-color

      // === 系統功能按鈕 ===
      generate: '產生',      // @btn-generate-color
      refresh: '重新整理',    // @btn-refresh-color
      info: '資訊',      // @btn-info-color
      log: '記錄',           // @btn-log-color
      chat: '聊天',          // @btn-chat-color
      copy: '複製',           // @btn-copy-color
      execute: '執行',      // @btn-execute-color
      resetPwd: '重設密碼', // @btn-reset-pwd-color
      open: '開啟',           // @btn-open-color
      close: '關閉',          // @btn-close-color
      more: '更多',             // @btn-more-color
      density: '密度',       // @btn-density-color
      columnSetting: '欄位設定', // @btn-column-setting-color

      // === 擴充功能按鈕 ===
      search: '搜尋',     // @btn-search-color
      filter: '篩選',        // @btn-filter-color
      sort: '排序',            // @btn-sort-color
      config: '設定',     // @btn-config-color
      save: '儲存',      // @btn-save-color
      cancel: '取消',        // @btn-cancel-color
      upload: '上傳',    // @btn-upload-color
      print: '列印',        // @btn-print-color
      help: '說明',             // @btn-help-color
      share: '分享',        // @btn-share-color
      lock: '鎖定',      // @btn-lock-color
      sync: '同步',     // @btn-sync-color
      expand: '展開',     // @btn-expand-color
      collapse: '收合',      // @btn-collapse-color
      approve: '核准',     // @btn-approve-color
      reject: '拒絕',        // @btn-reject-color
      comment: '註解',     // @btn-comment-color
      attach: '附加',        // @btn-attach-color

      // === 語言支援按鈕 ===
      translate: '翻譯',    // @btn-translate-color
      langSwitch: '切換語言', // @btn-lang-switch-color
      dict: '字典',     // @btn-dict-color

      // === 資料分析按鈕 ===
      analyze: '分析',      // @btn-analyze-color
      chart: '圖表',       // @btn-chart-color
      report: '報表',        // @btn-report-color
      dashboard: '儀表板', // @btn-dashboard-color
      statistics: '統計', // @btn-statistics-color
      forecast: '預測',    // @btn-forecast-color
      compare: '比較',      // @btn-compare-color

      // === 工作流程按鈕 ===
      startFlow: '啟動流程', // @btn-start-flow-color
      endFlow: '結束流程', // @btn-end-flow-color
      suspendFlow: '暫停流程', // @btn-suspend-flow-color
      resumeFlow: '恢復流程', // @btn-resume-flow-color
      transfer: '轉交',    // @btn-transfer-color
      delegate: '委派',      // @btn-delegate-color
      notify: '通知',        // @btn-notify-color
      urge: '催辦',             // @btn-urge-color
      sign: '簽核',            // @btn-sign-color
      countersign: '會簽', // @btn-countersign-color

      // === 行動裝置按鈕 ===
      scan: '掃描',          // @btn-scan-color
      location: '位置', // @btn-location-color
      call: '通話',          // @btn-call-color
      photo: '拍照', // @btn-photo-color
      voice: '語音',            // @btn-voice-color
      faceId: '臉部辨識',      // @btn-face-id-color
      fingerPrint: '指紋辨識', // @btn-finger-print-color

      // === 社群協作按鈕 ===
      follow: '追蹤',         // @btn-follow-color
      collect: '收藏',     // @btn-collect-color
      like: '讚',          // @btn-like-color
      forward: '轉發',    // @btn-forward-color
      at: '@',                  // @btn-at-color
      group: '群組',          // @btn-group-color
      team: '團隊',           // @btn-team-color

      // === 安全驗證按鈕 ===
      verifyCode: '驗證碼', // @btn-verify-code-color
      bind: '綁定',             // @btn-bind-color
      unbind: '解除綁定',         // @btn-unbind-color
      authorize: '授權',   // @btn-authorize-color
      deauthorize: '解除授權', // @btn-deauthorize-color

      // === 進階功能按鈕 ===
      version: '版本',       // @btn-version-color
      history: '歷史',    // @btn-history-color
      restore: '還原',     // @btn-restore-color
      archive: '封存',      // @btn-archive-color
      unarchive: '解除封存', // @btn-unarchive-color
      merge: '合併',       // @btn-merge-color
      split: '分割',         // @btn-split-color

      // === 系統管理按鈕 ===
      backup: '備份',    // @btn-backup-color
      restoreSys: '系統還原', // @btn-restore-sys-color
      clean: '清理',        // @btn-clean-color
      optimize: '最佳化',    // @btn-optimize-color
      monitor: '監控',    // @btn-monitor-color
      diagnose: '診斷', // @btn-diagnose-color
      maintain: '維護'     // @btn-maintain-color
    },

    // ==================== 結果及訊息 ====================
    // 結果狀態
    result: {
      success: '操作成功',
      failed: '操作失敗',
      warning: '警告',
      info: '資訊',
      error: '錯誤'
    },

    // 訊息
    message: {
      loading: '處理中...',
      saving: '儲存中...',
      submitting: '提交中...',
      deleting: '刪除中...',
      operationSuccess: '操作成功',
      operationFailed: '操作失敗',
      deleteConfirm: '確定要刪除嗎？',
      deleteSuccess: '刪除成功',
      deleteFailed: '刪除失敗',
      createSuccess: '建立成功',
      createFailed: '建立失敗',
      updateSuccess: '更新成功',
      updateFailed: '更新失敗',
      networkError: '網絡連接失敗，請檢查網絡',
      systemError: '系統錯誤',
      timeout: '請求超時',
      invalidResponse: '回應資料格式錯誤',
      backendNotStarted: '後端服務未啟動，請檢查服務狀態',
      invalidRequest: '請求參數錯誤',
      unauthorized: '未授權，請重新登入',
      forbidden: '存取被拒絕',
      notFound: '請求的資源不存在',
      serverError: '伺服器內部錯誤',
      httpError: {
        400: '請求參數錯誤',
        401: '未授權，請重新登入',
        403: '存取被拒絕',
        404: '請求的資源不存在',
        500: '伺服器內部錯誤',
        502: '錯誤的閘道',
        503: '服務暫時不可用',
        504: '閘道超時'
      },
      loadFailed: '載入失敗',
      forceOfflineConfirm: '確定要強制下線該用戶嗎？',
      forceOfflineSuccess: '強制下線成功',
      forceOfflineFailed: '強制下線失敗'
    },

    // ==================== 元件文字 ====================
    // 分頁
    tab: {
      // === 基本資訊 ===
      basic: '基本資訊',
      profile: '個人資料',
      avatar: '頭像設定',
      password: '密碼設定',
      security: '安全設定',

      // === 程式碼產生 ===
      codegen: '程式碼產生',
      codegenBasic: '基本設定',
      codegenField: '欄位設定',
      codegenPreview: '預覽',
      codegenTemplate: '範本設定',
      codegenImport: '匯入設定',
      
      // === 工作流程 ===
      workflow: '工作流程',
      flowDesign: '流程設計',
      flowDeploy: '流程部署',
      flowInstance: '流程實例',
      flowTask: '工作管理',
      flowForm: '表單設定',
      flowNotify: '訊息通知',
      
      // === 系統管理 ===
      permission: '資料權限',
      log: '操作記錄',
      dept: '部門及職位',
      role: '角色及權限',
      config: '參數設定',
      task: '排程工作',
      monitor: '系統監控'
    },

    // 按鈕文字
    button: {
      submit: '提交',
      confirm: '確認',
      ok: '確定',
      cancel: '取消',
      close: '關閉',
      reset: '重置',
      clear: '清空',
      save: '保存',
      delete: '刪除',
    }
  },

  // ==================== 選擇器元件 ====================
  select: {
    loadMore: '載入更多',
    loading: '載入中...',
    notFound: '無資料',
    selected: '已選 {count} 筆',
    selectedTotal: '共 {total} 筆',
    clear: '清除',
    search: '搜尋',
    all: '全部',
    // 錯誤訊息
    loadError: '資料載入失敗',
    searchError: '搜尋失敗',
    networkError: '網路連接失敗',
    serverError: '伺服器錯誤',
    unknownError: '未知錯誤'
  }
}


