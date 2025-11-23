export default {
  routine: {
    mail: {
      // Basic Information
      mailId: 'Mail ID',
      mailType: 'Mail Type',
      mailSubject: 'Mail Subject',
      mailContent: 'Mail Content',
      mailStatus: 'Mail Status',
      mailParams: 'Mail Parameters',
      remark: 'Remark',
      createTime: 'Create Time',
      updateTime: 'Update Time',

      // Action Buttons
      add: 'Add Mail',
      edit: 'Edit Mail',
      delete: 'Delete Mail',
      batchDelete: 'Batch Delete',
      export: 'Export',
      import: 'Import',
      downloadTemplate: 'Download Template',
      preview: 'Preview',
      send: 'Send',

      // Form Placeholders
      placeholder: {
        mailId: 'Please enter mail ID',
        mailType: 'Please select mail type',
        mailSubject: 'Please enter mail subject',
        mailContent: 'Please enter mail content',
        mailStatus: 'Please select mail status',
        mailParams: 'Please enter mail parameters',
        remark: 'Please enter remark',
        startTime: 'Start Time',
        endTime: 'End Time'
      },

      // Form Validation
      validation: {
        mailId: {
          required: 'Please enter mail ID',
          maxLength: 'Mail ID cannot exceed 100 characters'
        },
        mailType: {
          required: 'Please select mail type',
          maxLength: 'Mail type cannot exceed 50 characters'
        },
        mailSubject: {
          required: 'Please enter mail subject',
          maxLength: 'Mail subject cannot exceed 200 characters'
        },
        mailContent: {
          required: 'Please enter mail content',
          maxLength: 'Mail content cannot exceed 4000 characters'
        },
        mailStatus: {
          required: 'Please select mail status'
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
        title: 'Mail Details'
      }
    }
  }
} 