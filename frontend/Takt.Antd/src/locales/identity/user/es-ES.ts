export default {
  identity: {
    user: {
      title: 'Gestión de usuarios',
      profile: 'Información personal',
      changePasswordTitle: 'Cambiar contraseña',
      
      // Relacionado con contraseña
      password: {
        old: {
          label: 'Contraseña anterior',
          placeholder: 'Por favor ingrese la contraseña anterior',
          validation: {
            required: 'La contraseña anterior no puede estar vacía',
          }
        },
        new: {
          label: 'Nueva contraseña',
          placeholder: 'Por favor ingrese la nueva contraseña',
          validation: {
            required: 'La nueva contraseña no puede estar vacía',
          }
        },
        confirm: {
          label: 'Confirmar contraseña',
          placeholder: 'Por favor ingrese la confirmación de contraseña',
          validation: {
            required: 'La confirmación de contraseña no puede estar vacía',
          }
        },
      },
      tabs: {
        userInfo: 'Información del usuario',
        organization: 'Relaciones organizacionales',
        avatar: 'Avatar'
      },
      // Definiciones de campos de formulario
      fields: {
        userId: {
          label: 'ID de usuario'
        },
        userName: {
          label: 'Nombre de usuario',
          placeholder: 'Por favor ingrese el nombre de usuario',
          validation: {
            required: 'El nombre de usuario no puede estar vacío',
            format: 'Debe comenzar con letra minúscula, solo puede contener letras minúsculas, números y guiones, no puntos ni guiones bajos, longitud entre 4-50 caracteres'
          }
        },
        nickName: {
          label: 'Apodo',
          placeholder: 'Por favor ingrese el apodo',
          validation: {
            required: 'El apodo no puede estar vacío',
            format: '2-50 caracteres, permite chino, japonés, coreano, inglés, números, puntos ingleses y guiones, no permite guiones bajos ni espacios'
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
            format: 'La longitud del nombre en inglés debe estar entre 2-100 caracteres, debe comenzar con letra, solo puede contener letras inglesas, espacios, guiones y puntos ingleses'
          }
        },
        password: {
          label: 'Contraseña',
          placeholder: 'Por favor ingrese la contraseña',
          validation: {
            required: 'La contraseña no puede estar vacía',
            format: 'La longitud de la contraseña debe estar entre 6-20 caracteres, solo puede contener letras inglesas, números y caracteres especiales'
          }
        },
        userType: {
          label: 'Tipo de usuario',
          placeholder: 'Por favor seleccione el tipo de usuario',
          options: {
            admin: 'Administrador',
            user: 'Usuario normal'
          }
        },
        email: {
          label: 'Correo electrónico',
          placeholder: 'Por favor ingrese el correo electrónico',
          validation: {
            required: 'El correo electrónico no puede estar vacío',
            invalid: 'La longitud del correo electrónico debe estar entre 6-100 caracteres y en formato correcto'
          }
        },
        phoneNumber: {
          label: 'Número de teléfono',
          placeholder: 'Por favor ingrese el número de teléfono',
          validation: {
            required: 'El número de teléfono no puede estar vacío',
            invalid: 'Por favor ingrese el formato correcto de número de teléfono móvil o fijo'
          }
        },
        gender: {
          label: 'Género',
          placeholder: 'Por favor seleccione el género',
          options: {
            male: 'Masculino',
            female: 'Femenino',
            unknown: 'Desconocido'
          }
        },
        avatar: {
          label: 'Avatar',
          upload: 'Subir avatar',
          currentAvatar: 'Avatar actual',
          avatarUpload: 'Subida de avatar',
          uploadSuccess: 'Avatar subido exitosamente',
          uploadError: 'Error al subir avatar',
          uploading: 'Subiendo avatar...',
          onlyImage: '¡Solo se pueden subir archivos de imagen!',
          sizeLimit: '¡El tamaño de la imagen no puede exceder 5MB!'
        },
        status: {
          label: 'Estado',
          placeholder: 'Por favor seleccione el estado',
          options: {
            enabled: 'Habilitado',
            disabled: 'Deshabilitado'
          }
        },
        lastPasswordChangeTime: {
          label: 'Última modificación de contraseña'
        },
        lockEndTime: {
          label: 'Hora de fin de bloqueo'
        },
        lockReason: {
          label: 'Razón del bloqueo'
        },
        isLock: {
          label: 'Bloqueado'
        },
        errorLimit: {
          label: 'Límite de errores'
        },
        loginCount: {
          label: 'Número de inicios de sesión'
        },
        deptName: {
          label: 'Departamento',
          placeholder: 'Por favor seleccione el departamento',
          validation: {
            required: 'El departamento no puede estar vacío'
          }
        },
        role: {
          label: 'Rol',
          placeholder: 'Por favor seleccione el rol',
          validation: {
            required: 'El rol no puede estar vacío'
          }
        },
        post: {
          label: 'Cargo',
          placeholder: 'Por favor seleccione el cargo',
          validation: {
            required: 'El cargo no puede estar vacío'
          }
        },
        remark: {
          label: 'Observaciones',
          placeholder: 'Por favor ingrese las observaciones'
        }
      },

      // Botones de acción
      actions: {
        add: 'Agregar usuario',
        edit: 'Editar usuario',
        'delete': 'Eliminar usuario',
        resetPassword: 'Restablecer contraseña',
        export: 'Exportar usuarios'
      },

      // Mensajes de aviso
      messages: {
        confirmDelete: '¿Está seguro de que desea eliminar el usuario seleccionado?',
        confirmResetPassword: '¿Está seguro de que desea restablecer la contraseña del usuario seleccionado?',
        confirmToggleStatus: '¿Está seguro de que desea {action} este usuario?',
        deleteSuccess: 'Usuario eliminado exitosamente',
        deleteFailed: 'Error al eliminar usuario',
        saveSuccess: 'Información de usuario guardada exitosamente',
        saveFailed: 'Error al guardar información de usuario',
        resetPasswordSuccess: 'Contraseña restablecida exitosamente',
        resetPasswordFailed: 'Error al restablecer contraseña',
        importSuccess: 'Importación de usuario exitosa',
        importFailed: 'Error en importación de usuario',
        exportSuccess: 'Exportación de usuario exitosa',
        exportFailed: 'Error en exportación de usuario',
        toggleStatusSuccess: 'Estado cambiado exitosamente',
        toggleStatusFailed: 'Error al cambiar estado',
        cannotDeleteAdmin: '¡Los usuarios administrativos no pueden ser eliminados!',
        cannotEditAdmin: '¡Los usuarios administrativos no pueden ser editados!',
        cannotUpdateAdminStatus: '¡El estado de los usuarios administrativos no puede ser modificado!',
        cannotResetAdminPassword: '¡La contraseña de los usuarios administrativos no puede ser restablecida!',
        cannotUnlockAdmin: '¡Los usuarios administrativos no pueden ser desbloqueados!',
        cannotAllocateRole: '¡Los usuarios administrativos no pueden ser asignados a roles!',
        cannotAllocateDept: '¡Los usuarios administrativos no pueden ser asignados a departamentos!',
        cannotAllocatePost: '¡Los usuarios administrativos no pueden ser asignados a cargos!',
        statusUpdateSuccess: 'Estado actualizado exitosamente',
        statusUpdateFailed: 'Error al actualizar estado',
        lockStatusUpdateSuccess: 'Estado de bloqueo actualizado exitosamente',
        lockStatusUpdateFailed: 'Error al actualizar estado de bloqueo'
      },

      // Texto de visualización de tabla
      table: {
        notLocked: 'No bloqueado',
        none: 'Ninguno',
        queryParams: 'Parámetros de consulta',
        importResponseData: 'Datos de respuesta de importación',
        parsedData: 'Datos analizados',
        exportFailed: 'Error en exportación',
        downloadTemplateFailed: 'Error al descargar plantilla',
        rowClicked: 'Fila clickeada',
        toggleFullscreenState: 'Alternar estado de pantalla completa',
        getOptionsFailed: 'Error al obtener opciones',
        createUser: 'Crear usuario',
        updateUser: 'Actualizar usuario'
      },

      // Consejos de importación
      importTips: 'El nombre de la hoja de Excel debe ser "User"',

      // Pestañas
      tab: {
        basic: 'Información básica',
        profile: 'Perfil personal',
        role: 'Permisos de rol',
        dept: 'Departamento y cargo',
        other: 'Otra información',
        avatar: 'Configuración de avatar',
        loginLog: 'Registro de inicio de sesión',
        operateLog: 'Registro de operaciones',
        errorLog: 'Registro de excepciones',
        taskLog: 'Registro de tareas'
      },

      // Importación/Exportación
      import: {
        title: 'Importar usuarios',
        template: 'Descargar plantilla',
        success: 'Importación exitosa',
        failed: 'Error en importación',
        fileName: 'Datos de usuario'
      },
      export: {
        title: 'Exportar usuarios',
        fileName: 'Datos de usuario',
        success: 'Exportación exitosa',
        failed: 'Error en exportación'
      },
      template: {
        fileName: 'Plantilla de importación de usuario',
        downloadFailed: 'Error al descargar plantilla'
      },

      // Restablecer contraseña
      resetPwd: 'Restablecer contraseña',
      
      // Cambiar contraseña
      changePassword: {
        success: 'Contraseña cambiada exitosamente',
        failed: 'Error al cambiar contraseña, por favor intente de nuevo',
        changeFailed: 'Error al cambiar contraseña'
      },

      // Relacionado con asignación
      allocateDept: 'Asignar departamento',
      allocatePost: 'Asignar cargo',
      allocateRole: 'Asignar rol',
      
      roleAllocate: {
        unallocated: 'No asignado',
        allocated: 'Asignado',
        loadRoleListFailed: 'Error al cargar lista de roles',
        startLoadUserRoles: 'Iniciar carga de roles de usuario',
        userRolesApiResponse: 'Respuesta API de roles de usuario',
        setSelectedRoles: 'Establecer roles seleccionados',
        loadUserRolesFailed: 'Error al cargar roles de usuario'
      },
      
      postAllocate: {
        unallocated: 'No asignado',
        allocated: 'Asignado',
        loadPostListFailed: 'Error al cargar lista de cargos',
        loadUserPostsFailed: 'Error al cargar cargos de usuario'
      }
    }
  }
}
