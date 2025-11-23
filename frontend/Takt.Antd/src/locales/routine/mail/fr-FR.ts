export default {
  routine: {
    mail: {
      // Informations de base
      mailId: 'ID du courriel',
      mailType: 'Type de courriel',
      mailSubject: 'Sujet du courriel',
      mailContent: 'Contenu du courriel',
      mailStatus: 'Statut du courriel',
      mailParams: 'Paramètres du courriel',
      remark: 'Remarque',
      createTime: 'Date de création',
      updateTime: 'Date de mise à jour',

      // Boutons d'action
      add: 'Ajouter un courriel',
      edit: 'Modifier le courriel',
      delete: 'Supprimer le courriel',
      batchDelete: 'Suppression par lots',
      export: 'Exporter',
      import: 'Importer',
      downloadTemplate: 'Télécharger le modèle',
      preview: 'Aperçu',
      send: 'Envoyer',

      // Placeholders du formulaire
      placeholder: {
        mailId: 'Veuillez saisir l\'ID du courriel',
        mailType: 'Veuillez sélectionner le type de courriel',
        mailSubject: 'Veuillez saisir le sujet du courriel',
        mailContent: 'Veuillez saisir le contenu du courriel',
        mailStatus: 'Veuillez sélectionner le statut du courriel',
        mailParams: 'Veuillez saisir les paramètres du courriel',
        remark: 'Veuillez saisir une remarque',
        startTime: 'Date de début',
        endTime: 'Date de fin'
      },

      // Validation du formulaire
      validation: {
        mailId: {
          required: 'Veuillez saisir l\'ID du courriel',
          maxLength: 'L\'ID du courriel ne peut pas dépasser 100 caractères'
        },
        mailType: {
          required: 'Veuillez sélectionner le type de courriel',
          maxLength: 'Le type de courriel ne peut pas dépasser 50 caractères'
        },
        mailSubject: {
          required: 'Veuillez saisir le sujet du courriel',
          maxLength: 'Le sujet du courriel ne peut pas dépasser 200 caractères'
        },
        mailContent: {
          required: 'Veuillez saisir le contenu du courriel',
          maxLength: 'Le contenu du courriel ne peut pas dépasser 4000 caractères'
        },
        mailStatus: {
          required: 'Veuillez sélectionner le statut du courriel'
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
        title: 'Détails du courriel'
      }
    }
  }
} 