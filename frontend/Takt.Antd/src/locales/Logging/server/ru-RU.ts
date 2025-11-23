export default {
  logging: {
    server: {
      title: 'Сервер мониторинга',
      refresh: 'Обновить',
      refreshResult: {
        success: 'Данные обновлены успешно',
        failed: 'Данные не обновлены'
      },
      resource: {
        title: 'Системные ресурсы',
        cpu: 'Использование CPU',
        memory: 'Использование памяти',
        disk: 'Использование диска'
      },
      system: {
        title: 'Системная информация',
        os: 'Операционная система',
        architecture: 'Архитектура',
        version: 'Версия',
        processor: {
          name: 'Имя процессора',
          count: 'Количество процессоров',
          unit: 'шт'
        },
        startup: {
          time: 'Время запуска',
          uptime: 'Время работы'
        }
      },
      dotnet: {
        title: 'Информация о запуске',
        runtime: {
          title: 'Информация о запуске',
          directory: 'Директория',
          version: 'Версия',
          framework: 'Фреймворк'
        },
        clr: {
          title: 'CLR информация',
          version: 'Версия'
        }
      },
      network: {
        title: 'Информация о сети',
        adapter: 'Сетевой адаптер',
        mac: 'MAC адрес',
        ip:{
          address: 'IP адрес',
          location: 'Местоположение',
        },
        rate:
        {
          send: 'Отправка',
          receive: 'Прием'
        }
      }
    }
  }
}

