export default {
  accounting: {
    financial: {
      company: {
        title: 'Gestion des Entreprises',
        tabs: {
          basicInfo: 'Informations de Base',
          detailInfo: 'Informations Détaillées',
          financialInfo: 'Informations Financières'
        },
        fields: {
          companyCode: { label: 'Code Entreprise', placeholder: 'Saisissez le code entreprise', required: 'Saisissez le code entreprise' },
          companyName: { label: 'Nom de l\'Entreprise', placeholder: 'Saisissez le nom de l\'entreprise', required: 'Saisissez le nom de l\'entreprise' },
          companyShortName: { label: 'Nom Court', placeholder: 'Saisissez le nom court' },
          companyTaxNumber: { label: 'Numéro Fiscal', placeholder: 'Saisissez le numéro fiscal' },
          companyNature: { label: 'Nature de l\'Entreprise', placeholder: 'Sélectionnez la nature de l\'entreprise', required: 'Sélectionnez la nature de l\'entreprise' },
          companyIndustryType: { label: 'Type d\'Industrie', placeholder: 'Sélectionnez le type d\'industrie' },
          companyCurrency: { label: 'Devise', placeholder: 'Sélectionnez la devise', required: 'Sélectionnez la devise' },
          companyLanguage: { label: 'Langue', placeholder: 'Sélectionnez la langue', required: 'Sélectionnez la langue' },
          companyPhone: { label: 'Téléphone', placeholder: 'Saisissez le numéro de téléphone' },
          companyEmail: { label: 'E-mail', placeholder: 'Saisissez l\'e-mail' },
          companyWebsite: { label: 'Site Web', placeholder: 'Saisissez le site web' },
          companyScale: { label: 'Échelle de l\'Entreprise', placeholder: 'Saisissez l\'échelle de l\'entreprise' },
          companyAddress: { label: 'Adresse', placeholder: 'Saisissez l\'adresse' },
          companyName1: { label: 'Nom de l\'Entreprise 1', placeholder: 'Saisissez le nom de l\'entreprise 1' },
          companyName2: { label: 'Nom de l\'Entreprise 2', placeholder: 'Saisissez le nom de l\'entreprise 2' },
          companyName3: { label: 'Nom de l\'Entreprise 3', placeholder: 'Saisissez le nom de l\'entreprise 3' },
          companyLegalRepresentative: { label: 'Représentant Légal', placeholder: 'Saisissez le représentant légal' },
          companyRegisteredCapital: { label: 'Capital Enregistré', placeholder: 'Saisissez le capital enregistré' },
          companyBusinessLicense: { label: 'Licence Commerciale', placeholder: 'Saisissez le numéro de licence commerciale' },
          companyOrganizationCode: { label: 'Code d\'Organisation', placeholder: 'Saisissez le code d\'organisation' },
          companyUnifiedCreditCode: { label: 'Code de Crédit Unifié', placeholder: 'Saisissez le code de crédit unifié' },
          companyCity: { label: 'Ville', placeholder: 'Saisissez la ville' },
          companyRegion: { label: 'Région', placeholder: 'Saisissez la région' },
          companyPostalCode: { label: 'Code Postal', placeholder: 'Saisissez le code postal' },
          companyCountry: { label: 'Pays', placeholder: 'Saisissez le pays' },
          companyFax: { label: 'Fax', placeholder: 'Saisissez le numéro de fax' },
          establishmentDate: { label: 'Date d\'Établissement', placeholder: 'Sélectionnez la date d\'établissement' },
          dissolutionDate: { label: 'Date de Dissolution', placeholder: 'Sélectionnez la date de dissolution' },
          orderNum: { label: 'Numéro d\'Ordre', placeholder: 'Saisissez le numéro d\'ordre' },
          status: { label: 'Statut', placeholder: 'Sélectionnez le statut', required: 'Sélectionnez le statut' },
          companyFiscalYearVariant: { label: 'Variante d\'Année Fiscale', placeholder: 'Saisissez la variante d\'année fiscale' },
          companyChartOfAccounts: { label: 'Plan Comptable', placeholder: 'Saisissez le plan comptable' },
          companyFinancialManagementArea: { label: 'Zone de Gestion Financière', placeholder: 'Saisissez la zone de gestion financière' },
          companyMaxExchangeRateDeviation: { label: 'Déviation Maximale du Taux de Change', placeholder: 'Saisissez la déviation maximale du taux de change' },
          companyAllocationIdentifier: { label: 'Identifiant d\'Allocation', placeholder: 'Saisissez l\'identifiant d\'allocation' },
          companyRelatedCompany: { label: 'Entreprise Associée', placeholder: 'Saisissez l\'entreprise associée' },
          companyRelatedPlant: { label: 'Usine Associée', placeholder: 'Saisissez l\'usine associée' },
          companyAddressNumber: { label: 'Numéro d\'Adresse', placeholder: 'Saisissez le numéro d\'adresse' },
          remark: { label: 'Remarque', placeholder: 'Saisissez la remarque' }
        },
        actions: {
          add: 'Ajouter Entreprise',
          edit: 'Modifier Entreprise',
          delete: 'Supprimer Entreprise',
          batchDelete: 'Suppression Multiple',
          export: 'Exporter',
          import: 'Importer',
          downloadTemplate: 'Télécharger Modèle',
          reset: 'Réinitialiser',
          search: 'Rechercher'
        },
        messages: {
          addSuccess: 'Entreprise ajoutée avec succès',
          updateSuccess: 'Entreprise mise à jour avec succès',
          deleteSuccess: 'Entreprise supprimée avec succès',
          batchDeleteSuccess: 'Entreprises supprimées avec succès',
          deleteConfirm: 'Êtes-vous sûr de supprimer l\'entreprise sélectionnée ?',
          batchDeleteConfirm: 'Êtes-vous sûr de supprimer les entreprises sélectionnées ?',
          loadFailed: 'Échec du chargement des données',
          submitFailed: 'Échec de l\'envoi',
          exportSuccess: 'Exportation réussie',
          importSuccess: 'Importation réussie',
          importFailed: 'Échec de l\'importation',
          downloadTemplateSuccess: 'Modèle téléchargé avec succès'
        }
      }
    }
  }
}
