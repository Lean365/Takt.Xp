export default {
  workflow: {
    instance: {
      title: 'Instancia de Flujo de Trabajo',
      list: {
        title: 'Lista de Instancias de Flujo de Trabajo',
        search: {
          name: 'Nombre del Flujo de Trabajo',
          key: 'Clave del Flujo de Trabajo',
          version: 'Versión',
          status: 'Estado',
          startTime: 'Hora de Inicio',
          endTime: 'Hora de Finalización'
        },
        table: {
          name: 'Nombre del Flujo de Trabajo',
          key: 'Clave del Flujo de Trabajo',
          version: 'Versión',
          status: 'Estado',
          startTime: 'Hora de Inicio',
          endTime: 'Hora de Finalización',
          duration: 'Duración',
          actions: 'Acciones'
        },
        actions: {
          view: 'Ver',
          delete: 'Eliminar',
          terminate: 'Terminar',
          export: 'Exportar',
          import: 'Importar',
          refresh: 'Actualizar'
        },
        status: {
          running: 'En Ejecución',
          completed: 'Completado',
          terminated: 'Terminado',
          failed: 'Fallido'
        }
      },
      form: {
        title: {
          create: 'Crear Instancia de Flujo de Trabajo',
          import: 'Importar Instancia de Flujo de Trabajo'
        },
        fields: {
          workflowDefinitionId: 'Definición de Flujo de Trabajo',
          variables: 'Configuración de Variables'
        },
        rules: {
          workflowDefinitionId: {
            required: 'Por favor seleccione una definición de flujo de trabajo'
          }
        },
        buttons: {
          submit: 'Enviar',
          cancel: 'Cancelar'
        }
      },
      detail: {
        title: 'Detalle de Instancia de Flujo de Trabajo',
        basic: {
          title: 'Información Básica',
          name: 'Nombre del Flujo de Trabajo',
          key: 'Clave del Flujo de Trabajo',
          version: 'Versión',
          status: 'Estado',
          startTime: 'Hora de Inicio',
          endTime: 'Hora de Finalización',
          duration: 'Duración'
        },
        nodes: {
          title: 'Información del Nodo',
          name: 'Nombre del Nodo',
          type: 'Tipo de Nodo',
          status: 'Estado',
          startTime: 'Hora de Inicio',
          endTime: 'Hora de Finalización',
          duration: 'Duración',
          input: 'Entrada',
          output: 'Salida',
          error: 'Error'
        },
        actions: {
          back: 'Volver'
        }
      },
      terminate: {
        title: 'Terminar Instancia de Flujo de Trabajo',
        confirm: '¿Está seguro de que desea terminar esta instancia?',
        reason: 'Razón de Terminación',
        buttons: {
          submit: 'Enviar',
          cancel: 'Cancelar'
        }
      }
    }
  }
} 