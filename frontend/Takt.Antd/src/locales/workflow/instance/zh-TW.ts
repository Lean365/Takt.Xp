export default {
  workflow: {
    instance: {
      title: '工作流程實例',
      list: {
        title: '工作流程實例列表',
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
          terminate: '終止',
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
      form: {
        title: {
          create: '新建工作流程實例',
          import: '匯入工作流程實例'
        },
        fields: {
          workflowDefinitionId: '工作流程定義',
          variables: '變數設定'
        },
        rules: {
          workflowDefinitionId: {
            required: '請選擇工作流程定義'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      },
      detail: {
        title: '工作流程實例詳情',
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
      },
      terminate: {
        title: '終止工作流程實例',
        confirm: '確定要終止此實例嗎？',
        reason: '終止原因',
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      }
    }
  }
} 