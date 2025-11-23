export default {
  logging: {
    server: {
      title: 'Server Monitoring',
      refresh: 'Refresh',
      refreshResult: {
        success: 'Data refreshed successfully',
        failed: 'Data not refreshed'
      },
      resource: {
        title: 'System resources',
        cpu: 'CPU usage',
        memory: 'Memory usage',
        disk: 'Disk usage'
      },
      system: {
        title: 'System information',
        os: 'Operating system',
        architecture: 'Architecture',
        version: 'Version',
        processor: {
          name: 'Processor name',
          count: 'Processor count',
          unit: 'unit'
        },
        startup: {
          time: 'Start time',
          uptime: 'Uptime'
        }
      },
      dotnet: {
        title: 'Runtime information',
        runtime: {
          title: 'Runtime information',
          directory: 'Directory',
          version: 'Version',
          framework: 'Framework'
        },
        clr: {
          title: 'CLR information',
          version: 'Version'
        }
      },
      network: {
        title: 'Network information',
        adapter: 'Network adapter',
        mac: 'MAC address',
        ip:{
          address: 'IP address',
          location: 'Location',
        },
        rate:
        {
          send: 'Send',
          receive: 'Receive'
        }
      }
    }
  }
}

