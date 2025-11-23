#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLaw.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 法律法规实体类（从国家行政机关下载保存）
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Document
{
    /// <summary>
    /// 法律法规实体类（从国家行政机关下载保存）
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录从国家行政机关下载保存的法律法规，支持树形层级结构，包括法律、法规、规章、标准等
    /// </remarks>
    [SugarTable("Takt_routine_document_law", "法律法规表")]
    [SugarIndex("ix_law_code", nameof(LawCode), OrderByType.Asc, true)]
    [SugarIndex("ix_law_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_law_type", nameof(LawType), OrderByType.Asc, false)]
    [SugarIndex("ix_law_level", nameof(LawLevel), OrderByType.Asc, false)]
    [SugarIndex("ix_law_parent_id", nameof(ParentId), OrderByType.Asc, false)]
    [SugarIndex("ix_law_source", nameof(SourceAuthority), OrderByType.Asc, false)]
    public class TaktLaw : TaktBaseEntity
    {
        /// <summary>父级ID</summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>法规编号</summary>
        [SugarColumn(ColumnName = "law_code", ColumnDescription = "法规编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string LawCode { get; set; } = string.Empty;

        /// <summary>法规名称</summary>
        [SugarColumn(ColumnName = "law_name", ColumnDescription = "法规名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string LawName { get; set; } = string.Empty;

        /// <summary>法规标题</summary>
        [SugarColumn(ColumnName = "law_title", ColumnDescription = "法规标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string LawTitle { get; set; } = string.Empty;

        /// <summary>法规类型(1=法律 2=行政法规 3=部门规章 4=地方性法规 5=地方政府规章 6=标准规范 7=其他)</summary>
        [SugarColumn(ColumnName = "law_type", ColumnDescription = "法规类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int LawType { get; set; } = 1;

        /// <summary>法规级别(1=国家级 2=省级 3=市级 4=县级)</summary>
        [SugarColumn(ColumnName = "law_level", ColumnDescription = "法规级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int LawLevel { get; set; } = 1;

        /// <summary>法规描述</summary>
        [SugarColumn(ColumnName = "law_description", ColumnDescription = "法规描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LawDescription { get; set; }

        /// <summary>法规PDF文件路径</summary>
        [SugarColumn(ColumnName = "law_pdf_path", ColumnDescription = "法规PDF文件路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LawPdfPath { get; set; }

        /// <summary>法规版本</summary>
        [SugarColumn(ColumnName = "law_version", ColumnDescription = "法规版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "1.0")]
        public string LawVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        [SugarColumn(ColumnName = "revision_version", ColumnDescription = "修订版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RevisionVersion { get; set; }

        /// <summary>颁布机关</summary>
        [SugarColumn(ColumnName = "issuing_authority", ColumnDescription = "颁布机关", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssuingAuthority { get; set; }

        /// <summary>颁布日期</summary>
        [SugarColumn(ColumnName = "issue_date", ColumnDescription = "颁布日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? IssueDate { get; set; }

        /// <summary>生效日期</summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        [SugarColumn(ColumnName = "importance_level", ColumnDescription = "重要程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_mandatory", ColumnDescription = "是否强制", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsPublic { get; set; } = 1;



        /// <summary>来源机关</summary>
        [SugarColumn(ColumnName = "source_authority", ColumnDescription = "来源机关", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SourceAuthority { get; set; }

        /// <summary>下载日期</summary>
        [SugarColumn(ColumnName = "download_date", ColumnDescription = "下载日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DownloadDate { get; set; }

        /// <summary>下载人</summary>
        [SugarColumn(ColumnName = "downloader", ColumnDescription = "下载人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Downloader { get; set; }

        /// <summary>原始文件路径</summary>
        [SugarColumn(ColumnName = "original_file_path", ColumnDescription = "原始文件路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OriginalFilePath { get; set; }

        /// <summary>原始文件大小(KB)</summary>
        [SugarColumn(ColumnName = "original_file_size", ColumnDescription = "原始文件大小", ColumnDataType = "int", IsNullable = true)]
        public int? OriginalFileSize { get; set; }

        /// <summary>原始文件格式</summary>
        [SugarColumn(ColumnName = "original_file_format", ColumnDescription = "原始文件格式", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OriginalFileFormat { get; set; }

        /// <summary>相关法规</summary>
        [SugarColumn(ColumnName = "related_laws", ColumnDescription = "相关法规", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedLaws { get; set; }

        /// <summary>相关文件</summary>
        [SugarColumn(ColumnName = "related_files", ColumnDescription = "相关文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        [SugarColumn(ColumnName = "keywords", ColumnDescription = "关键词", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        [SugarColumn(ColumnName = "download_count", ColumnDescription = "下载次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DownloadCount { get; set; } = 0;

        /// <summary>状态。0=草稿，1=已保存，2=已归档。</summary>
        [SugarColumn(ColumnName = "law_status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;



        /// <summary>
        /// 父级法规
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public TaktLaw? Parent { get; set; }

        /// <summary>
        /// 子级法规列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(ParentId))]
        public List<TaktLaw>? Children { get; set; }
    }
}



