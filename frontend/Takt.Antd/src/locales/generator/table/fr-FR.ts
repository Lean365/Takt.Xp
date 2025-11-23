export default {
  generator: {
    table: {
      title: 'Génération de Code',
      list: {
        title: 'Liste de Génération de Code',
        search: {
          name: 'Nom de la Table',
          comment: 'Description de la Table'
        },
        table: {
          tableId: 'ID de la Table',
          databaseName: 'Nom de la Base de Données',
          tableName: 'Nom de la Table',
          tableComment: 'Description de la Table',
          className: 'Nom de la Classe',
          namespace: 'Espace de Noms',
          baseNamespace: 'Espace de Noms de Base',
          csharpTypeName: 'Nom du Type C#',
          parentTableName: 'Nom de la Table Parente',
          parentTableFkName: 'Clé Étrangère de la Table Parente',
          status: 'Statut',
          templateType: 'Type de Modèle',
          moduleName: 'Nom du Module',
          businessName: 'Nom du Business',
          functionName: 'Nom de la Fonction',
          author: 'Auteur',
          genMode: 'Mode de Génération',
          genPath: 'Chemin de Génération',
          options: 'Options',
          createBy: 'Créé par',
          createTime: 'Date de Création',
          updateBy: 'Mis à jour par',
          updateTime: 'Date de Mise à Jour',
          remark: 'Remarque',
          isDeleted: 'Supprimé'
        },
        actions: {
          create: 'Créer',
          edit: 'Modifier',
          delete: 'Supprimer',
          view: 'Voir',
          generate: 'Générer le Code',
          sync: 'Synchroniser la Table',
          import: 'Importer',
          export: 'Exporter',
          template: 'Télécharger le Modèle',
          refresh: 'Actualiser'
        },
        status: {
          enabled: 'Activé',
          disabled: 'Désactivé'
        }
      },
      form: {
        title: 'Formulaire de Génération de Code',
        tab: {
          basic: 'Informations de Base',
          generate: 'Informations de Génération',
          field: 'Informations des Champs'
        },
        basic: {
          title: 'Informations de Base',
          tableName: 'Nom de la Table',
          tableComment: 'Description de la Table',
          className: 'Nom de la Classe',
          namespace: 'Espace de Noms',
          baseNamespace: 'Espace de Noms de Base',
          csharpTypeName: 'Nom du Type C#',
          parentTableName: 'Nom de la Table Parente',
          parentTableFkName: 'Clé Étrangère de la Table Parente',
          status: 'Statut',
          author: 'Auteur',
          remark: 'Remarque'
        },
        generate: {
          title: 'Informations de Génération',
          moduleName: 'Nom du Module',
          packageName: 'Chemin du Package',
          businessName: 'Nom du Business',
          functionName: 'Nom de la Fonction',
          parentMenuId: 'Menu Parent',
          tplCategory: 'Type de Modèle',
          genPath: 'Chemin de Génération',
          options: 'Options de Génération',
          tplCategoryOptions: {
            crud: 'Table Unique (CRUD)',
            tree: 'Table Arborescente (CRUD)',
            sub: 'Table Maître-Détail (CRUD)'
          },
          optionsItems: {
            treeCode: 'Champ Code Arborescent',
            treeParentCode: 'Champ Code Parent Arborescent',
            treeName: 'Champ Nom Arborescent',
            parentMenuId: 'Menu Parent',
            query: 'Rechercher',
            add: 'Ajouter',
            edit: 'Modifier',
            delete: 'Supprimer',
            import: 'Importer',
            export: 'Exporter'
          }
        },
        field: {
          title: 'Informations des Champs',
          columnName: 'Nom de la Colonne',
          columnComment: 'Description de la Colonne',
          columnType: 'Type de la Colonne',
          csharpType: 'Type C#',
          csharpField: 'Champ C#',
          isRequired: 'Obligatoire',
          isInsert: 'Insertion',
          isEdit: 'Modification',
          isList: 'Liste',
          isQuery: 'Recherche',
          queryType: 'Type de Requête',
          htmlType: 'Type d\'Affichage',
          dictType: 'Type de Dictionnaire',
          queryTypeOptions: {
            EQ: 'Égal',
            NE: 'Non Égal',
            GT: 'Supérieur',
            GTE: 'Supérieur ou Égal',
            LT: 'Inférieur',
            LTE: 'Inférieur ou Égal',
            LIKE: 'Contient',
            BETWEEN: 'Entre',
            IN: 'Dans'
          },
          htmlTypeOptions: {
            input: 'Champ de Saisie',
            textarea: 'Zone de Texte',
            select: 'Liste Déroulante',
            radio: 'Boutons Radio',
            checkbox: 'Cases à Cocher',
            datetime: 'Date et Heure',
            imageUpload: 'Téléchargement d\'Image',
            fileUpload: 'Téléchargement de Fichier',
            editor: 'Éditeur de Texte Riche'
          }
        },
        buttons: {
          submit: 'Soumettre',
          cancel: 'Annuler'
        },
        name: 'Nom de la Table',
        comment: 'Description de la Table',
        className: 'Nom de la Classe',
        namespace: 'Espace de Noms',
        baseNamespace: 'Espace de Noms de Base',
        csharpTypeName: 'Nom du Type C#',
        parentTableName: 'Nom de la Table Parente',
        parentTableFkName: 'Clé Étrangère de la Table Parente',
        moduleName: 'Nom du Module',
        businessName: 'Nom du Business',
        functionName: 'Nom de la Fonction',
        author: 'Auteur',
        genMode: 'Mode de Génération',
        genPath: 'Chemin de Génération',
        options: 'Options'
      },
      detail: {
        title: 'Détails de Génération de Code',
        basic: {
          title: 'Informations de Base',
          tableName: 'Nom de la Table',
          tableComment: 'Description de la Table',
          className: 'Nom de la Classe',
          namespace: 'Espace de Noms',
          baseNamespace: 'Espace de Noms de Base',
          csharpTypeName: 'Nom du Type C#',
          parentTableName: 'Nom de la Table Parente',
          parentTableFkName: 'Clé Étrangère de la Table Parente',
          status: 'Statut',
          createTime: 'Date de Création',
          updateTime: 'Date de Mise à Jour'
        },
        generate: {
          title: 'Informations de Génération',
          moduleName: 'Nom du Module',
          packageName: 'Chemin du Package',
          businessName: 'Nom du Business',
          functionName: 'Nom de la Fonction',
          parentMenuId: 'Menu Parent',
          tplCategory: 'Type de Modèle',
          genPath: 'Chemin de Génération',
          options: 'Options de Génération'
        },
        field: {
          title: 'Informations des Champs',
          columnName: 'Nom de la Colonne',
          columnComment: 'Description de la Colonne',
          columnType: 'Type de la Colonne',
          csharpType: 'Type C#',
          csharpField: 'Champ C#',
          isRequired: 'Obligatoire',
          isInsert: 'Insertion',
          isEdit: 'Modification',
          isList: 'Liste',
          isQuery: 'Recherche',
          queryType: 'Type de Requête',
          htmlType: 'Type d\'Affichage',
          dictType: 'Type de Dictionnaire'
        },
        actions: {
          edit: 'Modifier',
          back: 'Retour'
        },
        columnInfo: 'Informations de la Colonne',
        javaType: 'Type Java',
        javaField: 'Champ Java',
        yes: 'Oui',
        no: 'Non'
      },
      name: 'Nom de la Table',
      comment: 'Description de la Table',
      databaseName: 'Nom de la Base de Données',
      className: 'Nom de la Classe',
      namespace: 'Espace de Noms',
      baseNamespace: 'Espace de Noms de Base',
      csharpTypeName: 'Nom du Type C#',
      parentTableName: 'Nom de la Table Parente',
      parentTableFkName: 'Clé Étrangère de la Table Parente',
      status: 'Statut',
      templateType: 'Type de Modèle',
      moduleName: 'Nom du Module',
      businessName: 'Nom du Business',
      functionName: 'Nom de la Fonction',
      author: 'Auteur',
      genMode: 'Mode de Génération',
      genPath: 'Chemin de Génération',
      options: 'Options',
      createBy: 'Créé par',
      createTime: 'Date de Création',
      updateBy: 'Mis à jour par',
      updateTime: 'Date de Mise à Jour',
      remark: 'Remarque',
      isDeleted: 'Supprimé',
      placeholder: {
        name: 'Veuillez saisir le nom de la table',
        comment: 'Veuillez saisir la description de la table'
      },
      preview: {
        title: 'Aperçu du Code',
        copy: 'Copier le Code',
        download: 'Télécharger le Code',
        showLineNumbers: 'Afficher les Numéros de Ligne',
        hideLineNumbers: 'Masquer les Numéros de Ligne',
        copySuccess: 'Copie Réussie',
        copyFailed: 'Échec de la Copie',
        downloadSuccess: 'Téléchargement Réussi',
        downloadFailed: 'Échec du Téléchargement',
        tab: {
          java: 'Code Java',
          vue: 'Code Vue',
          sql: 'Code SQL',
          domain: 'Classe d\'Entité',
          mapper: 'Interface Mapper',
          mapperXml: 'Mapper XML',
          service: 'Interface de Service',
          serviceImpl: 'Implémentation du Service',
          controller: 'Contrôleur',
          api: 'Interface API',
          index: 'Page de Liste',
          form: 'Page de Formulaire'
        },
        entities: {
          title: 'Code de Classe d\'Entité'
        },
        services: {
          title: 'Code d\'Interface de Service'
        },
        controllers: {
          title: 'Code de Contrôleur'
        },
        vue: {
          title: 'Code Vue'
        },
        dtos: {
          title: 'Code DTO'
        },
        types: {
          title: 'Code de Définition de Type'
        },
        locales: {
          title: 'Code de Traduction'
        }
      },
      import: {
        title: 'Importer la Table',
        database: 'Base de Données',
        table: {
          name: 'Nom de la Table',
          comment: 'Description de la Table',
          action: 'Action'
        },
        column: {
          title: 'Importer la Colonne',
          tableName: 'Nom de la Table',
          tableId: 'ID de la Table',
          columnName: 'Nom de la Colonne',
          propertyName: 'Nom de la Propriété',
          columnType: 'Type de la Colonne',
          propertyType: 'Type de la Propriété',
          isNullable: 'Nullable',
          isPrimaryKey: 'Clé Primaire',
          isAutoIncrement: 'Auto-incrémentation',
          defaultValue: 'Valeur par Défaut',
          columnComment: 'Description de la Colonne',
          value: 'Valeur',
          decimalDigits: 'Décimales',
          scale: 'Échelle',
          isArray: 'Tableau',
          isJson: 'Json',
          isUnsigned: 'Non Signé',
          createTableFieldSort: 'Tri des Champs de Table',
          insertServerTime: 'Heure d\'Insertion Serveur',
          insertSql: 'SQL d\'Insertion',
          updateServerTime: 'Heure de Mise à Jour Serveur',
          updateSql: 'SQL de Mise à Jour',
          sqlParameterDbType: 'Type de Paramètre SQL DB'
        }
      },
      message: {
        generateSuccess: 'Génération de code réussie',
        generateFailed: 'Échec de la génération de code',
        syncSuccess: 'Synchronisation de la table réussie',
        syncFailed: 'Échec de la synchronisation de la table',
        importSuccess: 'Importation réussie',
        importFailed: 'Échec de l\'importation',
        exportSuccess: 'Exportation réussie',
        exportFailed: 'Échec de l\'exportation',
        templateSuccess: 'Téléchargement du modèle réussi',
        templateFailed: 'Échec du téléchargement du modèle',
        selectDatabase: 'Veuillez d\'abord sélectionner la base de données',
        selectTable: 'Veuillez sélectionner les tables à importer',
        tableNameRequired: 'Le nom de la table est requis',
        importTimeout: 'Délai d\'importation dépassé, veuillez réessayer plus tard'
      },
      tab: {
        basic: 'Informations de Base',
        generate: 'Informations de Génération',
        field: 'Informations des Champs'
      },
      required: {
        name: 'Veuillez saisir le nom de la table',
        comment: 'Veuillez saisir la description de la table',
        className: 'Veuillez saisir le nom de la classe',
        namespace: 'Veuillez saisir l\'espace de noms',
        baseNamespace: 'Veuillez saisir l\'espace de noms de base',
        csharpTypeName: 'Veuillez saisir le nom du type C#',
        moduleName: 'Veuillez saisir le nom du module',
        businessName: 'Veuillez saisir le nom du business',
        functionName: 'Veuillez saisir le nom de la fonction',
        author: 'Veuillez saisir le nom de l\'auteur',
        genMode: 'Veuillez sélectionner le mode de génération',
        genPath: 'Veuillez saisir le chemin de génération'
      }
    }
  }
} 