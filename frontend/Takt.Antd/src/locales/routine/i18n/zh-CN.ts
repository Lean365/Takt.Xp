export default {
  routine: {
    i18n: {
      translations: {
        title: '翻译管理',
        fields: {
        transType: {
          label: '翻译类别',
          placeholder: '请选择翻译类别',
          validation: {
            required: '请选择翻译类别'
          }
        },
        transKey: {
          label: '国际化标识',
          placeholder: '请输入国际化标识',
          validation: {
            required: '国际化标识不能为空',
            maxLength: '国际化标识长度不能超过200个字符',
            pattern: '国际化标识格式必须为xxx:xxx:xxx，只能包含小写字母和冒号，前缀部分不能包含数字',
            length: '国际化标识长度必须在1-200个字符之间',
            format: '国际化标识格式必须为xxx:xxx:xxx，只能包含小写字母和冒号，前缀部分不能包含数字',
            invalid: '请输入有效的国际化标识'
          }
        },
        transValue: {
          label: '翻译值',
          placeholder: '请输入翻译值',
          validation: {
            required: '翻译值不能为空',
            maxLength: '翻译值长度不能超过1000个字符',
            pattern: '翻译值格式不正确',
            length: '翻译值长度必须在1-1000个字符之间',
            format: '翻译值格式不正确',
            invalid: '请输入有效的翻译值'
          }
        },
        langCode: {
          label: '语言代码',
          placeholder: '请选择语言代码',
          validation: {
            required: '请选择语言代码'
          }
        },
        i18nStatus: {
          label: '状态',
          placeholder: '请选择状态',
          validation: {
            required: '请选择状态'
          }
        },
        orderNum: {
          label: '排序号',
          placeholder: '请输入排序号',
          validation: {
            required: '排序号不能为空',
            format: '排序号必须是数字',
            invalid: '请输入有效的排序号'
          }
        }
        },

        // 消息提示
        messages: {
          confirmDelete: '是否确认删除选中的翻译？',
          deleteSuccess: '翻译删除成功',
          deleteFailed: '翻译删除失败',
          saveSuccess: '翻译信息保存成功',
          saveFailed: '翻译信息保存失败',
          importSuccess: '翻译导入成功',
          importFailed: '翻译导入失败',
          exportSuccess: '翻译导出成功',
          exportFailed: '翻译导出失败',
          getDataFailed: '获取翻译数据失败',
          createTranslation: '创建翻译',
          updateTranslation: '更新翻译'
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
          createTranslation: '创建翻译',
          updateTranslation: '更新翻译'
        },

        // 导入提示
        importTips: 'Excel工作表名称必须为 "Translation"',

        // 导入导出
        import: {
          title: '导入翻译',
          template: '下载模板',
          success: '导入成功',
          failed: '导入失败',
          fileName: '翻译数据'
        },
        export: {
          title: '导出翻译',
          fileName: '翻译数据',
          success: '导出成功',
          failed: '导出失败'
        },
        template: {
          fileName: '翻译导入模板',
          downloadFailed: '下载模板失败'
        }
      },
      languages: {
        title: '语言管理',
        fields: {
          langCode: {
            label: '语言代码',
            placeholder: '请输入语言代码',
            validation: {
              required: '请输入语言代码',
              maxLength: '语言代码长度不能超过50个字符',
              pattern: '语言代码格式不正确',
              length: '语言代码长度必须在1-50个字符之间',
              format: '语言代码格式不正确',
              invalid: '请输入有效的语言代码'
            }
          },
          langName: {
            label: '语言名称',
            placeholder: '请输入语言名称',
            validation: {
              required: '语言名称不能为空',
              maxLength: '语言名称长度不能超过100个字符',
              pattern: '语言名称格式不正确',
              length: '语言名称长度必须在1-100个字符之间',
              format: '语言名称格式不正确',
              invalid: '请输入有效的语言名称'
            }
          },
          langIcon: {
            label: '语言图标',
            placeholder: '请输入语言图标',
            validation: {
              maxLength: '语言图标长度不能超过100个字符',
              pattern: '语言图标格式不正确',
              length: '语言图标长度必须在1-100个字符之间',
              format: '图标格式不正确',
              invalid: '请输入有效的图标名称'
            }
          },
          isBuiltin: {
            label: '是否内置',
            placeholder: '请选择是否内置',
            validation: {
              required: '请选择是否内置'
            }
          },
          isDefault: {
            label: '是否默认',
            placeholder: '请选择是否默认',
            validation: {
              required: '请选择是否默认'
            }
          },
          i18nStatus: {
            label: '状态',
            placeholder: '请选择状态',
            validation: {
              required: '请选择状态'
            }
          },
          orderNum: {
            label: '排序号',
            placeholder: '请输入排序号',
            validation: {
              required: '排序号不能为空',
              format: '排序号必须是数字',
              invalid: '请输入有效的排序号'
            }
          },
        },

        // 消息提示
        messages: {
          confirmDelete: '是否确认删除选中的语言？',
          deleteSuccess: '语言删除成功',
          deleteFailed: '语言删除失败',
          saveSuccess: '语言信息保存成功',
          saveFailed: '语言信息保存失败',
          importSuccess: '语言导入成功',
          importFailed: '语言导入失败',
          exportSuccess: '语言导出成功',
          exportFailed: '语言导出失败',
          getDataFailed: '获取语言数据失败',
          createLanguage: '创建语言',
          updateLanguage: '更新语言'
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
          createLanguage: '创建语言',
          updateLanguage: '更新语言'
        },

        // 导入提示
        importTips: 'Excel工作表名称必须为 "Language"',

        // 导入导出
        import: {
          title: '导入语言',
          template: '下载模板',
          success: '导入成功',
          failed: '导入失败',
          fileName: '语言数据'
        },
        export: {
          title: '导出语言',
          fileName: '语言数据',
          success: '导出成功',
          failed: '导出失败'
        },
        template: {
          fileName: '语言导入模板',
          downloadFailed: '下载模板失败'
        }
      }
    }
  }
}