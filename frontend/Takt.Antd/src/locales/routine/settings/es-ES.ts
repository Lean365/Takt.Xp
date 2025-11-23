export default {
  admin: {
    config: {
      // 基本信息
      name: 'Nombre',
      key: 'Clave',
      value: 'Valor',
      builtin: 'Integrado',
      order: 'Orden',
      remark: 'Observación',
      status: 'Estado',
      createTime: 'Tiempo de creación',
      updateTime: 'Tiempo de actualización',
      createBy: 'Creado por',
      updateBy: 'Actualizado por',
      operation: 'Operación',

      // 提示信息
      placeholder: {
        name: 'Por favor ingrese el nombre de la configuración',
        key: 'Por favor ingrese la clave de la configuración',
        value: 'Por favor ingrese el valor de la configuración',
        builtin: 'Por favor seleccione si está integrado',
        order: 'Por favor ingrese el orden de visualización',
        remark: 'Por favor ingrese la observación',
        status: 'Por favor seleccione el estado'
      },

      // 验证信息
      validation: {
        name: {
          required: 'El nombre de la configuración es obligatorio',
          maxLength: 'El nombre de la configuración no puede exceder los 100 caracteres'
        },
        key: {
          required: 'La clave de la configuración es obligatoria',
          maxLength: 'La clave de la configuración no puede exceder los 100 caracteres',
          pattern: 'La clave de la configuración debe comenzar con una letra y solo puede contener letras, números, guiones bajos, puntos y dos puntos'
        },
        value: {
          required: 'El valor de la configuración es obligatorio',
          maxLength: 'El valor de la configuración no puede exceder los 500 caracteres'
        },
        builtin: {
          required: 'Por favor seleccione si está integrado'
        },
        order: {
          required: 'El orden de visualización es obligatorio',
          range: 'El orden de visualización debe estar entre 0 y 9999'
        },
        status: {
          required: 'Por favor seleccione el estado'
        }
      },

      // 操作结果
      result: {
        add: {
          success: 'Configuración agregada exitosamente',
          failed: 'Error al agregar la configuración'
        },
        edit: {
          success: 'Configuración actualizada exitosamente',
          failed: 'Error al actualizar la configuración'
        },
        delete: {
          success: 'Configuración eliminada exitosamente',
          failed: 'Error al eliminar la configuración',
          confirm: '¿Está seguro de eliminar esta configuración?'
        },
        export: {
          success: 'Configuración exportada exitosamente',
          failed: 'Error al exportar la configuración'
        },
        import: {
          success: 'Configuración importada exitosamente',
          failed: 'Error al importar la configuración'
        }
      }
    }
  }
} 