export default {
  routine: {
    core: {
      numberrule: {
        // Page title
        title: 'Number Rule Management',
        
        // Tabs
        tabs: {
          basicInfo: 'Basic Information',
          numberConfig: 'Number Configuration',
          advancedConfig: 'Advanced Configuration',
          otherInfo: 'Other Information'
        },

        // Field labels
        fields: {
          companyCode: {
            label: 'Company Code',
            placeholder: 'Please enter company code',
            required: 'Please enter company code',
            length: 'Company code length must be between 1-20 characters'
          },
          numberRuleName: {
            label: 'Number Rule Name',
            placeholder: 'Please enter number rule name',
            required: 'Please enter number rule name',
            length: 'Number rule name length must be between 1-100 characters'
          },
          numberRuleCode: {
            label: 'Number Rule Code',
            placeholder: 'Please enter number rule code',
            required: 'Please enter number rule code',
            length: 'Number rule code length must be between 1-50 characters'
          },
          deptCode: {
            label: 'Department Code',
            placeholder: 'Please enter department code',
            required: 'Please enter department code',
            length: 'Department code length must be between 1-20 characters'
          },
          numberRuleShortCode: {
            label: 'Number Rule Short Code',
            placeholder: 'Please enter number rule short code',
            required: 'Please enter number rule short code',
            length: 'Number rule short code length must be between 1-4 characters'
          },
          numberRuleType: {
            label: 'Number Rule Type',
            placeholder: 'Please select number rule type',
            required: 'Please select number rule type'
          },
          status: {
            label: 'Status',
            placeholder: 'Please select status',
            required: 'Please select status'
          },
          numberRuleDescription: {
            label: 'Number Rule Description',
            placeholder: 'Please enter number rule description'
          },
          numberPrefix: {
            label: 'Number Prefix',
            placeholder: 'Please enter number prefix'
          },
          numberSuffix: {
            label: 'Number Suffix',
            placeholder: 'Please enter number suffix'
          },
          dateFormat: {
            label: 'Date Format',
            placeholder: 'Please select date format',
            required: 'Please select date format'
          },
          sequenceLength: {
            label: 'Sequence Length',
            placeholder: 'Please enter sequence length',
            required: 'Please enter sequence length'
          },
          sequenceStart: {
            label: 'Sequence Start',
            placeholder: 'Please enter sequence start value',
            required: 'Please enter sequence start value'
          },
          sequenceStep: {
            label: 'Sequence Step',
            placeholder: 'Please enter sequence step',
            required: 'Please enter sequence step'
          },
          currentSequence: {
            label: 'Current Sequence',
            placeholder: 'Please enter current sequence'
          },
          sequenceResetRule: {
            label: 'Sequence Reset Rule',
            placeholder: 'Please select sequence reset rule'
          },
          includeCompanyCode: {
            label: 'Include Company Code',
            placeholder: 'Please select whether to include company code'
          },
          includeDepartmentCode: {
            label: 'Include Department Code',
            placeholder: 'Please select whether to include department code'
          },
          includeRevisionNumber: {
            label: 'Include Revision Number',
            placeholder: 'Please select whether to include revision number'
          },
          includeYear: {
            label: 'Include Year',
            placeholder: 'Please select whether to include year'
          },
          includeMonth: {
            label: 'Include Month',
            placeholder: 'Please select whether to include month'
          },
          includeDay: {
            label: 'Include Day',
            placeholder: 'Please select whether to include day'
          },
          allowDuplicate: {
            label: 'Allow Duplicate',
            placeholder: 'Please select whether to allow duplicate'
          },
          duplicateCheckScope: {
            label: 'Duplicate Check Scope',
            placeholder: 'Please select duplicate check scope'
          },
          orderNum: {
            label: 'Order Number',
            placeholder: 'Please enter order number'
          }
        },

        // Action buttons
        actions: {
          add: 'Add Number Rule',
          edit: 'Edit Number Rule',
          delete: 'Delete Number Rule',
          batchDelete: 'Batch Delete',
          export: 'Export',
          import: 'Import',
          downloadTemplate: 'Download Template',
          preview: 'Preview',
          generate: 'Generate Number',
          validate: 'Validate Rule'
        },

        // Form placeholders
        placeholder: {
          search: 'Please enter number rule name or code',
          selectAll: 'Select All',
          clear: 'Clear'
        },

        // Operation result messages
        message: {
          success: {
            add: 'Number rule added successfully',
            edit: 'Number rule updated successfully',
            delete: 'Number rule deleted successfully',
            batchDelete: 'Batch delete completed successfully',
            export: 'Export completed successfully',
            import: 'Import completed successfully',
            generate: 'Number generated successfully',
            validate: 'Validation passed'
          },
          failed: {
            add: 'Failed to add number rule',
            edit: 'Failed to update number rule',
            delete: 'Failed to delete number rule',
            batchDelete: 'Failed to batch delete',
            export: 'Failed to export',
            import: 'Failed to import',
            generate: 'Failed to generate number',
            validate: 'Validation failed'
          },
          confirm: {
            delete: 'Are you sure to delete the selected number rule?',
            batchDelete: 'Are you sure to batch delete the selected number rules?'
          }
        },

        // Detail page
        detail: {
          title: 'Number Rule Details',
          basicInfo: 'Basic Information',
          numberConfig: 'Number Configuration',
          advancedConfig: 'Advanced Configuration',
          otherInfo: 'Other Information'
        },

        // Table column titles
        columns: {
          numberRuleName: 'Number Rule Name',
          numberRuleCode: 'Number Rule Code',
          numberRuleShortCode: 'Number Rule Short Code',
          numberRuleType: 'Number Rule Type',
          deptCode: 'Department Code',
          status: 'Status',
          createTime: 'Create Time',
          updateTime: 'Update Time',
          remark: 'Remark',
          actions: 'Actions'
        },

        // Number rule types
        types: {
          daily: 'Daily Affairs',
          hr: 'HumanResource Resources',
          finance: 'Financial Accounting',
          logistics: 'Logistics Management',
          production: 'Production Management',
          workflow: 'Workflow'
        },

        // Status
        status: {
          normal: 'Normal',
          disabled: 'Disabled'
        },

        // Sequence reset rules
        resetRules: {
          none: 'No Reset',
          yearly: 'Yearly Reset',
          monthly: 'Monthly Reset',
          daily: 'Daily Reset'
        },

        // Duplicate check scopes
        checkScopes: {
          global: 'Global',
          yearly: 'Yearly',
          monthly: 'Monthly',
          daily: 'Daily'
        },

        // Date format options
        dateFormats: {
          yyyy: 'yyyy',
          yyyyMM: 'yyyyMM',
          yyyyMMdd: 'yyyyMMdd',
          yyyyMMddHH: 'yyyyMMddHH',
          yyyyMMddHHmm: 'yyyyMMddHHmm',
          yyyyMMddHHmmss: 'yyyyMMddHHmmss'
        }
      }
    }
  }
}

