export default {
  logistics: {
    manufacturing: {
      outputs: {
        assy: {
          assyMpOutput: {
            fields: {
              plantCode: {
                label: '工厂代码',
                placeholder: '请输入工厂代码',
                required: '工厂代码不能为空'
              },
              prodDate: {
                label: '生产日期',
                placeholder: '请选择生产日期',
                required: '生产日期不能为空'
              },
              prodLine: {
                label: '生产线',
                placeholder: '请输入生产线',
                required: '生产线不能为空'
              },
              shiftNo: {
                label: '班次',
                placeholder: '请选择班次',
                required: '班次不能为空'
              },
              prodOrderType: {
                label: '生产订单类型',
                placeholder: '请输入生产订单类型',
                required: '生产订单类型不能为空'
              },
              prodOrderCode: {
                label: '生产订单号',
                placeholder: '请输入生产订单号',
                required: '生产订单号不能为空'
              },
              modelCode: {
                label: '模型代码',
                placeholder: '请输入模型代码',
                required: '模型代码不能为空'
              },
              materialCode: {
                label: '物料代码',
                placeholder: '请输入物料代码',
                required: '物料代码不能为空'
              },
              batchNo: {
                label: '批次号',
                placeholder: '请输入批次号',
                required: '批次号不能为空'
              },
              directLabor: {
                label: '直接人工',
                placeholder: '请输入直接人工',
                required: '直接人工不能为空'
              },
              indirectLabor: {
                label: '间接人工',
                placeholder: '请输入间接人工',
                required: '间接人工不能为空'
              },
              orderQty: {
                label: '订单数量',
                placeholder: '请输入订单数量',
                required: '订单数量不能为空'
              },
              stdMinutes: {
                label: '标准分钟',
                placeholder: '请输入标准分钟',
                required: '标准分钟不能为空'
              },
              stdCapacity: {
                label: '标准产能',
                placeholder: '请输入标准产能',
                required: '标准产能不能为空'
              },
              status: {
                label: '状态',
                placeholder: '请选择状态',
                required: '状态不能为空'
              },
              details: {
                label: '明细数据',
                placeholder: '请输入明细数据',
                required: '明细数据不能为空'
              },
              startDate: {
                label: '开始日期',
                placeholder: '请选择开始日期',
                required: '开始日期不能为空'
              },
              endDate: {
                label: '结束日期',
                placeholder: '请选择结束日期',
                required: '结束日期不能为空'
              },
            }
          }
        }
      }
    }
  }
}
