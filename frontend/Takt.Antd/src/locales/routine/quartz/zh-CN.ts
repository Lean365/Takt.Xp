export default {
  routine: {
    quartz: {
      // 基本信息
      jobId: '任务ID',
      jobName: '任务名称',
      jobGroup: '任务组',
      jobClass: '任务类',
      jobMethod: '任务方法',
      jobParams: '任务参数',
      cronExpression: 'Cron表达式',
      jobStatus: '任务状态',
      createTime: '创建时间',
      updateTime: '更新时间',

      // 操作按钮
      add: '新增任务',
      edit: '编辑任务',
      delete: '删除任务',
      batchDelete: '批量删除',
      export: '导出',
      import: '导入',
      downloadTemplate: '下载模板',
      start: '启动',
      stop: '停止',
      pause: '暂停',
      resume: '恢复',
      run: '立即执行',

      // 表单占位符
      placeholder: {
        jobId: '请输入任务ID',
        jobName: '请输入任务名称',
        jobGroup: '请输入任务组',
        jobClass: '请输入任务类',
        jobMethod: '请输入任务方法',
        jobParams: '请输入任务参数',
        cronExpression: '请输入Cron表达式',
        jobStatus: '请选择任务状态',
        startTime: '开始时间',
        endTime: '结束时间'
      },

      // 表单验证
      validation: {
        jobId: {
          required: '请输入任务ID',
          maxLength: '任务ID不能超过100个字符'
        },
        jobName: {
          required: '请输入任务名称',
          maxLength: '任务名称不能超过50个字符'
        },
        jobGroup: {
          required: '请输入任务组',
          maxLength: '任务组不能超过50个字符'
        },
        jobClass: {
          required: '请输入任务类',
          maxLength: '任务类不能超过200个字符'
        },
        jobMethod: {
          required: '请输入任务方法',
          maxLength: '任务方法不能超过50个字符'
        },
        cronExpression: {
          required: '请输入Cron表达式',
          maxLength: 'Cron表达式不能超过50个字符'
        },
        jobStatus: {
          required: '请选择任务状态'
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
          start: '启动成功',
          stop: '停止成功',
          pause: '暂停成功',
          resume: '恢复成功',
          run: '执行成功'
        },
        failed: {
          add: '新增失败',
          edit: '编辑失败',
          delete: '删除失败',
          batchDelete: '批量删除失败',
          export: '导出失败',
          import: '导入失败',
          start: '启动失败',
          stop: '停止失败',
          pause: '暂停失败',
          resume: '恢复失败',
          run: '执行失败'
        }
      },

      // 详情页面
      detail: {
        title: '定时任务详情'
      }
    }
  }
} 