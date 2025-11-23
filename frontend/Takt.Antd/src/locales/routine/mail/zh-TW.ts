export default {
  routine: {
    mail: {
      // 基本資訊
      mailId: '郵件ID',
      mailType: '郵件類型',
      mailSubject: '郵件主題',
      mailContent: '郵件內容',
      mailStatus: '郵件狀態',
      mailParams: '郵件參數',
      remark: '備註',
      createTime: '建立時間',
      updateTime: '更新時間',

      // 操作按鈕
      add: '新增郵件',
      edit: '編輯郵件',
      delete: '刪除郵件',
      batchDelete: '批次刪除',
      export: '匯出',
      import: '匯入',
      downloadTemplate: '下載範本',
      preview: '預覽',
      send: '傳送',

      // 表單佔位符
      placeholder: {
        mailId: '請輸入郵件ID',
        mailType: '請選擇郵件類型',
        mailSubject: '請輸入郵件主題',
        mailContent: '請輸入郵件內容',
        mailStatus: '請選擇郵件狀態',
        mailParams: '請輸入郵件參數',
        remark: '請輸入備註',
        startTime: '開始時間',
        endTime: '結束時間'
      },

      // 表單驗證
      validation: {
        mailId: {
          required: '請輸入郵件ID',
          maxLength: '郵件ID不能超過100個字元'
        },
        mailType: {
          required: '請選擇郵件類型',
          maxLength: '郵件類型不能超過50個字元'
        },
        mailSubject: {
          required: '請輸入郵件主題',
          maxLength: '郵件主題不能超過200個字元'
        },
        mailContent: {
          required: '請輸入郵件內容',
          maxLength: '郵件內容不能超過4000個字元'
        },
        mailStatus: {
          required: '請選擇郵件狀態'
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
        title: '郵件詳情'
      }
    }
  }
} 