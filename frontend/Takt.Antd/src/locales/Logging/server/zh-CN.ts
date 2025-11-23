export default {
  logging: {
    server: {
      title: '服务器监控',
      refresh: '刷新',
      refreshResult: {
        success: '数据刷新成功',
        failed: '数据刷新失败'
      },
      resource: {
        title: '系统资源',
        cpu: 'CPU使用率',
        memory: '内存使用率',
        disk: '磁盘使用率'
      },
      system: {
        title: '系统信息',
        os: '操作系统',
        architecture: '架构',
        version: '版本',
        processor: {
          name: '处理器名称',
          count: '处理器数量',
          unit: '个'
        },
        startup: {
          time: '启动时间',
          uptime: '运行时间'
        }
      },
      dotnet: {
        title: '运行信息',
        runtime: {
          title: '运行时信息',
          directory: '目录',
          version: '版本',
          framework: '框架'
        },
        clr: {
          title: 'CLR信息',
          version: '版本'
        }
      },
      network: {
        title: '网络信息',
        adapter: '网络适配器',
        mac: 'MAC地址',
        ip:{
          address: 'IP地址',
          location: '位置',
        },
        rate:
        {
          send: '发送',
          receive: '接收'
        }
      }
    }
  }
}

