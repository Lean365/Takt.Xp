export default {
  workflow: {
    transition: {
      title: 'انتقال سير العمل',
      fields: {
        transitionId: 'معرف الانتقال',
        sourceNodeId: 'معرف العقدة المصدر',
        sourceNodeName: 'اسم العقدة المصدر',
        targetNodeId: 'معرف العقدة الهدف',
        targetNodeName: 'اسم العقدة الهدف',
        condition: 'شرط الانتقال',
        definitionId: 'معرف التعريف',
        definitionName: 'اسم التعريف',
        remark: 'ملاحظة',
      },
      placeholder: {
        sourceNodeId: 'اختر العقدة المصدر',
        targetNodeId: 'اختر العقدة الهدف',
        condition: 'أدخل شرط الانتقال',
        definitionId: 'اختر تعريف سير العمل',
        remark: 'أدخل ملاحظة',
        validation: {
          sourceNodeId: {
            required: 'اختر العقدة المصدر',
          },
          targetNodeId: {
            required: 'اختر العقدة الهدف',
          },
          definitionId: {
            required: 'اختر تعريف سير العمل',
          },
          condition: {
            length: 'شرط الانتقال يجب أن يحتوي على 2-2000 حرف',
          },
          remark: {
            length: 'الملاحظة يجب أن تحتوي على 2-200 حرف',
          },
        },
      },
    },
  },
} 