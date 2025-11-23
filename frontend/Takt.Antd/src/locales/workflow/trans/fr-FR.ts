export default {
  workflow: {
    transition: {
      title: 'Transition de flux de travail',
      fields: {
        transitionId: 'ID de transition',
        sourceNodeId: 'ID du nœud source',
        sourceNodeName: 'Nom du nœud source',
        targetNodeId: 'ID du nœud cible',
        targetNodeName: 'Nom du nœud cible',
        condition: 'Condition de transition',
        definitionId: 'ID de définition',
        definitionName: 'Nom de définition',
        remark: 'Remarque',
      },
      placeholder: {
        sourceNodeId: 'Sélectionnez le nœud source',
        targetNodeId: 'Sélectionnez le nœud cible',
        condition: 'Entrez la condition de transition',
        definitionId: 'Sélectionnez la définition de flux de travail',
        remark: 'Entrez une remarque',
        validation: {
          sourceNodeId: {
            required: 'Sélectionnez le nœud source',
          },
          targetNodeId: {
            required: 'Sélectionnez le nœud cible',
          },
          definitionId: {
            required: 'Sélectionnez la définition de flux de travail',
          },
          condition: {
            length: 'La condition de transition doit contenir entre 2 et 2000 caractères',
          },
          remark: {
            length: 'La remarque doit contenir entre 2 et 200 caractères',
          },
        },
      },
    },
  },
} 