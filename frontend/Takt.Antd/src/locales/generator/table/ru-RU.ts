export default {
  generator: {
    table: {
      title: 'Генерация кода',
      list: {
        title: 'Список генерации кода',
        search: {
          name: 'Имя таблицы',
          comment: 'Комментарий таблицы'
        },
        table: {
          tableId: 'ID таблицы',
          databaseName: 'Имя базы данных',
          tableName: 'Имя таблицы',
          tableComment: 'Комментарий таблицы',
          className: 'Имя класса',
          namespace: 'Пространство имен',
          baseNamespace: 'Базовое пространство имен',
          csharpTypeName: 'Имя типа C#',
          parentTableName: 'Имя родительской таблицы',
          parentTableFkName: 'Имя внешнего ключа родительской таблицы',
          status: 'Статус',
          templateType: 'Тип шаблона',
          moduleName: 'Имя модуля',
          businessName: 'Имя бизнес-процесса',
          functionName: 'Имя функции',
          author: 'Автор',
          genMode: 'Режим генерации',
          genPath: 'Путь генерации',
          options: 'Опции',
          createBy: 'Создано',
          createTime: 'Время создания',
          updateBy: 'Обновлено',
          updateTime: 'Время обновления',
          remark: 'Примечание',
          isDeleted: 'Удалено'
        },
        actions: {
          create: 'Создать',
          edit: 'Редактировать',
          delete: 'Удалить',
          view: 'Просмотр',
          generate: 'Сгенерировать код',
          sync: 'Синхронизировать таблицу',
          import: 'Импорт',
          export: 'Экспорт',
          template: 'Скачать шаблон',
          refresh: 'Обновить'
        },
        status: {
          enabled: 'Включено',
          disabled: 'Отключено'
        }
      },
      form: {
        title: 'Форма генерации кода',
        tab: {
          basic: 'Основная информация',
          generate: 'Информация о генерации',
          field: 'Информация о полях'
        },
        basic: {
          title: 'Основная информация',
          tableName: 'Имя таблицы',
          tableComment: 'Комментарий таблицы',
          className: 'Имя класса',
          namespace: 'Пространство имен',
          baseNamespace: 'Базовое пространство имен',
          csharpTypeName: 'Имя типа C#',
          parentTableName: 'Имя родительской таблицы',
          parentTableFkName: 'Имя внешнего ключа родительской таблицы',
          status: 'Статус',
          author: 'Автор',
          remark: 'Примечание'
        },
        generate: {
          title: 'Информация о генерации',
          moduleName: 'Имя модуля',
          packageName: 'Путь пакета',
          businessName: 'Имя бизнес-процесса',
          functionName: 'Имя функции',
          parentMenuId: 'Родительское меню',
          tplCategory: 'Тип шаблона',
          genPath: 'Путь генерации',
          options: 'Опции генерации',
          tplCategoryOptions: {
            crud: 'Одиночная таблица (CRUD)',
            tree: 'Древовидная таблица (CRUD)',
            sub: 'Таблица мастер-детали (CRUD)'
          },
          optionsItems: {
            treeCode: 'Поле кода дерева',
            treeParentCode: 'Поле родительского кода дерева',
            treeName: 'Поле имени дерева',
            parentMenuId: 'Родительское меню',
            query: 'Поиск',
            add: 'Добавить',
            edit: 'Редактировать',
            delete: 'Удалить',
            import: 'Импорт',
            export: 'Экспорт'
          }
        },
        field: {
          title: 'Информация о полях',
          columnName: 'Имя столбца',
          columnComment: 'Комментарий столбца',
          columnType: 'Тип столбца',
          csharpType: 'Тип C#',
          csharpField: 'Поле C#',
          isRequired: 'Обязательное',
          isInsert: 'Вставка',
          isEdit: 'Редактирование',
          isList: 'Список',
          isQuery: 'Поиск',
          queryType: 'Тип запроса',
          htmlType: 'Тип отображения',
          dictType: 'Тип словаря',
          queryTypeOptions: {
            EQ: 'Равно',
            NE: 'Не равно',
            GT: 'Больше',
            GTE: 'Больше или равно',
            LT: 'Меньше',
            LTE: 'Меньше или равно',
            LIKE: 'Содержит',
            BETWEEN: 'Диапазон',
            IN: 'В списке'
          },
          htmlTypeOptions: {
            input: 'Поле ввода',
            textarea: 'Текстовое поле',
            select: 'Выпадающий список',
            radio: 'Радиокнопки',
            checkbox: 'Флажки',
            datetime: 'Дата и время',
            imageUpload: 'Загрузка изображения',
            fileUpload: 'Загрузка файла',
            editor: 'Редактор форматированного текста'
          }
        },
        buttons: {
          submit: 'Отправить',
          cancel: 'Отмена'
        },
        name: 'Имя таблицы',
        comment: 'Комментарий таблицы',
        className: 'Имя класса',
        namespace: 'Пространство имен',
        baseNamespace: 'Базовое пространство имен',
        csharpTypeName: 'Имя типа C#',
        parentTableName: 'Имя родительской таблицы',
        parentTableFkName: 'Имя внешнего ключа родительской таблицы',
        moduleName: 'Имя модуля',
        businessName: 'Имя бизнес-процесса',
        functionName: 'Имя функции',
        author: 'Автор',
        genMode: 'Режим генерации',
        genPath: 'Путь генерации',
        options: 'Опции'
      },
      detail: {
        title: 'Детали генерации кода',
        basic: {
          title: 'Основная информация',
          tableName: 'Имя таблицы',
          tableComment: 'Комментарий таблицы',
          className: 'Имя класса',
          namespace: 'Пространство имен',
          baseNamespace: 'Базовое пространство имен',
          csharpTypeName: 'Имя типа C#',
          parentTableName: 'Имя родительской таблицы',
          parentTableFkName: 'Имя внешнего ключа родительской таблицы',
          status: 'Статус',
          createTime: 'Время создания',
          updateTime: 'Время обновления'
        },
        generate: {
          title: 'Информация о генерации',
          moduleName: 'Имя модуля',
          packageName: 'Путь пакета',
          businessName: 'Имя бизнес-процесса',
          functionName: 'Имя функции',
          parentMenuId: 'Родительское меню',
          tplCategory: 'Тип шаблона',
          genPath: 'Путь генерации',
          options: 'Опции генерации'
        },
        field: {
          title: 'Информация о полях',
          columnName: 'Имя столбца',
          columnComment: 'Комментарий столбца',
          columnType: 'Тип столбца',
          csharpType: 'Тип C#',
          csharpField: 'Поле C#',
          isRequired: 'Обязательное',
          isInsert: 'Вставка',
          isEdit: 'Редактирование',
          isList: 'Список',
          isQuery: 'Поиск',
          queryType: 'Тип запроса',
          htmlType: 'Тип отображения',
          dictType: 'Тип словаря'
        },
        actions: {
          edit: 'Редактировать',
          back: 'Назад'
        },
        columnInfo: 'Информация о столбце',
        javaType: 'Тип Java',
        javaField: 'Поле Java',
        yes: 'Да',
        no: 'Нет'
      },
      name: 'Имя таблицы',
      comment: 'Комментарий таблицы',
      databaseName: 'Имя базы данных',
      className: 'Имя класса',
      namespace: 'Пространство имен',
      baseNamespace: 'Базовое пространство имен',
      csharpTypeName: 'Имя типа C#',
      parentTableName: 'Имя родительской таблицы',
      parentTableFkName: 'Имя внешнего ключа родительской таблицы',
      status: 'Статус',
      templateType: 'Тип шаблона',
      moduleName: 'Имя модуля',
      businessName: 'Имя бизнес-процесса',
      functionName: 'Имя функции',
      author: 'Автор',
      genMode: 'Режим генерации',
      genPath: 'Путь генерации',
      options: 'Опции',
      createBy: 'Создано',
      createTime: 'Время создания',
      updateBy: 'Обновлено',
      updateTime: 'Время обновления',
      remark: 'Примечание',
      isDeleted: 'Удалено',
      placeholder: {
        name: 'Введите имя таблицы',
        comment: 'Введите комментарий таблицы'
      },
      preview: {
        title: 'Предпросмотр кода',
        copy: 'Копировать код',
        download: 'Скачать код',
        showLineNumbers: 'Показать номера строк',
        hideLineNumbers: 'Скрыть номера строк',
        copySuccess: 'Копирование успешно',
        copyFailed: 'Ошибка копирования',
        downloadSuccess: 'Скачивание успешно',
        downloadFailed: 'Ошибка скачивания',
        tab: {
          java: 'Java код',
          vue: 'Vue код',
          sql: 'SQL код',
          domain: 'Класс сущности',
          mapper: 'Интерфейс Mapper',
          mapperXml: 'Mapper XML',
          service: 'Интерфейс сервиса',
          serviceImpl: 'Реализация сервиса',
          controller: 'Контроллер',
          api: 'API интерфейс',
          index: 'Страница списка',
          form: 'Страница формы'
        },
        entities: {
          title: 'Код класса сущности'
        },
        services: {
          title: 'Код интерфейса сервиса'
        },
        controllers: {
          title: 'Код контроллера'
        },
        vue: {
          title: 'Vue код'
        },
        dtos: {
          title: 'Код DTO'
        },
        types: {
          title: 'Код определения типов'
        },
        locales: {
          title: 'Код локализации'
        }
      },
      import: {
        title: 'Импорт таблицы',
        database: 'База данных',
        table: {
          name: 'Имя таблицы',
          comment: 'Комментарий таблицы',
          action: 'Действие'
        },
        column: {
          title: 'Импорт столбца',
          tableName: 'Имя таблицы',
          tableId: 'ID таблицы',
          columnName: 'Имя столбца',
          propertyName: 'Имя свойства',
          columnType: 'Тип столбца',
          propertyType: 'Тип свойства',
          isNullable: 'Допускает NULL',
          isPrimaryKey: 'Первичный ключ',
          isAutoIncrement: 'Автоинкремент',
          defaultValue: 'Значение по умолчанию',
          columnComment: 'Комментарий столбца',
          value: 'Значение',
          decimalDigits: 'Десятичные знаки',
          scale: 'Масштаб',
          isArray: 'Массив',
          isJson: 'Json',
          isUnsigned: 'Без знака',
          createTableFieldSort: 'Сортировка полей таблицы',
          insertServerTime: 'Время вставки сервера',
          insertSql: 'SQL вставки',
          updateServerTime: 'Время обновления сервера',
          updateSql: 'SQL обновления',
          sqlParameterDbType: 'Тип параметра SQL DB'
        }
      },
      message: {
        generateSuccess: 'Генерация кода успешна',
        generateFailed: 'Ошибка генерации кода',
        syncSuccess: 'Синхронизация таблицы успешна',
        syncFailed: 'Ошибка синхронизации таблицы',
        importSuccess: 'Импорт успешен',
        importFailed: 'Ошибка импорта',
        exportSuccess: 'Экспорт успешен',
        exportFailed: 'Ошибка экспорта',
        templateSuccess: 'Скачивание шаблона успешно',
        templateFailed: 'Ошибка скачивания шаблона',
        selectDatabase: 'Сначала выберите базу данных',
        selectTable: 'Выберите таблицы для импорта',
        tableNameRequired: 'Имя таблицы обязательно',
        importTimeout: 'Таймаут импорта, попробуйте позже'
      },
      tab: {
        basic: 'Основная информация',
        generate: 'Информация о генерации',
        field: 'Информация о полях'
      },
      required: {
        name: 'Введите имя таблицы',
        comment: 'Введите комментарий таблицы',
        className: 'Введите имя класса',
        namespace: 'Введите пространство имен',
        baseNamespace: 'Введите базовое пространство имен',
        csharpTypeName: 'Введите имя типа C#',
        moduleName: 'Введите имя модуля',
        businessName: 'Введите имя бизнес-процесса',
        functionName: 'Введите имя функции',
        author: 'Введите имя автора',
        genMode: 'Выберите режим генерации',
        genPath: 'Введите путь генерации'
      }
    }
  }
} 