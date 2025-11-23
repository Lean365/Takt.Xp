#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktOfficeSupplyPurchaseRequestDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 办公用品请购申请明细表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.OfficeSupply
{
    /// <summary>
    /// 办公用品请购申请明细表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录办公用品请购申请的详细物品信息，包括物品、数量、预计单价等
    /// </remarks>
    [SugarTable("Takt_routine_office_supply_purchase_request_detail", "办公用品请购申请明细")]
    [SugarIndex("ix_office_supply_purchase_request_detail_request", nameof(RequestId), OrderByType.Asc, false)]
    [SugarIndex("ix_office_supply_purchase_request_detail_category", nameof(CategoryId), OrderByType.Asc, false)]
    public class TaktOfficeSupplyPurchaseRequestDetail : TaktBaseEntity
    {
        /// <summary>请购申请ID</summary>
        [SugarColumn(ColumnName = "request_id", ColumnDescription = "请购申请ID", ColumnDataType = "bigint", IsNullable = false)]
        public long RequestId { get; set; }

        /// <summary>用品分类ID</summary>
        [SugarColumn(ColumnName = "category_id", ColumnDescription = "用品分类ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CategoryId { get; set; }

        /// <summary>用品分类名称</summary>
        [SugarColumn(ColumnName = "category_name", ColumnDescription = "用品分类名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>用品名称</summary>
        [SugarColumn(ColumnName = "supply_name", ColumnDescription = "用品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplyName { get; set; } = string.Empty;

        /// <summary>用品规格</summary>
        [SugarColumn(ColumnName = "specification", ColumnDescription = "用品规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Specification { get; set; }

        /// <summary>品牌</summary>
        [SugarColumn(ColumnName = "brand", ColumnDescription = "品牌", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Brand { get; set; }

        /// <summary>型号</summary>
        [SugarColumn(ColumnName = "model", ColumnDescription = "型号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Model { get; set; }

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


    }
}



