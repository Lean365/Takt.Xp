export default {
  workflow: {

    definition: {
      title: '工作流定义',
      actions: {
        create: '创建',
        edit: '编辑',
      },
      tabs: {
        definition: '流程定义',
        instance: '流程实例',
      },
      fields: {
        workflowName: '流程名称',
        workflowCategory: '流程分类',
        workflowVersion: '流程版本',
        fromId: '表单ID',
        formName: '表单名称',
        workflowConfig: '流程配置',
        status: '状态',
      },
      placeholder: {
        workflowName: '请输入流程名称',
        workflowCategory: '请输入流程分类',
        workflowVersion: '请选择流程版本',
        formId: '请选择表单ID',
        formName: '请选择表单名称',
        workflowConfig: '请输入流程配置',
        status: '请选择状态',
      },
      validation: {
        workflowName: {
          required: '请输入流程名称',
          length: '流程名称长度必须在2-50个字符之间'
        },
        workflowConfig: {
          required: '请输入流程配置',
          length: '流程配置长度必须在2-20000个字符之间'
        }
      }
    }
  }
};
