export default {
  identity: {
    auth: {
      // 폼 필드 정의 - 모든 폼의 필드는 fields에서 통합 검증됨
      fields: {
        username: {
          label: '사용자명',
          placeholder: '사용자명을 입력하세요',
          validation: {
            required: '사용자명을 입력하세요',
            length: '사용자명 길이는 3-50자 사이여야 합니다'
          }
        },
        password: {
          label: '비밀번호',
          placeholder: '비밀번호를 입력하세요',
          validation: {
            required: '비밀번호를 입력하세요',
            length: '비밀번호 길이는 6-100자 사이여야 합니다'
          }
        },
        email: {
          label: '이메일',
          placeholder: '이메일을 입력하세요',
          validation: {
            required: '이메일을 입력하세요',
            format: '올바른 이메일 형식을 입력하세요'
          }
        },
        phone: {
          label: '휴대폰 번호',
          placeholder: '휴대폰 번호를 입력하세요',
          validation: {
            required: '휴대폰 번호를 입력하세요',
            format: '올바른 휴대폰 번호 형식을 입력하세요'
          }
        },
        captcha: {
          label: '인증코드',
          placeholder: '인증코드를 입력하세요',
          validation: {
            required: '인증코드를 입력하세요'
          }
        },
        confirmPassword: {
          label: '비밀번호 확인',
          placeholder: '비밀번호를 다시 입력하세요',
          validation: {
            required: '비밀번호를 확인하세요',
            mismatch: '입력한 두 비밀번호가 일치하지 않습니다'
          }
        },
        nickName: {
          label: '닉네임',
          placeholder: '닉네임을 입력하세요',
          validation: {
            required: '닉네임은 비어있을 수 없습니다',
            format: '2-50자, 중국어, 일본어, 한국어, 영어, 숫자, 마침표 및 하이픈 허용, 언더스코어 및 공백 불허'
          }
        },
        realName: {
          label: '실명',
          placeholder: '실명을 입력하세요',
          validation: {
            required: '실명은 비어있을 수 없습니다',
            format: '실명 길이는 2-20자 사이여야 하며, 중국어, 영어, 숫자 및 언더스코어만 포함 가능'
          }
        },
        fullName: {
          label: '전체 이름',
          placeholder: '전체 이름을 입력하세요',
          validation: {
            required: '전체 이름은 비어있을 수 없습니다',
            format: '전체 이름 길이는 2-20자 사이여야 하며, 중국어, 영어, 숫자 및 언더스코어만 포함 가능'
          }
        },
        englishName: {
          label: '영문명',
          placeholder: '영문명을 입력하세요',
          validation: {
            required: '영문명은 비어있을 수 없습니다',
            format: '영문명 길이는 2-100자 사이여야 하며, 문자로 시작해야 하고, 영문자, 공백, 하이픈 및 마침표만 포함 가능'
          }
        },
        verificationCode: {
          label: '인증코드',
          placeholder: '6자리 인증코드를 입력하세요',
          validation: {
            required: '인증코드를 입력하세요',
            length: '인증코드는 6자리 숫자여야 합니다',
            format: '인증코드는 6자리 숫자여야 합니다'
          }
        },
        newPassword: {
          label: '새 비밀번호',
          placeholder: '새 비밀번호를 입력하세요',
          validation: {
            required: '새 비밀번호를 입력하세요',
            length: '비밀번호 길이는 6-20자 사이여야 합니다',
            format: '비밀번호는 대소문자와 숫자를 포함해야 합니다'
          }
        },
        gender: {
          label: '성별',
          placeholder: '성별을 선택하세요',
          validation: {
            required: '성별을 선택하세요'
          },
          options: {
            unknown: '비공개',
            male: '남성',
            female: '여성'
          }
        },
        userType: {
          label: '사용자 유형',
          placeholder: '사용자 유형을 선택하세요',
          validation: {
            required: '사용자 유형을 선택하세요'
          },
          options: {
            normal: '일반 사용자',
            admin: '관리자'
          }
        },
        status: {
          label: '상태',
          placeholder: '상태를 선택하세요',
          validation: {
            required: '상태를 선택하세요'
          },
          options: {
            normal: '정상',
            disabled: '비활성화'
          }
        },
        deptId: {
          label: '부서',
          placeholder: '부서 ID를 입력하세요',
          validation: {
            required: '부서 ID를 입력하세요'
          }
        },
        roleIds: {
          label: '역할',
          placeholder: '역할을 선택하세요',
          validation: {
            required: '역할을 선택하세요'
          }
        },
        postIds: {
          label: '직책',
          placeholder: '직책을 선택하세요',
          validation: {
            required: '직책을 선택하세요'
          }
        },
        deptIds: {
          label: '소속 부서',
          placeholder: '소속 부서를 선택하세요',
          validation: {
            required: '소속 부서를 선택하세요'
          }
        },
        remark: {
          label: '비고',
          placeholder: '비고를 입력하세요'
        }
      },
      
      // 로그인 관련
      login: {
        title: '로그인',
        rememberMe: '비밀번호 기억',
        forgotPassword: '비밀번호 찾기',
        submit: '로그인',
        success: '로그인 성공',
        noAccount: '아직 계정이 없으신가요?',
        registerNow: '지금 등록',
        notAvailable: '기능을 사용할 수 없습니다',
        error: {
          invalidCredentials: '사용자명 또는 비밀번호가 잘못되었습니다',
          userDisabled: '사용자가 비활성화되었습니다',
          userNotFound: '사용자가 존재하지 않습니다',
          serverError: '서버 오류, 나중에 다시 시도하세요',
          unknown: '로그인 실패, 나중에 다시 시도하세요'
        }
      },
      
      // 사용자 등록
      register: {
        title: '사용자 등록',
        subtitle: '단계별로 사용자 등록을 완료하세요',
        step1: '신원 확인',
        step2: '기본 정보',
        step3: '상세 정보',
        step4: '권한 설정',
        roleUser: '사용자',
        roleAdmin: '관리자',
        postEmployee: '직원',
        postManager: '매니저',
        deptIT: 'IT부서',
        deptHR: '인사부서',
        agreement: '다음을 읽고 동의합니다',
        agreementPrefix: '다음을 읽고 동의합니다',
        agreementLink: '사용자 약관',
        agreementSuffix: '',
        agreementTitle: '사용자 등록 약관',
        agreementContent: '등록하기 전에 이 약관을 주의 깊게 읽고 동의하세요.',
        submit: '등록 완료',
        nextStep: '다음 단계',
        back: '이전 단계',
        backToLogin: '로그인으로 돌아가기',
        login: '기존 계정으로 로그인',
        confirmPassword: '비밀번호 확인',
        confirmPasswordPlaceholder: '비밀번호를 다시 입력하세요',
        deptId: '부서 ID',
        deptIdPlaceholder: '부서 ID를 입력하세요',
        roleIds: '역할',
        roleIdsPlaceholder: '역할을 선택하세요',
        postIds: '직책',
        postIdsPlaceholder: '직책을 선택하세요',
        deptIds: '부서',
        deptIdsPlaceholder: '부서를 선택하세요',
        remark: '비고',
        remarkPlaceholder: '비고를 입력하세요 (선택사항)',
        success: '등록 성공',
        successTitle: '등록 성공',
        successSubtitle: '계정이 성공적으로 생성되었습니다. 새 계정으로 로그인하세요',
        successMessage: '사용자 {userName}이(가) 성공적으로 등록되었습니다',
        step1Success: '신원 확인 통과',
        step2Success: '인증코드 확인 통과',
        step3Success: '정보 보완 완료',
        step4Success: '권한 설정 완료',
        form: {
          agreementRequired: '사용자 약관을 읽고 동의하세요',
          captchaRequired: '인증코드를 입력하세요',
          usernameRequired: '사용자명을 입력하세요',
          usernameLength: '사용자명 길이는 3-20자 사이여야 합니다',
          usernameFormat: '사용자명은 문자, 숫자, 밑줄만 포함할 수 있습니다',
          emailRequired: '이메일 주소를 입력하세요',
          emailFormat: '올바른 이메일 형식을 입력하세요',
          passwordRequired: '비밀번호를 입력하세요',
          passwordLength: '비밀번호 길이는 6-20자 사이여야 합니다',
          passwordFormat: '비밀번호는 대소문자와 숫자를 포함해야 합니다',
          confirmPasswordRequired: '비밀번호를 확인하세요',
          passwordMismatch: '입력한 두 비밀번호가 일치하지 않습니다',
          nickNameRequired: '닉네임을 입력하세요',
          nickNameLength: '닉네임 길이는 2-20자 사이여야 합니다',
          realNameRequired: '실명을 입력하세요',
          realNameLength: '실명 길이는 2-20자 사이여야 합니다',
          fullNameRequired: '전체 이름을 입력하세요',
          fullNameLength: '전체 이름 길이는 2-50자 사이여야 합니다',
          englishNameLength: '영문명 길이는 2-50자 사이여야 합니다',
          englishNameFormat: '영문명은 문자와 공백만 포함할 수 있습니다',
          phoneNumberFormat: '올바른 전화번호 형식을 입력하세요',
          userTypeRequired: '사용자 유형을 선택하세요',
          statusRequired: '상태를 선택하세요',
          deptIdRequired: '부서 ID를 입력하세요',
          roleIdsRequired: '역할을 선택하세요',
          postIdsRequired: '직책을 선택하세요',
          deptIdsRequired: '부서를 선택하세요'
        },
        error: {
          step1Failed: '신원 확인 실패',
          step2Failed: '인증코드 확인 실패',
          step3Failed: '정보 보완 실패',
          step4Failed: '권한 설정 실패',
          unknown: '등록 실패, 나중에 다시 시도하세요'
        }
      },
      
      // 비밀번호 찾기
      passwordRecovery: {
        title: '비밀번호 찾기',
        subtitle: '이메일 확인을 통해 비밀번호를 찾으세요',
        step1: '인증코드',
        step2: '사용자 및 이메일',
        step3: '이메일 인증코드',
        step4: '비밀번호 재설정',
        userName: '사용자명',
        userNamePlaceholder: '사용자명을 입력하세요',
        email: '이메일 주소',
        emailPlaceholder: '이메일 주소를 입력하세요',
        refreshCaptcha: '인증코드 새로고침',
        nextStep: '다음 단계',
        emailSent: '인증코드가 전송되었습니다',
        emailSentDesc: '인증코드가 {email}로 전송되었습니다. 확인하세요',
        verify: '확인',
        resendCode: '재전송',
        resetPassword: '비밀번호 재설정',
        successTitle: '비밀번호 재설정 성공',
        successSubtitle: '비밀번호가 성공적으로 재설정되었습니다. 새 비밀번호로 로그인하세요',
        backToLogin: '로그인으로 돌아가기',
        back: '이전 단계',
        identityVerified: '신원 확인 성공',
        codeSent: '인증코드 전송 성공',
        emailVerified: '이메일 확인 성공',
        passwordResetSuccess: '비밀번호 재설정 성공',
        captchaVerified: '인증코드 확인 성공',
        successMessage: '사용자 {userName}의 비밀번호가 성공적으로 재설정되었습니다',
        form: {
          emailRequired: '이메일 주소를 입력하세요',
          userNameLength: '사용자명 길이는 3-20자 사이여야 합니다'
        },
        error: {
          identityVerificationFailed: '신원 확인 실패',
          sendCodeFailed: '인증코드 전송 실패',
          emailVerificationFailed: '이메일 확인 실패',
          passwordResetFailed: '비밀번호 재설정 실패',
          captchaVerificationFailed: '인증코드 확인 실패',
          emailMismatch: '사용자명과 이메일 주소가 일치하지 않습니다',
          invalidCode: '인증코드가 잘못되었거나 만료되었습니다',
          codeCooldown: '인증코드 전송이 너무 빈번합니다. 나중에 다시 시도하세요'
        }
      },
      
      // 인증코드 관련
      captcha: {
        title: '보안 확인',
        error: {
          initFailed: '인증코드 초기화 실패'
        },
        behavior: {
          default: '슬라이더를 눌러 오른쪽 끝까지 드래그하세요',
          success: '인증 통과',
          failed: '인증 실패, 다시 시도하세요',
          verifyError: '인증 프로세스 오류, 다시 시도하세요'
        },
        slider: {
          bgImage: '캡차 배경 이미지',
          sliderImage: '캡차 슬라이더 이미지',
          default: '슬라이더를 눌러 퍼즐을 완성하세요',
          success: '인증 통과',
          failed: '인증 실패, 다시 시도하세요',
          verifyError: '인증 프로세스 오류, 다시 시도하세요',
          maxRetryReached: '인증 실패 횟수가 너무 많습니다. 페이지를 새로고침하고 다시 시도하세요',
          error: {
            invalidResponse: '캡차 응답이 유효하지 않습니다',
            loadFailed: '캡차 로드에 실패했습니다'
          }
        }
      },
      
      // SMS 로그인
      smsLogin: {
        title: 'SMS 로그인',
        subtitle: '휴대폰 인증코드로 빠른 로그인',
        sendCode: '인증코드 전송',
        login: '로그인',
        codeSent: '인증코드 전송 성공',
        sendCodeFailed: '인증코드 전송 실패',
        loginSuccess: '로그인 성공',
        loginFailed: '로그인 실패',
        or: '또는',
        switchToPasswordLogin: '비밀번호 로그인',
        register: '계정 등록',
        forgotPassword: '비밀번호 찾기'
      },
      
      // 제3자 로그인
      thirdPartyLogin: {
        title: '제3자 로그인',
        subtitle: '제3자 계정으로 빠른 로그인',
        alipay: '알리페이',
        amazon: 'Amazon',
        apple: 'Apple',
        dingtalk: '딩톡',
        facebook: 'Facebook',
        github: 'GitHub',
        google: 'Google',
        linkedin: 'LinkedIn',
        microsoft: 'Microsoft',
        qq: 'QQ',
        twitter: 'Twitter',
        wechat: '위챗',
        weibo: '웨이보',
        alipayFailed: '알리페이 로그인 실패',
        amazonFailed: 'Amazon 로그인 실패',
        appleFailed: 'Apple 로그인 실패',
        dingtalkFailed: '딩톡 로그인 실패',
        facebookFailed: 'Facebook 로그인 실패',
        githubFailed: 'GitHub 로그인 실패',
        googleFailed: 'Google 로그인 실패',
        linkedinFailed: 'LinkedIn 로그인 실패',
        microsoftFailed: 'Microsoft 로그인 실패',
        qqFailed: 'QQ 로그인 실패',
        twitterFailed: 'Twitter 로그인 실패',
        wechatFailed: '위챗 로그인 실패',
        weiboFailed: '웨이보 로그인 실패',
        noProviders: '사용 가능한 제3자 로그인 방법이 없습니다',
        unsupportedProvider: '지원하지 않는 로그인 방법',
        or: '또는',
        switchToPasswordLogin: '비밀번호 로그인',
        switchToSmsLogin: 'SMS 로그인',
        register: '계정 등록',
        forgotPassword: '비밀번호 찾기'
      },
      
      // 디바이스 지문
      device: {
        getDeviceInfo: '디바이스 지문 정보 획득',
        deviceFingerprintType: '디바이스 지문 유형',
        deviceFingerprintNull: '디바이스 지문이 null인지',
        deviceFingerprintUndefined: '디바이스 지문이 undefined인지',
        deviceFingerprintKeyCount: '디바이스 지문 키 수',
        deviceFingerprintKeyList: '디바이스 지문 키 목록',
        deviceFingerprintField: '디바이스 지문 loginFingerprint 필드',
        loginParamsDetail: '구성된 로그인 매개변수 세부사항',
        deviceFingerprintDetail: '디바이스 지문 세부 정보',
        empty: '비어있음',
        backendHandled: '비어있음 (백엔드 처리)',
        set: '설정됨',
        initSuccess: '디바이스 정보 초기화 성공',
        initFailed: '디바이스 정보 초기화 실패',
        collectionComponent: {
          title: '디바이스 정보 수집 구성요소',
          description: '네이티브 Web API를 사용하여 디바이스 정보 수집',
          collecting: '수집 중...',
          collectDeviceInfo: '디바이스 정보 수집',
          clearInfo: '정보 지우기',
          collectingInfo: '디바이스 정보 수집 중...',
          deviceInfo: '디바이스 정보:',
          deviceId: '디바이스 ID:',
          deviceType: '디바이스 유형:',
          deviceFingerprint: '디바이스 지문:',
          platform: '플랫폼:',
          language: '언어:',
          timezone: '시간대:',
          screenResolution: '화면 해상도:',
          colorDepth: '색상 깊이:',
          pixelRatio: '디바이스 픽셀 비율:',
          cpuCores: 'CPU 코어 수:',
          deviceMemory: '디바이스 메모리:',
          touchSupport: '터치 지원:',
          os: '운영체제:',
          browser: '브라우저:',
          vpnDetection: 'VPN 감지:',
          proxyDetection: '프록시 감지:',
          networkType: '네트워크 유형:',
          cookieSupport: 'Cookie 지원:',
          notGenerated: '생성되지 않음',
          notCollected: '수집되지 않음',
          notDetected: '감지되지 않음',
          supported: '지원됨',
          notSupported: '지원되지 않음',
          bits: '비트',
          copyToClipboard: '클립보드에 복사',
          copySuccess: '디바이스 정보가 클립보드에 복사되었습니다',
          copyFailed: '복사 실패, 수동으로 복사하세요',
          errorInfo: '오류 정보',
          startCollection: '디바이스 정보 수집 시작...',
          collectionSuccess: '디바이스 정보 수집 성공',
          collectionFailed: '디바이스 정보 수집 실패',
          copyError: '복사 실패'
        }
      },
      
      // 검증 관련
      validation: {
        usernamePasswordRequired: '사용자명과 비밀번호는 비어있을 수 없습니다',
        deviceInfoRequired: '디바이스 지문 정보는 비어있을 수 없습니다',
        deviceFingerprintRequired: '디바이스 지문은 비어있을 수 없습니다',
        userAgentRequired: '사용자 에이전트는 비어있을 수 없습니다',
        loginTypeSourceRequired: '로그인 유형과 소스는 비어있을 수 없습니다'
      },
      
      // 로그인 흐름
      loginFlow: {
        paramsSerialized: '직렬화된 매개변수 길이',
        paramsPreview: '직렬화된 매개변수 미리보기',
        paramsTooLarge: '로그인 매개변수가 너무 큽니다. 성능 문제를 일으킬 수 있습니다',
        serializationFailed: '매개변수 직렬화 실패',
        forceOfflineSuccess: '다른 디바이스가 제외되었습니다. 로그인 성공',
        loginCancelled: '로그인이 취소되었습니다',
        singleUserCheckFailed: '단일 사용자 로그인 확인 실패, 일반 로그인 흐름 계속',
        loginFailed: '로그인 실패',
        signalRInit: 'SignalR 연결 초기화 시작',
        signalRInitSuccess: 'SignalR 연결 초기화 성공',
        signalRInitFailed: 'SignalR 연결 초기화 실패',
        autoLogoutInit: '자동 로그아웃 기능 초기화 시작',
        autoLogoutInitSuccess: '자동 로그아웃 기능 초기화 성공',
        autoLogoutInitFailed: '자동 로그아웃 기능 초기화 실패',
        pageInitFailed: '로그인 페이지 초기화 실패',
        pageInitError: '페이지 초기화 실패, 새로고침 후 다시 시도하세요',
        checkLockStatusFailed: '계정 잠금 상태 확인 실패',
        singleUserCheck: {
          title: '단일 사용자 로그인 감지',
          content: '계정이 이미 다른 디바이스에서 로그인되어 있음을 감지했습니다 (온라인 디바이스 수: {onlineDeviceCount}).\n\n{reason}다른 디바이스를 제외하고 로그인을 계속하시겠습니까?',
          kickout: '다른 디바이스 제외',
          cancel: '로그인 취소'
        }
      },
      
      // 구성 관련
      config: {
        loading: '구성 로딩 중...',
        loadingLoginConfig: '로그인 구성 로딩 중, 잠시 기다려주세요...',
        captchaConfigSuccess: '인증코드 구성 로딩 성공',
        captchaConfigFailed: '인증코드 구성 가져오기 실패',
        captchaConfigError: '백엔드 인증코드 구성 가져오기 실패',
        loginMethodConfigSuccess: '로그인 방법 구성 로딩 성공',
        loginMethodConfigError: '백엔드 로그인 방법 구성 가져오기 실패',
        loginMethodConfigFailed: '로그인 방법 구성 가져오기 실패'
      },
      
      // 인증코드 구성요소
      captchaComponent: {
        refreshSuccess: '인증코드가 새로고침되었습니다',
        refreshFailed: '인증코드 새로고침 실패',
        getFailed: '인증코드 가져오기 실패',
        verifySuccess: '인증코드 확인 통과',
        invalidParams: '인증코드 매개변수가 유효하지 않습니다',
        statusUpdated: '인증코드 상태가 업데이트되었습니다',
        processError: '인증코드 성공 콜백 처리 중 오류',
        processFailed: '인증코드 처리 실패, 다시 시도하세요',
        verifyFailed: '인증코드 확인 실패',
        errorProcessFailed: '인증코드 오류 처리 실패',
        initFailed: '인증코드 초기화 실패',
        error: '인증코드 오류'
      },
      
      // 로그인 방법이 제거되었습니다
      
      // 오류 관련
      error: {
        permanentlyLocked: '계정이 영구적으로 잠겼습니다. 관리자에게 문의하세요',
        tooManyAttempts: '로그인 실패 횟수가 너무 많습니다. 계정이 잠겼습니다'
      }
    }
  }
}
