export default {
  common: {
    // ==================== Informations Système ====================
    system: {
      title: 'Plateforme Black Ice',
      slogan: 'Système de Gestion d\'Entreprise Professionnel, Efficace et Sécurisé',
      description: 'Système de Gestion d\'Entreprise Moderne basé sur .NET 8 et Vue 3',
      copyright: '© 2024Takt(Claude AI). Tous droits réservés.'
    },

    // ==================== Messages d\'Erreur ====================
    error: {
      clientError: 'Erreur de requête client, veuillez vérifier les paramètres de la requête',
      systemRestart: 'Le système est en maintenance, veuillez vous reconnecter plus tard',
      network: 'Échec de la connexion réseau, veuillez vérifier vos paramètres réseau',
      unauthorized: 'Accès non autorisé, veuillez vous reconnecter',
      forbidden: 'Accès interdit',
      notFound: 'La ressource demandée n\'existe pas',
      badRequest: 'Paramètres de requête invalides',
      serverError: 'Erreur interne du serveur',
      serviceUnavailable: 'Service temporairement indisponible',
      badGateway: 'Passerelle incorrecte',
      gatewayTimeout: 'Délai d\'attente de la passerelle',
      unknown: 'Erreur inconnue'
    },

    // ==================== Opérations de Base ====================
    // Titres de Base
    title: {
      list: 'Liste',
      detail: 'Détails',
      create: 'Ajouter Nouveau',
      edit: 'Modifier',
      preview: 'Aperçu'
    },

    // ==================== Définitions d\'État ====================
    status: {
      // État de Base
      base: {
        label: 'État',
        normal: 'Normal',
        disabled: 'Désactivé',
        placeholder: 'Veuillez sélectionner l\'état'
      },

      // État Oui/Non
      yesNo: {
        all: 'Tous',
        yes: 'Oui',
        no: 'Non'
      },

      // État de Visibilité
      visible: {
        show: 'Afficher',
        hide: 'Masquer'
      },

      // État de Cache
      cache: {
        enabled: 'Activé',
        disabled: 'Désactivé'
      },

      // État en Ligne
      online: {
        online: 'En ligne',
        offline: 'Hors ligne'
      },

      // État de Processus
      process: {
        pending: 'En attente',
        processing: 'En cours',
        completed: 'Terminé',
        failed: 'Échoué'
      },

      // État de Vérification
      verify: {
        unverified: 'Non vérifié',
        verified: 'Vérifié',
        failed: 'Échec de la vérification'
      },

      // État de Verrouillage
      lock: {
        locked: 'Verrouillé',
        unlocked: 'Déverrouillé'
      },

      // État de Publication
      publish: {
        draft: 'Brouillon',
        published: 'Publié',
        unpublished: 'Non publié'
      },

      // État de Synchronisation
      sync: {
        synced: 'Synchronisé',
        unsynced: 'Non synchronisé',
        syncing: 'Synchronisation en cours'
      }
    },

    // ==================== Opérations de Formulaire ====================
    form: {
      required: 'Requis',
      optional: 'Optionnel',
      invalid: 'Invalide',
      // Placeholders du Formulaire
      placeholder: {
        select: 'Veuillez sélectionner',
        input: 'Veuillez saisir',
        date: 'Veuillez sélectionner la date',
        time: 'Veuillez sélectionner l\'heure'
      },
      // Formulaire Utilisateur
      user: {
        // Informations de Base
        userId: {
          label: 'ID Utilisateur',
          placeholder: 'Veuillez saisir l\'ID utilisateur'
        },
        userName: {
          label: 'Nom d\'Utilisateur',
          placeholder: 'Veuillez saisir le nom d\'utilisateur'
        },
        nickName: {
          label: 'Pseudo',
          placeholder: 'Veuillez saisir le pseudo'
        },
        realName: {
          label: 'Nom Réel',
          placeholder: 'Veuillez saisir le nom réel'
        },
        englishName: {
          label: 'Nom en Anglais',
          placeholder: 'Veuillez saisir le nom en anglais'
        },
        password: {
          label: 'Mot de Passe',
          placeholder: 'Veuillez saisir le mot de passe'
        },
        confirmPassword: {
          label: 'Confirmer le Mot de Passe',
          placeholder: 'Veuillez saisir à nouveau le mot de passe'
        },
        
        // Informations Personnelles
        avatar: {
          label: 'Avatar',
          placeholder: 'Veuillez télécharger l\'avatar'
        },
        gender: {
          label: 'Genre',
          placeholder: 'Veuillez sélectionner le genre',
          options: {
            male: 'Masculin',
            female: 'Féminin',
            other: 'Autre'
          }
        },
        birthday: {
          label: 'Date de Naissance',
          placeholder: 'Veuillez sélectionner la date de naissance'
        },
        
        // Informations de Contact
        email: {
          label: 'E-mail',
          placeholder: 'Veuillez saisir l\'e-mail'
        },
        phoneNumber: {
          label: 'Téléphone Mobile',
          placeholder: 'Veuillez saisir le numéro de téléphone mobile'
        },
        telephone: {
          label: 'Téléphone',
          placeholder: 'Veuillez saisir le numéro de téléphone'
        },
        
        // Informations Organisationnelles
        deptId: {
          label: 'Département',
          placeholder: 'Veuillez sélectionner le département'
        },
        roleId: {
          label: 'Rôle',
          placeholder: 'Veuillez sélectionner le rôle'
        },
        postId: {
          label: 'Poste',
          placeholder: 'Veuillez sélectionner le poste'
        },
        
        // Informations de Compte
        userType: {
          label: 'Type d\'Utilisateur',
          placeholder: 'Veuillez sélectionner le type d\'utilisateur',
          options: {
            system: 'Utilisateur Système',
            normal: 'Utilisateur Normal'
          }
        },
        status: {
          label: 'État',
          placeholder: 'Veuillez sélectionner l\'état'
        },
        loginIp: {
          label: 'Dernière IP de Connexion',
          placeholder: 'Dernière IP de Connexion'
        },
        loginDate: {
          label: 'Dernière Heure de Connexion',
          placeholder: 'Dernière Heure de Connexion'
        },
        
        // Autres Informations
        remark: {
          label: 'Remarque',
          placeholder: 'Veuillez saisir la remarque'
        },
        sort: {
          label: 'Ordre',
          placeholder: 'Veuillez saisir le numéro d\'ordre'
        }
      },
      // Informations de Remarque
      remark: {
        label: 'Remarque',
        placeholder: 'Veuillez saisir la remarque'
      }
    },

    // ==================== Opérations de Table ====================
    table: {
      header: {
        operation: 'Opération'
      },
      config: {
        density: {
          default: 'Par défaut',
          middle: 'Moyen',
          small: 'Compact'
        },
        columnSetting: 'Paramètres des Colonnes'
      },
      pagination: {
        total: 'Total {total} éléments',
        current: 'Page {current}',
        pageSize: '{pageSize} éléments par page',
        jump: 'Aller à'
      },
      empty: 'Aucune donnée',
      loading: 'Chargement...',
      selectAll: 'Tout Sélectionner',
      selected: '{total} éléments sélectionnés'
    },

    // ==================== Opérations de Temps ====================
    datetime: {
      date: 'Date',
      time: 'Heure',
      year: 'Année',
      month: 'Mois',
      day: 'Jour',
      hour: 'Heure',
      minute: 'Minute',
      second: 'Seconde',
      startDate: 'Date de Début',
      endDate: 'Date de Fin',
      startTime: 'Heure de Début',
      endTime: 'Heure de Fin',
      createTime: 'Heure de Création',
      updateTime: 'Heure de Mise à Jour',
      formatError: 'Échec du formatage de l\'heure',
      relativeTimeFormatError: 'Échec du formatage de l\'heure relative',
      parseError: 'Échec de l\'analyse de la date',
      rangeSeparator: ' à '
    },

    // ==================== Opérations de Fichier ====================
    // Importer/Exporter
    import: {
      title: 'Importer des Données',
      file: 'Sélectionner un Fichier',
      select: 'Sélectionner un Fichier',
      template: 'Télécharger le Modèle',
      download: 'Télécharger le Modèle',
      note: 'Instructions d\'Importation',
      tips: 'Veuillez suivre strictement le format du modèle d\'importation, sinon l\'importation peut échouer',
      format: 'Seuls les fichiers Excel sont supportés !',
      size: 'La taille du fichier ne peut pas dépasser {size}MB !',
      total: 'Total des Enregistrements',
      success: 'Nombre de Succès',
      failed: 'Nombre d\'Échecs',
      message: 'Raison de l\'Échec',
      dragText: 'Cliquez ou glissez le fichier dans cette zone pour télécharger',
      dragHint: 'Prend en charge les fichiers Excel au format .xlsx',
      sheetName: 'Veuillez vous assurer que le nom de la feuille du fichier Excel est : {sheetName}',
      allSuccess: 'Importation réussie {count} enregistrements, tous réussis !',
      partialSuccess: 'Importation réussie {success} enregistrements, échoué {fail} enregistrements',
      allFailed: 'Toute l\'importation a échoué, total {count} enregistrements',
      noData: 'Aucune donnée lue',
      empty: 'Le fichier est vide, impossible de télécharger',
      importFailed: 'L\'importation a échoué',
      templateFileName: 'Import_Template_{time}.xlsx',
      limits: {
        title: 'Limites d\'importation',
        fileCount: 'Limite de nombre de fichiers : {count} fichier',
        fileSize: 'Limite de taille de fichier : {size}MB',
        recordCount: 'Limite de nombre d\'enregistrements : {count} enregistrements',
        fileFormat: 'Format de fichier : Seul le format .xlsx est pris en charge'
      },
      recordLimit: 'Le nombre d\'enregistrements à importer ({actual} enregistrements) dépasse la limite ({max} enregistrements), veuillez importer par lots'
    },

    // Téléchargement
    upload: {
      text: 'Glissez le fichier ici ou cliquez pour télécharger',
      picture: 'Cliquez pour télécharger l\'image',
      file: 'Cliquez pour télécharger le fichier',
      icon: 'Cliquez pour télécharger l\'icône',
      limit: {
        size: 'La taille du fichier ne peut pas dépasser {size}',
        type: 'Seul le format {type} est supporté'
      }
    },

    // ==================== Boutons de Fonction ====================
    actions: {
      // === Boutons d\'Opération de Base ===
      add: 'Ajouter',           // @btn-add-color
      edit: 'Modifier',         // @btn-edit-color
      delete: 'Supprimer',      // @btn-delete-color
      batchDelete: 'Supprimer par Lots', // @btn-batch-delete-color
      view: 'Voir',             // @btn-view-color
      clear: 'Effacer',         // @btn-clear-color
      forceOffline: 'Forcer Hors Ligne', // @btn-force-offline-color
      onlineStatus: 'Statut en Ligne', // @btn-online-status-color
      loginHistory: 'Historique de Connexion', // @btn-login-history-color
      sendMail: 'Envoyer un Mail', // @btn-send-mail-color
      viewMail: 'Voir le Mail', // @btn-view-mail-color
      mailTemplate: 'Modèle de Mail', // @btn-mail-template-color
      sendNotification: 'Envoyer une Notification', // @btn-send-notification-color
      viewNotification: 'Voir la Notification', // @btn-view-notification-color
      notificationSetting: 'Paramètres de Notification', // @btn-notification-setting-color
      sendMessage: 'Envoyer un Message', // @btn-send-message-color
      viewMessage: 'Voir le Message', // @btn-view-message-color
      messageSetting: 'Paramètres de Message', // @btn-message-setting-color

      // === Boutons d\'Opération de Données ===
      import: 'Importer',       // @btn-import-color
      export: 'Exporter',       // @btn-export-color
      template: 'Modèle',       // @btn-template-color
      preview: 'Aperçu',        // @btn-preview-color
      download: 'Télécharger',  // @btn-download-color
      batchImport: 'Importer par Lots', // @btn-batch-import-color
      batchExport: 'Exporter par Lots', // @btn-batch-export-color
      batchPrint: 'Imprimer par Lots', // @btn-batch-print-color
      batchEdit: 'Modifier par Lots',  // @btn-batch-edit-color
      batchUpdate: 'Mettre à Jour par Lots', // @btn-batch-update-color

      // === Boutons d\'Opération d\'État ===
      logging: 'Auditer',      // @btn-audit-color
      revoke: 'Révoquer',    // @btn-revoke-color
      stop: 'Arrêter',       // @btn-stop-color
      run: 'Exécuter',       // @btn-run-color
      force: 'Forcer',       // @btn-forced-color
      start: 'Démarrer',     // @btn-start-color
      suspend: 'Suspendre',  // @btn-suspend-color
      resume: 'Reprendre',   // @btn-resume-color
      submit: 'Soumettre',   // @btn-submit-color
      withdraw: 'Retirer',   // @btn-withdraw-color
      terminate: 'Terminer',  // @btn-terminate-color

      // === Boutons de Fonction Système ===
      generate: 'Générer',      // @btn-generate-color
      refresh: 'Actualiser',    // @btn-refresh-color
      info: 'Information',      // @btn-info-color
      log: 'Journal',           // @btn-log-color
      chat: 'Message',          // @btn-chat-color
      copy: 'Copier',           // @btn-copy-color
      execute: 'Exécuter',      // @btn-execute-color
      resetPwd: 'Réinitialiser le Mot de Passe', // @btn-reset-pwd-color
      open: 'Ouvrir',           // @btn-open-color
      close: 'Fermer',          // @btn-close-color
      more: 'Plus',             // @btn-more-color
      density: 'Densité',       // @btn-density-color
      columnSetting: 'Paramètres des Colonnes', // @btn-column-setting-color

      // === Boutons de Fonction Étendue ===
      search: 'Rechercher',     // @btn-search-color
      filter: 'Filtrer',        // @btn-filter-color
      sort: 'Trier',            // @btn-sort-color
      config: 'Configurer',     // @btn-config-color
      save: 'Enregistrer',      // @btn-save-color
      cancel: 'Annuler',        // @btn-cancel-color
      upload: 'Télécharger',    // @btn-upload-color
      print: 'Imprimer',        // @btn-print-color
      help: 'Aide',             // @btn-help-color
      share: 'Partager',        // @btn-share-color
      lock: 'Verrouiller',      // @btn-lock-color
      sync: 'Synchroniser',     // @btn-sync-color
      expand: 'Développer',     // @btn-expand-color
      collapse: 'Réduire',      // @btn-collapse-color
      approve: 'Approuver',     // @btn-approve-color
      reject: 'Rejeter',        // @btn-reject-color
      comment: 'Commenter',     // @btn-comment-color
      attach: 'Joindre',        // @btn-attach-color

      // === Boutons de Support Linguistique ===
      translate: 'Traduire',    // @btn-translate-color
      langSwitch: 'Changer de Langue', // @btn-lang-switch-color
      dict: 'Dictionnaire',     // @btn-dict-color

      // === Boutons d\'Analyse de Données ===
      analyze: 'Analyser',      // @btn-analyze-color
      chart: 'Graphique',       // @btn-chart-color
      report: 'Rapport',        // @btn-report-color
      dashboard: 'Tableau de Bord', // @btn-dashboard-color
      statistics: 'Statistiques', // @btn-statistics-color
      forecast: 'Prévision',    // @btn-forecast-color
      compare: 'Comparer',      // @btn-compare-color

      // === Boutons de Flux de Travail ===
      startFlow: 'Démarrer le Flux', // @btn-start-flow-color
      endFlow: 'Terminer le Flux', // @btn-end-flow-color
      suspendFlow: 'Suspendre le Flux', // @btn-suspend-flow-color
      resumeFlow: 'Reprendre le Flux', // @btn-resume-flow-color
      transfer: 'Transférer',    // @btn-transfer-color
      delegate: 'Déléguer',      // @btn-delegate-color
      notify: 'Notifier',        // @btn-notify-color
      urge: 'Urger',             // @btn-urge-color
      sign: 'Signer',            // @btn-sign-color
      countersign: 'Contresigner', // @btn-countersign-color

      // === Boutons Spécifiques aux Mobiles ===
      scan: 'Scanner',          // @btn-scan-color
      location: 'Localisation', // @btn-location-color
      call: 'Appeler',          // @btn-call-color
      photo: 'Prendre une Photo', // @btn-photo-color
      voice: 'Voix',            // @btn-voice-color
      faceId: 'ID Facial',      // @btn-face-id-color
      fingerPrint: 'Empreinte Digitale', // @btn-finger-print-color

      // === Boutons de Collaboration Sociale ===
      follow: 'Suivre',         // @btn-follow-color
      collect: 'Collecter',     // @btn-collect-color
      like: 'J\'aime',          // @btn-like-color
      forward: 'Transférer',    // @btn-forward-color
      at: '@',                  // @btn-at-color
      group: 'Groupe',          // @btn-group-color
      team: 'Équipe',           // @btn-team-color

      // === Boutons d\'Authentification de Sécurité ===
      verifyCode: 'Code de Vérification', // @btn-verify-code-color
      bind: 'Lier',             // @btn-bind-color
      unbind: 'Délier',         // @btn-unbind-color
      authorize: 'Autoriser',   // @btn-authorize-color
      deauthorize: 'Désautoriser', // @btn-deauthorize-color

      // === Boutons de Fonction Avancée ===
      version: 'Version',       // @btn-version-color
      history: 'Historique',    // @btn-history-color
      restore: 'Restaurer',     // @btn-restore-color
      archive: 'Archiver',      // @btn-archive-color
      unarchive: 'Désarchiver', // @btn-unarchive-color
      merge: 'Fusionner',       // @btn-merge-color
      split: 'Diviser',         // @btn-split-color

      // === Boutons de Gestion Système ===
      backup: 'Sauvegarder',    // @btn-backup-color
      restoreSys: 'Restaurer le Système', // @btn-restore-sys-color
      clean: 'Nettoyer',        // @btn-clean-color
      optimize: 'Optimiser',    // @btn-optimize-color
      monitor: 'Surveiller',    // @btn-monitor-color
      diagnose: 'Diagnostiquer', // @btn-diagnose-color
      maintain: 'Maintenir'     // @btn-maintain-color
    },

    // ==================== Résultats et Messages ====================
    // État du Résultat
    result: {
      success: 'Opération réussie',
      failed: 'Opération échouée',
      warning: 'Avertissement',
      info: 'Information',
      error: 'Erreur'
    },

    // Messages de Suggestion
    message: {
      loading: 'Traitement en cours...',
      saving: 'Enregistrement en cours...',
      submitting: 'Envoi en cours...',
      deleting: 'Suppression en cours...',
      operationSuccess: 'Opération réussie',
      operationFailed: 'Opération échouée',
      deleteConfirm: 'Êtes-vous sûr de vouloir supprimer ?',
      deleteSuccess: 'Suppression réussie',
      deleteFailed: 'Suppression échouée',
      createSuccess: 'Ajout réussi',
      createFailed: 'Ajout échoué',
      updateSuccess: 'Mise à jour réussie',
      updateFailed: 'Échec de la mise à jour',
      forceOfflineConfirm: 'Êtes-vous sûr de vouloir forcer cet utilisateur hors ligne ?',
      forceOfflineSuccess: 'Forçage hors ligne réussi',
      forceOfflineFailed: 'Échec du forçage hors ligne',
      networkError: 'Échec de la connexion réseau, veuillez vérifier le réseau',
      systemError: 'Erreur système',
      timeout: 'Délai d\'attente de la requête',
      invalidResponse: 'Format de données de réponse invalide',
      backendNotStarted: 'Service backend non démarré, veuillez vérifier l\'état du service',
      invalidRequest: 'Paramètres de requête invalides',
      unauthorized: 'Non autorisé, veuillez vous reconnecter',
      forbidden: 'Accès interdit',
      notFound: 'La ressource demandée n\'existe pas',
      serverError: 'Erreur interne du serveur',
      httpError: {
        400: 'Paramètres de requête invalides',
        401: 'Non autorisé, veuillez vous reconnecter',
        403: 'Accès interdit',
        404: 'La ressource demandée n\'existe pas',
        500: 'Erreur interne du serveur',
        502: 'Passerelle incorrecte',
        503: 'Service non disponible',
        504: 'Délai d\'attente de la passerelle'
      },
      loadFailed: 'Échec du chargement'
    },

    // ==================== Texte des Composants ====================
    // Onglets
    tab: {
      // === Informations de Base ===
      basic: 'Informations de Base',
      profile: 'Profil',
      avatar: 'Paramètres d\'Avatar',
      password: 'Paramètres de Mot de Passe',
      security: 'Paramètres de Sécurité',

      // === Génération de Code ===
      codegen: 'Génération de Code',
      codegenBasic: 'Configuration de Génération',
      codegenField: 'Configuration des Champs',
      codegenPreview: 'Aperçu de Génération',
      codegenTemplate: 'Configuration du Modèle',
      codegenImport: 'Configuration d\'Importation',
      
      // === Flux de Travail ===
      workflow: 'Flux de Travail',
      flowDesign: 'Conception de Flux',
      flowDeploy: 'Déploiement de Flux',
      flowInstance: 'Instance de Flux',
      flowTask: 'Gestion des Tâches',
      flowForm: 'Configuration de Formulaire',
      flowNotify: 'Notification de Messages',
      
      // === Gestion Système ===
      permission: 'Permission de Données',
      log: 'Journal des Opérations',
      dept: 'Département et Poste',
      role: 'Rôle et Permission',
      config: 'Configuration des Paramètres',
      task: 'Tâche Planifiée',
      monitor: 'Moniteur Système'
    },

    // Texte des Boutons
    button: {
      submit: 'Soumettre',
      confirm: 'Confirmer',
      ok: 'OK',
      cancel: 'Annuler',
      close: 'Fermer',
      reset: 'Réinitialiser',
      clear: 'Effacer',
      save: 'Enregistrer',
      delete: 'Supprimer'
    }
  },

  // ==================== Composant Sélecteur ====================
  select: {
    loadMore: 'Charger Plus',
    loading: 'Chargement...',
    notFound: 'Aucune donnée',
    selected: '{count} éléments sélectionnés',
    selectedTotal: 'Total {total} éléments',
    clear: 'Effacer',
    search: 'Rechercher',
    all: 'Tout',
    // Messages d\'Erreur
    loadError: 'Échec du chargement des données',
    searchError: 'Échec de la recherche',
    networkError: 'Échec de la connexion réseau',
    serverError: 'Erreur serveur',
    unknownError: 'Erreur inconnue'
  }
}


