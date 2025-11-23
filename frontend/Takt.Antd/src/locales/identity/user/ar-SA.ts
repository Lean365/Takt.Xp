export default {
  identity: {
    user: {
      title: 'إدارة المستخدمين',
      profile: 'المعلومات الشخصية',
      changePasswordTitle: 'تغيير كلمة المرور',
      
      // متعلق بكلمة المرور
      password: {
        old: {
          label: 'كلمة المرور القديمة',
          placeholder: 'يرجى إدخال كلمة المرور القديمة',
          validation: {
            required: 'كلمة المرور القديمة لا يمكن أن تكون فارغة',
          }
        },
        new: {
          label: 'كلمة المرور الجديدة',
          placeholder: 'يرجى إدخال كلمة المرور الجديدة',
          validation: {
            required: 'كلمة المرور الجديدة لا يمكن أن تكون فارغة',
          }
        },
        confirm: {
          label: 'تأكيد كلمة المرور',
          placeholder: 'يرجى إدخال تأكيد كلمة المرور',
          validation: {
            required: 'تأكيد كلمة المرور لا يمكن أن يكون فارغاً',
          }
        },
      },
      tabs: {
        userInfo: 'معلومات المستخدم',
        organization: 'العلاقات التنظيمية',
        avatar: 'الصورة الشخصية'
      },
      // تعريفات حقول النموذج
      fields: {
        userId: {
          label: 'معرف المستخدم'
        },
        userName: {
          label: 'اسم المستخدم',
          placeholder: 'يرجى إدخال اسم المستخدم',
          validation: {
            required: 'اسم المستخدم لا يمكن أن يكون فارغاً',
            format: 'يجب أن يبدأ بحرف صغير، يمكن أن يحتوي فقط على أحرف صغيرة وأرقام وشرطات، لا نقاط أو شرطات سفلية، الطول بين 4-50 حرف'
          }
        },
        nickName: {
          label: 'الاسم المستعار',
          placeholder: 'يرجى إدخال الاسم المستعار',
          validation: {
            required: 'الاسم المستعار لا يمكن أن يكون فارغاً',
            format: '2-50 حرف، يسمح بالصينية واليابانية والكورية والإنجليزية والأرقام والنقاط الإنجليزية والشرطات، لا يسمح بالشرطات السفلية والمسافات'
          }
        },
        realName: {
          label: 'الاسم الحقيقي',
          placeholder: 'يرجى إدخال الاسم الحقيقي',
          validation: {
            required: 'الاسم الحقيقي لا يمكن أن يكون فارغاً',
            format: 'طول الاسم الحقيقي يجب أن يكون بين 2-20 حرف، يمكن أن يحتوي فقط على الصينية والإنجليزية والأرقام والشرطات السفلية'
          }
        },
        fullName: {
          label: 'الاسم الكامل',
          placeholder: 'يرجى إدخال الاسم الكامل',
          validation: {
            required: 'الاسم الكامل لا يمكن أن يكون فارغاً',
            format: 'طول الاسم الكامل يجب أن يكون بين 2-20 حرف، يمكن أن يحتوي فقط على الصينية والإنجليزية والأرقام والشرطات السفلية'
          }
        },
        englishName: {
          label: 'الاسم الإنجليزي',
          placeholder: 'يرجى إدخال الاسم الإنجليزي',
          validation: {
            required: 'الاسم الإنجليزي لا يمكن أن يكون فارغاً',
            format: 'طول الاسم الإنجليزي يجب أن يكون بين 2-100 حرف، يجب أن يبدأ بحرف، يمكن أن يحتوي فقط على الأحرف الإنجليزية والمسافات والشرطات والنقاط الإنجليزية'
          }
        },
        password: {
          label: 'كلمة المرور',
          placeholder: 'يرجى إدخال كلمة المرور',
          validation: {
            required: 'كلمة المرور لا يمكن أن تكون فارغة',
            format: 'طول كلمة المرور يجب أن يكون بين 6-20 حرف، يمكن أن تحتوي فقط على الأحرف الإنجليزية والأرقام والرموز الخاصة'
          }
        },
        userType: {
          label: 'نوع المستخدم',
          placeholder: 'يرجى اختيار نوع المستخدم',
          options: {
            admin: 'مدير',
            user: 'مستخدم عادي'
          }
        },
        email: {
          label: 'البريد الإلكتروني',
          placeholder: 'يرجى إدخال البريد الإلكتروني',
          validation: {
            required: 'البريد الإلكتروني لا يمكن أن يكون فارغاً',
            invalid: 'طول البريد الإلكتروني يجب أن يكون بين 6-100 حرف وبالتنسيق الصحيح'
          }
        },
        phoneNumber: {
          label: 'رقم الهاتف',
          placeholder: 'يرجى إدخال رقم الهاتف',
          validation: {
            required: 'رقم الهاتف لا يمكن أن يكون فارغاً',
            invalid: 'يرجى إدخال التنسيق الصحيح لرقم الهاتف المحمول أو الثابت'
          }
        },
        gender: {
          label: 'الجنس',
          placeholder: 'يرجى اختيار الجنس',
          options: {
            male: 'ذكر',
            female: 'أنثى',
            unknown: 'غير معروف'
          }
        },
        avatar: {
          label: 'الصورة الشخصية',
          upload: 'رفع الصورة الشخصية',
          currentAvatar: 'الصورة الشخصية الحالية',
          avatarUpload: 'رفع الصورة الشخصية',
          uploadSuccess: 'تم رفع الصورة الشخصية بنجاح',
          uploadError: 'فشل في رفع الصورة الشخصية',
          uploading: 'جاري رفع الصورة الشخصية...',
          onlyImage: 'يمكن رفع ملفات الصور فقط!',
          sizeLimit: 'حجم الصورة لا يمكن أن يتجاوز 5 ميجابايت!'
        },
        status: {
          label: 'الحالة',
          placeholder: 'يرجى اختيار الحالة',
          options: {
            enabled: 'مفعل',
            disabled: 'معطل'
          }
        },
        lastPasswordChangeTime: {
          label: 'وقت آخر تغيير لكلمة المرور'
        },
        lockEndTime: {
          label: 'وقت انتهاء القفل'
        },
        lockReason: {
          label: 'سبب القفل'
        },
        isLock: {
          label: 'مقفل'
        },
        errorLimit: {
          label: 'حد عدد الأخطاء'
        },
        loginCount: {
          label: 'عدد مرات تسجيل الدخول'
        },
        deptName: {
          label: 'القسم',
          placeholder: 'يرجى اختيار القسم',
          validation: {
            required: 'القسم لا يمكن أن يكون فارغاً'
          }
        },
        role: {
          label: 'الدور',
          placeholder: 'يرجى اختيار الدور',
          validation: {
            required: 'الدور لا يمكن أن يكون فارغاً'
          }
        },
        post: {
          label: 'المنصب',
          placeholder: 'يرجى اختيار المنصب',
          validation: {
            required: 'المنصب لا يمكن أن يكون فارغاً'
          }
        },
        remark: {
          label: 'ملاحظات',
          placeholder: 'يرجى إدخال الملاحظات'
        }
      },

      // أزرار الإجراءات
      actions: {
        add: 'إضافة مستخدم',
        edit: 'تعديل المستخدم',
        'delete': 'حذف المستخدم',
        resetPassword: 'إعادة تعيين كلمة المرور',
        export: 'تصدير المستخدمين'
      },

      // رسائل التنبيه
      messages: {
        confirmDelete: 'هل أنت متأكد من أنك تريد حذف المستخدم المحدد؟',
        confirmResetPassword: 'هل أنت متأكد من أنك تريد إعادة تعيين كلمة مرور المستخدم المحدد؟',
        confirmToggleStatus: 'هل أنت متأكد من أنك تريد {action} هذا المستخدم؟',
        deleteSuccess: 'تم حذف المستخدم بنجاح',
        deleteFailed: 'فشل في حذف المستخدم',
        saveSuccess: 'تم حفظ معلومات المستخدم بنجاح',
        saveFailed: 'فشل في حفظ معلومات المستخدم',
        resetPasswordSuccess: 'تم إعادة تعيين كلمة المرور بنجاح',
        resetPasswordFailed: 'فشل في إعادة تعيين كلمة المرور',
        importSuccess: 'تم استيراد المستخدم بنجاح',
        importFailed: 'فشل في استيراد المستخدم',
        exportSuccess: 'تم تصدير المستخدم بنجاح',
        exportFailed: 'فشل في تصدير المستخدم',
        toggleStatusSuccess: 'تم تغيير الحالة بنجاح',
        toggleStatusFailed: 'فشل في تغيير الحالة',
        cannotDeleteAdmin: 'لا يمكن حذف المستخدمين الإداريين!',
        cannotEditAdmin: 'لا يمكن تعديل المستخدمين الإداريين!',
        cannotUpdateAdminStatus: 'لا يمكن تغيير حالة المستخدمين الإداريين!',
        cannotResetAdminPassword: 'لا يمكن إعادة تعيين كلمة مرور المستخدمين الإداريين!',
        cannotUnlockAdmin: 'لا يمكن إلغاء قفل المستخدمين الإداريين!',
        cannotAllocateRole: 'لا يمكن تخصيص أدوار للمستخدمين الإداريين!',
        cannotAllocateDept: 'لا يمكن تخصيص أقسام للمستخدمين الإداريين!',
        cannotAllocatePost: 'لا يمكن تخصيص مناصب للمستخدمين الإداريين!',
        statusUpdateSuccess: 'تم تحديث الحالة بنجاح',
        statusUpdateFailed: 'فشل في تحديث الحالة',
        lockStatusUpdateSuccess: 'تم تحديث حالة القفل بنجاح',
        lockStatusUpdateFailed: 'فشل في تحديث حالة القفل'
      },

      // نص عرض الجدول
      table: {
        notLocked: 'غير مقفل',
        none: 'لا يوجد',
        queryParams: 'معاملات الاستعلام',
        importResponseData: 'بيانات استجابة الاستيراد',
        parsedData: 'البيانات المحللة',
        exportFailed: 'فشل في التصدير',
        downloadTemplateFailed: 'فشل في تحميل القالب',
        rowClicked: 'تم النقر على الصف',
        toggleFullscreenState: 'تبديل حالة الشاشة الكاملة',
        getOptionsFailed: 'فشل في الحصول على الخيارات',
        createUser: 'إنشاء مستخدم',
        updateUser: 'تحديث المستخدم'
      },

      // نصائح الاستيراد
      importTips: 'يجب أن يكون اسم ورقة Excel "User"',

      // التبويبات
      tab: {
        basic: 'المعلومات الأساسية',
        profile: 'الملف الشخصي',
        role: 'أذونات الدور',
        dept: 'القسم والمنصب',
        other: 'معلومات أخرى',
        avatar: 'إعدادات الصورة الشخصية',
        loginLog: 'سجل تسجيل الدخول',
        operateLog: 'سجل العمليات',
        errorLog: 'سجل الأخطاء',
        taskLog: 'سجل المهام'
      },

      // الاستيراد/التصدير
      import: {
        title: 'استيراد المستخدمين',
        template: 'تحميل القالب',
        success: 'تم الاستيراد بنجاح',
        failed: 'فشل في الاستيراد',
        fileName: 'بيانات المستخدمين'
      },
      export: {
        title: 'تصدير المستخدمين',
        fileName: 'بيانات المستخدمين',
        success: 'تم التصدير بنجاح',
        failed: 'فشل في التصدير'
      },
      template: {
        fileName: 'قالب استيراد المستخدمين',
        downloadFailed: 'فشل في تحميل القالب'
      },

      // إعادة تعيين كلمة المرور
      resetPwd: 'إعادة تعيين كلمة المرور',
      
      // تغيير كلمة المرور
      changePassword: {
        success: 'تم تغيير كلمة المرور بنجاح',
        failed: 'فشل في تغيير كلمة المرور، يرجى المحاولة مرة أخرى',
        changeFailed: 'فشل في تغيير كلمة المرور'
      },

      // متعلق بالتخصيص
      allocateDept: 'تخصيص قسم',
      allocatePost: 'تخصيص منصب',
      allocateRole: 'تخصيص دور',
      
      roleAllocate: {
        unallocated: 'غير مخصص',
        allocated: 'مخصص',
        loadRoleListFailed: 'فشل في تحميل قائمة الأدوار',
        startLoadUserRoles: 'بدء تحميل أدوار المستخدم',
        userRolesApiResponse: 'استجابة API أدوار المستخدم',
        setSelectedRoles: 'تعيين الأدوار المحددة',
        loadUserRolesFailed: 'فشل في تحميل أدوار المستخدم'
      },
      
      postAllocate: {
        unallocated: 'غير مخصص',
        allocated: 'مخصص',
        loadPostListFailed: 'فشل في تحميل قائمة المناصب',
        loadUserPostsFailed: 'فشل في تحميل مناصب المستخدم'
      }
    }
  }
}
