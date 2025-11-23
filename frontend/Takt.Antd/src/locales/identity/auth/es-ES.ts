export default {
  identity: {
    auth: {
      // Definiciones de campos de formulario - todos los campos de formulario se validan de manera unificada en fields
      fields: {
        username: {
          label: 'Nombre de usuario',
          placeholder: 'Por favor ingrese el nombre de usuario',
          validation: {
            required: 'Por favor ingrese el nombre de usuario',
            length: 'La longitud del nombre de usuario debe estar entre 3-50 caracteres'
          }
        },
        password: {
          label: 'Contraseña',
          placeholder: 'Por favor ingrese la contraseña',
          validation: {
            required: 'Por favor ingrese la contraseña',
            length: 'La longitud de la contraseña debe estar entre 6-100 caracteres'
          }
        },
        email: {
          label: 'Correo electrónico',
          placeholder: 'Por favor ingrese el correo electrónico',
          validation: {
            required: 'Por favor ingrese el correo electrónico',
            format: 'Por favor ingrese un formato de correo electrónico correcto'
          }
        },
        phone: {
          label: 'Número de teléfono',
          placeholder: 'Por favor ingrese el número de teléfono',
          validation: {
            required: 'Por favor ingrese el número de teléfono',
            format: 'Por favor ingrese un formato de número de teléfono correcto'
          }
        },
        captcha: {
          label: 'Código de verificación',
          placeholder: 'Por favor ingrese el código de verificación',
          validation: {
            required: 'Por favor ingrese el código de verificación'
          }
        },
        confirmPassword: {
          label: 'Confirmar contraseña',
          placeholder: 'Por favor ingrese la contraseña nuevamente',
          validation: {
            required: 'Por favor confirme la contraseña',
            mismatch: 'Las dos contraseñas ingresadas no coinciden'
          }
        },
        nickName: {
          label: 'Apodo',
          placeholder: 'Por favor ingrese el apodo',
          validation: {
            required: 'El apodo no puede estar vacío',
            format: '2-50 caracteres, permite chino, japonés, coreano, inglés, números, puntos y guiones, no permite guiones bajos y espacios'
          }
        },
        realName: {
          label: 'Nombre real',
          placeholder: 'Por favor ingrese el nombre real',
          validation: {
            required: 'El nombre real no puede estar vacío',
            format: 'La longitud del nombre real debe estar entre 2-20 caracteres, solo puede contener chino, inglés, números y guiones bajos'
          }
        },
        fullName: {
          label: 'Nombre completo',
          placeholder: 'Por favor ingrese el nombre completo',
          validation: {
            required: 'El nombre completo no puede estar vacío',
            format: 'La longitud del nombre completo debe estar entre 2-20 caracteres, solo puede contener chino, inglés, números y guiones bajos'
          }
        },
        englishName: {
          label: 'Nombre en inglés',
          placeholder: 'Por favor ingrese el nombre en inglés',
          validation: {
            required: 'El nombre en inglés no puede estar vacío',
            format: 'La longitud del nombre en inglés debe estar entre 2-100 caracteres, debe comenzar con una letra, solo puede contener letras inglesas, espacios, guiones y puntos'
          }
        },
        verificationCode: {
          label: 'Código de verificación',
          placeholder: 'Por favor ingrese el código de verificación de 6 dígitos',
          validation: {
            required: 'Por favor ingrese el código de verificación',
            length: 'El código de verificación debe ser de 6 dígitos',
            format: 'El código de verificación debe ser de 6 dígitos'
          }
        },
        newPassword: {
          label: 'Nueva contraseña',
          placeholder: 'Por favor ingrese la nueva contraseña',
          validation: {
            required: 'Por favor ingrese la nueva contraseña',
            length: 'La longitud de la contraseña debe estar entre 6-20 caracteres',
            format: 'La contraseña debe contener letras mayúsculas y minúsculas y números'
          }
        },
        gender: {
          label: 'Género',
          placeholder: 'Por favor seleccione el género',
          validation: {
            required: 'Por favor seleccione el género'
          },
          options: {
            unknown: 'Confidencial',
            male: 'Masculino',
            female: 'Femenino'
          }
        },
        userType: {
          label: 'Tipo de usuario',
          placeholder: 'Por favor seleccione el tipo de usuario',
          validation: {
            required: 'Por favor seleccione el tipo de usuario'
          },
          options: {
            normal: 'Usuario normal',
            admin: 'Administrador'
          }
        },
        status: {
          label: 'Estado',
          placeholder: 'Por favor seleccione el estado',
          validation: {
            required: 'Por favor seleccione el estado'
          },
          options: {
            normal: 'Normal',
            disabled: 'Deshabilitado'
          }
        },
        deptId: {
          label: 'Departamento',
          placeholder: 'Por favor ingrese el ID del departamento',
          validation: {
            required: 'Por favor ingrese el ID del departamento'
          }
        },
        roleIds: {
          label: 'Roles',
          placeholder: 'Por favor seleccione los roles',
          validation: {
            required: 'Por favor seleccione los roles'
          }
        },
        postIds: {
          label: 'Cargos',
          placeholder: 'Por favor seleccione los cargos',
          validation: {
            required: 'Por favor seleccione los cargos'
          }
        },
        deptIds: {
          label: 'Departamentos pertenecientes',
          placeholder: 'Por favor seleccione los departamentos pertenecientes',
          validation: {
            required: 'Por favor seleccione los departamentos pertenecientes'
          }
        },
        remark: {
          label: 'Observaciones',
          placeholder: 'Por favor ingrese las observaciones'
        }
      },
      
      // Inicio de sesión
      login: {
        title: 'Iniciar sesión',
        rememberMe: 'Recordar',
        forgotPassword: '¿Olvidó la contraseña?',
        submit: 'Iniciar sesión',
        success: 'Inicio de sesión exitoso',
        noAccount: '¿No tiene una cuenta?',
        registerNow: 'Registrarse ahora',
        notAvailable: 'Función temporalmente no disponible',
        error: {
          invalidCredentials: 'Nombre de usuario o contraseña incorrectos',
          userDisabled: 'Usuario deshabilitado',
          userNotFound: 'Usuario no existe',
          serverError: 'Error del servidor, intente más tarde',
          unknown: 'Error de inicio de sesión, intente más tarde'
        }
      },
      
      // Registro de usuario
      register: {
        title: 'Registro de usuario',
        subtitle: 'Por favor complete el registro de usuario paso a paso',
        step1: 'Verificación de Identidad',
        step2: 'Información Básica',
        step3: 'Información Detallada',
        step4: 'Configuración de Permisos',
        roleUser: 'Usuario',
        roleAdmin: 'Administrador',
        postEmployee: 'Empleado',
        postManager: 'Gerente',
        deptIT: 'Departamento de TI',
        deptHR: 'Departamento de RRHH',
        agreement: 'He leído y acepto',
        agreementPrefix: 'He leído y acepto',
        agreementLink: 'los términos de usuario',
        agreementSuffix: '',
        agreementTitle: 'Acuerdo de registro de usuario',
        agreementContent: 'Por favor lea cuidadosamente y acepte este acuerdo antes del registro.',
        submit: 'Completar registro',
        nextStep: 'Siguiente paso',
        back: 'Paso anterior',
        backToLogin: 'Volver al inicio de sesión',
        login: 'Iniciar sesión con cuenta existente',
        confirmPassword: 'Confirmar Contraseña',
        confirmPasswordPlaceholder: 'Por favor ingrese la contraseña nuevamente',
        deptId: 'ID del Departamento',
        deptIdPlaceholder: 'Por favor ingrese el ID del departamento',
        roleIds: 'Roles',
        roleIdsPlaceholder: 'Por favor seleccione roles',
        postIds: 'Posiciones',
        postIdsPlaceholder: 'Por favor seleccione posiciones',
        deptIds: 'Departamentos',
        deptIdsPlaceholder: 'Por favor seleccione departamentos',
        remark: 'Observación',
        remarkPlaceholder: 'Por favor ingrese observación (opcional)',
        success: 'Registro exitoso',
        successTitle: 'Registro exitoso',
        successSubtitle: 'Su cuenta ha sido creada exitosamente, por favor inicie sesión con su nueva cuenta',
        successMessage: 'Usuario {userName} ha sido registrado exitosamente',
        step1Success: 'Verificación de identidad exitosa',
        step2Success: 'Verificación de código de verificación exitosa',
        step3Success: 'Completar información terminado',
        step4Success: 'Configuración de permisos terminada',
        form: {
          agreementRequired: 'Por favor lea y acepte los términos de usuario',
          captchaRequired: 'Por favor ingrese el código de verificación',
          usernameRequired: 'Por favor ingrese el nombre de usuario',
          usernameLength: 'La longitud del nombre de usuario debe estar entre 3-20 caracteres',
          usernameFormat: 'El nombre de usuario solo puede contener letras, números y guiones bajos',
          emailRequired: 'Por favor ingrese la dirección de correo electrónico',
          emailFormat: 'Por favor ingrese un formato de correo electrónico válido',
          passwordRequired: 'Por favor ingrese la contraseña',
          passwordLength: 'La longitud de la contraseña debe estar entre 6-20 caracteres',
          passwordFormat: 'La contraseña debe contener letras mayúsculas, minúsculas y números',
          confirmPasswordRequired: 'Por favor confirme la contraseña',
          passwordMismatch: 'Las dos contraseñas ingresadas no coinciden',
          nickNameRequired: 'Por favor ingrese el apodo',
          nickNameLength: 'La longitud del apodo debe estar entre 2-20 caracteres',
          realNameRequired: 'Por favor ingrese el nombre real',
          realNameLength: 'La longitud del nombre real debe estar entre 2-20 caracteres',
          fullNameRequired: 'Por favor ingrese el nombre completo',
          fullNameLength: 'La longitud del nombre completo debe estar entre 2-50 caracteres',
          englishNameLength: 'La longitud del nombre en inglés debe estar entre 2-50 caracteres',
          englishNameFormat: 'El nombre en inglés solo puede contener letras y espacios',
          phoneNumberFormat: 'Por favor ingrese un formato de número de teléfono válido',
          userTypeRequired: 'Por favor seleccione el tipo de usuario',
          statusRequired: 'Por favor seleccione el estado',
          deptIdRequired: 'Por favor ingrese el ID del departamento',
          roleIdsRequired: 'Por favor seleccione roles',
          postIdsRequired: 'Por favor seleccione posiciones',
          deptIdsRequired: 'Por favor seleccione departamentos'
        },
        error: {
          step1Failed: 'Error en verificación de identidad',
          step2Failed: 'Error en verificación de código de verificación',
          step3Failed: 'Error al completar información',
          step4Failed: 'Error en configuración de permisos',
          unknown: 'Error de registro, intente más tarde'
        }
      },
      
      // Recuperación de contraseña
      passwordRecovery: {
        title: 'Recuperar contraseña',
        subtitle: 'Recupere su contraseña a través de la verificación por correo electrónico',
        step1: 'Código de verificación',
        step2: 'Usuario y correo electrónico',
        step3: 'Código de verificación por correo electrónico',
        step4: 'Restablecer contraseña',
        userName: 'Nombre de usuario',
        userNamePlaceholder: 'Por favor ingrese su nombre de usuario',
        email: 'Dirección de correo electrónico',
        emailPlaceholder: 'Por favor ingrese su dirección de correo electrónico',
        refreshCaptcha: 'Actualizar código de verificación',
        nextStep: 'Siguiente paso',
        emailSent: 'Código de verificación enviado',
        emailSentDesc: 'El código de verificación ha sido enviado a {email}, por favor verifique',
        verify: 'Verificar',
        resendCode: 'Reenviar',
        resetPassword: 'Restablecer contraseña',
        successTitle: 'Restablecimiento de contraseña exitoso',
        successSubtitle: 'Su contraseña ha sido restablecida exitosamente, por favor inicie sesión con su nueva contraseña',
        backToLogin: 'Volver al inicio de sesión',
        back: 'Paso anterior',
        identityVerified: 'Verificación de identidad exitosa',
        codeSent: 'Código de verificación enviado exitosamente',
        emailVerified: 'Verificación de correo electrónico exitosa',
        passwordResetSuccess: 'Restablecimiento de contraseña exitoso',
        captchaVerified: 'Verificación de código de verificación exitosa',
        successMessage: 'La contraseña del usuario {userName} ha sido restablecida exitosamente',
        form: {
          emailRequired: 'Por favor ingrese la dirección de correo electrónico',
          userNameLength: 'La longitud del nombre de usuario debe estar entre 3-20 caracteres'
        },
        error: {
          identityVerificationFailed: 'Error en verificación de identidad',
          sendCodeFailed: 'Error al enviar código de verificación',
          emailVerificationFailed: 'Error en verificación de correo electrónico',
          passwordResetFailed: 'Error al restablecer contraseña',
          captchaVerificationFailed: 'Error en verificación de código de verificación',
          emailMismatch: 'El nombre de usuario y la dirección de correo electrónico no coinciden',
          invalidCode: 'Código de verificación incorrecto o expirado',
          codeCooldown: 'Envío de código de verificación demasiado frecuente, intente más tarde'
        }
      },
      
      // Código de verificación
      captcha: {
        title: 'Verificación de seguridad',
        error: {
          initFailed: 'Error en inicialización del código de verificación'
        }
      },
      
      // SMS和OAuth登录功能已移除
      
      // Huella digital del dispositivo
      device: {
        getDeviceInfo: 'Información de huella digital del dispositivo obtenida',
        deviceFingerprintType: 'Tipo de huella digital del dispositivo',
        deviceFingerprintNull: '¿La huella digital del dispositivo es null?',
        deviceFingerprintUndefined: '¿La huella digital del dispositivo es undefined?',
        deviceFingerprintKeyCount: 'Número de claves de huella digital del dispositivo',
        deviceFingerprintKeyList: 'Lista de claves de huella digital del dispositivo',
        deviceFingerprintField: 'Campo loginFingerprint de huella digital del dispositivo',
        loginParamsDetail: 'Detalles de parámetros de inicio de sesión construidos',
        deviceFingerprintDetail: 'Información detallada de huella digital del dispositivo',
        empty: 'Vacío',
        backendHandled: 'Vacío (manejado por backend)',
        set: 'Establecido',
        initSuccess: 'Inicialización de información del dispositivo exitosa',
        initFailed: 'Error en inicialización de información del dispositivo',
        collectionComponent: {
          title: 'Huella digital del dispositivo',
          description: 'Recopilar información del dispositivo usando API Web nativa',
          collecting: 'Recopilando...',
          collectDeviceInfo: 'Recopilar información del dispositivo',
          clearInfo: 'Limpiar información',
          collectingInfo: 'Recopilando información del dispositivo...',
          deviceInfo: 'Información del dispositivo:',
          deviceId: 'ID del dispositivo:',
          deviceType: 'Tipo de dispositivo:',
          deviceFingerprint: 'Huella digital del dispositivo:',
          platform: 'Plataforma:',
          language: 'Idioma:',
          timezone: 'Zona horaria:',
          screenResolution: 'Resolución de pantalla:',
          colorDepth: 'Profundidad de color:',
          pixelRatio: 'Relación de píxeles del dispositivo:',
          cpuCores: 'Número de núcleos CPU:',
          deviceMemory: 'Memoria del dispositivo:',
          touchSupport: 'Soporte táctil:',
          os: 'Sistema operativo:',
          browser: 'Navegador:',
          vpnDetection: 'Detección VPN:',
          proxyDetection: 'Detección de proxy:',
          networkType: 'Tipo de red:',
          cookieSupport: 'Soporte de cookies:',
          notGenerated: 'No generado',
          notCollected: 'No recopilado',
          notDetected: 'No detectado',
          supported: 'Soportado',
          notSupported: 'No soportado',
          bits: 'bits',
          copyToClipboard: 'Copiar al portapapeles',
          copySuccess: 'Información del dispositivo copiada al portapapeles',
          copyFailed: 'Error al copiar, por favor copie manualmente',
          errorInfo: 'Información de error',
          startCollection: 'Iniciando recopilación de información del dispositivo...',
          collectionSuccess: 'Recopilación de información del dispositivo exitosa',
          collectionFailed: 'Error en recopilación de información del dispositivo',
          copyError: 'Error al copiar'
        }
      },
      
      // Validación
      validation: {
        usernamePasswordRequired: 'El nombre de usuario y la contraseña no pueden estar vacíos',
        deviceInfoRequired: 'La información de huella digital del dispositivo no puede estar vacía',
        deviceFingerprintRequired: 'La huella digital del dispositivo no puede estar vacía',
        userAgentRequired: 'El agente de usuario no puede estar vacío',
        loginTypeSourceRequired: 'El tipo de inicio de sesión y la fuente no pueden estar vacíos'
      },
      
      // Flujo de inicio de sesión
      loginFlow: {
        paramsSerialized: 'Longitud de parámetros serializados',
        paramsPreview: 'Vista previa de parámetros serializados',
        paramsTooLarge: 'Parámetros de inicio de sesión demasiado grandes, pueden causar problemas de rendimiento',
        serializationFailed: 'Error en serialización de parámetros',
        forceOfflineSuccess: 'Otros dispositivos expulsados, inicio de sesión exitoso',
        loginCancelled: 'Inicio de sesión cancelado',
        singleUserCheckFailed: 'Verificación de inicio de sesión de usuario único falló, continuando flujo de inicio de sesión normal',
        loginFailed: 'Error de inicio de sesión',
        signalRInit: 'Iniciando inicialización de conexión SignalR',
        signalRInitSuccess: 'Inicialización de conexión SignalR exitosa',
        signalRInitFailed: 'Error en inicialización de conexión SignalR',
        autoLogoutInit: 'Iniciando inicialización de función de cierre de sesión automático',
        autoLogoutInitSuccess: 'Inicialización de función de cierre de sesión automático exitosa',
        autoLogoutInitFailed: 'Error en inicialización de función de cierre de sesión automático',
        pageInitFailed: 'Error en inicialización de página de inicio de sesión',
        pageInitError: 'Error en inicialización de página, por favor actualice e intente nuevamente',
        checkLockStatusFailed: 'Error al verificar estado de bloqueo de cuenta',
        singleUserCheck: {
          title: 'Detección de inicio de sesión de usuario único',
          content: 'Se detectó que su cuenta ya está iniciada en otros dispositivos (número de dispositivos en línea: {onlineDeviceCount}).\n\n{reason}¿Desea expulsar otros dispositivos y continuar el inicio de sesión?',
          kickout: 'Expulsar otros dispositivos',
          cancel: 'Cancelar inicio de sesión'
        }
      },
      
      // Configuración
      config: {
        loading: 'Cargando configuración...',
        loadingLoginConfig: 'Cargando configuración de inicio de sesión, por favor espere...',
        captchaConfigSuccess: 'Configuración de código de verificación cargada exitosamente',
        captchaConfigFailed: 'Error al obtener configuración de código de verificación',
        captchaConfigError: 'Error al obtener configuración de código de verificación del backend',
        loginMethodConfigSuccess: 'Configuración de método de inicio de sesión cargada exitosamente',
        loginMethodConfigError: 'Error al obtener configuración de método de inicio de sesión del backend',
        loginMethodConfigFailed: 'Error al obtener configuración de método de inicio de sesión'
      },
      
      // Componente código de verificación
      captchaComponent: {
        refreshSuccess: 'Código de verificación actualizado',
        refreshFailed: 'Error al actualizar código de verificación',
        getFailed: 'Error al obtener código de verificación',
        verifySuccess: 'Verificación de código de verificación exitosa',
        invalidParams: 'Parámetros de código de verificación inválidos',
        statusUpdated: 'Estado de código de verificación actualizado',
        processError: 'Error al procesar callback de éxito del código de verificación',
        processFailed: 'Procesamiento de código de verificación falló, por favor intente nuevamente',
        verifyFailed: 'Error en verificación de código de verificación',
        errorProcessFailed: 'Error en procesamiento de error de código de verificación',
        initFailed: 'Error en inicialización de código de verificación',
        error: 'Error de código de verificación'
      },
      
      // Métodos de inicio de sesión eliminados
      
      // Errores
      error: {
        permanentlyLocked: 'Cuenta bloqueada permanentemente, por favor contacte al administrador',
        tooManyAttempts: 'Demasiados errores de inicio de sesión, cuenta bloqueada'
      }
    }
  }
}
