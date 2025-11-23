export default {
  table: {
    viewMode: {
      normal: '傳統表格',
      transpose: '轉置表格'
    },
    columns: {
      id: 'ID',
      remark: '備註',
      createBy: '創建者',
      createTime: '創建時間',
      updateBy: '更新者',
      updateTime: '更新時間',
      deleteBy: '刪除者',
      deleteTime: '刪除時間',
      isDeleted: '是否刪除',
      operation: '操作',
    },
    config: {
      density: {
        default: '默認',
        middle: '中等',
        small: '緊湊'
      },
      columnSetting: '列設置'
    },
    pagination: {
      total: '共 {total} 條',
      current: '第 {current} 頁',
      pageSize: '每頁 {pageSize} 條',
      jump: '跳至'
    },
    empty: '暫無數據',
    loading: '加載中...',
    selectAll: '全選',
    selected: '已選擇 {total} 項'
  }
}