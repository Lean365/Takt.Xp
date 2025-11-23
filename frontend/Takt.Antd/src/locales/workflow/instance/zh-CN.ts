export default {
  workflow: {
    instance: {
      title: '工作流实例',
      add: '新增流程实例',
      edit: '编辑流程实例',
      detail: '流程实例详情',
      fields: {
        instanceId: '实例ID',
        instanceTitle: '实例标题',
        instanceName: '实例名称',
        businessKey: '业务主键',
        definitionId: '流程ID',
        definitionName: '流程名称',
        schemeId: '流程定义ID',
        currentNodeId: '当前节点ID',
        currentNodeName: '当前节点名称',
        initiatorId: '发起人ID',
        initiatorName: '发起人',
        formData: '表单数据',
        formType: '表单类型',
        variables: '流程变量',
        status: '状态',
        priority: '优先级',
        urgency: '紧急程度',
        startTime: '开始时间',
        endTime: '结束时间',
        timeRange: '时间范围',
        remark: '备注',        
      },
      placeholder: {
        instanceTitle: '请输入实例标题',
        instanceName: '实例名称',
        businessKey: '请输入业务主键',
        definitionId: '流程ID',
        schemeId: '请选择流程定义',
        currentNodeId: '当前节点ID',
        initiatorId: '请选择发起人',
        formData: '请输入表单数据',
        formType: '请选择表单类型',
        variables: '请输入流程变量（JSON格式）',
        status: '请选择状态',
        priority: '请选择优先级',
        urgency: '请选择紧急程度',
        startTime: '请选择开始时间',
        endTime: '请选择结束时间',
        remark: '请输入备注',    
        validation: {
          instanceName: {
            required: '请输入实例名称',
            length: '实例名称长度必须在2-50个字符之间',
            pattern: '实例名称只能包含中文、英文、数字、下划线和横线'
          },
          businessKey: {
            required: '请输入业务主键',
            length: '业务主键长度必须在2-50个字符之间',
            pattern: '业务主键只能包含中文、英文、数字、下划线和横线'
          },
          definitionId: {
            required: '请输入流程ID',
            length: '流程ID长度必须在2-50个字符之间',
            pattern: '流程ID只能包含中文、英文、数字、下划线和横线'
          },
          currentNodeId: {
            required: '请输入当前节点ID',
            length: '当前节点ID长度必须在2-50个字符之间',
            pattern: '当前节点ID只能包含中文、英文、数字、下划线和横线'
          },
          initiatorId: {
            required: '请选择发起人',
          },
          formData: {
            required: '请输入表单数据',
            length: '表单数据长度必须在2-20000个字符之间',
            pattern: '表单数据只能包含中文、英文、数字、下划线和横线'
          },
          status: {
            required: '请选择状态',
          },
          startTime: {
            required: '请选择开始时间',
          },
          endTime: {
            required: '请选择结束时间',
          },
          remark: {
            required: '请输入备注',
            length: '备注长度必须在2-200个字符之间',
          }
        }
      },
      status: {
        draft: '草稿',
        running: '运行中',
        completed: '已完成',
        stopped: '已停用'
      },
      priority: {
        low: '低',
        normal: '普通',
        high: '高',
        urgent: '紧急',
        critical: '特急'
      },
      urgency: {
        normal: '普通',
        urgent: '加急',
        critical: '特急'
      },
      formType: {
        basic: '基础表单',
        advanced: '高级表单',
        custom: '自定义表单'
      },
      titlePlaceholder: '请输入实例标题',
      businessKeyPlaceholder: '请输入业务主键',
      statusPlaceholder: '请选择状态',
      priorityPlaceholder: '请选择优先级',
      urgencyPlaceholder: '请选择紧急程度',
      startTimePlaceholder: '请选择开始时间',
      endTimePlaceholder: '请选择结束时间',
      formTypePlaceholder: '请选择表单类型',
      variablesPlaceholder: '请输入流程变量（JSON格式）',
      formDataPlaceholder: '请输入表单数据（JSON格式）',
      titleRequired: '请输入实例标题',
      schemeIdRequired: '请选择流程定义',
      initiatorIdRequired: '请选择发起人',
      priorityRequired: '请选择优先级',
      urgencyRequired: '请选择紧急程度',
      formTypeRequired: '请选择表单类型',
      statusRequired: '请选择状态'
    }
  }
}
 