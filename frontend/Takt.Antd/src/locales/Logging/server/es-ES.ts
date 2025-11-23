export default {
  logging: {
    server: {
      title: 'Servidor de monitorización',
      refresh: 'Actualizar',
      refreshResult: {
        success: 'Datos actualizados correctamente',
        failed: 'Datos no actualizados'
      },
      resource: {
        title: 'Recursos del sistema',
        cpu: 'Uso de CPU',
        memory: 'Uso de memoria',
        disk: 'Uso de disco'
      },
      system: {
        title: 'Información del sistema',
        os: 'Sistema operativo',
        architecture: 'Arquitectura',
        version: 'Versión',
        processor: {
          name: 'Nombre del procesador',
          count: 'Número de procesadores',
          unit: 'unidad'
        },
        startup: {
          time: 'Hora de inicio',
          uptime: 'Tiempo de ejecución'
        }
      },
      dotnet: {
        title: 'Información de inicio',
        runtime: {
          title: 'Información de inicio',
          directory: 'Directorio',
          version: 'Versión',
          framework: 'Framework'
        },
        clr: {
          title: 'Información CLR',
          version: 'Versión'
        }
      },
      network: {
        title: 'Información de red',
        adapter: 'Adaptador de red',
        mac: 'Dirección MAC',
        ip:{
          address: 'Dirección IP',
          location: 'Ubicación',
        },
        rate:
        {
          send: 'Envío',
          receive: 'Recepción'
        }
      }
    }
  }
}

