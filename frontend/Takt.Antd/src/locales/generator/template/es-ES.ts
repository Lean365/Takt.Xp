export default {
    generator: {
      template: {
        title: 'Plantilla de Generación de Código',  
        fields: {
            templateName: 'Nombre de Plantilla',
            templateOrmType: 'Framework ORM',
            templateCodeType: 'Tipo de Código',
            templateCategory: 'Categoría de Plantilla',
            templateLanguage: 'Lenguaje de Programación',
            templateVersion: 'Número de Versión',
            fileName: 'Nombre de Archivo',
            templateContent: 'Contenido de Plantilla',
            status: 'Estado',
            remark: 'Observación',
        },
        placeholder: {
            templateName: 'Por favor ingrese el nombre de la plantilla',
            templateOrmType: 'Por favor seleccione el framework ORM',
            templateCodeType: 'Por favor seleccione el tipo de código',
            templateCategory: 'Por favor seleccione la categoría de plantilla',
            templateLanguage: 'Por favor seleccione el lenguaje de programación',
            templateVersion: 'Por favor ingrese el número de versión',
            fileName: 'Por favor ingrese el nombre del archivo',
            templateContent: 'Por favor ingrese el contenido de la plantilla',
            remark: 'Por favor ingrese la observación',
            validation: {
                required: 'Por favor ingrese {field}',
                length: 'La longitud de {field} no puede exceder {length} caracteres',
                minLength: 'La longitud de {field} no puede ser menor que {min} caracteres',
                pascalCase: '{field} debe usar Pascal case (primera letra mayúscula, solo letras)'
            }
        },
        dialog: {
            create: 'Agregar Plantilla',
            update: 'Editar Plantilla'
        },
        messages: {
            success: 'Operación exitosa',
            error: 'Operación fallida',
            warning: 'Advertencia de operación',
            info: 'Información de operación',
            confirm: 'Confirmar',
        },
    }
} }