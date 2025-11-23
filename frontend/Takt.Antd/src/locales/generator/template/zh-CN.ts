export default {
    generator: {
      template: {
        title: '代码生成模板',  
        fields: {
            templateName: '模板名称',
            templateOrmType: 'ORM框架',
            templateCodeType: '代码类型',
            templateCategory: '模板分类',
            templateLanguage: '编程语言',
            templateVersion: '版本号',
            fileName: '文件名',
            templateContent: '模板内容',
            status: '状态',
            remark: '备注',
        },
        placeholder: {
            templateName: '请输入模板名称',
            templateOrmType: '请选择ORM框架',
            templateCodeType: '请选择代码类型',
            templateCategory: '请选择模板分类',
            templateLanguage: '请选择编程语言',
            templateVersion: '请输入版本号',
            fileName: '请输入文件名',
            templateContent: '请输入模板内容',
            remark: '请输入备注',
            validation: {
                required: '请输入{field}',
                length: '{field}长度不能超过{length}个字符',
                minLength: '{field}长度不能少于{min}个字符',
                pascalCase: '{field}必须使用帕斯卡命名法（首字母大写，只允许字母）'
            }
        },
        dialog: {
            create: '新增模板',
            update: '修改模板'
        },
        messages: {
            success: '操作成功',
            error: '操作失败',
            warning: '操作警告',
            info: '操作信息',
            confirm: '确定',
        },
    }
}
}

