export default {
  common: {
    // ==================== Системная информация ====================
    system: {
      title: 'Платформа Black Ice',
      slogan: 'Профессиональная, эффективная и безопасная система управления предприятием',
      description: 'Современная система управления предприятием на основе .NET 8 и Vue 3',
      copyright: '© 2024Takt(Claude AI). Все права защищены.'
    },

    // ==================== Сообщения об ошибках ====================
    error: {
      clientError: 'Ошибка запроса клиента, проверьте параметры запроса',
      systemRestart: 'Система находится на техническом обслуживании. Пожалуйста, войдите позже',
      network: 'Ошибка подключения к сети. Проверьте настройки сети',
      unauthorized: 'Не авторизован. Пожалуйста, войдите снова',
      forbidden: 'Доступ запрещен',
      notFound: 'Запрошенный ресурс не существует',
      badRequest: 'Неверные параметры запроса',
      serverError: 'Внутренняя ошибка сервера',
      serviceUnavailable: 'Сервис временно недоступен',
      badGateway: 'Неверный шлюз',
      gatewayTimeout: 'Таймаут шлюза',
      unknown: 'Неизвестная ошибка'
    },

    // ==================== Базовые операции ====================
    // Базовые заголовки
    title: {
      list: 'Список',
      detail: 'Детали',
      create: 'Создать',
      edit: 'Редактировать',
      preview: 'Предпросмотр'
    },

    // ==================== Определения статусов ====================
    status: {
      // Базовый статус
      base: {
        label: 'Статус',
        normal: 'Нормальный',
        disabled: 'Отключен',
        placeholder: 'Выберите статус'
      },

      // Статус Да/Нет
      yesNo: {
        all: 'Все',
        yes: 'Да',
        no: 'Нет'
      },

      // Статус видимости
      visible: {
        show: 'Показать',
        hide: 'Скрыть'
      },

      // Статус кэша
      cache: {
        enabled: 'Включен',
        disabled: 'Отключен'
      },

      // Статус онлайн
      online: {
        online: 'В сети',
        offline: 'Не в сети'
      },

      // Статус обработки
      process: {
        pending: 'В ожидании',
        processing: 'Обработка',
        completed: 'Завершено',
        failed: 'Ошибка'
      },

      // Статус проверки
      verify: {
        unverified: 'Не проверено',
        verified: 'Проверено',
        failed: 'Ошибка проверки'
      },

      // Статус блокировки
      lock: {
        locked: 'Заблокировано',
        unlocked: 'Разблокировано'
      },

      // Статус публикации
      publish: {
        draft: 'Черновик',
        published: 'Опубликовано',
        unpublished: 'Не опубликовано'
      },

      // Статус синхронизации
      sync: {
        synced: 'Синхронизировано',
        unsynced: 'Не синхронизировано',
        syncing: 'Синхронизация'
      }
    },

    // ==================== Формы ====================
    form: {
      required: 'Обязательно',
      optional: 'Необязательно',
      invalid: 'Недействительно',
      // Плейсхолдеры форм
      placeholder: {
        select: 'Выберите',
        input: 'Введите',
        date: 'Выберите дату',
        time: 'Выберите время'
      },
      // Форма пользователя
      user: {
        // Базовая информация
        userId: {
          label: 'ID пользователя',
          placeholder: 'Введите ID пользователя'
        },
        userName: {
          label: 'Имя пользователя',
          placeholder: 'Введите имя пользователя'
        },
        nickName: {
          label: 'Псевдоним',
          placeholder: 'Введите псевдоним'
        },
        realName: {
          label: 'Реальное имя',
          placeholder: 'Введите реальное имя'
        },
        englishName: {
          label: 'Имя на английском',
          placeholder: 'Введите имя на английском'
        },
        password: {
          label: 'Пароль',
          placeholder: 'Введите пароль'
        },
        confirmPassword: {
          label: 'Подтвердите пароль',
          placeholder: 'Введите пароль еще раз'
        },
        
        // Личная информация
        avatar: {
          label: 'Аватар',
          placeholder: 'Загрузите аватар'
        },
        gender: {
          label: 'Пол',
          placeholder: 'Выберите пол',
          options: {
            male: 'Мужской',
            female: 'Женский',
            other: 'Другой'
          }
        },
        birthday: {
          label: 'Дата рождения',
          placeholder: 'Выберите дату рождения'
        },
        
        // Контактная информация
        email: {
          label: 'Электронная почта',
          placeholder: 'Введите электронную почту'
        },
        phoneNumber: {
          label: 'Мобильный телефон',
          placeholder: 'Введите номер мобильного телефона'
        },
        telephone: {
          label: 'Телефон',
          placeholder: 'Введите номер телефона'
        },
        
        // Организационная информация
        deptId: {
          label: 'Отдел',
          placeholder: 'Выберите отдел'
        },
        roleId: {
          label: 'Роль',
          placeholder: 'Выберите роль'
        },
        postId: {
          label: 'Должность',
          placeholder: 'Выберите должность'
        },
        
        // Информация об учетной записи
        userType: {
          label: 'Тип пользователя',
          placeholder: 'Выберите тип пользователя',
          options: {
            system: 'Системный пользователь',
            normal: 'Обычный пользователь'
          }
        },
        status: {
          label: 'Статус',
          placeholder: 'Выберите статус'
        },
        loginIp: {
          label: 'Последний IP входа',
          placeholder: 'Последний IP входа'
        },
        loginDate: {
          label: 'Последнее время входа',
          placeholder: 'Последнее время входа'
        },
        
        // Прочая информация
        remark: {
          label: 'Примечание',
          placeholder: 'Введите примечание'
        },
        sort: {
          label: 'Порядок',
          placeholder: 'Введите порядковый номер'
        }
      },
      // Примечание
      remark: {
        label: 'Примечание',
        placeholder: 'Введите примечание'
      }
    },

    // ==================== Таблицы ====================
    table: {
      header: {
        operation: 'Операции'
      },
      config: {
        density: {
          default: 'По умолчанию',
          middle: 'Средний',
          small: 'Маленький'
        },
        columnSetting: 'Настройка столбцов'
      },
      pagination: {
        total: 'Всего {total}',
        current: 'Страница {current}',
        pageSize: '{pageSize} на страницу',
        jump: 'Перейти'
      },
      empty: 'Нет данных',
      loading: 'Загрузка...',
      selectAll: 'Выбрать все',
      selected: 'Выбрано {total}'
    },

    // ==================== Дата и время ====================
    datetime: {
      date: 'Дата',
      time: 'Время',
      year: 'Год',
      month: 'Месяц',
      day: 'День',
      hour: 'Час',
      minute: 'Минута',
      second: 'Секунда',
      startDate: 'Дата начала',
      endDate: 'Дата окончания',
      startTime: 'Время начала',
      endTime: 'Время окончания',
      createTime: 'Время создания',
      updateTime: 'Время обновления',
      formatError: 'Ошибка форматирования времени',
      relativeTimeFormatError: 'Ошибка форматирования относительного времени',
      parseError: 'Ошибка разбора даты',
      rangeSeparator: ' ~ '
    },

    // ==================== Файлы ====================
    // Импорт/Экспорт
    import: {
      title: 'Импорт данных',
      file: 'Выбор файла',
      select: 'Выберите файл',
      template: 'Скачать шаблон',
      download: 'Скачайте шаблон',
      note: 'Примечание к импорту',
      tips: 'Строго следуйте формату шаблона импорта, иначе импорт может завершиться с ошибкой',
      format: 'Поддерживаются только файлы Excel!',
      size: 'Размер файла не должен превышать {size}MB!',
      total: 'Всего записей',
      success: 'Успешно',
      failed: 'Ошибка',
      message: 'Причина ошибки',
      dragText: 'Нажмите или перетащите файл в эту область для загрузки',
      dragHint: 'Поддерживаются файлы Excel формата .xlsx',
      sheetName: 'Убедитесь, что имя листа Excel файла: {sheetName}',
      allSuccess: 'Импорт успешен {count} записей, все успешно!',
      partialSuccess: 'Импорт успешен {success} записей, ошибка {fail} записей',
      allFailed: 'Весь импорт завершился ошибкой, всего {count} записей',
      noData: 'Данные не прочитаны',
      empty: 'Файл пуст, загрузка невозможна',
      importFailed: 'Импорт завершился ошибкой',
      templateFileName: 'Import_Template_{time}.xlsx',
      limits: {
        title: 'Ограничения импорта',
        fileCount: 'Ограничение количества файлов: {count} файл',
        fileSize: 'Ограничение размера файла: {size}МБ',
        recordCount: 'Ограничение количества записей: {count} записей',
        fileFormat: 'Формат файла: Поддерживается только формат .xlsx'
      },
      recordLimit: 'Количество записей для импорта ({actual} записей) превышает лимит ({max} записей), пожалуйста, импортируйте пакетами'
    },

    // Загрузка
    upload: {
      text: 'Перетащите файл сюда или нажмите для загрузки',
      picture: 'Нажмите для загрузки изображения',
      file: 'Нажмите для загрузки файла',
      icon: 'Нажмите для загрузки иконки',
      limit: {
        size: 'Размер файла не должен превышать {size}',
        type: 'Поддерживается только формат {type}'
      }
    },

    // ==================== Кнопки действий ====================
    actions: {
      // === Базовые кнопки действий ===
      add: 'Добавить',           // @btn-add-color
      edit: 'Редактировать',         // @btn-edit-color
      delete: 'Удалить',      // @btn-delete-color
      batchDelete: 'Пакетное удаление', // @btn-batch-delete-color
      view: 'Просмотр',             // @btn-view-color
      clear: 'Очистить',         // @btn-clear-color
      forceOffline: 'Принудительно отключить', // @btn-force-offline-color
      onlineStatus: 'Статус онлайн', // @btn-online-status-color
      loginHistory: 'История входа', // @btn-login-history-color
      sendMail: 'Отправить письмо', // @btn-send-mail-color
      viewMail: 'Просмотр письма', // @btn-view-mail-color
      mailTemplate: 'Шаблон письма', // @btn-mail-template-color
      sendNotification: 'Отправить уведомление', // @btn-send-notification-color
      viewNotification: 'Просмотр уведомления', // @btn-view-notification-color
      notificationSetting: 'Настройки уведомлений', // @btn-notification-setting-color
      sendMessage: 'Отправить сообщение', // @btn-send-message-color
      viewMessage: 'Просмотр сообщения', // @btn-view-message-color
      messageSetting: 'Настройки сообщений', // @btn-message-setting-color

      // === Кнопки работы с данными ===
      import: 'Импорт',       // @btn-import-color
      export: 'Экспорт',       // @btn-export-color
      template: 'Шаблон',       // @btn-template-color
      preview: 'Предпросмотр',        // @btn-preview-color
      download: 'Скачать',  // @btn-download-color
      batchImport: 'Пакетный импорт', // @btn-batch-import-color
      batchExport: 'Пакетный экспорт', // @btn-batch-export-color
      batchPrint: 'Пакетная печать', // @btn-batch-print-color
      batchEdit: 'Пакетное редактирование',  // @btn-batch-edit-color
      batchUpdate: 'Пакетное обновление', // @btn-batch-update-color

      // === Кнопки операций состояния ===
      logging: 'Проверка',     // @btn-audit-color
      revoke: 'Отозвать',    // @btn-revoke-color
      stop: 'Остановить',    // @btn-stop-color
      run: 'Запустить',      // @btn-run-color
      force: 'Принудительно', // @btn-forced-color
      start: 'Запустить',    // @btn-start-color
      suspend: 'Приостановить', // @btn-suspend-color
      resume: 'Возобновить', // @btn-resume-color
      submit: 'Отправить',   // @btn-submit-color
      withdraw: 'Отозвать',  // @btn-withdraw-color
      terminate: 'Завершить', // @btn-terminate-color

      // === Системные кнопки ===
      generate: 'Сгенерировать',      // @btn-generate-color
      refresh: 'Обновить',    // @btn-refresh-color
      info: 'Информация',      // @btn-info-color
      log: 'Журнал',           // @btn-log-color
      chat: 'Чат',          // @btn-chat-color
      copy: 'Копировать',           // @btn-copy-color
      execute: 'Выполнить',      // @btn-execute-color
      resetPwd: 'Сбросить пароль', // @btn-reset-pwd-color
      open: 'Открыть',           // @btn-open-color
      close: 'Закрыть',          // @btn-close-color
      more: 'Еще',             // @btn-more-color
      density: 'Плотность',       // @btn-density-color
      columnSetting: 'Настройка столбцов', // @btn-column-setting-color

      // === Расширенные кнопки ===
      search: 'Поиск',     // @btn-search-color
      filter: 'Фильтр',        // @btn-filter-color
      sort: 'Сортировка',            // @btn-sort-color
      config: 'Настройки',     // @btn-config-color
      save: 'Сохранить',      // @btn-save-color
      cancel: 'Отмена',        // @btn-cancel-color
      upload: 'Загрузить',    // @btn-upload-color
      print: 'Печать',        // @btn-print-color
      help: 'Справка',             // @btn-help-color
      share: 'Поделиться',        // @btn-share-color
      lock: 'Заблокировать',      // @btn-lock-color
      sync: 'Синхронизация',     // @btn-sync-color
      expand: 'Развернуть',     // @btn-expand-color
      collapse: 'Свернуть',      // @btn-collapse-color
      approve: 'Утвердить',     // @btn-approve-color
      reject: 'Отклонить',        // @btn-reject-color
      comment: 'Комментарий',     // @btn-comment-color
      attach: 'Прикрепить',        // @btn-attach-color

      // === Кнопки языковой поддержки ===
      translate: 'Перевод',    // @btn-translate-color
      langSwitch: 'Переключение языка', // @btn-lang-switch-color
      dict: 'Словарь',     // @btn-dict-color

      // === Кнопки анализа данных ===
      analyze: 'Анализ',      // @btn-analyze-color
      chart: 'График',       // @btn-chart-color
      report: 'Отчет',        // @btn-report-color
      dashboard: 'Панель управления', // @btn-dashboard-color
      statistics: 'Статистика', // @btn-statistics-color
      forecast: 'Прогноз',    // @btn-forecast-color
      compare: 'Сравнение',      // @btn-compare-color

      // === Кнопки рабочего процесса ===
      startFlow: 'Запустить процесс', // @btn-start-flow-color
      endFlow: 'Завершить процесс', // @btn-end-flow-color
      suspendFlow: 'Приостановить процесс', // @btn-suspend-flow-color
      resumeFlow: 'Возобновить процесс', // @btn-resume-flow-color
      transfer: 'Передать',    // @btn-transfer-color
      delegate: 'Делегировать',      // @btn-delegate-color
      notify: 'Уведомить',        // @btn-notify-color
      urge: 'Напомнить',             // @btn-urge-color
      sign: 'Подписать',            // @btn-sign-color
      countersign: 'Согласовать', // @btn-countersign-color

      // === Кнопки для мобильных устройств ===
      scan: 'Сканировать',          // @btn-scan-color
      location: 'Местоположение', // @btn-location-color
      call: 'Позвонить',          // @btn-call-color
      photo: 'Сделать фото', // @btn-photo-color
      voice: 'Голос',            // @btn-voice-color
      faceId: 'Face ID',      // @btn-face-id-color
      fingerPrint: 'Отпечаток пальца', // @btn-finger-print-color

      // === Кнопки социального взаимодействия ===
      follow: 'Подписаться',         // @btn-follow-color
      collect: 'Собрать',     // @btn-collect-color
      like: 'Нравится',          // @btn-like-color
      forward: 'Переслать',    // @btn-forward-color
      at: '@',                  // @btn-at-color
      group: 'Группа',          // @btn-group-color
      team: 'Команда',           // @btn-team-color

      // === Кнопки безопасности ===
      verifyCode: 'Код проверки', // @btn-verify-code-color
      bind: 'Привязать',             // @btn-bind-color
      unbind: 'Отвязать',         // @btn-unbind-color
      authorize: 'Авторизовать',   // @btn-authorize-color
      deauthorize: 'Отменить авторизацию', // @btn-deauthorize-color

      // === Расширенные кнопки ===
      version: 'Версия',       // @btn-version-color
      history: 'История',    // @btn-history-color
      restore: 'Восстановить',     // @btn-restore-color
      archive: 'Архивировать',      // @btn-archive-color
      unarchive: 'Разархивировать', // @btn-unarchive-color
      merge: 'Объединить',       // @btn-merge-color
      split: 'Разделить',         // @btn-split-color

      // === Кнопки управления системой ===
      backup: 'Резервное копирование',    // @btn-backup-color
      restoreSys: 'Восстановление системы', // @btn-restore-sys-color
      clean: 'Очистить',        // @btn-clean-color
      optimize: 'Оптимизировать',    // @btn-optimize-color
      monitor: 'Мониторинг',    // @btn-monitor-color
      diagnose: 'Диагностика', // @btn-diagnose-color
      maintain: 'Обслуживание'     // @btn-maintain-color
    },

    // ==================== Результаты и сообщения ====================
    // Статус результата
    result: {
      success: 'Операция успешна',
      failed: 'Операция не выполнена',
      warning: 'Предупреждение',
      info: 'Информация',
      error: 'Ошибка'
    },

    // Сообщения
    message: {
      loading: 'Обработка...',
      saving: 'Сохранение...',
      submitting: 'Отправка...',
      deleting: 'Удаление...',
      operationSuccess: 'Операция успешна',
      operationFailed: 'Операция не выполнена',
      deleteConfirm: 'Вы уверены, что хотите удалить?',
      deleteSuccess: 'Удаление успешно',
      deleteFailed: 'Ошибка удаления',
      createSuccess: 'Создание успешно',
      createFailed: 'Ошибка создания',
      updateSuccess: 'Обновление успешно',
      updateFailed: 'Ошибка обновления',
      networkError: 'Ошибка сетевого подключения, проверьте сеть',
      systemError: 'Системная ошибка',
      timeout: 'Таймаут запроса',
      invalidResponse: 'Неверный формат ответа',
      backendNotStarted: 'Сервис бэкенда не запущен. Проверьте статус сервиса',
      invalidRequest: 'Неверные параметры запроса',
      unauthorized: 'Не авторизован. Пожалуйста, войдите снова',
      forbidden: 'Доступ запрещен',
      notFound: 'Запрошенный ресурс не существует',
      serverError: 'Внутренняя ошибка сервера',
      httpError: {
        400: 'Неверные параметры запроса',
        401: 'Не авторизован. Пожалуйста, войдите снова',
        403: 'Доступ запрещен',
        404: 'Запрошенный ресурс не существует',
        500: 'Внутренняя ошибка сервера',
        502: 'Неверный шлюз',
        503: 'Сервис недоступен',
        504: 'Таймаут шлюза'
      },
      loadFailed: 'Ошибка загрузки',
      forceOfflineConfirm: 'Вы уверены, что хотите принудительно отключить этого пользователя?',
      forceOfflineSuccess: 'Пользователь успешно отключен',
      forceOfflineFailed: 'Ошибка при отключении пользователя'
    },

    // ==================== Тексты компонентов ====================
    // Вкладки
    tab: {
      // === Базовая информация ===
      basic: 'Базовая информация',
      profile: 'Профиль',
      avatar: 'Настройка аватара',
      password: 'Настройка пароля',
      security: 'Настройки безопасности',

      // === Генерация кода ===
      codegen: 'Генерация кода',
      codegenBasic: 'Базовые настройки',
      codegenField: 'Настройки полей',
      codegenPreview: 'Предпросмотр',
      codegenTemplate: 'Настройки шаблона',
      codegenImport: 'Настройки импорта',
      
      // === Рабочий процесс ===
      workflow: 'Рабочий процесс',
      flowDesign: 'Проектирование процесса',
      flowDeploy: 'Развертывание процесса',
      flowInstance: 'Экземпляр процесса',
      flowTask: 'Управление задачами',
      flowForm: 'Настройка формы',
      flowNotify: 'Уведомления',
      
      // === Управление системой ===
      permission: 'Права доступа',
      log: 'Журнал операций',
      dept: 'Отделы и должности',
      role: 'Роли и права',
      config: 'Настройки параметров',
      task: 'Планировщик задач',
      monitor: 'Мониторинг системы'
    },

    // Тексты кнопок
    button: {
      submit: 'Отправить',
      confirm: 'Подтвердить',
      ok: 'OK',
      cancel: 'Отмена',
      close: 'Закрыть',
      reset: 'Сбросить',
      clear: 'Очистить',
      save: 'Сохранить',
      delete: 'Удалить'
    }
  },

  // ==================== Компонент выбора ====================
  select: {
    loadMore: 'Загрузить еще',
    loading: 'Загрузка...',
    notFound: 'Данные не найдены',
    selected: 'Выбрано {count}',
    selectedTotal: 'Всего {total}',
    clear: 'Очистить',
    search: 'Поиск',
    all: 'Все',
    // Сообщения об ошибках
    loadError: 'Ошибка загрузки данных',
    searchError: 'Ошибка поиска',
    networkError: 'Ошибка подключения к сети',
    serverError: 'Ошибка сервера',
    unknownError: 'Неизвестная ошибка'
  }
}


