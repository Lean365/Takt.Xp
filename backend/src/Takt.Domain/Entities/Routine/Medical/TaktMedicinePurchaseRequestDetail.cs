#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMedicineRequestDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 药品请购申请明细表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.MedicalManagement
{
    /// <summary>
    /// 药品请购申请明细表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录药品请购申请的详细药品信息，包括药品、数量、预计单价等
    /// </remarks>
    [SugarTable("Takt_routine_medical_medicine_purchase_request_detail", "药品请购申请明细")]
    [SugarIndex("ix_medical_medicine_purchase_request_detail_request", nameof(RequestId), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_medicine_purchase_request_detail_category", nameof(CategoryId), OrderByType.Asc, false)]
    public class TaktMedicinePurchaseRequestDetail : TaktBaseEntity
    {
        /// <summary>请购申请ID</summary>
        [SugarColumn(ColumnName = "request_id", ColumnDescription = "请购申请ID", ColumnDataType = "bigint", IsNullable = false)]
        public long RequestId { get; set; }

        /// <summary>药品分类ID</summary>
        [SugarColumn(ColumnName = "category_id", ColumnDescription = "药品分类ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CategoryId { get; set; }

        /// <summary>药品分类名称</summary>
        [SugarColumn(ColumnName = "category_name", ColumnDescription = "药品分类名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>药品名称</summary>
        [SugarColumn(ColumnName = "medicine_name", ColumnDescription = "药品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MedicineName { get; set; } = string.Empty;

        /// <summary>药品规格</summary>
        [SugarColumn(ColumnName = "specification", ColumnDescription = "药品规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Specification { get; set; }

        /// <summary>生产厂家</summary>
        [SugarColumn(ColumnName = "manufacturer", ColumnDescription = "生产厂家", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Manufacturer { get; set; }

        /// <summary>批准文号</summary>
        [SugarColumn(ColumnName = "approval_number", ColumnDescription = "批准文号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovalNumber { get; set; }

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>请购数量</summary>
        [SugarColumn(ColumnName = "request_quantity", ColumnDescription = "请购数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RequestQuantity { get; set; } = 0;

        /// <summary>审批数量</summary>
        [SugarColumn(ColumnName = "approve_quantity", ColumnDescription = "审批数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ApproveQuantity { get; set; } = 0;

        /// <summary>预计单价</summary>
        [SugarColumn(ColumnName = "estimated_unit_price", ColumnDescription = "预计单价", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal EstimatedUnitPrice { get; set; } = 0;

        /// <summary>预计总金额</summary>
        [SugarColumn(ColumnName = "estimated_total_amount", ColumnDescription = "预计总金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal EstimatedTotalAmount { get; set; } = 0;

        /// <summary>实际单价</summary>
        [SugarColumn(ColumnName = "actual_unit_price", ColumnDescription = "实际单价", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualUnitPrice { get; set; } = 0;

        /// <summary>实际总金额</summary>
        [SugarColumn(ColumnName = "actual_total_amount", ColumnDescription = "实际总金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualTotalAmount { get; set; } = 0;

        /// <summary>供应商</summary>
        [SugarColumn(ColumnName = "supplier", ColumnDescription = "供应商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Supplier { get; set; }

        /// <summary>供应商联系方式</summary>
        [SugarColumn(ColumnName = "supplier_contact", ColumnDescription = "供应商联系方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierContact { get; set; }

        /// <summary>是否处方药(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_prescription", ColumnDescription = "是否处方药", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsPrescription { get; set; } = 0;


    }
} 



