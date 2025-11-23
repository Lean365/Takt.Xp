export default {
  accounting: {
    financial: {
      company: {
        title: 'Gestión de Empresas',
        tabs: {
          basicInfo: 'Información Básica',
          detailInfo: 'Información Detallada',
          financialInfo: 'Información Financiera'
        },
        fields: {
          companyCode: { label: 'Código de Empresa', placeholder: 'Ingrese el código de empresa', required: 'Ingrese el código de empresa' },
          companyName: { label: 'Nombre de Empresa', placeholder: 'Ingrese el nombre de empresa', required: 'Ingrese el nombre de empresa' },
          companyShortName: { label: 'Nombre Corto', placeholder: 'Ingrese el nombre corto' },
          companyTaxNumber: { label: 'Número de Impuesto', placeholder: 'Ingrese el número de impuesto' },
          companyNature: { label: 'Naturaleza de Empresa', placeholder: 'Seleccione la naturaleza de empresa', required: 'Seleccione la naturaleza de empresa' },
          companyIndustryType: { label: 'Tipo de Industria', placeholder: 'Seleccione el tipo de industria' },
          companyCurrency: { label: 'Moneda', placeholder: 'Seleccione la moneda', required: 'Seleccione la moneda' },
          companyLanguage: { label: 'Idioma', placeholder: 'Seleccione el idioma', required: 'Seleccione el idioma' },
          companyPhone: { label: 'Teléfono', placeholder: 'Ingrese el número de teléfono' },
          companyEmail: { label: 'Correo Electrónico', placeholder: 'Ingrese el correo electrónico' },
          companyWebsite: { label: 'Sitio Web', placeholder: 'Ingrese el sitio web' },
          companyScale: { label: 'Escala de Empresa', placeholder: 'Ingrese la escala de empresa' },
          companyAddress: { label: 'Dirección', placeholder: 'Ingrese la dirección' },
          companyName1: { label: 'Nombre de Empresa 1', placeholder: 'Ingrese el nombre de empresa 1' },
          companyName2: { label: 'Nombre de Empresa 2', placeholder: 'Ingrese el nombre de empresa 2' },
          companyName3: { label: 'Nombre de Empresa 3', placeholder: 'Ingrese el nombre de empresa 3' },
          companyLegalRepresentative: { label: 'Representante Legal', placeholder: 'Ingrese el representante legal' },
          companyRegisteredCapital: { label: 'Capital Registrado', placeholder: 'Ingrese el capital registrado' },
          companyBusinessLicense: { label: 'Licencia Comercial', placeholder: 'Ingrese el número de licencia comercial' },
          companyOrganizationCode: { label: 'Código de Organización', placeholder: 'Ingrese el código de organización' },
          companyUnifiedCreditCode: { label: 'Código de Crédito Unificado', placeholder: 'Ingrese el código de crédito unificado' },
          companyCity: { label: 'Ciudad', placeholder: 'Ingrese la ciudad' },
          companyRegion: { label: 'Región', placeholder: 'Ingrese la región' },
          companyPostalCode: { label: 'Código Postal', placeholder: 'Ingrese el código postal' },
          companyCountry: { label: 'País', placeholder: 'Ingrese el país' },
          companyFax: { label: 'Fax', placeholder: 'Ingrese el número de fax' },
          establishmentDate: { label: 'Fecha de Establecimiento', placeholder: 'Seleccione la fecha de establecimiento' },
          dissolutionDate: { label: 'Fecha de Disolución', placeholder: 'Seleccione la fecha de disolución' },
          orderNum: { label: 'Número de Orden', placeholder: 'Ingrese el número de orden' },
          status: { label: 'Estado', placeholder: 'Seleccione el estado', required: 'Seleccione el estado' },
          companyFiscalYearVariant: { label: 'Variante de Año Fiscal', placeholder: 'Ingrese la variante de año fiscal' },
          companyChartOfAccounts: { label: 'Plan de Cuentas', placeholder: 'Ingrese el plan de cuentas' },
          companyFinancialManagementArea: { label: 'Área de Gestión Financiera', placeholder: 'Ingrese el área de gestión financiera' },
          companyMaxExchangeRateDeviation: { label: 'Desviación Máxima del Tipo de Cambio', placeholder: 'Ingrese la desviación máxima del tipo de cambio' },
          companyAllocationIdentifier: { label: 'Identificador de Asignación', placeholder: 'Ingrese el identificador de asignación' },
          companyRelatedCompany: { label: 'Empresa Relacionada', placeholder: 'Ingrese la empresa relacionada' },
          companyRelatedPlant: { label: 'Planta Relacionada', placeholder: 'Ingrese la planta relacionada' },
          companyAddressNumber: { label: 'Número de Dirección', placeholder: 'Ingrese el número de dirección' },
          remark: { label: 'Observación', placeholder: 'Ingrese la observación' }
        },
        actions: {
          add: 'Agregar Empresa',
          edit: 'Editar Empresa',
          delete: 'Eliminar Empresa',
          batchDelete: 'Eliminación Múltiple',
          export: 'Exportar',
          import: 'Importar',
          downloadTemplate: 'Descargar Plantilla',
          reset: 'Restablecer',
          search: 'Buscar'
        },
        messages: {
          addSuccess: 'Empresa agregada exitosamente',
          updateSuccess: 'Empresa actualizada exitosamente',
          deleteSuccess: 'Empresa eliminada exitosamente',
          batchDeleteSuccess: 'Empresas eliminadas exitosamente',
          deleteConfirm: '¿Está seguro de eliminar la empresa seleccionada?',
          batchDeleteConfirm: '¿Está seguro de eliminar las empresas seleccionadas?',
          loadFailed: 'Error al cargar los datos',
          submitFailed: 'Error al enviar',
          exportSuccess: 'Exportación exitosa',
          importSuccess: 'Importación exitosa',
          importFailed: 'Error en la importación',
          downloadTemplateSuccess: 'Plantilla descargada exitosamente'
        }
      }
    }
  }
}
