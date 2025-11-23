export default {
  routine: {
    quartz: {
      // Informations de base
      jobId: 'ID de tâche',
      jobName: 'Nom de la tâche',
      jobGroup: 'Groupe de tâches',
      jobClass: 'Classe de tâche',
      jobMethod: 'Méthode de tâche',
      jobParams: 'Paramètres de tâche',
      cronExpression: 'Expression Cron',
      jobStatus: 'État de la tâche',
      remark: 'Remarque',
      createTime: 'Date de création',
      updateTime: 'Date de mise à jour',

      // Boutons d'action
      add: 'Ajouter une tâche',
      edit: 'Modifier la tâche',
      delete: 'Supprimer la tâche',
      batchDelete: 'Suppression par lots',
      export: 'Exporter',
      import: 'Importer',
      downloadTemplate: 'Télécharger le modèle',
      preview: 'Aperçu',
      execute: 'Exécuter',
      pause: 'Suspendre',
      resume: 'Reprendre',

      // Placeholders du formulaire
      placeholder: {
        jobId: 'Entrez l\'ID de la tâche',
        jobName: 'Entrez le nom de la tâche',
        jobGroup: 'Entrez le groupe de tâches',
        jobClass: 'Entrez la classe de tâche',
        jobMethod: 'Entrez la méthode de tâche',
        jobParams: 'Entrez les paramètres de tâche',
        cronExpression: 'Entrez l\'expression Cron',
        jobStatus: 'Sélectionnez l\'état de la tâche',
        remark: 'Entrez la remarque',
        startTime: 'Heure de début',
        endTime: 'Heure de fin'
      },

      // Validation du formulaire
      validation: {
        jobId: {
          required: 'Entrez l\'ID de la tâche',
          maxLength: 'L\'ID de la tâche ne peut pas dépasser 100 caractères'
        },
        jobName: {
          required: 'Entrez le nom de la tâche',
          maxLength: 'Le nom de la tâche ne peut pas dépasser 50 caractères'
        },
        jobGroup: {
          required: 'Entrez le groupe de tâches',
          maxLength: 'Le groupe de tâches ne peut pas dépasser 50 caractères'
        },
        jobClass: {
          required: 'Entrez la classe de tâche',
          maxLength: 'La classe de tâche ne peut pas dépasser 200 caractères'
        },
        jobMethod: {
          required: 'Entrez la méthode de tâche',
          maxLength: 'La méthode de tâche ne peut pas dépasser 100 caractères'
        },
        cronExpression: {
          required: 'Entrez l\'expression Cron',
          maxLength: 'L\'expression Cron ne peut pas dépasser 100 caractères'
        },
        jobStatus: {
          required: 'Sélectionnez l\'état de la tâche'
        }
      },

      // Résultats des opérations
      message: {
        success: {
          add: 'Ajouté avec succès',
          edit: 'Modifié avec succès',
          delete: 'Supprimé avec succès',
          batchDelete: 'Suppression par lots réussie',
          export: 'Exporté avec succès',
          import: 'Importé avec succès',
          execute: 'Exécuté avec succès',
          pause: 'Suspendu avec succès',
          resume: 'Repris avec succès'
        },
        failed: {
          add: 'Échec de l\'ajout',
          edit: 'Échec de la modification',
          delete: 'Échec de la suppression',
          batchDelete: 'Échec de la suppression par lots',
          export: 'Échec de l\'exportation',
          import: 'Échec de l\'importation',
          execute: 'Échec de l\'exécution',
          pause: 'Échec de la suspension',
          resume: 'Échec de la reprise'
        }
      },

      // Page de détails
      detail: {
        title: 'Détails de la tâche'
      }
    }
  }
} 