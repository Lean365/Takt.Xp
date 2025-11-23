#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTechnicalDocument.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 技术联络文件实体类 (技术联络文档管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 技术联络文件实体类 (技术联络文档管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP DMS 文档管理系统
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_technical_document", "技术联络文件表")]
    [SugarIndex("ix_technical_document_code", nameof(TechnicalDocumentCode), OrderByType.Asc, true)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_document_type", nameof(DocumentType), OrderByType.Asc, false)]
    [SugarIndex("ix_upload_date", nameof(UploadDate), OrderByType.Desc, false)]
    [SugarIndex("ix_file_status", nameof(FileStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_effective_date", nameof(EffectiveDate), OrderByType.Asc, false)]
    [SugarIndex("ix_expiry_date", nameof(ExpiryDate), OrderByType.Asc, false)]

    public class TaktTechnicalDocument : TaktBaseEntity
    {
        
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 技术联络文件编码
        /// </summary>
        [SugarColumn(ColumnName = "technical_document_code", ColumnDescription = "技术联络文件编码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TechnicalDocumentCode { get; set; } = string.Empty;

        /// <summary>
        /// 文件名称
        /// </summary>
        [SugarColumn(ColumnName = "file_name", ColumnDescription = "文件名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件标题
        /// </summary>
        [SugarColumn(ColumnName = "file_title", ColumnDescription = "文件标题", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
        public string FileTitle { get; set; } = string.Empty;

        /// <summary>
        /// 文件类型(1=技术规格书 2=工艺文件 3=质量标准 4=技术变更通知 5=技术问题报告 6=其他)
        /// </summary>
        [SugarColumn(ColumnName = "document_type", ColumnDescription = "文件类型", ColumnDataType = "int", IsNullable = false)]
        public int DocumentType { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        [SugarColumn(ColumnName = "file_extension", ColumnDescription = "文件扩展名", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string FileExtension { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小(字节)
        /// </summary>
        [SugarColumn(ColumnName = "file_size", ColumnDescription = "文件大小(字节)", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
        public long FileSize { get; set; } = 0;

        /// <summary>
        /// 文件路径
        /// </summary>
        [SugarColumn(ColumnName = "file_path", ColumnDescription = "文件路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// 文件URL
        /// </summary>
        [SugarColumn(ColumnName = "file_url", ColumnDescription = "文件URL", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FileUrl { get; set; }

        /// <summary>
        /// 文件版本
        /// </summary>
        [SugarColumn(ColumnName = "file_version", ColumnDescription = "文件版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "1.0")]
        public string FileVersion { get; set; } = "1.0";

        /// <summary>
        /// 文件描述
        /// </summary>
        [SugarColumn(ColumnName = "file_description", ColumnDescription = "文件描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FileDescription { get; set; }

        /// <summary>
        /// 关联产品型号
        /// </summary>
        [SugarColumn(ColumnName = "related_model_code", ColumnDescription = "关联产品型号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedModelCode { get; set; }

        /// <summary>
        /// 关联生产线
        /// </summary>
        [SugarColumn(ColumnName = "related_production_line", ColumnDescription = "关联生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedProductionLine { get; set; }

        /// <summary>
        /// 上传日期
        /// </summary>
        [SugarColumn(ColumnName = "upload_date", ColumnDescription = "上传日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime UploadDate { get; set; }


        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 文件状态(0=草稿 1=已发布 2=已作废 3=已归档)
        /// </summary>
        [SugarColumn(ColumnName = "file_status", ColumnDescription = "文件状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int FileStatus { get; set; } = 0;

        /// <summary>
        /// 是否公开(0=否 1=是)
        /// </summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsPublic { get; set; } = 0;

        /// <summary>
        /// 下载次数
        /// </summary>
        [SugarColumn(ColumnName = "download_count", ColumnDescription = "下载次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DownloadCount { get; set; } = 0;

        /// <summary>
        /// 查看次数
        /// </summary>
        [SugarColumn(ColumnName = "view_count", ColumnDescription = "查看次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ViewCount { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常  1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;
    }
} 



