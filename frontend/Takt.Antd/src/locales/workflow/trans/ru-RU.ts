export default {
  workflow: {
    transition: {
      title: 'Переход рабочего процесса',
      fields: {
        transitionId: 'ID перехода',
        sourceNodeId: 'ID исходного узла',
        sourceNodeName: 'Название исходного узла',
        targetNodeId: 'ID целевого узла',
        targetNodeName: 'Название целевого узла',
        condition: 'Условие перехода',
        definitionId: 'ID определения',
        definitionName: 'Название определения',
        remark: 'Примечание',
      },
      placeholder: {
        sourceNodeId: 'Выберите исходный узел',
        targetNodeId: 'Выберите целевой узел',
        condition: 'Введите условие перехода',
        definitionId: 'Выберите определение рабочего процесса',
        remark: 'Введите примечание',
        validation: {
          sourceNodeId: {
            required: 'Выберите исходный узел',
          },
          targetNodeId: {
            required: 'Выберите целевой узел',
          },
          definitionId: {
            required: 'Выберите определение рабочего процесса',
          },
          condition: {
            length: 'Условие перехода должно содержать от 2 до 2000 символов',
          },
          remark: {
            length: 'Примечание должно содержать от 2 до 200 символов',
          },
        },
      },
    },
  },
} 