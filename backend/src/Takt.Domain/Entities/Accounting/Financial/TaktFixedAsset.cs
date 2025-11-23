#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktFixedAsset.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 固定资产实体类 (基于SAP FI固定资产管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Accounting.Financial
{
    /// <summary>
    /// 固定资产实体类 (基于SAP FI固定资产管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-AA 固定资产管理
    /// </remarks>
    [SugarTable("Takt_accounting_financial_fixed_asset", "固定资产")]
    [SugarIndex("ix_asset_code", nameof(AssetCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktFixedAsset : TaktBaseEntity
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 资产编码
        /// </summary>
        [SugarColumn(ColumnName = "asset_code", ColumnDescription = "资产编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string AssetCode { get; set; } = string.Empty;

        /// <summary>
        /// 资产名称
        /// </summary>
        [SugarColumn(ColumnName = "asset_name", ColumnDescription = "资产名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string AssetName { get; set; } = string.Empty;

        /// <summary>
        /// 资产类别
        /// </summary>
        [SugarColumn(ColumnName = "asset_category", ColumnDescription = "资产类别", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string AssetCategory { get; set; } = string.Empty;

        /// <summary>
        /// 资产子类别
        /// </summary>
        [SugarColumn(ColumnName = "asset_sub_category", ColumnDescription = "资产子类别", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssetSubCategory { get; set; }

        /// <summary>
        /// 资产规格型号
        /// </summary>
        [SugarColumn(ColumnName = "asset_spec", ColumnDescription = "资产规格型号", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssetSpec { get; set; }

        /// <summary>
        /// 资产品牌
        /// </summary>
        [SugarColumn(ColumnName = "asset_brand", ColumnDescription = "资产品牌", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssetBrand { get; set; }

        /// <summary>
        /// 资产型号
        /// </summary>
        [SugarColumn(ColumnName = "asset_model", ColumnDescription = "资产型号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssetModel { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "计量单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 数量
        /// </summary>
        [SugarColumn(ColumnName = "quantity", ColumnDescription = "数量", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// 原值
        /// </summary>
        [SugarColumn(ColumnName = "original_value", ColumnDescription = "原值", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal OriginalValue { get; set; }

        /// <summary>
        /// 累计折旧
        /// </summary>
        [SugarColumn(ColumnName = "accumulated_depreciation", ColumnDescription = "累计折旧", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal AccumulatedDepreciation { get; set; }

        /// <summary>
        /// 净值
        /// </summary>
        [SugarColumn(ColumnName = "net_value", ColumnDescription = "净值", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal NetValue { get; set; }

        /// <summary>
        /// 残值
        /// </summary>
        [SugarColumn(ColumnName = "salvage_value", ColumnDescription = "残值", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? SalvageValue { get; set; }

        /// <summary>
        /// 折旧基数
        /// </summary>
        [SugarColumn(ColumnName = "depreciation_base", ColumnDescription = "折旧基数", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? DepreciationBase { get; set; }

        /// <summary>
        /// 使用部门ID
        /// </summary>
        [SugarColumn(ColumnName = "dept_id", ColumnDescription = "使用部门ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DeptId { get; set; }

        /// <summary>
        /// 使用部门编码
        /// </summary>
        [SugarColumn(ColumnName = "dept_code", ColumnDescription = "使用部门编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeptCode { get; set; }

        /// <summary>
        /// 使用部门名称
        /// </summary>
        [SugarColumn(ColumnName = "dept_name", ColumnDescription = "使用部门名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeptName { get; set; }

        /// <summary>
        /// 使用人ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "使用人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? UserId { get; set; }

        /// <summary>
        /// 使用人编码
        /// </summary>
        [SugarColumn(ColumnName = "user_code", ColumnDescription = "使用人编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UserCode { get; set; }

        /// <summary>
        /// 使用人姓名
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "使用人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UserName { get; set; }

        /// <summary>
        /// 存放地点
        /// </summary>
        [SugarColumn(ColumnName = "location", ColumnDescription = "存放地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Location { get; set; }

        /// <summary>
        /// 资产位置
        /// </summary>
        [SugarColumn(ColumnName = "asset_location", ColumnDescription = "资产位置", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssetLocation { get; set; }

        /// <summary>
        /// 购置日期
        /// </summary>
        [SugarColumn(ColumnName = "purchase_date", ColumnDescription = "购置日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// 开始使用日期
        /// </summary>
        [SugarColumn(ColumnName = "start_use_date", ColumnDescription = "开始使用日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime StartUseDate { get; set; }

        /// <summary>
        /// 预计使用年限
        /// </summary>
        [SugarColumn(ColumnName = "expected_life", ColumnDescription = "预计使用年限", ColumnDataType = "int", IsNullable = false)]
        public int ExpectedLife { get; set; }

        /// <summary>
        /// 已使用年限
        /// </summary>
        [SugarColumn(ColumnName = "used_life", ColumnDescription = "已使用年限", ColumnDataType = "int", IsNullable = true)]
        public int? UsedLife { get; set; }

        /// <summary>
        /// 剩余使用年限
        /// </summary>
        [SugarColumn(ColumnName = "remaining_life", ColumnDescription = "剩余使用年限", ColumnDataType = "int", IsNullable = true)]
        public int? RemainingLife { get; set; }

        /// <summary>
        /// 折旧方法
        /// </summary>
        [SugarColumn(ColumnName = "depreciation_method", ColumnDescription = "折旧方法", ColumnDataType = "int", IsNullable = false)]
        public int DepreciationMethod { get; set; }

        /// <summary>
        /// 折旧率
        /// </summary>
        [SugarColumn(ColumnName = "depreciation_rate", ColumnDescription = "折旧率", ColumnDataType = "decimal(18,4)", IsNullable = false)]
        public decimal DepreciationRate { get; set; }

        /// <summary>
        /// 月折旧额
        /// </summary>
        [SugarColumn(ColumnName = "monthly_depreciation", ColumnDescription = "月折旧额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? MonthlyDepreciation { get; set; }

        /// <summary>
        /// 年折旧额
        /// </summary>
        [SugarColumn(ColumnName = "yearly_depreciation", ColumnDescription = "年折旧额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? YearlyDepreciation { get; set; }

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
        /// 合同编号
        /// </summary>
        [SugarColumn(ColumnName = "contract_number", ColumnDescription = "合同编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractNumber { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        [SugarColumn(ColumnName = "invoice_number", ColumnDescription = "发票号码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InvoiceNumber { get; set; }

        /// <summary>
        /// 保修期
        /// </summary>
        [SugarColumn(ColumnName = "warranty_period", ColumnDescription = "保修期", ColumnDataType = "int", IsNullable = true)]
        public int? WarrantyPeriod { get; set; }

        /// <summary>
        /// 保修到期日期
        /// </summary>
        [SugarColumn(ColumnName = "warranty_end_date", ColumnDescription = "保修到期日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? WarrantyEndDate { get; set; }

        /// <summary>
        /// 报废日期
        /// </summary>
        [SugarColumn(ColumnName = "scrap_date", ColumnDescription = "报废日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ScrapDate { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=闲置 1=在用 2=维修 3=报废 4=出租 5=出售)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
}



