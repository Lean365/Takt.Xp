export default {
    generator: {
      config: {
        title: '代码生成配置',  
        fields: {
            genConfigName: '配置名称',
            author: '作者',
            moduleName: '模块名称',
            projectName: '项目名称',
            businessName: '业务名称',
            functionName: '功能名称',
            genMethod: '生成方式',
            genTplType: '模板类型',
            genPath: '路径',
            options: '选项',
            status: '状态',
            dateRange: '时间',
        },
        placeholder: {
            genConfigName: '请输入配置名称',
            author: '请输入作者',
            moduleName: '请输入模块名称',
            projectName: '请输入项目名称',
            businessName: '请输入业务名称',
            functionName: '请输入功能名称',
            genMethod: '请选择生成方式',
            genTplType: '请选择模板类型',
            genPath: '请输入路径',
            options: '请输入选项',
            status: '请选择状态',
            dateRange: '请选择时间范围',
            validation: {
                length: '{{field}}长度不能超过{{length}}',
                required: '请输入{{field}}',
                minlength: '{{field}}长度不能小于{{min}}',
                maxlength: '{{field}}长度不能大于{{max}}',
                pattern: '{{field}}格式不正确',
            }
        },
        messages: {
            success: '操作成功',
            error: '操作失败',
            warning: '操作警告',
            info: '操作信息',
        }
    }
}
}


