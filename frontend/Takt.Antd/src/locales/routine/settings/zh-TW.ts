export default {
  // 系統配置
  config: {
    // 基礎信息
    name: '配置名稱',
    key: '配置鍵名',
    value: '配置鍵值',
    builtin: '系統內置',
    order: '排序',
    remark: '備註',
    status: '狀態',
    // 時間信息
    createTime: '創建時間',
    createBy: '創建者',
    updateTime: '更新時間',
    updateBy: '更新者',
    // 操作
    operation: '操作',
    // 提示信息
    placeholder: {
      name: '請輸入配置名稱',
      key: '請輸入配置鍵名',
      value: '請輸入配置鍵值',
      builtin: '請選擇是否系統內置',
      order: '請輸入排序號',
      remark: '請輸入備註',
      status: '請選擇狀態'
    },
    // 驗證信息
    validation: {
      name: {
        required: '請輸入配置名稱',
        maxLength: '配置名稱長度不能超過100個字符'
      },
      key: {
        required: '請輸入配置鍵名',
        maxLength: '配置鍵名長度不能超過100個字符',
        pattern: '配置鍵名只能包含字母、數字、下劃線、點號和冒號,且必須以字母開頭'
      },
      value: {
        required: '請輸入配置鍵值',
        maxLength: '配置鍵值長度不能超過500個字符'
      },
      builtin: {
        required: '請選擇是否系統內置'
      },
      order: {
        required: '請輸入排序號',
        range: '排序號必須在0-9999之間'
      },
      status: {
        required: '請選擇狀態'
      }
    },
    // 操作結果
    message: {
      addSuccess: '新增配置成功',
      editSuccess: '修改配置成功',
      deleteSuccess: '刪除配置成功',
      deleteConfirm: '是否確認刪除配置"{name}"?',
      exportSuccess: '導出配置成功',
      importSuccess: '導入配置成功',
      refreshSuccess: '刷新緩存成功'
    }
  }
} 