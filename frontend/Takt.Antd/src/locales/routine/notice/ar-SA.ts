export default {
  routine: {
    notice: {
      // Basic Information
      noticeId: 'معرف الإشعار',
      noticeType: 'نوع الإشعار',
      noticeTitle: 'عنوان الإشعار',
      noticeContent: 'محتوى الإشعار',
      noticeStatus: 'حالة الإشعار',
      noticeParams: 'معلمات الإشعار',
      remark: 'ملاحظة',
      createTime: 'وقت الإنشاء',
      updateTime: 'وقت التحديث',

      // Action Buttons
      add: 'إضافة إشعار',
      edit: 'تعديل الإشعار',
      delete: 'حذف الإشعار',
      batchDelete: 'حذف متعدد',
      export: 'تصدير',
      import: 'استيراد',
      downloadTemplate: 'تحميل القالب',
      preview: 'معاينة',
      send: 'إرسال',

      // Form Placeholders
      placeholder: {
        noticeId: 'الرجاء إدخال معرف الإشعار',
        noticeType: 'الرجاء اختيار نوع الإشعار',
        noticeTitle: 'الرجاء إدخال عنوان الإشعار',
        noticeContent: 'الرجاء إدخال محتوى الإشعار',
        noticeStatus: 'الرجاء اختيار حالة الإشعار',
        noticeParams: 'الرجاء إدخال معلمات الإشعار',
        remark: 'الرجاء إدخال ملاحظة',
        startTime: 'وقت البدء',
        endTime: 'وقت الانتهاء'
      },

      // Form Validation
      validation: {
        noticeId: {
          required: 'الرجاء إدخال معرف الإشعار',
          maxLength: 'لا يمكن أن يتجاوز معرف الإشعار 100 حرف'
        },
        noticeType: {
          required: 'الرجاء اختيار نوع الإشعار',
          maxLength: 'لا يمكن أن يتجاوز نوع الإشعار 50 حرف'
        },
        noticeTitle: {
          required: 'الرجاء إدخال عنوان الإشعار',
          maxLength: 'لا يمكن أن يتجاوز عنوان الإشعار 200 حرف'
        },
        noticeContent: {
          required: 'الرجاء إدخال محتوى الإشعار',
          maxLength: 'لا يمكن أن يتجاوز محتوى الإشعار 4000 حرف'
        },
        noticeStatus: {
          required: 'الرجاء اختيار حالة الإشعار'
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
        title: 'تفاصيل الإشعار'
      }
    }
  }
} 