export default {
  routine: {
    mail: {
      // Información básica
      mailId: 'ID del correo',
      mailType: 'Tipo de correo',
      mailSubject: 'Asunto del correo',
      mailContent: 'Contenido del correo',
      mailStatus: 'Estado del correo',
      mailParams: 'Parámetros del correo',
      remark: 'Observación',
      createTime: 'Fecha de creación',
      updateTime: 'Fecha de actualización',

      // Botones de acción
      add: 'Agregar correo',
      edit: 'Editar correo',
      delete: 'Eliminar correo',
      batchDelete: 'Eliminación por lotes',
      export: 'Exportar',
      import: 'Importar',
      downloadTemplate: 'Descargar plantilla',
      preview: 'Vista previa',
      send: 'Enviar',

      // Placeholders del formulario
      placeholder: {
        mailId: 'Por favor ingrese el ID del correo',
        mailType: 'Por favor seleccione el tipo de correo',
        mailSubject: 'Por favor ingrese el asunto del correo',
        mailContent: 'Por favor ingrese el contenido del correo',
        mailStatus: 'Por favor seleccione el estado del correo',
        mailParams: 'Por favor ingrese los parámetros del correo',
        remark: 'Por favor ingrese una observación',
        startTime: 'Fecha de inicio',
        endTime: 'Fecha de fin'
      },

      // Validación del formulario
      validation: {
        mailId: {
          required: 'Por favor ingrese el ID del correo',
          maxLength: 'El ID del correo no puede exceder 100 caracteres'
        },
        mailType: {
          required: 'Por favor seleccione el tipo de correo',
          maxLength: 'El tipo de correo no puede exceder 50 caracteres'
        },
        mailSubject: {
          required: 'Por favor ingrese el asunto del correo',
          maxLength: 'El asunto del correo no puede exceder 200 caracteres'
        },
        mailContent: {
          required: 'Por favor ingrese el contenido del correo',
          maxLength: 'El contenido del correo no puede exceder 4000 caracteres'
        },
        mailStatus: {
          required: 'Por favor seleccione el estado del correo'
        }
      },

      // Resultados de operación
      message: {
        success: {
          add: 'Agregado exitosamente',
          edit: 'Editado exitosamente',
          delete: 'Eliminado exitosamente',
          batchDelete: 'Eliminación por lotes exitosa',
          export: 'Exportación exitosa',
          import: 'Importación exitosa',
          send: 'Enviado exitosamente'
        },
        failed: {
          add: 'Error al agregar',
          edit: 'Error al editar',
          delete: 'Error al eliminar',
          batchDelete: 'Error en la eliminación por lotes',
          export: 'Error en la exportación',
          import: 'Error en la importación',
          send: 'Error al enviar'
        }
      },

      // Página de detalles
      detail: {
        title: 'Detalles del correo'
      }
    }
  }
} 