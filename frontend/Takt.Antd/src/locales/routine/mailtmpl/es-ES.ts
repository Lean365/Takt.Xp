export default {
  routine: {
    mailtmpl: {
      // Información Básica
      templateName: 'Nombre de la Plantilla',
      templateType: 'Tipo de Plantilla',
      templateSubject: 'Asunto de la Plantilla',
      templateContent: 'Contenido de la Plantilla',
      templateStatus: 'Estado de la Plantilla',
      templateParams: 'Parámetros de la Plantilla',
      remark: 'Observaciones',
      createTime: 'Fecha de Creación',
      updateTime: 'Fecha de Actualización',

      // Botones de Acción
      add: 'Agregar Plantilla',
      edit: 'Editar Plantilla',
      delete: 'Eliminar Plantilla',
      batchDelete: 'Eliminar en Lote',
      export: 'Exportar',
      import: 'Importar',
      downloadTemplate: 'Descargar Plantilla',
      preview: 'Vista Previa',
      send: 'Enviar',

      // Campos del Formulario
      placeholder: {
        templateName: 'Por favor ingrese el nombre de la plantilla',
        templateType: 'Por favor seleccione el tipo de plantilla',
        templateSubject: 'Por favor ingrese el asunto de la plantilla',
        templateContent: 'Por favor ingrese el contenido de la plantilla',
        templateStatus: 'Por favor seleccione el estado de la plantilla',
        templateParams: 'Por favor ingrese los parámetros de la plantilla',
        remark: 'Por favor ingrese las observaciones',
        startTime: 'Fecha de Inicio',
        endTime: 'Fecha de Fin'
      },

      // Validación del Formulario
      validation: {
        templateName: {
          required: 'Por favor ingrese el nombre de la plantilla',
          maxLength: 'El nombre de la plantilla no puede exceder 100 caracteres'
        },
        templateType: {
          required: 'Por favor seleccione el tipo de plantilla',
          maxLength: 'El tipo de plantilla no puede exceder 50 caracteres'
        },
        templateSubject: {
          required: 'Por favor ingrese el asunto de la plantilla',
          maxLength: 'El asunto de la plantilla no puede exceder 200 caracteres'
        },
        templateContent: {
          required: 'Por favor ingrese el contenido de la plantilla',
          maxLength: 'El contenido de la plantilla no puede exceder 4000 caracteres'
        },
        templateStatus: {
          required: 'Por favor seleccione el estado de la plantilla'
        }
      },

      // Resultados de Operaciones
      message: {
        success: {
          add: 'Agregado exitosamente',
          edit: 'Actualizado exitosamente',
          delete: 'Eliminado exitosamente',
          batchDelete: 'Eliminado en lote exitosamente',
          export: 'Exportado exitosamente',
          import: 'Importado exitosamente',
          send: 'Enviado exitosamente'
        },
        failed: {
          add: 'Error al agregar',
          edit: 'Error al actualizar',
          delete: 'Error al eliminar',
          batchDelete: 'Error al eliminar en lote',
          export: 'Error al exportar',
          import: 'Error al importar',
          send: 'Error al enviar'
        }
      },

      // Página de Detalles
      detail: {
        title: 'Detalles de la Plantilla de Correo'
      }
    }
  }
}
