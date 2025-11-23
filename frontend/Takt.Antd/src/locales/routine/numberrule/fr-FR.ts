export default {
  routine: {
    core: {
      numberrule: {
        // Titre de la page
        title: 'Gestion des Règles de Numérotation',
        
        // Onglets
        tabs: {
          basicInfo: 'Informations de Base',
          numberConfig: 'Configuration de Numérotation',
          advancedConfig: 'Configuration Avancée',
          otherInfo: 'Autres Informations'
        },

        // Étiquettes de champs
        fields: {
          companyCode: {
            label: 'Code d\'Entreprise',
            placeholder: 'Veuillez saisir le code d\'entreprise',
            required: 'Veuillez saisir le code d\'entreprise',
            length: 'La longueur du code d\'entreprise doit être entre 1-20 caractères'
          },
          numberRuleName: {
            label: 'Nom de la Règle de Numérotation',
            placeholder: 'Veuillez saisir le nom de la règle de numérotation',
            required: 'Veuillez saisir le nom de la règle de numérotation',
            length: 'La longueur du nom de la règle de numérotation doit être entre 1-100 caractères'
          },
          numberRuleCode: {
            label: 'Code de la Règle de Numérotation',
            placeholder: 'Veuillez saisir le code de la règle de numérotation',
            required: 'Veuillez saisir le code de la règle de numérotation',
            length: 'La longueur du code de la règle de numérotation doit être entre 1-50 caractères'
          },
          deptCode: {
            label: 'Code du Département',
            placeholder: 'Veuillez saisir le code du département',
            required: 'Veuillez saisir le code du département',
            length: 'La longueur du code du département doit être entre 1-20 caractères'
          },
          numberRuleShortCode: {
            label: 'Code Court de la Règle de Numérotation',
            placeholder: 'Veuillez saisir le code court de la règle de numérotation',
            required: 'Veuillez saisir le code court de la règle de numérotation',
            length: 'La longueur du code court de la règle de numérotation doit être entre 1-4 caractères'
          },
          numberRuleType: {
            label: 'Type de Règle de Numérotation',
            placeholder: 'Veuillez sélectionner le type de règle de numérotation',
            required: 'Veuillez sélectionner le type de règle de numérotation'
          },
          status: {
            label: 'Statut',
            placeholder: 'Veuillez sélectionner le statut',
            required: 'Veuillez sélectionner le statut'
          },
          numberRuleDescription: {
            label: 'Description de la Règle de Numérotation',
            placeholder: 'Veuillez saisir la description de la règle de numérotation'
          },
          numberPrefix: {
            label: 'Préfixe du Numéro',
            placeholder: 'Veuillez saisir le préfixe du numéro'
          },
          numberSuffix: {
            label: 'Suffixe du Numéro',
            placeholder: 'Veuillez saisir le suffixe du numéro'
          },
          dateFormat: {
            label: 'Format de Date',
            placeholder: 'Veuillez sélectionner le format de date',
            required: 'Veuillez sélectionner le format de date'
          },
          sequenceLength: {
            label: 'Longueur de Séquence',
            placeholder: 'Veuillez saisir la longueur de séquence',
            required: 'Veuillez saisir la longueur de séquence'
          },
          sequenceStart: {
            label: 'Valeur de Début de Séquence',
            placeholder: 'Veuillez saisir la valeur de début de séquence',
            required: 'Veuillez saisir la valeur de début de séquence'
          },
          sequenceStep: {
            label: 'Étape de Séquence',
            placeholder: 'Veuillez saisir l\'étape de séquence',
            required: 'Veuillez saisir l\'étape de séquence'
          },
          currentSequence: {
            label: 'Séquence Actuelle',
            placeholder: 'Veuillez saisir la séquence actuelle'
          },
          sequenceResetRule: {
            label: 'Règle de Réinitialisation de Séquence',
            placeholder: 'Veuillez sélectionner la règle de réinitialisation de séquence'
          },
          includeCompanyCode: {
            label: 'Inclure le Code d\'Entreprise',
            placeholder: 'Veuillez sélectionner si inclure le code d\'entreprise'
          },
          includeDepartmentCode: {
            label: 'Inclure le Code de Département',
            placeholder: 'Veuillez sélectionner si inclure le code de département'
          },
          includeRevisionNumber: {
            label: 'Inclure le Numéro de Révision',
            placeholder: 'Veuillez sélectionner si inclure le numéro de révision'
          },
          includeYear: {
            label: 'Inclure l\'Année',
            placeholder: 'Veuillez sélectionner si inclure l\'année'
          },
          includeMonth: {
            label: 'Inclure le Mois',
            placeholder: 'Veuillez sélectionner si inclure le mois'
          },
          includeDay: {
            label: 'Inclure le Jour',
            placeholder: 'Veuillez sélectionner si inclure le jour'
          },
          allowDuplicate: {
            label: 'Autoriser les Doublons',
            placeholder: 'Veuillez sélectionner si autoriser les doublons'
          },
          duplicateCheckScope: {
            label: 'Portée de Vérification des Doublons',
            placeholder: 'Veuillez sélectionner la portée de vérification des doublons'
          },
          orderNum: {
            label: 'Numéro d\'Ordre',
            placeholder: 'Veuillez saisir le numéro d\'ordre'
          }
        },

        // Boutons d'action
        actions: {
          add: 'Ajouter une Règle de Numérotation',
          edit: 'Modifier la Règle de Numérotation',
          delete: 'Supprimer la Règle de Numérotation',
          batchDelete: 'Suppression en Lot',
          export: 'Exporter',
          import: 'Importer',
          downloadTemplate: 'Télécharger le Modèle',
          preview: 'Aperçu',
          generate: 'Générer un Numéro',
          validate: 'Valider la Règle'
        },

        // Textes de champs
        placeholder: {
          search: 'Veuillez saisir le nom ou le code de la règle de numérotation',
          selectAll: 'Tout Sélectionner',
          clear: 'Effacer'
        },

        // Messages de résultats d'opérations
        message: {
          success: {
            add: 'Règle de numérotation ajoutée avec succès',
            edit: 'Règle de numérotation mise à jour avec succès',
            delete: 'Règle de numérotation supprimée avec succès',
            batchDelete: 'Suppression en lot terminée avec succès',
            export: 'Exportation terminée avec succès',
            import: 'Importation terminée avec succès',
            generate: 'Numéro généré avec succès',
            validate: 'Validation réussie'
          },
          failed: {
            add: 'Échec de l\'ajout de la règle de numérotation',
            edit: 'Échec de la mise à jour de la règle de numérotation',
            delete: 'Échec de la suppression de la règle de numérotation',
            batchDelete: 'Échec de la suppression en lot',
            export: 'Échec de l\'exportation',
            import: 'Échec de l\'importation',
            generate: 'Échec de la génération du numéro',
            validate: 'Échec de la validation'
          },
          confirm: {
            delete: 'Êtes-vous sûr de vouloir supprimer la règle de numérotation sélectionnée ?',
            batchDelete: 'Êtes-vous sûr de vouloir supprimer les règles de numérotation sélectionnées ?'
          }
        },

        // Page de détails
        detail: {
          title: 'Détails de la Règle de Numérotation',
          basicInfo: 'Informations de Base',
          numberConfig: 'Configuration de Numérotation',
          advancedConfig: 'Configuration Avancée',
          otherInfo: 'Autres Informations'
        },

        // Titres de colonnes de tableau
        columns: {
          numberRuleName: 'Nom de la Règle de Numérotation',
          numberRuleCode: 'Code de la Règle de Numérotation',
          numberRuleShortCode: 'Code Court de la Règle de Numérotation',
          numberRuleType: 'Type de Règle de Numérotation',
          deptCode: 'Code du Département',
          status: 'Statut',
          createTime: 'Heure de Création',
          updateTime: 'Heure de Mise à Jour',
          remark: 'Remarque',
          actions: 'Actions'
        },

        // Types de règles de numérotation
        types: {
          daily: 'Affaires Quotidiennes',
          hr: 'Ressources Humaines',
          finance: 'Comptabilité Financière',
          logistics: 'Gestion Logistique',
          production: 'Gestion de Production',
          workflow: 'Flux de Travail'
        },

        // Statuts
        status: {
          normal: 'Normal',
          disabled: 'Désactivé'
        },

        // Règles de réinitialisation de séquence
        resetRules: {
          none: 'Aucune Réinitialisation',
          yearly: 'Réinitialisation Annuelle',
          monthly: 'Réinitialisation Mensuelle',
          daily: 'Réinitialisation Quotidienne'
        },

        // Portées de vérification des doublons
        checkScopes: {
          global: 'Global',
          yearly: 'Annuel',
          monthly: 'Mensuel',
          daily: 'Quotidien'
        },

        // Options de format de date
        dateFormats: {
          yyyy: 'yyyy',
          yyyyMM: 'yyyyMM',
          yyyyMMdd: 'yyyyMMdd',
          yyyyMMddHH: 'yyyyMMddHH',
          yyyyMMddHHmm: 'yyyyMMddHHmm',
          yyyyMMddHHmmss: 'yyyyMMddHHmmss'
        }
      }
    }
  }
}
