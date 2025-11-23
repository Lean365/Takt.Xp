export default {
  workflow: {
    definition: {
      title: 'Workflow Definition',
      list: {
        title: 'Workflow Definition List',
        search: {
          workflowName: 'Workflow Name',
          workflowCategory: 'Workflow Type',
          status: 'Status'
        },
        table: {
          workflowId: 'Workflow ID',
          workflowName: 'Workflow Name',
          workflowCategory: 'Workflow Type',
          status: 'Status',
          createTime: 'Create Time',
          updateTime: 'Update Time',
          actions: 'Actions'
        },
        actions: {
          view: 'View',
          edit: 'Edit',
          delete: 'Delete',
          refresh: 'Refresh',
          import: 'Import',
          export: 'Export'
        },
        status: {
          normal: 'Normal',
          disable: 'Disabled'
        }
      },
      form: {
        title: {
          create: 'Create Workflow Definition',
          edit: 'Edit Workflow Definition'
        },
        fields: {
          workflowName: 'Workflow Name',
          workflowCategory: 'Workflow Type',
          workflowVersion: 'Version',
          status: 'Status',
          remark: 'Remark'
        },
        rules: {
          workflowName: {
            required: 'Please enter workflow name'
          },
          workflowCategory: {
            required: 'Please select workflow type'
          },
          status: {
            required: 'Please select status'
          }
        },
        buttons: {
          submit: 'Submit',
          cancel: 'Cancel'
        }
      },
      detail: {
        title: 'Workflow Definition Detail',
        basic: {
          title: 'Basic Information',
          workflowName: 'Workflow Name',
          workflowCategory: 'Workflow Type',
          workflowVersion: 'Version',
          status: 'Status',
          remark: 'Remark'
        },
        node: {
          title: 'Node Information',
          nodeName: 'Node Name',
          nodeType: 'Node Type',
          assigneeType: 'Assignee Type',
          formType: 'Form Type'
        },
        form: {
          title: 'Form Information',
          fieldName: 'Field Name',
          fieldType: 'Field Type',
          required: 'Required',
          remark: 'Remark'
        }
      }
    }
  }
} 