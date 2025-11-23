export default {
  workflow: {
    instance: {
      title: 'Instance de Workflow',
      list: {
        title: 'Liste des Instances de Workflow',
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
          terminate: 'Terminer',
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
      form: {
        title: {
          create: 'Créer une Instance de Workflow',
          import: 'Importer une Instance de Workflow'
        },
        fields: {
          workflowDefinitionId: 'Définition du Workflow',
          variables: 'Configuration des Variables'
        },
        rules: {
          workflowDefinitionId: {
            required: 'Veuillez sélectionner une définition de workflow'
          }
        },
        buttons: {
          submit: 'Envoyer',
          cancel: 'Annuler'
        }
      },
      detail: {
        title: 'Détail de l\'Instance de Workflow',
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
      },
      terminate: {
        title: 'Terminer l\'Instance de Workflow',
        confirm: 'Êtes-vous sûr de vouloir terminer cette instance ?',
        reason: 'Raison de la Terminaison',
        buttons: {
          submit: 'Envoyer',
          cancel: 'Annuler'
        }
      }
    }
  }
} 