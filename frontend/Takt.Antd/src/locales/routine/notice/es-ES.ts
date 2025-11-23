export default {
  routine: {
    notice: {
      // Información básica
      noticeId: 'ID del aviso',
      noticeType: 'Tipo de aviso',
      noticeTitle: 'Título del aviso',
      noticeContent: 'Contenido del aviso',
      noticeStatus: 'Estado del aviso',
      noticeParams: 'Parámetros del aviso',
      remark: 'Observación',
      createTime: 'Fecha de creación',
      updateTime: 'Fecha de actualización',

      // Botones de acción
      add: 'Agregar aviso',
      edit: 'Editar aviso',
      delete: 'Eliminar aviso',
      batchDelete: 'Eliminación por lotes',
      export: 'Exportar',
      import: 'Importar',
      downloadTemplate: 'Descargar plantilla',
      preview: 'Vista previa',
      send: 'Enviar',

      // Placeholders del formulario
      placeholder: {
        noticeId: 'Por favor ingrese el ID del aviso',
        noticeType: 'Por favor seleccione el tipo de aviso',
        noticeTitle: 'Por favor ingrese el título del aviso',
        noticeContent: 'Por favor ingrese el contenido del aviso',
        noticeStatus: 'Por favor seleccione el estado del aviso',
        noticeParams: 'Por favor ingrese los parámetros del aviso',
        remark: 'Por favor ingrese una observación',
        startTime: 'Fecha de inicio',
        endTime: 'Fecha de fin'
      },

      // Validación del formulario
      validation: {
        noticeId: {
          required: 'Por favor ingrese el ID del aviso',
          maxLength: 'El ID del aviso no puede exceder 100 caracteres'
        },
        noticeType: {
          required: 'Por favor seleccione el tipo de aviso',
          maxLength: 'El tipo de aviso no puede exceder 50 caracteres'
        },
        noticeTitle: {
          required: 'Por favor ingrese el título del aviso',
          maxLength: 'El título del aviso no puede exceder 200 caracteres'
        },
        noticeContent: {
          required: 'Por favor ingrese el contenido del aviso',
          maxLength: 'El contenido del aviso no puede exceder 4000 caracteres'
        },
        noticeStatus: {
          required: 'Por favor seleccione el estado del aviso'
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
        title: 'Detalles del aviso'
      }
    }
  }
} 