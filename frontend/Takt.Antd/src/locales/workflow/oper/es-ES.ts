export default {
  workflow: {
    history: {
      title: 'Historial de Flujo de Trabajo',
      list: {
        title: 'Lista de Historial de Flujo de Trabajo',
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
      detail: {
        title: 'Detalle del Historial de Flujo de Trabajo',
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
      }
    }
  }
} 