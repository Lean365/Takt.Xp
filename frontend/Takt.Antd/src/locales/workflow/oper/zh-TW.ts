export default {
  workflow: {
    history: {
      title: '工作流程歷史',
      list: {
        title: '工作流程歷史列表',
        search: {
          name: '工作流程名稱',
          key: '工作流程識別碼',
          version: '版本號',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間'
        },
        table: {
          name: '工作流程名稱',
          key: '工作流程識別碼',
          version: '版本號',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間',
          duration: '持續時間',
          actions: '操作'
        },
        actions: {
          view: '查看',
          delete: '刪除',
          export: '匯出',
          import: '匯入',
          refresh: '重新整理'
        },
        status: {
          running: '執行中',
          completed: '已完成',
          terminated: '已終止',
          failed: '已失敗'
        }
      },
      detail: {
        title: '工作流程歷史詳情',
        basic: {
          title: '基本資訊',
          name: '工作流程名稱',
          key: '工作流程識別碼',
          version: '版本號',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間',
          duration: '持續時間'
        },
        nodes: {
          title: '節點資訊',
          name: '節點名稱',
          type: '節點類型',
          status: '狀態',
          startTime: '開始時間',
          endTime: '結束時間',
          duration: '持續時間',
          input: '輸入',
          output: '輸出',
          error: '錯誤'
        },
        actions: {
          back: '返回'
        }
      }
    }
  }
} 