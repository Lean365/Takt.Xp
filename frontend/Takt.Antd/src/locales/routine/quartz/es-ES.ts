export default {
  routine: {
    quartz: {
      // Información básica
      jobId: 'ID de tarea',
      jobName: 'Nombre de tarea',
      jobGroup: 'Grupo de tareas',
      jobClass: 'Clase de tarea',
      jobMethod: 'Método de tarea',
      jobParams: 'Parámetros de tarea',
      cronExpression: 'Expresión Cron',
      jobStatus: 'Estado de tarea',
      remark: 'Observaciones',
      createTime: 'Fecha de creación',
      updateTime: 'Fecha de actualización',

      // Botones de acción
      add: 'Agregar tarea',
      edit: 'Editar tarea',
      delete: 'Eliminar tarea',
      batchDelete: 'Eliminar en lote',
      export: 'Exportar',
      import: 'Importar',
      downloadTemplate: 'Descargar plantilla',
      preview: 'Vista previa',
      execute: 'Ejecutar',
      pause: 'Pausar',
      resume: 'Reanudar',

      // Marcadores de posición del formulario
      placeholder: {
        jobId: 'Ingrese el ID de la tarea',
        jobName: 'Ingrese el nombre de la tarea',
        jobGroup: 'Ingrese el grupo de tareas',
        jobClass: 'Ingrese la clase de tarea',
        jobMethod: 'Ingrese el método de tarea',
        jobParams: 'Ingrese los parámetros de tarea',
        cronExpression: 'Ingrese la expresión Cron',
        jobStatus: 'Seleccione el estado de la tarea',
        remark: 'Ingrese las observaciones',
        startTime: 'Hora de inicio',
        endTime: 'Hora de finalización'
      },

      // Validación del formulario
      validation: {
        jobId: {
          required: 'Ingrese el ID de la tarea',
          maxLength: 'El ID de la tarea no puede exceder 100 caracteres'
        },
        jobName: {
          required: 'Ingrese el nombre de la tarea',
          maxLength: 'El nombre de la tarea no puede exceder 50 caracteres'
        },
        jobGroup: {
          required: 'Ingrese el grupo de tareas',
          maxLength: 'El grupo de tareas no puede exceder 50 caracteres'
        },
        jobClass: {
          required: 'Ingrese la clase de tarea',
          maxLength: 'La clase de tarea no puede exceder 200 caracteres'
        },
        jobMethod: {
          required: 'Ingrese el método de tarea',
          maxLength: 'El método de tarea no puede exceder 100 caracteres'
        },
        cronExpression: {
          required: 'Ingrese la expresión Cron',
          maxLength: 'La expresión Cron no puede exceder 100 caracteres'
        },
        jobStatus: {
          required: 'Seleccione el estado de la tarea'
        }
      },

      // Resultados de operación
      message: {
        success: {
          add: 'Agregado exitosamente',
          edit: 'Editado exitosamente',
          delete: 'Eliminado exitosamente',
          batchDelete: 'Eliminado en lote exitosamente',
          export: 'Exportado exitosamente',
          import: 'Importado exitosamente',
          execute: 'Ejecutado exitosamente',
          pause: 'Pausado exitosamente',
          resume: 'Reanudado exitosamente'
        },
        failed: {
          add: 'Error al agregar',
          edit: 'Error al editar',
          delete: 'Error al eliminar',
          batchDelete: 'Error al eliminar en lote',
          export: 'Error al exportar',
          import: 'Error al importar',
          execute: 'Error al ejecutar',
          pause: 'Error al pausar',
          resume: 'Error al reanudar'
        }
      },

      // Página de detalles
      detail: {
        title: 'Detalles de la tarea'
      }
    }
  }
}