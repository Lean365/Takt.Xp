export default {
    generator: {
      template: {
        title: 'Шаблон генерации кода',  
        fields: {
            templateName: 'Название шаблона',
            templateOrmType: 'ORM фреймворк',
            templateCodeType: 'Тип кода',
            templateCategory: 'Категория шаблона',
            templateLanguage: 'Язык программирования',
            templateVersion: 'Номер версии',
            fileName: 'Имя файла',
            templateContent: 'Содержимое шаблона',
            status: 'Статус',
            remark: 'Примечание',
        },
        placeholder: {
            templateName: 'Пожалуйста, введите название шаблона',
            templateOrmType: 'Пожалуйста, выберите ORM фреймворк',
            templateCodeType: 'Пожалуйста, выберите тип кода',
            templateCategory: 'Пожалуйста, выберите категорию шаблона',
            templateLanguage: 'Пожалуйста, выберите язык программирования',
            templateVersion: 'Пожалуйста, введите номер версии',
            fileName: 'Пожалуйста, введите имя файла',
            templateContent: 'Пожалуйста, введите содержимое шаблона',
            remark: 'Пожалуйста, введите примечание',
            validation: {
                required: 'Пожалуйста, введите {field}',
                length: 'Длина {field} не может превышать {length} символов',
                minLength: 'Длина {field} не может быть меньше {min} символов',
                pascalCase: '{field} должен использовать Pascal case (первая буква заглавная, только буквы)'
            }
        },
        dialog: {
            create: 'Добавить шаблон',
            update: 'Редактировать шаблон'
        },
        messages: {
            success: 'Операция успешна',
            error: 'Операция не удалась',
            warning: 'Предупреждение операции',
            info: 'Информация об операции',
            confirm: 'Подтвердить',
        },
    }
} }