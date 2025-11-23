export default {
  identity: {
    menu: {
      title: 'إدارة القوائم',
      columns: {
        menuName: 'اسم القائمة',
        transKey: 'مفتاح الترجمة',
        parentId: 'القائمة الرئيسية',
        orderNum: 'الترتيب',
        path: 'مسار التوجيه',
        component: 'مسار المكون',
        queryParams: 'معاملات التوجيه',
        isExternal: 'رابط خارجي',
        isCache: 'تخزين مؤقت',
        menuType: 'نوع القائمة',
        visible: 'حالة العرض',
        status: 'الحالة',
        perms: 'مفتاح الصلاحية',
        icon: 'الأيقونة',
        id: 'المعرف',
        createBy: 'أنشئ بواسطة',
        createTime: 'وقت الإنشاء',
        updateBy: 'تم التحديث بواسطة',
        updateTime: 'وقت التحديث',
        deleteBy: 'تم الحذف بواسطة',
        deleteTime: 'وقت الحذف',
        isDeleted: 'تم الحذف',
        remark: 'ملاحظة',
        action: 'إجراء'
      },
      fields: {
        menuName: {
          label: 'اسم القائمة',
          placeholder: 'يرجى إدخال اسم القائمة',
          validation: {
            required: 'يرجى إدخال اسم القائمة',
            length: 'يجب أن يكون طول اسم القائمة بين 2 و50 حرفًا'
          }
        },
        transKey: {
          label: 'مفتاح الترجمة',
          placeholder: 'يرجى إدخال مفتاح الترجمة'
        },
        parentId: {
          label: 'القائمة الرئيسية',
          placeholder: 'يرجى اختيار القائمة الرئيسية',
          root: 'القائمة الجذرية'
        },
        orderNum: {
          label: 'الترتيب',
          placeholder: 'يرجى إدخال الترتيب',
          validation: {
            required: 'يرجى إدخال الترتيب'
          }
        },
        path: {
          label: 'مسار التوجيه',
          placeholder: 'يرجى إدخال مسار التوجيه'
        },
        component: {
          label: 'مسار المكون',
          placeholder: 'يرجى إدخال مسار المكون'
        },
        queryParams: {
          label: 'معاملات التوجيه',
          placeholder: 'يرجى إدخال معاملات التوجيه'
        },
        isExternal: {
          label: 'رابط خارجي',
          placeholder: 'يرجى اختيار ما إذا كان رابط خارجي',
          options: {
            yes: 'نعم',
            no: 'لا'
          }
        },
        isCache: {
          label: 'تخزين مؤقت',
          placeholder: 'يرجى اختيار ما إذا كان هناك تخزين مؤقت',
          options: {
            yes: 'نعم',
            no: 'لا'
          }
        },
        menuType: {
          label: 'نوع القائمة',
          options: {
            directory: 'دليل',
            menu: 'قائمة',
            button: 'زر'
          },
          validation: {
            required: 'يرجى اختيار نوع القائمة'
          }
        },
        visible: {
          label: 'حالة العرض',
          placeholder: 'يرجى اختيار حالة العرض',
          options: {
            show: 'عرض',
            hide: 'إخفاء'
          }
        },
        status: {
          label: 'الحالة',
          placeholder: 'يرجى اختيار الحالة',
          options: {
            normal: 'عادي',
            disabled: 'معطل'
          }
        },
        perms: {
          label: 'مفتاح الصلاحية',
          placeholder: 'يرجى إدخال مفتاح الصلاحية'
        },
        icon: {
          label: 'أيقونة القائمة',
          placeholder: 'يرجى إدخال أيقونة القائمة'
        },
        }
      },
      dialog: {
        create: 'إضافة قائمة',
        update: 'تعديل القائمة',
        delete: 'حذف القائمة'
      },
      operation: {
        add: {
          title: 'إضافة قائمة',
          success: 'تمت الإضافة بنجاح',
          failed: 'فشلت الإضافة'
        },
        edit: {
          title: 'تعديل القائمة',
          success: 'تم التعديل بنجاح',
          failed: 'فشل التعديل'
        },
        delete: {
          title: 'حذف القائمة',
          confirm: 'هل أنت متأكد أنك تريد حذف هذه القائمة؟',
          success: 'تم الحذف بنجاح',
          failed: 'فشل الحذف'
        }
      }
    }
  }
