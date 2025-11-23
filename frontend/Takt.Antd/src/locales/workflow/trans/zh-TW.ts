export default {
  workflow: {
    transition: {
      title: '工作流轉換',
      fields: {
        transitionId: '轉換ID',
        sourceNodeId: '源節點ID',
        sourceNodeName: '源節點名稱',
        targetNodeId: '目標節點ID',
        targetNodeName: '目標節點名稱',
        condition: '轉換條件',
        definitionId: '定義ID',
        definitionName: '定義名稱',
        remark: '備註',
      },
      placeholder: {
        sourceNodeId: '請選擇源節點',
        targetNodeId: '請選擇目標節點',
        condition: '請輸入轉換條件',
        definitionId: '請選擇工作流定義',
        remark: '請輸入備註',
        validation: {
          sourceNodeId: {
            required: '請選擇源節點',
          },
          targetNodeId: {
            required: '請選擇目標節點',
          },
          definitionId: {
            required: '請選擇工作流定義',
          },
          condition: {
            length: '轉換條件長度必須在2-2000個字符之間',
          },
          remark: {
            length: '備註長度必須在2-200個字符之間',
          },
        },
      },
    },
  },
} 