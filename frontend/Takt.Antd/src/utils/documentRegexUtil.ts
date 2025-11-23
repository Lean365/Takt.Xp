/**
 * 单据相关正则表达式工具类
 * 参考SAP单据类型
 */
export class DocumentRegexUtils {
  /**
   * 通用单据编码正则表达式
   * 格式：公司代码4位+单据类型3位+编码8位
   * 示例：COMP001PO00000001
   * 说明：
   * - 公司代码：4位字母数字组合
   * - 单据类型：3位字母
   * - 编码：8位数字
   */
  static readonly DOCUMENT_CODE = /^[A-Z0-9]{4}[A-Z]{3}\d{8}$/

  /**
   * 采购订单编码
   * 示例：COMP001PO00000001
   */
  static readonly PURCHASE_ORDER_CODE = /^[A-Z0-9]{4}PO\d{8}$/

  /**
   * 销售订单编码
   * 示例：COMP001SO00000001
   */
  static readonly SALES_ORDER_CODE = /^[A-Z0-9]{4}SO\d{8}$/

  /**
   * 采购入库单编码
   * 示例：COMP001PIN00000001
   */
  static readonly PURCHASE_IN_CODE = /^[A-Z0-9]{4}PIN\d{8}$/

  /**
   * 销售出库单编码
   * 示例：COMP001SOU00000001
   */
  static readonly SALES_OUT_CODE = /^[A-Z0-9]{4}SOU\d{8}$/

  /**
   * 生产领料单编码
   * 示例：COMP001PM00000001
   */
  static readonly PRODUCTION_MATERIAL_CODE = /^[A-Z0-9]{4}PM\d{8}$/

  /**
   * 生产入库单编码
   * 示例：COMP001POU00000001
   */
  static readonly PRODUCTION_OUT_CODE = /^[A-Z0-9]{4}POU\d{8}$/

  /**
   * 库存调拨单编码
   * 示例：COMP001TR00000001
   */
  static readonly TRANSFER_CODE = /^[A-Z0-9]{4}TR\d{8}$/

  /**
   * 库存盘点单编码
   * 示例：COMP001IN00000001
   */
  static readonly INVENTORY_CODE = /^[A-Z0-9]{4}IN\d{8}$/

  /**
   * 退货单编码
   * 示例：COMP001RT00000001
   */
  static readonly RETURN_CODE = /^[A-Z0-9]{4}RT\d{8}$/

  /**
   * 付款单编码
   * 示例：COMP001PA00000001
   */
  static readonly PAYMENT_CODE = /^[A-Z0-9]{4}PA\d{8}$/

  /**
   * 收款单编码
   * 示例：COMP001RC00000001
   */
  static readonly RECEIPT_CODE = /^[A-Z0-9]{4}RC\d{8}$/

  /**
   * 发票编码
   * 示例：COMP001IV00000001
   */
  static readonly INVOICE_CODE = /^[A-Z0-9]{4}IV\d{8}$/

  /**
   * 合同编码
   * 示例：COMP001CT00000001
   */
  static readonly CONTRACT_CODE = /^[A-Z0-9]{4}CT\d{8}$/

  /**
   * 验证通用单据编码
   * @param code 单据编码
   * @returns boolean
   */
  static isValidDocumentCode(code: string): boolean {
    return this.DOCUMENT_CODE.test(code)
  }

  /**
   * 验证采购订单编码
   * @param code 采购订单编码
   * @returns boolean
   */
  static isValidPurchaseOrderCode(code: string): boolean {
    return this.PURCHASE_ORDER_CODE.test(code)
  }

  /**
   * 验证销售订单编码
   * @param code 销售订单编码
   * @returns boolean
   */
  static isValidSalesOrderCode(code: string): boolean {
    return this.SALES_ORDER_CODE.test(code)
  }

  /**
   * 验证采购入库单编码
   * @param code 采购入库单编码
   * @returns boolean
   */
  static isValidPurchaseInCode(code: string): boolean {
    return this.PURCHASE_IN_CODE.test(code)
  }

  /**
   * 验证销售出库单编码
   * @param code 销售出库单编码
   * @returns boolean
   */
  static isValidSalesOutCode(code: string): boolean {
    return this.SALES_OUT_CODE.test(code)
  }

  /**
   * 验证生产领料单编码
   * @param code 生产领料单编码
   * @returns boolean
   */
  static isValidProductionMaterialCode(code: string): boolean {
    return this.PRODUCTION_MATERIAL_CODE.test(code)
  }

  /**
   * 验证生产入库单编码
   * @param code 生产入库单编码
   * @returns boolean
   */
  static isValidProductionOutCode(code: string): boolean {
    return this.PRODUCTION_OUT_CODE.test(code)
  }

  /**
   * 验证库存调拨单编码
   * @param code 库存调拨单编码
   * @returns boolean
   */
  static isValidTransferCode(code: string): boolean {
    return this.TRANSFER_CODE.test(code)
  }

  /**
   * 验证库存盘点单编码
   * @param code 库存盘点单编码
   * @returns boolean
   */
  static isValidInventoryCode(code: string): boolean {
    return this.INVENTORY_CODE.test(code)
  }

  /**
   * 验证退货单编码
   * @param code 退货单编码
   * @returns boolean
   */
  static isValidReturnCode(code: string): boolean {
    return this.RETURN_CODE.test(code)
  }

  /**
   * 验证付款单编码
   * @param code 付款单编码
   * @returns boolean
   */
  static isValidPaymentCode(code: string): boolean {
    return this.PAYMENT_CODE.test(code)
  }

  /**
   * 验证收款单编码
   * @param code 收款单编码
   * @returns boolean
   */
  static isValidReceiptCode(code: string): boolean {
    return this.RECEIPT_CODE.test(code)
  }

  /**
   * 验证发票编码
   * @param code 发票编码
   * @returns boolean
   */
  static isValidInvoiceCode(code: string): boolean {
    return this.INVOICE_CODE.test(code)
  }

  /**
   * 验证合同编码
   * @param code 合同编码
   * @returns boolean
   */
  static isValidContractCode(code: string): boolean {
    return this.CONTRACT_CODE.test(code)
  }

  /**
   * 根据单据类型验证单据编码
   * @param code 单据编码
   * @param type 单据类型
   * @returns boolean
   */
  static isValidDocumentCodeByType(code: string, type: string): boolean {
    switch (type) {
      case 'PO':
        return this.isValidPurchaseOrderCode(code)
      case 'SO':
        return this.isValidSalesOrderCode(code)
      case 'PIN':
        return this.isValidPurchaseInCode(code)
      case 'SOU':
        return this.isValidSalesOutCode(code)
      case 'PM':
        return this.isValidProductionMaterialCode(code)
      case 'POU':
        return this.isValidProductionOutCode(code)
      case 'TR':
        return this.isValidTransferCode(code)
      case 'IN':
        return this.isValidInventoryCode(code)
      case 'RT':
        return this.isValidReturnCode(code)
      case 'PA':
        return this.isValidPaymentCode(code)
      case 'RC':
        return this.isValidReceiptCode(code)
      case 'IV':
        return this.isValidInvoiceCode(code)
      case 'CT':
        return this.isValidContractCode(code)
      default:
        return false
    }
  }

  /**
   * 获取单据类型名称
   * @param type 单据类型代码
   * @returns string
   */
  static getDocumentTypeName(type: string): string {
    const typeMap: Record<string, string> = {
      'PO': '采购订单',
      'SO': '销售订单',
      'PIN': '采购入库单',
      'SOU': '销售出库单',
      'PM': '生产领料单',
      'POU': '生产入库单',
      'TR': '库存调拨单',
      'IN': '库存盘点单',
      'RT': '退货单',
      'PA': '付款单',
      'RC': '收款单',
      'IV': '发票',
      'CT': '合同'
    }
    return typeMap[type] || '未知单据类型'
  }
} 