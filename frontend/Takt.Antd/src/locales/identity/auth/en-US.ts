export default {
  identity: {
    auth: {
      // Form field definitions - all form fields are uniformly validated in fields
      fields: {
        username: {
          label: 'Username',
          placeholder: 'Please enter username',
          validation: {
            required: 'Please enter username',
            length: 'Username length should be between 3-50 characters'
          }
        },
        password: {
          label: 'Password',
          placeholder: 'Please enter password',
          validation: {
            required: 'Please enter password',
            length: 'Password length should be between 6-100 characters'
          }
        },
        email: {
          label: 'Email',
          placeholder: 'Please enter email',
          validation: {
            required: 'Please enter email',
            format: 'Please enter a valid email format'
          }
        },
        phone: {
          label: 'Phone Number',
          placeholder: 'Please enter phone number',
          validation: {
            required: 'Please enter phone number',
            format: 'Please enter a valid phone number format'
          }
        },
        captcha: {
          label: 'Captcha',
          placeholder: 'Please enter captcha',
          validation: {
            required: 'Please enter captcha'
          }
        },
        confirmPassword: {
          label: 'Confirm Password',
          placeholder: 'Please enter password again',
          validation: {
            required: 'Please confirm password',
            mismatch: 'The two passwords entered are inconsistent'
          }
        },
        nickName: {
          label: 'Nickname',
          placeholder: 'Please enter nickname',
          validation: {
            required: 'Nickname cannot be empty',
            format: '2-50 characters, allowing Chinese, Japanese, Korean, English, numbers, periods and hyphens, no underscores and spaces'
          }
        },
        realName: {
          label: 'Real Name',
          placeholder: 'Please enter real name',
          validation: {
            required: 'Real name cannot be empty',
            format: 'Real name length must be between 2-20 characters, only Chinese, English, numbers and underscores'
          }
        },
        fullName: {
          label: 'Full Name',
          placeholder: 'Please enter full name',
          validation: {
            required: 'Full name cannot be empty',
            format: 'Full name length must be between 2-20 characters, only Chinese, English, numbers and underscores'
          }
        },
        englishName: {
          label: 'English Name',
          placeholder: 'Please enter English name',
          validation: {
            required: 'English name cannot be empty',
            format: 'English name length must be between 2-100 characters, must start with a letter, only English letters, spaces, hyphens and periods'
          }
        },
        verificationCode: {
          label: 'Verification Code',
          placeholder: 'Please enter 6-digit verification code',
          validation: {
            required: 'Please enter verification code',
            length: 'Verification code should be 6 digits',
            format: 'Verification code should be 6 digits'
          }
        },
        newPassword: {
          label: 'New Password',
          placeholder: 'Please enter new password',
          validation: {
            required: 'Please enter new password',
            length: 'Password length should be between 6-20 characters',
            format: 'Password must contain uppercase and lowercase letters and numbers'
          }
        },
        gender: {
          label: 'Gender',
          placeholder: 'Please select gender',
          validation: {
            required: 'Please select gender'
          },
          options: {
            unknown: 'Private',
            male: 'Male',
            female: 'Female'
          }
        },
        userType: {
          label: 'User Type',
          placeholder: 'Please select user type',
          validation: {
            required: 'Please select user type'
          },
          options: {
            normal: 'Normal User',
            admin: 'Administrator'
          }
        },
        status: {
          label: 'Status',
          placeholder: 'Please select status',
          validation: {
            required: 'Please select status'
          },
          options: {
            normal: 'Normal',
            disabled: 'Disabled'
          }
        },
        deptId: {
          label: 'Department',
          placeholder: 'Please enter department ID',
          validation: {
            required: 'Please enter department ID'
          }
        },
        roleIds: {
          label: 'Roles',
          placeholder: 'Please select roles',
          validation: {
            required: 'Please select roles'
          }
        },
        postIds: {
          label: 'Positions',
          placeholder: 'Please select positions',
          validation: {
            required: 'Please select positions'
          }
        },
        deptIds: {
          label: 'Departments',
          placeholder: 'Please select departments',
          validation: {
            required: 'Please select departments'
          }
        },
        remark: {
          label: 'Remarks',
          placeholder: 'Please enter remarks'
        }
      },
      
      // Login related
      login: {
        title: 'Login',
        rememberMe: 'Remember',
        forgotPassword: 'Forgot?',
        submit: 'Login',
        success: 'Login successful',
        noAccount: 'No account yet?',
        registerNow: 'Register Now',
        notAvailable: 'Feature temporarily unavailable',
        error: {
          invalidCredentials: 'Username or password incorrect',
          userDisabled: 'User has been disabled',
          userNotFound: 'User does not exist',
          serverError: 'Server error, please try again later',
          unknown: 'Login failed, please try again later'
        }
      },
      
      // User registration
      register: {
        title: 'User Registration',
        subtitle: 'Please complete user registration step by step',
        step1: 'Identity Verification',
        step2: 'Basic Information',
        step3: 'Detailed Information',
        step4: 'Permission Setup',
        roleUser: 'User',
        roleAdmin: 'Administrator',
        postEmployee: 'Employee',
        postManager: 'Manager',
        deptIT: 'IT Department',
        deptHR: 'HR Department',
        agreement: 'I have read and agree to',
        agreementPrefix: 'I have read and agree to',
        agreementLink: 'User Agreement',
        agreementSuffix: '',
        agreementTitle: 'User Registration Agreement',
        agreementContent: 'Please read carefully and agree to this agreement before registering.',
        submit: 'Complete Registration',
        nextStep: 'Next Step',
        back: 'Previous Step',
        backToLogin: 'Back to Login',
        login: 'Login with existing account',
        confirmPassword: 'Confirm Password',
        confirmPasswordPlaceholder: 'Please enter password again',
        deptId: 'Department ID',
        deptIdPlaceholder: 'Please enter department ID',
        roleIds: 'Roles',
        roleIdsPlaceholder: 'Please select roles',
        postIds: 'Positions',
        postIdsPlaceholder: 'Please select positions',
        deptIds: 'Departments',
        deptIdsPlaceholder: 'Please select departments',
        remark: 'Remark',
        remarkPlaceholder: 'Please enter remark (optional)',
        success: 'Registration successful',
        successTitle: 'Registration Successful',
        successSubtitle: 'Your account has been successfully created, please login with your new account',
        successMessage: 'User {userName} has been successfully registered',
        step1Success: 'Identity verification passed',
        step2Success: 'Verification code verification passed',
        step3Success: 'Information completion completed',
        step4Success: 'Permission setup completed',
        form: {
          agreementRequired: 'Please read and agree to the user agreement',
          captchaRequired: 'Please enter captcha',
          usernameRequired: 'Please enter username',
          usernameLength: 'Username length should be between 3-20 characters',
          usernameFormat: 'Username can only contain letters, numbers and underscores',
          emailRequired: 'Please enter email address',
          emailFormat: 'Please enter a valid email format',
          passwordRequired: 'Please enter password',
          passwordLength: 'Password length should be between 6-20 characters',
          passwordFormat: 'Password must contain uppercase, lowercase letters and numbers',
          confirmPasswordRequired: 'Please confirm password',
          passwordMismatch: 'The two passwords entered do not match',
          nickNameRequired: 'Please enter nickname',
          nickNameLength: 'Nickname length should be between 2-20 characters',
          realNameRequired: 'Please enter real name',
          realNameLength: 'Real name length should be between 2-20 characters',
          fullNameRequired: 'Please enter full name',
          fullNameLength: 'Full name length should be between 2-50 characters',
          englishNameLength: 'English name length should be between 2-50 characters',
          englishNameFormat: 'English name can only contain letters and spaces',
          phoneNumberFormat: 'Please enter a valid phone number format',
          userTypeRequired: 'Please select user type',
          statusRequired: 'Please select status',
          deptIdRequired: 'Please enter department ID',
          roleIdsRequired: 'Please select roles',
          postIdsRequired: 'Please select positions',
          deptIdsRequired: 'Please select departments'
        },
        error: {
          step1Failed: 'Identity verification failed',
          step2Failed: 'Verification code verification failed',
          step3Failed: 'Information completion failed',
          step4Failed: 'Permission setup failed',
          unknown: 'Registration failed, please try again later'
        }
      },
      
      // Password recovery
      passwordRecovery: {
        title: 'Password Recovery',
        subtitle: 'Recover your password through email verification',
        step1: 'Verification Code',
        step2: 'User and Email',
        step3: 'Email Verification Code',
        step4: 'Reset Password',
        userName: 'Username',
        userNamePlaceholder: 'Please enter your username',
        email: 'Email Address',
        emailPlaceholder: 'Please enter your email address',
        refreshCaptcha: 'Refresh Captcha',
        nextStep: 'Next Step',
        emailSent: 'Verification code sent',
        emailSentDesc: 'Verification code has been sent to your email {email}, please check',
        verify: 'Verify',
        resendCode: 'Resend',
        resetPassword: 'Reset Password',
        successTitle: 'Password Reset Successful',
        successSubtitle: 'Your password has been successfully reset, please login with your new password',
        backToLogin: 'Back to Login',
        back: 'Previous Step',
        identityVerified: 'Identity verification successful',
        codeSent: 'Verification code sent successfully',
        emailVerified: 'Email verification successful',
        passwordResetSuccess: 'Password reset successful',
        captchaVerified: 'Captcha verification successful',
        successMessage: 'Password for user {userName} has been successfully reset',
        form: {
          emailRequired: 'Please enter email address',
          userNameLength: 'Username length should be between 3-20 characters'
        },
        error: {
          identityVerificationFailed: 'Identity verification failed',
          sendCodeFailed: 'Verification code sending failed',
          emailVerificationFailed: 'Email verification failed',
          passwordResetFailed: 'Password reset failed',
          captchaVerificationFailed: 'Captcha verification failed',
          emailMismatch: 'Username and email address do not match',
          invalidCode: 'Verification code is incorrect or expired',
          codeCooldown: 'Verification code sending too frequent, please try again later'
        }
      },
      
      // Captcha related
      captcha: {
        title: 'Security Verification',
        error: {
          initFailed: 'Captcha initialization failed'
        },
        behavior: {
          default: 'Please hold and drag the slider to the right',
          success: 'Verification passed',
          failed: 'Verification failed, please try again',
          verifyError: 'Verification process error, please try again'
        },
        slider: {
          bgImage: 'Captcha background image',
          sliderImage: 'Captcha slider image',
          default: 'Please hold and drag the slider to complete the puzzle',
          success: 'Verification passed',
          failed: 'Verification failed, please try again',
          verifyError: 'Verification process error, please try again',
          maxRetryReached: 'Too many verification failures, please refresh and try again',
          error: {
            invalidResponse: 'Invalid captcha response',
            loadFailed: 'Failed to load captcha'
          }
        }
      },
      
      // SMS和OAuth登录功能已移除
      
      // Device fingerprint
      device: {
        getDeviceInfo: 'Device fingerprint information obtained',
        deviceFingerprintType: 'Device fingerprint type',
        deviceFingerprintNull: 'Is device fingerprint null',
        deviceFingerprintUndefined: 'Is device fingerprint undefined',
        deviceFingerprintKeyCount: 'Device fingerprint key count',
        deviceFingerprintKeyList: 'Device fingerprint key list',
        deviceFingerprintField: 'Device fingerprint loginFingerprint field',
        loginParamsDetail: 'Constructed login parameter details',
        deviceFingerprintDetail: 'Device fingerprint detailed information',
        empty: 'Empty',
        backendHandled: 'Empty (handled by backend)',
        set: 'Set',
        initSuccess: 'Device information initialization successful',
        initFailed: 'Device information initialization failed',
        collectionComponent: {
          title: 'Device fingerprint',
          description: 'Collect device information using native Web API',
          collecting: 'Collecting...',
          collectDeviceInfo: 'Collect Device Information',
          clearInfo: 'Clear Information',
          collectingInfo: 'Collecting device information...',
          deviceInfo: 'Device Information:',
          deviceId: 'Device ID:',
          deviceType: 'Device Type:',
          deviceFingerprint: 'Device Fingerprint:',
          platform: 'Platform:',
          language: 'Language:',
          timezone: 'Timezone:',
          screenResolution: 'Screen Resolution:',
          colorDepth: 'Color Depth:',
          pixelRatio: 'Device Pixel Ratio:',
          cpuCores: 'CPU Cores:',
          deviceMemory: 'Device Memory:',
          touchSupport: 'Touch Support:',
          os: 'Operating System:',
          browser: 'Browser:',
          vpnDetection: 'VPN Detection:',
          proxyDetection: 'Proxy Detection:',
          networkType: 'Network Type:',
          cookieSupport: 'Cookie Support:',
          notGenerated: 'Not Generated',
          notCollected: 'Not Collected',
          notDetected: 'Not Detected',
          supported: 'Supported',
          notSupported: 'Not Supported',
          bits: 'bits',
          copyToClipboard: 'Copy to Clipboard',
          copySuccess: 'Device information copied to clipboard',
          copyFailed: 'Copy failed, please copy manually',
          errorInfo: 'Error Information',
          startCollection: 'Starting device information collection...',
          collectionSuccess: 'Device information collection successful',
          collectionFailed: 'Device information collection failed',
          copyError: 'Copy failed'
        }
      },
      
      // Validation related
      validation: {
        usernamePasswordRequired: 'Username and password cannot be empty',
        deviceInfoRequired: 'Device fingerprint information cannot be empty',
        deviceFingerprintRequired: 'Device fingerprint cannot be empty',
        userAgentRequired: 'User agent cannot be empty',
        loginTypeSourceRequired: 'Login type and source cannot be empty'
      },
      
      // Login flow
      loginFlow: {
        paramsSerialized: 'Serialized parameter length',
        paramsPreview: 'Serialized parameter preview',
        paramsTooLarge: 'Login parameters too large, may cause performance issues',
        serializationFailed: 'Parameter serialization failed',
        forceOfflineSuccess: 'Other devices kicked out, login successful',
        loginCancelled: 'Login cancelled',
        singleUserCheckFailed: 'Single user login check failed, continuing normal login flow',
        loginFailed: 'Login failed',
        signalRInit: 'Starting SignalR connection initialization',
        signalRInitSuccess: 'SignalR connection initialization successful',
        signalRInitFailed: 'SignalR connection initialization failed',
        autoLogoutInit: 'Starting auto logout function initialization',
        autoLogoutInitSuccess: 'Auto logout function initialization successful',
        autoLogoutInitFailed: 'Auto logout function initialization failed',
        pageInitFailed: 'Login page initialization failed',
        pageInitError: 'Page initialization failed, please refresh and try again',
        checkLockStatusFailed: 'Failed to check account lock status',
        singleUserCheck: {
          title: 'Single User Login Detection',
          content: 'Detected that your account is already logged in on other devices (online device count: {onlineDeviceCount}).\n\n{reason}Do you want to kick out other devices and continue logging in?',
          kickout: 'Kick Out Other Devices',
          cancel: 'Cancel Login'
        }
      },
      
      // Configuration related
      config: {
        loading: 'Loading configuration...',
        loadingLoginConfig: 'Loading login configuration, please wait...',
        captchaConfigSuccess: 'Captcha configuration loaded successfully',
        captchaConfigFailed: 'Failed to get captcha configuration',
        captchaConfigError: 'Failed to get backend captcha configuration',
        loginMethodConfigSuccess: 'Login method configuration loaded successfully',
        loginMethodConfigError: 'Failed to get backend login method configuration',
        loginMethodConfigFailed: 'Failed to get login method configuration'
      },
      
      // Captcha component
      captchaComponent: {
        refreshSuccess: 'Captcha refreshed',
        refreshFailed: 'Failed to refresh captcha',
        getFailed: 'Failed to get captcha',
        verifySuccess: 'Captcha verification passed',
        invalidParams: 'Invalid captcha parameters',
        statusUpdated: 'Captcha status updated',
        processError: 'Error processing captcha success callback',
        processFailed: 'Captcha processing failed, please try again',
        verifyFailed: 'Captcha verification failed',
        errorProcessFailed: 'Captcha error processing failed',
        initFailed: 'Captcha initialization failed',
        error: 'Captcha error'
      },
      
      // Login methods removed
      
      // Error related
      error: {
        permanentlyLocked: 'Account has been permanently locked, please contact administrator',
        tooManyAttempts: 'Too many login failures, account has been locked'
      }
    }
  }
}
