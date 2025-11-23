export default {
  routine: {
    core: {
      numberrule: {
        // Título de la página
        title: 'Gestión de Reglas de Numeración',
        
        // Pestañas
        tabs: {
          basicInfo: 'Información Básica',
          numberConfig: 'Configuración de Numeración',
          advancedConfig: 'Configuración Avanzada',
          otherInfo: 'Otra Información'
        },

        // Etiquetas de campos
        fields: {
          companyCode: {
            label: 'Código de Empresa',
            placeholder: 'Por favor ingrese el código de empresa',
            required: 'Por favor ingrese el código de empresa',
            length: 'La longitud del código de empresa debe estar entre 1-20 caracteres'
          },
          numberRuleName: {
            label: 'Nombre de la Regla de Numeración',
            placeholder: 'Por favor ingrese el nombre de la regla de numeración',
            required: 'Por favor ingrese el nombre de la regla de numeración',
            length: 'La longitud del nombre de la regla de numeración debe estar entre 1-100 caracteres'
          },
          numberRuleCode: {
            label: 'Código de la Regla de Numeración',
            placeholder: 'Por favor ingrese el código de la regla de numeración',
            required: 'Por favor ingrese el código de la regla de numeración',
            length: 'La longitud del código de la regla de numeración debe estar entre 1-50 caracteres'
          },
          deptCode: {
            label: 'Código del Departamento',
            placeholder: 'Por favor ingrese el código del departamento',
            required: 'Por favor ingrese el código del departamento',
            length: 'La longitud del código del departamento debe estar entre 1-20 caracteres'
          },
          numberRuleShortCode: {
            label: 'Código Corto de la Regla de Numeración',
            placeholder: 'Por favor ingrese el código corto de la regla de numeración',
            required: 'Por favor ingrese el código corto de la regla de numeración',
            length: 'La longitud del código corto de la regla de numeración debe estar entre 1-4 caracteres'
          },
          numberRuleType: {
            label: 'Tipo de Regla de Numeración',
            placeholder: 'Por favor seleccione el tipo de regla de numeración',
            required: 'Por favor seleccione el tipo de regla de numeración'
          },
          status: {
            label: 'Estado',
            placeholder: 'Por favor seleccione el estado',
            required: 'Por favor seleccione el estado'
          },
          numberRuleDescription: {
            label: 'Descripción de la Regla de Numeración',
            placeholder: 'Por favor ingrese la descripción de la regla de numeración'
          },
          numberPrefix: {
            label: 'Prefijo del Número',
            placeholder: 'Por favor ingrese el prefijo del número'
          },
          numberSuffix: {
            label: 'Sufijo del Número',
            placeholder: 'Por favor ingrese el sufijo del número'
          },
          dateFormat: {
            label: 'Formato de Fecha',
            placeholder: 'Por favor seleccione el formato de fecha',
            required: 'Por favor seleccione el formato de fecha'
          },
          sequenceLength: {
            label: 'Longitud de Secuencia',
            placeholder: 'Por favor ingrese la longitud de secuencia',
            required: 'Por favor ingrese la longitud de secuencia'
          },
          sequenceStart: {
            label: 'Valor de Inicio de Secuencia',
            placeholder: 'Por favor ingrese el valor de inicio de secuencia',
            required: 'Por favor ingrese el valor de inicio de secuencia'
          },
          sequenceStep: {
            label: 'Paso de Secuencia',
            placeholder: 'Por favor ingrese el paso de secuencia',
            required: 'Por favor ingrese el paso de secuencia'
          },
          currentSequence: {
            label: 'Secuencia Actual',
            placeholder: 'Por favor ingrese la secuencia actual'
          },
          sequenceResetRule: {
            label: 'Regla de Reinicio de Secuencia',
            placeholder: 'Por favor seleccione la regla de reinicio de secuencia'
          },
          includeCompanyCode: {
            label: 'Incluir Código de Empresa',
            placeholder: 'Por favor seleccione si incluir el código de empresa'
          },
          includeDepartmentCode: {
            label: 'Incluir Código de Departamento',
            placeholder: 'Por favor seleccione si incluir el código de departamento'
          },
          includeRevisionNumber: {
            label: 'Incluir Número de Revisión',
            placeholder: 'Por favor seleccione si incluir el número de revisión'
          },
          includeYear: {
            label: 'Incluir Año',
            placeholder: 'Por favor seleccione si incluir el año'
          },
          includeMonth: {
            label: 'Incluir Mes',
            placeholder: 'Por favor seleccione si incluir el mes'
          },
          includeDay: {
            label: 'Incluir Día',
            placeholder: 'Por favor seleccione si incluir el día'
          },
          allowDuplicate: {
            label: 'Permitir Duplicados',
            placeholder: 'Por favor seleccione si permitir duplicados'
          },
          duplicateCheckScope: {
            label: 'Alcance de Verificación de Duplicados',
            placeholder: 'Por favor seleccione el alcance de verificación de duplicados'
          },
          orderNum: {
            label: 'Número de Orden',
            placeholder: 'Por favor ingrese el número de orden'
          }
        },

        // Botones de acción
        actions: {
          add: 'Agregar Regla de Numeración',
          edit: 'Editar Regla de Numeración',
          delete: 'Eliminar Regla de Numeración',
          batchDelete: 'Eliminación en Lote',
          export: 'Exportar',
          import: 'Importar',
          downloadTemplate: 'Descargar Plantilla',
          preview: 'Vista Previa',
          generate: 'Generar Número',
          validate: 'Validar Regla'
        },

        // Textos de campos
        placeholder: {
          search: 'Por favor ingrese el nombre o código de la regla de numeración',
          selectAll: 'Seleccionar Todo',
          clear: 'Limpiar'
        },

        // Mensajes de resultados de operaciones
        message: {
          success: {
            add: 'Regla de numeración agregada exitosamente',
            edit: 'Regla de numeración actualizada exitosamente',
            delete: 'Regla de numeración eliminada exitosamente',
            batchDelete: 'Eliminación en lote completada exitosamente',
            export: 'Exportación completada exitosamente',
            import: 'Importación completada exitosamente',
            generate: 'Número generado exitosamente',
            validate: 'Validación exitosa'
          },
          failed: {
            add: 'Error al agregar la regla de numeración',
            edit: 'Error al actualizar la regla de numeración',
            delete: 'Error al eliminar la regla de numeración',
            batchDelete: 'Error en la eliminación en lote',
            export: 'Error en la exportación',
            import: 'Error en la importación',
            generate: 'Error al generar el número',
            validate: 'Error en la validación'
          },
          confirm: {
            delete: '¿Está seguro de eliminar la regla de numeración seleccionada?',
            batchDelete: '¿Está seguro de eliminar las reglas de numeración seleccionadas?'
          }
        },

        // Página de detalles
        detail: {
          title: 'Detalles de la Regla de Numeración',
          basicInfo: 'Información Básica',
          numberConfig: 'Configuración de Numeración',
          advancedConfig: 'Configuración Avanzada',
          otherInfo: 'Otra Información'
        },

        // Títulos de columnas de tabla
        columns: {
          numberRuleName: 'Nombre de la Regla de Numeración',
          numberRuleCode: 'Código de la Regla de Numeración',
          numberRuleShortCode: 'Código Corto de la Regla de Numeración',
          numberRuleType: 'Tipo de Regla de Numeración',
          deptCode: 'Código del Departamento',
          status: 'Estado',
          createTime: 'Tiempo de Creación',
          updateTime: 'Tiempo de Actualización',
          remark: 'Observación',
          actions: 'Acciones'
        },

        // Tipos de reglas de numeración
        types: {
          daily: 'Asuntos Diarios',
          hr: 'Recursos HumanResourceos',
          finance: 'Contabilidad Financiera',
          logistics: 'Gestión Logística',
          production: 'Gestión de Producción',
          workflow: 'Flujo de Trabajo'
        },

        // Estados
        status: {
          normal: 'Normal',
          disabled: 'Deshabilitado'
        },

        // Reglas de reinicio de secuencia
        resetRules: {
          none: 'Sin Reinicio',
          yearly: 'Reinicio Anual',
          monthly: 'Reinicio Mensual',
          daily: 'Reinicio Diario'
        },

        // Alcances de verificación de duplicados
        checkScopes: {
          global: 'Global',
          yearly: 'Anual',
          monthly: 'Mensual',
          daily: 'Diario'
        },

        // Opciones de formato de fecha
        dateFormats: {
          yyyy: 'yyyy',
          yyyyMM: 'yyyyMM',
          yyyyMMdd: 'yyyyMMdd',
          yyyyMMddHH: 'yyyyMMddHH',
          yyyyMMddHHmm: 'yyyyMMddHHmm',
          yyyyMMddHHmmss: 'yyyyMMddHHmmss'
        }
      }
    }
  }
}

