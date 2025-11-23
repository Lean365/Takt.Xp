export default {
  routine: {
    mail: {
      // Basic Information
      mailId: 'معرف البريد الإلكتروني',
      mailType: 'نوع البريد الإلكتروني',
      mailSubject: 'موضوع البريد الإلكتروني',
      mailContent: 'محتوى البريد الإلكتروني',
      mailStatus: 'حالة البريد الإلكتروني',
      mailParams: 'معلمات البريد الإلكتروني',
      remark: 'ملاحظة',
      createTime: 'وقت الإنشاء',
      updateTime: 'وقت التحديث',

      // Action Buttons
      add: 'إضافة بريد إلكتروني',
      edit: 'تعديل البريد الإلكتروني',
      delete: 'حذف البريد الإلكتروني',
      batchDelete: 'حذف متعدد',
      export: 'تصدير',
      import: 'استيراد',
      downloadTemplate: 'تحميل القالب',
      preview: 'معاينة',
      send: 'إرسال',

      // Form Placeholders
      placeholder: {
        mailId: 'الرجاء إدخال معرف البريد الإلكتروني',
        mailType: 'الرجاء اختيار نوع البريد الإلكتروني',
        mailSubject: 'الرجاء إدخال موضوع البريد الإلكتروني',
        mailContent: 'الرجاء إدخال محتوى البريد الإلكتروني',
        mailStatus: 'الرجاء اختيار حالة البريد الإلكتروني',
        mailParams: 'الرجاء إدخال معلمات البريد الإلكتروني',
        remark: 'الرجاء إدخال ملاحظة',
        startTime: 'وقت البدء',
        endTime: 'وقت الانتهاء'
      },

      // Form Validation
      validation: {
        mailId: {
          required: 'الرجاء إدخال معرف البريد الإلكتروني',
          maxLength: 'لا يمكن أن يتجاوز معرف البريد الإلكتروني 100 حرف'
        },
        mailType: {
          required: 'الرجاء اختيار نوع البريد الإلكتروني',
          maxLength: 'لا يمكن أن يتجاوز نوع البريد الإلكتروني 50 حرف'
        },
        mailSubject: {
          required: 'الرجاء إدخال موضوع البريد الإلكتروني',
          maxLength: 'لا يمكن أن يتجاوز موضوع البريد الإلكتروني 200 حرف'
        },
        mailContent: {
          required: 'الرجاء إدخال محتوى البريد الإلكتروني',
          maxLength: 'لا يمكن أن يتجاوز محتوى البريد الإلكتروني 4000 حرف'
        },
        mailStatus: {
          required: 'الرجاء اختيار حالة البريد الإلكتروني'
        }
      },

      // Operation Results
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

      // Detail Page
      detail: {
        title: 'تفاصيل البريد الإلكتروني'
      }
    }
  }
} 