export default {
  routine: {
    mailtmpl: {
      // Informations de Base
      templateName: 'Nom du Modèle',
      templateType: 'Type de Modèle',
      templateSubject: 'Sujet du Modèle',
      templateContent: 'Contenu du Modèle',
      templateStatus: 'Statut du Modèle',
      templateParams: 'Paramètres du Modèle',
      remark: 'Remarques',
      createTime: 'Date de Création',
      updateTime: 'Date de Mise à Jour',

      // Boutons d'Action
      add: 'Ajouter un Modèle',
      edit: 'Modifier le Modèle',
      delete: 'Supprimer le Modèle',
      batchDelete: 'Suppression en Masse',
      export: 'Exporter',
      import: 'Importer',
      downloadTemplate: 'Télécharger le Modèle',
      preview: 'Aperçu',
      send: 'Envoyer',

      // Champs du Formulaire
      placeholder: {
        templateName: 'Veuillez saisir le nom du modèle',
        templateType: 'Veuillez sélectionner le type de modèle',
        templateSubject: 'Veuillez saisir le sujet du modèle',
        templateContent: 'Veuillez saisir le contenu du modèle',
        templateStatus: 'Veuillez sélectionner le statut du modèle',
        templateParams: 'Veuillez saisir les paramètres du modèle',
        remark: 'Veuillez saisir les remarques',
        startTime: 'Date de Début',
        endTime: 'Date de Fin'
      },

      // Validation du Formulaire
      validation: {
        templateName: {
          required: 'Veuillez saisir le nom du modèle',
          maxLength: 'Le nom du modèle ne peut pas dépasser 100 caractères'
        },
        templateType: {
          required: 'Veuillez sélectionner le type de modèle',
          maxLength: 'Le type de modèle ne peut pas dépasser 50 caractères'
        },
        templateSubject: {
          required: 'Veuillez saisir le sujet du modèle',
          maxLength: 'Le sujet du modèle ne peut pas dépasser 200 caractères'
        },
        templateContent: {
          required: 'Veuillez saisir le contenu du modèle',
          maxLength: 'Le contenu du modèle ne peut pas dépasser 4000 caractères'
        },
        templateStatus: {
          required: 'Veuillez sélectionner le statut du modèle'
        }
      },

      // Résultats des Opérations
      message: {
        success: {
          add: 'Ajouté avec succès',
          edit: 'Mis à jour avec succès',
          delete: 'Supprimé avec succès',
          batchDelete: 'Suppression en masse réussie',
          export: 'Exporté avec succès',
          import: 'Importé avec succès',
          send: 'Envoyé avec succès'
        },
        failed: {
          add: 'Échec de l\'ajout',
          edit: 'Échec de la mise à jour',
          delete: 'Échec de la suppression',
          batchDelete: 'Échec de la suppression en masse',
          export: 'Échec de l\'exportation',
          import: 'Échec de l\'importation',
          send: 'Échec de l\'envoi'
        }
      },

      // Page de Détails
      detail: {
        title: 'Détails du Modèle d\'Email'
      }
    }
  }
}
