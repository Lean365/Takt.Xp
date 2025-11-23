export default {
  identity: {
    menu: {
      title: 'Gestión de Menús',
      columns: {
        menuName: 'Nombre del Menú',
        transKey: 'Clave I18n',
        parentId: 'Menú Superior',
        orderNum: 'Orden',
        path: 'Ruta',
        component: 'Ruta del Componente',
        queryParams: 'Parámetros de Ruta',
        isExternal: 'Enlace Externo',
        isCache: 'Caché',
        menuType: 'Tipo de Menú',
        visible: 'Visibilidad',
        status: 'Estado',
        perms: 'Clave de Permiso',
        icon: 'Icono',
        id: 'ID',
        createBy: 'Creado Por',
        createTime: 'Fecha de Creación',
        updateBy: 'Actualizado Por',
        updateTime: 'Fecha de Actualización',
        deleteBy: 'Eliminado Por',
        deleteTime: 'Fecha de Eliminación',
        isDeleted: 'Eliminado',
        remark: 'Observación',
        action: 'Acción'
      },
      fields: {
        menuName: {
          label: 'Nombre del Menú',
          placeholder: 'Por favor ingrese el nombre del menú',
          validation: {
            required: 'Por favor ingrese el nombre del menú',
            length: 'La longitud del nombre debe estar entre 2 y 50 caracteres'
          }
        },
        transKey: {
          label: 'Clave I18n',
          placeholder: 'Por favor ingrese la clave I18n'
        },
        parentId: {
          label: 'Menú Superior',
          placeholder: 'Por favor seleccione el menú superior',
          root: 'Menú Raíz'
        },
        orderNum: {
          label: 'Orden',
          placeholder: 'Por favor ingrese el orden',
          validation: {
            required: 'Por favor ingrese el orden'
          }
        },
        path: {
          label: 'Ruta',
          placeholder: 'Por favor ingrese la ruta'
        },
        component: {
          label: 'Ruta del Componente',
          placeholder: 'Por favor ingrese la ruta del componente'
        },
        queryParams: {
          label: 'Parámetros de Ruta',
          placeholder: 'Por favor ingrese los parámetros de ruta'
        },
        isExternal: {
          label: 'Enlace Externo',
          placeholder: 'Por favor seleccione si es un enlace externo',
          options: {
            yes: 'Sí',
            no: 'No'
          }
        },
        isCache: {
          label: 'Caché',
          placeholder: 'Por favor seleccione si se almacena en caché',
          options: {
            yes: 'Sí',
            no: 'No'
          }
        },
        menuType: {
          label: 'Tipo de Menú',
          options: {
            directory: 'Directorio',
            menu: 'Menú',
            button: 'Botón'
          },
          validation: {
            required: 'Por favor seleccione el tipo de menú'
          }
        },
        visible: {
          label: 'Visibilidad',
          placeholder: 'Por favor seleccione la visibilidad',
          options: {
            show: 'Mostrar',
            hide: 'Ocultar'
          }
        },
        status: {
          label: 'Estado',
          placeholder: 'Por favor seleccione el estado',
          options: {
            normal: 'Normal',
            disabled: 'Deshabilitado'
          }
        },
        perms: {
          label: 'Clave de Permiso',
          placeholder: 'Por favor ingrese la clave de permiso'
        },
        icon: {
          label: 'Icono del Menú',
          placeholder: 'Por favor ingrese el icono del menú'
        },
        }
      },
      dialog: {
        create: 'Agregar Menú',
        update: 'Editar Menú',
        delete: 'Eliminar Menú'
      },
      operation: {
        add: {
          title: 'Agregar Menú',
          success: 'Agregado exitosamente',
          failed: 'Error al agregar'
        },
        edit: {
          title: 'Editar Menú',
          success: 'Editado exitosamente',
          failed: 'Error al editar'
        },
        delete: {
          title: 'Eliminar Menú',
          confirm: '¿Está seguro de que desea eliminar este menú?',
          success: 'Eliminado exitosamente',
          failed: 'Error al eliminar'
        }
      }
    }
  }
