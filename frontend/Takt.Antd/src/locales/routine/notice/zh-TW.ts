export default {
  routine: {
    notice: {
      // 基本資訊
      noticeId: '通知ID',
      noticeType: '通知類型',
      noticeTitle: '通知標題',
      noticeContent: '通知內容',
      noticeStatus: '通知狀態',
      noticeParams: '通知參數',
      remark: '備註',
      createTime: '建立時間',
      updateTime: '更新時間',

      // 操作按鈕
      add: '新增通知',
      edit: '編輯通知',
      delete: '刪除通知',
      batchDelete: '批次刪除',
      export: '匯出',
      import: '匯入',
      downloadTemplate: '下載範本',
      preview: '預覽',
      send: '傳送',

      // 表單佔位符
      placeholder: {
        noticeId: '請輸入通知ID',
        noticeType: '請選擇通知類型',
        noticeTitle: '請輸入通知標題',
        noticeContent: '請輸入通知內容',
        noticeStatus: '請選擇通知狀態',
        noticeParams: '請輸入通知參數',
        remark: '請輸入備註',
        startTime: '開始時間',
        endTime: '結束時間'
      },

      // 表單驗證
      validation: {
        noticeId: {
          required: '請輸入通知ID',
          maxLength: '通知ID不能超過100個字元'
        },
        noticeType: {
          required: '請選擇通知類型',
          maxLength: '通知類型不能超過50個字元'
        },
        noticeTitle: {
          required: '請輸入通知標題',
          maxLength: '通知標題不能超過200個字元'
        },
        noticeContent: {
          required: '請輸入通知內容',
          maxLength: '通知內容不能超過4000個字元'
        },
        noticeStatus: {
          required: '請選擇通知狀態'
        }
      },

      // 操作結果
      message: {
        success: {
          add: '新增成功',
          edit: '編輯成功',
          delete: '刪除成功',
          batchDelete: '批次刪除成功',
          export: '匯出成功',
          import: '匯入成功',
          send: '傳送成功'
        },
        failed: {
          add: '新增失敗',
          edit: '編輯失敗',
          delete: '刪除失敗',
          batchDelete: '批次刪除失敗',
          export: '匯出失敗',
          import: '匯入失敗',
          send: '傳送失敗'
        }
      },

      // 詳細頁面
      detail: {
        title: '通知詳情'
      }
    }
  }
} 