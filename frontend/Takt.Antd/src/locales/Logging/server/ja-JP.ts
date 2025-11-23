export default {
  logging: {
    server: {
      title: 'サーバー監視',
      refresh: '更新',
      refreshResult: {
        success: 'データ更新成功',
        failed: 'データ更新失敗'
      },
      resource: {
        title: 'システムリソース',
        cpu: 'CPU使用率',
        memory: 'メモリ使用率',
        disk: 'ディスク使用率'
      },
      system: {
        title: 'システム情報',
        os: 'オペレーティングシステム',
        architecture: 'アーキテクチャ',
        version: 'バージョン',
        processor: {
          name: 'プロセッサ名',
          count: 'プロセッサ数',
          unit: '個'
        },
        startup: {
          time: '起動時間',
          uptime: '実行時間'
        }
      },
      dotnet: {
        title: '运行信息',
        runtime: {
          title: '実行時間情報',
          directory: 'ディレクトリ',
          version: 'バージョン',
          framework: 'フレームワーク'
        },
        clr: {
          title: 'CLR情報',
          version: 'バージョン'
        }
      },
      network: {
        title: '网络信息',
        adapter: 'ネットワークアダプター',
        mac: 'MACアドレス',
        ip:{
          address: 'IPアドレス',
          location: '位置',
        },
        rate:
        {
          send: '送信',
          receive: '受信'
        }
      }
    }
  }
}

