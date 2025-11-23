#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSoftwareService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 软件服务实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 软件服务实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_service_software_service", "软件服务表")]
    [SugarIndex("ix_software_code", nameof(SoftwareName), OrderByType.Asc, true)]
    [SugarIndex("ix_customer_software", nameof(CustomerCode), OrderByType.Asc, nameof(SoftwareName), OrderByType.Asc, false)]

    public class TaktSoftware : TaktBaseEntity
    {
        /// <summary>客户代码</summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>软件名称</summary>
        [SugarColumn(ColumnName = "software_name", ColumnDescription = "软件名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SoftwareName { get; set; } = string.Empty;

        /// <summary>软件简称</summary>
        [SugarColumn(ColumnName = "software_short_name", ColumnDescription = "软件简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SoftwareShortName { get; set; } = string.Empty;

        /// <summary>软件功能描述</summary>
        [SugarColumn(ColumnName = "software_function_description", ColumnDescription = "软件功能描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SoftwareFunctionDescription { get; set; } = string.Empty;

        /// <summary>软件开发商</summary>
        [SugarColumn(ColumnName = "software_developer", ColumnDescription = "软件开发商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareDeveloper { get; set; }

        /// <summary>软件服务商</summary>
        [SugarColumn(ColumnName = "software_provider", ColumnDescription = "软件服务商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareProvider { get; set; }

        /// <summary>软件著作权号</summary>
        [SugarColumn(ColumnName = "software_copyright_no", ColumnDescription = "软件著作权号", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareCopyrightNo { get; set; }

        /// <summary>软件官网</summary>
        [SugarColumn(ColumnName = "software_official_website", ColumnDescription = "软件官网", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareOfficialWebsite { get; set; }

        /// <summary>软件客服电话</summary>
        [SugarColumn(ColumnName = "software_service_phone", ColumnDescription = "软件客服电话", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareServicePhone { get; set; }

        /// <summary>软件许可</summary>
        [SugarColumn(ColumnName = "software_license", ColumnDescription = "软件许可", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareLicense { get; set; }

        /// <summary>许可到期</summary>
        [SugarColumn(ColumnName = "license_expire_date", ColumnDescription = "许可到期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LicenseExpireDate { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 软件模块导航属性（一对多）
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktSoftwareModule.SoftwareId))]
        public List<TaktSoftwareModule>? SoftwareModules { get; set; }


    }
} 



