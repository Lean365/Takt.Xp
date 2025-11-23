export default {
  workflow: {
    definition: {
      title: 'Определение Рабочего Процесса',
      list: {
        title: 'Список Определений Рабочего Процесса',
        search: {
          workflowName: 'Название Рабочего Процесса',
          workflowCategory: 'Тип Рабочего Процесса',
          status: 'Статус'
        },
        table: {
          workflowId: 'ID Рабочего Процесса',
          workflowName: 'Название Рабочего Процесса',
          workflowCategory: 'Тип Рабочего Процесса',
          status: 'Статус',
          createTime: 'Время Создания',
          updateTime: 'Время Обновления',
          actions: 'Действия'
        },
        actions: {
          view: 'Просмотр',
          edit: 'Редактировать',
          delete: 'Удалить',
          refresh: 'Обновить',
          import: 'Импорт',
          export: 'Экспорт'
        },
        status: {
          normal: 'Нормальный',
          disable: 'Отключен'
        }
      },
      form: {
        title: {
          create: 'Создание Определения Рабочего Процесса',
          edit: 'Редактирование Определения Рабочего Процесса'
        },
        fields: {
          workflowName: 'Название Рабочего Процесса',
          workflowCategory: 'Тип Рабочего Процесса',
          workflowVersion: 'Версия',
          status: 'Статус',
          remark: 'Примечание'
        },
        rules: {
          workflowName: {
            required: 'Введите название рабочего процесса'
          },
          workflowCategory: {
            required: 'Выберите тип рабочего процесса'
          },
          status: {
            required: 'Выберите статус'
          }
        },
        buttons: {
          submit: 'Отправить',
          cancel: 'Отмена'
        }
      },
      detail: {
        title: 'Детали Определения Рабочего Процесса',
        basic: {
          title: 'Основная Информация',
          workflowName: 'Название Рабочего Процесса',
          workflowCategory: 'Тип Рабочего Процесса',
          workflowVersion: 'Версия',
          status: 'Статус',
          remark: 'Примечание'
        },
        node: {
          title: 'Информация об Узлах',
          nodeName: 'Название Узла',
          nodeType: 'Тип Узла',
          assigneeType: 'Тип Исполнителя',
          formType: 'Тип Формы'
        },
        form: {
          title: 'Информация о Форме',
          fieldName: 'Название Поля',
          fieldType: 'Тип Поля',
          required: 'Обязательность',
          remark: 'Примечание'
        }
      }
    }
  }
} 