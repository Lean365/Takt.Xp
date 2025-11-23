export default {
  routine: {
    mailtmpl: {
      // 基本資訊
      templateName: '模板名稱',
      templateType: '模板類型',
      templateSubject: '模板主題',
      templateContent: '模板內容',
      templateStatus: '模板狀態',
      templateParams: '模板參數',
      remark: '備註',
      createTime: '創建時間',
      updateTime: '更新時間',

      // 操作按鈕
      add: '新增模板',
      edit: '編輯模板',
      delete: '刪除模板',
      batchDelete: '批量刪除',
      export: '導出',
      import: '導入',
      downloadTemplate: '下載模板',
      preview: '預覽',
      send: '發送',

      // 表單佔位符
      placeholder: {
        templateName: '請輸入模板名稱',
        templateType: '請選擇模板類型',
        templateSubject: '請輸入模板主題',
        templateContent: '請輸入模板內容',
        templateStatus: '請選擇模板狀態',
        templateParams: '請輸入模板參數',
        remark: '請輸入備註',
        startTime: '開始時間',
        endTime: '結束時間'
      },

      // 表單驗證
      validation: {
        templateName: {
          required: '請輸入模板名稱',
          maxLength: '模板名稱不能超過100個字符'
        },
        templateType: {
          required: '請選擇模板類型',
          maxLength: '模板類型不能超過50個字符'
        },
        templateSubject: {
          required: '請輸入模板主題',
          maxLength: '模板主題不能超過200個字符'
        },
        templateContent: {
          required: '請輸入模板內容',
          maxLength: '模板內容不能超過4000個字符'
        },
        templateStatus: {
          required: '請選擇模板狀態'
        }
      },

      // 操作結果
      message: {
        success: {
          add: '新增成功',
          edit: '編輯成功',
          delete: '刪除成功',
          batchDelete: '批量刪除成功',
          export: '導出成功',
          import: '導入成功',
          send: '發送成功'
        },
        failed: {
          add: '新增失敗',
          edit: '編輯失敗',
          delete: '刪除失敗',
          batchDelete: '批量刪除失敗',
          export: '導出失敗',
          import: '導入失敗',
          send: '發送失敗'
        }
      },

      // 詳情頁面
      detail: {
        title: '郵件模板詳情'
      }
    }
  }
}
