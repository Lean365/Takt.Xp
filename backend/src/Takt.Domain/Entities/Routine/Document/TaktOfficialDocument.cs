#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktOfficialDocument.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 公文文档实体类（树形结构）
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Document
{
    /// <summary>
    /// 公文文档实体类（树形结构）
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录公司公文文档，支持树形层级结构，包括通知、公告、决定、报告等
    /// </remarks>
    [SugarTable("Takt_routine_document_official", "公文文档表")]
    [SugarIndex("ix_official_document_code", nameof(DocumentCode), OrderByType.Asc, true)]
    [SugarIndex("ix_official_document_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_official_document_type", nameof(DocumentType), OrderByType.Asc, false)]
    [SugarIndex("ix_official_document_level", nameof(DocumentLevel), OrderByType.Asc, false)]
    [SugarIndex("ix_official_document_parent_id", nameof(ParentId), OrderByType.Asc, false)]
    public class TaktOfficialDocument : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>父级ID</summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>文档编号</summary>
        [SugarColumn(ColumnName = "document_code", ColumnDescription = "文档编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>文档文号</summary>
        [SugarColumn(ColumnName = "document_number", ColumnDescription = "文档文号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentNumber { get; set; }

        /// <summary>文档标题</summary>
        [SugarColumn(ColumnName = "document_title", ColumnDescription = "文档标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentTitle { get; set; } = string.Empty;

        /// <summary>文档类型(1=通知 2=公告 3=决定 4=报告 5=请示 6=批复 7=函 8=纪要 9=其他)</summary>
        [SugarColumn(ColumnName = "document_type", ColumnDescription = "文档类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int DocumentType { get; set; } = 1;

        /// <summary>文档级别(1=公司级 2=部门级 3=科室级 4=班组级)</summary>
        [SugarColumn(ColumnName = "document_level", ColumnDescription = "文档级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int DocumentLevel { get; set; } = 1;

        /// <summary>文档描述</summary>
        [SugarColumn(ColumnName = "document_description", ColumnDescription = "文档描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentDescription { get; set; }

        /// <summary>文档内容</summary>
        [SugarColumn(ColumnName = "document_content", ColumnDescription = "文档内容", Length = 8000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentContent { get; set; }

        /// <summary>文档PDF文件路径</summary>
        [SugarColumn(ColumnName = "document_pdf_path", ColumnDescription = "文档PDF文件路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentPdfPath { get; set; }

        /// <summary>文档版本</summary>
        [SugarColumn(ColumnName = "document_version", ColumnDescription = "文档版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "1.0")]
        public string DocumentVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        [SugarColumn(ColumnName = "revision_version", ColumnDescription = "修订版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        [SugarColumn(ColumnName = "publish_date", ColumnDescription = "发布日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PublishDate { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        [SugarColumn(ColumnName = "importance_level", ColumnDescription = "重要程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_mandatory", ColumnDescription = "是否强制", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsPublic { get; set; } = 1;

        /// <summary>发文部门</summary>
        [SugarColumn(ColumnName = "issuing_department", ColumnDescription = "发文部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssuingDepartment { get; set; }

        /// <summary>发文日期</summary>
        [SugarColumn(ColumnName = "issue_date", ColumnDescription = "发文日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? IssueDate { get; set; }

        /// <summary>发布方式(1=内部发布 2=全员发布 3=指定部门发布)</summary>
        [SugarColumn(ColumnName = "publish_method", ColumnDescription = "发布方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        [SugarColumn(ColumnName = "publish_scope", ColumnDescription = "发布范围", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PublishScope { get; set; }

        /// <summary>相关文档</summary>
        [SugarColumn(ColumnName = "related_documents", ColumnDescription = "相关文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDocuments { get; set; }

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

        /// <summary>状态。0=草稿，1=已发布，2=已作废，3=已归档。</summary>
        [SugarColumn(ColumnName = "document_status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;





        /// <summary>
        /// 父级文档
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public TaktOfficialDocument? Parent { get; set; }

        /// <summary>
        /// 子级文档列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(ParentId))]
        public List<TaktOfficialDocument>? Children { get; set; }
    }
} 



