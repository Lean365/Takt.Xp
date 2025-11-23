export default {
  workflow: {
    history: {
      title: 'Historique des Workflows',
      list: {
        title: 'Liste des Historiques de Workflows',
        search: {
          name: 'Nom du Workflow',
          key: 'Clé du Workflow',
          version: 'Version',
          status: 'Statut',
          startTime: 'Heure de Début',
          endTime: 'Heure de Fin'
        },
        table: {
          name: 'Nom du Workflow',
          key: 'Clé du Workflow',
          version: 'Version',
          status: 'Statut',
          startTime: 'Heure de Début',
          endTime: 'Heure de Fin',
          duration: 'Durée',
          actions: 'Actions'
        },
        actions: {
          view: 'Voir',
          delete: 'Supprimer',
          export: 'Exporter',
          import: 'Importer',
          refresh: 'Actualiser'
        },
        status: {
          running: 'En Cours',
          completed: 'Terminé',
          terminated: 'Interrompu',
          failed: 'Échoué'
        }
      },
      detail: {
        title: 'Détail de l\'Historique du Workflow',
        basic: {
          title: 'Informations de Base',
          name: 'Nom du Workflow',
          key: 'Clé du Workflow',
          version: 'Version',
          status: 'Statut',
          startTime: 'Heure de Début',
          endTime: 'Heure de Fin',
          duration: 'Durée'
        },
        nodes: {
          title: 'Informations du Nœud',
          name: 'Nom du Nœud',
          type: 'Type de Nœud',
          status: 'Statut',
          startTime: 'Heure de Début',
          endTime: 'Heure de Fin',
          duration: 'Durée',
          input: 'Entrée',
          output: 'Sortie',
          error: 'Erreur'
        },
        actions: {
          back: 'Retour'
        }
      }
    }
  }
} 