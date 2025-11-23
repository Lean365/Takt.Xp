export default {
  routine: {
    file: {
      fileName: 'Nom du fichier',
      fileType: 'Type de fichier',
      fileSize: 'Taille du fichier',
      filePath: 'Chemin du fichier',
      fileUrl: 'URL du fichier',
      fileMd5: 'Fichier MD5',
      fileOriginalName: 'Nom original du fichier',
      fileExtension: 'Extension du fichier',
      fileStorageType: 'Type de stockage',
      fileStorageLocation: 'Emplacement de stockage',
      status: 'Statut',
      remark: 'Remarque',
      createTime: 'Date de création',
      updateTime: 'Date de mise à jour',
      operation: 'Opération',
      placeholder: {
        fileName: 'Veuillez saisir le nom du fichier',
        fileType: 'Veuillez saisir le type de fichier',
        fileSize: 'Veuillez saisir la taille du fichier',
        status: 'Veuillez sélectionner le statut',
        remark: 'Veuillez saisir une remarque'
      },
      validation: {
        fileName: {
          required: 'Veuillez saisir le nom du fichier',
          maxLength: 'Le nom du fichier ne peut pas dépasser 100 caractères'
        },
        fileType: {
          required: 'Veuillez saisir le type de fichier',
          maxLength: 'Le type de fichier ne peut pas dépasser 50 caractères'
        },
        status: {
          required: 'Veuillez sélectionner le statut'
        },
        file: {
          required: 'Veuillez sélectionner un fichier à télécharger',
          size: 'La taille du fichier ne peut pas dépasser 2 Mo !'
        }
      },
      message: {
        addSuccess: 'Fichier ajouté avec succès',
        editSuccess: 'Fichier mis à jour avec succès',
        deleteSuccess: 'Fichier supprimé avec succès',
        deleteConfirm: 'Êtes-vous sûr de vouloir supprimer le fichier "{name}" ?',
        exportSuccess: 'Fichier exporté avec succès',
        importSuccess: 'Fichier importé avec succès',
        uploadSuccess: 'Fichier téléchargé avec succès',
        downloadSuccess: 'Fichier téléchargé avec succès'
      },
      detail: {
        title: 'Détails du fichier'
      },
      upload: {
        success: 'Fichier téléchargé avec succès',
        failed: 'Échec du téléchargement du fichier',
        fileEmpty: 'Veuillez sélectionner un fichier à télécharger',
        fileType: 'Type de fichier non pris en charge',
        fileSize: 'La taille du fichier dépasse la limite',
        fileExists: 'Le fichier existe déjà',
        fileNameRequired: 'Le nom du fichier ne peut pas être vide'
      }
    }
  }
} 