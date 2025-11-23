export default {
  identity: {
    auth: {
      // フォームフィールド定義 - すべてのフォームのフィールドはfieldsで統一検証される
      fields: {
        username: {
          label: 'ユーザー名',
          placeholder: 'ユーザー名を入力してください',
          validation: {
            required: 'ユーザー名を入力してください',
            length: 'ユーザー名の長さは3-50文字の間である必要があります'
          }
        },
        password: {
          label: 'パスワード',
          placeholder: 'パスワードを入力してください',
          validation: {
            required: 'パスワードを入力してください',
            length: 'パスワードの長さは6-100文字の間である必要があります'
          }
        },
        email: {
          label: 'メールアドレス',
          placeholder: 'メールアドレスを入力してください',
          validation: {
            required: 'メールアドレスを入力してください',
            format: '正しいメールアドレス形式を入力してください'
          }
        },
        phone: {
          label: '携帯電話番号',
          placeholder: '携帯電話番号を入力してください',
          validation: {
            required: '携帯電話番号を入力してください',
            format: '正しい携帯電話番号形式を入力してください'
          }
        },
        captcha: {
          label: '認証コード',
          placeholder: '認証コードを入力してください',
          validation: {
            required: '認証コードを入力してください'
          }
        },
        confirmPassword: {
          label: 'パスワード確認',
          placeholder: 'パスワードを再度入力してください',
          validation: {
            required: 'パスワードを確認してください',
            mismatch: '入力した2つのパスワードが一致しません'
          }
        },
        nickName: {
          label: 'ニックネーム',
          placeholder: 'ニックネームを入力してください',
          validation: {
            required: 'ニックネームは空にできません',
            format: '2-50文字、中国語、日本語、韓国語、英語、数字、ピリオドとハイフンを許可、アンダースコアとスペースは禁止'
          }
        },
        realName: {
          label: '実名',
          placeholder: '実名を入力してください',
          validation: {
            required: '実名は空にできません',
            format: '実名の長さは2-20文字の間である必要があり、中国語、英語、数字とアンダースコアのみ含む'
          }
        },
        fullName: {
          label: 'フルネーム',
          placeholder: 'フルネームを入力してください',
          validation: {
            required: 'フルネームは空にできません',
            format: 'フルネームの長さは2-20文字の間である必要があり、中国語、英語、数字とアンダースコアのみ含む'
          }
        },
        englishName: {
          label: '英語名',
          placeholder: '英語名を入力してください',
          validation: {
            required: '英語名は空にできません',
            format: '英語名の長さは2-100文字の間である必要があり、文字で始まる必要があり、英字、スペース、ハイフンとピリオドのみ含む'
          }
        },
        verificationCode: {
          label: '認証コード',
          placeholder: '6桁の認証コードを入力してください',
          validation: {
            required: '認証コードを入力してください',
            length: '認証コードは6桁の数字である必要があります',
            format: '認証コードは6桁の数字である必要があります'
          }
        },
        newPassword: {
          label: '新しいパスワード',
          placeholder: '新しいパスワードを入力してください',
          validation: {
            required: '新しいパスワードを入力してください',
            length: 'パスワードの長さは6-20文字の間である必要があります',
            format: 'パスワードは大文字と小文字と数字を含む必要があります'
          }
        },
        gender: {
          label: '性別',
          placeholder: '性別を選択してください',
          validation: {
            required: '性別を選択してください'
          },
          options: {
            unknown: '非公開',
            male: '男性',
            female: '女性'
          }
        },
        userType: {
          label: 'ユーザータイプ',
          placeholder: 'ユーザータイプを選択してください',
          validation: {
            required: 'ユーザータイプを選択してください'
          },
          options: {
            normal: '一般ユーザー',
            admin: '管理者'
          }
        },
        status: {
          label: 'ステータス',
          placeholder: 'ステータスを選択してください',
          validation: {
            required: 'ステータスを選択してください'
          },
          options: {
            normal: '正常',
            disabled: '無効'
          }
        },
        deptId: {
          label: '部門',
          placeholder: '部門IDを入力してください',
          validation: {
            required: '部門IDを入力してください'
          }
        },
        roleIds: {
          label: 'ロール',
          placeholder: 'ロールを選択してください',
          validation: {
            required: 'ロールを選択してください'
          }
        },
        postIds: {
          label: '職位',
          placeholder: '職位を選択してください',
          validation: {
            required: '職位を選択してください'
          }
        },
        deptIds: {
          label: '所属部門',
          placeholder: '所属部門を選択してください',
          validation: {
            required: '所属部門を選択してください'
          }
        },
        remark: {
          label: '備考',
          placeholder: '備考を入力してください'
        }
      },
      
      // ログイン関連
      login: {
        title: 'ログイン',
        rememberMe: '記憶',
        forgotPassword: '忘れた？',
        submit: 'ログイン',
        success: 'ログイン成功',
        noAccount: 'アカウントをお持ちでないですか？',
        registerNow: '今すぐ登録',
        notAvailable: '機能は一時的に利用できません',
        error: {
          invalidCredentials: 'ユーザー名またはパスワードが間違っています',
          userDisabled: 'ユーザーが無効化されています',
          userNotFound: 'ユーザーが存在しません',
          serverError: 'サーバーエラー、後でもう一度お試しください',
          unknown: 'ログインに失敗しました、後でもう一度お試しください'
        }
      },
      
      // ユーザー登録
      register: {
        title: 'ユーザー登録',
        subtitle: 'ステップバイステップでユーザー登録を完了してください',
        step1: '身元認証',
        step2: '基本情報',
        step3: '詳細情報',
        step4: '権限設定',
        roleUser: 'ユーザー',
        roleAdmin: '管理者',
        postEmployee: '従業員',
        postManager: 'マネージャー',
        deptIT: 'IT部門',
        deptHR: '人事部門',
        agreement: '以下を読み同意します',
        agreementPrefix: '以下を読み同意します',
        agreementLink: 'ユーザー規約',
        agreementSuffix: '',
        agreementTitle: 'ユーザー登録規約',
        agreementContent: '登録前にこの規約を注意深く読み同意してください。',
        submit: '登録完了',
        nextStep: '次のステップ',
        back: '前のステップ',
        backToLogin: 'ログインに戻る',
        login: '既存のアカウントでログイン',
        confirmPassword: 'パスワード確認',
        confirmPasswordPlaceholder: 'パスワードを再度入力してください',
        deptId: '部署ID',
        deptIdPlaceholder: '部署IDを入力してください',
        roleIds: '役割',
        roleIdsPlaceholder: '役割を選択してください',
        postIds: 'ポジション',
        postIdsPlaceholder: 'ポジションを選択してください',
        deptIds: '部署',
        deptIdsPlaceholder: '部署を選択してください',
        remark: '備考',
        remarkPlaceholder: '備考を入力してください（任意）',
        success: '登録成功',
        successTitle: '登録成功',
        successSubtitle: 'アカウントが正常に作成されました、新しいアカウントでログインしてください',
        successMessage: 'ユーザー {userName} が正常に登録されました',
        step1Success: '身元確認通過',
        step2Success: '認証コード確認通過',
        step3Success: '情報補完完了',
        step4Success: '権限設定完了',
        form: {
          agreementRequired: 'ユーザー規約を読み同意してください',
          captchaRequired: '認証コードを入力してください',
          usernameRequired: 'ユーザー名を入力してください',
          usernameLength: 'ユーザー名の長さは3-20文字の間でなければなりません',
          usernameFormat: 'ユーザー名は文字、数字、アンダースコアのみを含むことができます',
          emailRequired: 'メールアドレスを入力してください',
          emailFormat: '正しいメール形式を入力してください',
          passwordRequired: 'パスワードを入力してください',
          passwordLength: 'パスワードの長さは6-20文字の間でなければなりません',
          passwordFormat: 'パスワードは大文字、小文字、数字を含む必要があります',
          confirmPasswordRequired: 'パスワードを確認してください',
          passwordMismatch: '入力された2つのパスワードが一致しません',
          nickNameRequired: 'ニックネームを入力してください',
          nickNameLength: 'ニックネームの長さは2-20文字の間でなければなりません',
          realNameRequired: '実名を入力してください',
          realNameLength: '実名の長さは2-20文字の間でなければなりません',
          fullNameRequired: 'フルネームを入力してください',
          fullNameLength: 'フルネームの長さは2-50文字の間でなければなりません',
          englishNameLength: '英語名の長さは2-50文字の間でなければなりません',
          englishNameFormat: '英語名は文字とスペースのみを含むことができます',
          phoneNumberFormat: '正しい電話番号形式を入力してください',
          userTypeRequired: 'ユーザータイプを選択してください',
          statusRequired: 'ステータスを選択してください',
          deptIdRequired: '部署IDを入力してください',
          roleIdsRequired: '役割を選択してください',
          postIdsRequired: 'ポジションを選択してください',
          deptIdsRequired: '部署を選択してください'
        },
        error: {
          step1Failed: '身元確認失敗',
          step2Failed: '認証コード確認失敗',
          step3Failed: '情報補完失敗',
          step4Failed: '権限設定失敗',
          unknown: '登録に失敗しました、後でもう一度お試しください'
        }
      },
      
      // パスワード復旧
      passwordRecovery: {
        title: 'パスワード復旧',
        subtitle: 'メール認証を通じてパスワードを復旧してください',
        step1: '認証コード',
        step2: 'ユーザーとメール',
        step3: 'メール認証コード',
        step4: 'パスワードリセット',
        userName: 'ユーザー名',
        userNamePlaceholder: 'ユーザー名を入力してください',
        email: 'メールアドレス',
        emailPlaceholder: 'メールアドレスを入力してください',
        refreshCaptcha: '認証コードを更新',
        nextStep: '次のステップ',
        emailSent: '認証コードが送信されました',
        emailSentDesc: '認証コードが {email} に送信されました、ご確認ください',
        verify: '認証',
        resendCode: '再送信',
        resetPassword: 'パスワードリセット',
        successTitle: 'パスワードリセット成功',
        successSubtitle: 'パスワードが正常にリセットされました、新しいパスワードでログインしてください',
        backToLogin: 'ログインに戻る',
        back: '前のステップ',
        identityVerified: '身元確認成功',
        codeSent: '認証コード送信成功',
        emailVerified: 'メール認証成功',
        passwordResetSuccess: 'パスワードリセット成功',
        captchaVerified: '認証コード確認成功',
        successMessage: 'ユーザー {userName} のパスワードが正常にリセットされました',
        form: {
          emailRequired: 'メールアドレスを入力してください',
          userNameLength: 'ユーザー名の長さは3-20文字の間である必要があります'
        },
        error: {
          identityVerificationFailed: '身元確認失敗',
          sendCodeFailed: '認証コード送信失敗',
          emailVerificationFailed: 'メール認証失敗',
          passwordResetFailed: 'パスワードリセット失敗',
          captchaVerificationFailed: '認証コード確認失敗',
          emailMismatch: 'ユーザー名とメールアドレスが一致しません',
          invalidCode: '認証コードが間違っているか期限切れです',
          codeCooldown: '認証コード送信が頻繁すぎます、後でもう一度お試しください'
        }
      },
      
      // 認証コード関連
      captcha: {
        title: 'セキュリティ認証',
        error: {
          initFailed: '認証コード初期化失敗'
        },
        behavior: {
          default: 'スライダーを押して右端までドラッグしてください',
          success: '認証通過',
          failed: '認証失敗、再試行してください',
          verifyError: '認証プロセスエラー、再試行してください'
        },
        slider: {
          bgImage: 'キャプチャ背景画像',
          sliderImage: 'キャプチャスライダー画像',
          default: 'スライダーを押してパズルを完成させてください',
          success: '認証通過',
          failed: '認証失敗、再試行してください',
          verifyError: '認証プロセスエラー、再試行してください',
          maxRetryReached: '認証失敗回数が多すぎます、ページを更新して再試行してください',
          error: {
            invalidResponse: 'キャプチャ応答が無効です',
            loadFailed: 'キャプチャの読み込みに失敗しました'
          }
        }
      },
      
      // SMSログイン
      smsLogin: {
        title: 'SMSログイン',
        subtitle: '携帯電話認証コードでクイックログイン',
        sendCode: '認証コード送信',
        login: 'ログイン',
        codeSent: '認証コード送信成功',
        sendCodeFailed: '認証コード送信失敗',
        loginSuccess: 'ログイン成功',
        loginFailed: 'ログイン失敗',
        or: 'または',
        switchToPasswordLogin: 'パスワードログイン',
        register: 'アカウント登録',
        forgotPassword: 'パスワードを忘れた'
      },
      
      // サードパーティログイン
      thirdPartyLogin: {
        title: 'サードパーティログイン',
        subtitle: 'サードパーティアカウントでクイックログイン',
        alipay: 'Alipay',
        amazon: 'Amazon',
        apple: 'Apple',
        dingtalk: 'DingTalk',
        facebook: 'Facebook',
        github: 'GitHub',
        google: 'Google',
        linkedin: 'LinkedIn',
        microsoft: 'Microsoft',
        qq: 'QQ',
        twitter: 'Twitter',
        wechat: 'WeChat',
        weibo: 'Weibo',
        alipayFailed: 'Alipayログイン失敗',
        amazonFailed: 'Amazonログイン失敗',
        appleFailed: 'Appleログイン失敗',
        dingtalkFailed: 'DingTalkログイン失敗',
        facebookFailed: 'Facebookログイン失敗',
        githubFailed: 'GitHubログイン失敗',
        googleFailed: 'Googleログイン失敗',
        linkedinFailed: 'LinkedInログイン失敗',
        microsoftFailed: 'Microsoftログイン失敗',
        qqFailed: 'QQログイン失敗',
        twitterFailed: 'Twitterログイン失敗',
        wechatFailed: 'WeChatログイン失敗',
        weiboFailed: 'Weiboログイン失敗',
        noProviders: '利用可能なサードパーティログイン方法がありません',
        unsupportedProvider: 'サポートされていないログイン方法',
        or: 'または',
        switchToPasswordLogin: 'パスワードログイン',
        switchToSmsLogin: 'SMSログイン',
        register: 'アカウント登録',
        forgotPassword: 'パスワードを忘れた'
      },
      
      // デバイスフィンガープリント
      device: {
        getDeviceInfo: 'デバイスフィンガープリント情報を取得',
        deviceFingerprintType: 'デバイスフィンガープリントタイプ',
        deviceFingerprintNull: 'デバイスフィンガープリントがnullか',
        deviceFingerprintUndefined: 'デバイスフィンガープリントがundefinedか',
        deviceFingerprintKeyCount: 'デバイスフィンガープリントキー数',
        deviceFingerprintKeyList: 'デバイスフィンガープリントキーリスト',
        deviceFingerprintField: 'デバイスフィンガープリントloginFingerprintフィールド',
        loginParamsDetail: '構築されたログインパラメータ詳細',
        deviceFingerprintDetail: 'デバイスフィンガープリント詳細情報',
        empty: '空',
        backendHandled: '空（バックエンド処理）',
        set: '設定済み',
        initSuccess: 'デバイス情報初期化成功',
        initFailed: 'デバイス情報初期化失敗',
        collectionComponent: {
          title: 'デバイスフィンガープリント',
          description: 'ネイティブWeb APIを使用してデバイス情報を収集',
          collecting: '収集中...',
          collectDeviceInfo: 'デバイス情報収集',
          clearInfo: '情報クリア',
          collectingInfo: 'デバイス情報収集中...',
          deviceInfo: 'デバイス情報：',
          deviceId: 'デバイスID：',
          deviceType: 'デバイスタイプ：',
          deviceFingerprint: 'デバイスフィンガープリント：',
          platform: 'プラットフォーム：',
          language: '言語：',
          timezone: 'タイムゾーン：',
          screenResolution: '画面解像度：',
          colorDepth: '色深度：',
          pixelRatio: 'デバイスピクセル比：',
          cpuCores: 'CPUコア数：',
          deviceMemory: 'デバイスメモリ：',
          touchSupport: 'タッチサポート：',
          os: 'オペレーティングシステム：',
          browser: 'ブラウザ：',
          vpnDetection: 'VPN検出：',
          proxyDetection: 'プロキシ検出：',
          networkType: 'ネットワークタイプ：',
          cookieSupport: 'Cookieサポート：',
          notGenerated: '生成されていません',
          notCollected: '収集されていません',
          notDetected: '検出されていません',
          supported: 'サポート',
          notSupported: 'サポートなし',
          bits: 'ビット',
          copyToClipboard: 'クリップボードにコピー',
          copySuccess: 'デバイス情報がクリップボードにコピーされました',
          copyFailed: 'コピーに失敗しました、手動でコピーしてください',
          errorInfo: 'エラー情報',
          startCollection: 'デバイス情報収集開始...',
          collectionSuccess: 'デバイス情報収集成功',
          collectionFailed: 'デバイス情報収集失敗',
          copyError: 'コピー失敗'
        }
      },
      
      // 検証関連
      validation: {
        usernamePasswordRequired: 'ユーザー名とパスワードは空にできません',
        deviceInfoRequired: 'デバイスフィンガープリント情報は空にできません',
        deviceFingerprintRequired: 'デバイスフィンガープリントは空にできません',
        userAgentRequired: 'ユーザーエージェントは空にできません',
        loginTypeSourceRequired: 'ログインタイプとソースは空にできません'
      },
      
      // ログインフロー
      loginFlow: {
        paramsSerialized: 'シリアライズされたパラメータ長',
        paramsPreview: 'シリアライズされたパラメータプレビュー',
        paramsTooLarge: 'ログインパラメータが大きすぎます、パフォーマンスの問題を引き起こす可能性があります',
        serializationFailed: 'パラメータシリアライズ失敗',
        forceOfflineSuccess: '他のデバイスがキックアウトされました、ログイン成功',
        loginCancelled: 'ログインがキャンセルされました',
        singleUserCheckFailed: '単一ユーザーログインチェック失敗、通常のログインフローを継続',
        loginFailed: 'ログイン失敗',
        signalRInit: 'SignalR接続初期化開始',
        signalRInitSuccess: 'SignalR接続初期化成功',
        signalRInitFailed: 'SignalR接続初期化失敗',
        autoLogoutInit: '自動ログアウト機能初期化開始',
        autoLogoutInitSuccess: '自動ログアウト機能初期化成功',
        autoLogoutInitFailed: '自動ログアウト機能初期化失敗',
        pageInitFailed: 'ログインページ初期化失敗',
        pageInitError: 'ページ初期化失敗、リフレッシュして再試行してください',
        checkLockStatusFailed: 'アカウントロック状態チェック失敗',
        singleUserCheck: {
          title: '単一ユーザーログイン検出',
          content: 'アカウントが既に他のデバイスでログインしていることを検出しました（オンラインデバイス数：{onlineDeviceCount}）。\n\n{reason}他のデバイスをキックアウトしてログインを継続しますか？',
          kickout: '他のデバイスをキックアウト',
          cancel: 'ログインキャンセル'
        }
      },
      
      // 設定関連
      config: {
        loading: '設定を読み込み中...',
        loadingLoginConfig: 'ログイン設定を読み込み中、お待ちください...',
        captchaConfigSuccess: '認証コード設定読み込み成功',
        captchaConfigFailed: '認証コード設定取得失敗',
        captchaConfigError: 'バックエンド認証コード設定取得失敗',
        loginMethodConfigSuccess: 'ログイン方法設定読み込み成功',
        loginMethodConfigError: 'バックエンドログイン方法設定取得失敗',
        loginMethodConfigFailed: 'ログイン方法設定取得失敗'
      },
      
      // 認証コードコンポーネント
      captchaComponent: {
        refreshSuccess: '認証コードが更新されました',
        refreshFailed: '認証コード更新失敗',
        getFailed: '認証コード取得失敗',
        verifySuccess: '認証コード認証通過',
        invalidParams: '認証コードパラメータが無効です',
        statusUpdated: '認証コードステータスが更新されました',
        processError: '認証コード成功コールバック処理中にエラー',
        processFailed: '認証コード処理失敗、再試行してください',
        verifyFailed: '認証コード認証失敗',
        errorProcessFailed: '認証コードエラー処理失敗',
        initFailed: '認証コード初期化失敗',
        error: '認証コードエラー'
      },
      
      // ログイン方法は削除されました
      
      // エラー関連
      error: {
        permanentlyLocked: 'アカウントが永久にロックされました、管理者にお問い合わせください',
        tooManyAttempts: 'ログイン失敗回数が多すぎます、アカウントがロックされました'
      }
    }
  }
}
