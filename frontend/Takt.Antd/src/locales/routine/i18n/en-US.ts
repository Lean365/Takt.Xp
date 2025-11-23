export default {
  admin: {
    translation: {
      title: 'Translation Management',
      id: 'Translation ID',
      langCode: {
        label: 'Language Code',
        placeholder: 'Please select language code',
        validation: {
          required: 'Language code cannot be empty'
        }
      },
      module: {
        label: 'Module Name',
        placeholder: 'Please enter module name',
        validation: {
          required: 'Module name cannot be empty'
        }
      },
      key: 'Translation Key',
      value: 'Translation Value',
      remark: {
        label: 'Remark',
        placeholder: 'Please enter remark'
      },
      language: 'Language',
      actions: {
        import: 'Import',
        export: 'Export',
        template: 'Download Template',
        refresh: 'Refresh Cache'
      }
    }
  }
} 