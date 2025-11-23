export default {
  workflow: {
    definition: {
      title: 'تعريف سير العمل',
      list: {
        title: 'قائمة تعريفات سير العمل',
        search: {
          workflowName: 'اسم سير العمل',
          workflowCategory: 'نوع سير العمل',
          status: 'الحالة'
        },
        table: {
          workflowId: 'معرف سير العمل',
          workflowName: 'اسم سير العمل',
          workflowCategory: 'نوع سير العمل',
          status: 'الحالة',
          createTime: 'وقت الإنشاء',
          updateTime: 'وقت التحديث',
          actions: 'الإجراءات'
        },
        actions: {
          view: 'عرض',
          edit: 'تعديل',
          delete: 'حذف',
          refresh: 'تحديث',
          import: 'استيراد',
          export: 'تصدير'
        },
        status: {
          normal: 'عادي',
          disable: 'معطل'
        }
      },
      form: {
        title: {
          create: 'إنشاء تعريف سير العمل',
          edit: 'تعديل تعريف سير العمل'
        },
        fields: {
          workflowName: 'اسم سير العمل',
          workflowCategory: 'نوع سير العمل',
          workflowVersion: 'الإصدار',
          status: 'الحالة',
          remark: 'ملاحظات'
        },
        rules: {
          workflowName: {
            required: 'الرجاء إدخال اسم سير العمل'
          },
          workflowCategory: {
            required: 'الرجاء اختيار نوع سير العمل'
          },
          status: {
            required: 'الرجاء اختيار الحالة'
          }
        },
        buttons: {
          submit: 'إرسال',
          cancel: 'إلغاء'
        }
      },
      detail: {
        title: 'تفاصيل تعريف سير العمل',
        basic: {
          title: 'المعلومات الأساسية',
          workflowName: 'اسم سير العمل',
          workflowCategory: 'نوع سير العمل',
          workflowVersion: 'الإصدار',
          status: 'الحالة',
          remark: 'ملاحظات'
        },
        node: {
          title: 'معلومات العقدة',
          nodeName: 'اسم العقدة',
          nodeType: 'نوع العقدة',
          assigneeType: 'نوع المعين',
          formType: 'نوع النموذج'
        },
        form: {
          title: 'معلومات النموذج',
          fieldName: 'اسم الحقل',
          fieldType: 'نوع الحقل',
          required: 'إلزامي',
          remark: 'ملاحظات'
        }
      }
    }
  }
} 