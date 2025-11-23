export default {
  workflow: {
    history: {
      title: 'Workflow History',
      list: {
        title: 'Workflow History List',
        search: {
          name: 'Workflow Name',
          key: 'Workflow Key',
          version: 'Version',
          status: 'Status',
          startTime: 'Start Time',
          endTime: 'End Time'
        },
        table: {
          name: 'Workflow Name',
          key: 'Workflow Key',
          version: 'Version',
          status: 'Status',
          startTime: 'Start Time',
          endTime: 'End Time',
          duration: 'Duration',
          actions: 'Actions'
        },
        actions: {
          view: 'View',
          delete: 'Delete',
          export: 'Export',
          import: 'Import',
          refresh: 'Refresh'
        },
        status: {
          running: 'Running',
          completed: 'Completed',
          terminated: 'Terminated',
          failed: 'Failed'
        }
      },
      detail: {
        title: 'Workflow History Detail',
        basic: {
          title: 'Basic Information',
          name: 'Workflow Name',
          key: 'Workflow Key',
          version: 'Version',
          status: 'Status',
          startTime: 'Start Time',
          endTime: 'End Time',
          duration: 'Duration'
        },
        nodes: {
          title: 'Node Information',
          name: 'Node Name',
          type: 'Node Type',
          status: 'Status',
          startTime: 'Start Time',
          endTime: 'End Time',
          duration: 'Duration',
          input: 'Input',
          output: 'Output',
          error: 'Error'
        },
        actions: {
          back: 'Back'
        }
      }
    }
  }
} 