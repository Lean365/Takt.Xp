export default {
  accounting: {
    financial: {
      company: {
        title: 'Управление Компаниями',
        tabs: {
          basicInfo: 'Основная Информация',
          detailInfo: 'Подробная Информация',
          financialInfo: 'Финансовая Информация'
        },
        fields: {
          companyCode: { label: 'Код Компании', placeholder: 'Введите код компании', required: 'Введите код компании' },
          companyName: { label: 'Название Компании', placeholder: 'Введите название компании', required: 'Введите название компании' },
          companyShortName: { label: 'Краткое Название', placeholder: 'Введите краткое название' },
          companyTaxNumber: { label: 'Налоговый Номер', placeholder: 'Введите налоговый номер' },
          companyNature: { label: 'Природа Предприятия', placeholder: 'Выберите природу предприятия', required: 'Выберите природу предприятия' },
          companyIndustryType: { label: 'Тип Отрасли', placeholder: 'Выберите тип отрасли' },
          companyCurrency: { label: 'Валюта', placeholder: 'Выберите валюту', required: 'Выберите валюту' },
          companyLanguage: { label: 'Язык', placeholder: 'Выберите язык', required: 'Выберите язык' },
          companyPhone: { label: 'Телефон', placeholder: 'Введите номер телефона' },
          companyEmail: { label: 'Электронная Почта', placeholder: 'Введите электронную почту' },
          companyWebsite: { label: 'Веб-сайт', placeholder: 'Введите веб-сайт' },
          companyScale: { label: 'Масштаб Компании', placeholder: 'Введите масштаб компании' },
          companyAddress: { label: 'Адрес', placeholder: 'Введите адрес' },
          companyName1: { label: 'Название Компании 1', placeholder: 'Введите название компании 1' },
          companyName2: { label: 'Название Компании 2', placeholder: 'Введите название компании 2' },
          companyName3: { label: 'Название Компании 3', placeholder: 'Введите название компании 3' },
          companyLegalRepresentative: { label: 'Юридический Представитель', placeholder: 'Введите юридического представителя' },
          companyRegisteredCapital: { label: 'Зарегистрированный Капитал', placeholder: 'Введите зарегистрированный капитал' },
          companyBusinessLicense: { label: 'Лицензия на Деятельность', placeholder: 'Введите номер лицензии на деятельность' },
          companyOrganizationCode: { label: 'Код Организации', placeholder: 'Введите код организации' },
          companyUnifiedCreditCode: { label: 'Единый Кредитный Код', placeholder: 'Введите единый кредитный код' },
          companyCity: { label: 'Город', placeholder: 'Введите город' },
          companyRegion: { label: 'Регион', placeholder: 'Введите регион' },
          companyPostalCode: { label: 'Почтовый Код', placeholder: 'Введите почтовый код' },
          companyCountry: { label: 'Страна', placeholder: 'Введите страну' },
          companyFax: { label: 'Факс', placeholder: 'Введите номер факса' },
          establishmentDate: { label: 'Дата Учреждения', placeholder: 'Выберите дату учреждения' },
          dissolutionDate: { label: 'Дата Роспуска', placeholder: 'Выберите дату роспуска' },
          orderNum: { label: 'Номер Порядка', placeholder: 'Введите номер порядка' },
          status: { label: 'Статус', placeholder: 'Выберите статус', required: 'Выберите статус' },
          companyFiscalYearVariant: { label: 'Вариант Фискального Года', placeholder: 'Введите вариант фискального года' },
          companyChartOfAccounts: { label: 'План Счетов', placeholder: 'Введите план счетов' },
          companyFinancialManagementArea: { label: 'Область Финансового Управления', placeholder: 'Введите область финансового управления' },
          companyMaxExchangeRateDeviation: { label: 'Максимальное Отклонение Курса', placeholder: 'Введите максимальное отклонение курса' },
          companyAllocationIdentifier: { label: 'Идентификатор Распределения', placeholder: 'Введите идентификатор распределения' },
          companyRelatedCompany: { label: 'Связанная Компания', placeholder: 'Введите связанную компанию' },
          companyRelatedPlant: { label: 'Связанный Завод', placeholder: 'Введите связанный завод' },
          companyAddressNumber: { label: 'Номер Адреса', placeholder: 'Введите номер адреса' },
          remark: { label: 'Примечание', placeholder: 'Введите примечание' }
        },
        actions: {
          add: 'Добавить Компанию',
          edit: 'Редактировать Компанию',
          delete: 'Удалить Компанию',
          batchDelete: 'Массовое Удаление',
          export: 'Экспорт',
          import: 'Импорт',
          downloadTemplate: 'Скачать Шаблон',
          reset: 'Сброс',
          search: 'Поиск'
        },
        messages: {
          addSuccess: 'Компания успешно добавлена',
          updateSuccess: 'Компания успешно обновлена',
          deleteSuccess: 'Компания успешно удалена',
          batchDeleteSuccess: 'Компании успешно удалены',
          deleteConfirm: 'Вы уверены, что хотите удалить выбранную компанию?',
          batchDeleteConfirm: 'Вы уверены, что хотите удалить выбранные компании?',
          loadFailed: 'Ошибка загрузки данных',
          submitFailed: 'Ошибка отправки',
          exportSuccess: 'Экспорт успешен',
          importSuccess: 'Импорт успешен',
          importFailed: 'Ошибка импорта',
          downloadTemplateSuccess: 'Шаблон успешно скачан'
        }
      }
    }
  }
}
