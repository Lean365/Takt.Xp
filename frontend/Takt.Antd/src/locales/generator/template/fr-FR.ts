export default {
    generator: {
      template: {
        title: 'Modèle de Génération de Code',  
        fields: {
            templateName: 'Nom du Modèle',
            templateOrmType: 'Framework ORM',
            templateCodeType: 'Type de Code',
            templateCategory: 'Catégorie de Modèle',
            templateLanguage: 'Langage de Programmation',
            templateVersion: 'Numéro de Version',
            fileName: 'Nom de Fichier',
            templateContent: 'Contenu du Modèle',
            status: 'Statut',
            remark: 'Remarque',
        },
        placeholder: {
            templateName: 'Veuillez saisir le nom du modèle',
            templateOrmType: 'Veuillez sélectionner le framework ORM',
            templateCodeType: 'Veuillez sélectionner le type de code',
            templateCategory: 'Veuillez sélectionner la catégorie de modèle',
            templateLanguage: 'Veuillez sélectionner le langage de programmation',
            templateVersion: 'Veuillez saisir le numéro de version',
            fileName: 'Veuillez saisir le nom de fichier',
            templateContent: 'Veuillez saisir le contenu du modèle',
            remark: 'Veuillez saisir la remarque',
            validation: {
                required: 'Veuillez saisir {field}',
                length: 'La longueur de {field} ne peut pas dépasser {length} caractères',
                minLength: 'La longueur de {field} ne peut pas être inférieure à {min} caractères',
                pascalCase: '{field} doit utiliser le cas Pascal (première lettre majuscule, lettres uniquement)'
            }
        },
        dialog: {
            create: 'Ajouter un Modèle',
            update: 'Modifier le Modèle'
        },
        messages: {
            success: 'Opération réussie',
            error: 'Opération échouée',
            warning: 'Avertissement d\'opération',
            info: 'Informations d\'opération',
            confirm: 'Confirmer',
        },
    }
}
}