export default {
  identity: {
    auth: {
      // 表单字段定义 - 所有表单的字段都统一验证，都在fields完成
      fields: {
        username: {
          label: '用户名',
          placeholder: '请输入用户名',
          validation: {
            required: '请输入用户名',
            length: '用户名长度应在3-50个字符之间'
          }
        },
        password: {
          label: '密码',
          placeholder: '请输入密码',
          validation: {
            required: '请输入密码',
            length: '密码长度应在6-100个字符之间'
          }
        },
        email: {
          label: '邮箱',
          placeholder: '请输入邮箱',
          validation: {
            required: '请输入邮箱',
            format: '请输入正确的邮箱格式'
          }
        },
        phone: {
          label: '手机号',
          placeholder: '请输入手机号',
          validation: {
            required: '请输入手机号',
            format: '请输入正确的手机号格式'
          }
        },
        captcha: {
          label: '验证码',
          placeholder: '请输入验证码',
          validation: {
            required: '请输入验证码'
          }
        },
        confirmPassword: {
          label: '确认密码',
          placeholder: '请再次输入密码',
          validation: {
            required: '请确认密码',
            mismatch: '两次输入的密码不一致'
          }
        },
        nickName: {
          label: '昵称',
          placeholder: '请输入昵称',
          validation: {
            required: '昵称不能为空',
            format: '2-50位字符，允许中文、日语、韩语、英文、数字、英文句点和连字符，不允许下划线和空格'
          }
        },
        realName: {
          label: '真实姓名',
          placeholder: '请输入真实姓名',
          validation: {
            required: '真实姓名不能为空',
            format: '真实姓名长度必须在2-20位之间，只能包含中文、英文、数字和下划线'
          }
        },
        fullName: {
          label: '全名',
          placeholder: '请输入全名',
          validation: {
            required: '全名不能为空',
            format: '全名长度必须在2-20位之间，只能包含中文、英文、数字和下划线'
          }
        },
        englishName: {
          label: '英文名',
          placeholder: '请输入英文名',
          validation: {
            required: '英文名不能为空',
            format: '英文名长度必须在2-100位之间，必须以字母开头，只能包含英文字母、空格、连字符和英文句点'
          }
        },
        verificationCode: {
          label: '验证码',
          placeholder: '请输入6位验证码',
          validation: {
            required: '请输入验证码',
            length: '验证码应为6位数字',
            format: '验证码应为6位数字'
          }
        },
        newPassword: {
          label: '新密码',
          placeholder: '请输入新密码',
          validation: {
            required: '请输入新密码',
            length: '密码长度应在6-20个字符之间',
            format: '密码必须包含大小写字母和数字'
          }
        },
        gender: {
          label: '性别',
          placeholder: '请选择性别',
          validation: {
            required: '请选择性别'
          },
          options: {
            unknown: '保密',
            male: '男',
            female: '女'
          }
        },
        userType: {
          label: '用户类型',
          placeholder: '请选择用户类型',
          validation: {
            required: '请选择用户类型'
          },
          options: {
            normal: '普通用户',
            admin: '管理员'
          }
        },
        status: {
          label: '状态',
          placeholder: '请选择状态',
          validation: {
            required: '请选择状态'
          },
          options: {
            normal: '正常',
            disabled: '禁用'
          }
        },
        deptId: {
          label: '部门',
          placeholder: '请输入部门ID',
          validation: {
            required: '请输入部门ID'
          }
        },
        roleIds: {
          label: '角色',
          placeholder: '请选择角色',
          validation: {
            required: '请选择角色'
          }
        },
        postIds: {
          label: '岗位',
          placeholder: '请选择岗位',
          validation: {
            required: '请选择岗位'
          }
        },
        deptIds: {
          label: '所属部门',
          placeholder: '请选择所属部门',
          validation: {
            required: '请选择所属部门'
          }
        },
        remark: {
          label: '备注',
          placeholder: '请输入备注'
        }
      },
      
      // 登录相关
      login: {
        title: '登录',
        rememberMe: '记住密码',
        forgotPassword: '忘记密码',
        submit: '登录',
        success: '登录成功',
        noAccount: '还没有账号？',
        registerNow: '立即注册',
        notAvailable: '功能暂不可用',
        error: {
          invalidCredentials: '用户名或密码错误',
          userDisabled: '用户已被禁用',
          userNotFound: '用户不存在',
          serverError: '服务器错误，请稍后重试',
          unknown: '登录失败，请稍后重试'
        }
      },
      
      // 用户注册
      register: {
        title: '用户注册',
        subtitle: '请按照步骤完成用户注册',
        step1: '身份验证',
        step2: '基本信息',
        step3: '详细信息',
        step4: '权限设置',
        roleUser: '用户',
        roleAdmin: '管理员',
        postEmployee: '员工',
        postManager: '经理',
        deptIT: '技术部',
        deptHR: '人事部',
        agreement: '我已阅读并同意',
        agreementPrefix: '我已阅读并同意',
        agreementLink: '《用户协议》',
        agreementSuffix: '',
        agreementTitle: '用户注册协议',
        agreementContent: '请仔细阅读并同意本协议后再注册。',
        submit: '完成注册',
        nextStep: '下一步',
        back: '上一步',
        backToLogin: '返回登录',
        login: '使用已有账号登录',
        confirmPassword: '确认密码',
        confirmPasswordPlaceholder: '请再次输入密码',
        deptId: '部门ID',
        deptIdPlaceholder: '请输入部门ID',
        roleIds: '角色',
        roleIdsPlaceholder: '请选择角色',
        postIds: '岗位',
        postIdsPlaceholder: '请选择岗位',
        deptIds: '部门',
        deptIdsPlaceholder: '请选择部门',
        remark: '备注',
        remarkPlaceholder: '请输入备注信息（可选）',
        success: '注册成功',
        successTitle: '注册成功',
        successSubtitle: '您的账号已成功创建，请使用新账号登录',
        successMessage: '用户 {userName} 已成功注册',
        step1Success: '身份验证通过',
        step2Success: '验证码验证通过',
        step3Success: '信息补全完成',
        step4Success: '权限设置完成',
        form: {
          agreementRequired: '请阅读并同意用户协议',
          captchaRequired: '请输入验证码',
          usernameRequired: '请输入用户名',
          usernameLength: '用户名长度应在3-20个字符之间',
          usernameFormat: '用户名只能包含字母、数字和下划线',
          emailRequired: '请输入邮箱地址',
          emailFormat: '请输入正确的邮箱格式',
          passwordRequired: '请输入密码',
          passwordLength: '密码长度应在6-20个字符之间',
          passwordFormat: '密码必须包含大小写字母和数字',
          confirmPasswordRequired: '请确认密码',
          passwordMismatch: '两次输入的密码不一致',
          nickNameRequired: '请输入昵称',
          nickNameLength: '昵称长度应在2-20个字符之间',
          realNameRequired: '请输入真实姓名',
          realNameLength: '真实姓名长度应在2-20个字符之间',
          fullNameRequired: '请输入全名',
          fullNameLength: '全名长度应在2-50个字符之间',
          englishNameLength: '英文名长度应在2-50个字符之间',
          englishNameFormat: '英文名只能包含字母和空格',
          phoneNumberFormat: '请输入正确的手机号格式',
          userTypeRequired: '请选择用户类型',
          statusRequired: '请选择状态',
          deptIdRequired: '请输入部门ID',
          roleIdsRequired: '请选择角色',
          postIdsRequired: '请选择岗位',
          deptIdsRequired: '请选择部门'
        },
        error: {
          step1Failed: '身份验证失败',
          step2Failed: '验证码验证失败',
          step3Failed: '信息补全失败',
          step4Failed: '权限设置失败',
          unknown: '注册失败，请稍后重试'
        }
      },
      
      // 找回密码
      passwordRecovery: {
        title: '找回密码',
        subtitle: '通过邮箱验证找回您的密码',
        step1: '验证码',
        step2: '用户和邮件',
        step3: '邮件验证码',
        step4: '重置密码',
        userName: '用户名',
        userNamePlaceholder: '请输入您的用户名',
        email: '邮箱地址',
        emailPlaceholder: '请输入您的邮箱地址',
        refreshCaptcha: '刷新验证码',
        nextStep: '下一步',
        emailSent: '验证码已发送',
        emailSentDesc: '验证码已发送到您的邮箱 {email}，请注意查收',
        verify: '验证',
        resendCode: '重新发送',
        resetPassword: '重置密码',
        successTitle: '密码重置成功',
        successSubtitle: '您的密码已成功重置，请使用新密码登录',
        backToLogin: '返回登录',
        back: '上一步',
        identityVerified: '身份验证成功',
        codeSent: '验证码发送成功',
        emailVerified: '邮箱验证成功',
        passwordResetSuccess: '密码重置成功',
        captchaVerified: '验证码验证成功',
        successMessage: '用户 {userName} 的密码已成功重置',
        form: {
          emailRequired: '请输入邮箱地址',
          userNameLength: '用户名长度应在3-20个字符之间'
        },
        error: {
          identityVerificationFailed: '身份验证失败',
          sendCodeFailed: '验证码发送失败',
          emailVerificationFailed: '邮箱验证失败',
          passwordResetFailed: '密码重置失败',
          captchaVerificationFailed: '验证码验证失败',
          emailMismatch: '用户名与邮箱地址不匹配',
          invalidCode: '验证码错误或已过期',
          codeCooldown: '验证码发送过于频繁，请稍后再试'
        }
      },
      
      // 验证码相关
      captcha: {
        title: '安全验证',
        error: {
          initFailed: '验证码初始化失败'
        },
        behavior: {
          default: '请按住滑块，拖动到最右边',
          success: '验证通过',
          failed: '验证失败，请重试',
          verifyError: '验证过程出错，请重试'
        },
        slider: {
          bgImage: '验证码背景图片',
          sliderImage: '验证码滑块图片',
          default: '请按住滑块，拖动完成上方拼图',
          success: '验证通过',
          failed: '验证失败，请重试',
          verifyError: '验证过程出错，请重试',
          maxRetryReached: '验证失败次数过多，请刷新页面重试',
          error: {
            invalidResponse: '验证码响应无效',
            loadFailed: '加载验证码失败'
          }
        }
      },
      
      // SMS和OAuth登录功能已移除
      
      // 设备指纹
      device: {
        getDeviceInfo: '获取到的设备指纹信息',
        deviceFingerprintType: '设备指纹类型',
        deviceFingerprintNull: '设备指纹是否为null',
        deviceFingerprintUndefined: '设备指纹是否为undefined',
        deviceFingerprintKeyCount: '设备指纹键数量',
        deviceFingerprintKeyList: '设备指纹键列表',
        deviceFingerprintField: '设备指纹loginFingerprint字段',
        loginParamsDetail: '构造的登录参数详情',
        deviceFingerprintDetail: '设备指纹详细信息',
        empty: '空',
        backendHandled: '空（后端处理）',
        set: '已设置',
        initSuccess: '设备信息初始化成功',
        initFailed: '初始化设备信息失败',
        collectionComponent: {
          title: '设备信息收集组件',
          description: '使用原生 Web API 收集设备信息',
          collecting: '收集中...',
          collectDeviceInfo: '收集设备信息',
          clearInfo: '清除信息',
          collectingInfo: '正在收集设备信息...',
          deviceInfo: '设备信息：',
          deviceId: '设备ID：',
          deviceType: '设备类型：',
          deviceFingerprint: '设备指纹：',
          platform: '平台：',
          language: '语言：',
          timezone: '时区：',
          screenResolution: '屏幕分辨率：',
          colorDepth: '颜色深度：',
          pixelRatio: '设备像素比：',
          cpuCores: 'CPU 核心数：',
          deviceMemory: '设备内存：',
          touchSupport: '触摸支持：',
          os: '操作系统：',
          browser: '浏览器：',
          vpnDetection: 'VPN 检测：',
          proxyDetection: '代理检测：',
          networkType: '网络类型：',
          cookieSupport: 'Cookie 支持：',
          notGenerated: '未生成',
          notCollected: '未收集',
          notDetected: '未检测',
          supported: '支持',
          notSupported: '不支持',
          bits: '位',
          copyToClipboard: '复制到剪贴板',
          copySuccess: '设备信息已复制到剪贴板',
          copyFailed: '复制失败，请手动复制',
          errorInfo: '错误信息',
          startCollection: '开始收集设备信息... ',
          collectionSuccess: '设备信息收集成功',
          collectionFailed: '收集设备信息失败 ',
          copyError: '复制失败'
        }
      },
      
      // 验证相关
      validation: {
        usernamePasswordRequired: '用户名和密码不能为空',
        deviceInfoRequired: '设备指纹信息不能为空',
        deviceFingerprintRequired: '设备指纹不能为空',
        userAgentRequired: '用户代理不能为空',
        loginTypeSourceRequired: '登录类型和来源不能为空'
      },
      
      // 登录流程
      loginFlow: {
        paramsSerialized: '序列化后的参数长度',
        paramsPreview: '序列化后的参数预览',
        paramsTooLarge: '登录参数过大，可能导致性能问题',
        serializationFailed: '参数序列化失败',
        forceOfflineSuccess: '其他设备已踢出，登录成功',
        loginCancelled: '登录已取消',
        singleUserCheckFailed: '单一用户登录检查失败，继续正常登录流程',
        loginFailed: '登录失败',
        signalRInit: '开始初始化 SignalR 连接',
        signalRInitSuccess: 'SignalR 连接初始化成功',
        signalRInitFailed: 'SignalR 连接初始化失败',
        autoLogoutInit: '开始初始化自动登出功能',
        autoLogoutInitSuccess: '自动登出功能初始化成功',
        autoLogoutInitFailed: '自动登出功能初始化失败',
        pageInitFailed: '登录页面初始化失败',
        pageInitError: '页面初始化失败，请刷新重试',
        checkLockStatusFailed: '检查账号锁定状态失败',
        singleUserCheck: {
          title: '单一用户登录检测',
          content: '检测到您的账号已在其他设备登录（在线设备数：{onlineDeviceCount}）。\n\n{reason}是否要踢出其他设备并继续登录？',
          kickout: '踢出其他设备',
          cancel: '取消登录'
        }
      },
      
      // 配置相关
      config: {
        loading: '正在加载配置...',
        loadingLoginConfig: '正在加载登录配置，请稍候...',
        captchaConfigSuccess: '验证码配置加载成功',
        captchaConfigFailed: '获取验证码配置失败',
        captchaConfigError: '获取后端验证码配置失败',
        loginMethodConfigSuccess: '登录方式配置加载成功',
        loginMethodConfigError: '获取后端登录方式配置失败',
        loginMethodConfigFailed: '获取登录方式配置失败'
      },
      
      // 验证码组件
      captchaComponent: {
        refreshSuccess: '验证码已刷新',
        refreshFailed: '刷新验证码失败',
        getFailed: '获取验证码失败',
        verifySuccess: '验证码验证通过',
        invalidParams: '验证码参数无效',
        statusUpdated: '验证码状态已更新',
        processError: '处理验证码成功回调时出错',
        processFailed: '验证码处理失败，请重试',
        verifyFailed: '验证码验证失败',
        errorProcessFailed: '验证码错误处理失败',
        initFailed: '验证码初始化失败',
        error: '验证码错误'
      },
      
      // 登录方式已移除
      
      // 错误相关
      error: {
        permanentlyLocked: '账号已被永久锁定，请联系管理员',
        tooManyAttempts: '登录失败次数过多，账号已被锁定'
      }
    }
  }
}