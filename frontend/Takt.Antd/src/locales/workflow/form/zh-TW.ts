export default {
  workflow: {
    definition: {
      title: '工作流程定義',
      list: {
        title: '工作流程定義列表',
        search: {
          workflowName: '工作流程名稱',
          workflowCategory: '工作流程類型',
          status: '狀態'
        },
        table: {
          workflowId: '工作流程ID',
          workflowName: '工作流程名稱',
          workflowCategory: '工作流程類型',
          status: '狀態',
          createTime: '創建時間',
          updateTime: '更新時間',
          actions: '操作'
        },
        actions: {
          view: '查看',
          edit: '編輯',
          delete: '刪除',
          refresh: '更新',
          import: '導入',
          export: '導出'
        },
        status: {
          normal: '正常',
          disable: '停用'
        }
      },
      form: {
        title: {
          create: '創建工作流程定義',
          edit: '編輯工作流程定義'
        },
        fields: {
          workflowName: '工作流程名稱',
          workflowCategory: '工作流程類型',
          workflowVersion: '版本',
          status: '狀態',
          remark: '備註'
        },
        rules: {
          workflowName: {
            required: '請輸入工作流程名稱'
          },
          workflowCategory: {
            required: '請選擇工作流程類型'
          },
          status: {
            required: '請選擇狀態'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        }
      },
      detail: {
        title: '工作流程定義詳情',
        basic: {
          title: '基本信息',
          workflowName: '工作流程名稱',
          workflowCategory: '工作流程類型',
          workflowVersion: '版本',
          status: '狀態',
          remark: '備註'
        },
        node: {
          title: '節點信息',
          nodeName: '節點名稱',
          nodeType: '節點類型',
          assigneeType: '處理人類型',
          formType: '表單類型'
        },
        form: {
          title: '表單信息',
          fieldName: '字段名稱',
          fieldType: '字段類型',
          required: '是否必填',
          remark: '備註'
        }
      }
    }
  }
} 