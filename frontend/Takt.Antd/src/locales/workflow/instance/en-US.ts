export default {
  workflow: {
    instance: {
      fields: {
        instanceId: 'Instance ID',
        instanceName: 'Instance Name',
        businessKey: 'Business Key',
        definitionId: 'Definition ID',
        definitionName: 'Definition Name',
        currentNodeId: 'Current Node ID',
        currentNodeName: 'Current Node Name',
        initiatorId: 'Initiator ID',
        initiatorName: 'Initiator',
        formData: 'Form Data',
        status: 'Status',
        startTime: 'Start Time',
        endTime: 'End Time',
        remark: 'Remark',
      },
      title: 'Workflow Instance',
      list: {
        title: 'Workflow Instance List',
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
          terminate: 'Terminate',
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
      form: {
        title: {
          create: 'Create Workflow Instance',
          import: 'Import Workflow Instance'
        },
        fields: {
          workflowDefinitionId: 'Workflow Definition',
          variables: 'Variable Configuration'
        },
        rules: {
          workflowDefinitionId: {
            required: 'Please select a workflow definition'
          }
        },
        buttons: {
          submit: 'Submit',
          cancel: 'Cancel'
        }
      },
      detail: {
        title: 'Workflow Instance Detail',
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
      },
      terminate: {
        title: 'Terminate Workflow Instance',
        confirm: 'Are you sure you want to terminate this instance?',
        reason: 'Termination Reason',
        buttons: {
          submit: 'Submit',
          cancel: 'Cancel'
        }
      }
    }
  }
} 