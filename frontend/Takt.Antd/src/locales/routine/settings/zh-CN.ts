export default {
  routine: {
    settings: {
      title: '系统设置',
      fields: {
        settingsName: {
          label: '设置名称',
          placeholder: '请输入设置名称',
          validation: {
            required: '设置名称不能为空',
            maxLength: '设置名称长度不能超过100个字符',
            pattern: '设置名称格式不正确',
            length: '设置名称长度必须在1-100个字符之间',
            format: '设置名称格式不正确',
            invalid: '请输入有效的设置名称'
          }
        },
        settingsKey: {
          label: '设置键名',
          placeholder: '请输入设置键名',
            validation: {
              required: '设置键名不能为空',
              maxLength: '设置键名长度不能超过100个字符',
              pattern: '设置键名必须以字母开头，只能包含字母、数字、下划线、点和冒号',
              length: '设置键名长度必须在1-100个字符之间',
              format: '设置键名格式不正确',
              invalid: '请输入有效的设置键名'
            }
        },
        settingsValue: {
          label: '设置键值',
          placeholder: '请输入设置键值',
            validation: {
              required: '设置键值不能为空',
              maxLength: '设置键值长度不能超过500个字符',
              pattern: '设置键值格式不正确',
              length: '设置键值长度必须在1-500个字符之间',
              format: '设置键值格式不正确',
              invalid: '请输入有效的设置键值'
            }
        },
        settingsType: {
          label: '设置类别',
          placeholder: '请选择设置类别',
            validation: {
              required: '请选择设置类别'
            }
        },
        isBuiltin: {
          label: '系统内置',
          placeholder: '请选择是否系统内置',
            validation: {
              required: '请选择是否系统内置'
            }
        },
        isEncrypted: {
          label: '是否加密',
          placeholder: '请选择是否加密',
            validation: {
              required: '请选择是否加密'
            }
        },
        orderNum: {
          label: '排序号',
          placeholder: '请输入排序号',
            validation: {
              required: '排序号不能为空',
              format: '排序号必须是数字',
              invalid: '排序号必须在0-9999之间'
            }
        },
        status: {
          label: '状态',
          placeholder: '请选择状态',
            validation: {
              required: '请选择状态'
            }
        }
      },

      // 消息提示
      messages: {
        confirmDelete: '是否确认删除选中的设置？',
        deleteSuccess: '设置删除成功',
        deleteFailed: '设置删除失败',
        saveSuccess: '设置信息保存成功',
        saveFailed: '设置信息保存失败',
        importSuccess: '设置导入成功',
        importFailed: '设置导入失败',
        exportSuccess: '设置导出成功',
        exportFailed: '设置导出失败',
        getDataFailed: '获取设置数据失败',
        createSetting: '创建设置',
        updateSetting: '更新设置',
        refreshSuccess: '缓存更新成功'
      },

      // 表格显示文本
      tableText: {
        queryParams: '查询参数',
        importResponseData: '导入响应数据',
        parsedData: '解析后的数据',
        exportFailed: '导出失败',
        downloadTemplateFailed: '下载模板失败',
        rowClicked: '行点击',
        toggleFullscreenState: '切换全屏状态',
        getOptionsFailed: '获取选项数据失败',
        createSetting: '创建设置',
        updateSetting: '更新设置'
      },

      // 导入提示
      importTips: 'Excel工作表名称必须为 "Settings"',

      // 导入导出
      import: {
        title: '导入设置',
        template: '下载模板',
        success: '导入成功',
        failed: '导入失败',
        fileName: '设置数据'
      },
      export: {
        title: '导出设置',
        fileName: '设置数据',
        success: '导出成功',
        failed: '导出失败'
      },
      template: {
        fileName: '设置导入模板',
        downloadFailed: '下载模板失败'
      }
    }
  }
}