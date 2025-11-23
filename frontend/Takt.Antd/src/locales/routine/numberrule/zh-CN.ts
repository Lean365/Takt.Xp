export default {
  routine: {
    numberrule: {
      title: '编号规则管理',
      fields: {
        companyCode: {
          label: '公司代码',
          placeholder: '请选择公司代码',
          validation: {
            required: '请选择公司代码'
          }
        },
        numberRuleName: {
          label: '编号规则名称',
          placeholder: '请输入编号规则名称',
            validation: {
              required: '请输入编号规则名称',
              maxLength: '编号规则名称长度必须在1-100个字符之间',
              pattern: '编号规则名称格式不正确',
              length: '编号规则名称长度必须在1-100个字符之间',
              format: '编号规则名称格式不正确',
              invalid: '请输入有效的编号规则名称'
            }
        },
        numberRuleCode: {
          label: '编号规则代码',
          placeholder: '请输入编号规则代码',
          validation: {
            required: '请输入编号规则代码',
            maxLength: '编号规则代码长度不能超过20个字符',
            pattern: '编号规则代码格式不正确',
            length: '编号规则代码长度必须在1-20个字符之间',
            format: '编号规则代码格式不正确',
            invalid: '请输入有效的编号规则代码'
          }
        },
        deptCode: {
          label: '部门代码',
          placeholder: '请选择部门代码'
        },
        numberRuleShortCode: {
          label: '编号规则简称',
          placeholder: '请输入编号规则简称',
            validation: {
              required: '请输入编号规则简称',
              maxLength: '编号规则简称长度必须在1-4个字符之间',
              pattern: '编号规则简称格式不正确',
              length: '编号规则简称长度必须在1-4个字符之间',
              format: '编号规则简称格式不正确',
              invalid: '请输入有效的编号规则简称'
            }
        },
        numberRuleType: {
          label: '编号规则类型',
          placeholder: '请选择编号规则类型',
            validation: {
              required: '请选择编号规则类型'
            }
        },
        status: {
          label: '状态',
          placeholder: '请选择状态',
            validation: {
              required: '请选择状态'
            }
        },
        numberRuleDescription: {
          label: '编号规则描述',
          placeholder: '请输入编号规则描述',
          validation: {
            maxLength: '编号规则描述长度不能超过500个字符',
            pattern: '编号规则描述格式不正确',
            format: '编号规则描述格式不正确',
            invalid: '请输入有效的编号规则描述'
          }
        },
        numberPrefix: {
          label: '编号前缀',
          placeholder: '请输入编号前缀',
          validation: {
            maxLength: '编号前缀长度不能超过50个字符',
            pattern: '编号前缀格式不正确',
            format: '编号前缀格式不正确',
            invalid: '请输入有效的编号前缀'
          }
        },
        numberSuffix: {
          label: '编号后缀',
          placeholder: '请输入编号后缀',
          validation: {
            maxLength: '编号后缀长度不能超过50个字符',
            pattern: '编号后缀格式不正确',
            format: '编号后缀格式不正确',
            invalid: '请输入有效的编号后缀'
          }
        },
        dateFormat: {
          label: '日期格式',
          placeholder: '请选择日期格式',
            validation: {
              required: '请选择日期格式'
            }
        },
        sequenceLength: {
          label: '序号长度',
          placeholder: '请输入序号长度',
            validation: {
              required: '请输入序号长度',
              format: '序号长度必须是数字',
              invalid: '请输入有效的序号长度'
            }
        },
        sequenceStart: {
          label: '序号起始值',
          placeholder: '请输入序号起始值',
            validation: {
              required: '请输入序号起始值',
              format: '序号起始值必须是数字',
              invalid: '请输入有效的序号起始值'
            }
        },
        sequenceStep: {
          label: '序号步长',
          placeholder: '请输入序号步长',
            validation: {
              required: '请输入序号步长',
              format: '序号步长必须是数字',
              invalid: '请输入有效的序号步长'
            }
        },
        currentSequence: {
          label: '当前序号',
          placeholder: '请输入当前序号',
          validation: {
            format: '当前序号必须是数字',
            pattern: '当前序号格式不正确',
            invalid: '请输入有效的当前序号'
          }
        },
        sequenceResetRule: {
          label: '序号重置规则',
          placeholder: '请选择序号重置规则',
          validation: {
            required: '请选择序号重置规则'
          }
        },
        includeCompanyCode: {
          label: '包含公司代码',
          placeholder: '请选择是否包含公司代码',
          validation: {
            required: '请选择是否包含公司代码'
          }
        },
        includeDepartmentCode: {
          label: '包含部门代码',
          placeholder: '请选择是否包含部门代码',
          validation: {
            required: '请选择是否包含部门代码'
          }
        },
        includeRevisionNumber: {
          label: '包含修订号',
          placeholder: '请选择是否包含修订号',
          validation: {
            required: '请选择是否包含修订号'
          }
        },
        includeYear: {
          label: '包含年份',
          placeholder: '请选择是否包含年份',
          validation: {
            required: '请选择是否包含年份'
          }
        },
        includeMonth: {
          label: '包含月份',
          placeholder: '请选择是否包含月份',
          validation: {
            required: '请选择是否包含月份'
          }
        },
        includeDay: {
          label: '包含日期',
          placeholder: '请选择是否包含日期',
          validation: {
            required: '请选择是否包含日期'
          }
        },
        allowDuplicate: {
          label: '允许重复',
          placeholder: '请选择是否允许重复',
          validation: {
            required: '请选择是否允许重复'
          }
        },
        duplicateCheckScope: {
          label: '重复检查范围',
          placeholder: '请选择重复检查范围',
          validation: {
            required: '请选择重复检查范围'
          }
        },
        orderNum: {
          label: '排序号',
          placeholder: '请输入排序号',
          validation: {
            required: '请输入排序号',
            format: '排序号必须是数字',
            invalid: '请输入有效的排序号'
          }
        }
      },

      // 消息提示
      messages: {
        confirmDelete: '是否确认删除选中的编号规则？',
        deleteSuccess: '编号规则删除成功',
        deleteFailed: '编号规则删除失败',
        saveSuccess: '编号规则信息保存成功',
        saveFailed: '编号规则信息保存失败',
        importSuccess: '编号规则导入成功',
        importFailed: '编号规则导入失败',
        exportSuccess: '编号规则导出成功',
        exportFailed: '编号规则导出失败',
        getDataFailed: '获取编号规则数据失败',
        createNumberRule: '创建编号规则',
        updateNumberRule: '更新编号规则',
        generateSuccess: '生成编号成功',
        generateFailed: '生成编号失败',
        validateSuccess: '验证通过',
        validateFailed: '验证失败'
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
        createNumberRule: '创建编号规则',
        updateNumberRule: '更新编号规则'
      },

      // 导入提示
      importTips: 'Excel工作表名称必须为 "NumberRule"',

      // 导入导出
      import: {
        title: '导入编号规则',
        template: '下载模板',
        success: '导入成功',
        failed: '导入失败',
        fileName: '编号规则数据'
      },
      export: {
        title: '导出编号规则',
        fileName: '编号规则数据',
        success: '导出成功',
        failed: '导出失败'
      },
      template: {
        fileName: '编号规则导入模板',
        downloadFailed: '下载模板失败'
      },

      // 标签页
      tabs: {
        basicInfo: '基本信息',
        numberConfig: '编号配置',
        advancedConfig: '高级配置',
        otherInfo: '其他信息'
      },

    }
  }
}
