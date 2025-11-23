export default {
  admin: {
    // Configuration système
    config: {
      // Informations de base
      name: 'Nom',
      key: 'Clé',
      value: 'Valeur',
      builtin: 'Intégré',
      order: 'Ordre',
      remark: 'Remarque',
      status: 'Statut',
      // Informations temporelles
      createTime: 'Date de création',
      createBy: 'Créé par',
      updateTime: 'Date de mise à jour',
      updateBy: 'Mis à jour par',
      // Opérations
      operation: 'Opération',
      // Informations de saisie
      placeholder: {
        name: 'Veuillez saisir le nom de la configuration',
        key: 'Veuillez saisir la clé de configuration',
        value: 'Veuillez saisir la valeur de configuration',
        builtin: 'Veuillez sélectionner si intégré',
        order: 'Veuillez saisir le numéro d\'ordre',
        remark: 'Veuillez saisir une remarque',
        status: 'Veuillez sélectionner le statut'
      },
      // Informations de validation
      validation: {
        name: {
          required: 'Veuillez saisir le nom de la configuration',
          maxLength: 'Le nom de la configuration ne peut pas dépasser 100 caractères'
        },
        key: {
          required: 'Veuillez saisir la clé de configuration',
          maxLength: 'La clé de configuration ne peut pas dépasser 100 caractères',
          pattern: 'La clé de configuration doit commencer par une lettre et ne peut contenir que des lettres, des chiffres, des underscores, des points et des deux-points'
        },
        value: {
          required: 'Veuillez saisir la valeur de configuration',
          maxLength: 'La valeur de configuration ne peut pas dépasser 500 caractères'
        },
        builtin: {
          required: 'Veuillez sélectionner si intégré'
        },
        order: {
          required: 'Veuillez saisir le numéro d\'ordre',
          range: 'Le numéro d\'ordre doit être compris entre 0 et 9999'
        },
        status: {
          required: 'Veuillez sélectionner le statut'
        }
      },
      // Résultats des opérations
      message: {
        addSuccess: 'Configuration ajoutée avec succès',
        editSuccess: 'Configuration mise à jour avec succès',
        deleteSuccess: 'Configuration supprimée avec succès',
        deleteConfirm: 'Êtes-vous sûr de vouloir supprimer la configuration "{name}" ?',
        exportSuccess: 'Configuration exportée avec succès',
        importSuccess: 'Configuration importée avec succès',
        refreshSuccess: 'Cache actualisé avec succès'
      }
    }
  }
} 