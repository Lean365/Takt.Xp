export default {
  common: {
    // ==================== Información del Sistema ====================
    system: {
      title: 'Plataforma Black Ice',
      slogan: 'Sistema de Gestión Empresarial Profesional, Eficiente y Seguro',
      description: 'Sistema de Gestión Empresarial Moderno basado en .NET 8 y Vue 3',
      copyright: '© 2024Takt(Claude AI). Todos los derechos reservados.'
    },

    // ==================== Mensajes de Error ====================
    error: {
      clientError: 'Error en la solicitud del cliente, verifique los parámetros de la solicitud',
      systemRestart: 'El sistema está en mantenimiento, por favor inicie sesión más tarde',
      network: 'Error de conexión de red, por favor verifique su configuración de red',
      unauthorized: 'Acceso no autorizado, por favor inicie sesión nuevamente',
      forbidden: 'Acceso prohibido',
      notFound: 'El recurso solicitado no existe',
      badRequest: 'Parámetros de solicitud inválidos',
      serverError: 'Error interno del servidor',
      serviceUnavailable: 'Servicio temporalmente no disponible',
      badGateway: 'Puerta de enlace incorrecta',
      gatewayTimeout: 'Tiempo de espera de puerta de enlace',
      unknown: 'Error desconocido'
    },

    // ==================== Operaciones Básicas ====================
    // Títulos Básicos
    title: {
      list: 'Lista',
      detail: 'Detalles',
      create: 'Agregar Nuevo',
      edit: 'Editar',
      preview: 'Vista previa'
    },

    // ==================== Definiciones de Estado ====================
    status: {
      // Estado Básico
      base: {
        label: 'Estado',
        normal: 'Normal',
        disabled: 'Deshabilitado',
        placeholder: 'Por favor seleccione el estado'
      },

      // Estado Sí/No
      yesNo: {
        all: 'Todos',
        yes: 'Sí',
        no: 'No'
      },

      // Estado de Visibilidad
      visible: {
        show: 'Mostrar',
        hide: 'Ocultar'
      },

      // Estado de Caché
      cache: {
        enabled: 'Habilitado',
        disabled: 'Deshabilitado'
      },

      // Estado en Línea
      online: {
        online: 'En línea',
        offline: 'Fuera de línea'
      },

      // Estado de Proceso
      process: {
        pending: 'Pendiente',
        processing: 'Procesando',
        completed: 'Completado',
        failed: 'Fallido'
      },

      // Estado de Verificación
      verify: {
        unverified: 'No verificado',
        verified: 'Verificado',
        failed: 'Verificación fallida'
      },

      // Estado de Bloqueo
      lock: {
        locked: 'Bloqueado',
        unlocked: 'Desbloqueado'
      },

      // Estado de Publicación
      publish: {
        draft: 'Borrador',
        published: 'Publicado',
        unpublished: 'No publicado'
      },

      // Estado de Sincronización
      sync: {
        synced: 'Sincronizado',
        unsynced: 'No sincronizado',
        syncing: 'Sincronizando'
      }
    },

    // ==================== Operaciones de Formulario ====================
    form: {
      required: 'Requerido',
      optional: 'Opcional',
      invalid: 'Inválido',
      // Marcadores de Posición del Formulario
      placeholder: {
        select: 'Por favor seleccione',
        input: 'Por favor ingrese',
        date: 'Por favor seleccione fecha',
        time: 'Por favor seleccione hora'
      },
      // Formulario de Usuario
      user: {
        // Información Básica
        userId: {
          label: 'ID de Usuario',
          placeholder: 'Por favor ingrese ID de usuario'
        },
        userName: {
          label: 'Nombre de Usuario',
          placeholder: 'Por favor ingrese nombre de usuario'
        },
        nickName: {
          label: 'Apodo',
          placeholder: 'Por favor ingrese apodo'
        },
        realName: {
          label: 'Nombre Real',
          placeholder: 'Por favor ingrese nombre real'
        },
        englishName: {
          label: 'Nombre en Inglés',
          placeholder: 'Por favor ingrese nombre en inglés'
        },
        password: {
          label: 'Contraseña',
          placeholder: 'Por favor ingrese contraseña'
        },
        confirmPassword: {
          label: 'Confirmar Contraseña',
          placeholder: 'Por favor ingrese contraseña nuevamente'
        },
        
        // Información Personal
        avatar: {
          label: 'Avatar',
          placeholder: 'Por favor suba avatar'
        },
        gender: {
          label: 'Género',
          placeholder: 'Por favor seleccione género',
          options: {
            male: 'Masculino',
            female: 'Femenino',
            other: 'Otro'
          }
        },
        birthday: {
          label: 'Fecha de Nacimiento',
          placeholder: 'Por favor seleccione fecha de nacimiento'
        },
        
        // Información de Contacto
        email: {
          label: 'Correo Electrónico',
          placeholder: 'Por favor ingrese correo electrónico'
        },
        phoneNumber: {
          label: 'Teléfono Móvil',
          placeholder: 'Por favor ingrese teléfono móvil'
        },
        telephone: {
          label: 'Teléfono',
          placeholder: 'Por favor ingrese teléfono'
        },
        
        // Información Organizacional
        deptId: {
          label: 'Departamento',
          placeholder: 'Por favor seleccione departamento'
        },
        roleId: {
          label: 'Rol',
          placeholder: 'Por favor seleccione rol'
        },
        postId: {
          label: 'Cargo',
          placeholder: 'Por favor seleccione cargo'
        },
        
        // Información de Cuenta
        userType: {
          label: 'Tipo de Usuario',
          placeholder: 'Por favor seleccione tipo de usuario',
          options: {
            system: 'Usuario del Sistema',
            normal: 'Usuario Normal'
          }
        },
        status: {
          label: 'Estado',
          placeholder: 'Por favor seleccione estado'
        },
        loginIp: {
          label: 'Última IP de Inicio de Sesión',
          placeholder: 'Última IP de Inicio de Sesión'
        },
        loginDate: {
          label: 'Último Tiempo de Inicio de Sesión',
          placeholder: 'Último Tiempo de Inicio de Sesión'
        },
        
        // Otra Información
        remark: {
          label: 'Observación',
          placeholder: 'Por favor ingrese observación'
        },
        sort: {
          label: 'Orden',
          placeholder: 'Por favor ingrese número de orden'
        }
      },
      // Información de Observación
      remark: {
        label: 'Observación',
        placeholder: 'Por favor ingrese observación'
      }
    },

    // ==================== Operaciones de Tabla ====================
    table: {
      header: {
        operation: 'Operación'
      },
      config: {
        density: {
          default: 'Predeterminado',
          middle: 'Medio',
          small: 'Compacto'
        },
        columnSetting: 'Configuración de Columnas'
      },
      pagination: {
        total: 'Total {total} elementos',
        current: 'Página {current}',
        pageSize: '{pageSize} elementos por página',
        jump: 'Ir a'
      },
      empty: 'Sin datos',
      loading: 'Cargando...',
      selectAll: 'Seleccionar Todo',
      selected: '{total} elementos seleccionados'
    },

    // ==================== Operaciones de Tiempo ====================
    datetime: {
      date: 'Fecha',
      time: 'Hora',
      year: 'Año',
      month: 'Mes',
      day: 'Día',
      hour: 'Hora',
      minute: 'Minuto',
      second: 'Segundo',
      startDate: 'Fecha de Inicio',
      endDate: 'Fecha de Fin',
      startTime: 'Hora de Inicio',
      endTime: 'Hora de Fin',
      createTime: 'Tiempo de Creación',
      updateTime: 'Tiempo de Actualización',
      formatError: 'Error al formatear tiempo',
      relativeTimeFormatError: 'Error al formatear tiempo relativo',
      parseError: 'Error al analizar fecha',
      rangeSeparator: ' a '
    },

    // ==================== Operaciones de Archivo ====================
    // Importar/Exportar
    import: {
      title: 'Importar Datos',
      file: 'Seleccionar Archivo',
      select: 'Seleccionar Archivo',
      template: 'Plantilla',
      download: 'Descargar Plantilla',
      note: 'Instrucciones',
      tips: 'Por favor siga estrictamente el formato de la plantilla de importación, de lo contrario la importación puede fallar',
      format: '¡Solo se admiten archivos Excel!',
      size: '¡El tamaño del archivo no puede exceder {size}MB!',
      total: 'Registros Totales',
      success: 'Cantidad de Éxitos',
      failed: 'Cantidad de Fallos',
      message: 'Razón del Fallo',
      dragText: 'Haga clic o arrastre el archivo a esta área para cargar',
      dragHint: 'Soporta archivos Excel en formato .xlsx',
      sheetName: 'Por favor asegúrese de que el nombre de la hoja del archivo Excel sea: {sheetName}',
      allSuccess: '¡Importación exitosa {count} registros, todos exitosos!',
      partialSuccess: 'Importación exitosa {success} registros, fallidos {fail} registros',
      allFailed: 'Toda la importación falló, total {count} registros',
      noData: 'No se leyeron datos',
      empty: 'El archivo está vacío, no se puede cargar',
      importFailed: 'La importación falló',
      templateFileName: 'Import_Template_{time}.xlsx',
      limits: {
        title: 'Límites de Importación',
        fileCount: 'Límite de archivos: {count} archivo',
        fileSize: 'Límite de tamaño de archivo: {size}MB',
        recordCount: 'Límite de registros: {count} registros',
        fileFormat: 'Formato de archivo: Solo se admite formato .xlsx'
      },
      recordLimit: 'La cantidad de registros para importar ({actual} registros) excede el límite ({max} registros), por favor importe en lotes'
    },

    // Carga
    upload: {
      text: 'Arrastre el archivo aquí o haga clic para cargar',
      picture: 'Haga clic para cargar imagen',
      file: 'Haga clic para cargar archivo',
      icon: 'Haga clic para cargar icono',
      limit: {
        size: 'El tamaño del archivo no puede exceder {size}',
        type: 'Solo se admite formato {type}'
      }
    },

    // ==================== Botones de Función ====================
    actions: {
      // === Botones de Operación Básica ===
      add: 'Agregar',           // @btn-add-color
      edit: 'Editar',          // @btn-edit-color
      delete: 'Eliminar',      // @btn-delete-color
      batchDelete: 'Eliminar por Lotes', // @btn-batch-delete-color
      view: 'Ver',             // @btn-view-color
      clear: 'Limpiar',        // @btn-clear-color
      forceOffline: 'Forzar Fuera de Línea', // @btn-force-offline-color
      onlineStatus: 'Estado en Línea', // @btn-online-status-color
      loginHistory: 'Historial de Inicio de Sesión', // @btn-login-history-color
      sendMail: 'Enviar Correo', // @btn-send-mail-color
      viewMail: 'Ver Correo', // @btn-view-mail-color
      mailTemplate: 'Plantilla de Correo', // @btn-mail-template-color
      sendNotification: 'Enviar Notificación', // @btn-send-notification-color
      viewNotification: 'Ver Notificación', // @btn-view-notification-color
      notificationSetting: 'Configuración de Notificación', // @btn-notification-setting-color
      sendMessage: 'Enviar Mensaje', // @btn-send-message-color
      viewMessage: 'Ver Mensaje', // @btn-view-message-color
      messageSetting: 'Configuración de Mensaje', // @btn-message-setting-color

      // === Botones de Operación de Datos ===
      import: 'Importar',      // @btn-import-color
      export: 'Exportar',      // @btn-export-color
      template: 'Plantilla',   // @btn-template-color
      preview: 'Vista Previa', // @btn-preview-color
      download: 'Descargar',   // @btn-download-color
      batchImport: 'Importar por Lotes', // @btn-batch-import-color
      batchExport: 'Exportar por Lotes', // @btn-batch-export-color
      batchPrint: 'Imprimir por Lotes', // @btn-batch-print-color
      batchEdit: 'Editar por Lotes',  // @btn-batch-edit-color
      batchUpdate: 'Actualizar por Lotes', // @btn-batch-update-color

      // === Botones de Operación de Estado ===
      logging: 'Auditar',      // @btn-audit-color
      revoke: 'Revocar',     // @btn-revoke-color
      stop: 'Detener',       // @btn-stop-color
      run: 'Ejecutar',       // @btn-run-color
      force: 'Forzar',       // @btn-forced-color
      start: 'Iniciar',      // @btn-start-color
      suspend: 'Suspender',  // @btn-suspend-color
      resume: 'Reanudar',    // @btn-resume-color
      submit: 'Enviar',      // @btn-submit-color
      withdraw: 'Retirar',   // @btn-withdraw-color
      terminate: 'Terminar',  // @btn-terminate-color

      // === Botones de Función del Sistema ===
      generate: 'Generar',     // @btn-generate-color
      refresh: 'Actualizar',   // @btn-refresh-color
      info: 'Información',     // @btn-info-color
      log: 'Registro',         // @btn-log-color
      chat: 'Mensaje',         // @btn-chat-color
      copy: 'Copiar',          // @btn-copy-color
      execute: 'Ejecutar',     // @btn-execute-color
      resetPwd: 'Restablecer Contraseña', // @btn-reset-pwd-color
      open: 'Abrir',           // @btn-open-color
      close: 'Cerrar',         // @btn-close-color
      more: 'Más',             // @btn-more-color
      density: 'Densidad',     // @btn-density-color
      columnSetting: 'Configuración de Columnas', // @btn-column-setting-color

      // === Botones de Función Extendida ===
      search: 'Buscar',        // @btn-search-color
      filter: 'Filtrar',       // @btn-filter-color
      sort: 'Ordenar',         // @btn-sort-color
      config: 'Configurar',    // @btn-config-color
      save: 'Guardar',         // @btn-save-color
      cancel: 'Cancelar',      // @btn-cancel-color
      upload: 'Cargar',        // @btn-upload-color
      print: 'Imprimir',       // @btn-print-color
      help: 'Ayuda',           // @btn-help-color
      share: 'Compartir',      // @btn-share-color
      lock: 'Bloquear',        // @btn-lock-color
      sync: 'Sincronizar',     // @btn-sync-color
      expand: 'Expandir',      // @btn-expand-color
      collapse: 'Contraer',    // @btn-collapse-color
      approve: 'Aprobar',      // @btn-approve-color
      reject: 'Rechazar',      // @btn-reject-color
      comment: 'Comentar',     // @btn-comment-color
      attach: 'Adjuntar',      // @btn-attach-color

      // === Botones de Soporte de Idioma ===
      translate: 'Traducir',   // @btn-translate-color
      langSwitch: 'Cambiar Idioma', // @btn-lang-switch-color
      dict: 'Diccionario',     // @btn-dict-color

      // === Botones de Análisis de Datos ===
      analyze: 'Analizar',     // @btn-analyze-color
      chart: 'Gráfico',        // @btn-chart-color
      report: 'Informe',       // @btn-report-color
      dashboard: 'Panel de Control', // @btn-dashboard-color
      statistics: 'Estadísticas', // @btn-statistics-color
      forecast: 'Pronóstico',  // @btn-forecast-color
      compare: 'Comparar',     // @btn-compare-color

      // === Botones de Flujo de Trabajo ===
      startFlow: 'Iniciar Flujo', // @btn-start-flow-color
      endFlow: 'Finalizar Flujo', // @btn-end-flow-color
      suspendFlow: 'Suspender Flujo', // @btn-suspend-flow-color
      resumeFlow: 'Reanudar Flujo', // @btn-resume-flow-color
      transfer: 'Transferir',    // @btn-transfer-color
      delegate: 'Delegar',       // @btn-delegate-color
      notify: 'Notificar',       // @btn-notify-color
      urge: 'Urgir',             // @btn-urge-color
      sign: 'Firmar',            // @btn-sign-color
      countersign: 'Refrendar',  // @btn-countersign-color

      // === Botones Específicos para Móviles ===
      scan: 'Escanear',         // @btn-scan-color
      location: 'Ubicación',    // @btn-location-color
      call: 'Llamar',           // @btn-call-color
      photo: 'Tomar Foto',      // @btn-photo-color
      voice: 'Voz',             // @btn-voice-color
      faceId: 'ID Facial',      // @btn-face-id-color
      fingerPrint: 'Huella Digital', // @btn-finger-print-color

      // === Botones de Colaboración Social ===
      follow: 'Seguir',         // @btn-follow-color
      collect: 'Recopilar',     // @btn-collect-color
      like: 'Me gusta',         // @btn-like-color
      forward: 'Reenviar',      // @btn-forward-color
      at: '@',                  // @btn-at-color
      group: 'Grupo',           // @btn-group-color
      team: 'Equipo',           // @btn-team-color

      // === Botones de Autenticación de Seguridad ===
      verifyCode: 'Código de Verificación', // @btn-verify-code-color
      bind: 'Vincular',         // @btn-bind-color
      unbind: 'Desvincular',    // @btn-unbind-color
      authorize: 'Autorizar',   // @btn-authorize-color
      deauthorize: 'Desautorizar', // @btn-deauthorize-color

      // === Botones de Función Avanzada ===
      version: 'Versión',       // @btn-version-color
      history: 'Historial',     // @btn-history-color
      restore: 'Restaurar',     // @btn-restore-color
      archive: 'Archivar',      // @btn-archive-color
      unarchive: 'Desarchivar', // @btn-unarchive-color
      merge: 'Combinar',        // @btn-merge-color
      split: 'Dividir',         // @btn-split-color

      // === Botones de Gestión del Sistema ===
      backup: 'Respaldar',      // @btn-backup-color
      restoreSys: 'Restaurar Sistema', // @btn-restore-sys-color
      clean: 'Limpiar',         // @btn-clean-color
      optimize: 'Optimizar',    // @btn-optimize-color
      monitor: 'Monitorear',    // @btn-monitor-color
      diagnose: 'Diagnosticar', // @btn-diagnose-color
      maintain: 'Mantener'      // @btn-maintain-color
    },

    // ==================== Resultados y Mensajes ====================
    // Estado del Resultado
    result: {
      success: 'Operación exitosa',
      failed: 'Operación fallida',
      warning: 'Advertencia',
      info: 'Información',
      error: 'Error'
    },

    // Mensajes de Sugerencia
    message: {
      loading: 'Procesando...',
      saving: 'Guardando...',
      submitting: 'Enviando...',
      deleting: 'Eliminando...',
      operationSuccess: 'Operación exitosa',
      operationFailed: 'Operación fallida',
      deleteConfirm: '¿Está seguro de eliminar?',
      deleteSuccess: 'Eliminación exitosa',
      deleteFailed: 'Eliminación fallida',
      createSuccess: 'Agregado exitoso',
      createFailed: 'Agregado fallido',
      updateSuccess: 'Actualización exitosa',
      updateFailed: 'Error en la actualización',
      networkError: 'Error de conexión de red, por favor verifique la red',
      systemError: 'Error del sistema',
      timeout: 'Tiempo de espera de solicitud',
      invalidResponse: 'Formato de datos de respuesta inválido',
      backendNotStarted: 'Servicio backend no iniciado, por favor verifique el estado del servicio',
      invalidRequest: 'Parámetros de solicitud inválidos',
      unauthorized: 'No autorizado, por favor inicie sesión nuevamente',
      forbidden: 'Acceso prohibido',
      notFound: 'El recurso solicitado no existe',
      serverError: 'Error interno del servidor',
      httpError: {
        400: 'Parámetros de solicitud inválidos',
        401: 'No autorizado, por favor inicie sesión nuevamente',
        403: 'Acceso prohibido',
        404: 'El recurso solicitado no existe',
        500: 'Error interno del servidor',
        502: 'Puerta de enlace incorrecta',
        503: 'Servicio no disponible',
        504: 'Tiempo de espera de puerta de enlace'
      },
      loadFailed: 'Carga fallida',
      forceOfflineConfirm: '¿Está seguro de que desea forzar a este usuario fuera de línea?',
      forceOfflineSuccess: 'Forzado fuera de línea exitoso',
      forceOfflineFailed: 'Error al forzar fuera de línea'
    },

    // ==================== Texto de Componentes ====================
    // Pestañas
    tab: {
      // === Información Básica ===
      basic: 'Información Básica',
      profile: 'Perfil',
      avatar: 'Configuración de Avatar',
      password: 'Configuración de Contraseña',
      security: 'Configuración de Seguridad',

      // === Generación de Código ===
      codegen: 'Generación de Código',
      codegenBasic: 'Configuración de Generación',
      codegenField: 'Configuración de Campos',
      codegenPreview: 'Vista Previa de Generación',
      codegenTemplate: 'Configuración de Plantilla',
      codegenImport: 'Configuración de Importación',
      
      // === Flujo de Trabajo ===
      workflow: 'Flujo de Trabajo',
      flowDesign: 'Diseño de Flujo',
      flowDeploy: 'Despliegue de Flujo',
      flowInstance: 'Instancia de Flujo',
      flowTask: 'Gestión de Tareas',
      flowForm: 'Configuración de Formulario',
      flowNotify: 'Notificación de Mensajes',
      
      // === Gestión del Sistema ===
      permission: 'Permiso de Datos',
      log: 'Registro de Operaciones',
      dept: 'Departamento y Cargo',
      role: 'Rol y Permiso',
      config: 'Configuración de Parámetros',
      task: 'Tarea Programada',
      monitor: 'Monitor del Sistema'
    },

    // Texto de Botones
    button: {
      submit: 'Enviar',
      confirm: 'Confirmar',
      ok: 'Aceptar',
      cancel: 'Cancelar',
      close: 'Cerrar',
      reset: 'Reiniciar',
      clear: 'Limpiar',
      save: 'Guardar',
      delete: 'Eliminar'
    }
  },

  tabs: {
    closeOthers: 'Cerrar Otros',
    closeRight: 'Cerrar Derecha',
    closeAll: 'Cerrar Todo',
    // Mensajes de límite de pestañas
    maxTabsReached: 'Límite de pestañas alcanzado ({count}), por favor cierre algunas pestañas antes de abrir nuevas páginas. Puede usar el menú desplegable en el lado derecho de las pestañas para cerrar rápidamente múltiples pestañas.',
    tabsTruncated: 'Se detectaron {total} pestañas, automáticamente se mantuvieron las primeras {count} pestañas'
  },

  // ==================== Componente Selector ====================
  select: {
    loadMore: 'Cargar Más',
    loading: 'Cargando...',
    notFound: 'Sin datos',
    selected: '{count} elementos seleccionados',
    selectedTotal: 'Total {total} elementos',
    clear: 'Limpiar',
    search: 'Buscar',
    all: 'Todo',
    // Mensajes de Error
    loadError: 'Error al cargar datos',
    searchError: 'Búsqueda fallida',
    networkError: 'Error de conexión de red',
    serverError: 'Error del servidor',
    unknownError: 'Error desconocido'
  }
}


