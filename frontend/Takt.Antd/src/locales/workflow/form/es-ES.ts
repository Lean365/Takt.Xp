export default {
  workflow: {
    definition: {
      title: 'Definición de Flujo de Trabajo',
      list: {
        title: 'Lista de Definiciones de Flujo de Trabajo',
        search: {
          name: 'Nombre del Flujo',
          key: 'Clave del Flujo',
          version: 'Versión',
          status: 'Estado'
        },
        table: {
          name: 'Nombre del Flujo',
          key: 'Clave del Flujo',
          version: 'Versión',
          status: 'Estado',
          createTime: 'Fecha de Creación',
          updateTime: 'Fecha de Actualización',
          actions: 'Acciones'
        },
        actions: {
          create: 'Crear',
          edit: 'Editar',
          delete: 'Eliminar',
          view: 'Ver',
          deploy: 'Desplegar',
          export: 'Exportar',
          import: 'Importar',
          refresh: 'Actualizar'
        },
        status: {
          draft: 'Borrador',
          deployed: 'Desplegado',
          disabled: 'Deshabilitado'
        }
      },
      form: {
        title: {
          create: 'Crear Definición de Flujo',
          edit: 'Editar Definición de Flujo'
        },
        fields: {
          name: 'Nombre del Flujo',
          key: 'Clave del Flujo',
          version: 'Versión',
          description: 'Descripción',
          status: 'Estado'
        },
        rules: {
          name: {
            required: 'Por favor ingrese el nombre del flujo',
            max: 'El nombre del flujo no puede exceder 50 caracteres'
          },
          key: {
            required: 'Por favor ingrese la clave del flujo',
            pattern: 'La clave del flujo solo puede contener letras, números y guiones bajos',
            max: 'La clave del flujo no puede exceder 50 caracteres'
          },
          version: {
            required: 'Por favor ingrese el número de versión',
            pattern: 'El formato del número de versión es incorrecto, debe ser formato x.y.z'
          }
        },
        buttons: {
          submit: 'Enviar',
          cancel: 'Cancelar'
        }
      },
      detail: {
        title: 'Detalle de Definición de Flujo',
        basic: {
          title: 'Información Básica',
          name: 'Nombre del Flujo',
          key: 'Clave del Flujo',
          version: 'Versión',
          description: 'Descripción',
          status: 'Estado',
          createTime: 'Fecha de Creación',
          updateTime: 'Fecha de Actualización'
        },
        actions: {
          edit: 'Editar',
          back: 'Volver'
        }
      }
    }
  }
} 