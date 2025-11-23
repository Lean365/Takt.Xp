export default {
  routine: {
    mailtmpl: {
      // 基本信息
      templateName: '模板名称',
      templateType: '模板类型',
      templateSubject: '模板主题',
      templateContent: '模板内容',
      templateStatus: '模板状态',
      templateParams: '模板参数',
      createTime: '创建时间',
      updateTime: '更新时间',

      // 操作按钮
      add: '新增模板',
      edit: '编辑模板',
      delete: '删除模板',
      batchDelete: '批量删除',
      export: '导出',
      import: '导入',
      downloadTemplate: '下载模板',
      preview: '预览',
      send: '发送',

      // 表单占位符
      placeholder: {
        templateName: '请输入模板名称',
        templateType: '请选择模板类型',
        templateSubject: '请输入模板主题',
        templateContent: '请输入模板内容',
        templateStatus: '请选择模板状态',
        templateParams: '请输入模板参数',
        startTime: '开始时间',
        endTime: '结束时间'
      },

      // 表单验证
      validation: {
        templateName: {
          required: '请输入模板名称',
          maxLength: '模板名称不能超过100个字符'
        },
        templateType: {
          required: '请选择模板类型',
          maxLength: '模板类型不能超过50个字符'
        },
        templateSubject: {
          required: '请输入模板主题',
          maxLength: '模板主题不能超过200个字符'
        },
        templateContent: {
          required: '请输入模板内容',
          maxLength: '模板内容不能超过4000个字符'
        },
        templateStatus: {
          required: '请选择模板状态'
        }
      },

      // 操作结果
      message: {
        success: {
          add: '新增成功',
          edit: '编辑成功',
          delete: '删除成功',
          batchDelete: '批量删除成功',
          export: '导出成功',
          import: '导入成功',
          send: '发送成功'
        },
        failed: {
          add: '新增失败',
          edit: '编辑失败',
          delete: '删除失败',
          batchDelete: '批量删除失败',
          export: '导出失败',
          import: '导入失败',
          send: '发送失败'
        }
      },

      // 详情页面
      detail: {
        title: '邮件模板详情'
      }
    }
  }
} 