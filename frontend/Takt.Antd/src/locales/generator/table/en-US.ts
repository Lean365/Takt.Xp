export default {
  generator: {
    table: {
      title: 'Code Generation',
      list: {
        title: 'Code Generation List',
        search: {
          name: 'Table Name',
          comment: 'Table Comment'
        },
        table: {
          tableId: 'Table ID',
          databaseName: 'Database Name',
          tableName: 'Table Name',
          tableComment: 'Table Comment',
          className: 'Class Name',
          namespace: 'Namespace',
          baseNamespace: 'Base Namespace',
          csharpTypeName: 'C# Type Name',
          parentTableName: 'Parent Table Name',
          parentTableFkName: 'Parent Table Foreign Key',
          status: 'Status',
          templateType: 'Template Type',
          moduleName: 'Module Name',
          businessName: 'Business Name',
          functionName: 'Function Name',
          author: 'Author',
          genMode: 'Generation Mode',
          genPath: 'Generation Path',
          options: 'Options',
          createBy: 'Created By',
          createTime: 'Create Time',
          updateBy: 'Updated By',
          updateTime: 'Update Time',
          remark: 'Remark',
          isDeleted: 'Is Deleted'
        },
        actions: {
          create: 'Create',
          edit: 'Edit',
          delete: 'Delete',
          view: 'View',
          generate: 'Generate Code',
          sync: 'Sync Table',
          import: 'Import',
          export: 'Export',
          template: 'Download Template',
          refresh: 'Refresh'
        },
        status: {
          enabled: 'Enabled',
          disabled: 'Disabled'
        }
      },
      form: {
        title: 'Code Generation Form',
        tab: {
          basic: 'Basic Information',
          generate: 'Generation Information',
          field: 'Field Information'
        },
        basic: {
          title: 'Basic Information',
          tableName: 'Table Name',
          tableComment: 'Table Comment',
          className: 'Class Name',
          namespace: 'Namespace',
          baseNamespace: 'Base Namespace',
          csharpTypeName: 'C# Type Name',
          parentTableName: 'Parent Table Name',
          parentTableFkName: 'Parent Table Foreign Key',
          status: 'Status',
          author: 'Author',
          remark: 'Remark'
        },
        generate: {
          title: 'Generation Information',
          moduleName: 'Module Name',
          packageName: 'Package Path',
          businessName: 'Business Name',
          functionName: 'Function Name',
          parentMenuId: 'Parent Menu',
          tplCategory: 'Template Type',
          genPath: 'Generation Path',
          options: 'Generation Options',
          tplCategoryOptions: {
            crud: 'Single Table (CRUD)',
            tree: 'Tree Table (CRUD)',
            sub: 'Master-Sub Table (CRUD)'
          },
          optionsItems: {
            treeCode: 'Tree Code Field',
            treeParentCode: 'Tree Parent Code Field',
            treeName: 'Tree Name Field',
            parentMenuId: 'Parent Menu',
            query: 'Query',
            add: 'Add',
            edit: 'Edit',
            delete: 'Delete',
            import: 'Import',
            export: 'Export'
          }
        },
        field: {
          title: 'Field Information',
          columnName: 'Column Name',
          columnComment: 'Column Comment',
          columnType: 'Column Type',
          csharpType: 'C# Type',
          csharpField: 'C# Field',
          isRequired: 'Required',
          isInsert: 'Insert',
          isEdit: 'Edit',
          isList: 'List',
          isQuery: 'Query',
          queryType: 'Query Type',
          htmlType: 'Display Type',
          dictType: 'Dictionary Type',
          queryTypeOptions: {
            EQ: 'Equal',
            NE: 'Not Equal',
            GT: 'Greater Than',
            GTE: 'Greater Than or Equal',
            LT: 'Less Than',
            LTE: 'Less Than or Equal',
            LIKE: 'Like',
            BETWEEN: 'Between',
            IN: 'In'
          },
          htmlTypeOptions: {
            input: 'Input Field',
            textarea: 'Text Area',
            select: 'Dropdown',
            radio: 'Radio Button',
            checkbox: 'Checkbox',
            datetime: 'Date Time',
            imageUpload: 'Image Upload',
            fileUpload: 'File Upload',
            editor: 'Rich Text Editor'
          }
        },
        buttons: {
          submit: 'Submit',
          cancel: 'Cancel'
        },
        name: 'Table Name',
        comment: 'Table Comment',
        className: 'Class Name',
        namespace: 'Namespace',
        baseNamespace: 'Base Namespace',
        csharpTypeName: 'C# Type Name',
        parentTableName: 'Parent Table Name',
        parentTableFkName: 'Parent Table Foreign Key',
        moduleName: 'Module Name',
        businessName: 'Business Name',
        functionName: 'Function Name',
        author: 'Author',
        genMode: 'Generation Mode',
        genPath: 'Generation Path',
        options: 'Options'
      },
      detail: {
        title: 'Code Generation Detail',
        basic: {
          title: 'Basic Information',
          tableName: 'Table Name',
          tableComment: 'Table Comment',
          className: 'Class Name',
          namespace: 'Namespace',
          baseNamespace: 'Base Namespace',
          csharpTypeName: 'C# Type Name',
          parentTableName: 'Parent Table Name',
          parentTableFkName: 'Parent Table Foreign Key',
          status: 'Status',
          createTime: 'Create Time',
          updateTime: 'Update Time'
        },
        generate: {
          title: 'Generation Information',
          moduleName: 'Module Name',
          packageName: 'Package Path',
          businessName: 'Business Name',
          functionName: 'Function Name',
          parentMenuId: 'Parent Menu',
          tplCategory: 'Template Type',
          genPath: 'Generation Path',
          options: 'Generation Options'
        },
        field: {
          title: 'Field Information',
          columnName: 'Column Name',
          columnComment: 'Column Comment',
          columnType: 'Column Type',
          csharpType: 'C# Type',
          csharpField: 'C# Field',
          isRequired: 'Required',
          isInsert: 'Insert',
          isEdit: 'Edit',
          isList: 'List',
          isQuery: 'Query',
          queryType: 'Query Type',
          htmlType: 'Display Type',
          dictType: 'Dictionary Type'
        },
        actions: {
          edit: 'Edit',
          back: 'Back'
        },
        columnInfo: 'Column Information',
        javaType: 'Java Type',
        javaField: 'Java Field',
        yes: 'Yes',
        no: 'No'
      },
      name: 'Table Name',
      comment: 'Table Comment',
      databaseName: 'Database Name',
      className: 'Class Name',
      namespace: 'Namespace',
      baseNamespace: 'Base Namespace',
      csharpTypeName: 'C# Type Name',
      parentTableName: 'Parent Table Name',
      parentTableFkName: 'Parent Table Foreign Key',
      status: 'Status',
      templateType: 'Template Type',
      moduleName: 'Module Name',
      businessName: 'Business Name',
      functionName: 'Function Name',
      author: 'Author',
      genMode: 'Generation Mode',
      genPath: 'Generation Path',
      options: 'Options',
      createBy: 'Created By',
      createTime: 'Create Time',
      updateBy: 'Updated By',
      updateTime: 'Update Time',
      remark: 'Remark',
      isDeleted: 'Is Deleted',
      placeholder: {
        name: 'Please enter table name',
        comment: 'Please enter table comment'
      },
      preview: {
        title: 'Code Preview',
        copy: 'Copy Code',
        download: 'Download Code',
        showLineNumbers: 'Show Line Numbers',
        hideLineNumbers: 'Hide Line Numbers',
        copySuccess: 'Copy successful',
        copyFailed: 'Copy failed',
        downloadSuccess: 'Download successful',
        downloadFailed: 'Download failed',
        tab: {
          java: 'Java Code',
          vue: 'Vue Code',
          sql: 'SQL Code',
          domain: 'Entity Class',
          mapper: 'Mapper Interface',
          mapperXml: 'Mapper XML',
          service: 'Service Interface',
          serviceImpl: 'Service Implementation',
          controller: 'Controller',
          api: 'API Interface',
          index: 'List Page',
          form: 'Form Page'
        },
        entities: {
          title: 'Entity Class Code'
        },
        services: {
          title: 'Service Interface Code'
        },
        controllers: {
          title: 'Controller Code'
        },
        vue: {
          title: 'Vue Code'
        },
        dtos: {
          title: 'DTO Code'
        },
        types: {
          title: 'Type Definition Code'
        },
        locales: {
          title: 'Translation Code'
        }
      },
      import: {
        title: 'Import Table',
        database: 'Database',
        table: {
          name: 'Table Name',
          comment: 'Table Comment',
          action: 'Action'
        },
        column: {
          title: 'Import Column',
          tableName: 'Table Name',
          tableId: 'Table ID',
          columnName: 'Column Name',
          propertyName: 'Property Name',
          columnType: 'Column Type',
          propertyType: 'Property Type',
          isNullable: 'Nullable',
          isPrimaryKey: 'Primary Key',
          isAutoIncrement: 'Auto Increment',
          defaultValue: 'Default Value',
          columnComment: 'Column Comment',
          value: 'Value',
          decimalDigits: 'Decimal Digits',
          scale: 'Scale',
          isArray: 'Array',
          isJson: 'Json',
          isUnsigned: 'Unsigned',
          createTableFieldSort: 'Create Table Field Sort',
          insertServerTime: 'Insert Server Time',
          insertSql: 'Insert SQL',
          updateServerTime: 'Update Server Time',
          updateSql: 'Update SQL',
          sqlParameterDbType: 'SQL Parameter DB Type'
        }
      },
      message: {
        generateSuccess: 'Code generation successful',
        generateFailed: 'Code generation failed',
        syncSuccess: 'Table sync successful',
        syncFailed: 'Table sync failed',
        importSuccess: 'Import successful',
        importFailed: 'Import failed',
        exportSuccess: 'Export successful',
        exportFailed: 'Export failed',
        templateSuccess: 'Template download successful',
        templateFailed: 'Template download failed',
        selectDatabase: 'Please select database first',
        selectTable: 'Please select tables to import',
        tableNameRequired: 'Table name cannot be empty',
        importTimeout: 'Import timeout, please try again later'
      },
      tab: {
        basic: 'Basic Information',
        generate: 'Generation Information',
        field: 'Field Information'
      },
      required: {
        name: 'Please enter table name',
        comment: 'Please enter table comment',
        className: 'Please enter class name',
        namespace: 'Please enter namespace',
        baseNamespace: 'Please enter base namespace',
        csharpTypeName: 'Please enter C# type name',
        moduleName: 'Please enter module name',
        businessName: 'Please enter business name',
        functionName: 'Please enter function name',
        author: 'Please enter author name',
        genMode: 'Please select generation mode',
        genPath: 'Please enter generation path'
      }
    }
  }
} 