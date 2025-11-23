export default {
    generator: {
      config: {
        title: 'Configuración de Generación de Código',  
        fields: {
            genConfigName: 'Nombre de Configuración',
            author: 'Autor',
            moduleName: 'Nombre del Módulo',
            projectName: 'Nombre del Proyecto',
            businessName: 'Nombre del Negocio',
            functionName: 'Nombre de la Función',
            genMethod: 'Método de Generación',
            genTplType: 'Tipo de Plantilla',
            genPath: 'Ruta',
            options: 'Opciones',
            status: 'Estado',
            dateRange: 'Rango de Fechas',
        },
        placeholder: {
            genConfigName: 'Por favor ingrese el nombre de configuración',
            author: 'Por favor ingrese el autor',
            moduleName: 'Por favor ingrese el nombre del módulo',
            projectName: 'Por favor ingrese el nombre del proyecto',
            businessName: 'Por favor ingrese el nombre del negocio',
            functionName: 'Por favor ingrese el nombre de la función',
            genMethod: 'Por favor seleccione el método de generación',
            genTplType: 'Por favor seleccione el tipo de plantilla',
            genPath: 'Por favor ingrese la ruta',
            options: 'Por favor ingrese las opciones',
            status: 'Por favor seleccione el estado',
            dateRange: 'Por favor seleccione el rango de fechas',
            validation: {
                length: 'La longitud de {{field}} no puede exceder {{length}}',
                required: 'Por favor ingrese {{field}}',
                minlength: 'La longitud de {{field}} no puede ser menor que {{min}}',
                maxlength: 'La longitud de {{field}} no puede ser mayor que {{max}}',
                pattern: 'El formato de {{field}} es incorrecto',
            }
        },
        messages: {
            success: 'Operación exitosa',
            error: 'Operación fallida',
            warning: 'Advertencia de operación',
            info: 'Información de operación',
        }
    }
} }