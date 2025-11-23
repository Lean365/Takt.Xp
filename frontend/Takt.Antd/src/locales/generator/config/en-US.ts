export default {
    generator: {
      config: {
        title: 'Code Generation Configuration',  
        fields: {
            genConfigName: 'Configuration Name',
            author: 'Author',
            moduleName: 'Module Name',
            projectName: 'Project Name',
            businessName: 'Business Name',
            functionName: 'Function Name',
            genMethod: 'Generation Method',
            genTplType: 'Template Type',
            genPath: 'Path',
            options: 'Options',
            status: 'Status',
            dateRange: 'Date Range',
        },
        placeholder: {
            genConfigName: 'Please enter configuration name',
            author: 'Please enter author',
            moduleName: 'Please enter module name',
            projectName: 'Please enter project name',
            businessName: 'Please enter business name',
            functionName: 'Please enter function name',
            genMethod: 'Please select generation method',
            genTplType: 'Please select template type',
            genPath: 'Please enter path',
            options: 'Please enter options',
            status: 'Please select status',
            dateRange: 'Please select date range',
            validation: {
                length: '{{field}} length cannot exceed {{length}}',
                required: 'Please enter {{field}}',
                minlength: '{{field}} length cannot be less than {{min}}',
                maxlength: '{{field}} length cannot be greater than {{max}}',
                pattern: '{{field}} format is incorrect',
            }
        },
        messages: {
            success: 'Operation successful',
            error: 'Operation failed',
            warning: 'Operation warning',
            info: 'Operation information',
        }
    }
} }