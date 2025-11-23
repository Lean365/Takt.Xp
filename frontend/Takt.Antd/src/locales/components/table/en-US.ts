export default {
  table: {
    viewMode: {
      normal: 'Traditional Table',
      transpose: 'Transpose Table'
    },
    columns: {
      id: 'ID',
      remark: 'Remark',
      createBy: 'Created By',
      createTime: 'Created Time',
      updateBy: 'Updated By',
      updateTime: 'Updated Time',
      deleteBy: 'Deleted By',
      deleteTime: 'Deleted Time',
      isDeleted: 'Is Deleted',
      operation: 'Operation',
    },
    config: {
      density: {
        default: 'Default',
        middle: 'Middle',
        small: 'Compact'
      },
      columnSetting: 'Column Setting'
    },
    pagination: {
      total: 'Total {total} items',
      current: 'Page {current}',
      pageSize: '{pageSize} items per page',
      jump: 'Go to'
    },
    empty: 'No Data',
    loading: 'Loading...',
    selectAll: 'Select All',
    selected: 'Selected {total} items'
  }
}