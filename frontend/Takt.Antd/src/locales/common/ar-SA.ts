export default {
  common: {
    // ==================== معلومات النظام ====================
    system: {
      title: 'منصة بلاك آيس',
      slogan: 'نظام إدارة مؤسسات احترافي وفعال وآمن',
      description: 'نظام إدارة مؤسسات حديث مبني على .NET 8 و Vue 3',
      copyright: '© 2024Takt(Claude AI). جميع الحقوق محفوظة.'
    },

    // ==================== رسائل الخطأ ====================
    error: {
      clientError: 'خطأ في طلب العميل، يرجى التحقق من معلمات الطلب',
      systemRestart: 'النظام قيد الصيانة، يرجى تسجيل الدخول لاحقاً',
      network: 'فشل الاتصال بالشبكة، يرجى التحقق من إعدادات الشبكة',
      unauthorized: 'وصول غير مصرح به، يرجى تسجيل الدخول مرة أخرى',
      forbidden: 'الوصول محظور',
      notFound: 'المورد المطلوب غير موجود',
      badRequest: 'معلمات الطلب غير صالحة',
      serverError: 'خطأ داخلي في الخادم',
      serviceUnavailable: 'الخدمة غير متوفرة مؤقتاً',
      badGateway: 'بوابة سيئة',
      gatewayTimeout: 'انتهت مهلة البوابة',
      unknown: 'خطأ غير معروف'
    },

    // ==================== العمليات الأساسية ====================
    // العناوين الأساسية
    title: {
      list: 'القائمة',
      detail: 'التفاصيل',
      create: 'إضافة جديد',
      edit: 'تعديل',
      preview: 'معاينة'
    },

    // ==================== تعريفات الحالة ====================
    status: {
      // الحالة الأساسية
      base: {
        label: 'الحالة',
        normal: 'عادي',
        disabled: 'معطل',
        placeholder: 'يرجى اختيار الحالة'
      },

      // حالة نعم/لا
      yesNo: {
        all: 'الكل',
        yes: 'نعم',
        no: 'لا'
      },

      // حالة الظهور
      visible: {
        show: 'إظهار',
        hide: 'إخفاء'
      },

      // حالة التخزين المؤقت
      cache: {
        enabled: 'مفعل',
        disabled: 'معطل'
      },

      // حالة الاتصال
      online: {
        online: 'متصل',
        offline: 'غير متصل'
      },

      // حالة المعالجة
      process: {
        pending: 'قيد الانتظار',
        processing: 'قيد المعالجة',
        completed: 'مكتمل',
        failed: 'فشل'
      },

      // حالة التحقق
      verify: {
        unverified: 'غير محقق',
        verified: 'محقق',
        failed: 'فشل التحقق'
      },

      // حالة القفل
      lock: {
        locked: 'مقفل',
        unlocked: 'غير مقفل'
      },

      // حالة النشر
      publish: {
        draft: 'مسودة',
        published: 'منشور',
        unpublished: 'غير منشور'
      },

      // حالة المزامنة
      sync: {
        synced: 'مزامن',
        unsynced: 'غير مزامن',
        syncing: 'جاري المزامنة'
      }
    },

    // ==================== عمليات النموذج ====================
    form: {
      required: 'مطلوب',
      optional: 'اختياري',
      invalid: 'غير صالح',
      // العناصر النائبة للنموذج
      placeholder: {
        select: 'يرجى الاختيار',
        input: 'يرجى الإدخال',
        date: 'يرجى اختيار التاريخ',
        time: 'يرجى اختيار الوقت'
      },
      // نموذج المستخدم
      user: {
        // المعلومات الأساسية
        userId: {
          label: 'معرف المستخدم',
          placeholder: 'يرجى إدخال معرف المستخدم'
        },
        userName: {
          label: 'اسم المستخدم',
          placeholder: 'يرجى إدخال اسم المستخدم'
        },
        nickName: {
          label: 'الاسم المستعار',
          placeholder: 'يرجى إدخال الاسم المستعار'
        },
        realName: {
          label: 'الاسم الحقيقي',
          placeholder: 'يرجى إدخال الاسم الحقيقي'
        },
        englishName: {
          label: 'الاسم بالإنجليزية',
          placeholder: 'يرجى إدخال الاسم بالإنجليزية'
        },
        password: {
          label: 'كلمة المرور',
          placeholder: 'يرجى إدخال كلمة المرور'
        },
        confirmPassword: {
          label: 'تأكيد كلمة المرور',
          placeholder: 'يرجى إدخال كلمة المرور مرة أخرى'
        },
        
        // المعلومات الشخصية
        avatar: {
          label: 'الصورة الشخصية',
          placeholder: 'يرجى رفع الصورة الشخصية'
        },
        gender: {
          label: 'الجنس',
          placeholder: 'يرجى اختيار الجنس',
          options: {
            male: 'ذكر',
            female: 'أنثى',
            other: 'آخر'
          }
        },
        birthday: {
          label: 'تاريخ الميلاد',
          placeholder: 'يرجى اختيار تاريخ الميلاد'
        },
        
        // معلومات الاتصال
        email: {
          label: 'البريد الإلكتروني',
          placeholder: 'يرجى إدخال البريد الإلكتروني'
        },
        phoneNumber: {
          label: 'رقم الهاتف المحمول',
          placeholder: 'يرجى إدخال رقم الهاتف المحمول'
        },
        telephone: {
          label: 'الهاتف الثابت',
          placeholder: 'يرجى إدخال الهاتف الثابت'
        },
        
        // معلومات المؤسسة
        deptId: {
          label: 'القسم',
          placeholder: 'يرجى اختيار القسم'
        },
        roleId: {
          label: 'الدور',
          placeholder: 'يرجى اختيار الدور'
        },
        postId: {
          label: 'المنصب',
          placeholder: 'يرجى اختيار المنصب'
        },
        
        // معلومات الحساب
        userType: {
          label: 'نوع المستخدم',
          placeholder: 'يرجى اختيار نوع المستخدم',
          options: {
            system: 'مستخدم النظام',
            normal: 'مستخدم عادي'
          }
        },
        status: {
          label: 'الحالة',
          placeholder: 'يرجى اختيار الحالة'
        },
        loginIp: {
          label: 'آخر IP تسجيل دخول',
          placeholder: 'آخر IP تسجيل دخول'
        },
        loginDate: {
          label: 'آخر وقت تسجيل دخول',
          placeholder: 'آخر وقت تسجيل دخول'
        },
        
        // معلومات أخرى
        remark: {
          label: 'ملاحظات',
          placeholder: 'يرجى إدخال الملاحظات'
        },
        sort: {
          label: 'الترتيب',
          placeholder: 'يرجى إدخال رقم الترتيب'
        }
      },
      // معلومات الملاحظات
      remark: {
        label: 'ملاحظات',
        placeholder: 'يرجى إدخال الملاحظات'
      }
    },

    // ==================== عمليات الجدول ====================
    table: {
      header: {
        operation: 'العملية'
      },
      config: {
        density: {
          default: 'افتراضي',
          middle: 'متوسط',
          small: 'مدمج'
        },
        columnSetting: 'إعدادات الأعمدة'
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
    },

    // ==================== عمليات الوقت ====================
    datetime: {
      date: 'التاريخ',
      time: 'الوقت',
      year: 'السنة',
      month: 'الشهر',
      day: 'اليوم',
      hour: 'الساعة',
      minute: 'الدقيقة',
      second: 'الثانية',
      startDate: 'تاريخ البداية',
      endDate: 'تاريخ النهاية',
      startTime: 'وقت البداية',
      endTime: 'وقت النهاية',
      createTime: 'وقت الإنشاء',
      updateTime: 'وقت التحديث',
      formatError: 'فشل تنسيق الوقت',
      relativeTimeFormatError: 'فشل تنسيق الوقت النسبي',
      parseError: 'فشل تحليل التاريخ',
      rangeSeparator: ' إلى '
    },

    // ==================== عمليات الملفات ====================
    // الاستيراد/التصدير
    import: {
      title: 'استيراد البيانات',
      file: 'اختر الملف',
      select: 'اختر الملف',
      template: 'تحميل القالب',
      download: 'تحميل القالب',
      note: 'تعليمات الاستيراد',
      tips: 'يرجى اتباع تنسيق قالب الاستيراد بدقة، وإلا قد يفشل الاستيراد',
      format: 'يدعم فقط ملفات Excel!',
      size: 'لا يمكن أن يتجاوز حجم الملف {size} ميجابايت!',
      total: 'إجمالي السجلات',
      success: 'عدد النجاح',
      failed: 'عدد الفشل',
      message: 'سبب الفشل',
      dragText: 'انقر أو اسحب الملف إلى هذه المنطقة للتحميل',
      dragHint: 'يدعم ملفات Excel بتنسيق .xlsx',
      sheetName: 'يرجى التأكد من أن اسم ورقة ملف Excel هو: {sheetName}',
      allSuccess: 'استيراد ناجح {count} سجل، كلها ناجحة!',
      partialSuccess: 'استيراد ناجح {success} سجل، فشل {fail} سجل',
      allFailed: 'فشل جميع الاستيراد، إجمالي {count} سجل',
      noData: 'لم يتم قراءة البيانات',
      empty: 'الملف فارغ، لا يمكن التحميل',
      importFailed: 'فشل الاستيراد',
      templateFileName: 'Import_Template_{time}.xlsx',
      limits: {
        title: 'قيود الاستيراد',
        fileCount: 'حد عدد الملفات: {count} ملف',
        fileSize: 'حد حجم الملف: {size} ميجابايت',
        recordCount: 'حد عدد السجلات: {count} سجل',
        fileFormat: 'تنسيق الملف: يدعم تنسيق .xlsx فقط'
      },
      recordLimit: 'عدد سجلات الاستيراد ({actual} سجل) يتجاوز الحد ({max} سجل)، يرجى الاستيراد على دفعات'
    },

    // التحميل
    upload: {
      text: 'اسحب الملف هنا أو انقر للتحميل',
      picture: 'انقر لتحميل الصورة',
      file: 'انقر لتحميل الملف',
      icon: 'انقر لتحميل الأيقونة',
      limit: {
        size: 'لا يمكن أن يتجاوز حجم الملف {size}',
        type: 'يدعم فقط تنسيق {type}'
      }
    },

    // ==================== أزرار الوظائف ====================
    actions: {
      // === أزرار العمليات الأساسية ===
      add: 'إضافة',           // @btn-add-color
      edit: 'تعديل',          // @btn-edit-color
      delete: 'حذف',          // @btn-delete-color
      batchDelete: 'حذف متعدد', // @btn-batch-delete-color
      view: 'عرض',            // @btn-view-color
      clear: 'مسح',           // @btn-clear-color
      forceOffline: 'إجبار غير متصل', // @btn-force-offline-color
      onlineStatus: 'حالة الاتصال', // @btn-online-status-color
      loginHistory: 'سجل تسجيل الدخول', // @btn-login-history-color
      sendMail: 'إرسال بريد', // @btn-send-mail-color
      viewMail: 'عرض البريد', // @btn-view-mail-color
      mailTemplate: 'قالب البريد', // @btn-mail-template-color
      sendNotification: 'إرسال إشعار', // @btn-send-notification-color
      viewNotification: 'عرض الإشعار', // @btn-view-notification-color
      notificationSetting: 'إعدادات الإشعار', // @btn-notification-setting-color
      sendMessage: 'إرسال رسالة', // @btn-send-message-color
      viewMessage: 'عرض الرسالة', // @btn-view-message-color
      messageSetting: 'إعدادات الرسالة', // @btn-message-setting-color

      // === أزرار عمليات البيانات ===
      import: 'استيراد',      // @btn-import-color
      export: 'تصدير',        // @btn-export-color
      template: 'قالب',       // @btn-template-color
      preview: 'معاينة',      // @btn-preview-color
      download: 'تحميل',      // @btn-download-color
      batchImport: 'استيراد متعدد', // @btn-batch-import-color
      batchExport: 'تصدير متعدد', // @btn-batch-export-color
      batchPrint: 'طباعة متعددة', // @btn-batch-print-color
      batchEdit: 'تعديل متعدد',  // @btn-batch-edit-color
      batchUpdate: 'تحديث متعدد', // @btn-batch-update-color

      // === أزرار عمليات الحالة ===
      logging: 'تدقيق',        // @btn-audit-color
      revoke: 'إلغاء',       // @btn-revoke-color
      stop: 'إيقاف',         // @btn-stop-color
      run: 'تشغيل',          // @btn-run-color
      force: 'إجبار',        // @btn-forced-color
      start: 'بدء',          // @btn-start-color
      suspend: 'تعليق',      // @btn-suspend-color
      resume: 'استئناف',     // @btn-resume-color
      submit: 'إرسال',       // @btn-submit-color
      withdraw: 'سحب',       // @btn-withdraw-color
      terminate: 'إنهاء',     // @btn-terminate-color

      // === أزرار وظائف النظام ===
      generate: 'توليد',      // @btn-generate-color
      refresh: 'تحديث',       // @btn-refresh-color
      info: 'معلومات',        // @btn-info-color
      log: 'سجل',             // @btn-log-color
      chat: 'رسالة',          // @btn-chat-color
      copy: 'نسخ',            // @btn-copy-color
      execute: 'تنفيذ',       // @btn-execute-color
      resetPwd: 'إعادة تعيين كلمة المرور', // @btn-reset-pwd-color
      open: 'فتح',            // @btn-open-color
      close: 'إغلاق',         // @btn-close-color
      more: 'المزيد',         // @btn-more-color
      density: 'الكثافة',     // @btn-density-color
      columnSetting: 'إعدادات الأعمدة', // @btn-column-setting-color

      // === أزرار الوظائف الممتدة ===
      search: 'بحث',          // @btn-search-color
      filter: 'تصفية',        // @btn-filter-color
      sort: 'ترتيب',          // @btn-sort-color
      config: 'تكوين',        // @btn-config-color
      save: 'حفظ',            // @btn-save-color
      cancel: 'إلغاء',        // @btn-cancel-color
      upload: 'تحميل',        // @btn-upload-color
      print: 'طباعة',         // @btn-print-color
      help: 'مساعدة',         // @btn-help-color
      share: 'مشاركة',        // @btn-share-color
      lock: 'قفل',            // @btn-lock-color
      sync: 'مزامنة',         // @btn-sync-color
      expand: 'توسيع',        // @btn-expand-color
      collapse: 'طي',         // @btn-collapse-color
      approve: 'موافقة',      // @btn-approve-color
      reject: 'رفض',          // @btn-reject-color
      comment: 'تعليق',       // @btn-comment-color
      attach: 'مرفق',         // @btn-attach-color

      // === أزرار دعم اللغة ===
      translate: 'ترجمة',     // @btn-translate-color
      langSwitch: 'تبديل اللغة', // @btn-lang-switch-color
      dict: 'قاموس',          // @btn-dict-color

      // === أزرار تحليل البيانات ===
      analyze: 'تحليل',       // @btn-analyze-color
      chart: 'رسم بياني',     // @btn-chart-color
      report: 'تقرير',        // @btn-report-color
      dashboard: 'لوحة التحكم', // @btn-dashboard-color
      statistics: 'إحصائيات', // @btn-statistics-color
      forecast: 'تنبؤ',       // @btn-forecast-color
      compare: 'مقارنة',      // @btn-compare-color

      // === أزرار سير العمل ===
      startFlow: 'بدء سير العمل', // @btn-start-flow-color
      endFlow: 'إنهاء سير العمل', // @btn-end-flow-color
      suspendFlow: 'تعليق سير العمل', // @btn-suspend-flow-color
      resumeFlow: 'استئناف سير العمل', // @btn-resume-flow-color
      transfer: 'تحويل',      // @btn-transfer-color
      delegate: 'تفويض',      // @btn-delegate-color
      notify: 'إشعار',        // @btn-notify-color
      urge: 'تذكير',          // @btn-urge-color
      sign: 'توقيع',          // @btn-sign-color
      countersign: 'توقيع موافقة', // @btn-countersign-color

      // === أزرار خاصة بالهواتف المحمولة ===
      scan: 'مسح',            // @btn-scan-color
      location: 'موقع',       // @btn-location-color
      call: 'اتصال',          // @btn-call-color
      photo: 'التقط صورة',     // @btn-photo-color
      voice: 'صوت',           // @btn-voice-color
      faceId: 'التعرف على الوجه', // @btn-face-id-color
      fingerPrint: 'بصمة الإصبع', // @btn-finger-print-color

      // === أزرار التعاون الاجتماعي ===
      follow: 'متابعة',       // @btn-follow-color
      collect: 'جمع',         // @btn-collect-color
      like: 'إعجاب',          // @btn-like-color
      forward: 'إعادة توجيه', // @btn-forward-color
      at: '@',                // @btn-at-color
      group: 'مجموعة',        // @btn-group-color
      team: 'فريق',           // @btn-team-color

      // === أزرار المصادقة الأمنية ===
      verifyCode: 'رمز التحقق', // @btn-verify-code-color
      bind: 'ربط',             // @btn-bind-color
      unbind: 'فك الربط',      // @btn-unbind-color
      authorize: 'تفويض',      // @btn-authorize-color
      deauthorize: 'إلغاء التفويض', // @btn-deauthorize-color

      // === أزرار الوظائف المتقدمة ===
      version: 'إصدار',       // @btn-version-color
      history: 'تاريخ',       // @btn-history-color
      restore: 'استعادة',     // @btn-restore-color
      archive: 'أرشيف',       // @btn-archive-color
      unarchive: 'إلغاء الأرشفة', // @btn-unarchive-color
      merge: 'دمج',           // @btn-merge-color
      split: 'تقسيم',         // @btn-split-color

      // === أزرار إدارة النظام ===
      backup: 'نسخ احتياطي',   // @btn-backup-color
      restoreSys: 'استعادة النظام', // @btn-restore-sys-color
      clean: 'تنظيف',         // @btn-clean-color
      optimize: 'تحسين',      // @btn-optimize-color
      monitor: 'مراقبة',      // @btn-monitor-color
      diagnose: 'تشخيص',      // @btn-diagnose-color
      maintain: 'صيانة'       // @btn-maintain-color
    },

    // ==================== النتائج والرسائل ====================
    // حالة النتيجة
    result: {
      success: 'تمت العملية بنجاح',
      failed: 'فشلت العملية',
      warning: 'تحذير',
      info: 'معلومات',
      error: 'خطأ'
    },

    // رسائل التلميح
    message: {
      loading: 'جاري المعالجة...',
      saving: 'جاري الحفظ...',
      submitting: 'جاري الإرسال...',
      deleting: 'جاري الحذف...',
      operationSuccess: 'تمت العملية بنجاح',
      operationFailed: 'فشلت العملية',
      deleteConfirm: 'هل أنت متأكد من الحذف؟',
      deleteSuccess: 'تم الحذف بنجاح',
      deleteFailed: 'فشل الحذف',
      createSuccess: 'تمت الإضافة بنجاح',
      createFailed: 'فشلت الإضافة',
      updateSuccess: 'تم التحديث بنجاح',
      updateFailed: 'فشل التحديث',
      networkError: 'فشل الاتصال بالشبكة، يرجى التحقق من الشبكة',
      systemError: 'خطأ في النظام',
      timeout: 'انتهت مهلة الطلب',
      invalidResponse: 'تنسيق بيانات الاستجابة غير صالح',
      backendNotStarted: 'خدمة الخادم الخلفي غير مشغلة، يرجى التحقق من حالة الخدمة',
      invalidRequest: 'معلمات الطلب غير صالحة',
      unauthorized: 'غير مصرح به، يرجى تسجيل الدخول مرة أخرى',
      forbidden: 'الوصول محظور',
      notFound: 'المورد المطلوب غير موجود',
      serverError: 'خطأ داخلي في الخادم',
      httpError: {
        400: 'معلمات الطلب غير صالحة',
        401: 'غير مصرح به، يرجى تسجيل الدخول مرة أخرى',
        403: 'الوصول محظور',
        404: 'المورد المطلوب غير موجود',
        500: 'خطأ داخلي في الخادم',
        502: 'بوابة سيئة',
        503: 'الخدمة غير متوفرة',
        504: 'انتهت مهلة البوابة'
      },
      loadFailed: 'فشل التحميل',
      forceOfflineConfirm: 'هل أنت متأكد من إجبار هذا المستخدم غير متصل؟',
      forceOfflineSuccess: 'تم إجبار المستخدم غير متصل بنجاح',
      forceOfflineFailed: 'فشل إجبار المستخدم غير متصل'
    },

    // ==================== نص المكونات ====================
    // علامات التبويب
    tab: {
      // === المعلومات الأساسية ===
      basic: 'المعلومات الأساسية',
      profile: 'الملف الشخصي',
      avatar: 'إعدادات الصورة الشخصية',
      password: 'إعدادات كلمة المرور',
      security: 'إعدادات الأمان',

      // === توليد الكود ===
      codegen: 'توليد الكود',
      codegenBasic: 'تكوين التوليد',
      codegenField: 'تكوين الحقول',
      codegenPreview: 'معاينة التوليد',
      codegenTemplate: 'تكوين القالب',
      codegenImport: 'تكوين الاستيراد',
      
      // === سير العمل ===
      workflow: 'سير العمل',
      flowDesign: 'تصميم سير العمل',
      flowDeploy: 'نشر سير العمل',
      flowInstance: 'مثيل سير العمل',
      flowTask: 'إدارة المهام',
      flowForm: 'تكوين النموذج',
      flowNotify: 'إشعارات الرسائل',
      
      // === إدارة النظام ===
      permission: 'صلاحيات البيانات',
      log: 'سجل العمليات',
      dept: 'القسم والمنصب',
      role: 'الدور والصلاحيات',
      config: 'تكوين المعلمات',
      task: 'المهام المجدولة',
      monitor: 'مراقبة النظام'
    },

    // نص الأزرار
    button: {
      submit: 'إرسال',
      confirm: 'تأكيد',
      ok: 'موافق',
      cancel: 'إلغاء',
      close: 'إغلاق',
      reset: 'إعادة تعيين',
      clear: 'مسح',
      save: 'حفظ',
      delete: 'حذف'
    }
  },

  // ==================== مكون الاختيار ====================
  select: {
    loadMore: 'تحميل المزيد',
    loading: 'جاري التحميل...',
    notFound: 'لا توجد بيانات',
    selected: 'تم تحديد {count} عنصر',
    selectedTotal: 'إجمالي {total} عنصر',
    clear: 'مسح',
    search: 'بحث',
    all: 'الكل',
    // رسائل الخطأ
    loadError: 'فشل تحميل البيانات',
    searchError: 'فشل البحث',
    networkError: 'فشل الاتصال بالشبكة',
    serverError: 'خطأ في الخادم',
    unknownError: 'خطأ غير معروف'
  }
}


