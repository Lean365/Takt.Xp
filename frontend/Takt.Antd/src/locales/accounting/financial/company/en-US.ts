export default {
  accounting: {
    financial: {
      company: {
        // Page title
        title: 'Company Management',
        
        // Tabs
        tabs: {
          basicInfo: 'Basic Information',
          detailInfo: 'Detail Information',
          financialInfo: 'Financial Information'
        },

        // Field labels
        fields: {
          companyCode: {
            label: 'Company Code',
            placeholder: 'Please enter company code',
            required: 'Please enter company code'
          },
          companyName: {
            label: 'Company Name',
            placeholder: 'Please enter company name',
            required: 'Please enter company name'
          },
          companyShortName: {
            label: 'Company Short Name',
            placeholder: 'Please enter company short name'
          },
          companyTaxNumber: {
            label: 'Tax Number',
            placeholder: 'Please enter tax number'
          },
          companyNature: {
            label: 'Enterprise Nature',
            placeholder: 'Please select enterprise nature',
            required: 'Please select enterprise nature'
          },
          companyIndustryType: {
            label: 'Industry Type',
            placeholder: 'Please select industry type'
          },
          companyCurrency: {
            label: 'Currency',
            placeholder: 'Please select currency',
            required: 'Please select currency'
          },
          companyLanguage: {
            label: 'Language',
            placeholder: 'Please select language',
            required: 'Please select language'
          },
          companyPhone: {
            label: 'Phone',
            placeholder: 'Please enter phone number'
          },
          companyEmail: {
            label: 'Email',
            placeholder: 'Please enter email address'
          },
          companyWebsite: {
            label: 'Website',
            placeholder: 'Please enter website'
          },
          companyScale: {
            label: 'Company Scale',
            placeholder: 'Please enter company scale'
          },
          companyAddress: {
            label: 'Address',
            placeholder: 'Please enter address'
          },
          companyName1: {
            label: 'Company Name 1',
            placeholder: 'Please enter company name 1'
          },
          companyName2: {
            label: 'Company Name 2',
            placeholder: 'Please enter company name 2'
          },
          companyName3: {
            label: 'Company Name 3',
            placeholder: 'Please enter company name 3'
          },
          companyLegalRepresentative: {
            label: 'Legal Representative',
            placeholder: 'Please enter legal representative'
          },
          companyRegisteredCapital: {
            label: 'Registered Capital',
            placeholder: 'Please enter registered capital'
          },
          companyBusinessLicense: {
            label: 'Business License',
            placeholder: 'Please enter business license number'
          },
          companyOrganizationCode: {
            label: 'Organization Code',
            placeholder: 'Please enter organization code'
          },
          companyUnifiedCreditCode: {
            label: 'Unified Credit Code',
            placeholder: 'Please enter unified credit code'
          },
          companyCity: {
            label: 'City',
            placeholder: 'Please enter city'
          },
          companyRegion: {
            label: 'Region',
            placeholder: 'Please enter region'
          },
          companyPostalCode: {
            label: 'Postal Code',
            placeholder: 'Please enter postal code'
          },
          companyCountry: {
            label: 'Country',
            placeholder: 'Please enter country'
          },
          companyFax: {
            label: 'Fax',
            placeholder: 'Please enter fax number'
          },
          establishmentDate: {
            label: 'Establishment Date',
            placeholder: 'Please select establishment date'
          },
          dissolutionDate: {
            label: 'Dissolution Date',
            placeholder: 'Please select dissolution date'
          },
          orderNum: {
            label: 'Order Number',
            placeholder: 'Please enter order number'
          },
          status: {
            label: 'Status',
            placeholder: 'Please select status',
            required: 'Please select status'
          },
          companyFiscalYearVariant: {
            label: 'Fiscal Year Variant',
            placeholder: 'Please enter fiscal year variant'
          },
          companyChartOfAccounts: {
            label: 'Chart of Accounts',
            placeholder: 'Please enter chart of accounts'
          },
          companyFinancialManagementArea: {
            label: 'Financial Management Area',
            placeholder: 'Please enter financial management area'
          },
          companyMaxExchangeRateDeviation: {
            label: 'Max Exchange Rate Deviation',
            placeholder: 'Please enter max exchange rate deviation'
          },
          companyAllocationIdentifier: {
            label: 'Allocation Identifier',
            placeholder: 'Please enter allocation identifier'
          },
          companyRelatedCompany: {
            label: 'Related Company',
            placeholder: 'Please enter related company'
          },
          companyRelatedPlant: {
            label: 'Related Plant',
            placeholder: 'Please enter related plant'
          },
          companyAddressNumber: {
            label: 'Address Number',
            placeholder: 'Please enter address number'
          },
          remark: {
            label: 'Remark',
            placeholder: 'Please enter remark'
          }
        },

        // Action buttons
        actions: {
          add: 'Add Company',
          edit: 'Edit Company',
          delete: 'Delete Company',
          batchDelete: 'Batch Delete',
          export: 'Export',
          import: 'Import',
          downloadTemplate: 'Download Template',
          reset: 'Reset',
          search: 'Search'
        },

        // Table column titles
        columns: {
          companyCode: 'Company Code',
          companyName: 'Company Name',
          companyShortName: 'Company Short Name',
          companyTaxNumber: 'Tax Number',
          companyNature: 'Enterprise Nature',
          companyIndustryType: 'Industry Type',
          companyCurrency: 'Currency',
          companyLanguage: 'Language',
          companyPhone: 'Phone',
          companyEmail: 'Email',
          companyWebsite: 'Website',
          companyScale: 'Company Scale',
          companyAddress: 'Address',
          companyName1: 'Company Name 1',
          companyName2: 'Company Name 2',
          companyName3: 'Company Name 3',
          companyLegalRepresentative: 'Legal Representative',
          companyRegisteredCapital: 'Registered Capital',
          companyBusinessLicense: 'Business License',
          companyOrganizationCode: 'Organization Code',
          companyUnifiedCreditCode: 'Unified Credit Code',
          companyCity: 'City',
          companyRegion: 'Region',
          companyPostalCode: 'Postal Code',
          companyCountry: 'Country',
          companyFax: 'Fax',
          establishmentDate: 'Establishment Date',
          dissolutionDate: 'Dissolution Date',
          orderNum: 'Order Number',
          status: 'Status',
          companyFiscalYearVariant: 'Fiscal Year Variant',
          companyChartOfAccounts: 'Chart of Accounts',
          companyFinancialManagementArea: 'Financial Management Area',
          companyMaxExchangeRateDeviation: 'Max Exchange Rate Deviation',
          companyAllocationIdentifier: 'Allocation Identifier',
          companyRelatedCompany: 'Related Company',
          companyRelatedPlant: 'Related Plant',
          companyAddressNumber: 'Address Number',
          remark: 'Remark',
          createBy: 'Created By',
          createTime: 'Created Time',
          updateBy: 'Updated By',
          updateTime: 'Updated Time',
          deleteBy: 'Deleted By',
          deleteTime: 'Deleted Time'
        },

        // Messages
        messages: {
          addSuccess: 'Company added successfully',
          updateSuccess: 'Company updated successfully',
          deleteSuccess: 'Company deleted successfully',
          batchDeleteSuccess: 'Companies deleted successfully',
          deleteConfirm: 'Are you sure to delete the selected company?',
          batchDeleteConfirm: 'Are you sure to batch delete the selected companies?',
          loadFailed: 'Failed to load data',
          submitFailed: 'Submit failed',
          exportSuccess: 'Export successful',
          importSuccess: 'Import successful',
          importFailed: 'Import failed',
          downloadTemplateSuccess: 'Template downloaded successfully'
        },

        // Query conditions
        query: {
          companyCode: 'Company Code',
          companyName: 'Company Name',
          companyNature: 'Enterprise Nature',
          companyIndustryType: 'Industry Type',
          companyCurrency: 'Currency',
          status: 'Status'
        }
      }
    }
  }
}