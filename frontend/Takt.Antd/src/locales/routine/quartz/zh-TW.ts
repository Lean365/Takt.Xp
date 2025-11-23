export default {
  routine: {
    quartz: {
      // 基本資訊
      jobId: '任務 ID',
      jobName: '任務名稱',
      jobGroup: '任務群組',
      jobClass: '任務類別',
      jobMethod: '任務方法',
      jobParams: '任務參數',
      cronExpression: 'Cron 表達式',
      jobStatus: '任務狀態',
      remark: '備註',
      createTime: '建立時間',
      updateTime: '更新時間',

      // 操作按鈕
      add: '新增任務',
      edit: '修改任務',
      delete: '刪除任務',
      batchDelete: '批次刪除',
      export: '匯出',
      import: '匯入',
      downloadTemplate: '下載範本',
      preview: '預覽',
      execute: '執行',
      pause: '暫停',
      resume: '恢復',

      // 表單佔位符
      placeholder: {
        jobId: '請輸入任務 ID',
        jobName: '請輸入任務名稱',
        jobGroup: '請輸入任務群組',
        jobClass: '請輸入任務類別',
        jobMethod: '請輸入任務方法',
        jobParams: '請輸入任務參數',
        cronExpression: '請輸入 Cron 表達式',
        jobStatus: '請選擇任務狀態',
        remark: '請輸入備註',
        startTime: '開始時間',
        endTime: '結束時間'
      },

      // 表單驗證
      validation: {
        jobId: {
          required: '請輸入任務 ID',
          maxLength: '任務 ID 不能超過 100 個字元'
        },
        jobName: {
          required: '請輸入任務名稱',
          maxLength: '任務名稱不能超過 50 個字元'
        },
        jobGroup: {
          required: '請輸入任務群組',
          maxLength: '任務群組不能超過 50 個字元'
        },
        jobClass: {
          required: '請輸入任務類別',
          maxLength: '任務類別不能超過 200 個字元'
        },
        jobMethod: {
          required: '請輸入任務方法',
          maxLength: '任務方法不能超過 100 個字元'
        },
        cronExpression: {
          required: '請輸入 Cron 表達式',
          maxLength: 'Cron 表達式不能超過 100 個字元'
        },
        jobStatus: {
          required: '請選擇任務狀態'
        }
      },

      // 操作結果
      message: {
        success: {
          add: '新增成功',
          edit: '修改成功',
          delete: '刪除成功',
          batchDelete: '批次刪除成功',
          export: '匯出成功',
          import: '匯入成功',
          execute: '執行成功',
          pause: '暫停成功',
          resume: '恢復成功'
        },
        failed: {
          add: '新增失敗',
          edit: '修改失敗',
          delete: '刪除失敗',
          batchDelete: '批次刪除失敗',
          export: '匯出失敗',
          import: '匯入失敗',
          execute: '執行失敗',
          pause: '暫停失敗',
          resume: '恢復失敗'
        }
      },

      // 詳細頁面
      detail: {
        title: '任務詳情'
      }
    }
  }
} 