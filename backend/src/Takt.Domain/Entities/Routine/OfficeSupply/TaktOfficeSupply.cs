#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktOfficeSupply.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 办公用品实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.OfficeSupply
{
    /// <summary>
    /// 办公用品实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录办公用品的基本信息，包括名称、规格、库存、价格等
    /// </remarks>
    [SugarTable("Takt_routine_office_supply", "办公用品")]
    [SugarIndex("ix_office_supply_code", nameof(SupplyCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_office_supply", nameof(CompanyCode), OrderByType.Asc, nameof(SupplyCode), OrderByType.Asc, false)]
    [SugarIndex("ix_office_supply_category", nameof(CategoryId), OrderByType.Asc, false)]
    [SugarIndex("ix_office_supply_status", nameof(Status), OrderByType.Asc, false)]
    public class TaktOfficeSupply : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>用品编号</summary>
        [SugarColumn(ColumnName = "supply_code", ColumnDescription = "用品编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplyCode { get; set; } = string.Empty;

        /// <summary>用品名称</summary>
        [SugarColumn(ColumnName = "supply_name", ColumnDescription = "用品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplyName { get; set; } = string.Empty;

        /// <summary>用品分类ID</summary>
        [SugarColumn(ColumnName = "category_id", ColumnDescription = "用品分类ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CategoryId { get; set; }

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

        /// <summary>单价</summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>库存数量</summary>
        [SugarColumn(ColumnName = "stock_quantity", ColumnDescription = "库存数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int StockQuantity { get; set; } = 0;

        /// <summary>最小库存</summary>
        [SugarColumn(ColumnName = "min_stock", ColumnDescription = "最小库存", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MinStock { get; set; } = 0;

        /// <summary>最大库存</summary>
        [SugarColumn(ColumnName = "max_stock", ColumnDescription = "最大库存", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaxStock { get; set; } = 0;

        /// <summary>供应商</summary>
        [SugarColumn(ColumnName = "supplier", ColumnDescription = "供应商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Supplier { get; set; }

        /// <summary>供应商联系方式</summary>
        [SugarColumn(ColumnName = "supplier_contact", ColumnDescription = "供应商联系方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierContact { get; set; }

        /// <summary>存放位置</summary>
        [SugarColumn(ColumnName = "storage_location", ColumnDescription = "存放位置", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StorageLocation { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>状态(0=正常 1=停用 2=缺货)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
    }
}



