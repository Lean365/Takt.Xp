export default {
  workflow: {
    form: {
      title: '表单管理',
      detail: '表单详情',
      tabs: {
        basic: '基础字段',
        designer: '表单设计器',
      },
      fields: {
        formKey: '表单键',
        formName: '表单名称',
        description: '表单描述',
        formCategory: '表单分类',
        formType: '表单类型',
        version: '表单版本',
        formConfig: '表单配置',
        definitionId: '流程ID',
        definitionName: '流程名称',
        status: '状态',
        remark: '备注',
      },
      placeholder: {
        formKey: '请输入表单键',
        formName: '请输入表单名称',
        description: '请输入表单描述',
        formCategory: '请选择表单分类',
        formType: '请选择表单类型',
        version: '请输入表单版本',
        formConfig: '请输入表单配置',
        definitionName: '请选择流程名称',
        status: '请选择状态',
        remark: '请输入备注',
        validation: {
          formKey: {
            required: '请输入表单键',
            length: '表单键长度必须在1-100个字符之间',
            pattern: '表单键只能包含英文、数字和下划线，且必须以字母开头'
          },
          formName: {
            required: '请输入表单名称',
            length: '表单名称长度必须在1-200个字符之间'
          },
          formCategory: {
            required: '请选择表单分类'
          },
          formType: {
            required: '请选择表单类型'
          },
          version: {
            required: '请输入表单版本'
          },
          formConfig: {
            required: '请通过表单设计器配置表单',
            length: '表单配置长度必须在2-20000个字符之间'
          },
          status: {
            required: '请选择状态'
          },
          remark: {
            required: '请输入备注',
            length: '备注长度必须在2-200个字符之间'
          }
        }
      },
      actions: {
        view: '查看表单'
      }
    }
  }
};
