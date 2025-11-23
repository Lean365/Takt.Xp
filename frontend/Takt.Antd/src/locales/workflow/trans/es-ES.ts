export default {
  workflow: {
    transition: {
      title: 'Transición de flujo de trabajo',
      fields: {
        transitionId: 'ID de transición',
        sourceNodeId: 'ID del nodo origen',
        sourceNodeName: 'Nombre del nodo origen',
        targetNodeId: 'ID del nodo destino',
        targetNodeName: 'Nombre del nodo destino',
        condition: 'Condición de transición',
        definitionId: 'ID de definición',
        definitionName: 'Nombre de definición',
        remark: 'Observación',
      },
      placeholder: {
        sourceNodeId: 'Seleccione el nodo origen',
        targetNodeId: 'Seleccione el nodo destino',
        condition: 'Ingrese la condición de transición',
        definitionId: 'Seleccione la definición de flujo de trabajo',
        remark: 'Ingrese una observación',
        validation: {
          sourceNodeId: {
            required: 'Seleccione el nodo origen',
          },
          targetNodeId: {
            required: 'Seleccione el nodo destino',
          },
          definitionId: {
            required: 'Seleccione la definición de flujo de trabajo',
          },
          condition: {
            length: 'La condición de transición debe contener entre 2 y 2000 caracteres',
          },
          remark: {
            length: 'La observación debe contener entre 2 y 200 caracteres',
          },
        },
      },
    },
  },
} 