export default {
  identity: {
    menu: {
      title: 'Управление меню',
      columns: {
        menuName: 'Название меню',
        transKey: 'Ключ I18n',
        parentId: 'Родительское меню',
        orderNum: 'Порядок',
        path: 'Путь маршрута',
        component: 'Путь компонента',
        queryParams: 'Параметры маршрута',
        isExternal: 'Внешняя ссылка',
        isCache: 'Кэш',
        menuType: 'Тип меню',
        visible: 'Видимость',
        status: 'Статус',
        perms: 'Ключ разрешения',
        icon: 'Иконка',
        id: 'ID',
        createBy: 'Создано',
        createTime: 'Время создания',
        updateBy: 'Обновлено',
        updateTime: 'Время обновления',
        deleteBy: 'Удалено',
        deleteTime: 'Время удаления',
        isDeleted: 'Удалено',
        remark: 'Примечание',
        action: 'Действие'
      },
      fields: {
        menuName: {
          label: 'Название меню',
          placeholder: 'Пожалуйста, введите название меню',
          validation: {
            required: 'Пожалуйста, введите название меню',
            length: 'Длина названия должна быть от 2 до 50 символов'
          }
        },
        transKey: {
          label: 'Ключ I18n',
          placeholder: 'Пожалуйста, введите ключ I18n'
        },
        parentId: {
          label: 'Родительское меню',
          placeholder: 'Пожалуйста, выберите родительское меню',
          root: 'Корневое меню'
        },
        orderNum: {
          label: 'Порядок',
          placeholder: 'Пожалуйста, введите порядок',
          validation: {
            required: 'Пожалуйста, введите порядок'
          }
        },
        path: {
          label: 'Путь маршрута',
          placeholder: 'Пожалуйста, введите путь маршрута'
        },
        component: {
          label: 'Путь компонента',
          placeholder: 'Пожалуйста, введите путь компонента'
        },
        queryParams: {
          label: 'Параметры маршрута',
          placeholder: 'Пожалуйста, введите параметры маршрута'
        },
        isExternal: {
          label: 'Внешняя ссылка',
          placeholder: 'Пожалуйста, выберите, является ли это внешней ссылкой',
          options: {
            yes: 'Да',
            no: 'Нет'
          }
        },
        isCache: {
          label: 'Кэш',
          placeholder: 'Пожалуйста, выберите, использовать ли кэш',
          options: {
            yes: 'Да',
            no: 'Нет'
          }
        },
        menuType: {
          label: 'Тип меню',
          options: {
            directory: 'Каталог',
            menu: 'Меню',
            button: 'Кнопка'
          },
          validation: {
            required: 'Пожалуйста, выберите тип меню'
          }
        },
        visible: {
          label: 'Видимость',
          placeholder: 'Пожалуйста, выберите видимость',
          options: {
            show: 'Показать',
            hide: 'Скрыть'
          }
        },
        status: {
          label: 'Статус',
          placeholder: 'Пожалуйста, выберите статус',
          options: {
            normal: 'Нормально',
            disabled: 'Отключено'
          }
        },
        perms: {
          label: 'Ключ разрешения',
          placeholder: 'Пожалуйста, введите ключ разрешения'
        },
        icon: {
          label: 'Иконка меню',
          placeholder: 'Пожалуйста, введите иконку меню'
        },
        }
      },
      dialog: {
        create: 'Добавить меню',
        update: 'Редактировать меню',
        delete: 'Удалить меню'
      },
      operation: {
        add: {
          title: 'Добавить меню',
          success: 'Успешно добавлено',
          failed: 'Ошибка добавления'
        },
        edit: {
          title: 'Редактировать меню',
          success: 'Успешно изменено',
          failed: 'Ошибка изменения'
        },
        delete: {
          title: 'Удалить меню',
          confirm: 'Вы уверены, что хотите удалить это меню?',
          success: 'Успешно удалено',
          failed: 'Ошибка удаления'
        }
      }
    }
  }
