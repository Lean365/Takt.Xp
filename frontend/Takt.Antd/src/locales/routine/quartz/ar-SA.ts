export default {
  routine: {
    quartz: {
      // Basic Information
      jobId: 'معرف المهمة',
      jobName: 'اسم المهمة',
      jobGroup: 'مجموعة المهمة',
      jobClass: 'فئة المهمة',
      jobMethod: 'طريقة المهمة',
      jobParams: 'معلمات المهمة',
      cronExpression: 'تعبير Cron',
      jobStatus: 'حالة المهمة',
      remark: 'ملاحظة',
      createTime: 'وقت الإنشاء',
      updateTime: 'وقت التحديث',

      // Action Buttons
      add: 'إضافة مهمة',
      edit: 'تعديل المهمة',
      delete: 'حذف المهمة',
      batchDelete: 'حذف متعدد',
      export: 'تصدير',
      import: 'استيراد',
      downloadTemplate: 'تحميل القالب',
      start: 'بدء',
      stop: 'إيقاف',
      pause: 'إيقاف مؤقت',
      resume: 'استئناف',
      run: 'تشغيل الآن',

      // Form Placeholders
      placeholder: {
        jobId: 'الرجاء إدخال معرف المهمة',
        jobName: 'الرجاء إدخال اسم المهمة',
        jobGroup: 'الرجاء إدخال مجموعة المهمة',
        jobClass: 'الرجاء إدخال فئة المهمة',
        jobMethod: 'الرجاء إدخال طريقة المهمة',
        jobParams: 'الرجاء إدخال معلمات المهمة',
        cronExpression: 'الرجاء إدخال تعبير Cron',
        jobStatus: 'الرجاء اختيار حالة المهمة',
        remark: 'الرجاء إدخال ملاحظة',
        startTime: 'وقت البدء',
        endTime: 'وقت الانتهاء'
      },

      // Form Validation
      validation: {
        jobId: {
          required: 'الرجاء إدخال معرف المهمة',
          maxLength: 'لا يمكن أن يتجاوز معرف المهمة 100 حرف'
        },
        jobName: {
          required: 'الرجاء إدخال اسم المهمة',
          maxLength: 'لا يمكن أن يتجاوز اسم المهمة 50 حرف'
        },
        jobGroup: {
          required: 'الرجاء إدخال مجموعة المهمة',
          maxLength: 'لا يمكن أن تتجاوز مجموعة المهمة 50 حرف'
        },
        jobClass: {
          required: 'الرجاء إدخال فئة المهمة',
          maxLength: 'لا يمكن أن تتجاوز فئة المهمة 200 حرف'
        },
        jobMethod: {
          required: 'الرجاء إدخال طريقة المهمة',
          maxLength: 'لا يمكن أن تتجاوز طريقة المهمة 50 حرف'
        },
        cronExpression: {
          required: 'الرجاء إدخال تعبير Cron',
          maxLength: 'لا يمكن أن يتجاوز تعبير Cron 50 حرف'
        },
        jobStatus: {
          required: 'الرجاء اختيار حالة المهمة'
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
          start: 'تم البدء بنجاح',
          stop: 'تم الإيقاف بنجاح',
          pause: 'تم الإيقاف المؤقت بنجاح',
          resume: 'تم الاستئناف بنجاح',
          run: 'تم التشغيل بنجاح'
        },
        failed: {
          add: 'فشلت عملية الإضافة',
          edit: 'فشلت عملية التحديث',
          delete: 'فشلت عملية الحذف',
          batchDelete: 'فشلت عملية الحذف المتعدد',
          export: 'فشلت عملية التصدير',
          import: 'فشلت عملية الاستيراد',
          start: 'فشلت عملية البدء',
          stop: 'فشلت عملية الإيقاف',
          pause: 'فشلت عملية الإيقاف المؤقت',
          resume: 'فشلت عملية الاستئناف',
          run: 'فشلت عملية التشغيل'
        }
      },

      // Detail Page
      detail: {
        title: 'تفاصيل المهمة'
      }
    }
  }
} 