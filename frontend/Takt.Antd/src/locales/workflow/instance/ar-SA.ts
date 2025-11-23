export default {
  workflow: {
    instance: {
      title: 'مثيل سير العمل',
      list: {
        title: 'قائمة مثيلات سير العمل',
        search: {
          name: 'اسم سير العمل',
          key: 'مفتاح سير العمل',
          version: 'الإصدار',
          status: 'الحالة',
          startTime: 'وقت البدء',
          endTime: 'وقت الانتهاء'
        },
        table: {
          name: 'اسم سير العمل',
          key: 'مفتاح سير العمل',
          version: 'الإصدار',
          status: 'الحالة',
          startTime: 'وقت البدء',
          endTime: 'وقت الانتهاء',
          duration: 'المدة',
          actions: 'الإجراءات'
        },
        actions: {
          view: 'عرض',
          delete: 'حذف',
          terminate: 'إنهاء',
          export: 'تصدير',
          import: 'استيراد',
          refresh: 'تحديث'
        },
        status: {
          running: 'قيد التشغيل',
          completed: 'مكتمل',
          terminated: 'مُنهي',
          failed: 'فشل'
        }
      },
      form: {
        title: {
          create: 'إنشاء مثيل سير عمل',
          import: 'استيراد مثيل سير عمل'
        },
        fields: {
          workflowDefinitionId: 'تعريف سير العمل',
          variables: 'تكوين المتغيرات'
        },
        rules: {
          workflowDefinitionId: {
            required: 'الرجاء اختيار تعريف سير العمل'
          }
        },
        buttons: {
          submit: 'إرسال',
          cancel: 'إلغاء'
        }
      },
      detail: {
        title: 'تفاصيل مثيل سير العمل',
        basic: {
          title: 'المعلومات الأساسية',
          name: 'اسم سير العمل',
          key: 'مفتاح سير العمل',
          version: 'الإصدار',
          status: 'الحالة',
          startTime: 'وقت البدء',
          endTime: 'وقت الانتهاء',
          duration: 'المدة'
        },
        nodes: {
          title: 'معلومات العقدة',
          name: 'اسم العقدة',
          type: 'نوع العقدة',
          status: 'الحالة',
          startTime: 'وقت البدء',
          endTime: 'وقت الانتهاء',
          duration: 'المدة',
          input: 'الإدخال',
          output: 'الإخراج',
          error: 'خطأ'
        },
        actions: {
          back: 'رجوع'
        }
      },
      terminate: {
        title: 'إنهاء مثيل سير العمل',
        confirm: 'هل أنت متأكد من إنهاء هذا المثيل؟',
        reason: 'سبب الإنهاء',
        buttons: {
          submit: 'إرسال',
          cancel: 'إلغاء'
        }
      }
    }
  }
} 