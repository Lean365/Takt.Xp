export default {
  workflow: {
    transition: {
      title: 'Workflow Transition',
      fields: {
        transitionId: 'Transition ID',
        sourceNodeId: 'Source Node ID',
        sourceNodeName: 'Source Node Name',
        targetNodeId: 'Target Node ID',
        targetNodeName: 'Target Node Name',
        condition: 'Transition Condition',
        definitionId: 'Definition ID',
        definitionName: 'Definition Name',
        remark: 'Remark',
      },
      placeholder: {
        sourceNodeId: 'Please select source node',
        targetNodeId: 'Please select target node',
        condition: 'Please enter transition condition',
        definitionId: 'Please select workflow definition',
        remark: 'Please enter remark',
        validation: {
          sourceNodeId: {
            required: 'Please select source node',
          },
          targetNodeId: {
            required: 'Please select target node',
          },
          definitionId: {
            required: 'Please select workflow definition',
          },
          condition: {
            length: 'Transition condition length must be between 2-2000 characters',
          },
          remark: {
            length: 'Remark length must be between 2-200 characters',
          },
        },
      },
    },
  },
} 