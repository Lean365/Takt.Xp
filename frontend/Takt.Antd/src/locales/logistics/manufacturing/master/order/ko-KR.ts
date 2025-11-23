export default {
  logistics: {
    manufacturing: {
      master: {
        order: {
          title: 'Production Order Management',
          
          // All field translations, unified use, avoid duplication
          fields: {
            // Basic fields - strictly follow TaktProdOrder.cs order and IsNullable property
            plantCode: {
              label: 'Plant',
              placeholder: 'Please enter plant',
              validation: {
                required: 'Plant is required'
              }
            },
            prodOrderType: {
              label: 'Order Type',
              placeholder: 'Please select order type',
              validation: {
                required: 'Order type is required'
              }
            },
            prodOrderCode: {
              label: 'Order Code',
              placeholder: 'Please enter order code',
              validation: {
                required: 'Order code is required'
              }
            },
            materialCode: {
              label: 'Material Code',
              placeholder: 'Please enter material code',
              validation: {
                required: 'Material code is required'
              }
            },
            prodOrderQty: {
              label: 'Order Quantity',
              placeholder: 'Please enter order quantity',
              validation: {
                required: 'Order quantity is required'
              }
            },
            producedQty: {
              label: 'Produced Quantity',
              placeholder: 'Please enter produced quantity',
              validation: {
                required: 'Produced quantity is required'
              }
            },
            unitOfMeasure: {
              label: 'Unit of Measure',
              placeholder: 'Please enter unit of measure',
              validation: {
                required: 'Unit of measure is required'
              }
            },
            actualStartDate: {
              label: 'Actual Start Date',
              placeholder: 'Please select actual start date'
            },
            actualEndDate: {
              label: 'Actual End Date',
              placeholder: 'Please select actual end date'
            },
            priority: {
              label: 'Priority',
              placeholder: 'Please select priority',
              validation: {
                required: 'Priority is required'
              }
            },
            workCenter: {
              label: 'Work Center',
              placeholder: 'Please enter work center'
            },
            prodLine: {
              label: 'Production Line',
              placeholder: 'Please enter production line'
            },
            prodBatch: {
              label: 'Production Batch',
              placeholder: 'Please enter production batch'
            },
            serialNo: {
              label: 'Serial Number',
              placeholder: 'Please enter serial number'
            },
            routingCode: {
              label: 'Routing Code',
              placeholder: 'Please enter routing code'
            },
            status: {
              label: 'Status',
              placeholder: 'Please select status',
              validation: {
                required: 'Status is required'
              }
            },
            // Query related fields
            dateRange: {
              label: 'Date Range',
              placeholder: 'Please select date range'
            },
            startDate: {
              label: 'Start Date',
              placeholder: 'Please select start date'
            },
            endDate: {
              label: 'End Date',
              placeholder: 'Please select end date'
            },
            // Change log fields - according to TaktProdOrderChangeLog.cs IsNullable property
            changeType: {
              label: 'Change Type',
              placeholder: 'Please select change type',
              validation: {
                required: 'Change type is required'
              }
            },
            changeDate: {
              label: 'Change Date',
              placeholder: 'Please select change date',
              validation: {
                required: 'Change date is required'
              }
            },
            changeUser: {
              label: 'Change User',
              placeholder: 'Please enter change user',
              validation: {
                required: 'Change user is required'
              }
            },
            changeReason: {
              label: 'Change Reason',
              placeholder: 'Please enter change reason'
            },
            beforeValue: {
              label: 'Before Value',
              placeholder: 'Please enter before value'
            },
            afterValue: {
              label: 'After Value',
              placeholder: 'Please enter after value'
            },
            changeField: {
              label: 'Change Field',
              placeholder: 'Please enter change field'
            },
            beforeFieldValue: {
              label: 'Field Before Value',
              placeholder: 'Please enter field before value'
            },
            afterFieldValue: {
              label: 'Field After Value',
              placeholder: 'Please enter field after value'
            },
            remark: {
              label: 'Remark',
              placeholder: 'Please enter remark information'
            }
          },

          // Action buttons
          actions: {
            add: 'Add Production Order',
            edit: 'Edit Production Order',
            delete: 'Delete Production Order',
            view: 'View Production Order',
            export: 'Export Production Order'
          },

          // Message prompts
          messages: {
            confirmDelete: 'Are you sure you want to delete the selected production order?',
            confirmBatchDelete: 'Are you sure you want to delete the selected {count} production orders?',
            deleteSuccess: 'Production order deleted successfully',
            deleteFailed: 'Failed to delete production order',
            saveSuccess: 'Production order information saved successfully',
            saveFailed: 'Failed to save production order information',
            createSuccess: 'Production order created successfully',
            createFailed: 'Failed to create production order',
            updateSuccess: 'Production order updated successfully',
            updateFailed: 'Failed to update production order',
            importSuccess: 'Production order imported successfully',
            importFailed: 'Failed to import production order',
            exportSuccess: 'Production order exported successfully',
            exportFailed: 'Failed to export production order',
            toggleStatusSuccess: 'Status changed successfully',
            toggleStatusFailed: 'Failed to change status',
            getDataFailed: 'Failed to get data'
          },

          // Import/Export
          import: {
            title: 'Import Production Order',
            template: 'Download Template',
            success: 'Import successful',
            failed: 'Import failed'
          },
          export: {
            title: 'Export Production Order',
            success: 'Export successful',
            failed: 'Export failed'
          },

          // Status options
          status: {
            options: {
              draft: 'Draft',
              confirmed: 'Confirmed',
              inProgress: 'In Progress',
              completed: 'Completed',
              cancelled: 'Cancelled',
              suspended: 'Suspended'
            }
          },

          // Order type options
          prodOrderType: {
            options: {
              standard: 'Standard Order',
              rework: 'Rework Order',
              repair: 'Repair Order',
              sample: 'Sample Order'
            }
          },

          // Priority options
          priority: {
            options: {
              low: 'Low',
              normal: 'Normal',
              high: 'High',
              urgent: 'Urgent'
            }
          }
        }
      }
    }
  }
}

