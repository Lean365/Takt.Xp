export default {
  workflow: {
    history: {
      title: 'سجل سير العمل',
      list: {
        title: 'قائمة سجلات سير العمل',
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
      detail: {
        title: 'تفاصيل سجل سير العمل',
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
      }
    }
  }
} 