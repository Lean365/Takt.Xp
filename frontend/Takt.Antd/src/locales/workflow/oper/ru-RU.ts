export default {
  workflow: {
    history: {
      title: 'История Рабочего Процесса',
      list: {
        title: 'Список Истории Рабочих Процессов',
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
      detail: {
        title: 'Детали Истории Рабочего Процесса',
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
      }
    }
  }
} 