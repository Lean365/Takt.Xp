export default {
  generator: {
    table: {
      title: '代碼生成',
      list: {
        title: '代碼生成列表',
        search: {
          name: '表名',
          comment: '表描述'
        },
        table: {
          tableId: '表ID',
          databaseName: '數據庫名',
          tableName: '表名',
          tableComment: '表描述',
          className: '類名',
          namespace: '命名空間',
          baseNamespace: '基礎命名空間',
          csharpTypeName: 'C#類型名',
          parentTableName: '父表名',
          parentTableFkName: '父表外鍵',
          status: '狀態',
          templateType: '模板類型',
          moduleName: '模塊名',
          businessName: '業務名',
          functionName: '功能名',
          author: '作者',
          genMode: '生成模式',
          genPath: '生成路徑',
          options: '選項',
          createBy: '創建者',
          createTime: '創建時間',
          updateBy: '更新者',
          updateTime: '更新時間',
          remark: '備註',
          isDeleted: '已刪除'
        },
        actions: {
          create: '新增',
          edit: '修改',
          delete: '刪除',
          view: '查看',
          generate: '生成代碼',
          sync: '同步表',
          import: '導入',
          export: '導出',
          template: '下載模板',
          refresh: '刷新'
        },
        status: {
          enabled: '啟用',
          disabled: '禁用'
        }
      },
      form: {
        title: '代碼生成表單',
        tab: {
          basic: '基本信息',
          generate: '生成信息',
          field: '字段信息'
        },
        basic: {
          title: '基本信息',
          tableName: '表名',
          tableComment: '表描述',
          className: '類名',
          namespace: '命名空間',
          baseNamespace: '基礎命名空間',
          csharpTypeName: 'C#類型名',
          parentTableName: '父表名',
          parentTableFkName: '父表外鍵',
          status: '狀態',
          author: '作者',
          remark: '備註'
        },
        generate: {
          title: '生成信息',
          moduleName: '模塊名',
          packageName: '包路徑',
          businessName: '業務名',
          functionName: '功能名',
          parentMenuId: '上級菜單',
          tplCategory: '模板類型',
          genPath: '生成路徑',
          options: '生成選項',
          tplCategoryOptions: {
            crud: '單表（增刪改查）',
            tree: '樹表（增刪改查）',
            sub: '主子表（增刪改查）'
          },
          optionsItems: {
            treeCode: '樹編碼字段',
            treeParentCode: '樹父編碼字段',
            treeName: '樹名稱字段',
            parentMenuId: '上級菜單',
            query: '查詢',
            add: '新增',
            edit: '修改',
            delete: '刪除',
            import: '導入',
            export: '導出'
          }
        },
        field: {
          title: '字段信息',
          columnName: '列名',
          columnComment: '列描述',
          columnType: '列類型',
          csharpType: 'C#類型',
          csharpField: 'C#字段',
          isRequired: '必填',
          isInsert: '插入',
          isEdit: '修改',
          isList: '列表',
          isQuery: '查詢',
          queryType: '查詢類型',
          htmlType: '顯示類型',
          dictType: '字典類型',
          queryTypeOptions: {
            EQ: '等於',
            NE: '不等於',
            GT: '大於',
            GTE: '大於等於',
            LT: '小於',
            LTE: '小於等於',
            LIKE: '模糊',
            BETWEEN: '範圍',
            IN: '包含'
          },
          htmlTypeOptions: {
            input: '輸入框',
            textarea: '文本域',
            select: '下拉框',
            radio: '單選框',
            checkbox: '複選框',
            datetime: '日期時間',
            imageUpload: '圖片上傳',
            fileUpload: '文件上傳',
            editor: '富文本編輯器'
          }
        },
        buttons: {
          submit: '提交',
          cancel: '取消'
        },
        name: '表名',
        comment: '表描述',
        className: '類名',
        namespace: '命名空間',
        baseNamespace: '基礎命名空間',
        csharpTypeName: 'C#類型名',
        parentTableName: '父表名',
        parentTableFkName: '父表外鍵',
        moduleName: '模塊名',
        businessName: '業務名',
        functionName: '功能名',
        author: '作者',
        genMode: '生成模式',
        genPath: '生成路徑',
        options: '選項'
      },
      detail: {
        title: '代碼生成詳情',
        basic: {
          title: '基本信息',
          tableName: '表名',
          tableComment: '表描述',
          className: '類名',
          namespace: '命名空間',
          baseNamespace: '基礎命名空間',
          csharpTypeName: 'C#類型名',
          parentTableName: '父表名',
          parentTableFkName: '父表外鍵',
          status: '狀態',
          createTime: '創建時間',
          updateTime: '更新時間'
        },
        generate: {
          title: '生成信息',
          moduleName: '模塊名',
          packageName: '包路徑',
          businessName: '業務名',
          functionName: '功能名',
          parentMenuId: '上級菜單',
          tplCategory: '模板類型',
          genPath: '生成路徑',
          options: '生成選項'
        },
        field: {
          title: '字段信息',
          columnName: '列名',
          columnComment: '列描述',
          columnType: '列類型',
          csharpType: 'C#類型',
          csharpField: 'C#字段',
          isRequired: '必填',
          isInsert: '插入',
          isEdit: '修改',
          isList: '列表',
          isQuery: '查詢',
          queryType: '查詢類型',
          htmlType: '顯示類型',
          dictType: '字典類型'
        },
        actions: {
          edit: '修改',
          back: '返回'
        },
        columnInfo: '列信息',
        javaType: 'Java類型',
        javaField: 'Java字段',
        yes: '是',
        no: '否'
      },
      name: '表名',
      comment: '表描述',
      databaseName: '數據庫名',
      className: '類名',
      namespace: '命名空間',
      baseNamespace: '基礎命名空間',
      csharpTypeName: 'C#類型名',
      parentTableName: '父表名',
      parentTableFkName: '父表外鍵',
      status: '狀態',
      templateType: '模板類型',
      moduleName: '模塊名',
      businessName: '業務名',
      functionName: '功能名',
      author: '作者',
      genMode: '生成模式',
      genPath: '生成路徑',
      options: '選項',
      createBy: '創建者',
      createTime: '創建時間',
      updateBy: '更新者',
      updateTime: '更新時間',
      remark: '備註',
      isDeleted: '已刪除',
      placeholder: {
        name: '請輸入表名',
        comment: '請輸入表描述'
      },
      preview: {
        title: '代碼預覽',
        copy: '複製代碼',
        download: '下載代碼',
        showLineNumbers: '顯示行號',
        hideLineNumbers: '隱藏行號',
        copySuccess: '複製成功',
        copyFailed: '複製失敗',
        downloadSuccess: '下載成功',
        downloadFailed: '下載失敗',
        tab: {
          java: 'Java代碼',
          vue: 'Vue代碼',
          sql: 'SQL代碼',
          domain: '實體類',
          mapper: 'Mapper接口',
          mapperXml: 'Mapper XML',
          service: '服務接口',
          serviceImpl: '服務實現',
          controller: '控制器',
          api: 'API接口',
          index: '列表頁面',
          form: '表單頁面'
        },
        entities: {
          title: '實體類代碼'
        },
        services: {
          title: '服務接口代碼'
        },
        controllers: {
          title: '控制器代碼'
        },
        vue: {
          title: 'Vue代碼'
        },
        dtos: {
          title: 'DTO代碼'
        },
        types: {
          title: '類型定義代碼'
        },
        locales: {
          title: '翻譯代碼'
        }
      },
      import: {
        title: '導入表',
        database: '數據庫',
        table: {
          name: '表名',
          comment: '表描述',
          action: '操作'
        },
        column: {
          title: '導入列',
          tableName: '表名',
          tableId: '表ID',
          columnName: '列名',
          propertyName: '屬性名',
          columnType: '列類型',
          propertyType: '屬性類型',
          isNullable: '允許空值',
          isPrimaryKey: '主鍵',
          isAutoIncrement: '自增',
          defaultValue: '默認值',
          columnComment: '列描述',
          value: '值',
          decimalDigits: '小數位數',
          scale: '精度',
          isArray: '數組',
          isJson: 'Json',
          isUnsigned: '無符號',
          createTableFieldSort: '表字段排序',
          insertServerTime: '服務器插入時間',
          insertSql: '插入SQL',
          updateServerTime: '服務器更新時間',
          updateSql: '更新SQL',
          sqlParameterDbType: 'SQL參數DB類型'
        }
      },
      message: {
        generateSuccess: '代碼生成成功',
        generateFailed: '代碼生成失敗',
        syncSuccess: '表同步成功',
        syncFailed: '表同步失敗',
        importSuccess: '導入成功',
        importFailed: '導入失敗',
        exportSuccess: '導出成功',
        exportFailed: '導出失敗',
        templateSuccess: '模板下載成功',
        templateFailed: '模板下載失敗',
        selectDatabase: '請先選擇數據庫',
        selectTable: '請選擇要導入的表',
        tableNameRequired: '表名不能為空',
        importTimeout: '導入超時，請稍後重試'
      },
      tab: {
        basic: '基本信息',
        generate: '生成信息',
        field: '字段信息'
      },
      required: {
        name: '請輸入表名',
        comment: '請輸入表描述',
        className: '請輸入類名',
        namespace: '請輸入命名空間',
        baseNamespace: '請輸入基礎命名空間',
        csharpTypeName: '請輸入C#類型名',
        moduleName: '請輸入模塊名',
        businessName: '請輸入業務名',
        functionName: '請輸入功能名',
        author: '請輸入作者名',
        genMode: '請選擇生成模式',
        genPath: '請輸入生成路徑'
      }
    }
  }
} 