export default {
  identity: {
    menu: {
      title: 'Gestion des menus',
      columns: {
        menuName: 'Nom du menu',
        transKey: 'Clé I18n',
        parentId: 'Menu parent',
        orderNum: 'Ordre',
        path: 'Chemin de la route',
        component: 'Chemin du composant',
        queryParams: 'Paramètres de la route',
        isExternal: 'Lien externe',
        isCache: 'Cache',
        menuType: 'Type de menu',
        visible: 'Visibilité',
        status: 'Statut',
        perms: 'Clé de permission',
        icon: 'Icône',
        id: 'ID',
        createBy: 'Créé par',
        createTime: 'Date de création',
        updateBy: 'Modifié par',
        updateTime: 'Date de modification',
        deleteBy: 'Supprimé par',
        deleteTime: 'Date de suppression',
        isDeleted: 'Supprimé',
        remark: 'Remarque',
        action: 'Action'
      },
      fields: {
        menuName: {
          label: 'Nom du menu',
          placeholder: 'Veuillez saisir le nom du menu',
          validation: {
            required: 'Veuillez saisir le nom du menu',
            length: 'La longueur du nom doit être comprise entre 2 et 50 caractères'
          }
        },
        transKey: {
          label: 'Clé I18n',
          placeholder: 'Veuillez saisir la clé I18n'
        },
        parentId: {
          label: 'Menu parent',
          placeholder: 'Veuillez sélectionner le menu parent',
          root: 'Menu racine'
        },
        orderNum: {
          label: 'Ordre',
          placeholder: 'Veuillez saisir l\'ordre',
          validation: {
            required: 'Veuillez saisir l\'ordre'
          }
        },
        path: {
          label: 'Chemin de la route',
          placeholder: 'Veuillez saisir le chemin de la route'
        },
        component: {
          label: 'Chemin du composant',
          placeholder: 'Veuillez saisir le chemin du composant'
        },
        queryParams: {
          label: 'Paramètres de la route',
          placeholder: 'Veuillez saisir les paramètres de la route'
        },
        isExternal: {
          label: 'Lien externe',
          placeholder: 'Veuillez sélectionner si c\'est un lien externe',
          options: {
            yes: 'Oui',
            no: 'Non'
          }
        },
        isCache: {
          label: 'Cache',
          placeholder: 'Veuillez sélectionner si c\'est en cache',
          options: {
            yes: 'Oui',
            no: 'Non'
          }
        },
        menuType: {
          label: 'Type de menu',
          options: {
            directory: 'Répertoire',
            menu: 'Menu',
            button: 'Bouton'
          },
          validation: {
            required: 'Veuillez sélectionner le type de menu'
          }
        },
        visible: {
          label: 'Visibilité',
          placeholder: 'Veuillez sélectionner la visibilité',
          options: {
            show: 'Afficher',
            hide: 'Masquer'
          }
        },
        status: {
          label: 'Statut',
          placeholder: 'Veuillez sélectionner le statut',
          options: {
            normal: 'Normal',
            disabled: 'Désactivé'
          }
        },
        perms: {
          label: 'Clé de permission',
          placeholder: 'Veuillez saisir la clé de permission'
        },
        icon: {
          label: 'Icône du menu',
          placeholder: 'Veuillez saisir l\'icône du menu'
        },
        }
      },
      dialog: {
        create: 'Ajouter un menu',
        update: 'Modifier le menu',
        delete: 'Supprimer le menu'
      },
      operation: {
        add: {
          title: 'Ajouter un menu',
          success: 'Ajout réussi',
          failed: 'Échec de l\'ajout'
        },
        edit: {
          title: 'Modifier le menu',
          success: 'Modification réussie',
          failed: 'Échec de la modification'
        },
        delete: {
          title: 'Supprimer le menu',
          confirm: 'Êtes-vous sûr de vouloir supprimer ce menu ?',
          success: 'Suppression réussie',
          failed: 'Échec de la suppression'
        }
      }
    }
  }
