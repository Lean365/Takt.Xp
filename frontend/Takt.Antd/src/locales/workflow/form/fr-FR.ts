export default {
  workflow: {
    definition: {
      title: 'Définition du Workflow',
      list: {
        title: 'Liste des Définitions de Workflow',
        search: {
          name: 'Nom du Workflow',
          key: 'Clé du Workflow',
          version: 'Version',
          status: 'Statut'
        },
        table: {
          name: 'Nom du Workflow',
          key: 'Clé du Workflow',
          version: 'Version',
          status: 'Statut',
          createTime: 'Date de Création',
          updateTime: 'Date de Mise à Jour',
          actions: 'Actions'
        },
        actions: {
          create: 'Créer',
          edit: 'Modifier',
          delete: 'Supprimer',
          view: 'Voir',
          deploy: 'Déployer',
          export: 'Exporter',
          import: 'Importer',
          refresh: 'Actualiser'
        },
        status: {
          draft: 'Brouillon',
          deployed: 'Déployé',
          disabled: 'Désactivé'
        }
      },
      form: {
        title: {
          create: 'Créer une Définition de Workflow',
          edit: 'Modifier la Définition du Workflow'
        },
        fields: {
          name: 'Nom du Workflow',
          key: 'Clé du Workflow',
          version: 'Version',
          description: 'Description',
          status: 'Statut'
        },
        rules: {
          name: {
            required: 'Veuillez saisir le nom du workflow',
            max: 'Le nom du workflow ne peut pas dépasser 50 caractères'
          },
          key: {
            required: 'Veuillez saisir la clé du workflow',
            pattern: 'La clé du workflow ne peut contenir que des lettres, des chiffres et des underscores',
            max: 'La clé du workflow ne peut pas dépasser 50 caractères'
          },
          version: {
            required: 'Veuillez saisir le numéro de version',
            pattern: 'Le format du numéro de version est incorrect, doit être au format x.y.z'
          }
        },
        buttons: {
          submit: 'Envoyer',
          cancel: 'Annuler'
        }
      },
      detail: {
        title: 'Détail de la Définition du Workflow',
        basic: {
          title: 'Informations de Base',
          name: 'Nom du Workflow',
          key: 'Clé du Workflow',
          version: 'Version',
          description: 'Description',
          status: 'Statut',
          createTime: 'Date de Création',
          updateTime: 'Date de Mise à Jour'
        },
        actions: {
          edit: 'Modifier',
          back: 'Retour'
        }
      }
    }
  }
} 