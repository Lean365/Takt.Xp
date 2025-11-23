export default {
  workflow: {
    transition: {
      title: '工作流转换',
      fields: {
        transitionId: '转换ID',
        sourceNodeId: '源节点ID',
        sourceNodeName: '源节点名称',
        targetNodeId: '目标节点ID',
        targetNodeName: '目标节点名称',
        condition: '转换条件',
        definitionId: '定义ID',
        definitionName: '定义名称',
        remark: '备注',
      },
      placeholder: {
        sourceNodeId: '请选择源节点',
        targetNodeId: '请选择目标节点',
        condition: '请输入转换条件',
        definitionId: '请选择工作流定义',
        remark: '请输入备注',
        validation: {
          sourceNodeId: {
            required: '请选择源节点',
          },
          targetNodeId: {
            required: '请选择目标节点',
          },
          definitionId: {
            required: '请选择工作流定义',
          },
          condition: {
            length: '转换条件长度必须在2-2000个字符之间',
          },
          remark: {
            length: '备注长度必须在2-200个字符之间',
          },
        },
      },
    },
  },
} 