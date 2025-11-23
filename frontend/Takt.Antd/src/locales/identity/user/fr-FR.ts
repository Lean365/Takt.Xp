export default {
  identity: {
    user: {
      title: 'Gestion des utilisateurs',
      profile: 'Informations personnelles',
      changePasswordTitle: 'Modifier le mot de passe',
      
      // Lié au mot de passe
      password: {
        old: {
          label: 'Ancien mot de passe',
          placeholder: 'Veuillez saisir l\'ancien mot de passe',
          validation: {
            required: 'L\'ancien mot de passe ne peut pas être vide',
          }
        },
        new: {
          label: 'Nouveau mot de passe',
          placeholder: 'Veuillez saisir le nouveau mot de passe',
          validation: {
            required: 'Le nouveau mot de passe ne peut pas être vide',
          }
        },
        confirm: {
          label: 'Confirmer le mot de passe',
          placeholder: 'Veuillez saisir la confirmation du mot de passe',
          validation: {
            required: 'La confirmation du mot de passe ne peut pas être vide',
          }
        },
      },
      tabs: {
        userInfo: 'Informations utilisateur',
        organization: 'Relations organisationnelles',
        avatar: 'Avatar'
      },
      // Définitions des champs de formulaire
      fields: {
        userId: {
          label: 'ID utilisateur'
        },
        userName: {
          label: 'Nom d\'utilisateur',
          placeholder: 'Veuillez saisir le nom d\'utilisateur',
          validation: {
            required: 'Le nom d\'utilisateur ne peut pas être vide',
            format: 'Doit commencer par une lettre minuscule, ne peut contenir que des lettres minuscules, des chiffres et des tirets, pas de points ou de traits de soulignement, longueur entre 4-50 caractères'
          }
        },
        nickName: {
          label: 'Pseudonyme',
          placeholder: 'Veuillez saisir le pseudonyme',
          validation: {
            required: 'Le pseudonyme ne peut pas être vide',
            format: '2-50 caractères, autorise le chinois, le japonais, le coréen, l\'anglais, les chiffres, les points anglais et les tirets, interdit les traits de soulignement et les espaces'
          }
        },
        realName: {
          label: 'Nom réel',
          placeholder: 'Veuillez saisir le nom réel',
          validation: {
            required: 'Le nom réel ne peut pas être vide',
            format: 'La longueur du nom réel doit être entre 2-20 caractères, ne peut contenir que du chinois, de l\'anglais, des chiffres et des traits de soulignement'
          }
        },
        fullName: {
          label: 'Nom complet',
          placeholder: 'Veuillez saisir le nom complet',
          validation: {
            required: 'Le nom complet ne peut pas être vide',
            format: 'La longueur du nom complet doit être entre 2-20 caractères, ne peut contenir que du chinois, de l\'anglais, des chiffres et des traits de soulignement'
          }
        },
        englishName: {
          label: 'Nom anglais',
          placeholder: 'Veuillez saisir le nom anglais',
          validation: {
            required: 'Le nom anglais ne peut pas être vide',
            format: 'La longueur du nom anglais doit être entre 2-100 caractères, doit commencer par une lettre, ne peut contenir que des lettres anglaises, des espaces, des tirets et des points anglais'
          }
        },
        password: {
          label: 'Mot de passe',
          placeholder: 'Veuillez saisir le mot de passe',
          validation: {
            required: 'Le mot de passe ne peut pas être vide',
            format: 'La longueur du mot de passe doit être entre 6-20 caractères, ne peut contenir que des lettres anglaises, des chiffres et des caractères spéciaux'
          }
        },
        userType: {
          label: 'Type d\'utilisateur',
          placeholder: 'Veuillez sélectionner le type d\'utilisateur',
          options: {
            admin: 'Administrateur',
            user: 'Utilisateur normal'
          }
        },
        email: {
          label: 'E-mail',
          placeholder: 'Veuillez saisir l\'e-mail',
          validation: {
            required: 'L\'e-mail ne peut pas être vide',
            invalid: 'La longueur de l\'e-mail doit être entre 6-100 caractères et au bon format'
          }
        },
        phoneNumber: {
          label: 'Numéro de téléphone',
          placeholder: 'Veuillez saisir le numéro de téléphone',
          validation: {
            required: 'Le numéro de téléphone ne peut pas être vide',
            invalid: 'Veuillez saisir le format correct du numéro de téléphone mobile ou fixe'
          }
        },
        gender: {
          label: 'Sexe',
          placeholder: 'Veuillez sélectionner le sexe',
          options: {
            male: 'Homme',
            female: 'Femme',
            unknown: 'Inconnu'
          }
        },
        avatar: {
          label: 'Avatar',
          upload: 'Télécharger l\'avatar',
          currentAvatar: 'Avatar actuel',
          avatarUpload: 'Téléchargement d\'avatar',
          uploadSuccess: 'Avatar téléchargé avec succès',
          uploadError: 'Échec du téléchargement de l\'avatar',
          uploading: 'Téléchargement de l\'avatar...',
          onlyImage: 'Seuls les fichiers image peuvent être téléchargés !',
          sizeLimit: 'La taille de l\'image ne peut pas dépasser 5MB !'
        },
        status: {
          label: 'Statut',
          placeholder: 'Veuillez sélectionner le statut',
          options: {
            enabled: 'Activé',
            disabled: 'Désactivé'
          }
        },
        lastPasswordChangeTime: {
          label: 'Dernière modification du mot de passe'
        },
        lockEndTime: {
          label: 'Heure de fin de verrouillage'
        },
        lockReason: {
          label: 'Raison du verrouillage'
        },
        isLock: {
          label: 'Verrouillé'
        },
        errorLimit: {
          label: 'Limite du nombre d\'erreurs'
        },
        loginCount: {
          label: 'Nombre de connexions'
        },
        deptName: {
          label: 'Département',
          placeholder: 'Veuillez sélectionner le département',
          validation: {
            required: 'Le département ne peut pas être vide'
          }
        },
        role: {
          label: 'Rôle',
          placeholder: 'Veuillez sélectionner le rôle',
          validation: {
            required: 'Le rôle ne peut pas être vide'
          }
        },
        post: {
          label: 'Poste',
          placeholder: 'Veuillez sélectionner le poste',
          validation: {
            required: 'Le poste ne peut pas être vide'
          }
        },
        remark: {
          label: 'Remarques',
          placeholder: 'Veuillez saisir les remarques'
        }
      },

      // Boutons d'action
      actions: {
        add: 'Ajouter un utilisateur',
        edit: 'Modifier l\'utilisateur',
        'delete': 'Supprimer l\'utilisateur',
        resetPassword: 'Réinitialiser le mot de passe',
        export: 'Exporter les utilisateurs'
      },

      // Messages d'invite
      messages: {
        confirmDelete: 'Êtes-vous sûr de vouloir supprimer l\'utilisateur sélectionné ?',
        confirmResetPassword: 'Êtes-vous sûr de vouloir réinitialiser le mot de passe de l\'utilisateur sélectionné ?',
        confirmToggleStatus: 'Êtes-vous sûr de vouloir {action} cet utilisateur ?',
        deleteSuccess: 'Utilisateur supprimé avec succès',
        deleteFailed: 'Échec de la suppression de l\'utilisateur',
        saveSuccess: 'Informations utilisateur sauvegardées avec succès',
        saveFailed: 'Échec de la sauvegarde des informations utilisateur',
        resetPasswordSuccess: 'Mot de passe réinitialisé avec succès',
        resetPasswordFailed: 'Échec de la réinitialisation du mot de passe',
        importSuccess: 'Import d\'utilisateur réussi',
        importFailed: 'Échec de l\'import d\'utilisateur',
        exportSuccess: 'Export d\'utilisateur réussi',
        exportFailed: 'Échec de l\'export d\'utilisateur',
        toggleStatusSuccess: 'Statut modifié avec succès',
        toggleStatusFailed: 'Échec de la modification du statut',
        cannotDeleteAdmin: 'Les utilisateurs administratifs ne peuvent pas être supprimés !',
        cannotEditAdmin: 'Les utilisateurs administratifs ne peuvent pas être modifiés !',
        cannotUpdateAdminStatus: 'Le statut des utilisateurs administratifs ne peut pas être modifié !',
        cannotResetAdminPassword: 'Le mot de passe des utilisateurs administratifs ne peut pas être réinitialisé !',
        cannotUnlockAdmin: 'Les utilisateurs administratifs ne peuvent pas être déverrouillés !',
        cannotAllocateRole: 'Les utilisateurs administratifs ne peuvent pas être assignés à des rôles !',
        cannotAllocateDept: 'Les utilisateurs administratifs ne peuvent pas être assignés à des départements !',
        cannotAllocatePost: 'Les utilisateurs administratifs ne peuvent pas être assignés à des postes !',
        statusUpdateSuccess: 'Statut mis à jour avec succès',
        statusUpdateFailed: 'Échec de la mise à jour du statut',
        lockStatusUpdateSuccess: 'Statut de verrouillage mis à jour avec succès',
        lockStatusUpdateFailed: 'Échec de la mise à jour du statut de verrouillage'
      },

      // Texte d'affichage du tableau
      table: {
        notLocked: 'Non verrouillé',
        none: 'Aucun',
        queryParams: 'Paramètres de requête',
        importResponseData: 'Données de réponse d\'import',
        parsedData: 'Données analysées',
        exportFailed: 'Échec de l\'export',
        downloadTemplateFailed: 'Échec du téléchargement du modèle',
        rowClicked: 'Ligne cliquée',
        toggleFullscreenState: 'Basculer l\'état plein écran',
        getOptionsFailed: 'Échec de l\'obtention des options',
        createUser: 'Créer un utilisateur',
        updateUser: 'Mettre à jour l\'utilisateur'
      },

      // Conseils d'import
      importTips: 'Le nom de la feuille Excel doit être "User"',

      // Onglets
      tab: {
        basic: 'Informations de base',
        profile: 'Profil personnel',
        role: 'Permissions de rôle',
        dept: 'Département et poste',
        other: 'Autres informations',
        avatar: 'Paramètres d\'avatar',
        loginLog: 'Journal de connexion',
        operateLog: 'Journal d\'opération',
        errorLog: 'Journal d\'exception',
        taskLog: 'Journal de tâches'
      },

      // Import/Export
      import: {
        title: 'Importer des utilisateurs',
        template: 'Télécharger le modèle',
        success: 'Import réussi',
        failed: 'Échec de l\'import',
        fileName: 'Données utilisateur'
      },
      export: {
        title: 'Exporter des utilisateurs',
        fileName: 'Données utilisateur',
        success: 'Export réussi',
        failed: 'Échec de l\'export'
      },
      template: {
        fileName: 'Modèle d\'import d\'utilisateur',
        downloadFailed: 'Échec du téléchargement du modèle'
      },

      // Réinitialiser le mot de passe
      resetPwd: 'Réinitialiser le mot de passe',
      
      // Modifier le mot de passe
      changePassword: {
        success: 'Mot de passe modifié avec succès',
        failed: 'Échec de la modification du mot de passe, veuillez réessayer',
        changeFailed: 'Échec de la modification du mot de passe'
      },

      // Lié à l'attribution
      allocateDept: 'Assigner le département',
      allocatePost: 'Assigner le poste',
      allocateRole: 'Assigner le rôle',
      
      roleAllocate: {
        unallocated: 'Non assigné',
        allocated: 'Assigné',
        loadRoleListFailed: 'Échec du chargement de la liste des rôles',
        startLoadUserRoles: 'Début du chargement des rôles utilisateur',
        userRolesApiResponse: 'Réponse API des rôles utilisateur',
        setSelectedRoles: 'Définir les rôles sélectionnés',
        loadUserRolesFailed: 'Échec du chargement des rôles utilisateur'
      },
      
      postAllocate: {
        unallocated: 'Non assigné',
        allocated: 'Assigné',
        loadPostListFailed: 'Échec du chargement de la liste des postes',
        loadUserPostsFailed: 'Échec du chargement des postes utilisateur'
      }
    }
  }
}
