export default {
    generator: {
      config: {
        title: '代碼生成配置',  
        fields: {
            genConfigName: '配置名稱',
            author: '作者',
            moduleName: '模組名稱',
            projectName: '專案名稱',
            businessName: '業務名稱',
            functionName: '功能名稱',
            genMethod: '生成方式',
            genTplType: '模板類型',
            genPath: '路徑',
            options: '選項',
            status: '狀態',
            dateRange: '時間',
        },
        placeholder: {
            genConfigName: '請輸入配置名稱',
            author: '請輸入作者',
            moduleName: '請輸入模組名稱',
            projectName: '請輸入專案名稱',
            businessName: '請輸入業務名稱',
            functionName: '請輸入功能名稱',
            genMethod: '請選擇生成方式',
            genTplType: '請選擇模板類型',
            genPath: '請輸入路徑',
            options: '請輸入選項',
            status: '請選擇狀態',
            dateRange: '請選擇時間範圍',
            validation: {
                length: '{{field}}長度不能超過{{length}}',
                required: '請輸入{{field}}',
                minlength: '{{field}}長度不能小於{{min}}',
                maxlength: '{{field}}長度不能大於{{max}}',
                pattern: '{{field}}格式不正確',
            }
        },
        messages: {
            success: '操作成功',
            error: '操作失敗',
            warning: '操作警告',
            info: '操作資訊',
        }
    }
} }