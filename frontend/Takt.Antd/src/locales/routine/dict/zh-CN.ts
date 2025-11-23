export default {
  routine: {
    dict: {
      dictTypes: {
        title: '字典类型',
        fields: {
          dictName: {
            label: '字典名称',
            placeholder: '请输入字典名称',
            validation: {
              required: '字典名称不能为空',
              maxLength: '字典名称长度不能超过100个字符',
              pattern: '字典名称只能包含字母、数字、中文、下划线和连字符，长度在2-100之间',
              length: '字典名称长度必须在2-100个字符之间',
              format: '字典名称格式不正确',
              invalid: '请输入有效的字典名称'
            }
          },
          dictType: {
            label: '字典类型',
            placeholder: '请输入字典类型',
            validation: {
              required: '字典类型不能为空',
              maxLength: '字典类型长度不能超过100个字符',
              pattern: '字典类型必须以字母开头，只能包含字母、数字和下划线，长度在2-100之间',
              length: '字典类型长度必须在2-100个字符之间',
              format: '字典类型必须由三部分组成，用下划线分隔',
              first: '第一部分必须为3位小写字母，不允许重叠字符（如aa、aaa等），不允许特殊字符，且不允许与其他部分有三个或以上重复字符',
              second: '第二部分必须为3-10位小写字母，不允许重叠字符（如aa、aaa等），不允许特殊字符，且不允许与其他部分有三个或以上重复字符',
              third: '第三部分必须以小写字母开头，可包含数字，不允许重叠字符（如aa、aaa等），不允许特殊字符，总长度3-10位，且不允许与其他部分有三个或以上重复字符',
              invalid: '请输入有效的字典类型'
            }
          },
          dictSource: {
            label: '字典数据源',
            placeholder: '请选择字典数据源',
            validation: {
              required: '请选择字典数据源'
            }
          },
          isBuiltin: {
            label: '是否内置',
            placeholder: '请选择是否内置',
            validation: {
              required: '请选择是否内置'
            }
          },
          sqlScript: {
            label: 'SQL脚本',
            placeholder: '请输入SQL脚本',
            validation: {
              format: 'SQL脚本格式不正确',
              invalid: '请输入有效的SQL脚本'
            },
            sqlHelp: {
              title: '标准SQL示例：',
              description: '字段说明：',
              example: `SELECT
      name as label,    -- 显示文本
      code as value,    -- 键值
      css_class,        -- CSS类名
      list_class,       -- 列表类名
      status,          -- 状态(0正常 1停用)
      ext_label,       -- 扩展标签
      ext_value,       -- 扩展值
      trans_key,       -- 翻译键
      order_num,       -- 排序号
      remark           -- 备注
    FROM your_table 
    WHERE status = 0 
    ORDER BY order_num`,
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
          status: {
            
            label: '状态',
            placeholder: '请选择状态',
            validation: {
              required: '状态不能为空'
            }
          },
        },

        // 消息提示
        messages: {
          confirmDelete: '是否确认删除选中的字典类型？',
          deleteSuccess: '字典类型删除成功',
          deleteFailed: '字典类型删除失败',
          saveSuccess: '字典类型信息保存成功',
          saveFailed: '字典类型信息保存失败',
          importSuccess: '字典类型导入成功',
          importFailed: '字典类型导入失败',
          exportSuccess: '字典类型导出成功',
          exportFailed: '字典类型导出失败',
          getDataFailed: '获取字典类型数据失败',
          createDictType: '创建字典类型',
          updateDictType: '更新字典类型'
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
          createDictType: '创建字典类型',
          updateDictType: '更新字典类型'
        },

        // 导入提示
        importTips: 'Excel工作表名称必须为 "DictType"',

        // 导入导出
        import: {
          title: '导入字典类型',
          template: '下载模板',
          success: '导入成功',
          failed: '导入失败',
          fileName: '字典类型数据'
        },
        export: {
          title: '导出字典类型',
          fileName: '字典类型数据',
          success: '导出成功',
          failed: '导出失败'
        },
        template: {
          fileName: '字典类型导入模板',
          downloadFailed: '下载模板失败'
        }
      },
      dictDatas: {
        title: '字典数据',
        fields: {
          dictType: {
            label: '字典类型',
            placeholder: '请选择字典类型',
            validation: {
              required: '请选择字典类型'
            }
          },
          dictLabel: {
            label: '字典标签',
            placeholder: '请输入字典标签',
            validation: {
              required: '字典标签不能为空',
              maxLength: '字典标签长度不能超过100个字符',
              pattern: '字典标签格式不正确',
              length: '字典标签长度必须在1-100个字符之间',
              format: '字典标签格式不正确',
              invalid: '请输入有效的字典标签'
            }
          },
          dictValue: {
            label: '字典值',
            placeholder: '请输入字典值',
            validation: {
              required: '字典值不能为空',
              maxLength: '字典值长度不能超过100个字符',
              pattern: '字典值格式不正确',
              length: '字典值长度必须在1-100个字符之间',
              format: '字典值格式不正确',
              invalid: '请输入有效的字典值'
            }
          },
          extLabel: {
            label: '扩展标签',
            placeholder: '请输入扩展标签',
            validation: {
              maxLength: '扩展标签长度不能超过100个字符',
              pattern: '扩展标签格式不正确',
              length: '扩展标签长度必须在1-100个字符之间',
              format: '扩展标签格式不正确',
              invalid: '请输入有效的扩展标签'
            }
          },
          extValue: {
            label: '扩展值',
            placeholder: '请输入扩展值',
            validation: {
              maxLength: '扩展值长度不能超过100个字符',
              pattern: '扩展值格式不正确',
              length: '扩展值长度必须在1-100个字符之间',
              format: '扩展值格式不正确',
              invalid: '请输入有效的扩展值'
            }
          },
          transKey: {
            label: '国际化标识',
            placeholder: '请输入国际化标识',
            validation: {
              maxLength: '国际化标识长度不能超过100个字符',
              pattern: '国际化标识格式必须为xxx:xxx:xxx，只能包含小写字母和冒号，前缀部分不能包含数字',
              length: '国际化标识长度必须在1-100个字符之间',
              format: '国际化标识格式必须为xxx:xxx:xxx，只能包含小写字母和冒号，前缀部分不能包含数字',
              invalid: '请输入有效的国际化标识'
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
          cssClass: {
            label: 'CSS类名',
            placeholder: '请输入CSS类名',
            validation: {
              format: 'CSS类名必须是数字',
              invalid: '请输入有效的CSS类名'
            }
          },
          listClass: {
            label: '列表类名',
            placeholder: '请输入列表类名',
            validation: {
              format: '列表类名必须是数字',
              invalid: '请输入有效的列表类名'
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
          confirmDelete: '是否确认删除选中的字典数据？',
          deleteSuccess: '字典数据删除成功',
          deleteFailed: '字典数据删除失败',
          saveSuccess: '字典数据信息保存成功',
          saveFailed: '字典数据信息保存失败',
          importSuccess: '字典数据导入成功',
          importFailed: '字典数据导入失败',
          exportSuccess: '字典数据导出成功',
          exportFailed: '字典数据导出失败',
          getDataFailed: '获取字典数据失败',
          createDictData: '创建字典数据',
          updateDictData: '更新字典数据'
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
          createDictData: '创建字典数据',
          updateDictData: '更新字典数据'
        },

        // 导入提示
        importTips: 'Excel工作表名称必须为 "DictData"',

        // 导入导出
        import: {
          title: '导入字典数据',
          template: '下载模板',
          success: '导入成功',
          failed: '导入失败',
          fileName: '字典数据'
        },
        export: {
          title: '导出字典数据',
          fileName: '字典数据',
          success: '导出成功',
          failed: '导出失败'
        },
        template: {
          fileName: '字典数据导入模板',
          downloadFailed: '下载模板失败'
        }
      }
    }
  }
}