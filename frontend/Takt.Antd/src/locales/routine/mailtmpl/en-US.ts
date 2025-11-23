export default {
  routine: {
    mailtmpl: {
      // Basic Information
      templateName: 'Template Name',
      templateType: 'Template Type',
      templateSubject: 'Template Subject',
      templateContent: 'Template Content',
      templateStatus: 'Template Status',
      templateParams: 'Template Parameters',
      remark: 'Remark',
      createTime: 'Create Time',
      updateTime: 'Update Time',

      // Action Buttons
      add: 'Add Template',
      edit: 'Edit Template',
      delete: 'Delete Template',
      batchDelete: 'Batch Delete',
      export: 'Export',
      import: 'Import',
      downloadTemplate: 'Download Template',
      preview: 'Preview',
      send: 'Send',

      // Form Placeholders
      placeholder: {
        templateName: 'Please enter template name',
        templateType: 'Please select template type',
        templateSubject: 'Please enter template subject',
        templateContent: 'Please enter template content',
        templateStatus: 'Please select template status',
        templateParams: 'Please enter template parameters',
        remark: 'Please enter remark',
        startTime: 'Start Time',
        endTime: 'End Time'
      },

      // Form Validation
      validation: {
        templateName: {
          required: 'Please enter template name',
          maxLength: 'Template name cannot exceed 100 characters'
        },
        templateType: {
          required: 'Please select template type',
          maxLength: 'Template type cannot exceed 50 characters'
        },
        templateSubject: {
          required: 'Please enter template subject',
          maxLength: 'Template subject cannot exceed 200 characters'
        },
        templateContent: {
          required: 'Please enter template content',
          maxLength: 'Template content cannot exceed 4000 characters'
        },
        templateStatus: {
          required: 'Please select template status'
        }
      },

      // Operation Results
      message: {
        success: {
          add: 'Added successfully',
          edit: 'Updated successfully',
          delete: 'Deleted successfully',
          batchDelete: 'Batch deleted successfully',
          export: 'Exported successfully',
          import: 'Imported successfully',
          send: 'Sent successfully'
        },
        failed: {
          add: 'Failed to add',
          edit: 'Failed to update',
          delete: 'Failed to delete',
          batchDelete: 'Failed to batch delete',
          export: 'Failed to export',
          import: 'Failed to import',
          send: 'Failed to send'
        }
      },

      // Detail Page
      detail: {
        title: 'Email Template Details'
      }
    }
  }
} 