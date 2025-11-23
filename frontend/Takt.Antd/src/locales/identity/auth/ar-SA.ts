export default {
  identity: {
    auth: {
      // تعريفات حقول النموذج - جميع حقول النموذج يتم التحقق منها بشكل موحد في fields
      fields: {
        username: {
          label: 'اسم المستخدم',
          placeholder: 'يرجى إدخال اسم المستخدم',
          validation: {
            required: 'يرجى إدخال اسم المستخدم',
            length: 'يجب أن يكون طول اسم المستخدم بين 3-50 حرف'
          }
        },
        password: {
          label: 'كلمة المرور',
          placeholder: 'يرجى إدخال كلمة المرور',
          validation: {
            required: 'يرجى إدخال كلمة المرور',
            length: 'يجب أن يكون طول كلمة المرور بين 6-100 حرف'
          }
        },
        email: {
          label: 'البريد الإلكتروني',
          placeholder: 'يرجى إدخال البريد الإلكتروني',
          validation: {
            required: 'يرجى إدخال البريد الإلكتروني',
            format: 'يرجى إدخال تنسيق بريد إلكتروني صحيح'
          }
        },
        phone: {
          label: 'رقم الهاتف',
          placeholder: 'يرجى إدخال رقم الهاتف',
          validation: {
            required: 'يرجى إدخال رقم الهاتف',
            format: 'يرجى إدخال تنسيق رقم هاتف صحيح'
          }
        },
        captcha: {
          label: 'رمز التحقق',
          placeholder: 'يرجى إدخال رمز التحقق',
          validation: {
            required: 'يرجى إدخال رمز التحقق'
          }
        },
        confirmPassword: {
          label: 'تأكيد كلمة المرور',
          placeholder: 'يرجى إدخال كلمة المرور مرة أخرى',
          validation: {
            required: 'يرجى تأكيد كلمة المرور',
            mismatch: 'كلمات المرور المدخلة غير متطابقة'
          }
        },
        nickName: {
          label: 'الاسم المستعار',
          placeholder: 'يرجى إدخال الاسم المستعار',
          validation: {
            required: 'الاسم المستعار لا يمكن أن يكون فارغاً',
            format: '2-50 حرف، يسمح بالصينية واليابانية والكورية والإنجليزية والأرقام والنقاط والشرطات، لا يسمح بالشرطات السفلية والمسافات'
          }
        },
        realName: {
          label: 'الاسم الحقيقي',
          placeholder: 'يرجى إدخال الاسم الحقيقي',
          validation: {
            required: 'الاسم الحقيقي لا يمكن أن يكون فارغاً',
            format: 'يجب أن يكون طول الاسم الحقيقي بين 2-20 حرف، يمكن أن يحتوي فقط على الصينية والإنجليزية والأرقام والشرطات السفلية'
          }
        },
        fullName: {
          label: 'الاسم الكامل',
          placeholder: 'يرجى إدخال الاسم الكامل',
          validation: {
            required: 'الاسم الكامل لا يمكن أن يكون فارغاً',
            format: 'يجب أن يكون طول الاسم الكامل بين 2-20 حرف، يمكن أن يحتوي فقط على الصينية والإنجليزية والأرقام والشرطات السفلية'
          }
        },
        englishName: {
          label: 'الاسم بالإنجليزية',
          placeholder: 'يرجى إدخال الاسم بالإنجليزية',
          validation: {
            required: 'الاسم بالإنجليزية لا يمكن أن يكون فارغاً',
            format: 'يجب أن يكون طول الاسم بالإنجليزية بين 2-100 حرف، يجب أن يبدأ بحرف، يمكن أن يحتوي فقط على الأحرف الإنجليزية والمسافات والشرطات والنقاط'
          }
        },
        verificationCode: {
          label: 'رمز التحقق',
          placeholder: 'يرجى إدخال رمز التحقق المكون من 6 أرقام',
          validation: {
            required: 'يرجى إدخال رمز التحقق',
            length: 'يجب أن يكون رمز التحقق مكوناً من 6 أرقام',
            format: 'يجب أن يكون رمز التحقق مكوناً من 6 أرقام'
          }
        },
        newPassword: {
          label: 'كلمة المرور الجديدة',
          placeholder: 'يرجى إدخال كلمة المرور الجديدة',
          validation: {
            required: 'يرجى إدخال كلمة المرور الجديدة',
            length: 'يجب أن يكون طول كلمة المرور بين 6-20 حرف',
            format: 'يجب أن تحتوي كلمة المرور على أحرف كبيرة وصغيرة وأرقام'
          }
        },
        gender: {
          label: 'الجنس',
          placeholder: 'يرجى اختيار الجنس',
          validation: {
            required: 'يرجى اختيار الجنس'
          },
          options: {
            unknown: 'سري',
            male: 'ذكر',
            female: 'أنثى'
          }
        },
        userType: {
          label: 'نوع المستخدم',
          placeholder: 'يرجى اختيار نوع المستخدم',
          validation: {
            required: 'يرجى اختيار نوع المستخدم'
          },
          options: {
            normal: 'مستخدم عادي',
            admin: 'مدير'
          }
        },
        status: {
          label: 'الحالة',
          placeholder: 'يرجى اختيار الحالة',
          validation: {
            required: 'يرجى اختيار الحالة'
          },
          options: {
            normal: 'عادي',
            disabled: 'معطل'
          }
        },
        deptId: {
          label: 'القسم',
          placeholder: 'يرجى إدخال معرف القسم',
          validation: {
            required: 'يرجى إدخال معرف القسم'
          }
        },
        roleIds: {
          label: 'الأدوار',
          placeholder: 'يرجى اختيار الأدوار',
          validation: {
            required: 'يرجى اختيار الأدوار'
          }
        },
        postIds: {
          label: 'المناصب',
          placeholder: 'يرجى اختيار المناصب',
          validation: {
            required: 'يرجى اختيار المناصب'
          }
        },
        deptIds: {
          label: 'الأقسام التابعة',
          placeholder: 'يرجى اختيار الأقسام التابعة',
          validation: {
            required: 'يرجى اختيار الأقسام التابعة'
          }
        },
        remark: {
          label: 'ملاحظات',
          placeholder: 'يرجى إدخال الملاحظات'
        }
      },
      
      // تسجيل الدخول
      login: {
        title: 'تسجيل الدخول',
        rememberMe: 'تذكر',
        forgotPassword: 'نسيت كلمة المرور؟',
        submit: 'تسجيل الدخول',
        success: 'تم تسجيل الدخول بنجاح',
        noAccount: 'ليس لديك حساب بعد؟',
        registerNow: 'سجل الآن',
        notAvailable: 'الوظيفة غير متاحة مؤقتاً',
        error: {
          invalidCredentials: 'اسم المستخدم أو كلمة المرور غير صحيحة',
          userDisabled: 'المستخدم معطل',
          userNotFound: 'المستخدم غير موجود',
          serverError: 'خطأ في الخادم، يرجى المحاولة لاحقاً',
          unknown: 'فشل تسجيل الدخول، يرجى المحاولة لاحقاً'
        }
      },
      
      // تسجيل المستخدم
      register: {
        title: 'تسجيل المستخدم',
        subtitle: 'يرجى إكمال تسجيل المستخدم خطوة بخطوة',
        step1: 'التحقق من الهوية',
        step2: 'المعلومات الأساسية',
        step3: 'معلومات مفصلة',
        step4: 'إعداد الأذونات',
        roleUser: 'مستخدم',
        roleAdmin: 'مدير',
        postEmployee: 'موظف',
        postManager: 'مدير',
        deptIT: 'قسم تكنولوجيا المعلومات',
        deptHR: 'قسم الموارد البشرية',
        agreement: 'لقد قرأت وأوافق على',
        agreementPrefix: 'لقد قرأت وأوافق على',
        agreementLink: 'شروط المستخدم',
        agreementSuffix: '',
        agreementTitle: 'اتفاقية تسجيل المستخدم',
        agreementContent: 'يرجى القراءة بعناية والموافقة على هذه الاتفاقية قبل التسجيل.',
        submit: 'إكمال التسجيل',
        nextStep: 'الخطوة التالية',
        back: 'الخطوة السابقة',
        backToLogin: 'العودة لتسجيل الدخول',
        login: 'تسجيل الدخول بحساب موجود',
        success: 'تم التسجيل بنجاح',
        successTitle: 'تم التسجيل بنجاح',
        successSubtitle: 'تم إنشاء حسابك بنجاح، يرجى تسجيل الدخول بحسابك الجديد',
        successMessage: 'تم تسجيل المستخدم {userName} بنجاح',
        step1Success: 'تم التحقق من الهوية بنجاح',
        step2Success: 'تم التحقق من رمز التحقق بنجاح',
        step3Success: 'تم إكمال المعلومات بنجاح',
        step4Success: 'تم إعداد الصلاحيات بنجاح',
        form: {
          agreementRequired: 'يرجى قراءة شروط المستخدم والموافقة عليها'
        },
        error: {
          step1Failed: 'فشل التحقق من الهوية',
          step2Failed: 'فشل التحقق من رمز التحقق',
          step3Failed: 'فشل إكمال المعلومات',
          step4Failed: 'فشل إعداد الصلاحيات',
          unknown: 'فشل التسجيل، يرجى المحاولة لاحقاً'
        }
      },
      
      // استرداد كلمة المرور
      passwordRecovery: {
        title: 'استرداد كلمة المرور',
        subtitle: 'استرد كلمة المرور من خلال التحقق عبر البريد الإلكتروني',
        step1: 'رمز التحقق',
        step2: 'المستخدم والبريد الإلكتروني',
        step3: 'رمز التحقق عبر البريد الإلكتروني',
        step4: 'إعادة تعيين كلمة المرور',
        userName: 'اسم المستخدم',
        userNamePlaceholder: 'يرجى إدخال اسم المستخدم',
        email: 'عنوان البريد الإلكتروني',
        emailPlaceholder: 'يرجى إدخال عنوان البريد الإلكتروني',
        refreshCaptcha: 'تحديث رمز التحقق',
        nextStep: 'الخطوة التالية',
        emailSent: 'تم إرسال رمز التحقق',
        emailSentDesc: 'تم إرسال رمز التحقق إلى {email}، يرجى التحقق',
        verify: 'تحقق',
        resendCode: 'إعادة الإرسال',
        resetPassword: 'إعادة تعيين كلمة المرور',
        successTitle: 'تم إعادة تعيين كلمة المرور بنجاح',
        successSubtitle: 'تم إعادة تعيين كلمة المرور بنجاح، يرجى تسجيل الدخول بكلمة المرور الجديدة',
        backToLogin: 'العودة لتسجيل الدخول',
        back: 'الخطوة السابقة',
        identityVerified: 'تم التحقق من الهوية بنجاح',
        codeSent: 'تم إرسال رمز التحقق بنجاح',
        emailVerified: 'تم التحقق من البريد الإلكتروني بنجاح',
        passwordResetSuccess: 'تم إعادة تعيين كلمة المرور بنجاح',
        captchaVerified: 'تم التحقق من رمز التحقق بنجاح',
        successMessage: 'تم إعادة تعيين كلمة مرور المستخدم {userName} بنجاح',
        form: {
          emailRequired: 'يرجى إدخال عنوان البريد الإلكتروني',
          userNameLength: 'يجب أن يكون طول اسم المستخدم بين 3-20 حرف'
        },
        error: {
          identityVerificationFailed: 'فشل التحقق من الهوية',
          sendCodeFailed: 'فشل إرسال رمز التحقق',
          emailVerificationFailed: 'فشل التحقق من البريد الإلكتروني',
          passwordResetFailed: 'فشل إعادة تعيين كلمة المرور',
          captchaVerificationFailed: 'فشل التحقق من رمز التحقق',
          emailMismatch: 'اسم المستخدم وعنوان البريد الإلكتروني غير متطابقين',
          invalidCode: 'رمز التحقق غير صحيح أو منتهي الصلاحية',
          codeCooldown: 'إرسال رمز التحقق متكرر جداً، يرجى المحاولة لاحقاً'
        }
      },
      
      // رمز التحقق
      captcha: {
        title: 'التحقق الأمني',
        error: {
          initFailed: 'فشل تهيئة رمز التحقق'
        }
      },
      
      // SMS和OAuth登录功能已移除
      
      // بصمة الجهاز
      device: {
        getDeviceInfo: 'تم الحصول على معلومات بصمة الجهاز',
        deviceFingerprintType: 'نوع بصمة الجهاز',
        deviceFingerprintNull: 'هل بصمة الجهاز null',
        deviceFingerprintUndefined: 'هل بصمة الجهاز undefined',
        deviceFingerprintKeyCount: 'عدد مفاتيح بصمة الجهاز',
        deviceFingerprintKeyList: 'قائمة مفاتيح بصمة الجهاز',
        deviceFingerprintField: 'حقل loginFingerprint في بصمة الجهاز',
        loginParamsDetail: 'تفاصيل معاملات تسجيل الدخول المبنية',
        deviceFingerprintDetail: 'معلومات مفصلة عن بصمة الجهاز',
        empty: 'فارغ',
        backendHandled: 'فارغ (معالج من الخادم)',
        set: 'محدد',
        initSuccess: 'تم تهيئة معلومات الجهاز بنجاح',
        initFailed: 'فشل تهيئة معلومات الجهاز',
        collectionComponent: {
          title: 'بصمة الجهاز',
          description: 'جمع معلومات الجهاز باستخدام Web API الأصلي',
          collecting: 'جاري الجمع...',
          collectDeviceInfo: 'جمع معلومات الجهاز',
          clearInfo: 'مسح المعلومات',
          collectingInfo: 'جاري جمع معلومات الجهاز...',
          deviceInfo: 'معلومات الجهاز:',
          deviceId: 'معرف الجهاز:',
          deviceType: 'نوع الجهاز:',
          deviceFingerprint: 'بصمة الجهاز:',
          platform: 'المنصة:',
          language: 'اللغة:',
          timezone: 'المنطقة الزمنية:',
          screenResolution: 'دقة الشاشة:',
          colorDepth: 'عمق اللون:',
          pixelRatio: 'نسبة بكسل الجهاز:',
          cpuCores: 'عدد أنوية المعالج:',
          deviceMemory: 'ذاكرة الجهاز:',
          touchSupport: 'دعم اللمس:',
          os: 'نظام التشغيل:',
          browser: 'المتصفح:',
          vpnDetection: 'كشف VPN:',
          proxyDetection: 'كشف الوكيل:',
          networkType: 'نوع الشبكة:',
          cookieSupport: 'دعم ملفات تعريف الارتباط:',
          notGenerated: 'غير مولد',
          notCollected: 'غير مجمع',
          notDetected: 'غير مكتشف',
          supported: 'مدعوم',
          notSupported: 'غير مدعوم',
          bits: 'بت',
          copyToClipboard: 'نسخ إلى الحافظة',
          copySuccess: 'تم نسخ معلومات الجهاز إلى الحافظة',
          copyFailed: 'فشل النسخ، يرجى النسخ يدوياً',
          errorInfo: 'معلومات الخطأ',
          startCollection: 'بدء جمع معلومات الجهاز...',
          collectionSuccess: 'تم جمع معلومات الجهاز بنجاح',
          collectionFailed: 'فشل جمع معلومات الجهاز',
          copyError: 'فشل النسخ'
        }
      },
      
      // التحقق
      validation: {
        usernamePasswordRequired: 'اسم المستخدم وكلمة المرور لا يمكن أن يكونا فارغين',
        deviceInfoRequired: 'معلومات بصمة الجهاز لا يمكن أن تكون فارغة',
        deviceFingerprintRequired: 'بصمة الجهاز لا يمكن أن تكون فارغة',
        userAgentRequired: 'وكيل المستخدم لا يمكن أن يكون فارغاً',
        loginTypeSourceRequired: 'نوع تسجيل الدخول والمصدر لا يمكن أن يكونا فارغين'
      },
      
      // تدفق تسجيل الدخول
      loginFlow: {
        paramsSerialized: 'طول المعاملات المسلسلة',
        paramsPreview: 'معاينة المعاملات المسلسلة',
        paramsTooLarge: 'معاملات تسجيل الدخول كبيرة جداً، قد تسبب مشاكل في الأداء',
        serializationFailed: 'فشل تسلسل المعاملات',
        forceOfflineSuccess: 'تم طرد الأجهزة الأخرى، تم تسجيل الدخول بنجاح',
        loginCancelled: 'تم إلغاء تسجيل الدخول',
        singleUserCheckFailed: 'فشل فحص تسجيل دخول المستخدم الواحد، متابعة تدفق تسجيل الدخول العادي',
        loginFailed: 'فشل تسجيل الدخول',
        signalRInit: 'بدء تهيئة اتصال SignalR',
        signalRInitSuccess: 'تم تهيئة اتصال SignalR بنجاح',
        signalRInitFailed: 'فشل تهيئة اتصال SignalR',
        autoLogoutInit: 'بدء تهيئة وظيفة تسجيل الخروج التلقائي',
        autoLogoutInitSuccess: 'تم تهيئة وظيفة تسجيل الخروج التلقائي بنجاح',
        autoLogoutInitFailed: 'فشل تهيئة وظيفة تسجيل الخروج التلقائي',
        pageInitFailed: 'فشل تهيئة صفحة تسجيل الدخول',
        pageInitError: 'فشل تهيئة الصفحة، يرجى التحديث والمحاولة مرة أخرى',
        checkLockStatusFailed: 'فشل فحص حالة قفل الحساب',
        singleUserCheck: {
          title: 'كشف تسجيل دخول المستخدم الواحد',
          content: 'تم اكتشاف أن حسابك مسجل الدخول بالفعل على أجهزة أخرى (عدد الأجهزة المتصلة: {onlineDeviceCount}).\n\n{reason}هل تريد طرد الأجهزة الأخرى ومتابعة تسجيل الدخول؟',
          kickout: 'طرد الأجهزة الأخرى',
          cancel: 'إلغاء تسجيل الدخول'
        }
      },
      
      // التكوين
      config: {
        loading: 'جاري تحميل التكوين...',
        loadingLoginConfig: 'جاري تحميل تكوين تسجيل الدخول، يرجى الانتظار...',
        captchaConfigSuccess: 'تم تحميل تكوين رمز التحقق بنجاح',
        captchaConfigFailed: 'فشل الحصول على تكوين رمز التحقق',
        captchaConfigError: 'فشل الحصول على تكوين رمز التحقق من الخادم',
        loginMethodConfigSuccess: 'تم تحميل تكوين طريقة تسجيل الدخول بنجاح',
        loginMethodConfigError: 'فشل الحصول على تكوين طريقة تسجيل الدخول من الخادم',
        loginMethodConfigFailed: 'فشل الحصول على تكوين طريقة تسجيل الدخول'
      },
      
      // مكون رمز التحقق
      captchaComponent: {
        refreshSuccess: 'تم تحديث رمز التحقق',
        refreshFailed: 'فشل تحديث رمز التحقق',
        getFailed: 'فشل الحصول على رمز التحقق',
        verifySuccess: 'تم التحقق من رمز التحقق بنجاح',
        invalidParams: 'معاملات رمز التحقق غير صحيحة',
        statusUpdated: 'تم تحديث حالة رمز التحقق',
        processError: 'خطأ في معالجة استدعاء نجاح رمز التحقق',
        processFailed: 'فشل معالجة رمز التحقق، يرجى المحاولة مرة أخرى',
        verifyFailed: 'فشل التحقق من رمز التحقق',
        errorProcessFailed: 'فشل معالجة خطأ رمز التحقق',
        initFailed: 'فشل تهيئة رمز التحقق',
        error: 'خطأ رمز التحقق'
      },
      
      // طرق تسجيل الدخول تم إزالتها
      
      // الأخطاء
      error: {
        permanentlyLocked: 'الحساب مقفل نهائياً، يرجى الاتصال بالمدير',
        tooManyAttempts: 'محاولات تسجيل الدخول الفاشلة كثيرة جداً، تم قفل الحساب'
      }
    }
  }
}
