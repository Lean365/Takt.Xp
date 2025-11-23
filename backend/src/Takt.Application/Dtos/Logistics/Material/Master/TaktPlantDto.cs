//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPlantDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 工厂数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Material.Master
{
    /// <summary>
    /// 工厂基础DTO
    /// </summary>
    public class TaktPlantDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPlantDto()
        {
            PlantCode = string.Empty;
            PlantName = string.Empty;
            PlantShortName = string.Empty;
            PlantType = 1;
            PlantTypeName = string.Empty;
            PlantAddress = string.Empty;
            PlantAddress1 = string.Empty;
            PlantAddress2 = string.Empty;
            PlantAddress3 = string.Empty;
            PostalCode = string.Empty;
            City = string.Empty;
            Province = string.Empty;
            Country = string.Empty;
            Phone = string.Empty;
            Fax = string.Empty;
            Email = string.Empty;
            ContactPerson = string.Empty;
            PurchaseOrg = string.Empty;
            SalesOrg = string.Empty;
            DistributionChannel = string.Empty;
            ProductGroup = string.Empty;
            FactoryCalendar = string.Empty;
            TimeZone = string.Empty;
            LanguageCode = 0;
            CurrencyCode = 0;
            IsBatchManaged = 0;
            IsSerialManaged = 0;
            IsQualityManaged = 0;
            IsCostManaged = 0;
            IsInventoryManaged = 1;
            IsProductionManaged = 0;
            IsPurchaseManaged = 0;
            IsSalesManaged = 0;
            OrderNum = 0;
            Status = 1;
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PlantId { get; set; }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂名称
        /// </summary>
        public string PlantName { get; set; } = string.Empty;

        /// <summary>
        /// 工厂简称
        /// </summary>
        public string? PlantShortName { get; set; }

        /// <summary>
        /// 工厂类型(1=生产工厂 2=销售工厂 3=采购工厂 4=物流中心 5=其他)
        /// </summary>
        public int PlantType { get; set; } = 1;

        /// <summary>
        /// 工厂类型名称
        /// </summary>
        public string? PlantTypeName { get; set; }

        /// <summary>
        /// 工厂地址
        /// </summary>
        public string? PlantAddress { get; set; }

        /// <summary>
        /// 工厂地址1
        /// </summary>
        public string? PlantAddress1 { get; set; }

        /// <summary>
        /// 工厂地址2
        /// </summary>
        public string? PlantAddress2 { get; set; }

        /// <summary>
        /// 工厂地址3
        /// </summary>
        public string? PlantAddress3 { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string? Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 采购组织
        /// </summary>
        public string? PurchaseOrg { get; set; }

        /// <summary>
        /// 销售组织
        /// </summary>
        public string? SalesOrg { get; set; }

        /// <summary>
        /// 分销渠道
        /// </summary>
        public string? DistributionChannel { get; set; }

        /// <summary>
        /// 产品组
        /// </summary>
        public string? ProductGroup { get; set; }

        /// <summary>
        /// 工厂日历
        /// </summary>
        public string? FactoryCalendar { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// 语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)
        /// </summary>
        public int LanguageCode { get; set; } = 0;

        /// <summary>
        /// 货币代码(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)
        /// </summary>
        public int CurrencyCode { get; set; } = 0;

        /// <summary>
        /// 是否启用批次管理
        /// </summary>
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用序列号管理
        /// </summary>
        public int IsSerialManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用质量管理
        /// </summary>
        public int IsQualityManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用成本管理
        /// </summary>
        public int IsCostManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用库存管理
        /// </summary>
        public int IsInventoryManaged { get; set; } = 1;

        /// <summary>
        /// 是否启用生产管理
        /// </summary>
        public int IsProductionManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用采购管理
        /// </summary>
        public int IsPurchaseManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用销售管理
        /// </summary>
        public int IsSalesManaged { get; set; } = 0;

        /// <summary>
        /// 工厂启用日期
        /// </summary>
        public DateTime? PlantStartDate { get; set; }

        /// <summary>
        /// 工厂停用日期
        /// </summary>
        public DateTime? PlantEndDate { get; set; }

        /// <summary>
        /// 工厂描述
        /// </summary>
        public string? PlantDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态(0=停用 1=正常)
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工厂查询DTO
    /// </summary>
    public class TaktPlantQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPlantQueryDto()
        {
            PlantCode = string.Empty;
            PlantName = string.Empty;
            PlantShortName = string.Empty;
            PlantTypeName = string.Empty;
            City = string.Empty;
            Province = string.Empty;
            Country = string.Empty;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 工厂名称
        /// </summary>
        public string? PlantName { get; set; }

        /// <summary>
        /// 工厂简称
        /// </summary>
        public string? PlantShortName { get; set; }

        /// <summary>
        /// 工厂类型(1=生产工厂 2=销售工厂 3=采购工厂 4=物流中心 5=其他)
        /// </summary>
        public int? PlantType { get; set; }

        /// <summary>
        /// 工厂类型名称
        /// </summary>
        public string? PlantTypeName { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string? Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// 状态（0停用 1正常）
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 工厂创建DTO
    /// </summary>
    public class TaktPlantCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPlantCreateDto()
        {
            PlantCode = string.Empty;
            PlantName = string.Empty;
            PlantShortName = string.Empty;
            PlantType = 1;
            PlantTypeName = string.Empty;
            PlantAddress = string.Empty;
            PlantAddress1 = string.Empty;
            PlantAddress2 = string.Empty;
            PlantAddress3 = string.Empty;
            PostalCode = string.Empty;
            City = string.Empty;
            Province = string.Empty;
            Country = string.Empty;
            Phone = string.Empty;
            Fax = string.Empty;
            Email = string.Empty;
            ContactPerson = string.Empty;
            PurchaseOrg = string.Empty;
            SalesOrg = string.Empty;
            DistributionChannel = string.Empty;
            ProductGroup = string.Empty;
            FactoryCalendar = string.Empty;
            TimeZone = string.Empty;
            LanguageCode = 0;
            CurrencyCode = 0;
            IsBatchManaged = 0;
            IsSerialManaged = 0;
            IsQualityManaged = 0;
            IsCostManaged = 0;
            IsInventoryManaged = 1;
            IsProductionManaged = 0;
            IsPurchaseManaged = 0;
            IsSalesManaged = 0;
            OrderNum = 0;
            Status = 1;
            Remark = string.Empty;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂名称
        /// </summary>
        public string PlantName { get; set; } = string.Empty;

        /// <summary>
        /// 工厂简称
        /// </summary>
        public string? PlantShortName { get; set; }

        /// <summary>
        /// 工厂类型(1=生产工厂 2=销售工厂 3=采购工厂 4=物流中心 5=其他)
        /// </summary>
        public int PlantType { get; set; } = 1;

        /// <summary>
        /// 工厂类型名称
        /// </summary>
        public string? PlantTypeName { get; set; }

        /// <summary>
        /// 工厂地址
        /// </summary>
        public string? PlantAddress { get; set; }

        /// <summary>
        /// 工厂地址1
        /// </summary>
        public string? PlantAddress1 { get; set; }

        /// <summary>
        /// 工厂地址2
        /// </summary>
        public string? PlantAddress2 { get; set; }

        /// <summary>
        /// 工厂地址3
        /// </summary>
        public string? PlantAddress3 { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string? Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 采购组织
        /// </summary>
        public string? PurchaseOrg { get; set; }

        /// <summary>
        /// 销售组织
        /// </summary>
        public string? SalesOrg { get; set; }

        /// <summary>
        /// 分销渠道
        /// </summary>
        public string? DistributionChannel { get; set; }

        /// <summary>
        /// 产品组
        /// </summary>
        public string? ProductGroup { get; set; }

        /// <summary>
        /// 工厂日历
        /// </summary>
        public string? FactoryCalendar { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// 语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)
        /// </summary>
        public int LanguageCode { get; set; } = 0;

        /// <summary>
        /// 货币代码(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)
        /// </summary>
        public int CurrencyCode { get; set; } = 0;

        /// <summary>
        /// 是否启用批次管理
        /// </summary>
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用序列号管理
        /// </summary>
        public int IsSerialManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用质量管理
        /// </summary>
        public int IsQualityManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用成本管理
        /// </summary>
        public int IsCostManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用库存管理
        /// </summary>
        public int IsInventoryManaged { get; set; } = 1;

        /// <summary>
        /// 是否启用生产管理
        /// </summary>
        public int IsProductionManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用采购管理
        /// </summary>
        public int IsPurchaseManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用销售管理
        /// </summary>
        public int IsSalesManaged { get; set; } = 0;

        /// <summary>
        /// 工厂启用日期
        /// </summary>
        public DateTime? PlantStartDate { get; set; }

        /// <summary>
        /// 工厂停用日期
        /// </summary>
        public DateTime? PlantEndDate { get; set; }

        /// <summary>
        /// 工厂描述
        /// </summary>
        public string? PlantDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态(0=停用 1=正常)
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工厂更新DTO
    /// </summary>
    public class TaktPlantUpdateDto : TaktPlantCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPlantUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PlantId { get; set; }
    }

    /// <summary>
    /// 工厂导入DTO
    /// </summary>
    public class TaktPlantImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPlantImportDto()
        {
            PlantCode = string.Empty;
            PlantName = string.Empty;
            PlantShortName = string.Empty;
            PlantType = 1;
            PlantTypeName = string.Empty;
            PlantAddress = string.Empty;
            PlantAddress1 = string.Empty;
            PlantAddress2 = string.Empty;
            PlantAddress3 = string.Empty;
            PostalCode = string.Empty;
            City = string.Empty;
            Province = string.Empty;
            Country = string.Empty;
            Phone = string.Empty;
            Fax = string.Empty;
            Email = string.Empty;
            ContactPerson = string.Empty;
            PurchaseOrg = string.Empty;
            SalesOrg = string.Empty;
            DistributionChannel = string.Empty;
            ProductGroup = string.Empty;
            FactoryCalendar = string.Empty;
            TimeZone = string.Empty;
            LanguageCode = 0;
            CurrencyCode = 0;
            IsBatchManaged = 0;
            IsSerialManaged = 0;
            IsQualityManaged = 0;
            IsCostManaged = 0;
            IsInventoryManaged = 1;
            IsProductionManaged = 0;
            IsPurchaseManaged = 0;
            IsSalesManaged = 0;
            OrderNum = 0;
            Status = 1;
            Remark = string.Empty;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂名称
        /// </summary>
        public string PlantName { get; set; } = string.Empty;

        /// <summary>
        /// 工厂简称
        /// </summary>
        public string? PlantShortName { get; set; }

        /// <summary>
        /// 工厂类型(1=生产工厂 2=销售工厂 3=采购工厂 4=物流中心 5=其他)
        /// </summary>
        public int PlantType { get; set; } = 1;

        /// <summary>
        /// 工厂类型名称
        /// </summary>
        public string? PlantTypeName { get; set; }

        /// <summary>
        /// 工厂地址
        /// </summary>
        public string? PlantAddress { get; set; }

        /// <summary>
        /// 工厂地址1
        /// </summary>
        public string? PlantAddress1 { get; set; }

        /// <summary>
        /// 工厂地址2
        /// </summary>
        public string? PlantAddress2 { get; set; }

        /// <summary>
        /// 工厂地址3
        /// </summary>
        public string? PlantAddress3 { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string? Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 采购组织
        /// </summary>
        public string? PurchaseOrg { get; set; }

        /// <summary>
        /// 销售组织
        /// </summary>
        public string? SalesOrg { get; set; }

        /// <summary>
        /// 分销渠道
        /// </summary>
        public string? DistributionChannel { get; set; }

        /// <summary>
        /// 产品组
        /// </summary>
        public string? ProductGroup { get; set; }

        /// <summary>
        /// 工厂日历
        /// </summary>
        public string? FactoryCalendar { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// 语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)
        /// </summary>
        public int LanguageCode { get; set; } = 0;

        /// <summary>
        /// 货币代码(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)
        /// </summary>
        public int CurrencyCode { get; set; } = 0;

        /// <summary>
        /// 是否启用批次管理
        /// </summary>
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用序列号管理
        /// </summary>
        public int IsSerialManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用质量管理
        /// </summary>
        public int IsQualityManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用成本管理
        /// </summary>
        public int IsCostManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用库存管理
        /// </summary>
        public int IsInventoryManaged { get; set; } = 1;

        /// <summary>
        /// 是否启用生产管理
        /// </summary>
        public int IsProductionManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用采购管理
        /// </summary>
        public int IsPurchaseManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用销售管理
        /// </summary>
        public int IsSalesManaged { get; set; } = 0;

        /// <summary>
        /// 工厂启用日期
        /// </summary>
        public DateTime? PlantStartDate { get; set; }

        /// <summary>
        /// 工厂停用日期
        /// </summary>
        public DateTime? PlantEndDate { get; set; }

        /// <summary>
        /// 工厂描述
        /// </summary>
        public string? PlantDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态(0=停用 1=正常)
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工厂导出DTO
    /// </summary>
    public class TaktPlantExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPlantExportDto()
        {
            PlantCode = string.Empty;
            PlantName = string.Empty;
            PlantShortName = string.Empty;
            PlantType = 1;
            PlantTypeName = string.Empty;
            PlantAddress = string.Empty;
            City = string.Empty;
            Province = string.Empty;
            Country = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            ContactPerson = string.Empty;
            Status = 1;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂名称
        /// </summary>
        public string PlantName { get; set; } = string.Empty;

        /// <summary>
        /// 工厂简称
        /// </summary>
        public string? PlantShortName { get; set; }

        /// <summary>
        /// 工厂类型(1=生产工厂 2=销售工厂 3=采购工厂 4=物流中心 5=其他)
        /// </summary>
        public int PlantType { get; set; } = 1;

        /// <summary>
        /// 工厂类型名称
        /// </summary>
        public string? PlantTypeName { get; set; }

        /// <summary>
        /// 工厂地址
        /// </summary>
        public string? PlantAddress { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string? Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 状态(0=停用 1=正常)
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 状态文本
        /// </summary>
        public string StatusText { get; set; } = string.Empty;
    }

    /// <summary>
    /// 工厂模板DTO
    /// </summary>
    public class TaktPlantTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPlantTemplateDto()
        {
            PlantCode = string.Empty;
            PlantName = string.Empty;
            PlantShortName = string.Empty;
            PlantType = 1;
            PlantTypeName = string.Empty;
            PlantAddress = string.Empty;
            PlantAddress1 = string.Empty;
            PlantAddress2 = string.Empty;
            PlantAddress3 = string.Empty;
            PostalCode = string.Empty;
            City = string.Empty;
            Province = string.Empty;
            Country = string.Empty;
            Phone = string.Empty;
            Fax = string.Empty;
            Email = string.Empty;
            ContactPerson = string.Empty;
            PurchaseOrg = string.Empty;
            SalesOrg = string.Empty;
            DistributionChannel = string.Empty;
            ProductGroup = string.Empty;
            FactoryCalendar = string.Empty;
            TimeZone = string.Empty;
            LanguageCode = 0;
            CurrencyCode = 0;
            IsBatchManaged = 0;
            IsSerialManaged = 0;
            IsQualityManaged = 0;
            IsCostManaged = 0;
            IsInventoryManaged = 1;
            IsProductionManaged = 0;
            IsPurchaseManaged = 0;
            IsSalesManaged = 0;
            OrderNum = 0;
            Status = 1;
            Remark = string.Empty;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂名称
        /// </summary>
        public string PlantName { get; set; } = string.Empty;

        /// <summary>
        /// 工厂简称
        /// </summary>
        public string? PlantShortName { get; set; }

        /// <summary>
        /// 工厂类型(1=生产工厂 2=销售工厂 3=采购工厂 4=物流中心 5=其他)
        /// </summary>
        public int PlantType { get; set; } = 1;

        /// <summary>
        /// 工厂类型名称
        /// </summary>
        public string? PlantTypeName { get; set; }

        /// <summary>
        /// 工厂地址
        /// </summary>
        public string? PlantAddress { get; set; }

        /// <summary>
        /// 工厂地址1
        /// </summary>
        public string? PlantAddress1 { get; set; }

        /// <summary>
        /// 工厂地址2
        /// </summary>
        public string? PlantAddress2 { get; set; }

        /// <summary>
        /// 工厂地址3
        /// </summary>
        public string? PlantAddress3 { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string? Province { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 采购组织
        /// </summary>
        public string? PurchaseOrg { get; set; }

        /// <summary>
        /// 销售组织
        /// </summary>
        public string? SalesOrg { get; set; }

        /// <summary>
        /// 分销渠道
        /// </summary>
        public string? DistributionChannel { get; set; }

        /// <summary>
        /// 产品组
        /// </summary>
        public string? ProductGroup { get; set; }

        /// <summary>
        /// 工厂日历
        /// </summary>
        public string? FactoryCalendar { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// 语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)
        /// </summary>
        public int LanguageCode { get; set; } = 0;

        /// <summary>
        /// 货币代码(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)
        /// </summary>
        public int CurrencyCode { get; set; } = 0;

        /// <summary>
        /// 是否启用批次管理
        /// </summary>
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用序列号管理
        /// </summary>
        public int IsSerialManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用质量管理
        /// </summary>
        public int IsQualityManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用成本管理
        /// </summary>
        public int IsCostManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用库存管理
        /// </summary>
        public int IsInventoryManaged { get; set; } = 1;

        /// <summary>
        /// 是否启用生产管理
        /// </summary>
        public int IsProductionManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用采购管理
        /// </summary>
        public int IsPurchaseManaged { get; set; } = 0;

        /// <summary>
        /// 是否启用销售管理
        /// </summary>
        public int IsSalesManaged { get; set; } = 0;

        /// <summary>
        /// 工厂启用日期
        /// </summary>
        public DateTime? PlantStartDate { get; set; }

        /// <summary>
        /// 工厂停用日期
        /// </summary>
        public DateTime? PlantEndDate { get; set; }

        /// <summary>
        /// 工厂描述
        /// </summary>
        public string? PlantDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态(0=停用 1=正常)
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 状态文本
        /// </summary>
        public string StatusText { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 工厂状态更新DTO
    /// </summary>
    public class TaktPlantStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPlantStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PlantId { get; set; }

        /// <summary>
        /// 状态（0停用 1正常）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}


