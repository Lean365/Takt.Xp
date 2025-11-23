export default {
  routine: {
    core: {
      numberrule: {
        // عنوان الصفحة
        title: 'إدارة قواعد الترقيم',
        
        // التبويبات
        tabs: {
          basicInfo: 'المعلومات الأساسية',
          numberConfig: 'إعدادات الترقيم',
          advancedConfig: 'الإعدادات المتقدمة',
          otherInfo: 'معلومات أخرى'
        },

        // تسميات الحقول
        fields: {
          companyCode: {
            label: 'كود الشركة',
            placeholder: 'يرجى إدخال كود الشركة',
            required: 'يرجى إدخال كود الشركة',
            length: 'يجب أن يكون طول كود الشركة بين 1-20 حرف'
          },
          numberRuleName: {
            label: 'اسم قاعدة الترقيم',
            placeholder: 'يرجى إدخال اسم قاعدة الترقيم',
            required: 'يرجى إدخال اسم قاعدة الترقيم',
            length: 'يجب أن يكون طول اسم قاعدة الترقيم بين 1-100 حرف'
          },
          numberRuleCode: {
            label: 'كود قاعدة الترقيم',
            placeholder: 'يرجى إدخال كود قاعدة الترقيم',
            required: 'يرجى إدخال كود قاعدة الترقيم',
            length: 'يجب أن يكون طول كود قاعدة الترقيم بين 1-50 حرف'
          },
          deptCode: {
            label: 'كود القسم',
            placeholder: 'يرجى إدخال كود القسم',
            required: 'يرجى إدخال كود القسم',
            length: 'يجب أن يكون طول كود القسم بين 1-20 حرف'
          },
          numberRuleShortCode: {
            label: 'كود قاعدة الترقيم المختصر',
            placeholder: 'يرجى إدخال كود قاعدة الترقيم المختصر',
            required: 'يرجى إدخال كود قاعدة الترقيم المختصر',
            length: 'يجب أن يكون طول كود قاعدة الترقيم المختصر بين 1-4 أحرف'
          },
          numberRuleType: {
            label: 'نوع قاعدة الترقيم',
            placeholder: 'يرجى اختيار نوع قاعدة الترقيم',
            required: 'يرجى اختيار نوع قاعدة الترقيم'
          },
          status: {
            label: 'الحالة',
            placeholder: 'يرجى اختيار الحالة',
            required: 'يرجى اختيار الحالة'
          },
          numberRuleDescription: {
            label: 'وصف قاعدة الترقيم',
            placeholder: 'يرجى إدخال وصف قاعدة الترقيم'
          },
          numberPrefix: {
            label: 'بادئة الرقم',
            placeholder: 'يرجى إدخال بادئة الرقم'
          },
          numberSuffix: {
            label: 'لاحقة الرقم',
            placeholder: 'يرجى إدخال لاحقة الرقم'
          },
          dateFormat: {
            label: 'تنسيق التاريخ',
            placeholder: 'يرجى اختيار تنسيق التاريخ',
            required: 'يرجى اختيار تنسيق التاريخ'
          },
          sequenceLength: {
            label: 'طول التسلسل',
            placeholder: 'يرجى إدخال طول التسلسل',
            required: 'يرجى إدخال طول التسلسل'
          },
          sequenceStart: {
            label: 'قيمة بداية التسلسل',
            placeholder: 'يرجى إدخال قيمة بداية التسلسل',
            required: 'يرجى إدخال قيمة بداية التسلسل'
          },
          sequenceStep: {
            label: 'خطوة التسلسل',
            placeholder: 'يرجى إدخال خطوة التسلسل',
            required: 'يرجى إدخال خطوة التسلسل'
          },
          currentSequence: {
            label: 'التسلسل الحالي',
            placeholder: 'يرجى إدخال التسلسل الحالي'
          },
          sequenceResetRule: {
            label: 'قاعدة إعادة تعيين التسلسل',
            placeholder: 'يرجى اختيار قاعدة إعادة تعيين التسلسل'
          },
          includeCompanyCode: {
            label: 'تضمين كود الشركة',
            placeholder: 'يرجى اختيار ما إذا كان سيتم تضمين كود الشركة'
          },
          includeDepartmentCode: {
            label: 'تضمين كود القسم',
            placeholder: 'يرجى اختيار ما إذا كان سيتم تضمين كود القسم'
          },
          includeRevisionNumber: {
            label: 'تضمين رقم المراجعة',
            placeholder: 'يرجى اختيار ما إذا كان سيتم تضمين رقم المراجعة'
          },
          includeYear: {
            label: 'تضمين السنة',
            placeholder: 'يرجى اختيار ما إذا كان سيتم تضمين السنة'
          },
          includeMonth: {
            label: 'تضمين الشهر',
            placeholder: 'يرجى اختيار ما إذا كان سيتم تضمين الشهر'
          },
          includeDay: {
            label: 'تضمين اليوم',
            placeholder: 'يرجى اختيار ما إذا كان سيتم تضمين اليوم'
          },
          allowDuplicate: {
            label: 'السماح بالتكرار',
            placeholder: 'يرجى اختيار ما إذا كان سيتم السماح بالتكرار'
          },
          duplicateCheckScope: {
            label: 'نطاق فحص التكرار',
            placeholder: 'يرجى اختيار نطاق فحص التكرار'
          },
          orderNum: {
            label: 'رقم الترتيب',
            placeholder: 'يرجى إدخال رقم الترتيب'
          }
        },

        // أزرار الإجراءات
        actions: {
          add: 'إضافة قاعدة ترقيم',
          edit: 'تحرير قاعدة الترقيم',
          delete: 'حذف قاعدة الترقيم',
          batchDelete: 'حذف متعدد',
          export: 'تصدير',
          import: 'استيراد',
          downloadTemplate: 'تحميل القالب',
          preview: 'معاينة',
          generate: 'توليد رقم',
          validate: 'التحقق من القاعدة'
        },

        // نصوص الحقول
        placeholder: {
          search: 'يرجى إدخال اسم أو كود قاعدة الترقيم',
          selectAll: 'تحديد الكل',
          clear: 'مسح'
        },

        // رسائل نتائج العمليات
        message: {
          success: {
            add: 'تم إضافة قاعدة الترقيم بنجاح',
            edit: 'تم تحديث قاعدة الترقيم بنجاح',
            delete: 'تم حذف قاعدة الترقيم بنجاح',
            batchDelete: 'تم الحذف المتعدد بنجاح',
            export: 'تم التصدير بنجاح',
            import: 'تم الاستيراد بنجاح',
            generate: 'تم توليد الرقم بنجاح',
            validate: 'تم التحقق بنجاح'
          },
          failed: {
            add: 'فشل في إضافة قاعدة الترقيم',
            edit: 'فشل في تحديث قاعدة الترقيم',
            delete: 'فشل في حذف قاعدة الترقيم',
            batchDelete: 'فشل في الحذف المتعدد',
            export: 'فشل في التصدير',
            import: 'فشل في الاستيراد',
            generate: 'فشل في توليد الرقم',
            validate: 'فشل في التحقق'
          },
          confirm: {
            delete: 'هل أنت متأكد من حذف قاعدة الترقيم المحددة؟',
            batchDelete: 'هل أنت متأكد من حذف قواعد الترقيم المحددة؟'
          }
        },

        // صفحة التفاصيل
        detail: {
          title: 'تفاصيل قاعدة الترقيم',
          basicInfo: 'المعلومات الأساسية',
          numberConfig: 'إعدادات الترقيم',
          advancedConfig: 'الإعدادات المتقدمة',
          otherInfo: 'معلومات أخرى'
        },

        // عناوين أعمدة الجدول
        columns: {
          numberRuleName: 'اسم قاعدة الترقيم',
          numberRuleCode: 'كود قاعدة الترقيم',
          numberRuleShortCode: 'كود قاعدة الترقيم المختصر',
          numberRuleType: 'نوع قاعدة الترقيم',
          deptCode: 'كود القسم',
          status: 'الحالة',
          createTime: 'وقت الإنشاء',
          updateTime: 'وقت التحديث',
          remark: 'ملاحظة',
          actions: 'الإجراءات'
        },

        // أنواع قواعد الترقيم
        types: {
          daily: 'الشؤون اليومية',
          hr: 'الموارد البشرية',
          finance: 'المحاسبة المالية',
          logistics: 'إدارة الخدمات اللوجستية',
          production: 'إدارة الإنتاج',
          workflow: 'سير العمل'
        },

        // الحالات
        status: {
          normal: 'عادي',
          disabled: 'معطل'
        },

        // قواعد إعادة تعيين التسلسل
        resetRules: {
          none: 'لا إعادة تعيين',
          yearly: 'إعادة تعيين سنوية',
          monthly: 'إعادة تعيين شهرية',
          daily: 'إعادة تعيين يومية'
        },

        // نطاقات فحص التكرار
        checkScopes: {
          global: 'عالمي',
          yearly: 'سنوي',
          monthly: 'شهري',
          daily: 'يومي'
        },

        // خيارات تنسيق التاريخ
        dateFormats: {
          yyyy: 'yyyy',
          yyyyMM: 'yyyyMM',
          yyyyMMdd: 'yyyyMMdd',
          yyyyMMddHH: 'yyyyMMddHH',
          yyyyMMddHHmm: 'yyyyMMddHHmm',
          yyyyMMddHHmmss: 'yyyyMMddHHmmss'
        }
      }
    }
  }
}
