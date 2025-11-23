export default {
  routine: {
    mailtmpl: {
      // Основная информация
      templateName: 'Название шаблона',
      templateType: 'Тип шаблона',
      templateSubject: 'Тема шаблона',
      templateContent: 'Содержание шаблона',
      templateStatus: 'Статус шаблона',
      templateParams: 'Параметры шаблона',
      remark: 'Примечание',
      createTime: 'Время создания',
      updateTime: 'Время обновления',

      // Кнопки действий
      add: 'Добавить шаблон',
      edit: 'Редактировать шаблон',
      delete: 'Удалить шаблон',
      batchDelete: 'Массовое удаление',
      export: 'Экспорт',
      import: 'Импорт',
      downloadTemplate: 'Скачать шаблон',
      preview: 'Предпросмотр',
      send: 'Отправить',

      // Плейсхолдеры формы
      placeholder: {
        templateName: 'Введите название шаблона',
        templateType: 'Выберите тип шаблона',
        templateSubject: 'Введите тему шаблона',
        templateContent: 'Введите содержание шаблона',
        templateStatus: 'Выберите статус шаблона',
        templateParams: 'Введите параметры шаблона',
        remark: 'Введите примечание',
        startTime: 'Время начала',
        endTime: 'Время окончания'
      },

      // Валидация формы
      validation: {
        templateName: {
          required: 'Введите название шаблона',
          maxLength: 'Название шаблона не может превышать 100 символов'
        },
        templateType: {
          required: 'Выберите тип шаблона',
          maxLength: 'Тип шаблона не может превышать 50 символов'
        },
        templateSubject: {
          required: 'Введите тему шаблона',
          maxLength: 'Тема шаблона не может превышать 200 символов'
        },
        templateContent: {
          required: 'Введите содержание шаблона',
          maxLength: 'Содержание шаблона не может превышать 4000 символов'
        },
        templateStatus: {
          required: 'Выберите статус шаблона'
        }
      },

      // Результаты операций
      message: {
        success: {
          add: 'Успешно добавлено',
          edit: 'Успешно обновлено',
          delete: 'Успешно удалено',
          batchDelete: 'Успешно удалено массово',
          export: 'Успешно экспортировано',
          import: 'Успешно импортировано',
          send: 'Успешно отправлено'
        },
        failed: {
          add: 'Ошибка добавления',
          edit: 'Ошибка обновления',
          delete: 'Ошибка удаления',
          batchDelete: 'Ошибка массового удаления',
          export: 'Ошибка экспорта',
          import: 'Ошибка импорта',
          send: 'Ошибка отправки'
        }
      },

      // Страница деталей
      detail: {
        title: 'Детали шаблона электронной почты'
      }
    }
  }
}
