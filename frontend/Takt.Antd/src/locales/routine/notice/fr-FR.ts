export default {
  routine: {
    notice: {
      // Informations de base
      noticeId: 'ID de l\'avis',
      noticeType: 'Type d\'avis',
      noticeTitle: 'Titre de l\'avis',
      noticeContent: 'Contenu de l\'avis',
      noticeStatus: 'Statut de l\'avis',
      noticeParams: 'Paramètres de l\'avis',
      remark: 'Remarque',
      createTime: 'Date de création',
      updateTime: 'Date de mise à jour',

      // Boutons d'action
      add: 'Ajouter un avis',
      edit: 'Modifier l\'avis',
      delete: 'Supprimer l\'avis',
      batchDelete: 'Suppression par lots',
      export: 'Exporter',
      import: 'Importer',
      downloadTemplate: 'Télécharger le modèle',
      preview: 'Aperçu',
      send: 'Envoyer',

      // Placeholders du formulaire
      placeholder: {
        noticeId: 'Veuillez saisir l\'ID de l\'avis',
        noticeType: 'Veuillez sélectionner le type d\'avis',
        noticeTitle: 'Veuillez saisir le titre de l\'avis',
        noticeContent: 'Veuillez saisir le contenu de l\'avis',
        noticeStatus: 'Veuillez sélectionner le statut de l\'avis',
        noticeParams: 'Veuillez saisir les paramètres de l\'avis',
        remark: 'Veuillez saisir une remarque',
        startTime: 'Date de début',
        endTime: 'Date de fin'
      },

      // Validation du formulaire
      validation: {
        noticeId: {
          required: 'Veuillez saisir l\'ID de l\'avis',
          maxLength: 'L\'ID de l\'avis ne peut pas dépasser 100 caractères'
        },
        noticeType: {
          required: 'Veuillez sélectionner le type d\'avis',
          maxLength: 'Le type d\'avis ne peut pas dépasser 50 caractères'
        },
        noticeTitle: {
          required: 'Veuillez saisir le titre de l\'avis',
          maxLength: 'Le titre de l\'avis ne peut pas dépasser 200 caractères'
        },
        noticeContent: {
          required: 'Veuillez saisir le contenu de l\'avis',
          maxLength: 'Le contenu de l\'avis ne peut pas dépasser 4000 caractères'
        },
        noticeStatus: {
          required: 'Veuillez sélectionner le statut de l\'avis'
        }
      },

      // Résultats des opérations
      message: {
        success: {
          add: 'Ajout réussi',
          edit: 'Modification réussie',
          delete: 'Suppression réussie',
          batchDelete: 'Suppression par lots réussie',
          export: 'Exportation réussie',
          import: 'Importation réussie',
          send: 'Envoi réussi'
        },
        failed: {
          add: 'Échec de l\'ajout',
          edit: 'Échec de la modification',
          delete: 'Échec de la suppression',
          batchDelete: 'Échec de la suppression par lots',
          export: 'Échec de l\'exportation',
          import: 'Échec de l\'importation',
          send: 'Échec de l\'envoi'
        }
      },

      // Page de détails
      detail: {
        title: 'Détails de l\'avis'
      }
    }
  }
} 