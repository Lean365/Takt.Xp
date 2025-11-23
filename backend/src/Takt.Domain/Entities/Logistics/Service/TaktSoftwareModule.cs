#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSoftwareModule.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 软件模块实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 软件模块实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_service_software_module", "软件模块表")]
    [SugarIndex("ix_module_code", nameof(ModuleCode), OrderByType.Asc, true)]
    [SugarIndex("ix_software_module", nameof(SoftwareId), OrderByType.Asc, nameof(ModuleCode), OrderByType.Asc, false)]
    [SugarIndex("ix_software_id", nameof(SoftwareId), OrderByType.Asc, false)]
    public class TaktSoftwareModule : TaktBaseEntity
    {
        /// <summary>软件ID</summary>
        [SugarColumn(ColumnName = "software_id", ColumnDescription = "软件ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SoftwareId { get; set; }

        /// <summary>模块编号</summary>
        [SugarColumn(ColumnName = "module_code", ColumnDescription = "模块编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ModuleCode { get; set; } = string.Empty;

        /// <summary>模块名称</summary>
        [SugarColumn(ColumnName = "module_name", ColumnDescription = "模块名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TransType { get; set; } = string.Empty;

        /// <summary>模块简称</summary>
        [SugarColumn(ColumnName = "module_short_name", ColumnDescription = "模块简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ModuleShortName { get; set; }

        /// <summary>模块类型(1=核心模块 2=业务模块 3=工具模块 4=报表模块 5=集成模块)</summary>
        [SugarColumn(ColumnName = "module_type", ColumnDescription = "模块类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ModuleType { get; set; } = 2;

        /// <summary>模块分类</summary>
        [SugarColumn(ColumnName = "module_category", ColumnDescription = "模块分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ModuleCategory { get; set; }

        /// <summary>模块描述</summary>
        [SugarColumn(ColumnName = "module_description", ColumnDescription = "模块描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ModuleDescription { get; set; }

        /// <summary>版本号</summary>
        [SugarColumn(ColumnName = "version", ColumnDescription = "版本号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "1.0.0")]
        public string Version { get; set; } = "1.0.0";

        /// <summary>模块状态(0=停用 1=正常 2=开发中 3=测试中 4=维护中)</summary>
        [SugarColumn(ColumnName = "module_status", ColumnDescription = "模块状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ModuleStatus { get; set; } = 1;

        /// <summary>开发语言</summary>
        [SugarColumn(ColumnName = "programming_language", ColumnDescription = "开发语言", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProgrammingLanguage { get; set; }

        /// <summary>技术架构</summary>
        [SugarColumn(ColumnName = "technology_stack", ColumnDescription = "技术架构", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechnologyStack { get; set; }

        /// <summary>数据库类型</summary>
        [SugarColumn(ColumnName = "database_type", ColumnDescription = "数据库类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DatabaseType { get; set; }

        /// <summary>部署方式(1=本地部署 2=云端部署 3=混合部署)</summary>
        [SugarColumn(ColumnName = "deployment_type", ColumnDescription = "部署方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int DeploymentType { get; set; } = 1;

        /// <summary>访问地址</summary>
        [SugarColumn(ColumnName = "access_url", ColumnDescription = "访问地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AccessUrl { get; set; }

        /// <summary>API接口地址</summary>
        [SugarColumn(ColumnName = "api_url", ColumnDescription = "API接口地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApiUrl { get; set; }

        /// <summary>文档地址</summary>
        [SugarColumn(ColumnName = "document_url", ColumnDescription = "文档地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentUrl { get; set; }

        /// <summary>源代码地址</summary>
        [SugarColumn(ColumnName = "source_code_url", ColumnDescription = "源代码地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SourceCodeUrl { get; set; }

        /// <summary>负责人</summary>
        [SugarColumn(ColumnName = "responsible_person", ColumnDescription = "负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePerson { get; set; }

        /// <summary>开发团队</summary>
        [SugarColumn(ColumnName = "development_team", ColumnDescription = "开发团队", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DevelopmentTeam { get; set; }

        /// <summary>开始开发日期</summary>
        [SugarColumn(ColumnName = "development_start_date", ColumnDescription = "开始开发日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DevelopmentStartDate { get; set; }

        /// <summary>计划完成日期</summary>
        [SugarColumn(ColumnName = "planned_completion_date", ColumnDescription = "计划完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>实际完成日期</summary>
        [SugarColumn(ColumnName = "actual_completion_date", ColumnDescription = "实际完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualCompletionDate { get; set; }

        /// <summary>上线日期</summary>
        [SugarColumn(ColumnName = "go_live_date", ColumnDescription = "上线日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? GoLiveDate { get; set; }

        /// <summary>维护周期(天)</summary>
        [SugarColumn(ColumnName = "maintenance_cycle", ColumnDescription = "维护周期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? MaintenanceCycle { get; set; }

        /// <summary>上次维护日期</summary>
        [SugarColumn(ColumnName = "last_maintenance_date", ColumnDescription = "上次维护日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LastMaintenanceDate { get; set; }

        /// <summary>下次维护日期</summary>
        [SugarColumn(ColumnName = "next_maintenance_date", ColumnDescription = "下次维护日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? NextMaintenanceDate { get; set; }

        /// <summary>模块图标</summary>
        [SugarColumn(ColumnName = "module_icon", ColumnDescription = "模块图标", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ModuleIcon { get; set; }

        /// <summary>模块截图</summary>
        [SugarColumn(ColumnName = "module_screenshot", ColumnDescription = "模块截图", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ModuleScreenshot { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 软件导航属性
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(SoftwareId))]
        public TaktSoftware? Software { get; set; }
    }
} 



