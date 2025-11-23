export default {
  identity: {
    auth: {
      // Définitions des champs de formulaire - tous les champs de formulaire sont validés de manière unifiée dans fields
      fields: {
        username: {
          label: 'Nom d\'utilisateur',
          placeholder: 'Veuillez saisir le nom d\'utilisateur',
          validation: {
            required: 'Veuillez saisir le nom d\'utilisateur',
            length: 'La longueur du nom d\'utilisateur doit être entre 3 et 50 caractères'
          }
        },
        password: {
          label: 'Mot de passe',
          placeholder: 'Veuillez saisir le mot de passe',
          validation: {
            required: 'Veuillez saisir le mot de passe',
            length: 'La longueur du mot de passe doit être entre 6 et 100 caractères'
          }
        },
        email: {
          label: 'Email',
          placeholder: 'Veuillez saisir l\'email',
          validation: {
            required: 'Veuillez saisir l\'email',
            format: 'Veuillez saisir un format d\'email correct'
          }
        },
        phone: {
          label: 'Numéro de téléphone',
          placeholder: 'Veuillez saisir le numéro de téléphone',
          validation: {
            required: 'Veuillez saisir le numéro de téléphone',
            format: 'Veuillez saisir un format de numéro de téléphone correct'
          }
        },
        captcha: {
          label: 'Code de vérification',
          placeholder: 'Veuillez saisir le code de vérification',
          validation: {
            required: 'Veuillez saisir le code de vérification'
          }
        },
        confirmPassword: {
          label: 'Confirmer le mot de passe',
          placeholder: 'Veuillez saisir le mot de passe à nouveau',
          validation: {
            required: 'Veuillez confirmer le mot de passe',
            mismatch: 'Les deux mots de passe saisis ne correspondent pas'
          }
        },
        nickName: {
          label: 'Pseudonyme',
          placeholder: 'Veuillez saisir le pseudonyme',
          validation: {
            required: 'Le pseudonyme ne peut pas être vide',
            format: '2-50 caractères, autorise le chinois, le japonais, le coréen, l\'anglais, les chiffres, les points et les tirets, interdit les soulignements et les espaces'
          }
        },
        realName: {
          label: 'Nom réel',
          placeholder: 'Veuillez saisir le nom réel',
          validation: {
            required: 'Le nom réel ne peut pas être vide',
            format: 'La longueur du nom réel doit être entre 2 et 20 caractères, ne peut contenir que du chinois, de l\'anglais, des chiffres et des soulignements'
          }
        },
        fullName: {
          label: 'Nom complet',
          placeholder: 'Veuillez saisir le nom complet',
          validation: {
            required: 'Le nom complet ne peut pas être vide',
            format: 'La longueur du nom complet doit être entre 2 et 20 caractères, ne peut contenir que du chinois, de l\'anglais, des chiffres et des soulignements'
          }
        },
        englishName: {
          label: 'Nom anglais',
          placeholder: 'Veuillez saisir le nom anglais',
          validation: {
            required: 'Le nom anglais ne peut pas être vide',
            format: 'La longueur du nom anglais doit être entre 2 et 100 caractères, doit commencer par une lettre, ne peut contenir que des lettres anglaises, des espaces, des tirets et des points'
          }
        },
        verificationCode: {
          label: 'Code de vérification',
          placeholder: 'Veuillez saisir le code de vérification à 6 chiffres',
          validation: {
            required: 'Veuillez saisir le code de vérification',
            length: 'Le code de vérification doit être à 6 chiffres',
            format: 'Le code de vérification doit être à 6 chiffres'
          }
        },
        newPassword: {
          label: 'Nouveau mot de passe',
          placeholder: 'Veuillez saisir le nouveau mot de passe',
          validation: {
            required: 'Veuillez saisir le nouveau mot de passe',
            length: 'La longueur du mot de passe doit être entre 6 et 20 caractères',
            format: 'Le mot de passe doit contenir des majuscules, des minuscules et des chiffres'
          }
        },
        gender: {
          label: 'Sexe',
          placeholder: 'Veuillez sélectionner le sexe',
          validation: {
            required: 'Veuillez sélectionner le sexe'
          },
          options: {
            unknown: 'Confidentiel',
            male: 'Homme',
            female: 'Femme'
          }
        },
        userType: {
          label: 'Type d\'utilisateur',
          placeholder: 'Veuillez sélectionner le type d\'utilisateur',
          validation: {
            required: 'Veuillez sélectionner le type d\'utilisateur'
          },
          options: {
            normal: 'Utilisateur normal',
            admin: 'Administrateur'
          }
        },
        status: {
          label: 'Statut',
          placeholder: 'Veuillez sélectionner le statut',
          validation: {
            required: 'Veuillez sélectionner le statut'
          },
          options: {
            normal: 'Normal',
            disabled: 'Désactivé'
          }
        },
        deptId: {
          label: 'Département',
          placeholder: 'Veuillez saisir l\'ID du département',
          validation: {
            required: 'Veuillez saisir l\'ID du département'
          }
        },
        roleIds: {
          label: 'Rôles',
          placeholder: 'Veuillez sélectionner les rôles',
          validation: {
            required: 'Veuillez sélectionner les rôles'
          }
        },
        postIds: {
          label: 'Postes',
          placeholder: 'Veuillez sélectionner les postes',
          validation: {
            required: 'Veuillez sélectionner les postes'
          }
        },
        deptIds: {
          label: 'Départements d\'appartenance',
          placeholder: 'Veuillez sélectionner les départements d\'appartenance',
          validation: {
            required: 'Veuillez sélectionner les départements d\'appartenance'
          }
        },
        remark: {
          label: 'Remarques',
          placeholder: 'Veuillez saisir les remarques'
        }
      },
      
      // Connexion
      login: {
        title: 'Connexion',
        rememberMe: 'Se souvenir',
        forgotPassword: 'Mot de passe oublié ?',
        submit: 'Se connecter',
        success: 'Connexion réussie',
        noAccount: 'Vous n\'avez pas encore de compte ?',
        registerNow: 'S\'inscrire maintenant',
        notAvailable: 'Fonctionnalité temporairement indisponible',
        error: {
          invalidCredentials: 'Nom d\'utilisateur ou mot de passe incorrect',
          userDisabled: 'Utilisateur désactivé',
          userNotFound: 'Utilisateur inexistant',
          serverError: 'Erreur serveur, veuillez réessayer plus tard',
          unknown: 'Échec de connexion, veuillez réessayer plus tard'
        }
      },
      
      // Enregistrement d'utilisateur
      register: {
        title: 'Enregistrement d\'utilisateur',
        subtitle: 'Veuillez compléter l\'enregistrement d\'utilisateur étape par étape',
        step1: 'Vérification d\'identité',
        step2: 'Informations de base',
        step3: 'Informations détaillées',
        step4: 'Configuration des permissions',
        roleUser: 'Utilisateur',
        roleAdmin: 'Administrateur',
        postEmployee: 'Employé',
        postManager: 'Manager',
        deptIT: 'Département IT',
        deptHR: 'Département RH',
        agreement: 'J\'ai lu et j\'accepte',
        agreementPrefix: 'J\'ai lu et j\'accepte',
        agreementLink: 'les conditions d\'utilisation',
        agreementSuffix: '',
        agreementTitle: 'Accord d\'enregistrement utilisateur',
        agreementContent: 'Veuillez lire attentivement et accepter cet accord avant l\'enregistrement.',
        submit: 'Terminer l\'enregistrement',
        nextStep: 'Étape suivante',
        back: 'Étape précédente',
        backToLogin: 'Retour à la connexion',
        login: 'Se connecter avec un compte existant',
        confirmPassword: 'Confirmer le mot de passe',
        confirmPasswordPlaceholder: 'Veuillez saisir le mot de passe à nouveau',
        deptId: 'ID du département',
        deptIdPlaceholder: 'Veuillez saisir l\'ID du département',
        roleIds: 'Rôles',
        roleIdsPlaceholder: 'Veuillez sélectionner les rôles',
        postIds: 'Postes',
        postIdsPlaceholder: 'Veuillez sélectionner les postes',
        deptIds: 'Départements',
        deptIdsPlaceholder: 'Veuillez sélectionner les départements',
        remark: 'Remarque',
        remarkPlaceholder: 'Veuillez saisir une remarque (optionnel)',
        success: 'Enregistrement réussi',
        successTitle: 'Enregistrement réussi',
        successSubtitle: 'Votre compte a été créé avec succès, veuillez vous connecter avec votre nouveau compte',
        successMessage: 'L\'utilisateur {userName} a été enregistré avec succès',
        step1Success: 'Vérification d\'identité réussie',
        step2Success: 'Vérification du code de vérification réussie',
        step3Success: 'Complétion des informations terminée',
        step4Success: 'Configuration des permissions terminée',
        form: {
          agreementRequired: 'Veuillez lire et accepter les conditions d\'utilisation'
        },
        error: {
          step1Failed: 'Échec de la vérification d\'identité',
          step2Failed: 'Échec de la vérification du code de vérification',
          step3Failed: 'Échec de la complétion des informations',
          step4Failed: 'Échec de la configuration des permissions',
          unknown: 'Échec de l\'enregistrement, veuillez réessayer plus tard'
        }
      },
      
      // Récupération de mot de passe
      passwordRecovery: {
        title: 'Récupération de mot de passe',
        subtitle: 'Récupérez votre mot de passe via la vérification par email',
        step1: 'Code de vérification',
        step2: 'Utilisateur et email',
        step3: 'Code de vérification email',
        step4: 'Réinitialisation du mot de passe',
        userName: 'Nom d\'utilisateur',
        userNamePlaceholder: 'Veuillez saisir votre nom d\'utilisateur',
        email: 'Adresse email',
        emailPlaceholder: 'Veuillez saisir votre adresse email',
        refreshCaptcha: 'Actualiser le code de vérification',
        nextStep: 'Étape suivante',
        emailSent: 'Code de vérification envoyé',
        emailSentDesc: 'Le code de vérification a été envoyé à {email}, veuillez vérifier',
        verify: 'Vérifier',
        resendCode: 'Renvoyer',
        resetPassword: 'Réinitialiser le mot de passe',
        successTitle: 'Réinitialisation du mot de passe réussie',
        successSubtitle: 'Votre mot de passe a été réinitialisé avec succès, veuillez vous connecter avec votre nouveau mot de passe',
        backToLogin: 'Retour à la connexion',
        back: 'Étape précédente',
        identityVerified: 'Vérification d\'identité réussie',
        codeSent: 'Code de vérification envoyé avec succès',
        emailVerified: 'Vérification email réussie',
        passwordResetSuccess: 'Réinitialisation du mot de passe réussie',
        captchaVerified: 'Vérification du code de vérification réussie',
        successMessage: 'Le mot de passe de l\'utilisateur {userName} a été réinitialisé avec succès',
        form: {
          emailRequired: 'Veuillez saisir l\'adresse email',
          userNameLength: 'La longueur du nom d\'utilisateur doit être entre 3 et 20 caractères'
        },
        error: {
          identityVerificationFailed: 'Échec de la vérification d\'identité',
          sendCodeFailed: 'Échec de l\'envoi du code de vérification',
          emailVerificationFailed: 'Échec de la vérification email',
          passwordResetFailed: 'Échec de la réinitialisation du mot de passe',
          captchaVerificationFailed: 'Échec de la vérification du code de vérification',
          emailMismatch: 'Le nom d\'utilisateur et l\'adresse email ne correspondent pas',
          invalidCode: 'Code de vérification incorrect ou expiré',
          codeCooldown: 'Envoi du code de vérification trop fréquent, veuillez réessayer plus tard'
        }
      },
      
      // Code de vérification
      captcha: {
        title: 'Vérification de sécurité',
        error: {
          initFailed: 'Échec de l\'initialisation du code de vérification'
        }
      },
      
      // SMS和OAuth登录功能已移除
      
      // Empreinte de l'appareil
      device: {
        getDeviceInfo: 'Informations d\'empreinte de l\'appareil obtenues',
        deviceFingerprintType: 'Type d\'empreinte de l\'appareil',
        deviceFingerprintNull: 'L\'empreinte de l\'appareil est-elle null',
        deviceFingerprintUndefined: 'L\'empreinte de l\'appareil est-elle undefined',
        deviceFingerprintKeyCount: 'Nombre de clés d\'empreinte de l\'appareil',
        deviceFingerprintKeyList: 'Liste des clés d\'empreinte de l\'appareil',
        deviceFingerprintField: 'Champ loginFingerprint de l\'empreinte de l\'appareil',
        loginParamsDetail: 'Détails des paramètres de connexion construits',
        deviceFingerprintDetail: 'Informations détaillées de l\'empreinte de l\'appareil',
        empty: 'Vide',
        backendHandled: 'Vide (traité par le backend)',
        set: 'Défini',
        initSuccess: 'Initialisation des informations de l\'appareil réussie',
        initFailed: 'Échec de l\'initialisation des informations de l\'appareil',
        collectionComponent: {
          title: 'Empreinte de l\'appareil',
          description: 'Collecte d\'informations sur l\'appareil à l\'aide de l\'API Web native',
          collecting: 'Collecte en cours...',
          collectDeviceInfo: 'Collecter les informations de l\'appareil',
          clearInfo: 'Effacer les informations',
          collectingInfo: 'Collecte des informations de l\'appareil en cours...',
          deviceInfo: 'Informations de l\'appareil :',
          deviceId: 'ID de l\'appareil :',
          deviceType: 'Type d\'appareil :',
          deviceFingerprint: 'Empreinte de l\'appareil :',
          platform: 'Plateforme :',
          language: 'Langue :',
          timezone: 'Fuseau horaire :',
          screenResolution: 'Résolution d\'écran :',
          colorDepth: 'Profondeur de couleur :',
          pixelRatio: 'Rapport de pixels de l\'appareil :',
          cpuCores: 'Nombre de cœurs CPU :',
          deviceMemory: 'Mémoire de l\'appareil :',
          touchSupport: 'Support tactile :',
          os: 'Système d\'exploitation :',
          browser: 'Navigateur :',
          vpnDetection: 'Détection VPN :',
          proxyDetection: 'Détection de proxy :',
          networkType: 'Type de réseau :',
          cookieSupport: 'Support des cookies :',
          notGenerated: 'Non généré',
          notCollected: 'Non collecté',
          notDetected: 'Non détecté',
          supported: 'Supporté',
          notSupported: 'Non supporté',
          bits: 'bits',
          copyToClipboard: 'Copier dans le presse-papiers',
          copySuccess: 'Informations de l\'appareil copiées dans le presse-papiers',
          copyFailed: 'Échec de la copie, veuillez copier manuellement',
          errorInfo: 'Informations d\'erreur',
          startCollection: 'Début de la collecte des informations de l\'appareil...',
          collectionSuccess: 'Collecte des informations de l\'appareil réussie',
          collectionFailed: 'Échec de la collecte des informations de l\'appareil',
          copyError: 'Échec de la copie'
        }
      },
      
      // Validation
      validation: {
        usernamePasswordRequired: 'Le nom d\'utilisateur et le mot de passe ne peuvent pas être vides',
        deviceInfoRequired: 'Les informations d\'empreinte de l\'appareil ne peuvent pas être vides',
        deviceFingerprintRequired: 'L\'empreinte de l\'appareil ne peut pas être vide',
        userAgentRequired: 'L\'agent utilisateur ne peut pas être vide',
        loginTypeSourceRequired: 'Le type de connexion et la source ne peuvent pas être vides'
      },
      
      // Flux de connexion
      loginFlow: {
        paramsSerialized: 'Longueur des paramètres sérialisés',
        paramsPreview: 'Aperçu des paramètres sérialisés',
        paramsTooLarge: 'Paramètres de connexion trop volumineux, peuvent causer des problèmes de performance',
        serializationFailed: 'Échec de la sérialisation des paramètres',
        forceOfflineSuccess: 'Autres appareils exclus, connexion réussie',
        loginCancelled: 'Connexion annulée',
        singleUserCheckFailed: 'Vérification de connexion utilisateur unique échouée, continuation du flux de connexion normal',
        loginFailed: 'Échec de connexion',
        signalRInit: 'Début de l\'initialisation de la connexion SignalR',
        signalRInitSuccess: 'Initialisation de la connexion SignalR réussie',
        signalRInitFailed: 'Échec de l\'initialisation de la connexion SignalR',
        autoLogoutInit: 'Début de l\'initialisation de la fonction de déconnexion automatique',
        autoLogoutInitSuccess: 'Initialisation de la fonction de déconnexion automatique réussie',
        autoLogoutInitFailed: 'Échec de l\'initialisation de la fonction de déconnexion automatique',
        pageInitFailed: 'Échec de l\'initialisation de la page de connexion',
        pageInitError: 'Échec de l\'initialisation de la page, veuillez actualiser et réessayer',
        checkLockStatusFailed: 'Échec de la vérification du statut de verrouillage du compte',
        singleUserCheck: {
          title: 'Détection de connexion utilisateur unique',
          content: 'Détecté que votre compte est déjà connecté sur d\'autres appareils (nombre d\'appareils en ligne : {onlineDeviceCount}).\n\n{reason}Voulez-vous exclure les autres appareils et continuer la connexion ?',
          kickout: 'Exclure les autres appareils',
          cancel: 'Annuler la connexion'
        }
      },
      
      // Configuration
      config: {
        loading: 'Chargement de la configuration...',
        loadingLoginConfig: 'Chargement de la configuration de connexion, veuillez patienter...',
        captchaConfigSuccess: 'Configuration du code de vérification chargée avec succès',
        captchaConfigFailed: 'Échec de l\'obtention de la configuration du code de vérification',
        captchaConfigError: 'Échec de l\'obtention de la configuration du code de vérification backend',
        loginMethodConfigSuccess: 'Configuration de la méthode de connexion chargée avec succès',
        loginMethodConfigError: 'Échec de l\'obtention de la configuration de la méthode de connexion backend',
        loginMethodConfigFailed: 'Échec de l\'obtention de la configuration de la méthode de connexion'
      },
      
      // Composant code de vérification
      captchaComponent: {
        refreshSuccess: 'Code de vérification actualisé',
        refreshFailed: 'Échec de l\'actualisation du code de vérification',
        getFailed: 'Échec de l\'obtention du code de vérification',
        verifySuccess: 'Vérification du code de vérification réussie',
        invalidParams: 'Paramètres du code de vérification invalides',
        statusUpdated: 'Statut du code de vérification mis à jour',
        processError: 'Erreur lors du traitement du rappel de succès du code de vérification',
        processFailed: 'Traitement du code de vérification échoué, veuillez réessayer',
        verifyFailed: 'Échec de la vérification du code de vérification',
        errorProcessFailed: 'Échec du traitement d\'erreur du code de vérification',
        initFailed: 'Échec de l\'initialisation du code de vérification',
        error: 'Erreur du code de vérification'
      },
      
      // Méthodes de connexion supprimées
      
      // Erreurs
      error: {
        permanentlyLocked: 'Compte verrouillé de façon permanente, veuillez contacter l\'administrateur',
        tooManyAttempts: 'Trop d\'échecs de connexion, compte verrouillé'
      }
    }
  }
}
