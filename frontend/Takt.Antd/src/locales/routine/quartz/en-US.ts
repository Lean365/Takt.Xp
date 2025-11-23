export default {
  routine: {
    quartz: {
      // Basic Information
      jobId: 'Job ID',
      jobName: 'Job Name',
      jobGroup: 'Job Group',
      jobClass: 'Job Class',
      jobMethod: 'Job Method',
      jobParams: 'Job Parameters',
      cronExpression: 'Cron Expression',
      jobStatus: 'Job Status',
      remark: 'Remark',
      createTime: 'Create Time',
      updateTime: 'Update Time',

      // Action Buttons
      add: 'Add Job',
      edit: 'Edit Job',
      delete: 'Delete Job',
      batchDelete: 'Batch Delete',
      export: 'Export',
      import: 'Import',
      downloadTemplate: 'Download Template',
      start: 'Start',
      stop: 'Stop',
      pause: 'Pause',
      resume: 'Resume',
      run: 'Run Now',

      // Form Placeholders
      placeholder: {
        jobId: 'Please enter job ID',
        jobName: 'Please enter job name',
        jobGroup: 'Please enter job group',
        jobClass: 'Please enter job class',
        jobMethod: 'Please enter job method',
        jobParams: 'Please enter job parameters',
        cronExpression: 'Please enter cron expression',
        jobStatus: 'Please select job status',
        remark: 'Please enter remark',
        startTime: 'Start Time',
        endTime: 'End Time'
      },

      // Form Validation
      validation: {
        jobId: {
          required: 'Please enter job ID',
          maxLength: 'Job ID cannot exceed 100 characters'
        },
        jobName: {
          required: 'Please enter job name',
          maxLength: 'Job name cannot exceed 50 characters'
        },
        jobGroup: {
          required: 'Please enter job group',
          maxLength: 'Job group cannot exceed 50 characters'
        },
        jobClass: {
          required: 'Please enter job class',
          maxLength: 'Job class cannot exceed 200 characters'
        },
        jobMethod: {
          required: 'Please enter job method',
          maxLength: 'Job method cannot exceed 50 characters'
        },
        cronExpression: {
          required: 'Please enter cron expression',
          maxLength: 'Cron expression cannot exceed 50 characters'
        },
        jobStatus: {
          required: 'Please select job status'
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
          start: 'Started successfully',
          stop: 'Stopped successfully',
          pause: 'Paused successfully',
          resume: 'Resumed successfully',
          run: 'Run successfully'
        },
        failed: {
          add: 'Failed to add',
          edit: 'Failed to update',
          delete: 'Failed to delete',
          batchDelete: 'Failed to batch delete',
          export: 'Failed to export',
          import: 'Failed to import',
          start: 'Failed to start',
          stop: 'Failed to stop',
          pause: 'Failed to pause',
          resume: 'Failed to resume',
          run: 'Failed to run'
        }
      },

      // Detail Page
      detail: {
        title: 'Job Details'
      }
    }
  }
} 