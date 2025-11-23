export default {
  common: {
    // ==================== System Information ====================
    system: {
      title: 'Black Ice Platform',
      slogan: 'Professional, Efficient, and Secure Enterprise Management System',
      description: 'Modern Enterprise Management System based on .NET 8 and Vue 3',
      copyright: '© 2024Takt(Claude AI). All rights reserved.'
    },

    // ==================== Error Messages ====================
    error: {
      clientError: 'Client request error, please check request parameters',
      systemRestart: 'System is under maintenance, please login again later',
      network: 'Network connection failed, please check your network settings',
      unauthorized: 'Unauthorized access, please login again',
      forbidden: 'Access forbidden',
      notFound: 'Requested resource does not exist',
      badRequest: 'Invalid request parameters',
      serverError: 'Internal server error',
      serviceUnavailable: 'Service temporarily unavailable',
      badGateway: 'Bad gateway',
      gatewayTimeout: 'Gateway timeout',
      unknown: 'Unknown error'
    },

    // ==================== Basic Operations ====================
    // Basic Titles
    title: {
      list: 'List',
      detail: 'Details',
      create: 'Add New',
      edit: 'Edit',
      preview: 'Preview'
    },

    // ==================== Status Definitions ====================
    status: {
      // Basic Status
      base: {
        label: 'Status',
        normal: 'Normal',
        disabled: 'Disabled',
        placeholder: 'Please select status'
      },

      // Yes/No Status
      yesNo: {
        all: 'All',
        yes: 'Yes',
        no: 'No'
      },

      // Visibility Status
      visible: {
        show: 'Show',
        hide: 'Hide'
      },

      // Cache Status
      cache: {
        enabled: 'Enabled',
        disabled: 'Disabled'
      },

      // Online Status
      online: {
        online: 'Online',
        offline: 'Offline'
      },

      // Process Status
      process: {
        pending: 'Pending',
        processing: 'Processing',
        completed: 'Completed',
        failed: 'Failed'
      },

      // Verification Status
      verify: {
        unverified: 'Unverified',
        verified: 'Verified',
        failed: 'Verification Failed'
      },

      // Lock Status
      lock: {
        locked: 'Locked',
        unlocked: 'Unlocked'
      },

      // Publish Status
      publish: {
        draft: 'Draft',
        published: 'Published',
        unpublished: 'Unpublished'
      },

      // Sync Status
      sync: {
        synced: 'Synced',
        unsynced: 'Unsynced',
        syncing: 'Syncing'
      }
    },

    // ==================== Form Operations ====================
    form: {
      required: 'Required',
      optional: 'Optional',
      invalid: 'Invalid',
      // Form Placeholders
      placeholder: {
        select: 'Please select',
        input: 'Please input',
        date: 'Please select date',
        time: 'Please select time'
      },
      // User Form
      user: {
        // Basic Information
        userId: {
          label: 'User ID',
          placeholder: 'Please input user ID'
        },
        userName: {
          label: 'Username',
          placeholder: 'Please input username'
        },
        nickName: {
          label: 'Nickname',
          placeholder: 'Please input nickname'
        },
        realName: {
          label: 'Real Name',
          placeholder: 'Please input real name'
        },
        englishName: {
          label: 'English Name',
          placeholder: 'Please input English name'
        },
        password: {
          label: 'Password',
          placeholder: 'Please input password'
        },
        confirmPassword: {
          label: 'Confirm Password',
          placeholder: 'Please input password again'
        },
        
        // Personal Information
        avatar: {
          label: 'Avatar',
          placeholder: 'Please upload avatar'
        },
        gender: {
          label: 'Gender',
          placeholder: 'Please select gender',
          options: {
            male: 'Male',
            female: 'Female',
            other: 'Other'
          }
        },
        birthday: {
          label: 'Birthday',
          placeholder: 'Please select birthday'
        },
        
        // Contact Information
        email: {
          label: 'Email',
          placeholder: 'Please input email'
        },
        phoneNumber: {
          label: 'Mobile Phone',
          placeholder: 'Please input mobile phone'
        },
        telephone: {
          label: 'Telephone',
          placeholder: 'Please input telephone'
        },
        
        // Organization Information
        deptId: {
          label: 'Department',
          placeholder: 'Please select department'
        },
        roleId: {
          label: 'Role',
          placeholder: 'Please select role'
        },
        postId: {
          label: 'Position',
          placeholder: 'Please select position'
        },
        
        // Account Information
        userType: {
          label: 'User Type',
          placeholder: 'Please select user type',
          options: {
            system: 'System User',
            normal: 'Normal User'
          }
        },
        status: {
          label: 'Status',
          placeholder: 'Please select status'
        },
        loginIp: {
          label: 'Last Login IP',
          placeholder: 'Last Login IP'
        },
        loginDate: {
          label: 'Last Login Time',
          placeholder: 'Last Login Time'
        },
        
        // Other Information
        remark: {
          label: 'Remark',
          placeholder: 'Please input remark'
        },
        sort: {
          label: 'Sort',
          placeholder: 'Please input sort number'
        }
      },
      // Remark Information
      remark: {
        label: 'Remark',
        placeholder: 'Please input remark'
      }
    },

    // ==================== Table Operations ====================
    table: {
      header: {
        operation: 'Operation'
      },
      config: {
        density: {
          default: 'Default',
          middle: 'Medium',
          small: 'Compact'
        },
        columnSetting: 'Column Settings'
      },
      pagination: {
        total: 'Total {total} items',
        current: 'Page {current}',
        pageSize: '{pageSize} items per page',
        jump: 'Go to'
      },
      empty: 'No data',
      loading: 'Loading...',
      selectAll: 'Select All',
      selected: '{total} items selected'
    },

    // ==================== Time Operations ====================
    datetime: {
      date: 'Date',
      time: 'Time',
      year: 'Year',
      month: 'Month',
      day: 'Day',
      hour: 'Hour',
      minute: 'Minute',
      second: 'Second',
      startDate: 'Start Date',
      endDate: 'End Date',
      startTime: 'Start Time',
      endTime: 'End Time',
      createTime: 'Create Time',
      updateTime: 'Update Time',
      formatError: 'Failed to format time',
      relativeTimeFormatError: 'Failed to format relative time',
      parseError: 'Failed to parse date',
      rangeSeparator: ' to '
    },

    // ==================== File Operations ====================
    // Import/Export
    import: {
      title: 'Import Data',
      file: 'Select File',
      select: 'Select File',
      template: 'Template',
      download: 'Download Template',
      note: 'Instructions',
      tips: 'Please strictly follow the import template format, otherwise import may fail',
      format: 'Only Excel files are supported!',
      size: 'File size cannot exceed {size}MB!',
      total: 'Total Records',
      success: 'Success Count',
      failed: 'Failed Count',
      message: 'Failure Reason',
      dragText: 'Click or drag file to this area to upload',
      dragHint: 'Support .xlsx format Excel files',
      sheetName: 'Please ensure the Excel file sheet name is: {sheetName}',
      allSuccess: 'Import successful {count} records, all successful!',
      partialSuccess: 'Import successful {success} records, failed {fail} records',
      allFailed: 'All import failed, total {count} records',
      noData: 'No data read',
      empty: 'File is empty, cannot upload',
      importFailed: 'Import failed',
      templateFileName: 'Import_Template_{time}.xlsx',
      limits: {
        title: 'Import Limits',
        fileCount: 'File count limit: {count} file',
        fileSize: 'File size limit: {size}MB',
        recordCount: 'Record count limit: {count} records',
        fileFormat: 'File format: Only .xlsx format supported'
      },
      recordLimit: 'Import record count ({actual} records) exceeds limit ({max} records), please import in batches'
    },

    // Upload
    upload: {
      text: 'Drag file here or click to upload',
      picture: 'Click to upload picture',
      file: 'Click to upload file',
      icon: 'Click to upload icon',
      limit: {
        size: 'File size cannot exceed {size}',
        type: 'Only {type} format is supported'
      }
    },

    // ==================== Function Buttons ====================
    actions: {
      // === Basic Operation Buttons ===
      add: 'Add',           // @btn-add-color
      edit: 'Edit',         // @btn-edit-color
      delete: 'Delete',     // @btn-delete-color
      batchDelete: 'Batch Delete', // @btn-batch-delete-color
      view: 'View',          // @btn-view-color
      clear: 'Clear',        // @btn-clear-color
      forceOffline: 'Force Offline', // @btn-force-offline-color
      onlineStatus: 'Online Status', // @btn-online-status-color
      loginHistory: 'Login History', // @btn-login-history-color
      sendMail: 'Send Mail', // @btn-send-mail-color
      viewMail: 'View Mail', // @btn-view-mail-color
      mailTemplate: 'Mail Template', // @btn-mail-template-color
      sendNotification: 'Send Notification', // @btn-send-notification-color
      viewNotification: 'View Notification', // @btn-view-notification-color
      notificationSetting: 'Notification Setting', // @btn-notification-setting-color
      sendMessage: 'Send Message', // @btn-send-message-color
      viewMessage: 'View Message', // @btn-view-message-color
      messageSetting: 'Message Setting', // @btn-message-setting-color

      // === Data Operation Buttons ===
      import: 'Import',     // @btn-import-color
      export: 'Export',     // @btn-export-color
      template: 'Template', // @btn-template-color
      preview: 'Preview',   // @btn-preview-color
      download: 'Download', // @btn-download-color
      batchImport: 'Batch Import', // @btn-batch-import-color
      batchExport: 'Batch Export', // @btn-batch-export-color
      batchPrint: 'Batch Print', // @btn-batch-print-color
      batchEdit: 'Batch Edit',  // @btn-batch-edit-color
      batchUpdate: 'Batch Update', // @btn-batch-update-color

      // === Status Operation Buttons ===
      logging: 'Audit',        // @btn-audit-color
      revoke: 'Revoke',      // @btn-revoke-color
      stop: 'Stop',          // @btn-stop-color
      run: 'Run',            // @btn-run-color
      force: 'Force',        // @btn-forced-color
      start: 'Start',        // @btn-start-color
      suspend: 'Suspend',    // @btn-suspend-color
      resume: 'Resume',      // @btn-resume-color
      submit: 'Submit',      // @btn-submit-color
      withdraw: 'Withdraw',  // @btn-withdraw-color
      terminate: 'Terminate', // @btn-terminate-color

      // === System Function Buttons ===
      generate: 'Generate',  // @btn-generate-color
      refresh: 'Refresh',    // @btn-refresh-color
      info: 'Info',          // @btn-info-color
      log: 'Log',            // @btn-log-color
      chat: 'Chat',          // @btn-chat-color
      copy: 'Copy',          // @btn-copy-color
      execute: 'Execute',    // @btn-execute-color
      resetPwd: 'Reset Password', // @btn-reset-pwd-color
      open: 'Open',          // @btn-open-color
      close: 'Close',        // @btn-close-color
      more: 'More',          // @btn-more-color
      density: 'Density',    // @btn-density-color
      columnSetting: 'Column Setting', // @btn-column-setting-color

      // === Extended Function Buttons ===
      search: 'Search',      // @btn-search-color
      filter: 'Filter',      // @btn-filter-color
      sort: 'Sort',          // @btn-sort-color
      config: 'Config',      // @btn-config-color
      save: 'Save',          // @btn-save-color
      cancel: 'Cancel',      // @btn-cancel-color
      upload: 'Upload',      // @btn-upload-color
      print: 'Print',        // @btn-print-color
      help: 'Help',          // @btn-help-color
      share: 'Share',        // @btn-share-color
      lock: 'Lock',          // @btn-lock-color
      sync: 'Sync',          // @btn-sync-color
      initialize: 'Initialize', // @btn-initialize-color
      expand: 'Expand',      // @btn-expand-color
      collapse: 'Collapse',  // @btn-collapse-color
      approve: 'Approve',    // @btn-approve-color
      reject: 'Reject',      // @btn-reject-color
      comment: 'Comment',    // @btn-comment-color
      attach: 'Attach',      // @btn-attach-color

      // === Language Support Buttons ===
      translate: 'Translate', // @btn-translate-color
      langSwitch: 'Language Switch', // @btn-lang-switch-color
      dict: 'Dictionary',    // @btn-dict-color

      // === Data Analysis Buttons ===
      analyze: 'Analyze',    // @btn-analyze-color
      chart: 'Chart',        // @btn-chart-color
      report: 'Report',      // @btn-report-color
      dashboard: 'Dashboard', // @btn-dashboard-color
      statistics: 'Statistics', // @btn-statistics-color
      forecast: 'Forecast',  // @btn-forecast-color
      compare: 'Compare',    // @btn-compare-color

      // === Workflow Buttons ===
      startFlow: 'Start Flow', // @btn-start-flow-color
      endFlow: 'End Flow',     // @btn-end-flow-color
      suspendFlow: 'Suspend Flow', // @btn-suspend-flow-color
      resumeFlow: 'Resume Flow',   // @btn-resume-flow-color
      transfer: 'Transfer',       // @btn-transfer-color
      delegate: 'Delegate',       // @btn-delegate-color
      notify: 'Notify',          // @btn-notify-color
      urge: 'Urge',              // @btn-urge-color
      sign: 'Sign',              // @btn-sign-color
      countersign: 'Countersign' // @btn-countersign-color
    },

    // ==================== Results and Messages ====================
    // Result Status
    result: {
      success: 'Operation successful',
      failed: 'Operation failed',
      warning: 'Warning',
      info: 'Info',
      error: 'Error'
    },

    // Message Tips
    message: {
      loading: 'Processing...',
      saving: 'Saving...',
      submitting: 'Submitting...',
      deleting: 'Deleting...',
      operationSuccess: 'Operation successful',
      operationFailed: 'Operation failed',
      deleteConfirm: 'Are you sure to delete?',
      deleteSuccess: 'Delete successful',
      deleteFailed: 'Delete failed',
      createSuccess: 'Add successful',
      createFailed: 'Add failed',
      updateSuccess: 'Update successful',
      updateFailed: 'Update failed',
      networkError: 'Network connection failed, please check network',
      systemError: 'System error',
      timeout: 'Request timeout',
      invalidResponse: 'Invalid response data format',
      backendNotStarted: 'Backend service not started, please check service status',
      invalidRequest: 'Invalid request parameters',
      unauthorized: 'Unauthorized, please login again',
      forbidden: 'Access forbidden',
      notFound: 'Requested resource does not exist',
      serverError: 'Internal server error',
      httpError: {
        400: 'Invalid request parameters',
        401: 'Unauthorized, please login again',
        403: 'Access forbidden',
        404: 'Requested resource does not exist',
        500: 'Internal server error',
        502: 'Bad gateway',
        503: 'Service unavailable',
        504: 'Gateway timeout'
      },
      loadFailed: 'Load failed',
      forceOfflineConfirm: 'Are you sure to force this user offline?',
      forceOfflineSuccess: 'Force offline successful',
      forceOfflineFailed: 'Force offline failed'
    },

    // Common operations
    selectOne: 'Please select one record',
    selectAtLeastOne: 'Please select at least one record',

    // Common fields
    fields: {
      createBy: 'Created By',
      createTime: 'Created Time',
      updateBy: 'Updated By',
      updateTime: 'Updated Time',
      operation: 'Operation'
    },

    // ==================== Component Text ====================
    // Tabs
    tab: {
      // === Basic Information ===
      basic: 'Basic Information',
      profile: 'Profile',
      avatar: 'Avatar Settings',
      password: 'Password Settings',
      security: 'Security Settings',

      // === Code Generation ===
      codegen: 'Code Generation',
      codegenBasic: 'Generation Config',
      codegenField: 'Field Config',
      codegenPreview: 'Generation Preview',
      codegenTemplate: 'Template Config',
      codegenImport: 'Import Config',
      
      // === Workflow ===
      workflow: 'Workflow',
      flowDesign: 'Flow Design',
      flowDeploy: 'Flow Deploy',
      flowInstance: 'Flow Instance',
      flowTask: 'Task Management',
      flowForm: 'Form Config',
      flowNotify: 'Message Notification',
      
      // === System Management ===
      permission: 'Data Permission',
      log: 'Operation Log',
      dept: 'Department Position',
      role: 'Role Permission',
      config: 'Parameter Config',
      task: 'Scheduled Task',
      monitor: 'System Monitor'
    },

    // Button Text
    button: {
      submit: 'Submit',
      confirm: 'Confirm',
      ok: 'OK',
      cancel: 'Cancel',
      close: 'Close',
      reset: 'Reset',
      clear: 'Clear',
      save: 'Save',
      delete: 'Delete'
    }
  },

  tabs: {
    closeOthers: 'Close Others',
    closeRight: 'Close Right',
    closeAll: 'Close All',
    // Tab limit messages
    maxTabsReached: 'Tab limit reached ({count}), please close some tabs before opening new pages. You can use the dropdown menu on the right side of tabs to quickly close multiple tabs.',
    tabsTruncated: 'Detected {total} tabs, automatically kept the first {count} tabs'
  },

  // ==================== Selector Component ====================
  select: {
    loadMore: 'Load More',
    loading: 'Loading...',
    notFound: 'No data',
    selected: '{count} items selected',
    selectedTotal: 'Total {total} items',
    clear: 'Clear',
    search: 'Search',
    all: 'All',
    // Error Messages
    loadError: 'Failed to load data',
    searchError: 'Search failed',
    networkError: 'Network connection failed',
    serverError: 'Server error',
    unknownError: 'Unknown error'
  }
} 

