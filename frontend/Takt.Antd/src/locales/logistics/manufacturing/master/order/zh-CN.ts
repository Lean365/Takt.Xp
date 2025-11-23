export default {
  logistics: {
    manufacturing: {
      master: {
        order: {
          title: '生产工单管理',
          
          // 所有字段的翻译，统一使用，避免重复
          fields: {
            // 基础字段 - 严格按照TaktProdOrder.cs的顺序和IsNullable属性
            plantCode: {
              label: '工厂',
              placeholder: '请输入工厂',
              validation: {
                required: '工厂不能为空'
              }
            },
            prodOrderType: {
              label: '工单类型',
              placeholder: '请选择工单类型',
              validation: {
                required: '工单类型不能为空'
              }
            },
            prodOrderCode: {
              label: '工单号',
              placeholder: '请输入工单号',
              validation: {
                required: '工单号不能为空'
              }
            },
            materialCode: {
              label: '物料编码',
              placeholder: '请输入物料编码',
              validation: {
                required: '物料编码不能为空'
              }
            },
            prodOrderQty: {
              label: '工单数量',
              placeholder: '请输入工单数量',
              validation: {
                required: '工单数量不能为空'
              }
            },
            producedQty: {
              label: '已生产数量',
              placeholder: '请输入已生产数量',
              validation: {
                required: '已生产数量不能为空'
              }
            },
            unitOfMeasure: {
              label: '计量单位',
              placeholder: '请输入计量单位',
              validation: {
                required: '计量单位不能为空'
              }
            },
            actualStartDate: {
              label: '实际开始日期',
              placeholder: '请选择实际开始日期'
            },
            actualEndDate: {
              label: '实际完成日期',
              placeholder: '请选择实际完成日期'
            },
            Priority: {
              label: '优先级',
              placeholder: '请选择优先级',
              validation: {
                required: '优先级不能为空'
              }
            },
            workCenter: {
              label: '工作中心',
              placeholder: '请输入工作中心'
            },
            prodLine: {
              label: '生产线',
              placeholder: '请输入生产线'
            },
            prodBatch: {
              label: '生产批次',
              placeholder: '请输入生产批次'
            },
            serialNo: {
              label: '序列号',
              placeholder: '请输入序列号'
            },
            routingCode: {
              label: '工艺路线编码',
              placeholder: '请输入工艺路线编码'
            },
            status: {
              label: '状态',
              placeholder: '请选择状态',
              validation: {
                required: '状态不能为空'
              }
            },
            // 查询相关字段
            dateRange: {
              label: '日期范围',
              placeholder: '请选择日期范围'
            },
            startDate: {
              label: '开始日期',
              placeholder: '请选择开始日期'
            },
            endDate: {
              label: '结束日期',
              placeholder: '请选择结束日期'
            },
            // 变更记录字段 - 根据TaktProdOrderChangeLog.cs的IsNullable属性
            changeType: {
              label: '变更类型',
              placeholder: '请选择变更类型',
              validation: {
                required: '变更类型不能为空'
              }
            },
            changeDate: {
              label: '变更日期',
              placeholder: '请选择变更日期',
              validation: {
                required: '变更日期不能为空'
              }
            },
            changeUser: {
              label: '变更用户',
              placeholder: '请输入变更用户',
              validation: {
                required: '变更用户不能为空'
              }
            },
            changeReason: {
              label: '变更原因',
              placeholder: '请输入变更原因'
            },
            beforeValue: {
              label: '变更前值',
              placeholder: '请输入变更前值'
            },
            afterValue: {
              label: '变更后值',
              placeholder: '请输入变更后值'
            },
            changeField: {
              label: '变更字段',
              placeholder: '请输入变更字段'
            },
            beforeFieldValue: {
              label: '字段变更前值',
              placeholder: '请输入字段变更前值'
            },
            afterFieldValue: {
              label: '字段变更后值',
              placeholder: '请输入字段变更后值'
            },
            remark: {
              label: '备注',
              placeholder: '请输入备注信息'
            },
            remarks: {
              label: '备注',
              placeholder: '请输入备注'
            }
          },

          // 操作按钮
          actions: {
            add: '新增生产工单',
            edit: '编辑生产工单',
            delete: '删除生产工单',
            view: '查看生产工单',
            export: '导出生产工单'
          },

          // 消息提示
          messages: {
            confirmDelete: '是否确认删除选中的生产工单？',
            confirmBatchDelete: '是否确认删除选中的{count}条生产工单？',
            deleteSuccess: '生产工单删除成功',
            deleteFailed: '生产工单删除失败',
            saveSuccess: '生产工单信息保存成功',
            saveFailed: '生产工单信息保存失败',
            createSuccess: '生产工单创建成功',
            createFailed: '生产工单创建失败',
            updateSuccess: '生产工单更新成功',
            updateFailed: '生产工单更新失败',
            importSuccess: '生产工单导入成功',
            importFailed: '生产工单导入失败',
            exportSuccess: '生产工单导出成功',
            exportFailed: '生产工单导出失败',
            toggleStatusSuccess: '状态修改成功',
            toggleStatusFailed: '状态修改失败',
            getDataFailed: '获取数据失败'
          },

          // 导入导出
          import: {
            title: '导入生产工单',
            template: '下载模板',
            success: '导入成功',
            failed: '导入失败'
          },
          export: {
            title: '导出生产工单',
            success: '导出成功',
            failed: '导出失败'
          },

          // 状态选项
          status: {
            options: {
              draft: '草稿',
              confirmed: '已确认',
              inProgress: '进行中',
              completed: '已完成',
              cancelled: '已取消',
              suspended: '已暂停'
            }
          },

          // 工单类型选项
          prodOrderType: {
            options: {
              standard: '标准工单',
              rework: '返工工单',
              repair: '维修工单',
              sample: '样品工单'
            }
          },

          // 优先级选项
          priority: {
            options: {
              low: '低',
              normal: '普通',
              high: '高',
              urgent: '紧急'
            }
          }
        }
      }
    }
  }
}
  
  
