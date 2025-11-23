export default {
  workflow: {
    history: {
      title: 'ワークフロー履歴',
      list: {
        title: 'ワークフロー履歴一覧',
        search: {
          name: 'ワークフロー名',
          key: 'ワークフローキー',
          version: 'バージョン',
          status: 'ステータス',
          startTime: '開始時間',
          endTime: '終了時間'
        },
        table: {
          name: 'ワークフロー名',
          key: 'ワークフローキー',
          version: 'バージョン',
          status: 'ステータス',
          startTime: '開始時間',
          endTime: '終了時間',
          duration: '実行時間',
          actions: '操作'
        },
        actions: {
          view: '表示',
          delete: '削除',
          export: 'エクスポート',
          import: 'インポート',
          refresh: '更新'
        },
        status: {
          running: '実行中',
          completed: '完了',
          terminated: '終了',
          failed: '失敗'
        }
      },
      detail: {
        title: 'ワークフロー履歴詳細',
        basic: {
          title: '基本情報',
          name: 'ワークフロー名',
          key: 'ワークフローキー',
          version: 'バージョン',
          status: 'ステータス',
          startTime: '開始時間',
          endTime: '終了時間',
          duration: '実行時間'
        },
        nodes: {
          title: 'ノード情報',
          name: 'ノード名',
          type: 'ノードタイプ',
          status: 'ステータス',
          startTime: '開始時間',
          endTime: '終了時間',
          duration: '実行時間',
          input: '入力',
          output: '出力',
          error: 'エラー'
        },
        actions: {
          back: '戻る'
        }
      }
    }
  }
} 