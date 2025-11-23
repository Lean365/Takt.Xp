export default {
  routine: {
    notice: {
      // Basic Information
      noticeId: 'Notice ID',
      noticeType: 'Notice Type',
      noticeTitle: 'Notice Title',
      noticeContent: 'Notice Content',
      noticeStatus: 'Notice Status',
      noticeParams: 'Notice Parameters',
      remark: 'Remark',
      createTime: 'Create Time',
      updateTime: 'Update Time',

      // Action Buttons
      add: 'Add Notice',
      edit: 'Edit Notice',
      delete: 'Delete Notice',
      batchDelete: 'Batch Delete',
      export: 'Export',
      import: 'Import',
      downloadTemplate: 'Download Template',
      preview: 'Preview',
      send: 'Send',

      // Form Placeholders
      placeholder: {
        noticeId: 'Please enter notice ID',
        noticeType: 'Please select notice type',
        noticeTitle: 'Please enter notice title',
        noticeContent: 'Please enter notice content',
        noticeStatus: 'Please select notice status',
        noticeParams: 'Please enter notice parameters',
        remark: 'Please enter remark',
        startTime: 'Start Time',
        endTime: 'End Time'
      },

      // Form Validation
      validation: {
        noticeId: {
          required: 'Please enter notice ID',
          maxLength: 'Notice ID cannot exceed 100 characters'
        },
        noticeType: {
          required: 'Please select notice type',
          maxLength: 'Notice type cannot exceed 50 characters'
        },
        noticeTitle: {
          required: 'Please enter notice title',
          maxLength: 'Notice title cannot exceed 200 characters'
        },
        noticeContent: {
          required: 'Please enter notice content',
          maxLength: 'Notice content cannot exceed 4000 characters'
        },
        noticeStatus: {
          required: 'Please select notice status'
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
        title: 'Notice Details'
      }
    }
  }
} 