export default {
  workflow: {
    transition: {
      title: '워크플로우 전환',
      fields: {
        transitionId: '전환ID',
        sourceNodeId: '소스 노드ID',
        sourceNodeName: '소스 노드명',
        targetNodeId: '타겟 노드ID',
        targetNodeName: '타겟 노드명',
        condition: '전환 조건',
        definitionId: '정의ID',
        definitionName: '정의명',
        remark: '비고',
      },
      placeholder: {
        sourceNodeId: '소스 노드를 선택하세요',
        targetNodeId: '타겟 노드를 선택하세요',
        condition: '전환 조건을 입력하세요',
        definitionId: '워크플로우 정의를 선택하세요',
        remark: '비고를 입력하세요',
        validation: {
          sourceNodeId: {
            required: '소스 노드를 선택하세요',
          },
          targetNodeId: {
            required: '타겟 노드를 선택하세요',
          },
          definitionId: {
            required: '워크플로우 정의를 선택하세요',
          },
          condition: {
            length: '전환 조건은 2-2000자 사이로 입력하세요',
          },
          remark: {
            length: '비고는 2-200자 사이로 입력하세요',
          },
        },
      },
    },
  },
} 