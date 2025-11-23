export default {
  table: {
    viewMode: {
      normal: 'جدول تقليدي',
      transpose: 'جدول منقول'
    },
    columns: {
      id: 'المعرف',
      remark: 'ملاحظة',
      createBy: 'تم الإنشاء بواسطة',
      createTime: 'وقت الإنشاء',
      updateBy: 'تم التحديث بواسطة',
      updateTime: 'وقت التحديث',
      deleteBy: 'تم الحذف بواسطة',
      deleteTime: 'وقت الحذف',
      isDeleted: 'محذوف',
      operation: 'عملية',
    },
    config: {
      density: {
        default: 'افتراضي',
        middle: 'متوسط',
        small: 'مضغوط'
      },
      columnSetting: 'إعداد العمود'
    },
    pagination: {
      total: 'إجمالي {total} عنصر',
      current: 'الصفحة {current}',
      pageSize: '{pageSize} عنصر في الصفحة',
      jump: 'انتقل إلى'
    },
    empty: 'لا توجد بيانات',
    loading: 'جاري التحميل...',
    selectAll: 'تحديد الكل',
    selected: 'تم تحديد {total} عنصر'
  }
}