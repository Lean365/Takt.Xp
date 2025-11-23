export default {
  routine: {
    mailtmpl: {
      // المعلومات الأساسية
      templateName: 'اسم القالب',
      templateType: 'نوع القالب',
      templateSubject: 'موضوع القالب',
      templateContent: 'محتوى القالب',
      templateStatus: 'حالة القالب',
      templateParams: 'معلمات القالب',
      remark: 'ملاحظة',
      createTime: 'وقت الإنشاء',
      updateTime: 'وقت التحديث',

      // أزرار الإجراءات
      add: 'إضافة قالب',
      edit: 'تعديل القالب',
      delete: 'حذف القالب',
      batchDelete: 'حذف متعدد',
      export: 'تصدير',
      import: 'استيراد',
      downloadTemplate: 'تحميل القالب',
      preview: 'معاينة',
      send: 'إرسال',

      // حقول النموذج
      placeholder: {
        templateName: 'الرجاء إدخال اسم القالب',
        templateType: 'الرجاء اختيار نوع القالب',
        templateSubject: 'الرجاء إدخال موضوع القالب',
        templateContent: 'الرجاء إدخال محتوى القالب',
        templateStatus: 'الرجاء اختيار حالة القالب',
        templateParams: 'الرجاء إدخال معلمات القالب',
        remark: 'الرجاء إدخال ملاحظة',
        startTime: 'وقت البدء',
        endTime: 'وقت الانتهاء'
      },

      // التحقق من النموذج
      validation: {
        templateName: {
          required: 'الرجاء إدخال اسم القالب',
          maxLength: 'لا يمكن أن يتجاوز اسم القالب 100 حرف'
        },
        templateType: {
          required: 'الرجاء اختيار نوع القالب',
          maxLength: 'لا يمكن أن يتجاوز نوع القالب 50 حرف'
        },
        templateSubject: {
          required: 'الرجاء إدخال موضوع القالب',
          maxLength: 'لا يمكن أن يتجاوز موضوع القالب 200 حرف'
        },
        templateContent: {
          required: 'الرجاء إدخال محتوى القالب',
          maxLength: 'لا يمكن أن يتجاوز محتوى القالب 4000 حرف'
        },
        templateStatus: {
          required: 'الرجاء اختيار حالة القالب'
        }
      },

      // نتائج العمليات
      message: {
        success: {
          add: 'تمت الإضافة بنجاح',
          edit: 'تم التحديث بنجاح',
          delete: 'تم الحذف بنجاح',
          batchDelete: 'تم الحذف المتعدد بنجاح',
          export: 'تم التصدير بنجاح',
          import: 'تم الاستيراد بنجاح',
          send: 'تم الإرسال بنجاح'
        },
        failed: {
          add: 'فشلت عملية الإضافة',
          edit: 'فشلت عملية التحديث',
          delete: 'فشلت عملية الحذف',
          batchDelete: 'فشلت عملية الحذف المتعدد',
          export: 'فشلت عملية التصدير',
          import: 'فشلت عملية الاستيراد',
          send: 'فشلت عملية الإرسال'
        }
      },

      // صفحة التفاصيل
      detail: {
        title: 'تفاصيل قالب البريد الإلكتروني'
      }
    }
  }
}
