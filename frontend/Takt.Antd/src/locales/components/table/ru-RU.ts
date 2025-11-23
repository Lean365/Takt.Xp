export default {
  table: {
    viewMode: {
      normal: 'Традиционная таблица',
      transpose: 'Транспонированная таблица'
    },
    columns: {
      id: 'ID',
      remark: 'Примечание',
      createBy: 'Создано пользователем',
      createTime: 'Время создания',
      updateBy: 'Обновлено пользователем',
      updateTime: 'Время обновления',
      deleteBy: 'Удалено пользователем',
      deleteTime: 'Время удаления',
      isDeleted: 'Удалено',
      operation: 'Операция',
    },
    config: {
      density: {
        default: 'По умолчанию',
        middle: 'Средний',
        small: 'Компактный'
      },
      columnSetting: 'Настройка колонки'
    },
    pagination: {
      total: 'Всего {total} элементов',
      current: 'Страница {current}',
      pageSize: '{pageSize} элементов на странице',
      jump: 'Перейти к'
    },
    empty: 'Нет данных',
    loading: 'Загрузка...',
    selectAll: 'Выбрать все',
    selected: 'Выбрано {total} элементов'
  }
}