export default {
    generator: {
      template: {
        title: '代碼生成模板',  
        fields: {
            templateName: '模板名稱',
            templateOrmType: 'ORM框架',
            templateCodeType: '代碼類型',
            templateCategory: '模板分類',
            templateLanguage: '編程語言',
            templateVersion: '版本號',
            fileName: '檔案名稱',
            templateContent: '模板內容',
            status: '狀態',
            remark: '備註',
        },
        placeholder: {
            templateName: '請輸入模板名稱',
            templateOrmType: '請選擇ORM框架',
            templateCodeType: '請選擇代碼類型',
            templateCategory: '請選擇模板分類',
            templateLanguage: '請選擇編程語言',
            templateVersion: '請輸入版本號',
            fileName: '請輸入檔案名稱',
            templateContent: '請輸入模板內容',
            remark: '請輸入備註',
            validation: {
                required: '請輸入{field}',
                length: '{field}長度不能超過{length}個字元',
                minLength: '{field}長度不能少於{min}個字元',
                pascalCase: '{field}必須使用帕斯卡命名法（首字母大寫，只允許字母）'
            }
        },
        dialog: {
            create: '新增模板',
            update: '修改模板'
        },
        messages: {
            success: '操作成功',
            error: '操作失敗',
            warning: '操作警告',
            info: '操作資訊',
            confirm: '確定',
        },
    }
} }