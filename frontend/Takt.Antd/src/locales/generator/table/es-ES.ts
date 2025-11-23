export default {
  generator: {
    table: {
      title: 'Generación de Código',
      list: {
        title: 'Lista de Generación de Código',
        search: {
          name: 'Nombre de la Tabla',
          comment: 'Descripción de la Tabla'
        },
        table: {
          tableId: 'ID de la Tabla',
          databaseName: 'Nombre de la Base de Datos',
          tableName: 'Nombre de la Tabla',
          tableComment: 'Descripción de la Tabla',
          className: 'Nombre de la Clase',
          namespace: 'Espacio de Nombres',
          baseNamespace: 'Espacio de Nombres Base',
          csharpTypeName: 'Nombre del Tipo C#',
          parentTableName: 'Nombre de la Tabla Padre',
          parentTableFkName: 'Clave Foránea de la Tabla Padre',
          status: 'Estado',
          templateType: 'Tipo de Plantilla',
          moduleName: 'Nombre del Módulo',
          businessName: 'Nombre del Negocio',
          functionName: 'Nombre de la Función',
          author: 'Autor',
          genMode: 'Modo de Generación',
          genPath: 'Ruta de Generación',
          options: 'Opciones',
          createBy: 'Creado por',
          createTime: 'Fecha de Creación',
          updateBy: 'Actualizado por',
          updateTime: 'Fecha de Actualización',
          remark: 'Observación',
          isDeleted: 'Eliminado'
        },
        actions: {
          create: 'Crear',
          edit: 'Editar',
          delete: 'Eliminar',
          view: 'Ver',
          generate: 'Generar Código',
          sync: 'Sincronizar Tabla',
          import: 'Importar',
          export: 'Exportar',
          template: 'Descargar Plantilla',
          refresh: 'Actualizar'
        },
        status: {
          enabled: 'Habilitado',
          disabled: 'Deshabilitado'
        }
      },
      form: {
        title: 'Formulario de Generación de Código',
        tab: {
          basic: 'Información Básica',
          generate: 'Información de Generación',
          field: 'Información de Campos'
        },
        basic: {
          title: 'Información Básica',
          tableName: 'Nombre de la Tabla',
          tableComment: 'Descripción de la Tabla',
          className: 'Nombre de la Clase',
          namespace: 'Espacio de Nombres',
          baseNamespace: 'Espacio de Nombres Base',
          csharpTypeName: 'Nombre del Tipo C#',
          parentTableName: 'Nombre de la Tabla Padre',
          parentTableFkName: 'Clave Foránea de la Tabla Padre',
          status: 'Estado',
          author: 'Autor',
          remark: 'Observación'
        },
        generate: {
          title: 'Información de Generación',
          moduleName: 'Nombre del Módulo',
          packageName: 'Ruta del Paquete',
          businessName: 'Nombre del Negocio',
          functionName: 'Nombre de la Función',
          parentMenuId: 'Menú Padre',
          tplCategory: 'Tipo de Plantilla',
          genPath: 'Ruta de Generación',
          options: 'Opciones de Generación',
          tplCategoryOptions: {
            crud: 'Tabla Única (CRUD)',
            tree: 'Tabla de Árbol (CRUD)',
            sub: 'Tabla Maestro-Detalle (CRUD)'
          },
          optionsItems: {
            treeCode: 'Campo de Código de Árbol',
            treeParentCode: 'Campo de Código Padre de Árbol',
            treeName: 'Campo de Nombre de Árbol',
            parentMenuId: 'Menú Padre',
            query: 'Consultar',
            add: 'Agregar',
            edit: 'Editar',
            delete: 'Eliminar',
            import: 'Importar',
            export: 'Exportar'
          }
        },
        field: {
          title: 'Información de Campos',
          columnName: 'Nombre de la Columna',
          columnComment: 'Descripción de la Columna',
          columnType: 'Tipo de Columna',
          csharpType: 'Tipo C#',
          csharpField: 'Campo C#',
          isRequired: 'Requerido',
          isInsert: 'Inserción',
          isEdit: 'Edición',
          isList: 'Lista',
          isQuery: 'Consulta',
          queryType: 'Tipo de Consulta',
          htmlType: 'Tipo de Visualización',
          dictType: 'Tipo de Diccionario',
          queryTypeOptions: {
            EQ: 'Igual',
            NE: 'No Igual',
            GT: 'Mayor Que',
            GTE: 'Mayor o Igual Que',
            LT: 'Menor Que',
            LTE: 'Menor o Igual Que',
            LIKE: 'Contiene',
            BETWEEN: 'Entre',
            IN: 'En'
          },
          htmlTypeOptions: {
            input: 'Campo de Entrada',
            textarea: 'Área de Texto',
            select: 'Lista Desplegable',
            radio: 'Botones de Radio',
            checkbox: 'Casillas de Verificación',
            datetime: 'Fecha y Hora',
            imageUpload: 'Carga de Imagen',
            fileUpload: 'Carga de Archivo',
            editor: 'Editor de Texto Enriquecido'
          }
        },
        buttons: {
          submit: 'Enviar',
          cancel: 'Cancelar'
        },
        name: 'Nombre de la Tabla',
        comment: 'Descripción de la Tabla',
        className: 'Nombre de la Clase',
        namespace: 'Espacio de Nombres',
        baseNamespace: 'Espacio de Nombres Base',
        csharpTypeName: 'Nombre del Tipo C#',
        parentTableName: 'Nombre de la Tabla Padre',
        parentTableFkName: 'Clave Foránea de la Tabla Padre',
        moduleName: 'Nombre del Módulo',
        businessName: 'Nombre del Negocio',
        functionName: 'Nombre de la Función',
        author: 'Autor',
        genMode: 'Modo de Generación',
        genPath: 'Ruta de Generación',
        options: 'Opciones'
      },
      detail: {
        title: 'Detalles de Generación de Código',
        basic: {
          title: 'Información Básica',
          tableName: 'Nombre de la Tabla',
          tableComment: 'Descripción de la Tabla',
          className: 'Nombre de la Clase',
          namespace: 'Espacio de Nombres',
          baseNamespace: 'Espacio de Nombres Base',
          csharpTypeName: 'Nombre del Tipo C#',
          parentTableName: 'Nombre de la Tabla Padre',
          parentTableFkName: 'Clave Foránea de la Tabla Padre',
          status: 'Estado',
          createTime: 'Fecha de Creación',
          updateTime: 'Fecha de Actualización'
        },
        generate: {
          title: 'Información de Generación',
          moduleName: 'Nombre del Módulo',
          packageName: 'Ruta del Paquete',
          businessName: 'Nombre del Negocio',
          functionName: 'Nombre de la Función',
          parentMenuId: 'Menú Padre',
          tplCategory: 'Tipo de Plantilla',
          genPath: 'Ruta de Generación',
          options: 'Opciones de Generación'
        },
        field: {
          title: 'Información de Campos',
          columnName: 'Nombre de la Columna',
          columnComment: 'Descripción de la Columna',
          columnType: 'Tipo de Columna',
          csharpType: 'Tipo C#',
          csharpField: 'Campo C#',
          isRequired: 'Requerido',
          isInsert: 'Inserción',
          isEdit: 'Edición',
          isList: 'Lista',
          isQuery: 'Consulta',
          queryType: 'Tipo de Consulta',
          htmlType: 'Tipo de Visualización',
          dictType: 'Tipo de Diccionario'
        },
        actions: {
          edit: 'Editar',
          back: 'Volver'
        },
        columnInfo: 'Información de la Columna',
        javaType: 'Tipo Java',
        javaField: 'Campo Java',
        yes: 'Sí',
        no: 'No'
      },
      name: 'Nombre de la Tabla',
      comment: 'Descripción de la Tabla',
      databaseName: 'Nombre de la Base de Datos',
      className: 'Nombre de la Clase',
      namespace: 'Espacio de Nombres',
      baseNamespace: 'Espacio de Nombres Base',
      csharpTypeName: 'Nombre del Tipo C#',
      parentTableName: 'Nombre de la Tabla Padre',
      parentTableFkName: 'Clave Foránea de la Tabla Padre',
      status: 'Estado',
      templateType: 'Tipo de Plantilla',
      moduleName: 'Nombre del Módulo',
      businessName: 'Nombre del Negocio',
      functionName: 'Nombre de la Función',
      author: 'Autor',
      genMode: 'Modo de Generación',
      genPath: 'Ruta de Generación',
      options: 'Opciones',
      createBy: 'Creado por',
      createTime: 'Fecha de Creación',
      updateBy: 'Actualizado por',
      updateTime: 'Fecha de Actualización',
      remark: 'Observación',
      isDeleted: 'Eliminado',
      placeholder: {
        name: 'Por favor ingrese el nombre de la tabla',
        comment: 'Por favor ingrese la descripción de la tabla'
      },
      preview: {
        title: 'Vista Previa del Código',
        copy: 'Copiar Código',
        download: 'Descargar Código',
        showLineNumbers: 'Mostrar Números de Línea',
        hideLineNumbers: 'Ocultar Números de Línea',
        copySuccess: 'Copia Exitosa',
        copyFailed: 'Error al Copiar',
        downloadSuccess: 'Descarga Exitosa',
        downloadFailed: 'Error al Descargar',
        tab: {
          java: 'Código Java',
          vue: 'Código Vue',
          sql: 'Código SQL',
          domain: 'Clase de Entidad',
          mapper: 'Interfaz Mapper',
          mapperXml: 'Mapper XML',
          service: 'Interfaz de Servicio',
          serviceImpl: 'Implementación del Servicio',
          controller: 'Controlador',
          api: 'Interfaz API',
          index: 'Página de Lista',
          form: 'Página de Formulario'
        },
        entities: {
          title: 'Código de Clase de Entidad'
        },
        services: {
          title: 'Código de Interfaz de Servicio'
        },
        controllers: {
          title: 'Código de Controlador'
        },
        vue: {
          title: 'Código Vue'
        },
        dtos: {
          title: 'Código DTO'
        },
        types: {
          title: 'Código de Definición de Tipo'
        },
        locales: {
          title: 'Código de Traducción'
        }
      },
      import: {
        title: 'Importar Tabla',
        database: 'Base de Datos',
        table: {
          name: 'Nombre de la Tabla',
          comment: 'Descripción de la Tabla',
          action: 'Acción'
        },
        column: {
          title: 'Importar Columna',
          tableName: 'Nombre de la Tabla',
          tableId: 'ID de la Tabla',
          columnName: 'Nombre de la Columna',
          propertyName: 'Nombre de la Propiedad',
          columnType: 'Tipo de Columna',
          propertyType: 'Tipo de Propiedad',
          isNullable: 'Nulo',
          isPrimaryKey: 'Clave Primaria',
          isAutoIncrement: 'Autoincremental',
          defaultValue: 'Valor Predeterminado',
          columnComment: 'Descripción de la Columna',
          value: 'Valor',
          decimalDigits: 'Dígitos Decimales',
          scale: 'Escala',
          isArray: 'Arreglo',
          isJson: 'Json',
          isUnsigned: 'Sin Signo',
          createTableFieldSort: 'Orden de Campos de Tabla',
          insertServerTime: 'Hora de Inserción del Servidor',
          insertSql: 'SQL de Inserción',
          updateServerTime: 'Hora de Actualización del Servidor',
          updateSql: 'SQL de Actualización',
          sqlParameterDbType: 'Tipo de Parámetro SQL DB'
        }
      },
      message: {
        generateSuccess: 'Generación de código exitosa',
        generateFailed: 'Error en la generación de código',
        syncSuccess: 'Sincronización de tabla exitosa',
        syncFailed: 'Error en la sincronización de tabla',
        importSuccess: 'Importación exitosa',
        importFailed: 'Error en la importación',
        exportSuccess: 'Exportación exitosa',
        exportFailed: 'Error en la exportación',
        templateSuccess: 'Descarga de plantilla exitosa',
        templateFailed: 'Error en la descarga de plantilla',
        selectDatabase: 'Por favor seleccione primero la base de datos',
        selectTable: 'Por favor seleccione las tablas a importar',
        tableNameRequired: 'El nombre de la tabla es requerido',
        importTimeout: 'Tiempo de espera de importación agotado, por favor intente más tarde'
      },
      tab: {
        basic: 'Información Básica',
        generate: 'Información de Generación',
        field: 'Información de Campos'
      },
      required: {
        name: 'Por favor ingrese el nombre de la tabla',
        comment: 'Por favor ingrese la descripción de la tabla',
        className: 'Por favor ingrese el nombre de la clase',
        namespace: 'Por favor ingrese el espacio de nombres',
        baseNamespace: 'Por favor ingrese el espacio de nombres base',
        csharpTypeName: 'Por favor ingrese el nombre del tipo C#',
        moduleName: 'Por favor ingrese el nombre del módulo',
        businessName: 'Por favor ingrese el nombre del negocio',
        functionName: 'Por favor ingrese el nombre de la función',
        author: 'Por favor ingrese el nombre del autor',
        genMode: 'Por favor seleccione el modo de generación',
        genPath: 'Por favor ingrese la ruta de generación'
      }
    }
  }
} 