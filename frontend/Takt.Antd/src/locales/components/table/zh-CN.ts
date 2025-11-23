export default {
    // ==================== 表格操作 ====================
    table: {
      viewMode: {
        normal: '传统表格',
        transpose: '转置表格'
      },
      columns: {
        id: 'ID',
        remark: '备注',
        createBy: '创建者',
        createTime: '创建时间',
        updateBy: '更新者',
        updateTime: '更新时间',
        deleteBy: '删除者',
        deleteTime: '删除时间',
        isDeleted: '是否删除',
        operation: '操作',
      },
      config: {
        density: {
          default: '默认',
          middle: '中等',
          small: '紧凑'
        },
        columnSetting: '列设置'
      },
      pagination: {
        total: '共 {total} 条',
        current: '第 {current} 页',
        pageSize: '每页 {pageSize} 条',
        jump: '跳至'
      },
      empty: '暂无数据',
      loading: '加载中...',
      selectAll: '全选',
      selected: '已选择 {total} 项'
    }
} 