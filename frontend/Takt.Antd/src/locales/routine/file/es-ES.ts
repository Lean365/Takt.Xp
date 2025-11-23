export default {
  routine: {
    file: {
      fileName: 'Nombre del archivo',
      fileType: 'Tipo de archivo',
      fileSize: 'Tamaño del archivo',
      filePath: 'Ruta del archivo',
      fileUrl: 'URL del archivo',
      fileMd5: 'Archivo MD5',
      fileOriginalName: 'Nombre original del archivo',
      fileExtension: 'Extensión del archivo',
      fileStorageType: 'Tipo de almacenamiento',
      fileStorageLocation: 'Ubicación de almacenamiento',
      status: 'Estado',
      remark: 'Observación',
      createTime: 'Fecha de creación',
      updateTime: 'Fecha de actualización',
      operation: 'Operación',
      placeholder: {
        fileName: 'Por favor ingrese el nombre del archivo',
        fileType: 'Por favor ingrese el tipo de archivo',
        fileSize: 'Por favor ingrese el tamaño del archivo',
        status: 'Por favor seleccione el estado',
        remark: 'Por favor ingrese una observación'
      },
      validation: {
        fileName: {
          required: 'Por favor ingrese el nombre del archivo',
          maxLength: 'El nombre del archivo no puede exceder los 100 caracteres'
        },
        fileType: {
          required: 'Por favor ingrese el tipo de archivo',
          maxLength: 'El tipo de archivo no puede exceder los 50 caracteres'
        },
        status: {
          required: 'Por favor seleccione el estado'
        },
        file: {
          required: 'Por favor seleccione un archivo para subir',
          size: '¡El tamaño del archivo no puede exceder los 2MB!'
        }
      },
      message: {
        addSuccess: 'Archivo agregado exitosamente',
        editSuccess: 'Archivo actualizado exitosamente',
        deleteSuccess: 'Archivo eliminado exitosamente',
        deleteConfirm: '¿Está seguro de que desea eliminar el archivo "{name}"?',
        exportSuccess: 'Archivo exportado exitosamente',
        importSuccess: 'Archivo importado exitosamente',
        uploadSuccess: 'Archivo subido exitosamente',
        downloadSuccess: 'Archivo descargado exitosamente'
      },
      detail: {
        title: 'Detalles del archivo'
      },
      upload: {
        success: 'Archivo subido exitosamente',
        failed: 'Error al subir el archivo',
        fileEmpty: 'Por favor seleccione un archivo para subir',
        fileType: 'Tipo de archivo no soportado',
        fileSize: 'El tamaño del archivo excede el límite',
        fileExists: 'El archivo ya existe',
        fileNameRequired: 'El nombre del archivo no puede estar vacío'
      }
    }
  }
} 