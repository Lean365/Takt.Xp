export default {
  identity: {
    auth: {
      // 表單欄位定義 - 所有表單的欄位都統一驗證，都在fields完成
      fields: {
        username: {
          label: '使用者名稱',
          placeholder: '請輸入使用者名稱',
          validation: {
            required: '請輸入使用者名稱',
            length: '使用者名稱長度應在3-50個字元之間'
          }
        },
        password: {
          label: '密碼',
          placeholder: '請輸入密碼',
          validation: {
            required: '請輸入密碼',
            length: '密碼長度應在6-100個字元之間'
          }
        },
        email: {
          label: '電子郵件',
          placeholder: '請輸入電子郵件',
          validation: {
            required: '請輸入電子郵件',
            format: '請輸入正確的電子郵件格式'
          }
        },
        phone: {
          label: '手機號碼',
          placeholder: '請輸入手機號碼',
          validation: {
            required: '請輸入手機號碼',
            format: '請輸入正確的手機號碼格式'
          }
        },
        captcha: {
          label: '驗證碼',
          placeholder: '請輸入驗證碼',
          validation: {
            required: '請輸入驗證碼'
          }
        },
        confirmPassword: {
          label: '確認密碼',
          placeholder: '請再次輸入密碼',
          validation: {
            required: '請確認密碼',
            mismatch: '兩次輸入的密碼不一致'
          }
        },
        nickName: {
          label: '暱稱',
          placeholder: '請輸入暱稱',
          validation: {
            required: '暱稱不能為空',
            format: '2-50位字元，允許中文、日語、韓語、英文、數字、英文句點和連字號，不允許底線和空格'
          }
        },
        realName: {
          label: '真實姓名',
          placeholder: '請輸入真實姓名',
          validation: {
            required: '真實姓名不能為空',
            format: '真實姓名長度必須在2-20位之間，只能包含中文、英文、數字和底線'
          }
        },
        fullName: {
          label: '全名',
          placeholder: '請輸入全名',
          validation: {
            required: '全名不能為空',
            format: '全名長度必須在2-20位之間，只能包含中文、英文、數字和底線'
          }
        },
        englishName: {
          label: '英文名',
          placeholder: '請輸入英文名',
          validation: {
            required: '英文名不能為空',
            format: '英文名長度必須在2-100位之間，必須以字母開頭，只能包含英文字母、空格、連字號和英文句點'
          }
        },
        verificationCode: {
          label: '驗證碼',
          placeholder: '請輸入6位驗證碼',
          validation: {
            required: '請輸入驗證碼',
            length: '驗證碼應為6位數字',
            format: '驗證碼應為6位數字'
          }
        },
        newPassword: {
          label: '新密碼',
          placeholder: '請輸入新密碼',
          validation: {
            required: '請輸入新密碼',
            length: '密碼長度應在6-20個字元之間',
            format: '密碼必須包含大小寫字母和數字'
          }
        },
        gender: {
          label: '性別',
          placeholder: '請選擇性別',
          validation: {
            required: '請選擇性別'
          },
          options: {
            unknown: '保密',
            male: '男',
            female: '女'
          }
        },
        userType: {
          label: '使用者類型',
          placeholder: '請選擇使用者類型',
          validation: {
            required: '請選擇使用者類型'
          },
          options: {
            normal: '一般使用者',
            admin: '管理員'
          }
        },
        status: {
          label: '狀態',
          placeholder: '請選擇狀態',
          validation: {
            required: '請選擇狀態'
          },
          options: {
            normal: '正常',
            disabled: '停用'
          }
        },
        deptId: {
          label: '部門',
          placeholder: '請輸入部門ID',
          validation: {
            required: '請輸入部門ID'
          }
        },
        roleIds: {
          label: '角色',
          placeholder: '請選擇角色',
          validation: {
            required: '請選擇角色'
          }
        },
        postIds: {
          label: '職位',
          placeholder: '請選擇職位',
          validation: {
            required: '請選擇職位'
          }
        },
        deptIds: {
          label: '所屬部門',
          placeholder: '請選擇所屬部門',
          validation: {
            required: '請選擇所屬部門'
          }
        },
        remark: {
          label: '備註',
          placeholder: '請輸入備註'
        }
      },
      
      // 登入相關
      login: {
        title: '登入',
        rememberMe: '記住密碼',
        forgotPassword: '忘記密碼',
        submit: '登入',
        success: '登入成功',
        noAccount: '還沒有帳號？',
        registerNow: '立即註冊',
        notAvailable: '功能暫不可用',
        error: {
          invalidCredentials: '使用者名稱或密碼錯誤',
          userDisabled: '使用者已被停用',
          userNotFound: '使用者不存在',
          serverError: '伺服器錯誤，請稍後重試',
          unknown: '登入失敗，請稍後重試'
        }
      },
      
      // 使用者註冊
      register: {
        title: '使用者註冊',
        subtitle: '請按照步驟完成使用者註冊',
        step1: '身分驗證',
        step2: '基本資訊',
        step3: '詳細資訊',
        step4: '權限設定',
        roleUser: '使用者',
        roleAdmin: '管理員',
        postEmployee: '員工',
        postManager: '經理',
        deptIT: '技術部',
        deptHR: '人事部',
        agreement: '我已閱讀並同意',
        agreementPrefix: '我已閱讀並同意',
        agreementLink: '《使用者協議》',
        agreementSuffix: '',
        agreementTitle: '使用者註冊協議',
        agreementContent: '請仔細閱讀並同意本協議後再註冊。',
        submit: '完成註冊',
        nextStep: '下一步',
        back: '上一步',
        backToLogin: '返回登入',
        login: '使用已有帳號登入',
        confirmPassword: '確認密碼',
        confirmPasswordPlaceholder: '請再次輸入密碼',
        deptId: '部門ID',
        deptIdPlaceholder: '請輸入部門ID',
        roleIds: '角色',
        roleIdsPlaceholder: '請選擇角色',
        postIds: '崗位',
        postIdsPlaceholder: '請選擇崗位',
        deptIds: '部門',
        deptIdsPlaceholder: '請選擇部門',
        remark: '備註',
        remarkPlaceholder: '請輸入備註資訊（可選）',
        success: '註冊成功',
        successTitle: '註冊成功',
        successSubtitle: '您的帳號已成功建立，請使用新帳號登入',
        successMessage: '使用者 {userName} 已成功註冊',
        step1Success: '身分驗證通過',
        step2Success: '驗證碼驗證通過',
        step3Success: '資訊補全完成',
        step4Success: '權限設定完成',
        form: {
          agreementRequired: '請閱讀並同意使用者協議',
          captchaRequired: '請輸入驗證碼',
          usernameRequired: '請輸入使用者名稱',
          usernameLength: '使用者名稱長度應在3-20個字元之間',
          usernameFormat: '使用者名稱只能包含字母、數字和底線',
          emailRequired: '請輸入電子郵件地址',
          emailFormat: '請輸入正確的電子郵件格式',
          passwordRequired: '請輸入密碼',
          passwordLength: '密碼長度應在6-20個字元之間',
          passwordFormat: '密碼必須包含大小寫字母和數字',
          confirmPasswordRequired: '請確認密碼',
          passwordMismatch: '兩次輸入的密碼不一致',
          nickNameRequired: '請輸入暱稱',
          nickNameLength: '暱稱長度應在2-20個字元之間',
          realNameRequired: '請輸入真實姓名',
          realNameLength: '真實姓名長度應在2-20個字元之間',
          fullNameRequired: '請輸入全名',
          fullNameLength: '全名長度應在2-50個字元之間',
          englishNameLength: '英文名長度應在2-50個字元之間',
          englishNameFormat: '英文名只能包含字母和空格',
          phoneNumberFormat: '請輸入正確的手機號格式',
          userTypeRequired: '請選擇使用者類型',
          statusRequired: '請選擇狀態',
          deptIdRequired: '請輸入部門ID',
          roleIdsRequired: '請選擇角色',
          postIdsRequired: '請選擇崗位',
          deptIdsRequired: '請選擇部門'
        },
        error: {
          step1Failed: '身分驗證失敗',
          step2Failed: '驗證碼驗證失敗',
          step3Failed: '資訊補全失敗',
          step4Failed: '權限設定失敗',
          unknown: '註冊失敗，請稍後重試'
        }
      },
      
      // 找回密碼
      passwordRecovery: {
        title: '找回密碼',
        subtitle: '透過電子郵件驗證找回您的密碼',
        step1: '驗證碼',
        step2: '使用者和郵件',
        step3: '郵件驗證碼',
        step4: '重設密碼',
        userName: '使用者名稱',
        userNamePlaceholder: '請輸入您的使用者名稱',
        email: '電子郵件地址',
        emailPlaceholder: '請輸入您的電子郵件地址',
        refreshCaptcha: '重新整理驗證碼',
        nextStep: '下一步',
        emailSent: '驗證碼已傳送',
        emailSentDesc: '驗證碼已傳送到您的電子郵件 {email}，請注意查收',
        verify: '驗證',
        resendCode: '重新傳送',
        resetPassword: '重設密碼',
        successTitle: '密碼重設成功',
        successSubtitle: '您的密碼已成功重設，請使用新密碼登入',
        backToLogin: '返回登入',
        back: '上一步',
        identityVerified: '身分驗證成功',
        codeSent: '驗證碼傳送成功',
        emailVerified: '電子郵件驗證成功',
        passwordResetSuccess: '密碼重設成功',
        captchaVerified: '驗證碼驗證成功',
        successMessage: '使用者 {userName} 的密碼已成功重設',
        form: {
          emailRequired: '請輸入電子郵件地址',
          userNameLength: '使用者名稱長度應在3-20個字元之間'
        },
        error: {
          identityVerificationFailed: '身分驗證失敗',
          sendCodeFailed: '驗證碼傳送失敗',
          emailVerificationFailed: '電子郵件驗證失敗',
          passwordResetFailed: '密碼重設失敗',
          captchaVerificationFailed: '驗證碼驗證失敗',
          emailMismatch: '使用者名稱與電子郵件地址不符',
          invalidCode: '驗證碼錯誤或已過期',
          codeCooldown: '驗證碼傳送過於頻繁，請稍後再試'
        }
      },
      
      // 驗證碼相關
      captcha: {
        title: '安全驗證',
        error: {
          initFailed: '驗證碼初始化失敗'
        },
        behavior: {
          default: '請按住滑塊，拖動到最右邊',
          success: '驗證通過',
          failed: '驗證失敗，請重試',
          verifyError: '驗證過程出錯，請重試'
        },
        slider: {
          bgImage: '驗證碼背景圖片',
          sliderImage: '驗證碼滑塊圖片',
          default: '請按住滑塊，拖動完成上方拼圖',
          success: '驗證通過',
          failed: '驗證失敗，請重試',
          verifyError: '驗證過程出錯，請重試',
          maxRetryReached: '驗證失敗次數過多，請重新整理頁面重試',
          error: {
            invalidResponse: '驗證碼響應無效',
            loadFailed: '載入驗證碼失敗'
          }
        }
      },
      
      // 簡訊登入
      smsLogin: {
        title: '簡訊登入',
        subtitle: '使用手機驗證碼快速登入',
        sendCode: '傳送驗證碼',
        login: '登入',
        codeSent: '驗證碼傳送成功',
        sendCodeFailed: '驗證碼傳送失敗',
        loginSuccess: '登入成功',
        loginFailed: '登入失敗',
        or: '或',
        switchToPasswordLogin: '密碼登入',
        register: '註冊帳號',
        forgotPassword: '忘記密碼'
      },
      
      // 第三方登入
      thirdPartyLogin: {
        title: '第三方登入',
        subtitle: '使用第三方帳號快速登入',
        alipay: '支付寶',
        amazon: 'Amazon',
        apple: 'Apple',
        dingtalk: '釘釘',
        facebook: 'Facebook',
        github: 'GitHub',
        google: 'Google',
        linkedin: 'LinkedIn',
        microsoft: 'Microsoft',
        qq: 'QQ',
        twitter: 'Twitter',
        wechat: '微信',
        weibo: '微博',
        alipayFailed: '支付寶登入失敗',
        amazonFailed: 'Amazon登入失敗',
        appleFailed: 'Apple登入失敗',
        dingtalkFailed: '釘釘登入失敗',
        facebookFailed: 'Facebook登入失敗',
        githubFailed: 'GitHub登入失敗',
        googleFailed: 'Google登入失敗',
        linkedinFailed: 'LinkedIn登入失敗',
        microsoftFailed: 'Microsoft登入失敗',
        qqFailed: 'QQ登入失敗',
        twitterFailed: 'Twitter登入失敗',
        wechatFailed: '微信登入失敗',
        weiboFailed: '微博登入失敗',
        noProviders: '暫無可用的第三方登入方式',
        unsupportedProvider: '不支援的登入方式',
        or: '或',
        switchToPasswordLogin: '密碼登入',
        switchToSmsLogin: '簡訊登入',
        register: '註冊帳號',
        forgotPassword: '忘記密碼'
      },
      
      // 裝置指紋
      device: {
        getDeviceInfo: '取得的裝置指紋資訊',
        deviceFingerprintType: '裝置指紋類型',
        deviceFingerprintNull: '裝置指紋是否為null',
        deviceFingerprintUndefined: '裝置指紋是否為undefined',
        deviceFingerprintKeyCount: '裝置指紋鍵數量',
        deviceFingerprintKeyList: '裝置指紋鍵列表',
        deviceFingerprintField: '裝置指紋loginFingerprint欄位',
        loginParamsDetail: '建構的登入參數詳情',
        deviceFingerprintDetail: '裝置指紋詳細資訊',
        empty: '空',
        backendHandled: '空（後端處理）',
        set: '已設定',
        initSuccess: '裝置資訊初始化成功',
        initFailed: '初始化裝置資訊失敗',
        collectionComponent: {
          title: '裝置資訊收集元件',
          description: '使用原生 Web API 收集裝置資訊',
          collecting: '收集中...',
          collectDeviceInfo: '收集裝置資訊',
          clearInfo: '清除資訊',
          collectingInfo: '正在收集裝置資訊...',
          deviceInfo: '裝置資訊：',
          deviceId: '裝置ID：',
          deviceType: '裝置類型：',
          deviceFingerprint: '裝置指紋：',
          platform: '平台：',
          language: '語言：',
          timezone: '時區：',
          screenResolution: '螢幕解析度：',
          colorDepth: '顏色深度：',
          pixelRatio: '裝置像素比：',
          cpuCores: 'CPU 核心數：',
          deviceMemory: '裝置記憶體：',
          touchSupport: '觸控支援：',
          os: '作業系統：',
          browser: '瀏覽器：',
          vpnDetection: 'VPN 偵測：',
          proxyDetection: '代理偵測：',
          networkType: '網路類型：',
          cookieSupport: 'Cookie 支援：',
          notGenerated: '未產生',
          notCollected: '未收集',
          notDetected: '未偵測',
          supported: '支援',
          notSupported: '不支援',
          bits: '位元',
          copyToClipboard: '複製到剪貼簿',
          copySuccess: '裝置資訊已複製到剪貼簿',
          copyFailed: '複製失敗，請手動複製',
          errorInfo: '錯誤資訊',
          startCollection: '開始收集裝置資訊...',
          collectionSuccess: '裝置資訊收集成功',
          collectionFailed: '收集裝置資訊失敗',
          copyError: '複製失敗'
        }
      },
      
      // 驗證相關
      validation: {
        usernamePasswordRequired: '使用者名稱和密碼不能為空',
        deviceInfoRequired: '裝置指紋資訊不能為空',
        deviceFingerprintRequired: '裝置指紋不能為空',
        userAgentRequired: '使用者代理不能為空',
        loginTypeSourceRequired: '登入類型和來源不能為空'
      },
      
      // 登入流程
      loginFlow: {
        paramsSerialized: '序列化後的參數長度',
        paramsPreview: '序列化後的參數預覽',
        paramsTooLarge: '登入參數過大，可能導致效能問題',
        serializationFailed: '參數序列化失敗',
        forceOfflineSuccess: '其他裝置已踢出，登入成功',
        loginCancelled: '登入已取消',
        singleUserCheckFailed: '單一使用者登入檢查失敗，繼續正常登入流程',
        loginFailed: '登入失敗',
        signalRInit: '開始初始化 SignalR 連線',
        signalRInitSuccess: 'SignalR 連線初始化成功',
        signalRInitFailed: 'SignalR 連線初始化失敗',
        autoLogoutInit: '開始初始化自動登出功能',
        autoLogoutInitSuccess: '自動登出功能初始化成功',
        autoLogoutInitFailed: '自動登出功能初始化失敗',
        pageInitFailed: '登入頁面初始化失敗',
        pageInitError: '頁面初始化失敗，請重新整理重試',
        checkLockStatusFailed: '檢查帳號鎖定狀態失敗',
        singleUserCheck: {
          title: '單一使用者登入偵測',
          content: '偵測到您的帳號已在其他裝置登入（線上裝置數：{onlineDeviceCount}）。\n\n{reason}是否要踢出其他裝置並繼續登入？',
          kickout: '踢出其他裝置',
          cancel: '取消登入'
        }
      },
      
      // 設定相關
      config: {
        loading: '正在載入設定...',
        loadingLoginConfig: '正在載入登入設定，請稍候...',
        captchaConfigSuccess: '驗證碼設定載入成功',
        captchaConfigFailed: '取得驗證碼設定失敗',
        captchaConfigError: '取得後端驗證碼設定失敗',
        loginMethodConfigSuccess: '登入方式設定載入成功',
        loginMethodConfigError: '取得後端登入方式設定失敗',
        loginMethodConfigFailed: '取得登入方式設定失敗'
      },
      
      // 驗證碼元件
      captchaComponent: {
        refreshSuccess: '驗證碼已重新整理',
        refreshFailed: '重新整理驗證碼失敗',
        getFailed: '取得驗證碼失敗',
        verifySuccess: '驗證碼驗證通過',
        invalidParams: '驗證碼參數無效',
        statusUpdated: '驗證碼狀態已更新',
        processError: '處理驗證碼成功回調時出錯',
        processFailed: '驗證碼處理失敗，請重試',
        verifyFailed: '驗證碼驗證失敗',
        errorProcessFailed: '驗證碼錯誤處理失敗',
        initFailed: '驗證碼初始化失敗',
        error: '驗證碼錯誤'
      },
      
      // 登入方式已移除
      
      // 錯誤相關
      error: {
        permanentlyLocked: '帳號已被永久鎖定，請聯絡管理員',
        tooManyAttempts: '登入失敗次數過多，帳號已被鎖定'
      }
    }
  }
}
