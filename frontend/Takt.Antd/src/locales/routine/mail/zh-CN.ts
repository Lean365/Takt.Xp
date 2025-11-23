export default {
  routine: {
    mail: {
      // 基本信息
      mailId: '邮件ID',
      mailType: '邮件类型',
      mailSubject: '邮件主题',
      mailContent: '邮件内容',
      mailStatus: '邮件状态',
      mailParams: '邮件参数',
      createTime: '创建时间',
      updateTime: '更新时间',

      // 操作按钮
      add: '新增邮件',
      edit: '编辑邮件',
      delete: '删除邮件',
      batchDelete: '批量删除',
      export: '导出',
      import: '导入',
      downloadTemplate: '下载模板',
      preview: '预览',
      send: '发送',

      // 表单占位符
      placeholder: {
        mailId: '请输入邮件ID',
        mailType: '请选择邮件类型',
        mailSubject: '请输入邮件主题',
        mailContent: '请输入邮件内容',
        mailStatus: '请选择邮件状态',
        mailParams: '请输入邮件参数',
        startTime: '开始时间',
        endTime: '结束时间'
      },

      // 表单验证
      validation: {
        mailId: {
          required: '请输入邮件ID',
          maxLength: '邮件ID不能超过100个字符'
        },
        mailType: {
          required: '请选择邮件类型',
          maxLength: '邮件类型不能超过50个字符'
        },
        mailSubject: {
          required: '请输入邮件主题',
          maxLength: '邮件主题不能超过200个字符'
        },
        mailContent: {
          required: '请输入邮件内容',
          maxLength: '邮件内容不能超过4000个字符'
        },
        mailStatus: {
          required: '请选择邮件状态'
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
        title: '邮件详情'
      }
    }
  }
} 