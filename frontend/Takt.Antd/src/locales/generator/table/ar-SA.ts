export default {
  generator: {
    table: {
      title: 'توليد الكود',
      list: {
        title: 'قائمة توليد الكود',
        search: {
          name: 'اسم الجدول',
          comment: 'وصف الجدول'
        },
        table: {
          tableId: 'معرف الجدول',
          databaseName: 'اسم قاعدة البيانات',
          tableName: 'اسم الجدول',
          tableComment: 'وصف الجدول',
          className: 'اسم الفئة',
          namespace: 'مساحة الأسماء',
          baseNamespace: 'مساحة الأسماء الأساسية',
          csharpTypeName: 'اسم نوع C#',
          parentTableName: 'اسم الجدول الأب',
          parentTableFkName: 'مفتاح الجدول الأب الخارجي',
          status: 'الحالة',
          templateType: 'نوع القالب',
          moduleName: 'اسم الوحدة',
          businessName: 'اسم العمل',
          functionName: 'اسم الوظيفة',
          author: 'المؤلف',
          genMode: 'وضع التوليد',
          genPath: 'مسار التوليد',
          options: 'الخيارات',
          createBy: 'أنشئ بواسطة',
          createTime: 'وقت الإنشاء',
          updateBy: 'حدث بواسطة',
          updateTime: 'وقت التحديث',
          remark: 'ملاحظة',
          isDeleted: 'هل تم الحذف'
        },
        actions: {
          create: 'إنشاء',
          edit: 'تعديل',
          delete: 'حذف',
          view: 'عرض',
          generate: 'توليد الكود',
          sync: 'مزامنة الجدول',
          import: 'استيراد',
          export: 'تصدير',
          template: 'تنزيل القالب',
          refresh: 'تحديث'
        },
        status: {
          enabled: 'مفعل',
          disabled: 'معطل'
        }
      },
      form: {
        title: 'نموذج توليد الكود',
        tab: {
          basic: 'المعلومات الأساسية',
          generate: 'معلومات التوليد',
          field: 'معلومات الحقل'
        },
        basic: {
          title: 'المعلومات الأساسية',
          tableName: 'اسم الجدول',
          tableComment: 'وصف الجدول',
          className: 'اسم الفئة',
          namespace: 'مساحة الأسماء',
          baseNamespace: 'مساحة الأسماء الأساسية',
          csharpTypeName: 'اسم نوع C#',
          parentTableName: 'اسم الجدول الأب',
          parentTableFkName: 'مفتاح الجدول الأب الخارجي',
          status: 'الحالة',
          author: 'المؤلف',
          remark: 'ملاحظة'
        },
        generate: {
          title: 'معلومات التوليد',
          moduleName: 'اسم الوحدة',
          packageName: 'مسار الحزمة',
          businessName: 'اسم العمل',
          functionName: 'اسم الوظيفة',
          parentMenuId: 'القائمة الأب',
          tplCategory: 'نوع القالب',
          genPath: 'مسار التوليد',
          options: 'خيارات التوليد',
          tplCategoryOptions: {
            crud: 'جدول واحد (CRUD)',
            tree: 'جدول شجري (CRUD)',
            sub: 'جدول رئيسي-فرعي (CRUD)'
          },
          optionsItems: {
            treeCode: 'حقل رمز الشجرة',
            treeParentCode: 'حقل رمز الأب',
            treeName: 'حقل اسم الشجرة',
            parentMenuId: 'القائمة الأب',
            query: 'استعلام',
            add: 'إضافة',
            edit: 'تعديل',
            delete: 'حذف',
            import: 'استيراد',
            export: 'تصدير'
          }
        },
        field: {
          title: 'معلومات الحقل',
          columnName: 'اسم العمود',
          columnComment: 'وصف العمود',
          columnType: 'نوع العمود',
          csharpType: 'نوع C#',
          csharpField: 'حقل C#',
          isRequired: 'مطلوب',
          isInsert: 'إدراج',
          isEdit: 'تعديل',
          isList: 'قائمة',
          isQuery: 'استعلام',
          queryType: 'نوع الاستعلام',
          htmlType: 'نوع العرض',
          dictType: 'نوع القاموس',
          queryTypeOptions: {
            EQ: 'يساوي',
            NE: 'لا يساوي',
            GT: 'أكبر من',
            GTE: 'أكبر من أو يساوي',
            LT: 'أقل من',
            LTE: 'أقل من أو يساوي',
            LIKE: 'يشبه',
            BETWEEN: 'بين',
            IN: 'ضمن'
          },
          htmlTypeOptions: {
            input: 'حقل إدخال',
            textarea: 'منطقة نصية',
            select: 'قائمة منسدلة',
            radio: 'زر راديو',
            checkbox: 'مربع اختيار',
            datetime: 'تاريخ ووقت',
            imageUpload: 'رفع صورة',
            fileUpload: 'رفع ملف',
            editor: 'محرر النصوص الغني'
          }
        },
        buttons: {
          submit: 'إرسال',
          cancel: 'إلغاء'
        },
        name: 'اسم الجدول',
        comment: 'وصف الجدول',
        className: 'اسم الفئة',
        namespace: 'مساحة الأسماء',
        baseNamespace: 'مساحة الأسماء الأساسية',
        csharpTypeName: 'اسم نوع C#',
        parentTableName: 'اسم الجدول الأب',
        parentTableFkName: 'مفتاح الجدول الأب الخارجي',
        moduleName: 'اسم الوحدة',
        businessName: 'اسم العمل',
        functionName: 'اسم الوظيفة',
        author: 'المؤلف',
        genMode: 'وضع التوليد',
        genPath: 'مسار التوليد',
        options: 'الخيارات'
      },
      detail: {
        title: 'تفاصيل توليد الكود',
        basic: {
          title: 'المعلومات الأساسية',
          tableName: 'اسم الجدول',
          tableComment: 'وصف الجدول',
          className: 'اسم الفئة',
          namespace: 'مساحة الأسماء',
          baseNamespace: 'مساحة الأسماء الأساسية',
          csharpTypeName: 'اسم نوع C#',
          parentTableName: 'اسم الجدول الأب',
          parentTableFkName: 'مفتاح الجدول الأب الخارجي',
          status: 'الحالة',
          createTime: 'وقت الإنشاء',
          updateTime: 'وقت التحديث'
        },
        generate: {
          title: 'معلومات التوليد',
          moduleName: 'اسم الوحدة',
          packageName: 'مسار الحزمة',
          businessName: 'اسم العمل',
          functionName: 'اسم الوظيفة',
          parentMenuId: 'القائمة الأب',
          tplCategory: 'نوع القالب',
          genPath: 'مسار التوليد',
          options: 'خيارات التوليد'
        },
        field: {
          title: 'معلومات الحقل',
          columnName: 'اسم العمود',
          columnComment: 'وصف العمود',
          columnType: 'نوع العمود',
          csharpType: 'نوع C#',
          csharpField: 'حقل C#',
          isRequired: 'مطلوب',
          isInsert: 'إدراج',
          isEdit: 'تعديل',
          isList: 'قائمة',
          isQuery: 'استعلام',
          queryType: 'نوع الاستعلام',
          htmlType: 'نوع العرض',
          dictType: 'نوع القاموس'
        },
        actions: {
          edit: 'تعديل',
          back: 'رجوع'
        },
        columnInfo: 'معلومات العمود',
        javaType: 'نوع Java',
        javaField: 'حقل Java',
        yes: 'نعم',
        no: 'لا'
      },
      name: 'اسم الجدول',
      comment: 'وصف الجدول',
      databaseName: 'اسم قاعدة البيانات',
      className: 'اسم الفئة',
      namespace: 'مساحة الأسماء',
      baseNamespace: 'مساحة الأسماء الأساسية',
      csharpTypeName: 'اسم نوع C#',
      parentTableName: 'اسم الجدول الأب',
      parentTableFkName: 'مفتاح الجدول الأب الخارجي',
      status: 'الحالة',
      templateType: 'نوع القالب',
      moduleName: 'اسم الوحدة',
      businessName: 'اسم العمل',
      functionName: 'اسم الوظيفة',
      author: 'المؤلف',
      genMode: 'وضع التوليد',
      genPath: 'مسار التوليد',
      options: 'الخيارات',
      createBy: 'أنشئ بواسطة',
      createTime: 'وقت الإنشاء',
      updateBy: 'حدث بواسطة',
      updateTime: 'وقت التحديث',
      remark: 'ملاحظة',
      isDeleted: 'هل تم الحذف',
      placeholder: {
        name: 'الرجاء إدخال اسم الجدول',
        comment: 'الرجاء إدخال وصف الجدول'
      },
      preview: {
        title: 'معاينة الكود',
        copy: 'نسخ الكود',
        download: 'تنزيل الكود',
        showLineNumbers: 'إظهار أرقام الأسطر',
        hideLineNumbers: 'إخفاء أرقام الأسطر',
        copySuccess: 'تم النسخ بنجاح',
        copyFailed: 'فشل النسخ',
        downloadSuccess: 'تم التنزيل بنجاح',
        downloadFailed: 'فشل التنزيل',
        tab: {
          java: 'كود Java',
          vue: 'كود Vue',
          sql: 'كود SQL',
          domain: 'فئة الكيان',
          mapper: 'واجهة Mapper',
          mapperXml: 'Mapper XML',
          service: 'واجهة الخدمة',
          serviceImpl: 'تنفيذ الخدمة',
          controller: 'المتحكم',
          api: 'واجهة API',
          index: 'صفحة القائمة',
          form: 'صفحة النموذج'
        },
        entities: {
          title: 'كود فئة الكيان'
        },
        services: {
          title: 'كود واجهة الخدمة'
        },
        controllers: {
          title: 'كود المتحكم'
        },
        vue: {
          title: 'كود Vue'
        },
        dtos: {
          title: 'كود كائن نقل البيانات'
        },
        types: {
          title: 'كود تعريف النوع'
        },
        locales: {
          title: 'كود الترجمة'
        }
      },
      import: {
        title: 'استيراد الجدول',
        database: 'قاعدة البيانات',
        table: {
          name: 'اسم الجدول',
          comment: 'وصف الجدول',
          action: 'إجراء'
        },
        column: {
          title: 'استيراد العمود',
          tableName: 'اسم الجدول',
          tableId: 'معرف الجدول',
          columnName: 'اسم العمود',
          propertyName: 'اسم الخاصية',
          columnType: 'نوع العمود',
          propertyType: 'نوع الخاصية',
          isNullable: 'قابل للقيمة الفارغة',
          isPrimaryKey: 'مفتاح أساسي',
          isAutoIncrement: 'تزايد تلقائي',
          defaultValue: 'القيمة الافتراضية',
          columnComment: 'وصف العمود',
          value: 'القيمة',
          decimalDigits: 'الأرقام العشرية',
          scale: 'المقياس',
          isArray: 'مصفوفة',
          isJson: 'Json',
          isUnsigned: 'غير موقعة',
          createTableFieldSort: 'ترتيب حقل إنشاء الجدول',
          insertServerTime: 'وقت إدراج الخادم',
          insertSql: 'SQL الإدراج',
          updateServerTime: 'وقت تحديث الخادم',
          updateSql: 'SQL التحديث',
          sqlParameterDbType: 'نوع معلمة SQL'
        }
      },
      message: {
        generateSuccess: 'تم توليد الكود بنجاح',
        generateFailed: 'فشل توليد الكود',
        syncSuccess: 'تمت مزامنة الجدول بنجاح',
        syncFailed: 'فشلت مزامنة الجدول',
        importSuccess: 'تم الاستيراد بنجاح',
        importFailed: 'فشل الاستيراد',
        exportSuccess: 'تم التصدير بنجاح',
        exportFailed: 'فشل التصدير',
        templateSuccess: 'تم تنزيل القالب بنجاح',
        templateFailed: 'فشل تنزيل القالب',
        selectDatabase: 'الرجاء اختيار قاعدة البيانات أولاً',
        selectTable: 'الرجاء اختيار الجداول للاستيراد',
        tableNameRequired: 'اسم الجدول لا يمكن أن يكون فارغاً',
        importTimeout: 'انتهت مهلة الاستيراد، يرجى المحاولة مرة أخرى لاحقاً'
      },
      tab: {
        basic: 'المعلومات الأساسية',
        generate: 'معلومات التوليد',
        field: 'معلومات الحقل'
      },
      required: {
        name: 'الرجاء إدخال اسم الجدول',
        comment: 'الرجاء إدخال وصف الجدول',
        className: 'الرجاء إدخال اسم الفئة',
        namespace: 'الرجاء إدخال مساحة الأسماء',
        baseNamespace: 'الرجاء إدخال مساحة الأسماء الأساسية',
        csharpTypeName: 'الرجاء إدخال اسم نوع C#',
        moduleName: 'الرجاء إدخال اسم الوحدة',
        businessName: 'الرجاء إدخال اسم العمل',
        functionName: 'الرجاء إدخال اسم الوظيفة',
        author: 'الرجاء إدخال اسم المؤلف',
        genMode: 'الرجاء اختيار وضع التوليد',
        genPath: 'الرجاء إدخال مسار التوليد'
      }
    }
  }
} 