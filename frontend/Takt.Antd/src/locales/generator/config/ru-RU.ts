export default {
    generator: {
      config: {
        title: 'Конфигурация генерации кода',  
        fields: {
            genConfigName: 'Название конфигурации',
            author: 'Автор',
            moduleName: 'Название модуля',
            projectName: 'Название проекта',
            businessName: 'Название бизнеса',
            functionName: 'Название функции',
            genMethod: 'Метод генерации',
            genTplType: 'Тип шаблона',
            genPath: 'Путь',
            options: 'Опции',
            status: 'Статус',
            dateRange: 'Диапазон дат',
        },
        placeholder: {
            genConfigName: 'Пожалуйста, введите название конфигурации',
            author: 'Пожалуйста, введите автора',
            moduleName: 'Пожалуйста, введите название модуля',
            projectName: 'Пожалуйста, введите название проекта',
            businessName: 'Пожалуйста, введите название бизнеса',
            functionName: 'Пожалуйста, введите название функции',
            genMethod: 'Пожалуйста, выберите метод генерации',
            genTplType: 'Пожалуйста, выберите тип шаблона',
            genPath: 'Пожалуйста, введите путь',
            options: 'Пожалуйста, введите опции',
            status: 'Пожалуйста, выберите статус',
            dateRange: 'Пожалуйста, выберите диапазон дат',
            validation: {
                length: 'Длина {{field}} не может превышать {{length}}',
                required: 'Пожалуйста, введите {{field}}',
                minlength: 'Длина {{field}} не может быть меньше {{min}}',
                maxlength: 'Длина {{field}} не может быть больше {{max}}',
                pattern: 'Формат {{field}} неверен',
            }
        },
        messages: {
            success: 'Операция успешна',
            error: 'Операция не удалась',
            warning: 'Предупреждение операции',
            info: 'Информация об операции',
        }
    }
} }