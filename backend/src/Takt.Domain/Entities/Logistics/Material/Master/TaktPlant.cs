#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPlant.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 工厂实体类 (基于SAP MM物料管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Material.Master
{
    /// <summary>
    /// 工厂实体类 (基于SAP MM物料管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP MM 物料管理模块
    /// </remarks>
    [SugarTable("Takt_logistics_plant", "工厂信息")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, true)]
    public class TaktPlant : TaktBaseEntity
    {
        /// <summary>
        /// 工厂编码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂编码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂名称
        /// </summary>
        [SugarColumn(ColumnName = "plant_name", ColumnDescription = "工厂名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantName { get; set; } = string.Empty;

        /// <summary>
        /// 工厂简称
        /// </summary>
        [SugarColumn(ColumnName = "plant_short_name", ColumnDescription = "工厂简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantShortName { get; set; }

        /// <summary>
        /// 工厂类型(1=生产工厂 2=销售工厂 3=采购工厂 4=物流中心 5=其他)
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "plant_type", ColumnDescription = "工厂类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PlantType { get; set; } = 1;

        /// <summary>
        /// 工厂类型名称
        /// </summary>
        [SugarColumn(ColumnName = "plant_type_name", ColumnDescription = "工厂类型名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantTypeName { get; set; }

        /// <summary>
        /// 工厂地址
        /// </summary>
        [SugarColumn(ColumnName = "plant_address", ColumnDescription = "工厂地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantAddress { get; set; }

        /// <summary>
        /// 工厂地址1
        /// </summary>
        [SugarColumn(ColumnName = "plant_address1", ColumnDescription = "工厂地址1", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantAddress1 { get; set; }

        /// <summary>
        /// 工厂地址2
        /// </summary>
        [SugarColumn(ColumnName = "plant_address2", ColumnDescription = "工厂地址2", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantAddress2 { get; set; }

        /// <summary>
        /// 工厂地址3
        /// </summary>
        [SugarColumn(ColumnName = "plant_address3", ColumnDescription = "工厂地址3", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantAddress3 { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [SugarColumn(ColumnName = "postal_code", ColumnDescription = "邮政编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PostalCode { get; set; }

        /// <summary>
        /// 城市
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "city", ColumnDescription = "城市", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? City { get; set; }

        /// <summary>
        /// 省份
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "province", ColumnDescription = "省份", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Province { get; set; }

        /// <summary>
        /// 国家
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "country", ColumnDescription = "国家", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Country { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [SugarColumn(ColumnName = "phone", ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [SugarColumn(ColumnName = "fax", ColumnDescription = "传真", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Email { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [SugarColumn(ColumnName = "contact_person", ColumnDescription = "联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 采购组织
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "purchase_org", ColumnDescription = "采购组织", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseOrg { get; set; }

        /// <summary>
        /// 销售组织
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "sales_org", ColumnDescription = "销售组织", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SalesOrg { get; set; }

        /// <summary>
        /// 分销渠道
        /// </summary>
        [SugarColumn(ColumnName = "distribution_channel", ColumnDescription = "分销渠道", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DistributionChannel { get; set; }

        /// <summary>
        /// 产品组
        /// </summary>
        [SugarColumn(ColumnName = "product_group", ColumnDescription = "产品组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductGroup { get; set; }

        /// <summary>
        /// 工厂日历
        /// </summary>
        [SugarColumn(ColumnName = "factory_calendar", ColumnDescription = "工厂日历", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FactoryCalendar { get; set; }

        /// <summary>
        /// 时区
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "time_zone", ColumnDescription = "时区", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TimeZone { get; set; }

        /// <summary>
        /// 语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "language_code", ColumnDescription = "语言代码", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LanguageCode { get; set; } = 0;

        /// <summary>
        /// 货币代码(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "currency_code", ColumnDescription = "货币代码", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CurrencyCode { get; set; } = 0;

        /// <summary>
        /// 是否启用批次管理
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "is_batch_managed", ColumnDescription = "是否启用批次管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用序列号管理
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "is_serial_managed", ColumnDescription = "是否启用序列号管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsSerialManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用质量管理
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "is_quality_managed", ColumnDescription = "是否启用质量管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsQualityManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用成本管理
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "is_cost_managed", ColumnDescription = "是否启用成本管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCostManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用库存管理
        /// </summary>
        [SugarColumn(ColumnName = "is_inventory_managed", ColumnDescription = "是否启用库存管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsInventoryManaged { get; set; } = 1;

        /// <summary>
        /// 是否启用生产管理
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "is_production_managed", ColumnDescription = "是否启用生产管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsProductionManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用采购管理
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "is_purchase_managed", ColumnDescription = "是否启用采购管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsPurchaseManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用销售管理
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "is_sales_managed", ColumnDescription = "是否启用销售管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsSalesManaged { get; set; } = 0;

        /// <summary>
        /// 成立日期
        /// </summary>
        [SugarColumn(ColumnName = "establishment_date", ColumnDescription = "成立日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>
        /// 注销日期
        /// </summary>
        [SugarColumn(ColumnName = "dissolution_date", ColumnDescription = "注销日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DissolutionDate { get; set; }

        /// <summary>
        /// 工厂描述
        /// </summary>
        [SugarColumn(ColumnName = "plant_description", ColumnDescription = "工厂描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
} 



