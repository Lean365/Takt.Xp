export default {
  routine: {
    notice: {
      // 基本信息
      noticeId: '通知ID',
      noticeType: '通知类型',
      noticeTitle: '通知标题',
      noticeContent: '通知内容',
      noticeStatus: '通知状态',
      noticeParams: '通知参数',
      createTime: '创建时间',
      updateTime: '更新时间',

      // 操作按钮
      add: '新增通知',
      edit: '编辑通知',
      delete: '删除通知',
      batchDelete: '批量删除',
      export: '导出',
      import: '导入',
      downloadTemplate: '下载模板',
      preview: '预览',
      send: '发送',

      // 表单占位符
      placeholder: {
        noticeId: '请输入通知ID',
        noticeType: '请选择通知类型',
        noticeTitle: '请输入通知标题',
        noticeContent: '请输入通知内容',
        noticeStatus: '请选择通知状态',
        noticeParams: '请输入通知参数',
        startTime: '开始时间',
        endTime: '结束时间'
      },

      // 表单验证
      validation: {
        noticeId: {
          required: '请输入通知ID',
          maxLength: '通知ID不能超过100个字符'
        },
        noticeType: {
          required: '请选择通知类型',
          maxLength: '通知类型不能超过50个字符'
        },
        noticeTitle: {
          required: '请输入通知标题',
          maxLength: '通知标题不能超过200个字符'
        },
        noticeContent: {
          required: '请输入通知内容',
          maxLength: '通知内容不能超过4000个字符'
        },
        noticeStatus: {
          required: '请选择通知状态'
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
        title: '通知详情'
      }
    }
  }
}