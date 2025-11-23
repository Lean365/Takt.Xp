export default {
  workflow: {
    instance: {
      title: 'Экземпляр Рабочего Процесса',
      list: {
        title: 'Список Экземпляров Рабочих Процессов',
        search: {
          name: 'Название Рабочего Процесса',
          key: 'Ключ Рабочего Процесса',
          version: 'Версия',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания'
        },
        table: {
          name: 'Название Рабочего Процесса',
          key: 'Ключ Рабочего Процесса',
          version: 'Версия',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания',
          duration: 'Длительность',
          actions: 'Действия'
        },
        actions: {
          view: 'Просмотр',
          delete: 'Удалить',
          terminate: 'Завершить',
          export: 'Экспорт',
          import: 'Импорт',
          refresh: 'Обновить'
        },
        status: {
          running: 'Выполняется',
          completed: 'Завершено',
          terminated: 'Прервано',
          failed: 'Ошибка'
        }
      },
      form: {
        title: {
          create: 'Создание Экземпляра Рабочего Процесса',
          import: 'Импорт Экземпляра Рабочего Процесса'
        },
        fields: {
          workflowDefinitionId: 'Определение Рабочего Процесса',
          variables: 'Настройка Переменных'
        },
        rules: {
          workflowDefinitionId: {
            required: 'Пожалуйста, выберите определение рабочего процесса'
          }
        },
        buttons: {
          submit: 'Отправить',
          cancel: 'Отмена'
        }
      },
      detail: {
        title: 'Детали Экземпляра Рабочего Процесса',
        basic: {
          title: 'Основная Информация',
          name: 'Название Рабочего Процесса',
          key: 'Ключ Рабочего Процесса',
          version: 'Версия',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания',
          duration: 'Длительность'
        },
        nodes: {
          title: 'Информация об Узле',
          name: 'Название Узла',
          type: 'Тип Узла',
          status: 'Статус',
          startTime: 'Время Начала',
          endTime: 'Время Окончания',
          duration: 'Длительность',
          input: 'Вход',
          output: 'Выход',
          error: 'Ошибка'
        },
        actions: {
          back: 'Назад'
        }
      },
      terminate: {
        title: 'Завершение Экземпляра Рабочего Процесса',
        confirm: 'Вы уверены, что хотите завершить этот экземпляр?',
        reason: 'Причина Завершения',
        buttons: {
          submit: 'Отправить',
          cancel: 'Отмена'
        }
      }
    }
  }
} 