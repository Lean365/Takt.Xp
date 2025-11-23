export default {
  logging: {
    server: {
      title: '伺服器監控',
      refresh: '刷新',
      refreshResult: {
        success: '資料刷新成功',
        failed: '資料刷新失敗'
      },
      resource: {
        title: '系統資源',
        cpu: 'CPU使用率',
        memory: '記憶體使用率',
        disk: '磁盤使用率'
      },
      system: {
        title: '系統資訊',
        os: '作業系統',
        architecture: '架構',
        version: '版本',
        processor: {
          name: '處理器名稱',
          count: '處理器數量',
          unit: '個'
        },
        startup: {
          time: '啟動時間',
          uptime: '運行時間'
        }
      },
      dotnet: {
        title: '運行信息',
        runtime: {
          title: '運行時信息',
          directory: '目錄',
          version: '版本',
          framework: '框架'
        },
        clr: {
          title: 'CLR信息',
          version: '版本'
        }
      },
      network: {
        title: '網路資訊',
        adapter: '網路適配器',
        mac: 'MAC位址',
        ip:{
          address: 'IP位址',
          location: '地點',
        },
        rate:
        {
          send: '發送',
          receive: '接收'
        }
      }
    }
  }
}

