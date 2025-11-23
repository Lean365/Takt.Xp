export default {
  table: {
    viewMode: {
      normal: '従来のテーブル',
      transpose: '転置テーブル'
    },
    columns: {
      id: 'ID',
      remark: '備考',
      createBy: '作成者',
      createTime: '作成時間',
      updateBy: '更新者',
      updateTime: '更新時間',
      deleteBy: '削除者',
      deleteTime: '削除時間',
      isDeleted: '削除済み',
      operation: '操作',
    },
    config: {
      density: {
        default: 'デフォルト',
        middle: '中',
        small: 'コンパクト'
      },
      columnSetting: '列設定'
    },
    pagination: {
      total: '全 {total} 件',
      current: '{current} ページ',
      pageSize: '1ページ {pageSize} 件',
      jump: '移動'
    },
    empty: 'データなし',
    loading: '読み込み中...',
    selectAll: 'すべて選択',
    selected: '{total} 項目を選択'
  }
}