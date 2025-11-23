#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMedicine.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 医务室药品库存实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Medical
{
    /// <summary>
    /// 医务室药品库存实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录医务室药品库存信息，适用于企业内部医务管理
    /// </remarks>
    [SugarTable("Takt_routine_medical_medicine", "医务室药品库存")]
    [SugarIndex("ix_medical_medicine_code", nameof(MedicineCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_medical_medicine", nameof(CompanyCode), OrderByType.Asc, nameof(MedicineCode), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_medicine_category", nameof(CategoryId), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_medicine_status", nameof(Status), OrderByType.Asc, false)]
    public class TaktMedicine : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>药品编号</summary>
        [SugarColumn(ColumnName = "medicine_code", ColumnDescription = "药品编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MedicineCode { get; set; } = string.Empty;

        /// <summary>药品名称</summary>
        [SugarColumn(ColumnName = "medicine_name", ColumnDescription = "药品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MedicineName { get; set; } = string.Empty;

        /// <summary>药品分类ID</summary>
        [SugarColumn(ColumnName = "category_id", ColumnDescription = "药品分类ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CategoryId { get; set; }

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

        /// <summary>生产日期</summary>
        [SugarColumn(ColumnName = "production_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ProductionDate { get; set; }

        /// <summary>有效期</summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "有效期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>是否处方药(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_prescription", ColumnDescription = "是否处方药", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsPrescription { get; set; } = 0;

        /// <summary>是否医保药品(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_medical_insurance", ColumnDescription = "是否医保药品", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsMedicalInsurance { get; set; } = 0;

        /// <summary>药品说明</summary>
        [SugarColumn(ColumnName = "description", ColumnDescription = "药品说明", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Description { get; set; }

        /// <summary>使用方法</summary>
        [SugarColumn(ColumnName = "usage_method", ColumnDescription = "使用方法", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UsageMethod { get; set; }

        /// <summary>注意事项</summary>
        [SugarColumn(ColumnName = "precautions", ColumnDescription = "注意事项", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Precautions { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>状态(0=正常 1=停用 2=缺货 3=过期)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
    }
} 



