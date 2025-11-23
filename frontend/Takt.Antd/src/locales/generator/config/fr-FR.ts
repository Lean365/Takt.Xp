export default {
    generator: {
      config: {
        title: 'Configuration de Génération de Code',  
        fields: {
            genConfigName: 'Nom de Configuration',
            author: 'Auteur',
            moduleName: 'Nom du Module',
            projectName: 'Nom du Projet',
            businessName: 'Nom de l\'Entreprise',
            functionName: 'Nom de la Fonction',
            genMethod: 'Méthode de Génération',
            genTplType: 'Type de Modèle',
            genPath: 'Chemin',
            options: 'Options',
            status: 'Statut',
            dateRange: 'Plage de Dates',
        },
        placeholder: {
            genConfigName: 'Veuillez saisir le nom de configuration',
            author: 'Veuillez saisir l\'auteur',
            moduleName: 'Veuillez saisir le nom du module',
            projectName: 'Veuillez saisir le nom du projet',
            businessName: 'Veuillez saisir le nom de l\'entreprise',
            functionName: 'Veuillez saisir le nom de la fonction',
            genMethod: 'Veuillez sélectionner la méthode de génération',
            genTplType: 'Veuillez sélectionner le type de modèle',
            genPath: 'Veuillez saisir le chemin',
            options: 'Veuillez saisir les options',
            status: 'Veuillez sélectionner le statut',
            dateRange: 'Veuillez sélectionner la plage de dates',
            validation: {
                length: 'La longueur de {{field}} ne peut pas dépasser {{length}}',
                required: 'Veuillez saisir {{field}}',
                minlength: 'La longueur de {{field}} ne peut pas être inférieure à {{min}}',
                maxlength: 'La longueur de {{field}} ne peut pas être supérieure à {{max}}',
                pattern: 'Le format de {{field}} est incorrect',
            }
        },
        messages: {
            success: 'Opération réussie',
            error: 'Opération échouée',
            warning: 'Avertissement d\'opération',
            info: 'Informations d\'opération',
        }
    }
} }