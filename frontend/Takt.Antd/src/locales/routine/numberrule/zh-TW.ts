export default {
  routine: {
    core: {
      numberrule: {
        // 頁面標題
        title: '編號規則管理',
        
        // 標籤頁
        tabs: {
          basicInfo: '基本資訊',
          numberConfig: '編號配置',
          advancedConfig: '進階配置',
          otherInfo: '其他資訊'
        },

        // 欄位標籤
        fields: {
          companyCode: {
            label: '公司代碼',
            placeholder: '請輸入公司代碼',
            required: '請輸入公司代碼',
            length: '公司代碼長度必須在1-20個字元之間'
          },
          numberRuleName: {
            label: '編號規則名稱',
            placeholder: '請輸入編號規則名稱',
            required: '請輸入編號規則名稱',
            length: '編號規則名稱長度必須在1-100個字元之間'
          },
          numberRuleCode: {
            label: '編號規則代碼',
            placeholder: '請輸入編號規則代碼',
            required: '請輸入編號規則代碼',
            length: '編號規則代碼長度必須在1-50個字元之間'
          },
          deptCode: {
            label: '部門代碼',
            placeholder: '請輸入部門代碼',
            required: '請輸入部門代碼',
            length: '部門代碼長度必須在1-20個字元之間'
          },
          numberRuleShortCode: {
            label: '編號規則簡稱',
            placeholder: '請輸入編號規則簡稱',
            required: '請輸入編號規則簡稱',
            length: '編號規則簡稱長度必須在1-4個字元之間'
          },
          numberRuleType: {
            label: '編號規則類型',
            placeholder: '請選擇編號規則類型',
            required: '請選擇編號規則類型'
          },
          status: {
            label: '狀態',
            placeholder: '請選擇狀態',
            required: '請選擇狀態'
          },
          numberRuleDescription: {
            label: '編號規則描述',
            placeholder: '請輸入編號規則描述'
          },
          numberPrefix: {
            label: '編號前綴',
            placeholder: '請輸入編號前綴'
          },
          numberSuffix: {
            label: '編號後綴',
            placeholder: '請輸入編號後綴'
          },
          dateFormat: {
            label: '日期格式',
            placeholder: '請選擇日期格式',
            required: '請選擇日期格式'
          },
          sequenceLength: {
            label: '序號長度',
            placeholder: '請輸入序號長度',
            required: '請輸入序號長度'
          },
          sequenceStart: {
            label: '序號起始值',
            placeholder: '請輸入序號起始值',
            required: '請輸入序號起始值'
          },
          sequenceStep: {
            label: '序號步長',
            placeholder: '請輸入序號步長',
            required: '請輸入序號步長'
          },
          currentSequence: {
            label: '當前序號',
            placeholder: '請輸入當前序號'
          },
          sequenceResetRule: {
            label: '序號重置規則',
            placeholder: '請選擇序號重置規則'
          },
          includeCompanyCode: {
            label: '包含公司代碼',
            placeholder: '請選擇是否包含公司代碼'
          },
          includeDepartmentCode: {
            label: '包含部門代碼',
            placeholder: '請選擇是否包含部門代碼'
          },
          includeRevisionNumber: {
            label: '包含修訂號',
            placeholder: '請選擇是否包含修訂號'
          },
          includeYear: {
            label: '包含年份',
            placeholder: '請選擇是否包含年份'
          },
          includeMonth: {
            label: '包含月份',
            placeholder: '請選擇是否包含月份'
          },
          includeDay: {
            label: '包含日期',
            placeholder: '請選擇是否包含日期'
          },
          allowDuplicate: {
            label: '允許重複',
            placeholder: '請選擇是否允許重複'
          },
          duplicateCheckScope: {
            label: '重複檢查範圍',
            placeholder: '請選擇重複檢查範圍'
          },
          orderNum: {
            label: '排序號',
            placeholder: '請輸入排序號'
          }
        },

        // 操作按鈕
        actions: {
          add: '新增編號規則',
          edit: '編輯編號規則',
          delete: '刪除編號規則',
          batchDelete: '批量刪除',
          export: '匯出',
          import: '匯入',
          downloadTemplate: '下載範本',
          preview: '預覽',
          generate: '產生編號',
          validate: '驗證規則'
        },

        // 表單佔位符
        placeholder: {
          search: '請輸入編號規則名稱或代碼',
          selectAll: '全選',
          clear: '清空'
        },

        // 操作結果訊息
        message: {
          success: {
            add: '新增編號規則成功',
            edit: '編輯編號規則成功',
            delete: '刪除編號規則成功',
            batchDelete: '批量刪除成功',
            export: '匯出成功',
            import: '匯入成功',
            generate: '產生編號成功',
            validate: '驗證通過'
          },
          failed: {
            add: '新增編號規則失敗',
            edit: '編輯編號規則失敗',
            delete: '刪除編號規則失敗',
            batchDelete: '批量刪除失敗',
            export: '匯出失敗',
            import: '匯入失敗',
            generate: '產生編號失敗',
            validate: '驗證失敗'
          },
          confirm: {
            delete: '確定要刪除選中的編號規則嗎？',
            batchDelete: '確定要批量刪除選中的編號規則嗎？'
          }
        },

        // 詳情頁面
        detail: {
          title: '編號規則詳情',
          basicInfo: '基本資訊',
          numberConfig: '編號配置',
          advancedConfig: '進階配置',
          otherInfo: '其他資訊'
        },

        // 表格欄位標題
        columns: {
          numberRuleName: '編號規則名稱',
          numberRuleCode: '編號規則代碼',
          numberRuleShortCode: '編號規則簡稱',
          numberRuleType: '編號規則類型',
          deptCode: '部門代碼',
          status: '狀態',
          createTime: '建立時間',
          updateTime: '更新時間',
          remark: '備註',
          actions: '操作'
        },

        // 編號規則類型
        types: {
          daily: '日常事務',
          hr: '人力資源',
          finance: '財務核算',
          logistics: '後勤管理',
          production: '生產管理',
          workflow: '工作流程'
        },

        // 狀態
        status: {
          normal: '正常',
          disabled: '停用'
        },

        // 序號重置規則
        resetRules: {
          none: '不重置',
          yearly: '按年重置',
          monthly: '按月重置',
          daily: '按日重置'
        },

        // 重複檢查範圍
        checkScopes: {
          global: '全域',
          yearly: '按年',
          monthly: '按月',
          daily: '按日'
        },

        // 日期格式選項
        dateFormats: {
          yyyy: 'yyyy',
          yyyyMM: 'yyyyMM',
          yyyyMMdd: 'yyyyMMdd',
          yyyyMMddHH: 'yyyyMMddHH',
          yyyyMMddHHmm: 'yyyyMMddHHmm',
          yyyyMMddHHmmss: 'yyyyMMddHHmmss'
        }
      }
    }
  }
}
