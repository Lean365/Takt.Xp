export default {
  workflow: {
    transition: {
      title: 'ワークフロー遷移',
      fields: {
        transitionId: '遷移ID',
        sourceNodeId: 'ソースノードID',
        sourceNodeName: 'ソースノード名',
        targetNodeId: 'ターゲットノードID',
        targetNodeName: 'ターゲットノード名',
        condition: '遷移条件',
        definitionId: '定義ID',
        definitionName: '定義名',
        remark: '備考',
      },
      placeholder: {
        sourceNodeId: 'ソースノードを選択してください',
        targetNodeId: 'ターゲットノードを選択してください',
        condition: '遷移条件を入力してください',
        definitionId: 'ワークフロー定義を選択してください',
        remark: '備考を入力してください',
        validation: {
          sourceNodeId: {
            required: 'ソースノードを選択してください',
          },
          targetNodeId: {
            required: 'ターゲットノードを選択してください',
          },
          definitionId: {
            required: 'ワークフロー定義を選択してください',
          },
          condition: {
            length: '遷移条件は2-2000文字の間で入力してください',
          },
          remark: {
            length: '備考は2-200文字の間で入力してください',
          },
        },
      },
    },
  },
} 