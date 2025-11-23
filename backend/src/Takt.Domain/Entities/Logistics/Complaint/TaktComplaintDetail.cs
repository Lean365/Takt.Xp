#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktComplaintDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 核心客诉子表实体类 (基于SAP QM质量管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Complaint
{
    /// <summary>
    /// 核心客诉子表实体类 (基于SAP QM质量管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP QM 质量管理模块
    /// </remarks>
    [SugarTable("Takt_logistics_complaint_detail", "核心客诉明细")]
    [SugarIndex("ix_complaint_id", nameof(ComplaintId), OrderByType.Asc, false)]
    [SugarIndex("ix_product_code", nameof(ProductCode), OrderByType.Asc, false)]
    [SugarIndex("ix_order_number", nameof(OrderNumber), OrderByType.Asc, false)]
    public class TaktComplaintDetail : TaktBaseEntity
    {
        /// <summary>
        /// 客诉主表ID
        /// </summary>
        [SugarColumn(ColumnName = "complaint_id", ColumnDescription = "客诉主表ID", ColumnDataType = "bigint", IsNullable = false)]
        public long ComplaintId { get; set; }

        /// <summary>
        /// 客诉编号
        /// </summary>
        [SugarColumn(ColumnName = "complaint_code", ColumnDescription = "客诉编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ComplaintCode { get; set; } = string.Empty;

        /// <summary>
        /// 客诉主表关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ComplaintCode))]
        public TaktComplaint? Complaint { get; set; }

        /// <summary>
        /// 行项目编号
        /// </summary>
        [SugarColumn(ColumnName = "line_number", ColumnDescription = "行项目编号", ColumnDataType = "int", IsNullable = false)]
        public int LineNumber { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        [SugarColumn(ColumnName = "product_code", ColumnDescription = "产品编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [SugarColumn(ColumnName = "product_name", ColumnDescription = "产品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductName { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        [SugarColumn(ColumnName = "product_spec", ColumnDescription = "产品规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductSpec { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        [SugarColumn(ColumnName = "product_model", ColumnDescription = "产品型号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductModel { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [SugarColumn(ColumnName = "batch_number", ColumnDescription = "批次号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BatchNumber { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [SugarColumn(ColumnName = "order_number", ColumnDescription = "订单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// 订单行项目号
        /// </summary>
        [SugarColumn(ColumnName = "order_line_number", ColumnDescription = "订单行项目号", ColumnDataType = "int", IsNullable = true)]
        public int? OrderLineNumber { get; set; }

        /// <summary>
        /// 发货单号
        /// </summary>
        [SugarColumn(ColumnName = "delivery_number", ColumnDescription = "发货单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryNumber { get; set; }

        /// <summary>
        /// 发货行项目号
        /// </summary>
        [SugarColumn(ColumnName = "delivery_line_number", ColumnDescription = "发货行项目号", ColumnDataType = "int", IsNullable = true)]
        public int? DeliveryLineNumber { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        [SugarColumn(ColumnName = "invoice_number", ColumnDescription = "发票号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InvoiceNumber { get; set; }

        /// <summary>
        /// 发票行项目号
        /// </summary>
        [SugarColumn(ColumnName = "invoice_line_number", ColumnDescription = "发票行项目号", ColumnDataType = "int", IsNullable = true)]
        public int? InvoiceLineNumber { get; set; }

        /// <summary>
        /// 客诉数量
        /// </summary>
        [SugarColumn(ColumnName = "complaint_quantity", ColumnDescription = "客诉数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ComplaintQuantity { get; set; } = 0;

        /// <summary>
        /// 单位
        /// </summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Unit { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>
        /// 客诉金额
        /// </summary>
        [SugarColumn(ColumnName = "complaint_amount", ColumnDescription = "客诉金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ComplaintAmount { get; set; } = 0;

        /// <summary>
        /// 币种
        /// </summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>
        /// 生产日期
        /// </summary>
        [SugarColumn(ColumnName = "production_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ProductionDate { get; set; }

        /// <summary>
        /// 发货日期
        /// </summary>
        [SugarColumn(ColumnName = "delivery_date", ColumnDescription = "发货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 收货日期
        /// </summary>
        [SugarColumn(ColumnName = "receipt_date", ColumnDescription = "收货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ReceiptDate { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        [SugarColumn(ColumnName = "problem_description", ColumnDescription = "问题描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProblemDescription { get; set; }

        /// <summary>
        /// 问题类型(1=外观缺陷 2=功能缺陷 3=尺寸偏差 4=包装破损 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "problem_type", ColumnDescription = "问题类型", ColumnDataType = "int", IsNullable = true)]
        public int? ProblemType { get; set; }

        /// <summary>
        /// 问题类型名称
        /// </summary>
        [SugarColumn(ColumnName = "problem_type_name", ColumnDescription = "问题类型名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProblemTypeName { get; set; }

        /// <summary>
        /// 问题严重程度(1=轻微 2=一般 3=严重 4=重大)
        /// </summary>
        [SugarColumn(ColumnName = "problem_severity", ColumnDescription = "问题严重程度", ColumnDataType = "int", IsNullable = true)]
        public int? ProblemSeverity { get; set; }

        /// <summary>
        /// 问题严重程度名称
        /// </summary>
        [SugarColumn(ColumnName = "problem_severity_name", ColumnDescription = "问题严重程度名称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProblemSeverityName { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierCode { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierName { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [SugarColumn(ColumnName = "warehouse_code", ColumnDescription = "仓库编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WarehouseCode { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [SugarColumn(ColumnName = "warehouse_name", ColumnDescription = "仓库名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WarehouseName { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        [SugarColumn(ColumnName = "location_code", ColumnDescription = "库位编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LocationCode { get; set; }

        /// <summary>
        /// 库位名称
        /// </summary>
        [SugarColumn(ColumnName = "location_name", ColumnDescription = "库位名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LocationName { get; set; }


        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
} 



