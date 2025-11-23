export default {
    generator: {
      template: {
        title: 'Code Generation Template',  
        fields: {
            templateName: 'Template Name',
            templateOrmType: 'ORM Framework',
            templateCodeType: 'Code Type',
            templateCategory: 'Template Category',
            templateLanguage: 'Programming Language',
            templateVersion: 'Version Number',
            fileName: 'File Name',
            templateContent: 'Template Content',
            status: 'Status',
            remark: 'Remark',
        },
        placeholder: {
            templateName: 'Please enter template name',
            templateOrmType: 'Please select ORM framework',
            templateCodeType: 'Please select code type',
            templateCategory: 'Please select template category',
            templateLanguage: 'Please select programming language',
            templateVersion: 'Please enter version number',
            fileName: 'Please enter file name',
            templateContent: 'Please enter template content',
            remark: 'Please enter remark',
            validation: {
                required: 'Please enter {field}',
                length: '{field} length cannot exceed {length} characters',
                minLength: '{field} length cannot be less than {min} characters',
                pascalCase: '{field} must use Pascal case (first letter uppercase, letters only)'
            }
        },
        dialog: {
            create: 'Add Template',
            update: 'Edit Template'
        },
        messages: {
            success: 'Operation successful',
            error: 'Operation failed',
            warning: 'Operation warning',
            info: 'Operation information',
            confirm: 'Confirm',
        },
    }
} }