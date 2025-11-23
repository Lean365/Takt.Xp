export default {
  accounting: {
    financial: {
      company: {
        title: 'إدارة الشركة',
        tabs: {
          basicInfo: 'المعلومات الأساسية',
          detailInfo: 'المعلومات التفصيلية',
          financialInfo: 'المعلومات المالية'
        },
        fields: {
          companyCode: { label: 'رمز الشركة', placeholder: 'أدخل رمز الشركة', required: 'أدخل رمز الشركة' },
          companyName: { label: 'اسم الشركة', placeholder: 'أدخل اسم الشركة', required: 'أدخل اسم الشركة' },
          companyShortName: { label: 'الاسم المختصر للشركة', placeholder: 'أدخل الاسم المختصر للشركة' },
          companyTaxNumber: { label: 'رقم الضريبة', placeholder: 'أدخل رقم الضريبة' },
          companyNature: { label: 'طبيعة المؤسسة', placeholder: 'اختر طبيعة المؤسسة', required: 'اختر طبيعة المؤسسة' },
          companyIndustryType: { label: 'نوع الصناعة', placeholder: 'اختر نوع الصناعة' },
          companyCurrency: { label: 'العملة', placeholder: 'اختر العملة', required: 'اختر العملة' },
          companyLanguage: { label: 'اللغة', placeholder: 'اختر اللغة', required: 'اختر اللغة' },
          companyPhone: { label: 'الهاتف', placeholder: 'أدخل رقم الهاتف' },
          companyEmail: { label: 'البريد الإلكتروني', placeholder: 'أدخل البريد الإلكتروني' },
          companyWebsite: { label: 'الموقع الإلكتروني', placeholder: 'أدخل الموقع الإلكتروني' },
          companyScale: { label: 'حجم الشركة', placeholder: 'أدخل حجم الشركة' },
          companyAddress: { label: 'العنوان', placeholder: 'أدخل العنوان' },
          companyName1: { label: 'اسم الشركة 1', placeholder: 'أدخل اسم الشركة 1' },
          companyName2: { label: 'اسم الشركة 2', placeholder: 'أدخل اسم الشركة 2' },
          companyName3: { label: 'اسم الشركة 3', placeholder: 'أدخل اسم الشركة 3' },
          companyLegalRepresentative: { label: 'الممثل القانوني', placeholder: 'أدخل الممثل القانوني' },
          companyRegisteredCapital: { label: 'رأس المال المسجل', placeholder: 'أدخل رأس المال المسجل' },
          companyBusinessLicense: { label: 'رخصة العمل', placeholder: 'أدخل رقم رخصة العمل' },
          companyOrganizationCode: { label: 'رمز المنظمة', placeholder: 'أدخل رمز المنظمة' },
          companyUnifiedCreditCode: { label: 'رمز الائتمان الموحد', placeholder: 'أدخل رمز الائتمان الموحد' },
          companyCity: { label: 'المدينة', placeholder: 'أدخل المدينة' },
          companyRegion: { label: 'المنطقة', placeholder: 'أدخل المنطقة' },
          companyPostalCode: { label: 'الرمز البريدي', placeholder: 'أدخل الرمز البريدي' },
          companyCountry: { label: 'البلد', placeholder: 'أدخل البلد' },
          companyFax: { label: 'الفاكس', placeholder: 'أدخل رقم الفاكس' },
          establishmentDate: { label: 'تاريخ التأسيس', placeholder: 'اختر تاريخ التأسيس' },
          dissolutionDate: { label: 'تاريخ الحل', placeholder: 'اختر تاريخ الحل' },
          orderNum: { label: 'رقم الترتيب', placeholder: 'أدخل رقم الترتيب' },
          status: { label: 'الحالة', placeholder: 'اختر الحالة', required: 'اختر الحالة' },
          companyFiscalYearVariant: { label: 'متغير السنة المالية', placeholder: 'أدخل متغير السنة المالية' },
          companyChartOfAccounts: { label: 'دليل الحسابات', placeholder: 'أدخل دليل الحسابات' },
          companyFinancialManagementArea: { label: 'منطقة الإدارة المالية', placeholder: 'أدخل منطقة الإدارة المالية' },
          companyMaxExchangeRateDeviation: { label: 'الحد الأقصى لانحراف سعر الصرف', placeholder: 'أدخل الحد الأقصى لانحراف سعر الصرف' },
          companyAllocationIdentifier: { label: 'معرف التخصيص', placeholder: 'أدخل معرف التخصيص' },
          companyRelatedCompany: { label: 'الشركة ذات الصلة', placeholder: 'أدخل الشركة ذات الصلة' },
          companyRelatedPlant: { label: 'المصنع ذو الصلة', placeholder: 'أدخل المصنع ذو الصلة' },
          companyAddressNumber: { label: 'رقم العنوان', placeholder: 'أدخل رقم العنوان' },
          remark: { label: 'ملاحظة', placeholder: 'أدخل ملاحظة' }
        },
        actions: {
          add: 'إضافة شركة',
          edit: 'تحرير شركة',
          delete: 'حذف شركة',
          batchDelete: 'حذف متعدد',
          export: 'تصدير',
          import: 'استيراد',
          downloadTemplate: 'تحميل القالب',
          reset: 'إعادة تعيين',
          search: 'بحث'
        },
        messages: {
          addSuccess: 'تم إضافة الشركة بنجاح',
          updateSuccess: 'تم تحديث الشركة بنجاح',
          deleteSuccess: 'تم حذف الشركة بنجاح',
          batchDeleteSuccess: 'تم حذف الشركات بنجاح',
          deleteConfirm: 'هل أنت متأكد من حذف الشركة المحددة؟',
          batchDeleteConfirm: 'هل أنت متأكد من حذف الشركات المحددة؟',
          loadFailed: 'فشل في تحميل البيانات',
          submitFailed: 'فشل في الإرسال',
          exportSuccess: 'تم التصدير بنجاح',
          importSuccess: 'تم الاستيراد بنجاح',
          importFailed: 'فشل في الاستيراد',
          downloadTemplateSuccess: 'تم تحميل القالب بنجاح'
        }
      }
    }
  }
}
